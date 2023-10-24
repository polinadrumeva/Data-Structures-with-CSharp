using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BitcoinWalletManagementSystem
{
    public class Wallet
    {
        //public Wallet(string id, string userId)
        //{
        //    this.Id = id;
        //    this.UserId = userId;
        //}

        //public Wallet(string id, string userId, long balance)
        //{
        //    this.Id = id;
        //    this.UserId = userId;
        //    this.Balance = balance;
        //}
        public Wallet()
        {
            this.Transactions = new List<Transaction>();
        }
        public string Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public long Balance { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}