using Allure.Core.Api.Request;
using Allure.Core.Api.Services;
using Allure.PlayerController.Api.Domain.Interfaces;
using Allure.PlayerController.Api.Domain.ResponseModels;
using RestSharp;
using System.Net;

namespace Allure.PlayerController.Api.Repositories.Repositories;

public class PlayerRepository(string baseServiceUrl) : BaseService(baseServiceUrl), IPlayerRepository
{
    public const string GetAllUri = "get/all";

    Dictionary<string, string> Headers = new();

    public IRestResponse<PlayerGetAllResponseDto> GetAll(HttpStatusCode expectedCode = HttpStatusCode.OK)
    {
        var request = new RequestBuilder(GetAllUri)
            .SetMethod(Method.GET)
            .AddHeaders(Headers)
            .AddHeader("Content-Type", "application/json")
            .Build();

        return Client.Execute<PlayerGetAllResponseDto>(request, expectedCode);
    }
}