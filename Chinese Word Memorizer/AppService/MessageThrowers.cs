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
    }
}