using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class PrivacyController : Controller
    {
        public async Task<IActionResult> Privacy()
        {
            return View();
        }
    }
}
