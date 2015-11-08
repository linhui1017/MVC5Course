using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class MyClass
    {
       public int MyProperty { get; set; }

       enum MyEnum
       {
           Male,
           Female
       }
      
       public MyClass()
       {
           
           ConsoleColor c= ConsoleColor.Black;
           switch (c)
           {
               case ConsoleColor.Black:
                   break;
               case ConsoleColor.Blue:
                   break;
               case ConsoleColor.Cyan:
                   break;
               case ConsoleColor.DarkBlue:
                   break;
               case ConsoleColor.DarkCyan:
                   break;
               case ConsoleColor.DarkGray:
                   break;
               case ConsoleColor.DarkGreen:
                   break;
               case ConsoleColor.DarkMagenta:
                   break;
               case ConsoleColor.DarkRed:
                   break;
               case ConsoleColor.DarkYellow:
                   break;
               case ConsoleColor.Gray:
                   break;
               case ConsoleColor.Green:
                   break;
               case ConsoleColor.Magenta:
                   break;
               case ConsoleColor.Red:
                   break;
               case ConsoleColor.White:
                   break;
               case ConsoleColor.Yellow:
                   break;
               default:
                   break;
           }
       }


        
    }
}