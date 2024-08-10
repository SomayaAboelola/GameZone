

using GameZone.Services;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISelectService _categoryService;
        private readonly IGameService _gameServices;

        public GamesController(AppDbContext context,
                ISelectService categoryService,
                IGameService gameServices)
        {
            _context = context;
            _categoryService = categoryService;
            _gameServices = gameServices;
        }
        public IActionResult Index()
        {
            return View(_gameServices.GetAll());
        }

        #region Create
        public IActionResult Create()
        {
            CreateGameFromViewModel viewModel = new()
            {
                Categories = _categoryService.GetSelectListCate(),

                Devices = _categoryService.GetSelectListDev(),
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFromViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.Categories = _categoryService.GetSelectListCate();
                model.Devices = _categoryService.GetSelectListDev();
                return View(model);
            }

            await _gameServices.CreateAsync(model);

            return RedirectToAction(nameof(Index));

        }
        #endregion



        #region Details 
        [HttpGet]
        public IActionResult Details(int id)
        {
            var game = _gameServices.GetById(id);
            if (game is null)
                return NotFound();

            return View(game);
        }
        #endregion
        [HttpGet]
        public IActionResult Update(int id)
        {
            var game = _gameServices.GetById(id);
            if (game is null)
                return NotFound();

            UpdateGameFromViewModel viewModel = new UpdateGameFromViewModel()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices=game.Devices.Select(d=>d.DeviceId).ToList(),
                Categories=_categoryService.GetSelectListCate(),
                Devices=_categoryService.GetSelectListDev(),
                UrlCover=game.Cover,
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public async Task <IActionResult>Update(UpdateGameFromViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.Categories = _categoryService.GetSelectListCate();
                model.Devices = _categoryService.GetSelectListDev();
                return View(model);
            }

          var game = await _gameServices.Update(model);

            if (game is null)   
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public IActionResult Delete (int id)
        {
            var isDeleted= _gameServices.Delete(id);
            return isDeleted ? Ok() : BadRequest();

        }
    }
}
