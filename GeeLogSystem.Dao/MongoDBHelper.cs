using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GeepLogSystem.Dao
{
    /// <summary>
    /// 数据库助手类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MongoDBHelper<TEntity> where TEntity : GeepLogModel
    {
        /// <summary>
        /// 集合名称
        /// </summary>
        protected IMongoCollection<TEntity> Collection { get; private set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected static string mongoDbString = Help.GetConfigValueForNull("MongoDB");

        /// <summary>
        /// 数据库连接串实体
        /// </summary>
        protected static MongoUrl mongoUrl = new MongoUrl(mongoDbString);

        /// <summary>
        /// 数据库连接实体
        /// </summary>
        protected static IMongoClient client = new MongoClient(mongoDbString);

        /// <summary>
        /// 数据库
        /// </summary>
        //protected static IMongoDatabase database = client.GetDatabase(mongoUrl.DatabaseName);
        protected static IMongoDatabase database = client.GetDatabase("log");

        public MongoDBHelper()
        {
            Collection = database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower());
        }

        /// <summary>
        /// 异步插入一条数据
        /// </summary>
        /// <param name="entity">插入实体</param>
        /// <returns></returns>
        public async Task InsterOneAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// 异步插入多条数据
        /// </summary>
        /// <param name="entity">插入实体集合</param>
        /// <returns></returns>
        public async Task InsterManyAsync(IEnumerable<TEntity> entity)
        {
            await Collection.InsertManyAsync(entity);
        }

        /// <summary>
        /// 异步替换一条数据
        /// </summary>
        /// <param name="entity">替换数据实体</param>
        /// <returns></returns>
        public async Task ReplaceOneAsync(TEntity entity)
        {
            await Collection.ReplaceOneAsync(a => a._id == entity._id, entity);
        }

        /// <summary>
        /// 异步更新一条数据
        /// </summary>
        /// <param name="filter">筛选器</param>
        /// <param name="updateEntity">更新器</param>
        /// <returns></returns>
        public async Task UpdateOneAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateEntity)
        {
            await Collection.UpdateOneAsync(filter, updateEntity);
        }

        /// <summary>
        /// 异步更新多条数据
        /// </summary>
        /// <param name="filter">筛选器</param>
        /// <param name="updateEntity">更新器</param>
        /// <returns></returns>
        public async Task UpdateManyAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateEntity)
        {
            await Collection.UpdateManyAsync(filter, updateEntity);
        }

        /// <summary>
        /// 异步查找
        /// </summary>
        /// <param name="filter">筛选器</param>
        /// <returns></returns>
        public async Task FindAsync(FilterDefinition<TEntity> filter)
        {
            await Collection.FindAsync(filter);
        }

        /// <summary>
        /// 获取所有异常日志
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAllError(string logName, out long count, int p = 1)
        {
            var builder = Builders<TEntity>.Filter;
            var filter = builder.Eq("IsErrorLog", true) & builder.Eq("LogName", logName);
            return FindAll(out count, filter, p);
        }

        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <param name="count">条数</param>
        /// <param name="filter"></param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll(out long count, FilterDefinition<TEntity> filter = null, int page = 1, int PageSize = 15)
        {
            if (filter == null)
            {
                filter = new BsonDocument();
            }
            if (page < 1)
            {
                page = 1;
            }
            //var sort = Builders<TEntity>.Sort.Descending("time");
            var sort = new BsonDocument{
                {"$natural",-1}
            };
            var a = Collection.Find(filter);
            count = a.CountAsync().Result;
            //int PageSize = Convert.ToInt16(Help.GetConfigValueForNull("PageSize"));
            return a.Skip((page - 1) * PageSize).Limit(PageSize).Sort(sort).ToListAsync<TEntity>().Result;
            //List<TEntity> a=new List<TEntity>();
            //Collection.Find(new MongoDB.Bson.BsonDocument()).ForEachAsync(x => a.Add(x));
            //return a;

            //using (var qry = await Collection.FindAsync<TEntity>(new MongoDB.Bson.BsonDocument()))
            //{
            //    while (await qry.MoveNextAsync())
            //    {
            //        return qry.Current;
            //    }
            //    return null;
            //}
        }

        public IEnumerable<TEntity> FindAll(FilterDefinition<TEntity> filter = null, BsonDocument sort = null, int page = 1, int PageSize = 15)
        {
            if (filter == null)
            {
                filter = new BsonDocument();
            }
            if (page < 1)
            {
                page = 1;
            }
            if (sort == null)
            {
                sort = new BsonDocument{
                    {"$natural",-1}
                };
            }
            var a = Collection.Find(filter);
            return a.Skip((page - 1) * PageSize).Limit(PageSize).Sort(sort).ToListAsync<TEntity>().Result;
        }
        public IEnumerable<TEntity> command(string str, int p = 1, int pageSize = 50)
        {
            var sort = new BsonDocument{
                {"_id",-1}
            };
            if (string.IsNullOrWhiteSpace(str))
            {
                return Collection.Find(new BsonDocument()).Skip((p - 1) * pageSize).Limit(pageSize).Sort(sort).ToListAsync<TEntity>().Result;
            }
            var com = new BsonDocument{
                {"$text",new BsonDocument{{"$search",str}}}
            };
            return Collection.Find(com).Skip((p - 1) * pageSize).Limit(pageSize).Sort(sort).ToListAsync<TEntity>().Result;
        }

        public IEnumerable<TEntity> logSearch(string str, string[] logname, string startTime, string endTime, bool isError, int page = 1, int PageSize = 50)
        {
            var builder = Builders<TEntity>.Filter;
            var filter = builder.Text(str);
            if (logname != null && logname.Length > 1)
            {
                FilterDefinition<TEntity> namefilter = null;
                namefilter = builder.Eq("logname", logname[0]);
                for (var i = 1; i < logname.Length; i++)
                {
                    namefilter = namefilter | builder.Eq("logname", logname[i]);
                }
                filter = filter & namefilter;
            }
            if (logname != null && logname.Length == 1)
            {
                filter = filter & builder.Eq("logname", logname[0]);
            }
            if (isError)
            {
                filter = filter & builder.Eq("IsErrorLog", true);
            }
            if (startTime != null)
            {
                filter = filter & builder.Gte("Time", DateTime.Parse(startTime));
            }
            if (endTime != null)
            {
                filter = filter & builder.Lte("Time", DateTime.Parse(endTime));
            }
            var sort = new BsonDocument{
                    {"_id",-1}
                };
            return FindAll(filter,sort, page, PageSize);
        }

        public int cout(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return Collection.Find(new BsonDocument()).ToListAsync<TEntity>().Result.Count;
            }
            var com = new BsonDocument{
                {"$text",new BsonDocument{{"$search",str}}},
                {"logname","wxback"}
            };
            return Collection.Find(com).ToListAsync<TEntity>().Result.Count;
        }

        /// <summary>
        /// 获取所有的collection
        /// </summary>
        /// <returns></returns>
        public List<string> FindAllCollections()
        {
            List<string> a = new List<string>();
            database.ListCollectionsAsync().Result.ToListAsync().Result.ForEach(x => a.Add(x[0].ToString()));
            a.Remove(a.Find(x => x == "system.indexes"));
            return a;
        }

        /// <summary>
        /// map-reduce获取一周最多的类名
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsonDocument> MapReduce()
        {
            string map = @"function(){
                        emit(this.class, 1);
                        }";
            string reduce = @"function(key,values){
                        var sum=0;
                        values.forEach(function(doc){
                            sum += 1;
                            });
                        return sum;
                        }";
            var builder = Builders<TEntity>.Filter;
            var options = new MapReduceOptions<TEntity, BsonDocument>
            {
                Filter = builder.Eq("iserror", true) & builder.Gte("time", DateTime.Now.AddMonths(-1)) & builder.Lte("time", DateTime.Now),
                MaxTime = TimeSpan.FromSeconds(2)
            };
            return Collection.MapReduceAsync<BsonDocument>(new BsonJavaScript(map), new BsonJavaScript(reduce), options).Result.ToListAsync().Result;
        }

        public void Export(string fileName, string str)
        {
            #region or关系
            //var com = new BsonDocument{
            //    {"$text",new BsonDocument{{"$search",str}}}
            //}; 
            #endregion
            #region and关系
            var com = new BsonDocument{
                {"$text",new BsonDocument{{"$search",str}}}
            };
            #endregion
            //using (var streamWriter = new StreamWriter(fileName))
            //{
            //    await Collection.Find(com)
            //        .ForEachAsync(async (document) =>
            //        {
            //            using (var stringWriter = new StringWriter())
            //            using (var jsonWriter = new JsonWriter(stringWriter))
            //            {
            //                var context = BsonSerializationContext.CreateRoot(jsonWriter);
            //                Collection.DocumentSerializer.Serialize(context, document);
            //                var line = stringWriter.ToString();
            //                await streamWriter.WriteLineAsync(line);
            //            }
            //        });
            //}
            using (var streamWriter = new StreamWriter(fileName))
            {

                var cursor = Collection.Find(com).ToCursor();
                foreach (var document in cursor.ToEnumerable())
                {
                    using (var stringWriter = new StringWriter())
                    using (var jsonWriter = new JsonWriter(stringWriter))
                    {
                        var context = BsonSerializationContext.CreateRoot(jsonWriter);
                        Collection.DocumentSerializer.Serialize(context, document);
                        var line = stringWriter.ToString();
                        streamWriter.WriteLine(line);
                    }
                }
            }
        }

        public IEnumerable<TEntity> BeforeLogs(string id, string logName,int size=5)
        {
            return NeighborLogs(id,logName,size,-1);
        }

        public IEnumerable<TEntity> AfterLogs(string id, string logName, int size = 5)
        {
            return NeighborLogs(id, logName, size, 1);
        }

        public IEnumerable<TEntity> NeighborLogs(string id, string logName, int size, int sort)
        {
            var builder = Builders<TEntity>.Filter;
            var filter = builder.Eq("logname", logName);
            filter = filter & builder.Lt("_id", new ObjectId(id));
            var sortFilter = new BsonDocument{
                    {"_id",sort}
                };
            return Collection.Find(filter).Sort(sortFilter).Limit(size).ToListAsync<TEntity>().Result;
        }
    }
}
