using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.IO;
using System.Threading.Tasks;

// ...
public class ExportingJSON
{
    string outputFileName; // initialize to the output file
    IMongoCollection<BsonDocument> collection; // initialize to the collection to read from
    public async Task Export()
    {
        using (var streamWriter = new StreamWriter(outputFileName))
        {
            await collection.Find(new BsonDocument())
                .ForEachAsync(async (document) =>
                {
                    using (var stringWriter = new StringWriter())
                    using (var jsonWriter = new JsonWriter(stringWriter))
                    {
                        var context = BsonSerializationContext.CreateRoot(jsonWriter);
                        collection.DocumentSerializer.Serialize(context, document);
                        var line = stringWriter.ToString();
                        await streamWriter.WriteLineAsync(line);
                    }
                });
        }
    }


}

