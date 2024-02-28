using Allure.Core.Api.RestClient;

namespace Allure.Core.Api.Services;

public class BaseService
{
    protected IBaseClient Client { get; }

    protected BaseService(string baseServiceUrl)
    {
        Client = new BaseClient(baseServiceUrl);
    }
}