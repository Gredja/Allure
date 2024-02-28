using Allure.Core.Api.Tests.ConfigModels;
using Allure.Core.Tests;
using NUnit.Framework;

namespace Allure.Core.Api.Tests.Base;

[TestFixture]
public class BaseApiTest
{
    protected readonly BaseApiTestConfiguration Configuration;

    public BaseApiTest()
    {
        Configuration = ConfigurationProvider<BaseApiTestConfiguration>.Configuration;
    }


}