using AppCore.Entities;
using Chinese_Word_Memorizer.AppService;
using System.Collections.Generic;

namespace Chinese_Word_Memorizer.ViewModels.HSK_ViewModels
{
    /// <summary>
    /// Модель визуального представления для окна показа пользователю словаря
    /// </summary>
    public class HSK_DictionaryDialogWindow : NotifyPropertyChanged
    {
        // Локальная переменная, отвечающая за показ текущего словаря
        public List<DictionaryElement>? viewDictionary = AppData.CurrentAppDictionary;
        public List<DictionaryElement> ViewDictionary
        {
            get { return viewDictionary; }
            set
            {
                viewDictionary = value;
                CheckChanges();
            }
        }

        // Скрипт закрытия окна просмотра словаря
        public Command Close
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        WindowsObjects.HSK_DictionaryDialogWindow.Close();
                        WindowsObjects.HSK_DictionaryDialogWindow = null;
                    }
                );
            }
        }

    }
}