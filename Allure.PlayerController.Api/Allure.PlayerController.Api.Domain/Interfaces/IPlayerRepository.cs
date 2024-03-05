using RestSharp;
using System.Net;
using Allure.PlayerController.Api.Domain.ResponseModels;

namespace Allure.PlayerController.Api.Domain.Interfaces;

public interface IPlayerRepository
{
    public IRestResponse<PlayerGetAllResponseDto> GetAll(HttpStatusCode expectedCode = HttpStatusCode.OK);
}