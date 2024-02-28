using Newtonsoft.Json;

namespace Allure.Core.Tests.ConfigModels;

public class BaseConfig
{
    [JsonProperty("Environment")]
    public string Environment { get; set; }
}

