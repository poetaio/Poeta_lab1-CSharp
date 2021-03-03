using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Entities
{
    public class Transaction : EntityBase
    {
        private string _name;
        private string _currency;
        private Category _category;
        private string _description;
        private DateTime _date;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }
        public Category Category
        {
            get { return _category; }
            set { _category = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
