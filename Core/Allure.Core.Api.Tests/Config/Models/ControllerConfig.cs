using Newtonsoft.Json;

namespace Allure.Core.Api.Tests.Config.Models;

public class ControllerConfig
{
    [JsonProperty("PlayerController")]
    public string PlayerController { get; set; }
}