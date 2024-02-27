using Allure.Driver.Enums;
using Newtonsoft.Json;

namespace Allure.Core.Tests.ConfigModels;

public class BaseConfig
{
    [JsonProperty("Browser")]
    public BrowserType Browser { get; set; }

    [JsonProperty("Environment")]
    public string Environment { get; set; }
}