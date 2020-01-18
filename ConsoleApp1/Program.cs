using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is HelloWorld application. Application allows to read data to file, and write them to file.");
            
            switch (Console.ReadLine())
            {
                case "read":
                    Console.WriteLine("Working in progress... this function coming soon.");
                    break;

                case "write":
                    using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(@"C:\Users\Kamil\Desktop\WriteLines2.txt", true))
                    {
                        file.WriteLine("Fourth line");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid option, application will be closed...");
                    break;
            }
            Console.WriteLine("Press any key to close application...");
            Console.ReadKey();
        }
    }
}
