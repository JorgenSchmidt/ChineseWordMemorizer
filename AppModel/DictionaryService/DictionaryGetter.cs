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
            OutputListData<DictionaryElement> Result = new OutputListData<DictionaryElement>();
            Result.Data = new List<DictionaryElement> ();
            Result.ErrorMessage = "";
            Result.IsSucsess = true;
            var counter = 0;

            string fileContent = File.ReadAllText (FilePath);
            var lines = fileContent.Replace("\r", "").Split('\n');
            try
            {
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
                Result.ErrorMessage = "Возникла ошибка на этапе чтения словаря в строке №" + counter + "."
                    + "\n\nИсключение: " + e.ToString() + "."
                    + "\n\nПроверьте структуру целевого файла (" + FilePath + ").";
            }

            return Result;
        }

        /// <summary>
        /// Метод, ожидающий получить на вход путь до существующего файла и возращающий на выход список пользовательских элементов, 
        /// а так же информацию об успешности выполнения метода, успешно ли он был выполнен и сообщение об ошибке если таковая имела место быть.
        /// </summary>
        public static OutputHashSetData<string> GetUserListFromFile(string FilePath)
        {
            OutputHashSetData<string> Result = new OutputHashSetData<string>();
            Result.Data = new HashSet<string>();   
            Result.ErrorMessage = "";
            Result.IsSucsess = true;

            string fileContent = File.ReadAllText(FilePath);
            var counter = 0;
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
                    Result.Data.Add(line);
                }
                Result.IsSucsess = true;
            }
            catch (Exception e)
            {
                Result.IsSucsess = false;
                Result.ErrorMessage = "Возникла ошибка на этапе чтения словаря в строке №" + counter + "."
                    + "\n\nИсключение: " + e.ToString() + "."
                    + "\n\nПроверьте структуру целевого файла (" + FilePath + ").";
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
            Result.ErrorMessage = "";
            Result.IsSucsess = true;

            var counter = 0;

            try
            {
                foreach (var currentString in InputUserList)
                {
                    counter++;
                    bool IsFind = false;
                    foreach (var element in InputTargetList)
                    {
                        if (element.RussianWord.Contains(currentString))
                        {
                            Result.Data.Add(element);
                            IsFind = true;
                            break;
                        }
                    }
                    if (!IsFind)
                    {
                        Result.IsSucsess = false;
                        Result.ErrorMessage = "Ошибка в строке #" + counter + ". Обнаружен элемент пользовательского списка, которого нет в словаре.";
                    }
                }
            }
            catch (Exception e)
            {
                Result.IsSucsess = false;
                Result.ErrorMessage = "Возникла неизвестная ошибка на этапе составления списка словарных единиц для тестирования. \n" 
                    + "Исключение:\n" 
                    + e.ToString();
            }

            return Result;
        } 
    }
}