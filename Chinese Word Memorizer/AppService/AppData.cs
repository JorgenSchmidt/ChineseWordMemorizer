using AppCore.Entities;
using AppModel.DictionaryService;
using System.Collections.Generic;

namespace Chinese_Word_Memorizer.AppService
{
    /// <summary>
    /// Содержит основные данные приложения, в том числе основной словарь HSK.
    /// </summary>
    public class AppData
    {
        /// <summary>
        /// Разрешение на открытие окна. При возникновении сбоев при чтении словарей - состояние данной глобальной переменной остаётся в статусе false.
        /// </summary>
        public static bool WindowOpeningIsAllow = false;
        
        /// <summary>
        /// Основной словарь приложения, содержащий в себе элементы выбранного словаря HSK.
        /// </summary>
        public static List<DictionaryElement>? MainHSK_Dictionary = new List<DictionaryElement>();

        /// <summary>
        /// Словарь отображаемых элементов (окно просмотра словаря приложения)
        /// </summary>
        public static List<DictionaryChoisingElement>? MainViewedHSK_Dictionary = new List<DictionaryChoisingElement>();
    }
}