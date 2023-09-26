using AppCore.Entities;
using AppCore.Responses;
using AppModel.ChekerService;
using AppModel.ConverterService;
using AppModel.DictionaryService;
using AppModel.DirectoryService;
using AppModel.ValidateService;
using Chinese_Word_Memorizer.AppService;
using System;
using System.IO;
using System.Windows;

namespace Chinese_Word_Memorizer.ViewModels.HSK_ViewModels
{
    /// <summary>
    /// Модель визуального представления для окна тестирования пользователя на знание лексики к выбранному уровню HSK.
    /// </summary>
    public class HSK_DialogWindowViewModel : NotifyPropertyChanged
    {
        #region Локальные переменные (окна)
        // Локальная переменная, которая содержит в себе словарь тестирования
        private OutputListData<DictionaryElement> CurrentWindowWordsListObject = new OutputListData<DictionaryElement>();
        // Локальная переменная, отвечающая за переключение режимов тестирования
        private SwitchModes CurrentSwitchMode;
        // Локальная переменная, отвечающая за номер текущего элемента, по которому пользователю нужно дать ответ
        private int CurrentWordNumber;
        #endregion

        #region Вспомогательные методы
        // Отвечает за показ контента в элементе, который ассоциирован со словом, которому необходимо дать соответствующий перевод/транскрипцию 
        // в зависимости от включённого пользователем режима тестирования
        private void GetContentForTestLabels (string RussianWord, string ChineseWord, string Pinyin)
        {
            try
            {
                switch (CurrentSwitchMode)
                {
                    case SwitchModes.RussianMode:
                        MainTestWord = RussianWord;
                    break;

                    case SwitchModes.ChineseMode:

                        var changedChineseWord = StringGetterWithDicts.GetChangeStringByDefaultList(ChineseWord, RussianWord, AppData.CurrentAppDictionary);

                        MainTestWord = changedChineseWord;
                    break;

                    case SwitchModes.PinyinMode:
                        MainTestWord = Pinyin;
                    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Скрипт, переключающий состояние элементов окна в дефолтный режим
        private void FinishTesting ()
        {
            TestingWasStarted = false;
            InitiatingButtonContent = "Начать!";
            MainTestWord = "";
            LeftTestWordLabel = "Китайское слово:";
            LeftTestWordTextIsEnabled = false;
            LeftTestWordText = "";
            RightTestWordLabel = "Пиньинь";
            RightTestWordText = "";
            ConfirmButtonIsEnabled = false;
            RightTestWordTextIsEnabled = false;
            RussianModeButtonIsEnabled = false;
            ChineseModeButtonIsEnabled = true;
            PinyinModeButtonIsEnabled = true;
            ShowDictIsEnabled = true;
            CurrentWindowWordsListObject.Data.Clear();
            CurrentWindowWordsListObject.IsSucsess = false;
            CurrentWindowWordsListObject.ErrorMessage = "";
        }
        #endregion

        #region Блок ввода имени файла
        // Имя файла (задаётся относительнный путь)
        public string? fileName;
        public string? FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                CheckChanges();
            }
        }
        #endregion

        #region Блок тестирования
        // Ассоциирован с label элементом, содержит слово, по которому нужно дать ответ
        public string? mainTestWord; // вмещается 26 символов
        public string? MainTestWord
        {
            get { return mainTestWord; }
            set
            {
                mainTestWord = value;
                CheckChanges();
            }
        }

        // Ассоциирован с первым тестировочным label элементом, отображает на экране что нужно ввести в соответствующий textbox
        public string? leftTestWordLabel = "Китайское слово:";
        public string? LeftTestWordLabel
        {
            get { return leftTestWordLabel; }
            set
            {
                leftTestWordLabel = value;
                CheckChanges();
            }
        }

        // Ассоциирован с первым тестировочным textbox'ом, в который необходимо ввести ответ
        public string? leftTestWordText;
        public string? LeftTestWordText
        {
            get { return leftTestWordText; }
            set
            {
                leftTestWordText = value;
                CheckChanges();
            }
        }

        // Ассоциирован с параметром IsEnabled первого textbox'a, отвечает за то, будет ли активирован textbox
        public bool leftTestWordTextIsEnabled = false;
        public bool LeftTestWordTextIsEnabled
        {
            get { return leftTestWordTextIsEnabled; }
            set
            {
                leftTestWordTextIsEnabled = value;
                CheckChanges();
            }
        }

        // Ассоциирован со вторым тестировочным label элементом, отображает на экране что нужно ввести в соответствующий textbox
        public string? rightTestWordLabel = "Пиньинь:";
        public string? RightTestWordLabel
        {
            get { return rightTestWordLabel; }
            set
            {
                rightTestWordLabel = value;
                CheckChanges();
            }
        }

        // Ассоциирован со вторым тестировочным textbox'ом, в который необходимо ввести ответ
        public string? rightTestWordText;
        public string? RightTestWordText
        {
            get { return rightTestWordText; }
            set
            {
                rightTestWordText = value;
                CheckChanges();
            }
        }

        // Ассоциирован с параметром IsEnabled второго textbox'a, отвечает за то, будет ли активирован textbox
        public bool rightTestWordTextIsEnabled = false;
        public bool RightTestWordTextIsEnabled
        {
            get { return rightTestWordTextIsEnabled; }
            set
            {
                rightTestWordTextIsEnabled = value;
                CheckChanges();
            }
        }

        // Отвечает за то, будет ли активирована кнопка ответа
        public bool confirmButtonIsEnabled = false;
        public bool ConfirmButtonIsEnabled
        {
            get { return confirmButtonIsEnabled; }
            set
            {
                confirmButtonIsEnabled = value;
                CheckChanges();
            }
        }

        // Скрипт, выполняющийся при зажатии кнопки ответа
        public Command CheckAnswer
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        // Инициализация специальных переменных
                        var leftword = "";
                        var rightword = "";

                        // Проверка на то, какой режим тестирования включен, выполнение соответствующего скрипта
                        switch (CurrentSwitchMode)
                        {

                            // Режим тестирования по ассоциации с русским словом
                            case SwitchModes.RussianMode:
                                // Присваиваем специальной "левой" переменной значение соответствующего словарю слова
                                leftword = CurrentWindowWordsListObject.Data[CurrentWordNumber].ChineseWord;
                                // Присваиваем специальной "правой" переменной значение соответствующего словарю слова
                                rightword = CurrentWindowWordsListObject.Data[CurrentWordNumber].PinyinString;

                                // Сравниваем полученные слова с введённым пользователем контентом
                                if (
                                           (leftword.Equals(LeftTestWordText) && rightword.Equals(RightTestWordText))
                                        || (leftword.Equals(LeftTestWordText) && (PinyinConverters.StandartPinyinToLocalPinyin(rightword)).Equals(RightTestWordText))
                                   )
                                {
                                    MessageBox.Show("Верно!");
                                }
                                else
                                {
                                    MessageBox.Show("Ошибка. Правильный ответ:\n"
                                        + "Китайское слово: " + leftword + "\n"
                                        + "拼音: " + rightword
                                    );
                                }
                            break;

                            // Режим тестирования по ассоциации с китайским словом
                            case SwitchModes.ChineseMode:
                                
                                // Присваиваем специальной "левой" переменной значение соответствующего словарю слова
                                leftword = CurrentWindowWordsListObject.Data[CurrentWordNumber].RussianWord;
                                
                                // Присваиваем специальной "правой" переменной значение соответствующего словарю слова
                                rightword = CurrentWindowWordsListObject.Data[CurrentWordNumber].PinyinString;

                                // Сравниваем полученные слова с введённым пользователем контентом
                                if (
                                           (RussianCheckService.CheckAnswer(LeftTestWordText, leftword) && rightword.Equals(RightTestWordText))
                                        || (RussianCheckService.CheckAnswer(LeftTestWordText, leftword) && (PinyinConverters.StandartPinyinToLocalPinyin(rightword)).Equals(RightTestWordText))
                                   )
                                {
                                    MessageBox.Show("Верно!");
                                }
                                else
                                {
                                    MessageBox.Show("Ошибка. Правильный ответ:\n"
                                        + "Русское слово: " + leftword + "\n"
                                        + "拼音: " + rightword
                                    );
                                }
                            break;

                            // Режим тестирования по ассоциации с транскрипицей 拼音
                            case SwitchModes.PinyinMode:
                                
                                // Присваиваем специальной "левой" переменной значение соответствующего словарю слова
                                leftword = CurrentWindowWordsListObject.Data[CurrentWordNumber].ChineseWord;

                                // Присваиваем специальной "правой" переменной значение соответствующего словарю слова
                                rightword = CurrentWindowWordsListObject.Data[CurrentWordNumber].RussianWord;

                                // Сравниваем полученные слова с введённым пользователем контентом
                                if (leftword.Equals(LeftTestWordText) && RussianCheckService.CheckAnswer(RightTestWordText, rightword))
                                {
                                    MessageBox.Show("Верно!");
                                }
                                else
                                {
                                    MessageBox.Show("Ошибка. Правильный ответ:\n"
                                        + "Китайское слово слово: " + leftword + "\n"
                                        + "Русское слово: " + rightword
                                    );
                                }
                            break;
                        }

                        // Обнуление контента локальных переменных, ассоциированных с соответствующим textbox'ом
                        LeftTestWordText = "";
                        RightTestWordText = "";
                        // Удаление элемента, по которому был дан ответ пользователем
                        CurrentWindowWordsListObject.Data.RemoveAt(CurrentWordNumber);

                        // Проверка на то, остались ли ещё элементы в словаре тестирования
                        // Если элементы остались, переменная, отвечающая за номер текущего элемента тестирования перезадаётся
                        // В противном случае выдаётся сообщение об окончании тестирования и запускается скрипт перехода текущего окна к дефолтному состояниюы
                        if (CurrentWindowWordsListObject.Data.Count != 0)
                        {
                            Random randObj = new Random();
                            CurrentWordNumber = randObj.Next(0, CurrentWindowWordsListObject.Data.Count - 1);
                            GetContentForTestLabels(
                                CurrentWindowWordsListObject.Data[CurrentWordNumber].RussianWord,
                                CurrentWindowWordsListObject.Data[CurrentWordNumber].ChineseWord,
                                CurrentWindowWordsListObject.Data[CurrentWordNumber].PinyinString
                            );
                        }
                        else
                        {
                            MessageBox.Show("Тестирование окончено!");
                            FinishTesting();
                        }
                    }    
                );
            }
        }
        #endregion

        #region Блок переключения режимов тестирования
        // Отвечает за активацию ассоциированной с переменной кнопки
        public bool russianModeButtonIsEnabled = false;
        public bool RussianModeButtonIsEnabled
        {
            get { return russianModeButtonIsEnabled; }
            set
            {
                russianModeButtonIsEnabled = value;
                CheckChanges();
            }
        }
        // Запускает скрипт переключения по нажатии на ассоциированную кнопку
        public Command ToRussianMode
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        RussianModeButtonIsEnabled = false;
                        ChineseModeButtonIsEnabled = true;
                        PinyinModeButtonIsEnabled = true;
                        LeftTestWordLabel = "Китайское слово:";
                        RightTestWordLabel = "Пиньинь:";
                        CurrentSwitchMode = SwitchModes.RussianMode;
                    }
                ); 
            }
        }

        // Отвечает за активацию ассоциированной с переменной кнопки
        public bool chineseModeButtonIsEnabled = true;
        public bool ChineseModeButtonIsEnabled
        {
            get { return chineseModeButtonIsEnabled; }
            set
            {
                chineseModeButtonIsEnabled = value;
                CheckChanges();
            }
        }
        // Запускает скрипт переключения по нажатии на ассоциированную кнопку
        public Command ToChineseMode
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        RussianModeButtonIsEnabled = true;
                        ChineseModeButtonIsEnabled = false;
                        PinyinModeButtonIsEnabled = true;
                        LeftTestWordLabel = "Русское слово:";
                        RightTestWordLabel = "Пиньинь:";
                        CurrentSwitchMode = SwitchModes.ChineseMode;
                    }
                );
            }
        }

        // Отвечает за активацию ассоциированной с переменной кнопки
        public bool pinyinModeButtonIsEnabled = true;
        public bool PinyinModeButtonIsEnabled
        {
            get { return pinyinModeButtonIsEnabled; }
            set
            {
                pinyinModeButtonIsEnabled = value;
                CheckChanges();
            }
        }
        // Запускает скрипт переключения по нажатии на ассоциированную кнопку
        public Command ToPinyinMode
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        RussianModeButtonIsEnabled = true;
                        ChineseModeButtonIsEnabled = true;
                        PinyinModeButtonIsEnabled = false;
                        LeftTestWordLabel = "Китайское слово:";
                        RightTestWordLabel = "Русское слово:";
                        CurrentSwitchMode = SwitchModes.PinyinMode;
                    }
                );
            }
        }
        #endregion

        #region Блок основных операций
        // Отвечает за активацию кнопки, по нажатию которой открывается словарь
        public bool showDictIsEnabled = true;
        public bool ShowDictIsEnabled
        {
            get { return showDictIsEnabled; }
            set
            {
                showDictIsEnabled = value;
                CheckChanges();
            }
        }
        // Скрипт запуска окна просмотра элементов словаря HSK
        public Command ShowDictionary
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        WindowsObjects.HSK_DictionaryDialogWindow = new();
                        if (WindowsObjects.HSK_DictionaryDialogWindow.ShowDialog() == true)
                        {
                            WindowsObjects.HSK_DictionaryDialogWindow.Show();
                        }
                    }
                );
            }
        }

        // Скрипт показа messagebox'а с инструкцией по проведению тестирования
        public Command GetInfo
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        string Message = "Инструкция:\n"
                        + "1а. Файл с пользовательскими словами должен находится в папке WordsLists и иметь расширение .txt\n"
                        + "1б. Также список слов должен состоять из русских слов, разделённых переносом строки, обязательно имеющихся в выбранном словаре\n\n"
                        + "2а. Для начала тестирования необходимо сначала выбрать нужный режим тестирования (о чём будет рассказано далее), "
                        + "затем ввести имя файла и нажать на кнопку Начать. В случае если приложение выдаёт ошибку - необходимо проверить "
                        + "существует файл с указанным именем и если такой существует - проверить файл на соответствие с требованиями из пункта \"1б\".\n"
                        + "2б. После начала тестирования кнопки выбора режима тестирования будут отключены!!!\n"
                        + "2в. Для как можно более продуктивной работы необходимо на усмотрение пользователя разбить словарь на части и давать файлам имена по"
                        + "следующему принципу (пример будет приближённый, название файлов остаётся на усмотрение пользователя): \n"
                        + "<Номер HSK> - <Номер части>, пример \"hsk1-1\".\n\n"
                        + "3а. Возможно включать различные режимы тестирования: знание русского, китайского перевода, знание 拼音\n"
                        + "3б. При любом из режимов, необходимо ввести в поля элементы, соответствующие выведенному в зелёной рамке слову\\транскрипции\n"
                        + "3в. В случае если нет доступа к особым символам, использующихся в транскрипции Пиньинь, можно вводить слова по следующему принципу:\n"
                        + "после каждой гласной, имеющей тон, необходимо ввести после самой гласной её номер, например zhèngzài --> zhe4ngza4i\n\n"
                        + "4а. До начала тестирования приложением предусматривается возможность просмотра выбранного пользователем словарём, после запуска тестирования, "
                        + "кнопка просмотра словаря будет отключена.\n"
                        + "4б. В окне просмотра словаря можно так же составить список слов для изучения (с обязательным указанием имени выходного файла).";

                        MessageBox.Show(Message);
                    }
                );
            }
        }

        // Позволяет вывести в messagebox'е список файлов из папки WordsLists
        public Command ShowFileList
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        var filelist = DirectoryInfoGetters.GetValideFileList(Environment.CurrentDirectory + @"\WordsLists");
                        MessageBox.Show(filelist);
                    }
                );
            }
        }

        // Переменная, отвечающая за то, началось ли тестирование
        private bool TestingWasStarted = false;

        // Контент кнопки начала тестирования
        public string? initiatingButtonContent = "Начать!";
        public string? InitiatingButtonContent
        {
            get { return initiatingButtonContent; }
            set
            {
                initiatingButtonContent= value;
                CheckChanges();
            }
        }

        // Свойство, ассоциирование с кнопкой, запускающей скрипт начала/окончания тестирования
        public Command InitiatingButton
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        // Если тестирование уже было начато (если нажать на кнопку - тестирование остановится). 
                        // Тестирование приостанавливается, интерфейс приходит к изначальному состоянию вне зависимости от выбранного режима
                        if (TestingWasStarted)
                        {
                            MessageBox.Show("Тестирование окончено досрочно.");
                            FinishTesting();
                        }
                        // Если тестирование не было начато (если нажать на кнопку - тестирование начнётся). 
                        else
                        {
                            if (FileName == null || FileName.Length == 0)
                            {
                                MessageBox.Show("Введите имя файла.");
                                return;
                            }

                            var FileDirectory = Environment.CurrentDirectory + @"\WordsLists\" + FileName + ".txt";
                            var File = new FileInfo(FileDirectory);
                            if (!File.Exists || File.Length == 0)
                            {
                                MessageBox.Show("Файла с указанным именем не существует.");
                                return;
                            }

                            var UserFileOutputData = DictionaryGetter.GetUserListFromFile(FileDirectory);
                            if (!UserFileOutputData.IsSucsess)
                            {
                                MessageBox.Show(UserFileOutputData.ErrorMessage);
                                return; 
                            }

                            var ValideUserListObject = DictionaryValidator.UserListIsCorrect(UserFileOutputData.Data);
                            if (!ValideUserListObject.IsValide)
                            {
                                MessageBox.Show(ValideUserListObject.ErrorMessage);
                                return;
                            }
                            
                            CurrentWindowWordsListObject = DictionaryGetter.GetElementsByUserList(UserFileOutputData.Data, AppData.CurrentAppDictionary);
                            if (!CurrentWindowWordsListObject.IsSucsess)
                            {
                                MessageBox.Show(CurrentWindowWordsListObject.ErrorMessage);
                                return;
                            }

                            TestingWasStarted = true;
                            InitiatingButtonContent = "Завершить\nдосрочно";
                            LeftTestWordTextIsEnabled = true;
                            RightTestWordTextIsEnabled = true;
                            ConfirmButtonIsEnabled = true;
                            RussianModeButtonIsEnabled = false;
                            ChineseModeButtonIsEnabled = false;
                            PinyinModeButtonIsEnabled = false;
                            ShowDictIsEnabled = false;

                            Random randObj = new Random();
                            CurrentWordNumber = randObj.Next(0, CurrentWindowWordsListObject.Data.Count - 1);
                            GetContentForTestLabels(
                                CurrentWindowWordsListObject.Data[CurrentWordNumber].RussianWord,
                                CurrentWindowWordsListObject.Data[CurrentWordNumber].ChineseWord,
                                CurrentWindowWordsListObject.Data[CurrentWordNumber].PinyinString
                            );

                        }
                    }
                );
            }
        }

        // Скрипт закрытия окна тестирования, дополнительно происходит обнуление глобальной переменной, содержащей весь HSK словарь
        public Command CloseWindow
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        AppData.CurrentAppDictionary.Clear();
                        AppData.WindowOpeningIsAllow = false;
                        WindowsObjects.HSK_DialogWindow.Close();
                        WindowsObjects.HSK_DialogWindow = null;
                    }
                );
            }
        }
        #endregion
    }

    /// <summary>
    /// Список всех режимов тестирования
    /// </summary>
    internal enum SwitchModes
    {
        RussianMode,
        ChineseMode,
        PinyinMode
    }
}