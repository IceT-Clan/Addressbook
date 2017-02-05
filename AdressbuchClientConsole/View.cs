﻿using People;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Addressbook {
    enum ViewMode {
        Title,
        Connecting,
        Menu_Main,
        SingleEntry,
        MultipleEntries,
    }

    class View {
        private ViewMode mode;
        private List<Person> data;
        
        public ViewMode Mode {
            get { return this.mode; }
            set { this.mode = value; }
        }
        public List<Person> Data {
            get { return this.data; }
            set { this.data = value; }
        }

        /// <summary>
        /// Refresh Display
        /// </summary>
        public void Refresh() {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            switch (this.mode) {
                case ViewMode.Title:
                    Display_Title();
                    break;
                case ViewMode.Connecting:
                    Display_Title();
                    break;
                case ViewMode.Menu_Main:
                    Display_MenuMain();
                    break;
                case ViewMode.SingleEntry:
                    Display_MultipleEntries();
                    break;
                case ViewMode.MultipleEntries:
                    Display_MultipleEntries();
                    break;
            }
        }

        /// <summary>
        /// Set new ViewMode and Refresh Display
        /// </summary>
        /// <param name="mode">ViewMode to switch to</param>
        public void Refresh(ViewMode mode) {
            this.mode = mode;
            Refresh();
        }

        private void Display_Title() {
            const Int32 titleWidth = 48;
            const Int32 offsetY = 3;
            const Int32 offsetTitle_TextY = 8;
            Int32 offsetTitleX = Console.WindowWidth / 2 - titleWidth / 2;
            String offsetTitleXstr = MultiplyChar(' ', offsetTitleX);
            const String text = "Press a Button to start";
            Int32 offsetTextX = Console.WindowWidth / 2 - text.Length / 2;
            String offsetTextXstr = MultiplyChar(' ', offsetTextX);

            Console.Write(MultiplyChar('\n', offsetY));
            Console.WriteLine(offsetTitleXstr + @"  _____ _        _ _    ____              _    ");
            Console.WriteLine(offsetTitleXstr + @" / ____| |      | | |  |  _ \            | |   ");
            Console.WriteLine(offsetTitleXstr + @"| (___ | |_ __ _| | | _| |_) | ___   ___ | | __");
            Console.WriteLine(offsetTitleXstr + @" \___ \| __/ _` | | |/ /  _ < / _ \ / _ \| |/ /");
            Console.WriteLine(offsetTitleXstr + @" ____) | || (_| | |   <| |_) | (_) | (_) |   < ");
            Console.WriteLine(offsetTitleXstr + @"|_____/ \__\__,_|_|_|\_\____/ \___/ \___/|_|\_\");
            Console.Write(MultiplyChar('\n', offsetTitle_TextY));
            Console.WriteLine(offsetTextXstr + text);
        }

        private void Display_Connecting() {
            const Int32 titleWidth = 48;
            const Int32 offsetY = 2;
            const Int32 offsetTitle_TextY = 3;
            Int32 offsetTitleX = Console.WindowWidth / 2 - titleWidth / 2;
            String offsetTitleXstr = MultiplyChar(' ', offsetTitleX);
            const String text = "Press a Button to start";
            Int32 offsetTextX = Console.WindowWidth / 2 - text.Length / 2;
            String offsetTextXstr = MultiplyChar(' ', offsetTextX);

            Console.Write(MultiplyChar('\n', offsetY));
            Console.WriteLine(offsetTitleXstr + @"  _____ _        _ _    ____              _    ");
            Console.WriteLine(offsetTitleXstr + @" / ____| |      | | |  |  _ \            | |   ");
            Console.WriteLine(offsetTitleXstr + @"| (___ | |_ __ _| | | _| |_) | ___   ___ | | __");
            Console.WriteLine(offsetTitleXstr + @" \___ \| __/ _` | | |/ /  _ < / _ \ / _ \| |/ /");
            Console.WriteLine(offsetTitleXstr + @" ____) | || (_| | |   <| |_) | (_) | (_) |   < ");
            Console.WriteLine(offsetTitleXstr + @"|_____/ \__\__,_|_|_|\_\____/ \___/ \___/|_|\_\");
            Console.Write(MultiplyChar('\n', offsetTitle_TextY));
            Console.WriteLine("Connecting to Server..");
        }

        private void Display_MenuMain() {
            const Int32 titleWidth = 48;
            const Int32 menuWidth = 38;
            const Int32 offsetY = 2;
            const Int32 offsetTitle_MenuY = 2;
            Int32 offsetTitleX = Console.WindowWidth / 2 - titleWidth / 2;
            String offsetTitleXstr = MultiplyChar(' ', offsetTitleX);
            Int32 offsetMenuX = Console.WindowWidth / 2 - menuWidth / 2;
            String offsetMenuXstr = MultiplyChar(' ', offsetMenuX);

            Console.Write(MultiplyChar('\n', offsetY));
            Console.WriteLine(offsetTitleXstr + @"  _____ _        _ _    ____              _    ");
            Console.WriteLine(offsetTitleXstr + @" / ____| |      | | |  |  _ \            | |   ");
            Console.WriteLine(offsetTitleXstr + @"| (___ | |_ __ _| | | _| |_) | ___   ___ | | __");
            Console.WriteLine(offsetTitleXstr + @" \___ \| __/ _` | | |/ /  _ < / _ \ / _ \| |/ /");
            Console.WriteLine(offsetTitleXstr + @" ____) | || (_| | |   <| |_) | (_) | (_) |   < ");
            Console.WriteLine(offsetTitleXstr + @"|_____/ \__\__,_|_|_|\_\____/ \___/ \___/|_|\_\");
            Console.Write(MultiplyChar('\n', offsetTitle_MenuY));
            Console.WriteLine(offsetMenuXstr + "┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine(offsetMenuXstr + "┃      [1] - Search Person          ┃");
            Console.WriteLine(offsetMenuXstr + "┃      [2] - Load Complete A.B      ┃");
            Console.WriteLine(offsetMenuXstr + "┃      [3] - Modify Person          ┃");
            Console.WriteLine(offsetMenuXstr + "┃      [4] - Delete Person          ┃");
            Console.WriteLine(offsetMenuXstr + "┃      [5] - Delete Person IDs      ┃");
            Console.WriteLine(offsetMenuXstr + "┃      [6] - Delete AdressBook      ┃");
            Console.WriteLine(offsetMenuXstr + "┃      [9] - EXIT                   ┃");
            if (Debugger.IsAttached)
                Console.WriteLine(offsetMenuXstr + "┃     [10] - Debug View             ┃");
            Console.WriteLine(offsetMenuXstr + "┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
            Console.CursorTop = Console.WindowHeight;
            Console.Write("$ INPUT> ");
        }

        private void Display_MultipleEntries() {
            const Int32 offsetFromBorderX = 2;
            const Int32 borderWidth = 54;
            String offsetFromBorderXstr = MultiplyChar(' ', offsetFromBorderX);
            String borderHorizontal = MultiplyChar('━', borderWidth);
            foreach (Person person in this.data) {
                Console.WriteLine("┏" + borderHorizontal + "┓");
                Console.WriteLine("┃" + offsetFromBorderXstr + ("Name: " + person.Name.PadRight(borderWidth)).Substring(0, borderWidth - 2) + "┃");
                Console.WriteLine("┃" + offsetFromBorderXstr + ("Address: " + person.Address.PadRight(borderWidth)).Substring(0, borderWidth - 2) + "┃");
                Console.WriteLine("┃" + offsetFromBorderXstr + ("Birth Data: " + person.Birth_data.ToShortDateString().PadRight(100)).Substring(0, borderWidth - 2) + "┃");
                Console.WriteLine("┃" + offsetFromBorderXstr + ("Phone Home: " + person.Phone_h.PadRight(borderWidth)).Substring(0, borderWidth - 2) + "┃");
                Console.WriteLine("┃" + offsetFromBorderXstr + ("Phone Work: " + person.Phone_w.PadRight(borderWidth)).Substring(0, borderWidth - 2) + "┃");
                Console.WriteLine("┃" + offsetFromBorderXstr + ("Email: " + person.Email.PadRight(borderWidth)).Substring(0, borderWidth - 2) + "┃");
                Console.WriteLine("┃" + offsetFromBorderXstr + ("Color: " + person.Color.PadRight(borderWidth)).Substring(0, borderWidth - 2) + "┃");
                Console.WriteLine("┃" + offsetFromBorderXstr + ("Height: " + (person.Height.ToString() + "cm").PadRight(borderWidth)).Substring(0, borderWidth - 2) + "┃");
                Console.WriteLine("┃" + offsetFromBorderXstr + ("Weight: " + (person.Weight.ToString() + "kg").PadRight(borderWidth)).Substring(0, borderWidth - 2) + "┃");
                Console.WriteLine("┃" + offsetFromBorderXstr + ("Blood Type: " + person.Blood.PadRight(borderWidth)).Substring(0, borderWidth - 2) + "┃");
                Console.WriteLine("┃" + offsetFromBorderXstr + ("Eye Color: " + person.Eye.PadRight(borderWidth)).Substring(0, borderWidth - 2) + "┃");
                Console.WriteLine("┃" + offsetFromBorderXstr + ("Hair Type: " + person.Hair.PadRight(borderWidth)).Substring(0, borderWidth - 2) + "┃");
                Console.WriteLine("┣" + borderHorizontal + "┫");
            }
            Console.CursorTop--;
            Console.WriteLine("┗" + borderHorizontal + "┛");
        }

        /// <summary>
        /// Debug View
        /// </summary>
        public void Debug() {
            this.data = new List<Person> {
                new Person("Name",
                           "Address",
                           new DateTime(1337, 1, 1),
                           "Phone Home",
                           "Phone Work",
                           "Email",
                           "Color",
                           1337,
                           1337,
                           "Blood",
                           "Eye",
                           "Hair")
            };

            for (Int32 mode = 0; mode < 5; mode++) {
                Refresh((ViewMode)mode);
                System.Threading.Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Return a string with factor times character
        /// </summary>
        /// <param name="character">A Char to repeat</param>
        /// <param name="factor">How often to repeat</param>
        /// <returns></returns>
        private String MultiplyChar(Char character, Int32 factor) => "".PadRight(factor, character);
    }
}
