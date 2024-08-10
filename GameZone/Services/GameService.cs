using GameZone.Settings;

namespace GameZone.Services
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly string _imagePath;

        public GameService(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _imagePath = $"{_hostEnvironment.WebRootPath}{FileSetting.filePath}";

        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                 .Include(c => c.Category)
                 .Include(d => d.Devices)
                 .ThenInclude(d => d.Device)
                 .AsNoTracking()
                 .ToList();
        }

        public Game? GetById(int id)
        {
            return _context.Games
              .Include(c => c.Category)
              .Include(d => d.Devices)
              .ThenInclude(d => d.Device)
              .AsNoTracking()
              .SingleOrDefault(g => g.Id == id);
        }
        public async Task CreateAsync(CreateGameFromViewModel model)
        {

            var coverName = await SaveCover(model.Cover);

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()

            };
            _context.Add(game);
            _context.SaveChanges();


        }

        public async Task<Game?> Update(UpdateGameFromViewModel model)
        {

            var game = _context.Games
                .Include(d => d.Devices)
                .SingleOrDefault(d => d.Id == model.Id);
            if (game is null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (hasNewCover)
                game.Cover = await SaveCover(model.Cover!);

            var result = _context.SaveChanges();
            if (result > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagePath, oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
                return null;
            }

        }
        public bool Delete(int id)
        {
            var isDeleted = false;
            var game = _context.Games.Find(id);
            if (game is null)
                return isDeleted;

            _context.Remove(game);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);

            }
            return isDeleted;

        }

        public async Task<string> SaveCover(IFormFile file)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var path = Path.Combine(_imagePath, coverName);

            using var Stream = File.Create(path);

            await file.CopyToAsync(Stream);

            return coverName;
        }

    
    }
}
