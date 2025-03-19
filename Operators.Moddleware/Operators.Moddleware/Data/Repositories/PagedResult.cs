namespace Operators.Moddleware.Data.Repositories {

    public class PagedResult<T> {
        public List<T> Entities { get; set; } = new();
        public int Count { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
