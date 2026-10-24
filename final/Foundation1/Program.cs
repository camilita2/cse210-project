using System;
using System.Collections.Generic;

namespace VideoTracker
{
    // A single comment left on a video.
    public class Comment
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public Comment(string name, string text)
        {
            Name = name;
            Text = text;
        }
    }

    // A video with a title, author, length (seconds), and a list of comments.
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Length { get; set; }
        public List<Comment> Comments { get; } = new List<Comment>();

        public Video(string title, string author, int length)
        {
            Title = title;
            Author = author;
            Length = length;
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public int GetCommentCount()
        {
            return Comments.Count;
        }
    }

    public class Program
    {
        static void DisplayVideo(Video video)
        {
            Console.WriteLine($"Title:    {video.Title}");
            Console.WriteLine($"Author:   {video.Author}");
            Console.WriteLine($"Length:   {video.Length} seconds");
            Console.WriteLine($"Comments: {video.GetCommentCount()}");
            Console.WriteLine(new string('-', 40));

            foreach (Comment comment in video.Comments)
            {
                Console.WriteLine($"  {comment.Name}: {comment.Text}");
            }

            Console.WriteLine(new string('=', 40));
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            List<Video> videos = new List<Video>();

            // Video 1
            Video v1 = new Video("Unboxing the New Trailblazer Backpack", "GearGuru", 482);
            v1.AddComment(new Comment("hikerhannah", "Love the side pockets!"));
            v1.AddComment(new Comment("tom_travels", "Is it waterproof?"));
            v1.AddComment(new Comment("GearGuru", "@tom_travels Yes, fully sealed seams."));
            videos.Add(v1);

            // Video 2
            Video v2 = new Video("Best Energy Drinks Taste Test", "SipSquad", 735);
            v2.AddComment(new Comment("fizzfan99", "Number 3 was my favorite."));
            v2.AddComment(new Comment("caffeine_carl", "You should try the citrus flavor."));
            v2.AddComment(new Comment("mia_m", "Great comparison, thanks!"));
            v2.AddComment(new Comment("DrinkDaily", "Subscribed for more reviews."));
            videos.Add(v2);

            // Video 3
            Video v3 = new Video("Day in the Life: Software Intern", "CodeWithKai", 910);
            v3.AddComment(new Comment("newbie_dev", "This is so helpful, thank you."));
            v3.AddComment(new Comment("laptoplover", "What laptop are you using?"));
            v3.AddComment(new Comment("CodeWithKai", "A 14-inch ultrabook, link in the description."));
            videos.Add(v3);

            foreach (Video video in videos)
            {
                DisplayVideo(video);
            }
        }
    }
}