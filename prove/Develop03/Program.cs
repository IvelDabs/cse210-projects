using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Word
{
    private string _text; 
    private bool _isHidden; 

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    // Hide the word by setting its hidden state to true
    public void Hide()
    {
        _isHidden = true;
    }

    // Display the word or an underscore if it is hidden
    public string Display()
    {
        return _isHidden ? "____" : _text;
    }
}

public class Reference
{
    private string _book;     
    private string _chapter;  
    private List<string> _verses; 

    // Constructor to parse the reference from a string
    public Reference(string reference)
    {
        var parts = reference.Split(' ');
        _book = parts[0];
        var chapterAndVerses = parts[1].Split(':');
        _chapter = chapterAndVerses[0];
        _verses = chapterAndVerses.Length > 1 ? chapterAndVerses[1].Split('-').ToList() : new List<string> { chapterAndVerses[1] };
    }

    // Override ToString to format the reference output
    public override string ToString()
    {
        return $"{_book} {_chapter}:{string.Join("-", _verses)}";
    }
}

public class Scripture
{
    private Reference _reference;  
    private List<Word> _words;    

    public Scripture(string reference, string text)
    {
        _reference = new Reference(reference);
        _words = text.Split(' ').Select(word => new Word(word)).ToList(); 
    }

    // Display the scripture reference and its words
    public string Display()
    {
        return $"{_reference}\n" + string.Join(" ", _words.Select(word => word.Display()));
    }

    // Hide a specified number of random words
    public void HideRandomWords(int count)
    {
        var random = new Random();
        var availableWords = _words.Where(w => !w.Display().Equals("____")).ToList(); 

        for (int i = 0; i < count && availableWords.Count > 0; i++)
        {
            var index = random.Next(availableWords.Count); 
            availableWords[index].Hide(); 
            availableWords.RemoveAt(index); 
        }
    }

    // Check if all words are hidden
    public bool AllWordsHidden()
    {
        return _words.All(w => w.Display().Equals("____")); 
    }
}

public class ScriptureLibrary
{
    private List<Scripture> _scriptures; 

    // Constructor to load scriptures from a file
    public ScriptureLibrary(string filePath)
    {
        _scriptures = new List<Scripture>();
        LoadScripturesFromFile(filePath); 
    }

    // Load scriptures from a specified text file
    private void LoadScripturesFromFile(string filePath)
    {
        foreach (var line in File.ReadLines(filePath)) 
        {
            var parts = line.Split('|'); 
            if (parts.Length == 2) 
            {
                _scriptures.Add(new Scripture(parts[0].Trim(), parts[1].Trim())); 
            }
        }
    }

    // Get a random scripture from the library
    public Scripture GetRandomScripture()
    {
        var random = new Random();
        return _scriptures[random.Next(_scriptures.Count)]; 
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Load scriptures from a file (ensure the file path is correct)
        var scriptureLibrary = new ScriptureLibrary("scriptures.txt");

        // Get a random scripture from the library for the user
        var scripture = scriptureLibrary.GetRandomScripture();

        while (!scripture.AllWordsHidden()) 
        {
            Console.Clear();
            Console.WriteLine(scripture.Display()); 
            Console.WriteLine("Press Enter to hide more words, or type 'quit' to exit.");

            var input = Console.ReadLine();
            if (input?.ToLower() == "quit") 
                break;

            scripture.HideRandomWords(2); 
        }
    }
}
