using System.Text.Json.Serialization;

namespace Heartbeats.Models
{
    public class Metadata
    {
        public string DbPublishedDate { get; set; }
        public int ElementsPerPage { get; set; }
        public string CurrentUrl { get; set; }
        public string NextPageUrl { get; set; }
        public int TotalElements { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }

        public string PreviousPage { get; set; }
        public string PreviousPageUrl { get; set; }

        [JsonPropertyName("next_page")]
        public int NextPage { get; set; }
    }
}