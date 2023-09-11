namespace AppCore.Responses
{
    /// <summary>
    /// Объект, содержащий данные (хэшсет), а также информацию об успешности выполнения операции и полученном в ходе работы исключении, если возник сбой
    /// </summary>
    public class OutputHashSetData<T>
    {
        public bool IsSucsess { get; set; }
        public HashSet<T>? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}