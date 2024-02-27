using Newtonsoft.Json;

namespace Allure.Tests.ConfigModels;

public class BaseUiTestConfiguration
{
    [JsonProperty("BaseConfig")]
    public BaseConfig BaseConfig { get; set; }
}