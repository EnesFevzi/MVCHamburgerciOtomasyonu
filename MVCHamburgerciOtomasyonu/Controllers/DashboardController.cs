using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using Newtonsoft.Json;

namespace MVCHamburgerciOtomasyonu.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult UserDashboard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> YearlyOrderCounts()
        {
            var count = await _dashboardService.GetYearlyOrderCounts();
            return Json(JsonConvert.SerializeObject(count));
        }
        [HttpGet]
        public async Task<IActionResult> YearlyUserOrderCounts()
        {
            var count = await _dashboardService.GetYearlyUserOrderCounts();
            return Json(JsonConvert.SerializeObject(count));
        }
        [HttpGet]
        public async Task<IActionResult> TotalMenuCount()
        {
            var count = await _dashboardService.GetTotalMenuCount();
            return Json(count);
        }
        [HttpGet]
        public async Task<IActionResult> TotalMenuSizeCounts()
        {
            var count = await _dashboardService.GetTotalMenuSizeCount();
            return Json(count);
        }

        [HttpGet]
        public async Task<IActionResult> TotalExtraCounts()
        {
            var count = await _dashboardService.GetTotalExtraCount();
            return Json(count);
        }

        [HttpGet]
        public async Task<IActionResult> TotalUserOrderCounts()
        {
            var count = await _dashboardService.GetUserOrderCount();
            return Json(count);
        }
        [HttpGet]
        public async Task<IActionResult> TotalUserOrderCompletedCounts()
        {
            var count = await _dashboardService.GetUserOrderCompletedCount();
            return Json(count);
        }
        [HttpGet]
        public async Task<IActionResult> TotalUserOrderCancelCounts()
        {
            var count = await _dashboardService.GetUserOrderCancelCount();
            return Json(count);
        }




    }
}
