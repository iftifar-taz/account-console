using Account.Models;

namespace Account.Services
{
    public interface IAccountService
    {
        void Deposit(Amount amount);
        void Withdraw(Amount amount);
        void PrintStatement();
    }
}
