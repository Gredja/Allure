using Allure.Core.Api.Services;
using Allure.PlayerController.Api.Tests.ResponseModels;
using RestSharp;

namespace Allure.PlayerController.Api.Tests.Services;

public class PlayerService : BaseService
{
    public PlayerService(string baseServiceUrl) : base(baseServiceUrl)
    {
    }

    public IRestResponse<PlayerResponse> GetAll()
    {
        return null;
    }
}