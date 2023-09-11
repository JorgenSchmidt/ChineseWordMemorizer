namespace AppCore.Responses
{
    /// <summary>
    /// Объект, содержащий данные (список), а также информацию об успешности выполнения операции и полученном в ходе работы исключении, если возник сбой
    /// </summary>
    public class OutputListData<T>
    {
        public bool IsSucsess { get; set; }
        public List<T>? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }

}