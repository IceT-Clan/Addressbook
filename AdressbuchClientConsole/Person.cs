using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch {
    class Person {
        private String name;
        private String address;
        private DateTime birth_data;
        private String phone_h;
        private String phone_w;
        private String email;
        private String color;
        private Int32 height;
        private Int32 weight;
        private String blood;
        private String eye;
        private String hair;
        private String hair_color;

        public String Name {
            get { return this.name; }
            set { this.name = value; }
        }

        public String Address {
            get { return this.address; }
            set { this.address = value; }
        }

        public DateTime Birth_data {
            get { return this.birth_data; }
            set { this.birth_data = value; }
        }

        public String Phone_h {
            get { return this.phone_h; }
            set { this.phone_h = value; }
        }

        public String Phone_w {
            get { return this.phone_w; }
            set { this.phone_w = value; }
        }

        public String Email {
            get { return this.email; }
            set { this.email = value; }
        }

        public String Color {
            get { return this.color; }
            set { this.color = value; }
        }

        public Int32 Height {
            get { return this.height; }
            set { this.height = value; }
        }

        public Int32 Weight {
            get { return this.weight; }
            set { this.weight = value; }
        }

        public String Blood {
            get { return this.blood; }
            set { this.blood = value; }
        }

        public String Eye {
            get { return this.eye; }
            set { this.eye = value; }
        }

        public String Hair {
            get { return this.hair; }
            set { this.hair = value; }
        }
        public String Hair_color {
            get { return this.hair_color; }
            set { this.hair_color = value; }
        }

        public Person(String name,
                      String address,
                      DateTime birth_data,
                      String phone_h,
                      String phone_w,
                      String email,
                      String color,
                      Int32 height,
                      Int32 weight,
                      String blood,
                      String eye,
                      String hair,
                      String hair_color) {
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
            String formatted = "";
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