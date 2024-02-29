using Allure.Core.Tests.ConfigModels;
using Allure.Driver.Enums;
using Newtonsoft.Json;

namespace Allure.Core.Web.Tests.ConfigModels;

public class WebTestConfiguration : TestConfiguration
{
    [JsonProperty("Browser")]
    public BrowserType Browser { get; set; }
}