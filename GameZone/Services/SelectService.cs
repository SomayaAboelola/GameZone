
using GameZone.Entities;

namespace GameZone.Services
{
    public class SelectService : ISelectService
    {
        private readonly AppDbContext _context;

        public SelectService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectListCate()
        {
            var Categories = _context.Categories.
                             Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).
                             OrderBy(c => c.Text).
                             AsNoTracking().
                             ToList();
            return Categories;
        }


        public IEnumerable<SelectListItem> GetSelectListDev()
        {
            var Devices = _context.Devices.
                        Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).
                        OrderBy(c => c.Text).
                        AsNoTracking().
                        ToList();
            return Devices; 
        }
    }
}
