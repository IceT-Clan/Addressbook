﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace People {
    class Person {
        private String name;
        private String address;
        private DateTime birth_data;
        private String phone_h;
        private String phone_w;
        private String email;
        private String color;
        private Single height;
        private Single weight;
        private String blood;
        private String eye;
        private String hair;
        public const String dateFormat = "yyyy-MM-dd";

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
        public Single Height {
            get { return this.height; }
            set { this.height = value; }
        }
        public Single Weight {
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

        public Person(String name,
                      String address,
                      DateTime birth_data,
                      String phone_h,
                      String phone_w,
                      String email,
                      String color,
                      Single height,
                      Single weight,
                      String blood,
                      String eye,
                      String hair) {
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

        public Person() { }

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
        }

        public Person(String csvline, Char seperator=',') {
            List<String> entries = new List<string>();
            String currentEntry = null;
            Boolean inApostrophe = false;
            foreach (Char character in csvline) {
                // toggle inApostrophe when encountering a apostrophe
                if (character == '\"') {
                    inApostrophe = inApostrophe ? false : true;
                    continue;
                } else if (character == seperator && !inApostrophe) {
                    entries.Add(currentEntry);
                    currentEntry = null;
                    continue;
                }
                currentEntry += character;
            }
            entries.Add(currentEntry); // Add last entry

            switch (entries.Count) {
                default:
                case 12:
                    this.hair = entries[11];
                    goto case 11;
                case 11:
                    this.eye = entries[10];
                    goto case 10;
                case 10:
                    this.blood = entries[9];
                    goto case 9;
                case 9:
                    try {
                        this.weight = Convert.ToSingle(entries[8]);
                    } catch (FormatException) {
                        this.weight = 0;
                    }
                    goto case 8;
                case 8:
                    try {
                        this.height = Convert.ToSingle(entries[7]);
                    } catch (FormatException) {
                        this.height = 0;
                    }
                    goto case 7;
                case 7:
                    this.color = entries[6];
                    goto case 6;
                case 6:
                    this.email = entries[5];
                    goto case 5;
                case 5:
                    this.phone_w = entries[4];
                    goto case 4;
                case 4:
                    this.phone_h = entries[3];
                    goto case 3;
                case 3:
                    DateTime.TryParseExact(entries[2], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out this.birth_data);
                    // FIXME: ^ always returns 1
                    goto case 2;
                case 2:
                    this.address = entries[1];
                    goto case 1;
                case 1:
                    this.name = entries[0];
                    break;
            }
        }
        /// <Summary>
        /// Return all Properties of Person as a string
        /// </Summary>
        public override String ToString() {
            String formatted = "";
            formatted += "Name: " + this.name + " \n";
            formatted += "Address: " + this.address + " \n";
            formatted += "Birth Data: " + this.birth_data.ToString(dateFormat) + " \n";
            formatted += "Phone Home: " + this.phone_h + " \n";
            formatted += "Phone Work: " + this.phone_w + " \n";
            formatted += "Email: " + this.email + " \n";
            formatted += "Color: " + this.color + " \n";
            formatted += "Height: " + this.height + " \n";
            formatted += "Weight: " + this.weight + " \n";
            formatted += "Blood: " + this.blood + " \n";
            formatted += "Eye Color: " + this.eye + " \n";
            formatted += "Hair: " + this.hair + " \n";

            return formatted;
        }

        public String ToString(Char seperator) {
            String formatted = "";
            formatted += Surround(this.name) + seperator;
            formatted += Surround(this.address) + seperator;
            formatted += Surround(this.birth_data.ToString(dateFormat)) + seperator;
            formatted += Surround(this.phone_h) + seperator;
            formatted += Surround(this.phone_w) + seperator;
            formatted += Surround(this.email) + seperator;
            formatted += Surround(this.color) + seperator;
            formatted += Surround(this.height.ToString()) + seperator;
            formatted += Surround(this.weight.ToString()) + seperator;
            formatted += Surround(this.blood) + seperator;
            formatted += Surround(this.eye) + seperator;
            formatted += Surround(this.hair) + seperator;

            return formatted;
        }

        public List<Object> ToList() => new List<Object> {
                this.name,
                this.address,
                this.birth_data,
                this.phone_h,
                this.phone_w,
                this.email,
                this.color,
                this.height,
                this.weight,
                this.blood,
                this.eye,
                this.hair
        };

        private String SurroundString(String s, Char c1='(', Char c2=')') => c1 + s + c2;
        private String Surround(String s, Char c='"') => SurroundString(s, c);
        private String SurroundString(String s, Char c='"') => c + s + c;
    }
}
