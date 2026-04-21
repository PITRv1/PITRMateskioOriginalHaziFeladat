using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CPPC
{
    internal class Program
    {
        public List<Menu> MainMenuOption;

        static void Main(string[] args) {
            MenuOption optionOne = new MenuOption("title", ConsoleKey.D1, () => { Console.WriteLine("option1 selected"); } ) ;
            MenuOption optionTwo = new MenuOption("Other title", ConsoleKey.D2, () => { Console.WriteLine("option2 selected"); });


            Menu newMenu = new Menu("Hello Im your first menu", "I am a secondary message", new List<MenuOption>{ optionOne });

            DisplayMenu(newMenu);
            ConsoleKeyInfo input = ReadInput();
            MenuOption? selectedOption = GetOptionFromKey(input, newMenu);

            if (selectedOption == null) ;


        }

        public static void DisplayMenu(Menu menu) {
            ClearConsole();

            Console.WriteLine(menu.mainText);
            foreach(MenuOption option in menu.menuOptions) {
                Console.WriteLine(option.optionTitle);
            }

            Console.WriteLine(menu.message);

        }

        public static ConsoleKeyInfo ReadInput() {
            Console.WriteLine("==========================================");
            Console.Write("> ");
            return Console.ReadKey();
        }

        public static MenuOption? GetOptionFromKey(ConsoleKeyInfo keyInfo, Menu currentMenu) {
            foreach(MenuOption option in currentMenu.menuOptions) {
                if (option.optionTrigger == keyInfo.Key) {
                    return option;
                }
            }
            return null;
        }

        public static void ClearConsole() {
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.WindowWidth * Console.WindowHeight));
            Console.SetCursorPosition(0, 0);
        }

        public static void WriteToConsole() {

        }
    }
}
