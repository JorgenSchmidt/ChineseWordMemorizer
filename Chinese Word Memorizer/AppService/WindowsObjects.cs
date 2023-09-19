using Chinese_Word_Memorizer.Views.HSK_Windows;

namespace Chinese_Word_Memorizer.AppService
{
    /// <summary>
    /// Содержит объекты окон, используемых во время работы программы.
    /// </summary>
    public class WindowsObjects
    {
        /// <summary>
        /// Объект, ассоциированный с окном проверки пользователем знания лексики HSK
        /// </summary>
        public static HSK_DialogWindow? HSK_DialogWindow;

        /// <summary>
        /// Объект, ассоциированный с окном показа пользователю словаря в удобном для чтения виде
        /// </summary>
        public static HSK_DictionaryDialogWindow? HSK_DictionaryDialogWindow;
    }
}