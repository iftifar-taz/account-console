using System;

namespace Account.Models
{
    public class AccountHistory
    {
        public Amount Amount { get; set; }
        public Amount Balance { get; set; }
        public DateTime Date { get; set; }
    }
}
