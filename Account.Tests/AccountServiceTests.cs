using Account.Models;
using Account.Services;
using NUnit.Framework;
using System;
using System.IO;

namespace Account.Tests
{
    public class AccountServiceTests
    {
        private IAccountService _service;

        [SetUp]
        public void Setup()
        {
            _service = new AccountService();
        }

        [Test]
        public void Negetive_Diposit_Throw_Exception()
        {
            var amount = new Amount()
            {
                Value = -100
            };
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.Deposit(amount));
        }

        [Test]
        public void First_Call_To_Withdraw_Throw_Exception()
        {
            var amount = new Amount()
            {
                Value = 100
            };
            Assert.Throws<NullReferenceException>(() => _service.Withdraw(amount));
        }

        [Test]
        public void Negetive_Withdraw_Throw_Exception()
        {
            var amount = new Amount()
            {
                Value = -100
            };
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.Withdraw(amount));
        }

        [Test]
        public void More_Than_Balance_Withdraw_Throw_Exception()
        {
            var amount = new Amount()
            {
                Value = 100
            };
            _service.Deposit(amount);
            _service.Withdraw(amount);
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.Withdraw(amount));
        }

        [Test]
        public void First_Call_To_PrintStatement_Throw_Exception()
        {
            Assert.Throws<NullReferenceException>(() => _service.PrintStatement());
        }

        [Test]
        public void It_Works()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var amount = new Amount()
            {
                Value = 100
            };
            _service.Deposit(amount);
            _service.Deposit(amount);
            _service.Withdraw(amount);
            _service.PrintStatement();

            var outputLines = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var today = DateTime.UtcNow;
            Assert.AreEqual($"{today:dd/MM/yyyy}||-100||100", outputLines[1]);
            Assert.AreEqual($"{today:dd/MM/yyyy}||100||200", outputLines[2]);
            Assert.AreEqual($"{today:dd/MM/yyyy}||100||100", outputLines[3]);
        }
    }
}