using System;
using System.Text;


namespace Gallows
{
    public class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Menu.Start();
            Game.StartGame();
        }
    }

}