using System;
using System.Collections.Generic;

public class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }
}

class Program
{
    static void Main()
    {
        Video video1 = new Video("Python Tutorial", "John Doe", 3600);
        Video video2 = new Video("C# Tutorial", "Jane Doe", 2700);
        Video video3 = new Video("JavaScript Tutorial", "Bob Smith", 4500);

        video1.AddComment(new Comment("Alice Johnson", "Great tutorial!"));
        video1.AddComment(new Comment("Mike Brown", "Very informative."));
        video1.AddComment(new Comment("Emily Davis", "Thanks for sharing."));

        video2.AddComment(new Comment("David Lee", "Excellent explanation."));
        video2.AddComment(new Comment("Sarah Taylor", "Well done!"));
        video2.AddComment(new Comment("Kevin White", "Helpful video."));

        video3.AddComment(new Comment("Olivia Martin", "Awesome content."));
        video3.AddComment(new Comment("William Harris", "Very useful."));
        video3.AddComment(new Comment("Amanda Hall", "Thanks for the tutorial."));

        List<Video> videos = new List<Video> { video1, video2, video3 };

       
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");

            Console.WriteLine("Comments:");
            foreach (var comment in video.Comments)
            {
                Console.WriteLine($"  {comment.CommenterName}: {comment.CommentText}");
            }

            Console.WriteLine();
        }
    }
}
