namespace Instacart_BusinessLogin
{
    public class ResponseStatus<T>
    {
        public bool Status { get; set; }
        public T Data { get; set; }
        public IEnumerable<T>? Enumerabledata { get; set; }
        public IQueryable<T>? Queryabledata { get; set; }
        public IList<T>? Listdata { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}