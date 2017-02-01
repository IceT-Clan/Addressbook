using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch {
    class Person {
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
            get { return this.name; }
            set { this.name = value; }
        }

        public string Address {
            get { return this.address; }
            set { this.address = value; }
        }

        public DateTime Birth_data {
            get { return this.birth_data; }
            set { this.birth_data = value; }
        }

        public string Phone_h {
            get { return this.phone_h; }
            set { this.phone_h = value; }
        }

        public string Phone_w {
            get { return this.phone_w; }
            set { this.phone_w = value; }
        }

        public string Email {
            get { return this.email; }
            set { this.email = value; }
        }

        public string Color {
            get { return this.color; }
            set { this.color = value; }
        }

        public int Height {
            get { return this.height; }
            set { this.height = value; }
        }

        public int Weight {
            get { return this.weight; }
            set { this.weight = value; }
        }

        public string Blood {
            get { return this.blood; }
            set { this.blood = value; }
        }

        public string Eye {
            get { return this.eye; }
            set { this.eye = value; }
        }

        public string Hair {
            get { return this.hair; }
            set { this.hair = value; }
        }
        public string Hair_color {
            get { return this.hair_color; }
            set { this.hair_color = value; }
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
                      string hair,
                      string hair_color) {
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
            this.hair_color = hair_color;
        }

        public Person(Person person) {
            this.name = person.name;
            this.address = person.address;
            this.birth_data = person.birth_data;
            this.phone_h = person.phone_h;
            this.phone_w = person.phone_w;
            this.email = person.email;
            this.color = person.color;
            this.height = person.height;
            this.weight = person.weight;
            this.blood = person.blood;
            this.eye = person.eye;
            this.hair = person.hair;
            this.hair_color = person.hair_color;
        }

        public override String ToString() {
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
            formatted += "Hair Color: " + this.hair_color + "\n";

            return formatted;
        }
    }
}