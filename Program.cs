namespace EfCoreBug
{
    public class Post
    {
        public int Id { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<PostTag>? PostTags { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<PostTag>? PostTags { get; set; }
    }

    public class PostTag
    {
        //public int PostId { get; set; }
        //public int TagId { get; set; }
        public Post? Post { get; set; }
        public Tag? Tag { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
