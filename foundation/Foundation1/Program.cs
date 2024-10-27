using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Create videos
        Video video1 = new Video("Understanding C#", "Jane Doe", 300);
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charlie", "I learned a lot."));
        videos.Add(video1);

        Video video2 = new Video("Advanced C# Techniques", "John Smith", 450);
        video2.AddComment(new Comment("Dave", "Loved the deep dive!"));
        video2.AddComment(new Comment("Eve", "I wish there were more examples."));
        videos.Add(video2);

        Video video3 = new Video("C# Best Practices", "Mike Johnson", 600);
        video3.AddComment(new Comment("Frank", "Useful tips!"));
        video3.AddComment(new Comment("Grace", "I will implement these."));
        video3.AddComment(new Comment("Hank", "Very insightful."));
        videos.Add(video3);

        // Display video details
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");
            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }
}
