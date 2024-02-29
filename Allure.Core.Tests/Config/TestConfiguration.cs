using Allure.Core.Tests.Config.Models;
using Newtonsoft.Json;

namespace Allure.Core.Tests.ConfigModels;

public class TestConfiguration
{
    [JsonProperty("BaseConfig")]
    public BaseConfig BaseConfig { get; set; }
}