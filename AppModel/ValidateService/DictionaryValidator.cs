using AppCore.Entities;
using AppCore.Responses;
using System.Text.RegularExpressions;

namespace AppModel.ValidateService
{
    /// <summary>
    /// Содержит методы валидации входящего контента
    /// </summary>
    public class DictionaryValidator
    {
        /// <summary>
        /// Проверка входного словаря HSK на соответствие поставленным требованиям
        /// </summary>
        public static ValideData DictIsCorrect (List<DictionaryElement> InputDictionary)
        {
            ValideData Message = new ValideData ();
            Message.IsValide = true;
            var counter = 0;
            var errorcounter = 0;
            var maxerror = 20;

            foreach (var element in InputDictionary)
            {
                counter++ ;
                if (!IsCyrrilicWord(element.RussianWord))
                {
                    Message.IsValide = false;
                    Message.Message += ("\nОшибка в строке словаря hsk #" + counter + " (кириллица, словарь HSK).") ;
                    errorcounter++;
                } 

                if (!IsChineseHieroglyph(element.ChineseWord))
                {
                    Message.IsValide = false;
                    Message.Message += ("\nОшибка в строке словаря hsk #" + counter + " (中文单词, 普通话, словарь HSK).");
                    errorcounter++;
                }

                if (!IsPinyinString(element.PinyinString))
                {
                    Message.IsValide = false;
                    Message.Message += ("\nОшибка в строке #" + counter + " (拼音, словарь HSK).");
                    errorcounter++;
                }
                
                if (errorcounter == maxerror)
                {
                    Message.Message += "\nНакопилось " + maxerror + " ошибок в словаре.";
                    break;
                }
            } 

            return Message;
        }

        /// <summary>
        /// Проверяет соответствует ли пользовательский список слов следующим требованиям:
        /// 1. Контент содержит только кириллические символы;
        /// 2. Элементы словаря должны быть разделены переносом строки.
        /// </summary>
        public static ValideData UserListIsCorrect (HashSet<string> dict)
        {
            ValideData Message = new ValideData ();
            Message.IsValide = true;
            var counter = 0;

            foreach (var currentString in dict)
            {
                counter++;
                if (!IsCyrrilicWord(currentString))
                {
                    Message.IsValide = false;
                    Message.Message = "\nОшибка в строке #" + counter + " (пользовательский файл). Проверьте файл на соответствие требованиям.\n"
                        + "Файл должен содержать кириллические слова, разделённые переносом строки (наличие табуляции в любой строке воспринимается как ошибка)";
                }
            }

            return Message;
        }

        /// <summary>
        /// Определяет является ли входная строка кириллическим словом (допускаются так же знаки '/', пробел, '!', '?', '*', точка и квадратные скобки)
        /// </summary>
        public static bool IsCyrrilicWord (string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            var lowered = input.ToLower();
            foreach (var symbol in lowered)
            { 
                if (symbol <= '\u0400' || symbol >= '\u04FF')
                {
                    if (!(     symbol == '/' || symbol == ' ' || symbol == '!' || symbol == '-' || symbol == '.'
                            || symbol == '?' || symbol == '*' || symbol == '[' || symbol == ']'))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Определяет является ли входная строка китайским словом
        /// </summary>
        public static bool IsChineseHieroglyph (string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            foreach (var symbol in input)
            {
                if (!(  (symbol >= '\u4E00' && symbol <= '\u9FFF')
                    ||  (symbol >= '\u3400' && symbol <= '\u4DBF')
                    ||  (symbol >= '\uF900' && symbol <= '\uFAFF')))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Проверяет соответствует ли строка принятому в мире стандарту записи 拼音.
        /// </summary>
        public static bool IsPinyinString (string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            foreach (var symbol in input)
            {
                if(!(   symbol == 257 || symbol == 225 || symbol == 462 || symbol == 224 ||
                        symbol == 275 || symbol == 233 || symbol == 283 || symbol == 232 ||
                        symbol == 299 || symbol == 237 || symbol == 464 || symbol == 236 ||
                        symbol == 333 || symbol == 243 || symbol == 466 || symbol == 242 ||
                        symbol == 363 || symbol == 250 || symbol == 468 || symbol == 249 ||
                        symbol == 252 || symbol == 472 || symbol == 470 || symbol == 474 || symbol == 476

                        || (symbol >= '\u0061' && symbol <= '\u007A')

                        || symbol == 32
                        || symbol == 8217
                   ))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Проверяет соответствует ли строка заранее установленному в программе стандарту записи 拼音 для тестирования
        /// </summary>
        public static bool IsSimplifiedPinyin (string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            if (!(input[0] >= '\u0061' && input[0] <= '\u007A'))
            {
                return false;
            }

            if (!(Regex.IsMatch(input, @"\d+") && Regex.IsMatch(input, @"[a-z]") ))
            {
                return false;
            }

            foreach (var symbol in input)
            {
                if (!(  (symbol >= '\u0061' && symbol <= '\u007A')
                    ||  (symbol >= '\u0030' && symbol <= '\u0034')
                    ||  (symbol == 33)))
                {
                    return false;
                }
            }
            return true;
        }

    }
}