namespace GameZone.Services
{
    public interface ISelectService
    {
        public IEnumerable<SelectListItem> GetSelectListCate();
        public IEnumerable<SelectListItem> GetSelectListDev();
    }
}
