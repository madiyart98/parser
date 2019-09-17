using System;

namespace ConsoleApp3
{
    class Program
    {
       
        static void Main(string[] args)
        {
            int Start=0, End=0;
            Start = Convert.ToInt16(Console.ReadLine());
            End = Convert.ToInt16(Console.ReadLine());
            ParseHref parser = new ParseHref();
            parser.ParserHref(Start,End);
            Console.ReadKey();
            ParsePhones ph = new ParsePhones();
            ph.ParserPhones();
            Console.ReadKey();
            
        }
    }
}
