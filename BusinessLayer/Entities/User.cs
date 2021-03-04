using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Entities
{
    public class User : EntityBase
    {
        private string _name;
        private string _surname;
        private string _email;
        private Dictionary<string, Category> _categories;
        private Dictionary<string, Wallet> _wallets;
        
        public string Name 
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Surname 
        {
            get { return _surname; }
            set { _surname = value; } 
        }
        public string FullName 
        { 
            get 
            { 
                return $"{Name} {Surname}".Trim(); 
            }
            private set { }
        }
        public string Email 
        {
            get { return _email; } 
            set { _email = value; }
        }

        public User() 
        {
            _categories = new Dictionary<string, Category>();
            _wallets = new Dictionary<string, Wallet>();
        }

        public void AddWallet(Wallet newWallet)
        {
            _wallets.Add(newWallet.Name, newWallet);
        }

        public bool RemoveWallet(string name)
        {
            return _wallets.Remove(name);
        }

        public Wallet GetWallet(string name)
        {
            Wallet resWallet;
            _wallets.TryGetValue(name, out resWallet);
            return resWallet;
        }

        public override bool Validate()
        {
            return !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Surname);
        }


    }
}
