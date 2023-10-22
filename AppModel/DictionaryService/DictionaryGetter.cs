using AppCore.Entities;
using AppCore.Responses;
using AppModel.ConverterService;
using AppModel.ValidateService;

namespace AppModel.DictionaryService
{
    /// <summary>
    /// Содержит методы формирования основных словарей: пользовательского словаря, словаря HSK и словаря тестирования.
    /// </summary>
    public class DictionaryGetter
    {
        #region Методы данных
        /// <summary>
        /// Метод, ожидающий получить на вход путь до существующего файла и возращающий на выход список элементов словаря HSK, 
        /// а так же информацию об успешности выполнения метода, успешно ли он был выполнен и сообщение об ошибке если таковая имела место быть.
        /// </summary>
        public static OutputListData<DictionaryElement> GetMainDictionary(string Content)
        {
            OutputListData<DictionaryElement> Result = new OutputListData<DictionaryElement>();
            Result.Data = new List<DictionaryElement>();
            Result.Message = "";
            Result.IsSucsess = true;
            var counter = 0;

            // Удаление ненужных символов, а так же разбиение входной строки по переносам строки
            var lines = Content.Replace("\r", "").Split('\n');
            try
            {
                // Основной алгоритм парсинга массива строк (массив был получен внутри метода и забит в переменную lines)
                // основное требование, чтоб было ровно 3 элемента, разделённых табуляцией в каждом из элементов массива.
                // Потом каждый из сформированных объектов будет дополнительно проверен парсером
                // Алгоритм дополнительно считает количество итераций, чтобы в случае возникновения ошибки 
                // отобразить пользователю в какой конкретно строке возникла ошибка
                foreach (var line in lines)
                {
                    counter++;
                    var currentLine = line.Split('\t');
                    if (currentLine.Length >= 4)
                    {
                        throw new Exception("В строке #" + counter + "содержалось 4 или более слов, разделённых табуляцией. Чтение HSK словаря остановлено.");
                    }
                    Result.Data.Add(new DictionaryElement
                    {
                        RussianWord = currentLine[0],
                        ChineseWord = currentLine[1],
                        PinyinString = currentLine[2]
                    });
                }
                Result.IsSucsess = true;
            }
            catch (Exception e)
            {
                Result.IsSucsess = false;
                Result.Message = "Возникла ошибка на этапе чтения словаря в строке №" + counter + "."
                    + "\n\nИсключение: " + e.ToString() + ".";
            }

            return Result;
        }

        /// <summary>
        /// Метод, ожидающий получить на вход путь до существующего файла и возращающий на выход список пользовательских элементов, 
        /// а так же информацию об успешности выполнения метода, успешно ли он был выполнен и сообщение об ошибке если таковая имела место быть.
        /// </summary>
        public static OutputHashSetData<string> GetUsersWordsList(string Content)
        {
            OutputHashSetData<string> Result = new OutputHashSetData<string>();
            Result.Data = new HashSet<string>();
            Result.ErrorMessage = "";
            Result.IsSucsess = true;

            var counter = 0;

            // Удаление ненужных символов, а так же разбиение входной строки по переносам строки
            var lines = Content.Replace("\r", "").Split('\n');

            try
            {
                // Алгоритм парсинга строк. Главное требование к нему - отсутствие табуляций в любой из строк, т.к. ожидается,
                // что в пользовательском словаре будет в каждой стркое всего по одному элементу
                // Алгоритм дополнительно считает количество итераций, чтобы в случае возникновения ошибки 
                // отобразить пользователю в какой конкретно строке возникла ошибка
                foreach (var line in lines)
                {
                    counter++;
                    if (line.Contains('\t'))
                    {
                        throw new Exception("Встречена табуляция в строке #" + counter + ". Чтение пользовательского словаря остановлено.");
                    }
                    Result.Data.Add(line);
                }
                Result.IsSucsess = true;
            }
            catch (Exception e)
            {
                Result.IsSucsess = false;
                Result.ErrorMessage = "Возникла ошибка на этапе чтения словаря в строке №" + counter + "."
                    + "\n\nИсключение: " + e.ToString() + ".";
            }

            return Result;
        }

        /// <summary>
        /// Метод, которому на вход подаётся непосредственно список элементов пользовательского словаря и список элементов словаря HSK,
        /// и дающий на выход словарь тестирования, сформированный на основе пользовательского словаря и соответствующих ему данных из словаря HSK.
        /// </summary>
        public static OutputListData<DictionaryElement> GetElementsByUserList(HashSet<string> InputUserList, List<DictionaryElement> InputTargetList)
        {
            OutputListData<DictionaryElement> Result = new OutputListData<DictionaryElement>();
            Result.Data = new List<DictionaryElement>();
            Result.Message = "";
            Result.IsSucsess = true;

            var counter = 0;

            try
            {
                // Алгоритм составления словаря тестирования, в дополнительной обработке парсером не нуждается
                // Алгоритм дополнительно считает количество итераций, чтобы в случае возникновения ошибки 
                // отобразить пользователю в какой конкретно строке возникла ошибка
                // Ошибка, предусмотренная алгоритмом - в пользовательском словаре не обнаружено вхождения элемента из основного

                // Перебор элементов пользовательского словаря
                foreach (var currentString in InputUserList)
                {
                    // Счётчик строк
                    counter++;
                    // Вентиль, отображающий обнаружено ли слово, в случае если нет, выдаётся сообщение об ошибке (см. код после цикла foreach)
                    bool IsFind = false;
                    
                    // Перебор элементов генерального словаря
                    foreach (var element in InputTargetList)
                    {
                        // Если прямое вхождение строки из пользовательского словаря обнаружено, 
                        if (element.RussianWord.Equals(currentString))
                        {
                            Result.Data.Add(element);
                            IsFind = true;
                            break;
                        }
                    }

                    // Если в конкретной строке пользовательского словаря не было обнаружено 
                    // ни одного прямого вхождения из генерального - выдаётся ошибка,
                    // но цикл продолжается (цель - отобразить полный список некорректных вхождений)
                    if (!IsFind)
                    {
                        Result.IsSucsess = false;
                        Result.Message += "Ошибка в строке #" + counter + ". Обнаружен элемент пользовательского списка, которого нет в словаре.\n";
                    }
                }
            }
            catch (Exception e)
            {
                Result.IsSucsess = false;
                Result.Message = "Возникла неизвестная ошибка на этапе составления списка словарных единиц для тестирования. \n"
                    + "Исключение:\n"
                    + e.ToString();
            }

            return Result;
        }
        #endregion

        #region Методы элементов интерфейса
        /// <summary>
        /// Получение списка элементов на основе входного, на основе расширенного класса DictionaryChoisingElement, 
        /// наследуемого от класса DictionaryElement. Объекты данного типа имеют два дополнительных "вентиля" по сравнению с классом DictionaryElement,
        /// один отвечает за то, выбран ли элемент пользователем, другой за то, может ли он быть отображён в интерфейсе.
        /// </summary>
        public static List<DictionaryChoisingElement>? GetInitialViewedList(List<DictionaryElement> InputList)
        {
            var Result = new List<DictionaryChoisingElement>();

            foreach (var element in InputList)
            {
                Result.Add(
                    new DictionaryChoisingElement()
                    {
                        RussianWord = element.RussianWord,
                        ChineseWord = element.ChineseWord,
                        PinyinString = element.PinyinString,
                        IsChoised = false,
                        IsViewed = true
                    }
                );
            }

            return Result;
        }

        /// <summary>
        /// Метод-запрос, получающий на выход список элементов, которые могут быть отображены
        /// </summary>
        public static List<DictionaryChoisingElement>? GetViewedList(List<DictionaryChoisingElement> InputList)
        {
            var Result = new List<DictionaryChoisingElement>();

            Result = InputList.Where(x => x.IsViewed).Select(x => x).ToList();

            return Result;
        }

        /// <summary>
        /// Метод "сбрасывает" входной список по полю IsChoised, т.е. все элементы, которые выбрал пользователь,
        /// в выходном списке будут помечены как невыбранные (поле IsChoised = false).
        /// </summary>
        public static List<DictionaryChoisingElement>? GetResetByChoisedViewedList(List<DictionaryChoisingElement> InputList)
        {
            var Result = new List<DictionaryChoisingElement>();

            foreach (var element in InputList)
            {
                Result.Add(
                    new DictionaryChoisingElement()
                    {
                        RussianWord = element.RussianWord,
                        ChineseWord = element.ChineseWord,
                        PinyinString = element.PinyinString,
                        IsChoised = false,
                        IsViewed = element.IsViewed
                    }    
                );
            }

            return Result;
        }

        /// <summary>
        /// Метод, который делает все элементы входного списка отображаемыми в интерфейсе пользователя
        /// </summary>
        public static List<DictionaryChoisingElement> GetResetBySearchedViewedList(List<DictionaryChoisingElement> InputList)
        {
            var Result = new List<DictionaryChoisingElement>();

            foreach (var element in InputList)
            {
                Result.Add(
                    new DictionaryChoisingElement()
                    {
                        RussianWord = element.RussianWord,
                        ChineseWord = element.ChineseWord,
                        PinyinString = element.PinyinString,
                        IsChoised = element.IsChoised,
                        IsViewed = true
                    }
                );
            }

            return Result;
        }

        /// <summary>
        /// Сортирует все элементы генерального "графического" списка по русскому алфавиту
        /// </summary>
        public static List<DictionaryChoisingElement>? GetSortedByRussianViewedList(List<DictionaryChoisingElement> InputList)
        {
            var Result = new List<DictionaryChoisingElement>();
            foreach (var element in InputList)
            {
                Result.Add(
                    new DictionaryChoisingElement()
                    {
                        RussianWord = element.RussianWord,
                        ChineseWord = element.ChineseWord,
                        PinyinString = element.PinyinString,
                        IsChoised = element.IsChoised,
                        IsViewed = element.IsViewed
                    }
                );
            }
            Result.Sort(
                    delegate (DictionaryChoisingElement First, DictionaryChoisingElement Second)
                    {
                        return First.RussianWord.CompareTo(Second.RussianWord);
                    }
            );

            return Result;
        }

        /// <summary>
        /// Сортирует все элементы генерального "графического" списка по транскрипции пиньинь
        /// </summary>
        public static List<DictionaryChoisingElement>? GetSortedByPinYinViewedList(List<DictionaryChoisingElement> InputList)
        {
            var Result = new List<DictionaryChoisingElement>();
            foreach (var element in InputList)
            {
                Result.Add(
                    new DictionaryChoisingElement()
                    {
                        RussianWord = element.RussianWord,
                        ChineseWord = element.ChineseWord,
                        PinyinString = element.PinyinString,
                        IsChoised = element.IsChoised,
                        IsViewed = element.IsViewed
                    }
                );
            }
            Result.Sort(
                    delegate (DictionaryChoisingElement First, DictionaryChoisingElement Second)
                    {
                        return First.PinyinString.CompareTo(Second.PinyinString);
                    }
            );

            return Result;
        }

        /// <summary>
        /// Возвращает список найденных элементов. В данном случае происходит не отбор конкретных элементов, а воздействие
        /// на поле IsViewed класса DictionaryChoisingElement для избежания потери информации во время сёрфинга пользователем
        /// по словарю (и как следствие информации о уже выбранных раннее элементах).
        /// Метод сначала определяет чем строка является, китайским иероглифом, русским словом или транскрипцией пиньинь,
        /// в зависимости от результатов данной проверки выбирает нужный алгоритм составления списка.
        /// </summary>
        public static OutputListData<DictionaryChoisingElement>? GetSearchedElements(List<DictionaryChoisingElement> InputList, string UserQuery)
        {
            OutputListData<DictionaryChoisingElement> Result = new OutputListData<DictionaryChoisingElement>();
            Result.Data = new List<DictionaryChoisingElement>();
            Result.Message = "";

            if (InputList == null || InputList.Count() == 0)
            {
                Result.IsSucsess = false;
                Result.Message = "Входной лист оказался пустым (внутренняя ошибка программы).";

                return Result;
            }

            // Если входная строка является русским словом - список формируется методом GetRussianQuery
            if (DictionaryValidator.IsCyrrilicWord(UserQuery))
            {
                Result.Data = GetRussianQuery(InputList, UserQuery);
                Result.IsSucsess = true;
                Result.Message = "Найдено " + Result.Data.Where(x => x.IsViewed).Select(x => x).Count() + " элементов.";

                return Result;
            }

            // Если входная строка является китайским иероглифом - список формируется методом GetChineseQuery
            if (DictionaryValidator.IsChineseHieroglyph(UserQuery))
            {
                Result.Data = GetChineseQuery(InputList, UserQuery);
                Result.IsSucsess = true;
                Result.Message = "Найдено " + Result.Data.Where(x => x.IsViewed).Select(x => x).Count() + " элементов.";

                return Result;
            }

            // Если входная строка является транскрипцией пиньинь - список формируется методом GetPinyinQuery
            if (DictionaryValidator.IsPinyinString(UserQuery))
            {
                Result.Data = GetPinyinQuery(InputList, UserQuery);
                Result.IsSucsess = true;
                Result.Message = "Найдено " + Result.Data.Where(x => x.IsViewed).Select(x => x).Count() + " элементов.";

                return Result;
            }

            // Если входная строка является упрощённой транскрипцией пиньинь - создаётся новая переменная, преобразующая
            // входную строку в стандартную запись, после составление самого списка по новой переменной список формируется методом GetPinyinQuery
            if (DictionaryValidator.IsSimplifiedPinyin(UserQuery))
            {
                var modifiedQuery = PinyinConverters.SimplifiedPinyinToStandart(UserQuery);
                Result.Data = GetPinyinQuery(InputList, modifiedQuery);
                Result.IsSucsess = true;
                Result.Message = "Найдено " + Result.Data.Where(x => x.IsViewed).Select(x => x).Count() + " элементов.";

                return Result;
            }

            Result.IsSucsess = false;
            Result.Message = "Строка не является ни русским словом, ни китайским иероглифом, ни транскрипцией пиньинь.";
            return Result;
        }

        private static List<DictionaryChoisingElement> GetRussianQuery(List<DictionaryChoisingElement> InputList, string UserQuery)
        {
            var Result = new List<DictionaryChoisingElement>();

            foreach (var element in InputList)
            {
                if (element.RussianWord.Contains(UserQuery))
                {
                    Result.Add(
                        new DictionaryChoisingElement(
                            element.ChineseWord, element.RussianWord, element.PinyinString, element.IsChoised, true
                        )
                    );
                }
                else
                {
                    Result.Add(
                        new DictionaryChoisingElement(
                            element.ChineseWord, element.RussianWord, element.PinyinString, element.IsChoised, false
                        )
                    );
                }
            }

            return Result;
        }
        private static List<DictionaryChoisingElement> GetChineseQuery(List<DictionaryChoisingElement> InputList, string UserQuery)
        {
            var Result = new List<DictionaryChoisingElement>();

            foreach(var element in InputList)
            {
                if (element.ChineseWord.Contains(UserQuery))
                {
                    Result.Add(
                        new DictionaryChoisingElement(
                            element.ChineseWord, element.RussianWord, element.PinyinString, element.IsChoised, true    
                        )    
                    );
                }
                else
                {
                    Result.Add(
                        new DictionaryChoisingElement(
                            element.ChineseWord, element.RussianWord, element.PinyinString, element.IsChoised, false
                        )
                    );
                }
            }

            return Result;
        }
        private static List<DictionaryChoisingElement> GetPinyinQuery(List<DictionaryChoisingElement> InputList, string UserQuery)
        {
            var Result = new List<DictionaryChoisingElement>();

            foreach (var element in InputList)
            {
                if (element.PinyinString.Contains(UserQuery))
                {
                    Result.Add(
                        new DictionaryChoisingElement(
                            element.ChineseWord, element.RussianWord, element.PinyinString, element.IsChoised, true
                        )
                    );
                }
                else
                {
                    Result.Add(
                        new DictionaryChoisingElement(
                            element.ChineseWord, element.RussianWord, element.PinyinString, element.IsChoised, false
                        )
                    );
                }
            }

            return Result;
        }
        #endregion
    }
}