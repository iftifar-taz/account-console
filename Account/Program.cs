using Account.Models;
using Account.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using static System.Guid;

namespace Account
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) => services.AddScoped<IAccountService, AccountService>())
            .Build();
            using var serviceScope = host.Services.CreateScope();
            var provider = serviceScope.ServiceProvider;
            var accountService = provider.GetRequiredService<IAccountService>();

            Diposit(accountService, 1000);
            Diposit(accountService, 2000);
            Withdraw(accountService, 500);
            PrintStatement(accountService);

            host.Run();
        }

        static void Diposit(IAccountService service, int amount)
        {
            service.Deposit(new Amount()
            {
                Value = amount
            });
        }

        static void Withdraw(IAccountService service, int amount)
        {
            service.Withdraw(new Amount()
            {
                Value = amount
            });
        }

        static void PrintStatement(IAccountService service)
        {
            service.PrintStatement();
        }
    }
}
