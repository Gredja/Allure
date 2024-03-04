using Allure.Core.Api.Request;
using Allure.Core.Api.Services;
using Allure.PlayerController.Api.Tests.ResponseModels;
using RestSharp;
using System.Net;
using RestSharp.Serialization.Json;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Allure.PlayerController.Api.Tests.Services;

public class PlayerService(string baseServiceUrl) : BaseService(baseServiceUrl)
{
    private const string GetAllUri = "get/all";

    Dictionary<string, string> Headers = new();

    public IRestResponse<List<Player>> GetAll(HttpStatusCode expectedCode = HttpStatusCode.OK)
    {
        var request = new RequestBuilder(GetAllUri)
            .SetMethod(Method.GET)
            .AddHeaders(Headers)
            .AddHeader("Content-Type", "application/json")
            .Build();

        var aa = Client.Execute<List<Player>>(request, expectedCode);

        var tt = JsonConvert.DeserializeObject<PlayersResponse>(aa.Content);

        //JsonDeseriale


        return aa;
    }
}