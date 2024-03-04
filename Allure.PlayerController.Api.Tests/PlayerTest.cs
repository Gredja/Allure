using Allure.PlayerController.Api.Tests.Base;
using Allure.PlayerController.Api.Tests.Services;
using FluentAssertions;
using NUnit.Framework;

namespace Allure.PlayerController.Api.Tests;

[TestFixture]
public class PlayerTest : BasePlayerTest
{
    [Test]
    [Description("Test cases: C934240 | Get all players.")]
    public void PlayerController_GetAllPlayer_SuccessGettingAllPlayers()
    {


        var ggg = new PlayerService($"{Configuration.BaseHost}/{Configuration.Controllers.PlayerController}").GetAll().Content;

     
    }
}