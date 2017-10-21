using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClickerSDK;
using ClickerSDK.Keyboard;

namespace MyClicker
{
    class Program
    {
        static void Main(string[] args)
        {
            MouseHelper.MoveMouse(1, 1);

            using (var keyboard = Keyboard.Instanse)
            {
                Console.Read();
                Console.Read(); Console.Read(); Console.Read(); Console.Read(); Console.Read();
            }

            Console.Read();
        }
    }
}
