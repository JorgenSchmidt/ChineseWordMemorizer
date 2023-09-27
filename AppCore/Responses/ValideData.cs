namespace AppCore.Responses
{
    /// <summary>
    /// Простой тип данных, используемый в методах валидации данных, содержит поле bool и поле для отображения ошибки, если таковая имела место быть.
    /// </summary>
    public class ValideData
    {
        public bool IsValide { get; set; }
        public string? Message { get; set; }
    }
}