using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Wallet.Shared.Queries.GetWalletByIdService;
using Store.Application.Services.Wallet.Transaction.Commands.CreateTransactionService;

namespace EndPoint.WebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WalletsController : Controller
    {
        private readonly IGetWalletByIdService _getWalletById;
        private readonly ICreateTransactionService _createTransactionService;
        public WalletsController(ICreateTransactionService createTransactionService, IGetWalletByIdService getWalletById)
        {
            _createTransactionService = createTransactionService;
            _getWalletById = getWalletById;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(Guid Id, int page = 1)
        {
            var wallet = await _getWalletById.ExecuteAsync(Id, page);
            return View(wallet.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DepositMoney(Guid walletId, decimal amount)
        {
            var res = await _createTransactionService.ExecuteAsync(new CreateTransactionDTO()
            {
                Amount = amount,
                WalletId = walletId,
                TransactionType = Store.Application.Services.Wallet.Shared.Queries.GetWalletByIdService.TransactionType.InCome,
                UserId = ClaimUtility.GetUserId(User) ?? 0
            });

            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> WithdrawMoney(Guid walletId, decimal amount)
        {
            var res = await _createTransactionService.ExecuteAsync(new CreateTransactionDTO()
            {
                Amount = amount,
                WalletId = walletId,
                TransactionType = Store.Application.Services.Wallet.Shared.Queries.GetWalletByIdService.TransactionType.OutCome,
                UserId = ClaimUtility.GetUserId(User) ?? 0
            });

            return Json(res);
        }
    }
}
