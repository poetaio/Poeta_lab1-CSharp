using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace BusinessLayer.Entities
{
    public class Wallet : EntityBase, IComparable<Wallet>
    {

        private string _name;
        private string _description;
        private string _currency;
        private decimal _balance;
        private List<Category> _categories;
        private SortedSet<Transaction> _transactionsSet;
        private List<Transaction> _transactionsList;

        public string Name { get => _name; set => _name = value; }
        public decimal Balance 
        { 
            get { return _balance; } 
            set { _balance = value; }
        }
        public string Description { get => _description; set => _description = value; }
        public string Currency { get => _currency; set => _currency = value; }
        public List<Category> Categories { get => _categories; set => _categories = value; }

        public Wallet(string name, string description, string currency, decimal balance)
        {
            _name = name;
            _description = description;
            _currency = currency;
            _balance = balance;
            _categories = new List<Category>();
            _transactionsList = new List<Transaction>();
            _transactionsSet = new SortedSet<Transaction>();
        }

        public void AddTransaction(Transaction newTransaction)
        {
            _transactionsList.Add(newTransaction);
            _transactionsSet.Add(newTransaction);
            _balance += newTransaction.Sum;
        }

        public bool RemoveTransaction(string transactionToRemoveName)
        {
            _transactionsList.RemoveAll(x => x.Name == transactionToRemoveName);
            return _transactionsSet.RemoveWhere(x => x.Name == transactionToRemoveName) > 0;
        }

        // setters
        public void setTransactionSum(int index, decimal newSum)
        {
            Balance += -_transactionsList[index].Sum + newSum;
            _transactionsList[index].Sum = newSum;
        }

        public void setTransactionName(int index, string newName)
        {
            _transactionsList[index].Name = newName;
        }

        public void setTransactionDescription(int index, string newDescription)
        {
            _transactionsList[index].Description = newDescription;
        }

        public void setTransactionCurrency(int index, string newCurrency)
        {
            _transactionsList[index].Currency = newCurrency;
        }

        public void setTransactionCategory(int index, Category newCategory)
        {
            _transactionsList[index].Category = newCategory;
        }

        public void setTransactionDate(int index, DateTime newDate)
        {
            Transaction transaction = _transactionsList[index];
            _transactionsSet.Remove(transaction);
            transaction.Date = newDate;
            _transactionsSet.Add(transaction);
            _transactionsList[index].Date = newDate;
        }

        // getters
        public decimal getTransactionSum(int index)
        {
            return _transactionsList[index].Sum;
        }

        public string getTransactionName(int index)
        {
            return _transactionsList[index].Name;
        }

        public string getTransactionDescription(int index)
        {
            return _transactionsList[index].Description;
        }

        public string getTransactionCurrency(int index)
        {
            return _transactionsList[index].Currency;
        }

        public Category getTransactionCategory(int index)
        {
            return _transactionsList[index].Category;
        }

        public DateTime getTransactionDate(int index)
        {
            return _transactionsList[index].Date;
        }

        public decimal GetLastMonthIncome()
        {
            return LastMonthTransactionsTotal(true);
        }

        public decimal GetLastMonthExpenses()
        {
            return LastMonthTransactionsTotal(false);
        }

        public override bool Validate()
        {
            return !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Description) &&
                new Regex("[A-Z]{3}").IsMatch(Currency);
        }

        public string DisplayTransactions(int from, int to)
        {
            string res = "";

            if (from < 0 || to > _transactionsList.Count || from >= to)
                throw new ArgumentException("Invalid indexes");

            foreach (Transaction transaction in _transactionsList)
            {
                res += transaction.ToString() + '\n';
            }

            return res;
        }

        public int CompareTo(Wallet other)
        {
            if (other == null)
                return 1;

            return Name.CompareTo(other.Name);
        }

        private decimal LastMonthTransactionsTotal(bool positive)
        {
            Transaction currentTimeTransaction = new Transaction (0.0m, "Current time transaction", "", 
                null, null, DateTime.Now );
            Transaction monthAgoTransaction = new Transaction ( 0.0m, "Month ago transaction", "",
                null, null, DateTime.Now.AddDays(-30));

            decimal result = 0.0m;
            foreach (Transaction transaction in _transactionsSet.GetViewBetween(monthAgoTransaction, currentTimeTransaction))
            {
                if (positive && transaction.Sum > 0)
                    result += transaction.Sum;
                else if (!positive && transaction.Sum < 0)
                    result += -transaction.Sum;
            }
            return result;
            
        }
    }
}
