using Allure.PlayerController.Api.Repositories.Interfaces;
using Allure.PlayerController.Api.Services.Services;
using Allure.PlayerController.Api.Tests.Base;
using FluentAssertions;
using NUnit.Framework;

namespace Allure.PlayerController.Api.Tests;

[TestFixture]
public class PlayerTest : BasePlayerTest
{
    private IPlayerService _playerService;

    [SetUp]
    public void Setup()
    {
        _playerService = new PlayerService($"{Configuration.BaseHost}/{Configuration.Controllers.PlayerController}");
    }

    [Test]
    [Description("Test cases: C934240 | Get all players.")]
    public void PlayerController_GetAllPlayer_SuccessGettingAllPlayers()
    {
        var players= _playerService.GetAll();

        players.Should().NotBeEmpty();
    }

    [Test]
    [Description("Test cases: C934242 | Get all players.")]
    public void PlayerController_GetAllPlayer_АailureGettingAllPlayers()
    {
        var players = _playerService.GetAll();

        players.Should().NotBeEmpty();

        foreach (var playerItem in players)
        {
            playerItem.GetType().GetProperties().Should().AllSatisfy(x => x.GetValue(playerItem).Should().NotBeNull());
        }
    }
}