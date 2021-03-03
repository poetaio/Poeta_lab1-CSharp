using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Entities
{
    class User : EntityBase
    {
        private string _name;
        private string _surname;
        private string _email;
        private List<Category> _categories;
        private List<Wallet> _wallets;
        
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
            get { return $"{Name} {Surname}"; }
            private set { } 
        }
        public string Email 
        {
            get { return _email; } 
            set { _email = value; }
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
