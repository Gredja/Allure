using RestSharp;

namespace Allure.Core.Api.Request;

public interface IRequestBuilder
{
    int HeaderCount { get; }

    IRequestBuilder AddCookie(string name, string value);

    IRequestBuilder AddHeader(string name, string value);

    IRequestBuilder AddHeaders(Dictionary<string, string> headers);

    IRequestBuilder SetTimeout(int timeout);

    IRequestBuilder SetMethod(Method method);

    IRequestBuilder AddParameter(string parameter, string value);

    IRequestBuilder AddParameters(Dictionary<string, string> parameters);

    IRequestBuilder RemoveHeaders();

    IRequestBuilder RemoveHeader(string name);

    IRequestBuilder RemoveCookies();

    IRequestBuilder RemoveParameters();

    IRequestBuilder RemoveParameter(string parameter, string value);

    IRequestBuilder AlwaysMultipartFormData(bool enabled);

    IRequestBuilder SetFormat(DataFormat dataFormat);

    IRequestBuilder AddBody(object body);

    IRequestBuilder AddJsonBody(object body);

    IRequestBuilder AddFile(string name, string path, string contentType = null);

    IRestRequest Build();
}