using Microsoft.AspNetCore.Mvc;

namespace VDOEasy.ViewComponents
{
    public class LeftNavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
