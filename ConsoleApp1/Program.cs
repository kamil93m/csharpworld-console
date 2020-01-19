using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setting of dataFile path - start
            var dataDirectiory = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "CSharp\\Data");
            System.Diagnostics.Debug.WriteLine(dataDirectiory);
            if (!Directory.Exists(dataDirectiory))
            {
                DirectoryInfo pathOfDataDirectory = Directory.CreateDirectory(dataDirectiory);
            }

            var fileName = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "Csharp\\Data\\dataFile.txt");
            Console.WriteLine("This is HelloWorld application. Application allows to read data to file, and write them to file.");
            System.Diagnostics.Debug.WriteLine(fileName);
            //Setting of dataFile path - end

            //Main functionality start
            switch (Console.ReadLine())
            {
                case "read":
                    Console.WriteLine("Working in progress... this function coming soon.");
                    break;

                case "write":
                    using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(fileName, true))
                    {
                        file.WriteLine("Fourth line");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid option, application will be closed...");
                    break;
            }
            //Main functionality - end

            Console.WriteLine("Press any key to close application...");
            Console.ReadKey();
        }
    }
}
