namespace BAITZ_BLOG_API
{
    public class Key
    {
        public static string Secret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "123as4d56asd45ads465a4s5d6s54dass69aa5";

        
    }
}
