using Allure.PlayerController.Api.Tests.Base;
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

        tt.Should().BeFalse();
    }
}