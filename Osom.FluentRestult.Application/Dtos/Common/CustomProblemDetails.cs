using System.Text.Json.Serialization;

namespace Osom.FluentRestult.Application.Dtos.Common
{
    public class CustomProblemDetails
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public int? Status { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public string Type { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, List<string>> Errors { get; set; }

        public CustomProblemDetails(
            string title,
            int? status,
            string detail,
            string instance,
            string type
        )
        {
            Title = title;
            Status = status;
            Detail = detail;
            Instance = instance;
            Type = type;
        }

        public CustomProblemDetails() { }
    }
}
