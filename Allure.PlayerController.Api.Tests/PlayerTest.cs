using Allure.PlayerController.Api.Tests.Base;
using Allure.PlayerController.Api.Tests.Services;
using FluentAssertions;
using NUnit.Framework;

namespace Allure.PlayerController.Api.Tests;

[TestFixture]
public class PlayerTest : BasePlayerTest
{
    [Test]
    public void Test()
    {
        var tt = false;

        var ttt = Configuration.BaseHost;

        var ggg = new PlayerService($"{Configuration.BaseHost}#/{Configuration.Controllers.PlayerController}");

        tt.Should().BeFalse();
    }
}