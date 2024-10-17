using System;
using System.Collections.Generic;
using System.Linq;

public class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public string Display()
    {
        return IsHidden ? "____" : Text;
    }
}

public class Reference
{
    public string Book { get; private set; }
    public string Chapter { get; private set; }
    public List<string> Verses { get; private set; }

    public Reference(string reference)
    {
        var parts = reference.Split(' ');
        Book = parts[0];
        var chapterAndVerses = parts[1].Split(':');
        Chapter = chapterAndVerses[0];
        Verses = chapterAndVerses.Length > 1 ? chapterAndVerses[1].Split('-').ToList() : new List<string> { chapterAndVerses[1] };
    }

    public override string ToString()
    {
        return $"{Book} {Chapter}:{string.Join("-", Verses)}";
    }
}

public class Scripture
{
    public Reference Reference { get; private set; }
    private List<Word> Words { get; set; }

    public Scripture(string reference, string text)
    {
        Reference = new Reference(reference);
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public string Display()
    {
        return $"{Reference}\n" + string.Join(" ", Words.Select(word => word.Display()));
    }

    public void HideRandomWords(int count)
    {
        var random = new Random();
        var availableWords = Words.Where(w => !w.IsHidden).ToList();

        for (int i = 0; i < count && availableWords.Count > 0; i++)
        {
            var index = random.Next(availableWords.Count);
            availableWords[index].Hide();
            availableWords.RemoveAt(index);
        }
    }

    public bool AllWordsHidden()
    {
        return Words.All(w => w.IsHidden);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var scripture = new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.Display());
            Console.WriteLine("Press Enter to hide more words, or type 'quit' to exit.");

            var input = Console.ReadLine();
            if (input?.ToLower() == "quit")
                break;

            scripture.HideRandomWords(2); // Hide 2 random words each time
        }
    }
}
