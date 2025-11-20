namespace Osom.FluentRestult.API.Configurations
{
    public static class ErrorTypes
    {
        public static Dictionary<int, string> Documentation = new()
        {
            [400] = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            [401] = "https://tools.ietf.org/html/rfc7235#section-3.1",
            [403] = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            [404] = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            [500] = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            [422] = "https://tools.ietf.org/html/rfc4918#section-11.2",
            [409] = "https://tools.ietf.org/html/rfc4918#section-11.5",
        };
    }
}
