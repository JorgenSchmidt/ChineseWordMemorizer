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
        public static ValideData DictIsCorrect (List<DictionaryElement> dict)
        {
            ValideData answer = new ValideData ();
            answer.IsValide = true;
            var counter = 0;
            var errorcounter = 0;
            var maxerror = 20;

            foreach (var curEl in dict)
            {
                counter++ ;
                if (!IsCyrrilicWord(curEl.RussianWord))
                {
                    answer.IsValide = false;
                    answer.ErrorMessage += ("\nОшибка в строке словаря hsk #" + counter + " (кириллица, словарь HSK).") ;
                    errorcounter++;
                } 

                if (!IsChineseHieroglyph(curEl.ChineseWord))
                {
                    answer.IsValide = false;
                    answer.ErrorMessage += ("\nОшибка в строке словаря hsk #" + counter + " (中文单词, 普通话, словарь HSK).");
                    errorcounter++;
                }

                if (!IsPinyinString(curEl.PinyinString))
                {
                    answer.IsValide = false;
                    answer.ErrorMessage += ("\nОшибка в строке #" + counter + " (拼音, словарь HSK).");
                    errorcounter++;
                }
                
                if (errorcounter == maxerror)
                {
                    answer.ErrorMessage += "\nНакопилось " + maxerror + " ошибок в словаре.";
                    break;
                }
            } 

            return answer;
        }

        /// <summary>
        /// Проверяет соответствует ли пользовательский список слов следующим требованиям:
        /// 1. Контент содержит только кириллические символы;
        /// 2. Элементы словаря должны быть разделены переносом строки.
        /// </summary>
        public static ValideData UserListIsCorrect (HashSet<string> dict)
        {
            ValideData answer = new ValideData ();
            answer.IsValide = true;
            var counter = 0;

            foreach (var curStr in dict)
            {
                counter++;
                if (!IsCyrrilicWord(curStr))
                {
                    answer.IsValide = false;
                    answer.ErrorMessage = "\nОшибка в строке #" + counter + " (пользовательский файл). Проверьте файл на соответствие требованиям.\n"
                        + "Файл должен содержать кириллические слова, разделённые переносом строки (наличие табуляции в любой строке воспринимается как ошибка)";
                }
            }

            return answer;
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
            var locString = input.ToLower();
            foreach (var curSym in locString)
            { 
                if (curSym <= '\u0400' || curSym >= '\u04FF')
                {
                    if (!(     curSym == '/' || curSym == ' ' || curSym == '!' || curSym == '-' || curSym == '.'
                            || curSym == '?' || curSym == '*' || curSym == '[' || curSym == ']'))
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
            foreach (var curSym in input)
            {
                if (!(  (curSym >= '\u4E00' && curSym <= '\u9FFF')
                    ||  (curSym >= '\u3400' && curSym <= '\u4DBF')
                    ||  (curSym >= '\uF900' && curSym <= '\uFAFF')))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Проверяет соответствует ли строка принятому в мире стандарту записи 拼音
        /// </summary>
        public static bool IsPinyinString (string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            foreach (var curSym in input)
            {
                if(!(   curSym == 257 || curSym == 225 || curSym == 462 || curSym == 224 ||
                        curSym == 275 || curSym == 233 || curSym == 283 || curSym == 232 ||
                        curSym == 299 || curSym == 237 || curSym == 464 || curSym == 236 ||
                        curSym == 333 || curSym == 243 || curSym == 466 || curSym == 242 ||
                        curSym == 363 || curSym == 250 || curSym == 468 || curSym == 249 ||
                        curSym == 252 || curSym == 472 || curSym == 470 || curSym == 474 || curSym == 476

                        || (curSym >= '\u0061' && curSym <= '\u007A')

                        || curSym == 32
                        || curSym == 8217
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

            foreach (var curSym in input)
            {
                if (!(  (curSym >= '\u0061' && curSym <= '\u007A')
                    ||  (curSym >= '\u0030' && curSym <= '\u0034')
                    ||  (curSym == 33)))
                {
                    return false;
                }
            }
            return true;
        }

    }
}