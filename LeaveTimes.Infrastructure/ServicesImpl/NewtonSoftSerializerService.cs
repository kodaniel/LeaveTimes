using LeaveTimes.Application.Services.Serializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace LeaveTimes.Infrastructure.ServicesImpl;

internal class NewtonSoftSerializerService : ISerializerService
{
    private JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        NullValueHandling = NullValueHandling.Ignore,
        Converters = [new StringEnumConverter() { NamingStrategy = new CamelCaseNamingStrategy() }]
    };

    public string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj, _serializerSettings);

    public string Serialize<T>(T obj, Type type) => JsonConvert.SerializeObject(obj, type, _serializerSettings);

    public T? Deserialize<T>(string text) => JsonConvert.DeserializeObject<T>(text, _serializerSettings);
}
