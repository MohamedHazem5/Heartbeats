using System.Text.Json.Serialization;

namespace Heartbeats.Models
{
    public class Medication
    {
        public int SplVersion { get; set; }
        public string PublishedDate { get; set; }
        public string Title { get; set; }
        public string SetId { get; set; }
    }

}