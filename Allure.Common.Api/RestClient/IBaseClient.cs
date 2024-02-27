using System.Net;
using RestSharp;

namespace Allure.Core.Api.RestClient;

public interface IBaseClient
{
    IRestResponse Execute(IRestRequest request, HttpStatusCode expectedCode = HttpStatusCode.OK);

    IRestResponse<T> Execute<T>(IRestRequest request, HttpStatusCode expectedCode = HttpStatusCode.OK) where T : class;
}