namespace SMPT.Entities
{
    public class CustomResponse<T> where T : class
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Value { get; set; }
    }
}
