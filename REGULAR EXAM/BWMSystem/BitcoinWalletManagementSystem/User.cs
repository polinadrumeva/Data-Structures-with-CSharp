using System.Collections.Generic;

namespace BitcoinWalletManagementSystem
{
    public class User
    {
        //public User()
        //{
        //}

        //public User(string id, string name, string email)
        //{
        //    this.Id = id;
        //    this.Name = name;
        //    this.Email = email;
        //}

        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

		public List<Transaction> Transactions { get; set; }
	}
}