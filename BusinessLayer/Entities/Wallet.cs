namespace BusinessLayer.Entities
{
    public class Wallet : EntityBase
    {

        private string _name;
        private double _balance;
        private string _description;
        private string _currency;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
