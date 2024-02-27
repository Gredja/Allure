using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using RestSharp;
using RestSharp.Serialization.Json;

namespace Allure.Common.Api.Extensions;

public static class BodyExtensions
{
    public static T Get<T>(this IRestResponse response) where T : new()
    {
        return new JsonDeserializer().Deserialize<T>(response);
    }

    public static string ConvertToString(this IRestResponse response)
    {
        if (response.Content.Length == 0)
        {
            return string.Empty;
        }

        var responseToLog = new
        {
            content = JToken.Parse(response.Content).ToString(Formatting.Indented),
            status = response.StatusCode.ToString(),
        };

        return new StringBuilder()
            .Append(responseToLog.status).Append("\n")
            .Append(responseToLog.content).Append("\n")
            .ToString();
    }

    public static T GetValueByName<T>(this IRestResponse response, string name)
    {
        var jsonObj = JObject.Parse(response.Content);
        return JsonConvert.DeserializeObject<T>(jsonObj[name]?.ToString());
    }

    public static bool HasErrors(this IRestResponse response)
    {
        return response.Content.Contains("errors");
    }

    public static string GetFormattedMessage(this IRestResponse response, HttpStatusCode code)
    {
        return $"Expected {(int)code} {code} status code. Actual {(int)response.StatusCode} {response.StatusCode} \n {response.Content}";
    }

    public static string GetFormattedMessage(this IRestResponse response)
    {
        var builder = new StringBuilder();
        foreach (var header in response.Headers)
        {
            builder.Append(header.Value).Append("\n");
        }

        return $"Received response has {response.StatusCode} status code. Content is in place \n {response.Content} with headers \n {builder}";
    }

    public static string GetFormattedMessage(this IRestRequest request, string url)
    {
        var requestBody = request.Body != null ? request.Body.Value : "empty";
        var builder = new StringBuilder();
        foreach (var param in request.Parameters)
        {
            builder.Append(param.Value).Append("\n");
        }

        return $"Sending request to {url} with {requestBody} body and headers {builder}";
    }
}