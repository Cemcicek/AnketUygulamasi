using Microsoft.AspNetCore.Mvc;

namespace AnketUygulamasiUI.ViewComponents.LayoutComponents
{
    public class _LayoutHeaderComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
