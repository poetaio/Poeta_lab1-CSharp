using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace BusinessLayer.Entities
{
    public class Wallet : EntityBase
    {

        private string _name;
        private string _description;
        private string _currency;
        private readonly double _initialBalance;
        private SortedSet<Category> _categories;
        private SortedSet<Transaction> _sortedByDateTransactions;
        private Dictionary<string, Transaction> _nameTransactionDictionary;

        public string Name { get => _name; set => _name = value; }
        public double Balance 
        { 
            get 
            { 
                double res = 0.0; 
                foreach (Transaction transaction in Transactions)
                {
                    res += transaction.Sum;
                }
                return res + _initialBalance; 
            } 
        }
        public string Description { get => _description; set => _description = value; }
        public string Currency { get => _currency; set => _currency = value; }
        public SortedSet<Category> Categories { get => _categories; private set => _categories = value; }
        public SortedSet<Transaction> Transactions { get => _sortedByDateTransactions; set => _sortedByDateTransactions = value; }

        public Wallet(double initialBalance = 0.0)
        {
            _initialBalance = initialBalance;
            Transactions = new SortedSet<Transaction>();
            _nameTransactionDictionary = new Dictionary<string, Transaction>();
        }

        public void AddTransaction(Transaction newTransaction)
        {
            Transactions.Add(newTransaction);
            _nameTransactionDictionary.Add(newTransaction.Name, newTransaction);
        }

        public bool RemoveTransaction(string transactionToRemoveName)
        {
            Transaction transactionToRemove;
            
            if (_nameTransactionDictionary.TryGetValue(transactionToRemoveName, out transactionToRemove)) 
            {
                _nameTransactionDictionary.Remove(transactionToRemoveName);
                return Transactions.Remove(transactionToRemove);
            } 
            return false;
        }

        public Transaction GetTransaction(string transactionName)
        {
            Transaction resTransaction;
            _nameTransactionDictionary.TryGetValue(transactionName, out resTransaction);
            return null;
        }

        public double GetLastMonthIncome()
        {
            double result = 0.0;
            foreach (Transaction transaction in LastMonthTransactions)
            {
                if (transaction.Sum > 0)
                    result += transaction.Sum;
            }
            return result;
        }

        public double GetLastMonthExpenses()
        {
            double result = 0.0;
            foreach (Transaction transaction in LastMonthTransactions)
            {
                if (transaction.Sum < 0)
                    result += transaction.Sum;
            }
            return -result;
        }

        public void AddCategory(Category newCategory)
        {
            Categories.Add(newCategory);
        }

        public override bool Validate()
        {
            return !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Description) &&
                new Regex("[A-Z]{3}").IsMatch(Currency);
        }

        private SortedSet<Transaction> LastMonthTransactions
        {
            get
            {
                Transaction currentTimeTransaction = new Transaction { Date = DateTime.Now };
                Transaction monthAgoTransaction = new Transaction { Date = DateTime.Now.AddDays(-30) };
                return Transactions.GetViewBetween(monthAgoTransaction, currentTimeTransaction);
            }
        }
    }
}
