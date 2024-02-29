using Allure.Core.Api.Tests.Config;
using Allure.Core.Tests;
using NUnit.Framework;

[assembly: LevelOfParallelism(5)]

namespace Allure.PlayerController.Api.Tests.Base;

[TestFixture]
[Parallelizable]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public abstract class BasePlayerTest
{
    protected readonly ApiTestConfiguration Configuration = ConfigurationProvider<ApiTestConfiguration>.Configuration;
}