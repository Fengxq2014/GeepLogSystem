using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
// ...stringoutputFileName;
//initialize to the output file
//IMongoCollection<BsonDocument> collection;
// initialize to the collection to read fromusing(varstreamWriter =newStreamWriter(outputFileName)){awaitcollection.Find(newBsonDocument())        .ForEachAsync(async(document) =>        {using(varstringWriter =newStringWriter())using(varjsonWriter =newJsonWriter(stringWriter))            {varcontext = BsonSerializationContext.CreateRoot(jsonWriter);
//collection.DocumentSerializer.Serialize(context, document);varline = stringWriter.ToString();awaitstreamWriter.WriteLineAsync(line);            }        });}
