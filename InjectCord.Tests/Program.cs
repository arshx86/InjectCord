#region

using System;
using System.Net;
using System.Windows;

#endregion

namespace InjectCord.Tests
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {

            Console.Title = "InjectCord";
            Help();

            string c = Console.ReadKey().Key.ToString();
            Console.Clear();
            switch (c)
            {
                case "D1": // clipboard
                    if (Clipboard.ContainsText(TextDataFormat.Text))
                    {
                        string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                        Console.WriteLine(InjectCord.Inject(clipboardText) ? "Injected" : "Failed");
                    }
                    else
                    {
                        Console.WriteLine("The clipboard doesn't contain any text. Please copy the code.");
                        Console.ReadKey();
                        Main();
                    }
                    break;
                case "D2": // link
                    Console.WriteLine("> Paste the link of the code.");
                    string hLink = Console.ReadLine();
                    using (WebClient wc = new WebClient())
                    {
                        Console.WriteLine(
                            InjectCord.Inject(wc.DownloadString(hLink))
                                ? "Injected" : "Failed");
                    }
                    break;
                case "D3": // scratch
                    Console.WriteLine("> Write the code to paste");
                    string sCode = Console.ReadLine();
                    Console.WriteLine(InjectCord.Inject(sCode) ? "Injected" : "Failed");
                    break;
                case "D4": // uninject
                    Console.WriteLine(InjectCord.Revert() ? "Revert Success" : "Failed");
                    break;
                default: Main(); break;
            }
            Console.ReadKey();
            Environment.Exit(0);
        }

        static void Help()
        {

            Console.Clear();

            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("1: [Inject] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("A code from clipboard");

            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("2: [Inject] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("A code from link");

            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("3: [Inject] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("A code from scratch");

            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("4: [UnInject] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Revert back to default state");

            Console.Write("\n");

        }

    }
}