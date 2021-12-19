using Microsoft.AspNetCore.Mvc;

namespace PlaceOsmApi.Controllers
{
    /// <summary>
    /// home controller
    /// </summary>
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
