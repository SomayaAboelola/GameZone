using GameZone.Entities;

namespace GameZone.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAll(); 
        Game? GetById(int id);
        Task CreateAsync(CreateGameFromViewModel model);
        Task<Game?> Update(UpdateGameFromViewModel model);
        bool Delete (int id);   
    }
}
