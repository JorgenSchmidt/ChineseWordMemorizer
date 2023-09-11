using Chinese_Word_Memorizer.AppService;

namespace Chinese_Word_Memorizer.ViewModels.HSK_ViewModels
{
    public class HSK_DictionaryDialogWindow : NotifyPropertyChanged
    {
        private static string GetDictionaryString ()
        {
            string Answer = "";
            foreach (var element in AppData.CurrentAppDictionary)
            {
                Answer += element.ChineseWord + "\t" + element.PinyinString + "\t" + element.RussianWord +  "\n\n";
            }
            return Answer;
        }

        public string viewDictionary = GetDictionaryString();
        public string ViewDictionary
        {
            get { return viewDictionary; }
            set
            {
                viewDictionary = value;
                CheckChanges();
            }
        }

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