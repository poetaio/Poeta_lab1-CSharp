using System;
using System.Text.RegularExpressions;

namespace BusinessLayer.Entities
{
    public class Transaction : EntityBase, IComparable<Transaction>
    {
        private double _sum;
        private string _name;
        private string _description;
        private string _currency;
        private Category _category;
        private DateTime _date;

        public double Sum
        {
            get { return _sum; }
            set { _sum = value; }
        }
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

        public int CompareTo(Transaction other)
        {
            if (other == null)
                return 1;
            if (Date.CompareTo(other.Date) == 0)
                return Name.CompareTo(other.Name);

            return Date.CompareTo(other.Date);
        }

        public override bool Validate()
        {
            return !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Description) &&
                new Regex("[A-Z]{3}").IsMatch(Currency);
        }

        public override string ToString()
        {
            string sum = (Sum > 0 ? "+" : "-") + Math.Abs(Sum);
            return $"Transaction \"{Name}\" - {Description}" +
                $"\n{sum}{Currency} on {Date}\n" +
                $"Category: {Category}";
        }
    }
}
