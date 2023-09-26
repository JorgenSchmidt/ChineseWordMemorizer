namespace AppModel.DirectoryService
{
    /// <summary>
    /// Содержит методы для получения информации из директорий
    /// </summary>
    public class DirectoryInfoGetters
    {
        /// <summary>
        /// Возвращает список пригодных для открытия в приложении файлов (пользовательский список)
        /// </summary>
        public static string GetValideFileList(string DirectoryPath) // Запланировано изменение логики к 0.0.1.3 (открывается окно, где в асинхронном режиме проверяются все файлы на валидность)
        {
            string Message = "";

            if (!Directory.Exists(DirectoryPath))
            {
                return "Ошибка. Папки WordsLists не существует в директории с приложением.";
            }

            try
            {
                var fileList = Directory.GetFiles(DirectoryPath);
                foreach (var file in fileList)
                {
                    var sp = file.Split('\\');
                    Message += sp[sp.Length - 1] + "\n";
                }
            }
            catch (Exception e) 
            {
                Message = "Возникла непредвиденная ошибка: \n" + e.Message;
            }

            return Message;
        }
    }
}