using Newtonsoft.Json;

namespace Allure.PlayerController.Api.Tests.ResponseModels
{
    public class PlayersResponse
    {
        [JsonProperty("players")]
        public List<Player> Players { get; set; }
    }
}
