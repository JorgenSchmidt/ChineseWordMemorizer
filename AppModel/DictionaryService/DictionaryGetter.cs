using AppCore.Entities;
using AppCore.Responses;

namespace AppModel.DictionaryService
{
    /// <summary>
    /// Содержит методы формирования основных словарей: пользовательского словаря, словаря HSK и словаря тестирования.
    /// </summary>
    public class DictionaryGetter
    {
        /// <summary>
        /// Метод, ожидающий получить на вход путь до существующего файла и возращающий на выход список элементов словаря HSK, 
        /// а так же информацию об успешности выполнения метода, успешно ли он был выполнен и сообщение об ошибке если таковая имела место быть.
        /// </summary>
        public static OutputListData<DictionaryElement> GetDictionaryFromFile (string FilePath)
        {
            OutputListData<DictionaryElement> answer = new OutputListData<DictionaryElement>();
            answer.Data = new List<DictionaryElement> ();
            answer.ErrorMessage = "";
            answer.IsSucsess = true;
            var counter = 0;

            string fileContent = File.ReadAllText (FilePath);
            var lines = fileContent.Replace("\r", "").Split('\n');
            try
            {
                foreach (var line in lines)
                {
                    counter++;
                    var curLine = line.Split('\t');
                    if (curLine.Length >= 4)
                    {
                        throw new Exception("В строке #" + counter + "содержалось 4 или более слов, разделённых табуляцией. Чтение HSK словаря остановлено.");
                    }
                    answer.Data.Add(new DictionaryElement
                    {
                        RussianWord = curLine[0],
                        ChineseWord = curLine[1],
                        PinyinString = curLine[2]
                    });
                }
                answer.IsSucsess = true; 
            }
            catch (Exception e)
            {
                answer.IsSucsess = false;
                answer.ErrorMessage = "Возникла ошибка на этапе чтения словаря в строке №" + counter + "."
                    + "\n\nИсключение: " + e.ToString() + "."
                    + "\n\nПроверьте структуру целевого файла (" + FilePath + ").";
            }

            return answer;
        }

        /// <summary>
        /// Метод, ожидающий получить на вход путь до существующего файла и возращающий на выход список пользовательских элементов, 
        /// а так же информацию об успешности выполнения метода, успешно ли он был выполнен и сообщение об ошибке если таковая имела место быть.
        /// </summary>
        public static OutputHashSetData<string> GetUserListFromFile(string FilePath)
        {
            OutputHashSetData<string> answer = new OutputHashSetData<string>();
            answer.Data = new HashSet<string>();   
            answer.ErrorMessage = "";
            answer.IsSucsess = true;
            var counter = 0;

            string fileContent = File.ReadAllText(FilePath);
            var lines = fileContent.Replace("\r", "").Split('\n');

            try
            {
                foreach (var line in lines)
                {
                    counter++;
                    if (line.Contains('\t'))
                    {
                        throw new Exception("Встречена табуляция в строке #" + counter + ". Чтение пользовательского словаря остановлено.");
                    }
                    answer.Data.Add(line);
                }
                answer.IsSucsess = true;
            }
            catch (Exception e)
            {
                answer.IsSucsess = false;
                answer.ErrorMessage = "Возникла ошибка на этапе чтения словаря в строке №" + counter + "."
                    + "\n\nИсключение: " + e.ToString() + "."
                    + "\n\nПроверьте структуру целевого файла (" + FilePath + ").";
            }

            return answer;
        }

        /// <summary>
        /// Метод, которому на вход подаётся непосредственно список элементов пользовательского словаря и список элементов словаря HSK,
        /// и дающий на выход словарь тестирования, сформированный на основе пользовательского словаря и соответствующих ему данных из словаря HSK.
        /// </summary>
        public static OutputListData<DictionaryElement> GetElementsByUserList(HashSet<string> InputUserList, List<DictionaryElement> InputTargetList)
        {
            OutputListData<DictionaryElement> answer = new OutputListData<DictionaryElement>();
            answer.Data = new List<DictionaryElement>();
            answer.ErrorMessage = "";
            answer.IsSucsess = true;
            var counter = 0;

            try
            {
                foreach (var curStr in InputUserList)
                {
                    counter++;
                    bool IsFind = false;
                    foreach (var curEl in InputTargetList)
                    {
                        if (curEl.RussianWord.Contains(curStr))
                        {
                            answer.Data.Add(curEl);
                            IsFind = true;
                            break;
                        }
                    }
                    if (!IsFind)
                    {
                        answer.IsSucsess = false;
                        answer.ErrorMessage = "Ошибка в строке #" + counter + ". Обнаружен элемент пользовательского списка, которого нет в словаре.";
                    }
                }
            }
            catch (Exception e)
            {
                answer.IsSucsess = false;
                answer.ErrorMessage = "Возникла неизвестная ошибка на этапе составления списка словарных единиц для тестирования. \n" 
                    + "Исключение:\n" 
                    + e.ToString();
            }

            return answer;
        } 
    }
}