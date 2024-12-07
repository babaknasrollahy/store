using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Finanace.Commands.GetOrderService;
using Store.Common;

namespace EndPoint.WebSite.Controllers
{
    [Authorize(nameof(RolesList.Customer))]
    public class OrdersController : Controller
    {
        private readonly IGetOrderService _db;
        public OrdersController(IGetOrderService db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Execute(ClaimUtility.GetUserId(User).Value).Data);
        }
    }
}
