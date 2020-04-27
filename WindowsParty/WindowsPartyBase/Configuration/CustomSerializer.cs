using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization;

namespace WindowsPartyBase.Configuration
{
    public class CustomSerializer : IRestSerializer
    {
        private readonly JsonSerializerSettings _serializerSettings;
        public CustomSerializer()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new LowerCaseContractResolver()
                {
                    
                }
            };
        }
        public string Serialize(object obj) =>
            JsonConvert.SerializeObject(obj);

        public string Serialize(Parameter parameter) =>
            JsonConvert.SerializeObject(parameter.Value, _serializerSettings);

        public T Deserialize<T>(IRestResponse response) =>
            JsonConvert.DeserializeObject<T>(response.Content);

        public string[] SupportedContentTypes { get; } =
        {
            "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
        };

        public string ContentType { get; set; } = "application/json";

        public DataFormat DataFormat { get; } = DataFormat.Json;
    }
}