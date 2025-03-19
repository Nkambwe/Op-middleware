using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class PagedResponse<T> : SystemResponse<T> {

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }

}
