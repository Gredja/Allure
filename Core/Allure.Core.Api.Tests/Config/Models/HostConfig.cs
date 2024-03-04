using Newtonsoft.Json;

namespace Allure.Core.Api.Tests.Config.Models;

public class HostConfig
{
    [JsonProperty("BaseHost")]
    public string BaseHost { get; set; }
}