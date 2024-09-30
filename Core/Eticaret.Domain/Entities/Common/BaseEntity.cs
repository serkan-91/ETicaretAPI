using System.Text.Json.Serialization;

namespace EticaretAPI.Domain.Entities.Common
{
    public class BaseEntity
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("createdDate")]
        public DateTime? CreatedDate { get; set; }

        [JsonPropertyName("updatedDate")]
        public virtual DateTime? UpdatedDate { get; set; }
    }
}
