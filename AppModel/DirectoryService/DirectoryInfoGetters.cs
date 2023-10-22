namespace AppModel.DirectoryService
{
    /// <summary>
    /// Содержит методы для получения информации из директорий
    /// </summary>
    public class DirectoryInfoGetters
    {
        /// <summary>
        /// Возвращает список пригодных для открытия в приложении файлов (пользовательский список) (запланировано изменение логики к 0.0.1.3)
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
                if (fileList.Length == 0)
                {
                    return "Ошибка. Папка с пользовательскими списками оказалась пуста.";
                }
                foreach (var file in fileList)
                {
                    var splits = file.Split('\\');
                    Message += splits [splits.Length - 1] + "\n";
                }
                Message = "Список пользовательских словарей (файлы): \n\n" + Message;
            }
            catch (Exception e) 
            {
                Message = "Возникла непредвиденная ошибка: \n" + e.Message;
            }

            return Message;
        }
    }
}