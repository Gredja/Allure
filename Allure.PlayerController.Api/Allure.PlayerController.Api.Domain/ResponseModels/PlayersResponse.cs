using Newtonsoft.Json;

namespace Allure.PlayerController.Api.Domain.ResponseModels
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.3.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public class PlayerGetAllResponseDto
    {
        [JsonProperty("players", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<PlayerItem> Players { get; set; }

    }
}
