using Newtonsoft.Json;
using RestSharp;

namespace Allure.Core.Api.Request;

public class RequestBuilder : IRequestBuilder
{
    protected readonly string _resource;
    protected readonly Dictionary<string, string> _headers;
    protected readonly Dictionary<string, string> _cookies;
    protected readonly Dictionary<string, string> _parameters;
    protected readonly Dictionary<string, string> _queryParameters;
    protected DataFormat _dataFormat;
    protected Method _method;
    protected object _body;
    protected object _jsonBody;
    protected int _timeOut;
    protected string _token;
    protected string _fileName;
    protected string _filePath;
    protected string _fileType;
    protected bool _alwaysMultipartFormData;

    public int HeaderCount => _headers.Count;

    public string Token { get; set; }

    public RequestBuilder()
    {
        _headers = new Dictionary<string, string>();
        _parameters = new Dictionary<string, string>();
        _queryParameters = new Dictionary<string, string>();
        _method = Method.GET;
        _dataFormat = DataFormat.Json;
        _cookies = new Dictionary<string, string>();
        _timeOut = 0;
    }

    public RequestBuilder(string resource, string authenticationToken = null)
    {
        if (string.IsNullOrEmpty(resource))
        {
            throw new ArgumentNullException(nameof(resource));
        }

        _resource = resource;

        if (authenticationToken != null)
        {
            _token = authenticationToken;
        }
 
        _headers = new Dictionary<string, string>();
        _parameters = new Dictionary<string, string>();
        _queryParameters = new Dictionary<string, string>();
        _method = Method.GET;
        _dataFormat = DataFormat.Json;
        _cookies = new Dictionary<string, string>();
        _timeOut = 0;
    }

    public RequestBuilder(FormattableString resource)
    {
        if (resource is null)
        {
            throw new ArgumentNullException(nameof(resource));
        }

        _resource = resource.ToString();
        _headers = new Dictionary<string, string>();
        _parameters = new Dictionary<string, string>();
        _queryParameters = new Dictionary<string, string>();
        _method = Method.GET;
        _dataFormat = DataFormat.Json;
        _cookies = new Dictionary<string, string>();
        _timeOut = 0;
    }

    public RequestBuilder(Uri resource)
    {
        if (resource is null)
        {
            throw new ArgumentNullException(nameof(resource));
        }

        _resource = resource.ToString();
        _headers = new Dictionary<string, string>();
        _parameters = new Dictionary<string, string>();
        _queryParameters = new Dictionary<string, string>();
        _method = Method.GET;
        _dataFormat = DataFormat.Json;
        _cookies = new Dictionary<string, string>();
        _timeOut = 0;
    }

    public RequestBuilder(string resource, Method method)
    {
        if (string.IsNullOrEmpty(resource))
        {
            throw new ArgumentNullException(nameof(resource));
        }

        _resource = resource;
        _headers = new Dictionary<string, string>();
        _parameters = new Dictionary<string, string>();
        _queryParameters = new Dictionary<string, string>();
        _method = method;
        _dataFormat = DataFormat.Json;
        _cookies = new Dictionary<string, string>();
        _timeOut = 0;
    }

    public RequestBuilder(string resource, Method method, DataFormat format)
    {
        if (string.IsNullOrEmpty(resource))
        {
            throw new ArgumentNullException(nameof(resource));
        }

        _resource = resource;
        _headers = new Dictionary<string, string>();
        _parameters = new Dictionary<string, string>();
        _queryParameters = new Dictionary<string, string>();
        _method = method;
        _dataFormat = format;
        _cookies = new Dictionary<string, string>();
        _timeOut = 0;
    }

    public IRequestBuilder AddBody(object body)
    {
        _body = body ?? throw new ArgumentNullException(nameof(body));
        return this;
    }

    public IRequestBuilder AddJsonBody(object body)
    {
        _jsonBody = body ?? throw new ArgumentNullException(nameof(body));
        return this;
    }

    public IRequestBuilder AddFile(string name, string path, string contentType = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }

        _fileName = name;
        _filePath = path;
        _fileType = contentType;

        return this;
    }

    public IRequestBuilder SetFormat(DataFormat dataFormat)
    {
        _dataFormat = dataFormat;
        return this;
    }

    public IRequestBuilder AddHeader(string name, string value)
    {
        string headerValue = string.Empty;

        if (_headers.TryGetValue(name, out headerValue))
        {
            if (value != headerValue)
            {
                _headers[name] = value;
            }

            return this;
        }

        _headers.Add(name, value);
        return this;
    }

    public IRequestBuilder AddCookie(string name, string value)
    {
        string cookieValue = string.Empty;

        if (_cookies.TryGetValue(name, out cookieValue))
        {
            if (value != cookieValue)
            {
                _cookies[name] = value;
            }

            return this;
        }

        _cookies.Add(name, value);
        return this;
    }

    public IRequestBuilder AddHeaders(Dictionary<string, string> headers)
    {
        foreach (var header in headers)
        {
            string value = string.Empty;

            if (_headers.TryGetValue(header.Key, out value))
            {
                if (value != header.Value)
                {
                    _headers[header.Key] = header.Value;
                }

                continue;
            }

            _headers.Add(header.Key, header.Value);
        }

        return this;
    }

    public IRequestBuilder SetMethod(Method method)
    {
        _method = method;
        return this;
    }

    public IRequestBuilder SetTimeout(int timeout)
    {
        if (timeout < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(timeout));
        }

        _timeOut = timeout;
        return this;
    }

    public IRequestBuilder AddParameter(string parameter, string value)
    {
        if (parameter is null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        _parameters.Add(parameter, value);

        return this;
    }

    public IRequestBuilder AddParameters(Dictionary<string, string> parameters)
    {
        // TODO: Revisit, this doesn't seem like the best approach.

        var duplicates = _parameters.Select(x => x.Key).Intersect(parameters.Select(x => x.Key)).ToArray();

        // Check for duplicates.
        if (!duplicates.Any())
        {
            foreach (var parameter in parameters)
            {
                _parameters.Add(parameter.Key, parameter.Value);
            }

            return this;
        }

        // Iterate over duplicate items.
        foreach (var dup in duplicates)
        {
            KeyValuePair<string, string> param = parameters.FirstOrDefault(x => x.Key == dup);
            if (param.Equals(default(KeyValuePair<string, string>)))
            {
                continue;
            }

            _parameters.Remove(dup);
            _parameters.Add(param.Key, param.Value);
        }

        return this;
    }

    public IRequestBuilder RemoveHeader(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (_headers.ContainsKey(name))
        {
            _headers.Remove(name);
        }

        return this;
    }

    public IRequestBuilder RemoveHeaders()
    {
        _headers.Clear();
        return this;
    }

    public IRequestBuilder RemoveCookies()
    {
        _cookies.Clear();
        return this;
    }

    public IRequestBuilder RemoveParameters()
    {
        _parameters.Clear();
        return this;
    }

    public IRequestBuilder RemoveParameter(string parameter, string value)
    {
        if (parameter is null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        var param = _parameters.FirstOrDefault(p => p.Key == parameter);

        if (param.Equals(default(KeyValuePair<string, string>)))
        {
            return this;
        }

        _parameters.Remove(param.Key);

        return this;
    }

    public IRequestBuilder AlwaysMultipartFormData(bool enabled)
    {
        _alwaysMultipartFormData = enabled;
        return this;
    }

    public IRestRequest Build()
    {
        RestRequest request = new RestRequest(_resource, _method, _dataFormat);

        foreach (var param in _parameters)
        {
            request.AddParameter(param.Key, param.Value);
        }

        if (_body != null)
        {
            request.AddJsonBody(_body);
        }

        if (_jsonBody != null)
        {
            string jsonString = JsonConvert.SerializeObject(_jsonBody, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
        }

        foreach (KeyValuePair<string, string> header in _headers)
        {
            request.AddHeader(header.Key, header.Value);
        }

        foreach (KeyValuePair<string, string> cookie in _cookies)
        {
            request.AddCookie(cookie.Key, cookie.Value);
        }

        if (!string.IsNullOrEmpty(_fileName) && !string.IsNullOrEmpty(_filePath))
        {
            request.AddFile(_fileName, _filePath, _fileType);
        }

        request.Timeout = _timeOut;
        request.AlwaysMultipartFormData = _alwaysMultipartFormData;

        if (!string.IsNullOrWhiteSpace(_token))
        {
            request.AddHeader("Authorization", $"Bearer {_token}");
        }

        return request;
    }
}