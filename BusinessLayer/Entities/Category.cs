using System;
using System.Drawing;

namespace BusinessLayer.Entities
{
    public class Category : EntityBase, IComparable<Category>
    {
        private string _name;
        private string _description;
        private Color _color;
        private string _icon;

        public string Name 
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public Color Color 
        {
            get { return _color; }
            set { _color = value; }
        }
        public string Icon 
        {
            get { return _icon; }
            set { _icon = value; } 
        }

        public Category(string name, string description, Color color, string icon)
        {
            _name = name;
            _description = description;
            _color = color;
            _icon = icon;
        }

        public override bool Validate()
        {
            return !String.IsNullOrWhiteSpace(Name) && 
                !String.IsNullOrWhiteSpace(Description);
        }

        public override string ToString()
        {
            return $"Category \"{Name}\"\nDescription: {Description}\n" +
                $"Color: {Color}\nIcon: {Icon}";
        }

        public int CompareTo(Category other)
        {
            if (other == null)
                return 1;

            return Name.CompareTo(other.Name);
        }
    }
}
