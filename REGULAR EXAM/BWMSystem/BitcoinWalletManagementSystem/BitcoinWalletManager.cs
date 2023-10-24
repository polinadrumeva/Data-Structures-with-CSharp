using System;
using System.Collections.Generic;
using System.Linq;

namespace BitcoinWalletManagementSystem
{
    public class BitcoinWalletManager : IBitcoinWalletManager
    {
        private Dictionary<string, User> users;
        private Dictionary<string, Wallet> wallets;
        private Dictionary<string, SortedList<string, Transaction>> transactions;
            

        public BitcoinWalletManager()
        {
            this.users = new Dictionary<string, User>();
            this.wallets = new Dictionary<string, Wallet>();
            this.transactions = new Dictionary<string, SortedList<string, Transaction>>();
            
        }

        public void CreateUser(User user)
        {
            users.Add(user.Id, user);
        }

        public void CreateWallet(Wallet wallet)
        {
			wallets.Add(wallet.Id, wallet);
        }

        public bool ContainsUser(User user)
        {
            return users.ContainsKey(user.Id);
        }

        public bool ContainsWallet(Wallet wallet)
        {
            return wallets.ContainsKey(wallet.Id);
        }

        public IEnumerable<Wallet> GetWalletsByUser(string userId)
        {
           return wallets.Values.Where(x => x.UserId == userId);
        }

        public void PerformTransaction(Transaction transaction)
        {

            if (!wallets.ContainsKey(transaction.SenderWalletId) || !wallets.ContainsKey(transaction.ReceiverWalletId) || wallets[transaction.SenderWalletId].Balance < transaction.Amount)
            {
                throw new ArgumentException();
            }

            if (!transactions.ContainsKey(wallets[transaction.SenderWalletId].UserId))
            {
                transactions.Add(wallets[transaction.SenderWalletId].UserId, new SortedList<string, Transaction>());
            }
            else if (!transactions.ContainsKey(wallets[transaction.ReceiverWalletId].UserId))
            { 
                transactions.Add(wallets[transaction.ReceiverWalletId].UserId, new SortedList<string, Transaction>());
            }

			wallets[transaction.SenderWalletId].Balance -= transaction.Amount;
            wallets[transaction.SenderWalletId].Transactions.Add(transaction);
            wallets[transaction.ReceiverWalletId].Balance += transaction.Amount;
			wallets[transaction.ReceiverWalletId].Transactions.Add(transaction);
			transactions[wallets[transaction.SenderWalletId].UserId].Add(transaction.Id, transaction);
            
            if (!transactions.ContainsKey(wallets[transaction.ReceiverWalletId].UserId))
            {
				transactions.Add(wallets[transaction.ReceiverWalletId].UserId, new SortedList<string, Transaction>());
			}

            transactions[wallets[transaction.ReceiverWalletId].UserId].Add(transaction.Id, transaction);
            
        }

        public IEnumerable<Transaction> GetTransactionsByUser(string userId)
        {
           return transactions[userId].Values;
        }

        public IEnumerable<Wallet> GetWalletsSortedByBalanceDescending()
        {
            return wallets.Values.OrderByDescending(x => x.Balance);
        }

        public IEnumerable<User> GetUsersSortedByBalanceDescending()
        {
            return users.Values.OrderByDescending(x => wallets.Values.Where(y => y.UserId == x.Id).Sum(y => y.Balance));
        }

        public IEnumerable<User> GetUsersByTransactionCount()
        {
           return this.users.Where(x => transactions.ContainsKey(x.Key)).Select(t => t.Value)
                    .OrderByDescending(x => transactions[x.Id].Count);  
        }
    }
}