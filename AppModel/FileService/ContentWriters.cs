using AppCore.Responses;

namespace AppModel.FileService
{
    public class ContentWriters
    {
        /// <summary>
        /// Ожидает получить на вход
        /// контент на запись в файл,
        /// путь до файла с его именем по шаблону "Первая директория\Вторая директория\..\[имя файла]",
        /// расширение файла.
        /// </summary>
        /// <param name="Content"> Контент, записываемый в файл. </param>
        /// <param name="FilePath"> Относительный путь до файла (относительно исполняемого .exe файла программы). </param>
        /// <param name="Extention"> Расширение файла. </param>
        /// <returns> Сообщение об успешности выполнения записи данных в файл. </returns>
        public static ValideData WriteContentToFile (string Content, string FilePath, string Extension)
        {
            var Message = new ValideData();
            Message.Message = "";

            string ActuallyFilePath = Environment.CurrentDirectory + @"\" + FilePath + "." + Extension;

            try
            {
                using (StreamWriter stream = new StreamWriter(ActuallyFilePath))
                {
                    stream.Write(Content);
                    stream.Close();
                }
                Message.IsValide = true;
                Message.Message = "Успешно.";
            }
            catch (Exception e)
            {
                Message.IsValide = false;
                Message.Message += e.ToString();
            }

            return Message;
        }
    }
}