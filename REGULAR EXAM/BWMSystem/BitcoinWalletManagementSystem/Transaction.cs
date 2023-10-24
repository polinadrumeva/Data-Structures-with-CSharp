using System;

namespace BitcoinWalletManagementSystem
{
    public class Transaction
    {
        //public Transaction()
        //{

        //}

        //public Transaction(string id, string senderId, string receiveerId, long amount, DateTime date)
        //{
        //    this.Id = id;
        //    this.SenderWalletId = senderId;
        //    this.ReceiverWalletId = receiveerId;
        //    this.Amount = amount;
        //    this.Timestamp = date;
        //}

        public string Id { get; set; }

        public string SenderWalletId { get; set; }

        public string ReceiverWalletId { get; set; }

        public long Amount { get; set; }

        public DateTime Timestamp { get; set; }
    }
}

