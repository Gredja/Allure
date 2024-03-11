using Allure.Core.Configuration.Config.Models;
using Newtonsoft.Json;

namespace Allure.Core.Configuration.Config;

public class TestConfiguration
{
    [JsonProperty("BaseConfig")]
    public BaseConfig BaseConfig { get; set; }
}