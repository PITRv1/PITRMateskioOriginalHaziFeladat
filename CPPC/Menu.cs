using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPC
{
    public struct MenuOption {
        public string optionTitle;
        public ConsoleKey optionTrigger;
        public Action optionAction;

        public MenuOption(string title, ConsoleKey binding, Action action) {
            optionTitle = title;
            optionTrigger = binding;
            optionAction = action;
        }

    }

    internal class Menu
    {
        public string mainText;
        public string message;
        public List<MenuOption> menuOptions;

        public Menu(string mainText, string message, List<MenuOption> menuOptions) {
            this.mainText = mainText;
            this.message = message;
            this.menuOptions = menuOptions;
        }
    }
}
