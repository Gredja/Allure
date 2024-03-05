using Allure.PlayerController.Api.Domain.ResponseModels;

namespace Allure.PlayerController.Api.Repositories.Interfaces;

public interface IPlayerService
{
    public List<PlayerItem> GetAll();
}