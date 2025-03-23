using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {
    public class Meta { 

        [JsonPropertyName("count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("pageNumber")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }
    }

}
