namespace AppCore.Responses
{
    public class SimpleData<T>
    {
        public bool IsSucsess { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}