using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            journal.Run();
        }
    }

    class Journal
    {
        private List<Entry> entries;
        private List<string> prompts;

        public Journal()
        {
            entries = new List<Entry>();
            prompts = new List<string>
            {
                "Who was the most interesting person I interacted with today?",
                "What was the best part of my day?",
                "How did I see the hand of the Lord in my life today?",
                "What was the strongest emotion I felt today?",
                "If I had one thing I could do over today, what would it be?"
            };
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display journal");
                Console.WriteLine("3. Save journal to file");
                Console.WriteLine("4. Load journal from file");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                
                switch (Console.ReadLine())
                {
                    case "1":
                        WriteEntry();
                        break;
                    case "2":
                        DisplayEntries();
                        break;
                    case "3":
                        SaveToFile();
                        break;
                    case "4":
                        LoadFromFile();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void WriteEntry()
        {
            Random random = new Random();
            string prompt = prompts[random.Next(prompts.Count)];
            Console.WriteLine(prompt);
            string response = Console.ReadLine();
            string date = DateTime.Now.ToShortDateString();
            entries.Add(new Entry(prompt, response, date));
        }

        private void DisplayEntries()
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"{entry.Date}: {entry.Prompt}\n{entry.Response}\n");
            }
        }

        private void SaveToFile()
        {
            Console.Write("Enter filename to save: ");
            string filename = Console.ReadLine();
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    outputFile.WriteLine($"{entry.Date}~{entry.Prompt}~{entry.Response}");
                }
            }
            Console.WriteLine("Journal saved successfully.");
        }

        private void LoadFromFile()
        {
            Console.Write("Enter filename to load: ");
            string filename = Console.ReadLine();
            if (File.Exists(filename))
            {
                entries.Clear();
                string[] lines = File.ReadAllLines(filename);
                foreach (var line in lines)
                {
                    var parts = line.Split('~');
                    if (parts.Length == 3)
                    {
                        entries.Add(new Entry(parts[1], parts[2], parts[0]));
                    }
                }
                Console.WriteLine("Journal loaded successfully.");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
    }

    class Entry
    {
        public string Prompt { get; }
        public string Response { get; }
        public string Date { get; }

        public Entry(string prompt, string response, string date)
        {
            Prompt = prompt;
            Response = response;
            Date = date;
        }
    }
}
