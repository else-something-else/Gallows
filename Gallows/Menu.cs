using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallows;

public static class Menu
{
    public static void Start()
    {
        bool menuExit = false;

        Console.Clear();
        Console.WriteLine("1 - играть или играть заново.\n0 - выход");

        while (!menuExit)
        {
            if (!int.TryParse(Console.ReadLine(), out var input))
            {
                Console.WriteLine("Некорректный ввод! Повторите попытку.");
                Console.WriteLine("1 - играть или играть заново.\n0 - выход");
                continue;
            }
            else
            {
                switch ((Gallows.MenuItems)input)
                {
                    case Gallows.MenuItems.Play:
                        menuExit = true;
                        break;
                    case Gallows.MenuItems.Exit:
                        Console.WriteLine("Не играем!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Неверный выбор!"); 
                        Console.WriteLine("1 - играть или играть заново.\n0 - выход");
                        break;
                }
            }
        }
    }
}