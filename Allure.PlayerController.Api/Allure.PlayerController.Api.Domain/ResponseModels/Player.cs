using Allure.PlayerController.Api.Domain.Enums;
using Newtonsoft.Json;

namespace Allure.PlayerController.Api.Domain.ResponseModels;

public class Player
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("screenName")]
    public string Name { get; set; }

    [JsonProperty("gender")]
    public Gender Gender { get; set; }

    [JsonProperty("age")]
    public int Age { get; set; }
}