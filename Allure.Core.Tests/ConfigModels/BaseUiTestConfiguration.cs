using Allure.Tests.ConfigModels;
using Newtonsoft.Json;

namespace Allure.Core.Tests.ConfigModels;

public class BaseUiTestConfiguration
{
    [JsonProperty("BaseConfig")]
    public BaseConfig BaseConfig { get; set; }
}