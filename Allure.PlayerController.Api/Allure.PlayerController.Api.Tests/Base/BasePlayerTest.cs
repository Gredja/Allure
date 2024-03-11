using Allure.Core.Api.Tests.Base;
using Allure.Core.Api.Tests.Config;
using Allure.Core.Configuration;
using NUnit.Framework;

[assembly: LevelOfParallelism(5)]

namespace Allure.PlayerController.Api.Tests.Base;

[TestFixture]
[Parallelizable]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public abstract class BasePlayerTest : BaseApiTest
{
    protected readonly ApiTestConfiguration Configuration = ConfigurationProvider<ApiTestConfiguration>.Configuration;
}