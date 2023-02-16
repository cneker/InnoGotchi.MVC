using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.MVC.Controllers
{
    public class PetController : Controller
    {
        public IActionResult PetDetails(Guid petId)
        {


            return View();
        }
    }
}
