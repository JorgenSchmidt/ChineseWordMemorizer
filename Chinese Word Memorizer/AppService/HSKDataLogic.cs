using AppModel.DictionaryService;
using AppModel.ValidateService;
using System.IO;
using System.Windows;

namespace Chinese_Word_Memorizer.AppService
{
    /// <summary>
    /// Класс содержит скрипт формирования и проверки словаря HSK из соответствующего файла
    /// </summary>
    public class HSKDataLogic
    {
        /// <summary>
        /// Запуск скрипта формирования и проверки словаря HSK из соответствующего файла, а так же его добавление в глобальную переменную программы, 
        /// отвечающей за хранение словаря.
        /// </summary>
        public static void Start(string fileName)
        {
            var file = new FileInfo(fileName);
            // Проверка файла на существование, а так же содержится ли в файле что-либо
            if (!file.Exists || file.Length == 0)
            {
                MessageBox.Show("Проверьте существует ли файл " + fileName + " в директории \"Dictionares\" и не является ли его содержимое пустым.");
                AppData.WindowOpeningIsAllow = false;
                return;
            }

            // Формирование списка данных по определённому шаблону, если операция не была произведена успешно, выдаёт сообщение об ошибке
            var Input = DictionaryGetter.GetDictionaryFromFile(fileName);
            if (!Input.IsSucsess)
            {
                MessageBox.Show(Input.ErrorMessage);
                AppData.WindowOpeningIsAllow = false;
                return;
            }

            // Проверка содержимого составленного списка на соответствие некоторым требованиям
            var Valide = DictionaryValidator.DictIsCorrect(Input.Data);
            if (!Valide.IsValide)
            {
                MessageBox.Show(Valide.ErrorMessage);
                AppData.WindowOpeningIsAllow = false;
                return;
            }

            // Передача полученного списка в глобальную память программы и получение разрешения на открытие окна
            AppData.WindowOpeningIsAllow = true;
            AppData.CurrentAppDictionary = Input.Data;
        }
    }
}