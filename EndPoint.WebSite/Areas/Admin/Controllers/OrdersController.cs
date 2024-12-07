using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Finanace.Commands.GetOrderForAdminService;
using Store.Domain.Entities.Finance;

namespace EndPoint.WebSite.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin, Operator")]
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IGetOrderForAdminService _db;
        public OrdersController(IGetOrderForAdminService db)
        {
            _db = db;
        }
        public IActionResult Index(OrderState orderState, int page = 1)
        {
            return View(_db.Execute(orderState, 20, page).Data);
        }
    }
}
