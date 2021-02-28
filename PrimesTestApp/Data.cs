using System.Text.Json.Serialization;

namespace PrimesTestApp
{
    class Data
    {
        [JsonPropertyName("baseAddress")]
        public string BaseAddress { get; set; }
    }
}
