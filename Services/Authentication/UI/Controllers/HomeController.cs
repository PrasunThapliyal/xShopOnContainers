using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("ui/[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet("login")]
        public async Task<ActionResult> Login()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:5001/api/home/Authenticate").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var r = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return Ok(r);
            }
            return View();
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            return RedirectToAction("Index");
        }

        [HttpGet("protectedEntity")]
        public async Task<IActionResult> ProtectedEntity()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:5001/api/home").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var r = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return Ok(r);
            }
            return View();
        }
    }
}