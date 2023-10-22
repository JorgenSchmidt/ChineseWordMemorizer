using AppModel.DictionaryService;
using AppModel.FileService;
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
        /// Расширения словарей обязательно должны быть формата .txt
        /// </summary>
        public static void Start(string fileName)
        {
            // Получение содержимого файла
            var Content = ContentGetters.GetContentFromFile(fileName, "txt");
            if (!Content.IsSucsess)
            {
                MessageThrowers.ShowErrorByFile(Content.ErrorMessage, fileName);
                AppData.WindowOpeningIsAllow = false;
                return;
            }

            // Формирование списка данных по определённому шаблону, если операция не была произведена успешно, выдаёт сообщение об ошибке
            var Input = DictionaryGetter.GetMainDictionary(Content.Data);
            if (!Input.IsSucsess)
            {
                MessageThrowers.ShowErrorByFile(Input.Message, fileName);
                AppData.WindowOpeningIsAllow = false;
                return;
            }

            // Проверка содержимого составленного списка на соответствие некоторым требованиям
            var Valide = DictionaryValidator.DictIsCorrect(Input.Data);
            if (!Valide.IsValide)
            {
                MessageThrowers.ShowErrorByFile(Valide.Message, fileName);
                AppData.WindowOpeningIsAllow = false;
                return;
            }

            // Передача полученного списка в глобальную память программы и получение разрешения на открытие окна
            AppData.WindowOpeningIsAllow = true;
            AppData.MainHSK_Dictionary = Input.Data;
        }
    }
}