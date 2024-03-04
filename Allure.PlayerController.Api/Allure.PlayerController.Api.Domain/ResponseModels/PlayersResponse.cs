using Newtonsoft.Json;

namespace Allure.PlayerController.Api.Domain.ResponseModels
{
    public class PlayersResponse
    {
        [JsonProperty("players")]
        public List<Player> Players { get; set; }
    }
}
