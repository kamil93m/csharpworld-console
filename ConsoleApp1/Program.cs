using System;
using System.Collections;
using System.IO;

namespace ConsoleApp1
{
    class WorkingLine
    {
        private int lineNumber;
        private String lineText;

        public WorkingLine(string lineText)
        {
            this.lineNumber = 0;
            this.lineText = lineText;
        }

        public void setLineNumber(int number)
        {
            this.lineNumber = number;
        }

        public int getLineNumber()
        {
            return this.lineNumber;
        }
        public String getLineText()
        {
            return this.lineText;
        }
    }

    class LineToWrite
    {
        private int lineNumber;
        private String lineText;

        public LineToWrite(int lineNumber, string lineText)
        {
            this.lineNumber = lineNumber;
            this.lineText = lineText;
        }

        public String write()
        {
            return lineNumber + ": " + lineText;
        }
    }
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
            Console.WriteLine("This is HelloWorld application. Application allows to [read] data to file, and [write] them to file.");
            System.Diagnostics.Debug.WriteLine(fileName);
            //Setting of dataFile path - end

            //Main functionality - start
            switch (Console.ReadLine())
            {
                case "read":
                    ArrayList textBuffer = new ArrayList();
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        String lineBuffer;
                        do
                        {
                            lineBuffer = sr.ReadLine();
                            if (lineBuffer != null)
                            {
                                textBuffer.Add(new WorkingLine(lineBuffer));
                                (textBuffer[textBuffer.Count-1] as WorkingLine).setLineNumber(textBuffer.Count);
                            }
                        } while (lineBuffer != null);
                    }
                    foreach(WorkingLine line in textBuffer)
                    {
                        LineToWrite lineToWrite = new LineToWrite(line.getLineNumber(), line.getLineText());
                        Console.WriteLine(lineToWrite.write());
                    }
                    Console.WriteLine("Working in progress... this function coming soon.");
                    break;

                case "write":
                    Console.WriteLine("What do you want to write to file?");
                    var inputBuffer = Console.ReadLine();
                    using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(fileName, true))
                    {
                        file.WriteLine(inputBuffer);
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
