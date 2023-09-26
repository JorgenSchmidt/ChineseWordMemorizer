using AppCore.Entities;
using Chinese_Word_Memorizer.AppService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Chinese_Word_Memorizer.ViewModels.HSK_ViewModels
{
    /// <summary>
    /// Модель визуального представления для окна показа пользователю словаря
    /// </summary>
    public class HSK_DictionaryDialogWindow : NotifyPropertyChanged
    {
        #region Строка поиска и отображение словаря
        public string? searchString;
        public string? SearchString
        {
            get { return searchString; }
            set
            {
                searchString = value;
                CheckChanges();
            }
        }
        public Command FindWords
        {
            get
            {
                return new Command(
                    obj =>
                    {

                    }    
                );
            }
        }

        public Command ResetFinded
        {
            get
            {
                return new Command(
                    obj =>
                    {

                    }
                );
            }
        }

        private VisibleModes CurrentVisMode = VisibleModes.Center;

        public Command ChangeVisibleMode
        {
            get
            {
                return new Command(
                    obj =>
                    {

                    }
                );
            }
        }
        #endregion

        #region Список

        // Локальная переменная, отвечающая за показ текущего словаря
        private static List<DictionaryChoisingElement>? LocalDictionary = GetInitialList();
        public List<DictionaryChoisingElement>? viewedDictionary = GetViewedList();
        public List<DictionaryChoisingElement>? ViewedDictionary
        {
            get { return viewedDictionary; }
            set
            {
                viewedDictionary = value;
                CheckChanges();
            }
        }
        #endregion

        #region Инструменты словаря
        public string? fileName;
        public string? FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
            }
        }

        public Command CreateFile
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        if (FileName == null || FileName.Length == 0)
                        {
                            MessageBox.Show("Введите имя файла.");
                            return;
                        }
                        string locFileName = Environment.CurrentDirectory + @"\WordsLists\" + FileName + ".txt";
                        if (File.Exists(locFileName))
                        {
                            MessageBox.Show("Такой файл уже существует в директории WordsList.");
                            return;
                        }
                        var choisedlist = ViewedDictionary.Where(x => x.IsChoised).Select(x => x.RussianWord).ToList();
                        if (choisedlist.Count() == 0)
                        {
                            MessageBox.Show("Не выбрано ни одного элемента.");
                            return;
                        }
                        string content = "";
                        foreach (var element in choisedlist)
                        {
                            content += element + "\n";
                        }
                        content = content.Remove(content.Length - 1);
                        using (StreamWriter stream = new StreamWriter(locFileName))
                        {
                            stream.Write(content);
                            stream.Close();
                        }
                    }
                );
            }
        }
        public Command ShowChoisedElements
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        var elements = ViewedDictionary.Where(x => x.IsChoised == true).Select(x => x.RussianWord);

                        if (elements.Count() != 0)
                        {
                            var content = "Выбранные слова: \n\n";
                            var counter = 0;
                            foreach (var element in elements)
                            {
                                counter++;
                                content += counter + ". " + element + "\n";
                            }
                            MessageBox.Show(content);
                        }
                        else
                        {
                            MessageBox.Show("Не выбрано ни одного элемента");
                        }
                    }
                );
            }
        }

        public Command ResetChoised
        {
            get
            {
                return new Command(
                    obj =>
                    {

                    }
                );
            }
        }
        #endregion

        #region Другое
        // Скрипт закрытия окна просмотра словаря
        public Command GetInfo
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        string info = "";

                        MessageBox.Show(info);
                    }    
                );
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
        #endregion

        #region Скрипты и методы
        private static List<DictionaryChoisingElement>? GetInitialList ()
        {
            var Answer = new List<DictionaryChoisingElement>();
            foreach (var element in AppData.CurrentAppDictionary)
            {
                Answer.Add(
                    new DictionaryChoisingElement ()
                    {
                        RussianWord = element.RussianWord,
                        ChineseWord = element.ChineseWord,
                        PinyinString = element.PinyinString,
                        IsChoised = false,
                        IsViewed = true
                    }    
                );
            }

            return Answer;
        }
        private static List<DictionaryChoisingElement> GetViewedList()
        {
            var Answer = LocalDictionary.Where(x => x.IsViewed).Select(x => x).ToList();
            return Answer;
        }
        #endregion
    }

    public enum VisibleModes
    {
        Center,
        Left
    }
}