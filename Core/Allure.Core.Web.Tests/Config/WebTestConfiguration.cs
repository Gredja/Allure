using Allure.Core.Configuration.Config;
using Allure.Driver.Enums;
using Newtonsoft.Json;

namespace Allure.Core.Web.Tests.Config;

public class WebTestConfiguration : TestConfiguration
{
    [JsonProperty("Browser")]
    public BrowserType Browser { get; set; }
}