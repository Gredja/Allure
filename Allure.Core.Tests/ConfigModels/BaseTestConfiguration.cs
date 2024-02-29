using Newtonsoft.Json;

namespace Allure.Core.Tests.ConfigModels;

public class BaseTestConfiguration
{
    [JsonProperty("BaseConfig")]
    public BaseConfig BaseConfig { get; set; }
}