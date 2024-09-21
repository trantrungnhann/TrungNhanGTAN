using GiaoHangMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using GiaoHangMongoDB.Models.ViewModels;

namespace GiaoHangMongoDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }

        public IActionResult Index()
        {
            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "ThucAn.json");
            var jsonData = System.IO.File.ReadAllText(jsonPath);

            // Sử dụng đúng namespace và kiểu dữ liệu
            List<GiaoHangMongoDB.Models.ViewModels.ThucAnDTO> thucAnList = JsonConvert.DeserializeObject<List<GiaoHangMongoDB.Models.ViewModels.ThucAnDTO>>(jsonData);

            // Truyền dữ liệu tới view
            return View(thucAnList);
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
    }
}
