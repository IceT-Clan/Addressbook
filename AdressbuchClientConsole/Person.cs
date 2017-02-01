using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch
{
    class Person
    {
        private string name;           
        private string address;        
        private DateTime birth_data;   
        private string phone_h;        
        private string phone_w;        
        private string email;          
        private string color;
        private int height;                  
        private int weight;
        private string blood;
        private string eye;
        private string hair;
        private string hair_color;

        public string Name {
            get {
                return name;
            }

            set {
                name = value;
            }
        }

        public string Address {
            get {
                return address;
            }

            set {
                address = value;
            }
        }

        public DateTime Birth_data 
        {
            get {
                return birth_data;
            }

            set {
                birth_data = value;
            }
        }

        public string Phone_h {
            get {
                return phone_h;
            }

            set {
                phone_h = value;
            }
        }

        public string Phone_w {
            get {
                return phone_w;
            }

            set {
                phone_w = value;
            }
        }

        public string Email {
            get {
                return email;
            }

            set {
                email = value;
            }
        }

        public string Color {
            get {
                return color;
            }

            set {
                color = value;
            }
        }

        public int Height {
            get {
                return height;
            }

            set {
                height = value;
            }
        }

        public int Weight {
            get {
                return weight;
            }

            set {
                weight = value;
            }
        }

        public string Blood {
            get {
                return blood;
            }

            set {
                blood = value;
            }
        }

        public string Eye {
            get { return eye; }

            set { eye = value; }
        }

        public string Hair
        {
            get { return this.hair; }
            set { this.hair = value; }
        }
        public string Hair_color
        {
            get { return hair_color; }
            set { hair_color = value; }
        }

        public Person(string name,
                      string address,
                      DateTime birth_data,
                      string phone_h,
                      string phone_w,
                      string email,
                      string color,
                      int height,
                      int weight,
                      string blood,
                      string eye,
                      string hair)
        {
            this.name = name;
            this.address = address;
            this.birth_data = birth_data;
            this.phone_h = phone_h;
            this.phone_w = phone_w;
            this.email = email;
            this.color = color;
            this.height = height;
            this.weight = weight;
            this.blood = blood;
            this.eye = eye;
            this.hair = hair;
        }

        public override String ToString()
        {
            string formatted = "";
            formatted += "Name: " + this.name + "\n";
            formatted += "Address: " + this.address + "\n";
            formatted += "Phone Home: " + this.phone_h + "\n";
            formatted += "Phone Work: " + this.phone_w + "\n";
            formatted += "Email: " + this.email + "\n";
            formatted += "Color: " + this.color + "\n";
            formatted += "Height: " + this.height + "\n";
            formatted += "Weight: " + this.weight + "\n";
            formatted += "Blood: " + this.blood + "\n";
            formatted += "Eye Color: " + this.eye + "\n";
            formatted += "Hair: " + this.hair + "\n";
            formatted += "Hair Colro: " + this.hair_color + "\n";

            return formatted;
        }
    }
}