using AppCore.Entities;
using AppCore.Responses;
using System.Text.RegularExpressions;

namespace AppModel.ValidateService
{
    /// <summary>
    /// Содержит методы для проверки 
    /// </summary>
    public class DictionaryValidator
    {
        /// <summary>
        /// Проверка входного списка на соответствие поставленным требованиям
        /// </summary>
        public static ValideData DictIsCorrect (List<DictionaryElement> dict)
        {
            ValideData answer = new ValideData ();
            answer.IsValide = true;
            var counter = 0;

            foreach (var curEl in dict)
            {
                counter++ ;
                if (!IsCyrrilicWord(curEl.RussianWord))
                {
                    answer.IsValide = false;
                    answer.ErrorMessage += ("\nОшибка в строке словаря hsk #" + counter + " (кириллица, словарь HSK).") ;
                } 

                if (!IsChineseHieroglyph(curEl.ChineseWord))
                {
                    answer.IsValide = false;
                    answer.ErrorMessage += ("\nОшибка в строке словаря hsk #" + counter + " (中文单词, 普通话, словарь HSK).");
                }

                if (!IsSimplifiedPinyin(curEl.PinyinString))
                {
                    answer.IsValide = false;
                    answer.ErrorMessage += ("\nОшибка в строке #" + counter + " (拼音, словарь HSK).");
                }
            } 

            return answer;
        }

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
        /// Определяет является ли входная строка кириллическим словом (допускаются так же знаки '/', пробел, '!', '?')
        /// </summary>
        public static bool IsCyrrilicWord (string input)
        {
            var locString = input.ToLower();
            foreach (var curSym in locString)
            { 
                if (curSym <= '\u0400' || curSym >= '\u04FF')
                {
                    if (!(curSym == '/' || curSym == ' ' || curSym == '!' || curSym == '?'))
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
        /// Проверяет соответствует ли строка заранее установленному стандарту записи 拼音
        /// </summary>
        public static bool IsSimplifiedPinyin (string input)
        {
            var locString = input.ToLower(); 
            if (!(locString[0] >= '\u0061' && locString[0] <= '\u007A'))
            {
                return false;
            }

            if (!(Regex.IsMatch(locString, @"\d+") && Regex.IsMatch(locString, @"[a-z]") ))
            {
                return false;
            }

            foreach (var curSym in locString)
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