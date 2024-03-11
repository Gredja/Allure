using Allure.Core.Api.Tests.Config.Models;
using Allure.Core.Configuration.Config;
using Newtonsoft.Json;

namespace Allure.Core.Api.Tests.Config;

public class ApiTestConfiguration : TestConfiguration
{
    [JsonProperty("BaseHost")]
    public string BaseHost { get; set; }

    [JsonProperty("Controllers")]
    public ControllerConfig Controllers { get; set; }
}