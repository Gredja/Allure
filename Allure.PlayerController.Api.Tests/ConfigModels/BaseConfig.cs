using Newtonsoft.Json;

namespace Allure.PlayerController.Api.Tests.ConfigModels;

public class BaseConfig
{
    [JsonProperty("Environment")]
    public string Environment { get; set; }
}