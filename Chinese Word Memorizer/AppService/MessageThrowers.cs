using AppModel.DirectoryService;
using System;
using System.Windows;

namespace Chinese_Word_Memorizer.AppService
{
    /// <summary>
    /// Содержит методы, отображающие на экран сообщения различных типов
    /// </summary>
    public class MessageThrowers
    {
        /// <summary>
        /// Отображает сообщение об ошибке, возникшее в конкретном файле.
        /// </summary>
        public static void ShowErrorByFile (string Message, string FilePath)
        {
            var FullMessage = "Возникла ошибка в файле " + FilePath + ".\n\n"
                + Message;
            MessageBox.Show(FullMessage);
        }

        public static void ShowUserLists ()
        {
            // Запланировано изменение логики к 0.0.1.3 (открывается окно, где в асинхронном режиме проверяются все файлы на валидность)
            var filelist = DirectoryInfoGetters.GetValideFileList(Environment.CurrentDirectory + @"\WordsLists");
            MessageBox.Show(filelist);
        }
    }
}