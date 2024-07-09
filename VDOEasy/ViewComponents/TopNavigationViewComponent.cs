using Microsoft.AspNetCore.Mvc;

namespace VDOEasy.ViewComponents
{
    public class TopNavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
