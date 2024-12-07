using Store.Common;

namespace Store.Application.Services.Wallet.Transaction.Commands.CreateTransactionService
{
    public interface ICreateTransactionService
    {
        Task<ResultDTO> ExecuteAsync(CreateTransactionDTO transactionDTO);
    }
}
