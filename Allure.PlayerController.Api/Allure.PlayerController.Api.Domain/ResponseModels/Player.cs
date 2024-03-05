using Newtonsoft.Json;

namespace Allure.PlayerController.Api.Domain.ResponseModels;

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.3.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
public class PlayerItem
{
    [JsonProperty("age", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public int? Age { get; set; }

    [JsonProperty("gender", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string Gender { get; set; }

    [JsonProperty("id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public long? Id { get; set; }

    [JsonProperty("role", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string Role { get; set; }

    [JsonProperty("screenName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string ScreenName { get; set; }

}