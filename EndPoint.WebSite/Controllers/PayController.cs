using Dto.Payment;
using EndPoint.Site.Utilities;
using EndPoint.WebSite.Units;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Cart;
using Store.Application.Services.Finanace.Commands;
using Store.Application.Services.Finanace.Commands.PayUsingWalletService;
using Store.Application.Services.Finanace.Queries;
using Store.Application.Services.Wallet.Shared.Queries.GetWalletsBalanceByUserService;
using Store.Common;
using ZarinPal.Class;

namespace EndPoint.WebSite.Controllers
{
    [Authorize("Customer")]
    public class PayController : Controller
    {
        private readonly ICartService _cart;
        private readonly IAddPayRequestService _addPay;
        private readonly IGetPayRequestService _getPay;
        private readonly IAddOrderService _addOrder;
        private readonly IPayUsingWalletService _payUsingWallet;

        private readonly CookieManeger _cookieManeger;
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;

        public PayController(ICartService cart, IAddPayRequestService addPay, IGetPayRequestService getPay, IAddOrderService addOrder, IPayUsingWalletService payUsingWallet, IGetWalletsBalanceByUserService getWalletsBalanceByUserService)
        {
            _cart = cart;
            _addPay = addPay;
            _getPay = getPay;
            _addOrder = addOrder;
            _payUsingWallet = payUsingWallet;
            _cookieManeger = new CookieManeger();

            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
        }

        public async Task<IActionResult> Index()
        {
            int? UserId = ClaimUtility.GetUserId(User);
            var cart = _cart.GetCart(_cookieManeger.GetBrowserId(HttpContext), UserId);
            if (cart.Data.TotalAmount > 0)
            {
                var requestPay = _addPay.Execute(UserId.Value, cart.Data.TotalAmount);
                // ارسال در گاه پرداخت
                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09121112222",
                    CallbackUrl = $"http://localhost:5000/Pay/Verify?guid={requestPay.Data.Guid}",
                    Description = "پرداخت فاکتور شماره:" + requestPay.Data.Id,
                    Email = requestPay.Data.Email,
                    Amount = requestPay.Data.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, Payment.Mode.sandbox);
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }
        public async Task<IActionResult> Verify(Guid guid, string authority, string status)
        {

            var requestPay = _getPay.Execute(guid);

            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = requestPay.Data.Amount,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority
            }, Payment.Mode.sandbox);

            if (verification.Status == 100)
            {
                int? userId = ClaimUtility.GetUserId(User);
                var cart = _cart.GetCart(_cookieManeger.GetBrowserId(HttpContext), userId.Value).Data;
                _addOrder.Execute(requestPay.Data.Id, userId.Value, cart.Id, " ");

                return RedirectToAction("Index", "Orders");
            }

            return Redirect("/Orders");
        }

        [HttpPost]
        public async Task<IActionResult> PayByCompanyWallet()
        {
            int? UserId = ClaimUtility.GetUserId(User);

            var cart = _cart.GetCart(_cookieManeger.GetBrowserId(HttpContext), UserId);
            if (cart.Data.TotalAmount > 0)
            {
                var res = await _payUsingWallet.ExecuteAsync(UserId.Value, true);
                return Json(res);
            }
            return Json(new ResultDTO() { isSuccess = false, Message = "سبد خرید شما خالی است" });
        }

        [HttpPost]
        public async Task<IActionResult> PayByPersonalWallet()
        {
            int? UserId = ClaimUtility.GetUserId(User);

            var cart = _cart.GetCart(_cookieManeger.GetBrowserId(HttpContext), UserId);
            if (cart.Data.TotalAmount > 0)
            {
                var res = await _payUsingWallet.ExecuteAsync(UserId.Value, false);
                return Json(res);
            }
            return Json(new ResultDTO() { isSuccess = false, Message = "سبد خرید شما خالی است" });
        }
    }
}
