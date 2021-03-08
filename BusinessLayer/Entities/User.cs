using System;
using System.Collections.Generic;

namespace BusinessLayer.Entities
{
    public class User : EntityBase
    {
        private string _name;
        private string _surname;
        private string _email;

        private List<Category> _categories;
        private List<Wallet> _wallets;
        
       
        public string Name { get => _name; set => _name = value; }
        public string Surname { get => _surname; set => _surname = value; }
        public string FullName
        {
            get
            { return $"{Name} {Surname}".Trim(); }
            private set { }
        }
        public string Email { get => _email; set => _email = value; }
        public List<Category> Categories { get => _categories; set => _categories = value; }
        public List<Wallet> Wallets { get => _wallets; set => _wallets = value; }

        public User(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Categories = new List<Category>();
            Wallets = new List<Wallet>();
        }

        public override bool Validate()
        {
            return !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Surname);
        }
    }
}
