using Allure.Core.Api.Tests.ConfigModels;
using Allure.Core.Tests;
using NUnit.Framework;

namespace Allure.PlayerController.Api.Tests.Base;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public abstract class BasePlayerTest
{
    protected readonly BaseApiTestConfiguration Configuration;

    protected BasePlayerTest()
    {
        Configuration = ConfigurationProvider<BaseApiTestConfiguration>.Configuration;
    }
}