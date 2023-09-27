using AppCore.Responses;
using AppModel.FileService;
using Chinese_Word_Memorizer.AppService;
using System;
using System.Windows;

namespace Chinese_Word_Memorizer.ViewModels
{
    /// <summary>
    /// Модель визуального представления основного окна (происходит выбор )
    /// </summary>
    public class AppWindowViewModel
    {
        public Command StartHSK1Window
        {
            get
            {
                return new Command(
                    
                    async (obj) =>
                    {
                        // Открытие файла со словарём и загрузка из него данных в память программы
                        //SimpleData<string> content = ContentGetters.GetContentFromFile(@"Dictionaries\hsk1_dict", "txt");

                        string fileName = Environment.CurrentDirectory + @"\Dictionaries\hsk1_dict.txt";
                        HSKDataLogic.Start(fileName);

                        if (AppData.WindowOpeningIsAllow)
                        {
                            WindowsObjects.HSK_DialogWindow = new();
                            if (WindowsObjects.HSK_DialogWindow.ShowDialog() == true)
                            {
                                WindowsObjects.HSK_DialogWindow.Show();
                            }
                        }

                    }
                );
            }
        }
        public Command StartHSK2Window
        {
            get
            {
                return new Command(

                    async (obj) =>
                    {
                        // Открытие файла со словарём и загрузка из него данных в память программы
                        string fileName = Environment.CurrentDirectory + @"\Dictionaries\hsk2_dict.txt";
                        HSKDataLogic.Start(fileName);

                        if (AppData.WindowOpeningIsAllow)
                        {
                            WindowsObjects.HSK_DialogWindow = new();
                            if (WindowsObjects.HSK_DialogWindow.ShowDialog() == true)
                            {
                                WindowsObjects.HSK_DialogWindow.Show();
                            }
                        }
                    }
                );
            }
        }
        public Command StartHSK3Window
        {
            get
            {
                return new Command(

                    async (obj) =>
                    {
                        // Открытие файла со словарём и загрузка из него данных в память программы
                        string fileName = Environment.CurrentDirectory + @"\Dictionaries\hsk3_dict.txt";
                        HSKDataLogic.Start(fileName);

                        if (AppData.WindowOpeningIsAllow)
                        {
                            WindowsObjects.HSK_DialogWindow = new();
                            if (WindowsObjects.HSK_DialogWindow.ShowDialog() == true)
                            {
                                WindowsObjects.HSK_DialogWindow.Show();
                            }
                        }
                    }
                );
            }
        }
        public Command StartHSK4Window
        {
            get
            {
                return new Command(

                    async (obj) =>
                    {
                        // Открытие файла со словарём и загрузка из него данных в память программы
                        string fileName = Environment.CurrentDirectory + @"\Dictionaries\hsk4_dict.txt";
                        HSKDataLogic.Start(fileName);

                        if (AppData.WindowOpeningIsAllow)
                        {
                            WindowsObjects.HSK_DialogWindow = new();
                            if (WindowsObjects.HSK_DialogWindow.ShowDialog() == true)
                            {
                                WindowsObjects.HSK_DialogWindow.Show();
                            }
                        }
                    }
                );
            }
        }
        public Command StartHSK5Window
        {
            get
            {
                return new Command(

                    async (obj) =>
                    {
                        // Открытие файла со словарём и загрузка из него данных в память программы
                        string fileName = Environment.CurrentDirectory + @"\Dictionaries\hsk5_dict.txt";
                        HSKDataLogic.Start(fileName);

                        if (AppData.WindowOpeningIsAllow)
                        {
                            WindowsObjects.HSK_DialogWindow = new();
                            if (WindowsObjects.HSK_DialogWindow.ShowDialog() == true)
                            {
                                WindowsObjects.HSK_DialogWindow.Show();
                            }
                        }
                    }
                );
            }
        }
        public Command StartHSK6Window
        {
            get
            {
                return new Command(

                    async (obj) =>
                    {
                        // Открытие файла со словарём и загрузка из него данных в память программы
                        string fileName = Environment.CurrentDirectory + @"\Dictionaries\hsk6_dict.txt";
                        HSKDataLogic.Start(fileName);

                        if (AppData.WindowOpeningIsAllow)
                        {
                            WindowsObjects.HSK_DialogWindow = new();
                            if (WindowsObjects.HSK_DialogWindow.ShowDialog() == true)
                            {
                                WindowsObjects.HSK_DialogWindow.Show();
                            }
                        }
                    }
                );
            }
        }
        public Command StartTraitsWindow
        {
            get
            {
                return new Command(

                    async (obj) =>
                    {
                        MessageBox.Show("Не реализовано");
                    }
                );
            }
        }
        public Command StartKeysWindow
        {
            get
            {
                return new Command(

                    async (obj) =>
                    {
                        MessageBox.Show("Не реализовано");
                    }
                );
            }
        }
    }
}