using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WordFinder.Dtos
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GetFoundWordsDto
    {
        public required IEnumerable<string> Matrix { get; set; }
        public required IEnumerable<string> Wordstream { get; set; }
    }
}
