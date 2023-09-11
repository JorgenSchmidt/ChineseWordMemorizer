using AppCore.Entities;
using AppCore.Responses;
using AppModel.DictionaryService;
using AppModel.ValidateService;
using Chinese_Word_Memorizer.AppService;
using System;
using System.IO;
using System.Windows;

namespace Chinese_Word_Memorizer.ViewModels.HSK_ViewModels
{
    public class HSK_DialogWindowViewModel : NotifyPropertyChanged
    {
        #region Локальные переменные (окна)
        private OutputListData<DictionaryElement> CurrentWindowWordsListObject = new OutputListData<DictionaryElement>();
        private SwitchModes CurrentSwitchMode;
        private int CurrentWordNumber;
        #endregion

        #region Вспомогательные методы
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
                        MainTestWord = ChineseWord;
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
        public Command CheckAnswer
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        var leftword = "";
                        var rightword = "";
                        switch (CurrentSwitchMode)
                        {
                            case SwitchModes.RussianMode:
                                leftword = CurrentWindowWordsListObject.Data[CurrentWordNumber].ChineseWord;
                                rightword = CurrentWindowWordsListObject.Data[CurrentWordNumber].PinyinString;
                                if (leftword.Equals(LeftTestWordText) && rightword.Equals(RightTestWordText))
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

                            case SwitchModes.ChineseMode:
                                leftword = CurrentWindowWordsListObject.Data[CurrentWordNumber].RussianWord;
                                rightword = CurrentWindowWordsListObject.Data[CurrentWordNumber].PinyinString;
                                if (leftword.Equals(LeftTestWordText) && rightword.Equals(RightTestWordText))
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

                            case SwitchModes.PinyinMode: 
                                leftword = CurrentWindowWordsListObject.Data[CurrentWordNumber].ChineseWord;
                                rightword = CurrentWindowWordsListObject.Data[CurrentWordNumber].RussianWord;
                                if (leftword.Equals(LeftTestWordText) && rightword.Equals(RightTestWordText))
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
                        LeftTestWordText = "";
                        RightTestWordText = "";
                        CurrentWindowWordsListObject.Data.RemoveAt(CurrentWordNumber);

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
                        + "3б. При любом из режимов, необходимо ввести в соответствующие поля элементы, соответствующие выведенному в красной рамке слову\\транскрипции\n\n"
                        + "4. До начала тестирования приложением предусматривается возможность просмотра выбранного пользователем словарём, после запуска тестирования, "
                        + "кнопка просмотра словаря будет отключена.";

                        MessageBox.Show(Message);
                    }
                );
            }
        }

        private bool TestingWasStarted = false;

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

        public Command CloseWindow
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        AppData.CurrentAppDictionary = null;
                        WindowsObjects.HSK_DialogWindow.Close();
                        WindowsObjects.HSK_DialogWindow = null;
                    }
                );
            }
        }
        #endregion
    }

    internal enum SwitchModes
    {
        RussianMode,
        ChineseMode,
        PinyinMode
    }
}