using mandiri_project.Entities;
using mandiri_project.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using mandiri_project.Services;
using mandiri_project.RequestResponseModels.Responses;
using NuGet.Protocol.Plugins;

namespace mandiri_project.Controllers
{
   public class TesResponse
   {
        //public int acknowledge { get; set; }
        //public List<Employee> employeeList { get; set; }
   }

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext _db;
        private readonly UserIdentityService _userIdentityService;

        public HomeController(ILogger<HomeController> logger, AppDbContext db, UserIdentityService userIdentityService)
        {
            _logger = logger;
            _db = db;
            _userIdentityService = userIdentityService;
        }

        public IActionResult Index()
        {
            string token = HttpContext.Session.GetString("Token");
            string jwtToken = Request.Cookies["Token"];
            if (string.IsNullOrEmpty(token))
                return View();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
             

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet("tes")]
        public async Task<ActionResult<TesResponse>> Tes()
        {
            var UserIdentity = _userIdentityService;
            var UserIdentity2 = User.Identity;
            //var result = new List<Employee>();
            //result = await _db.Employees.ToListAsync();

            //var response = new TesResponse()
            //{
            //    acknowledge = 1,
            //    employeeList = result
            //};

            return Ok();
        }

      

    }
}