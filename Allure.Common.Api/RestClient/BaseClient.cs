using System.Net;
using Allure.Common.Api.Extensions;
using Allure.Logger;
using NLog;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Allure.Common.Api.RestClient;

public class BaseClient : IBaseClient
{
    private ILogger Logger => LoggingManager.GetInstance();

    private IRestClient Client { get; }

    public BaseClient(string baseUrl)
    {
        Client = new RestSharp.RestClient(baseUrl).UseNewtonsoftJson();
    }

    public IRestResponse Execute(IRestRequest request, HttpStatusCode expectedCode = HttpStatusCode.OK)
    {
        Logger.Info($"Request: {request.GetFormattedMessage(Client.BuildUri(request).AbsolutePath)}");

        var response = Client.Execute(request);

        Logger.Info($"Response: {response.GetFormattedMessage()}");

        if (response.StatusCode != expectedCode)
        {
            var message = response.GetFormattedMessage(expectedCode);
            Logger.Fatal(message);
            throw new WebException(message);
        }

        return response;
    }

    public IRestResponse<T> Execute<T>(IRestRequest request, HttpStatusCode expectedCode = HttpStatusCode.OK) where T : class
    {
        Logger.Info($"Request: {request.GetFormattedMessage(Client.BuildUri(request).AbsolutePath)}");

        var response = Client.Execute<T>(request);

        Logger.Info($"Response: {response.GetFormattedMessage()}");

        if (response.StatusCode != expectedCode)
        {
            var message = response.GetFormattedMessage(expectedCode);
            Logger.Fatal(message);
            throw new WebException(message);
        }

        return response;
    }
}