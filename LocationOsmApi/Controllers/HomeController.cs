using Microsoft.AspNetCore.Mvc;

namespace PlaceOsmApi.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
