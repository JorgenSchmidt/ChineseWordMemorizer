using AppCore.Responses;

namespace AppModel.FileService
{
    /// <summary>
    /// Содержит методы для получения контента из файлов
    /// </summary>
    public class ContentGetters
    {
        /// <summary>
        /// Ожидает получить на вход путь до файла с его именем по шаблону "Первая директория\Вторая директория\..\[имя файла]" и расширение файла.
        /// Обратная косая черта в передаваемой строке не ставится.
        /// </summary>
        /// <param name="FilePath"> Относительный путь до файла (относительно исполняемого .exe файла программы). </param>
        /// <param name="Extention"> Расширение файла. </param>
        /// <returns> Возвращает объект, в случае успеха содержащий контент файла, значение поля IsSuccsess равным true. В обратном случае сообщение об ошибке и значение поля IsSuccsess равным false. </returns>
        public static SimpleData<string> GetContentFromFile(string FilePath, string Extention)
        {
            SimpleData<string> Result = new SimpleData<string>();
            Result.Data = "";
            Result.ErrorMessage = "";

            string ActuallyFilePath = Environment.CurrentDirectory + @"\" + FilePath + "." + Extention;

            var file = new FileInfo(ActuallyFilePath);
            if (!file.Exists || file.Length == 0)
            {
                Result.ErrorMessage = "Проверьте существует ли файл " + FilePath + " и не является ли его содержимое пустым.";
                Result.IsSucsess = false;
                return Result;
            }

            try
            {
                Result.Data = File.ReadAllText(ActuallyFilePath);
                Result.IsSucsess = true;
            }
            catch(Exception ex)
            {
                Result.ErrorMessage = ex.Message;
                Result.IsSucsess = false;
            }

            return Result;
        }
    }
}