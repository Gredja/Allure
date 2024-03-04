using Newtonsoft.Json;

namespace Allure.Core.Tests.Config.Models;

public class BaseConfig
{
    [JsonProperty("Environment")]
    public string Environment { get; set; }


    [JsonProperty("ThreadCount")]
    public int ThreadCount { get; set; }
}

