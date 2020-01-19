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
        public void setLineText(String text)
        {
            this.lineText = text;
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

            bool stopTheApp = false;
            bool editOptionRunning = false;
            String option = Console.ReadLine();
            ArrayList textBuffer = new ArrayList();

            //Main functionality - start
            while (!stopTheApp)
            {
                switch (option)
                {
                    case "read":
                        textBuffer = new ArrayList();
                        readDataFromFile(fileName, textBuffer);
                        foreach(WorkingLine line in textBuffer)
                        {
                            LineToWrite lineToWrite = new LineToWrite(line.getLineNumber(), line.getLineText());
                            Console.WriteLine(lineToWrite.write());
                        }

                        bool tortureUserAfterReadFile = true;
                        while (tortureUserAfterReadFile) { 
                            Console.WriteLine("Do you want to edit above text? [yes/no]");
                            switch (Console.ReadLine())
                            {
                                case "yes":
                                    Console.WriteLine("Ok. What to you want to do?" +
                                        "\n - Insert line [insert]" +
                                        "\n - Rewrite line [rewrite]" +
                                        "\n - Delete line [delete]");
                                    tortureUserAfterReadFile = true;
                                    while (tortureUserAfterReadFile)
                                    {
                                        option = Console.ReadLine();
                                        switch (option)
                                        {
                                            case "insert":
                                            case "rewrite":
                                            case "delete":
                                                tortureUserAfterReadFile = false;
                                                break;
                                            default:
                                                Console.WriteLine("Invalid option");
                                                tortureUserAfterReadFile = true;
                                                break;
                                        }
                                    }
                                    editOptionRunning = true;
                                    tortureUserAfterReadFile = false;
                                    break;
                                case "no":
                                    tortureUserAfterReadFile = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid selection");
                                    tortureUserAfterReadFile = true;
                                    break;
                            }
                        }
                        break;

                    case "write":
                        Console.WriteLine("What do you want to write to file?");
                        var inputBuffer = Console.ReadLine();
                        writeDataToFile(fileName, inputBuffer, true);
                        break;

                    case "insert":
                        Console.WriteLine("What number will have a inserted record? Ask is about the position of the new record in the file.");
                        int numberInsertedLine = getNumberFromUser();
                        Console.WriteLine("Ok. What contain do you want insert on position " + numberInsertedLine + "?");
                        String newText = new String(Console.ReadLine());
                        textBuffer.Insert(numberInsertedLine - 1, new WorkingLine(newText));
                        editOptionRunning = false;
                        reWriteDataInFile(fileName, textBuffer);
                        editOptionRunning = false;
                        break;

                    case "rewrite":
                        Console.WriteLine("Which line you want to rewrite? Please give a number line.");
                        int numberLineToRewrite = getNumberFromUser();
                        Console.WriteLine("Selected line with number: " + numberLineToRewrite + " has got following text:\n" +
                             (textBuffer[numberLineToRewrite - 1] as WorkingLine).getLineText());
                        Console.WriteLine("What would you like to replace it with? Please give a text...");
                        (textBuffer[numberLineToRewrite - 1] as WorkingLine).setLineText(Console.ReadLine());
                        Console.WriteLine("New contain of line " + numberLineToRewrite + " has been saved.");

                        reWriteDataInFile(fileName, textBuffer);
                        editOptionRunning = false;
                        break;

                    case "delete":
                        Console.WriteLine("Ok. You want do delete all of records in file or one selected record? [one/all]");
                        tortureUserAfterReadFile = true;
                        while (tortureUserAfterReadFile)
                        {
                            switch (Console.ReadLine())
                            {
                                case "one":
                                    Console.WriteLine("Enter record number to delete?");
                                    int numberLineToDelete = getNumberFromUser();
                                    textBuffer.RemoveAt(numberLineToDelete - 1);
                                    reWriteDataInFile(fileName, textBuffer);
                                    tortureUserAfterReadFile = false;
                                    break;

                                case "all":
                                    writeDataToFile(fileName, null ,false);
                                    tortureUserAfterReadFile = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid option");
                                    tortureUserAfterReadFile = true;
                                    break;
                            }
                        }
                        editOptionRunning = false;
                        break;

                    case "end":
                        stopTheApp = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

                if (stopTheApp == false && editOptionRunning == false)
                {
                    Console.WriteLine("\nWhat do you want to do now? [read/write/end]");
                    option = Console.ReadLine();
                }
            }
            //Main functionality - end

            Console.WriteLine("Press any key to close application...");
            Console.ReadKey();
        }

        public static void readDataFromFile(String fileName, ArrayList textBuffer)
        {   
            using (StreamReader sr = new StreamReader(fileName))
            {
                String lineBuffer;
                do
                {
                    lineBuffer = sr.ReadLine();
                    if (lineBuffer != null)
                    {
                        textBuffer.Add(new WorkingLine(lineBuffer));
                        (textBuffer[textBuffer.Count - 1] as WorkingLine).setLineNumber(textBuffer.Count);
                    }
                } while (lineBuffer != null);
            }
        }

        public static void writeDataToFile(String fileName, String data, bool append)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fileName, append))
            {
                file.WriteLine(data);
            }
        }
        public static void reWriteDataInFile(String fileName, ArrayList textBuffer)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fileName))
            {
                foreach (WorkingLine line in textBuffer)
                {
                    file.WriteLine(line.getLineText());
                }
            }
        }
        public static int getNumberFromUser()
        {
            int numberLine = 0;
            bool enteredNumberIsNumeric = false;
            while (!enteredNumberIsNumeric)
            {
                var endteredNumberLine = Console.ReadLine();
                enteredNumberIsNumeric = int.TryParse(endteredNumberLine, out numberLine);
                if (!enteredNumberIsNumeric) Console.WriteLine("Please give a number...");
            }
            return numberLine;
        }
    }
}
