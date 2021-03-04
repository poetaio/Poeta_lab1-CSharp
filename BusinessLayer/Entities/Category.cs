using System;
using System.Drawing;

namespace BusinessLayer.Entities
{
    public class Category : EntityBase
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
    }
}
