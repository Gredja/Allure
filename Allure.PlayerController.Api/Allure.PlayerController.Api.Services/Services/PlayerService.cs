
using Allure.PlayerController.Api.Domain.Interfaces;
using Allure.PlayerController.Api.Domain.ResponseModels;
using Allure.PlayerController.Api.Repositories.Interfaces;
using Allure.PlayerController.Api.Repositories.Repositories;

namespace Allure.PlayerController.Api.Services.Services;

public class PlayerService(string baseUrl) : IPlayerService
{
    private readonly IPlayerRepository _playerRepository = new PlayerRepository(baseUrl);

    public List<Player> GetAll()
    {
       return _playerRepository.GetAll().Data.Players; 
    }
}