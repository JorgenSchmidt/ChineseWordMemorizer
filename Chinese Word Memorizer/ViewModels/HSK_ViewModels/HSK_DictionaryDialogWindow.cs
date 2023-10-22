using AppCore.Entities;
using AppModel.DictionaryService;
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
                        if (SearchString == null || SearchString.Length == 0)
                        {
                            MessageBox.Show("Введите запрос в строку поиска.");
                            return;
                        }

                        var QueryResult = DictionaryGetter.GetSearchedElements(displayedDictionary, SearchString);

                        if (QueryResult.IsSucsess)
                        {
                            if (QueryResult.Data.Where(x=> x.IsViewed).Select(x => x).Count() == 0)
                            {
                                MessageBox.Show(QueryResult.Message + "\nПопробуйте выполнить другой запрос.");
                                return;
                            }
                            DisplayedDictionary = QueryResult.Data;
                        }

                        if (QueryResult.Data.Where(x => x.IsViewed).Select(x => x).Count() > 12)
                        {
                            MessageBox.Show(QueryResult.Message);
                        }
                    }    
                );
            }
        }

        public Command ResetFound
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        var loc = displayedDictionary;
                        DisplayedDictionary = DictionaryGetter.GetResetBySearchedViewedList(loc);
                    }
                );
            }
        }

        public TextAlignment currentVisibleMode = TextAlignment.Center;

        public TextAlignment CurrentVisibleMode
        {
            get
            {
                return currentVisibleMode;
            }
            set
            {
                currentVisibleMode = value;
                CheckChanges();
            }
        }

        public Command ChangeVisibleMode
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        if (CurrentVisibleMode == TextAlignment.Center)
                        {
                            CurrentVisibleMode = TextAlignment.Left;
                            return;
                        }
                        if (CurrentVisibleMode == TextAlignment.Left)
                        {
                            CurrentVisibleMode = TextAlignment.Center;
                            return;
                        }
                    }
                );
            }
        }
        #endregion

        #region Список

        // Локальная переменная, отвечающая за показ текущего словаря
        public List<DictionaryChoisingElement>? displayedDictionary = DictionaryGetter.GetViewedList(AppData.MainViewedHSK_Dictionary);
        public List<DictionaryChoisingElement>? DisplayedDictionary
        {
            get 
            {
                var loc = new List<DictionaryChoisingElement>();

                foreach (var element in displayedDictionary)
                {
                    if (element.IsViewed)
                    {
                        loc.Add(element);
                    }
                }

                return loc; 
            }
            set
            {
                displayedDictionary = value;
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
                        var choisedlist = displayedDictionary.Where(x => x.IsChoised).Select(x => x.RussianWord).ToList();
                        if (choisedlist.Count() == 0)
                        {
                            MessageBox.Show("Не выбрано ни одного элемента.");
                            return;
                        }
                        if (choisedlist.Count() < 5)
                        {
                            MessageBox.Show("Выберите по меньшей мере 5 элементов.");
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
                        MessageBox.Show("Файл " + FileName + ".txt успешно создан!");
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
                        var elements = displayedDictionary.Where(x => x.IsChoised == true).Select(x => x.RussianWord);

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
                            MessageBox.Show("Не выбрано ни одного элемента.");
                        }
                    }
                );
            }
        }

        public Command ShowAllUsersLists
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        MessageThrowers.ShowUserLists();
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
                        var loc = displayedDictionary;
                        if (loc.Where(x => x.IsChoised).Select(x => x).ToList().Count() == 0)
                        {
                            MessageBox.Show("Не выбрано ни одного элемента.");
                            return;
                        }
                        DisplayedDictionary = DictionaryGetter.GetResetByChoisedViewedList(loc);
                    }
                );
            }
        }

        public bool russianSortingButtonIsEnabled = true;
        public bool RussianSortingButtonIsEnabled
        {
            get { return russianSortingButtonIsEnabled; }
            set
            {
                russianSortingButtonIsEnabled = value;
                CheckChanges();
            }
        }
        public Command SortingByRussian
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        var loc = displayedDictionary;
                        DisplayedDictionary = DictionaryGetter.GetSortedByRussianViewedList(loc);
                        RussianSortingButtonIsEnabled = false;
                        PinyinSortingButtonIsEnabled = true;
                    }
                );
            }
        }

        public bool pinyinSortingButtonIsEnabled = false;
        public bool PinyinSortingButtonIsEnabled
        {
            get { return pinyinSortingButtonIsEnabled;}
            set
            {
                pinyinSortingButtonIsEnabled = value;
                CheckChanges();
            }
        }
        public Command SortingByPinYin
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        var loc = displayedDictionary;
                        DisplayedDictionary = DictionaryGetter.GetSortedByPinYinViewedList(loc);
                        RussianSortingButtonIsEnabled = true;
                        PinyinSortingButtonIsEnabled = false;
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
                        string info = "Генеральная инструкция: \n"
                                    + "Данное окно в основном служит для просмотра имеющейся лексики на данный уровень HSK и составления пользовательских "
                                    + "словарей. Для этого необходимо отметить галочками все необходимые элементы, "
                                    + "ввести нужное имя для файла и нажать кнопку подтверждения.\n"
                                    + "Все словари сохраняются по относительному пути в директории \"WordsLists\".\n\n"
                                    + "1. Интерфейс данного окна разделён на три сегмента: \n" 
                                    + "I. Строка поиска с соответствующими инструментами;\n"
                                    + "II. Отображаемый список;\n"
                                    + "III. Панель инструментов словаря.\n\n"
                                    + "2a. Строка поиска поддерживает ввод слов по русским и китайским словам, а так же по транскрипции пиньинь\n"
                                    + "Транскрипция пиньинь может быть введена как в общепринятой форме, так и в упрощённой (ài --> a4i)\n" 
                                    + "В более поздних версиях будет выпущена возможность более удобной записи 拼音 (hālóu --> ha1lou2)\n"
                                    + "2б. При выполнении запроса на поиск лексических единиц можно не беспокоиться за утерю данных о выбранных ранее словах. "
                                    + "Чтобы проверить какие слова выбраны, можно нажать на кнопку \"Показать выбранные элементы\".\n"
                                    + "2в. Запрос можно сбросить, чтобы отобразить все остальные элементы. Также можно сразу ввести новый запрос.\n\n"
                                    + "3а. На основе выбранных слов можно создать файл пользовательского словаря.\n"
                                    + "3б. Так же чтобы исключить конфликт имён, а так же просмотреть выбранный ранее способ классификации пользовательских "
                                    + "словарей, можно нажать на кнопку \"Показать пользовательские словари\".\n "
                                    + "3в. Список выбранных слов можно сбросить до изначального состояния "
                                    + "(те которые предназначаются для добавления в пользовательский словарь).";

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
                        AppData.MainViewedHSK_Dictionary.Clear();
                        WindowsObjects.HSK_DictionaryDialogWindow.Close();
                        WindowsObjects.HSK_DictionaryDialogWindow = null;
                    }
                );
            }
        }
        #endregion

    }
}