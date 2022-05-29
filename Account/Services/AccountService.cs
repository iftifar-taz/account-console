using Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Account.Services
{
    public class AccountService : IAccountService
    {
        private readonly List<AccountHistory> _history = new List<AccountHistory>();

        public AccountService()
        {
        }

        public void Deposit(Amount amount)
        {
            if (amount.Value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }
            var lastTransaction = _history.LastOrDefault();
            var newHistory = GetNewHistory("Deposit", lastTransaction, amount);
            _history.Add(newHistory);
        }

        public void Withdraw(Amount amount)
        {
            if (amount.Value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }
            
            var lastTransaction = _history.LastOrDefault();

            if (lastTransaction == null)
            {
                throw new NullReferenceException(nameof(lastTransaction));
            }

            if (amount.Value > lastTransaction.Balance.Value)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            var newHistory = GetNewHistory("Withdraw", lastTransaction, amount);
            _history.Add(newHistory);
        }

        public void PrintStatement()
        {
            var history = _history;
            if (!history.Any())
            {
                throw new NullReferenceException(nameof(_history));
            }
            history.Reverse();
            Console.WriteLine("Date||Amount||Balance");
            foreach (var row in history)
            {
                Console.WriteLine(@$"{row.Date:dd/MM/yyyy}||{row.Amount.Value}||{row.Balance.Value}");
            }
        }

        private static AccountHistory GetNewHistory(string type, AccountHistory lastTransaction, Amount amount)
        {
            var newAmount = new Amount()
            {
                Value = type == "Deposit" ? amount.Value : (amount.Value * -1),
            };

            return new AccountHistory()
            {
                Amount = newAmount,
                Balance = new Amount()
                {
                    Value = lastTransaction != null ? lastTransaction.Balance.Value + newAmount.Value : newAmount.Value
                },
                Date = DateTime.UtcNow,
            };
        }
    }
}
