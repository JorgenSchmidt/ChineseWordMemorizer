using AppCore.Constants;
using AppCore.Entities;

namespace AppModel.DictionaryService
{
    /// <summary>
    /// Методы, возвращающие строку по данным из списков/списка
    /// </summary>
    public class StringGetterWithDicts
    {
        /// <summary>
        /// Если из дефолтного словаря есть 2 и более вхождений по русскому слову, возвращает изменённую строку для китайской записи с указанием тона.
        /// Это связано с тем, что некоторые китайские иероглифы могут иметь несколько вариантов их произношения (по тонам).
        /// </summary>
        public static string GetChangeStringByDefaultList (    string chineseInp, // Изменить логику работы до 0.0.1.3
                                                               string russianInp, 
                                                               List<DictionaryElement> defaultDict
                                                          ) 
        {
            string Result = chineseInp;

            var chineseEntries = new List<DictionaryElement>();
            chineseEntries = defaultDict.Where(x => x.ChineseWord.Equals(chineseInp)).Select(x => x).ToList();

            if (chineseEntries.Count == 1)
            {
                return Result;
            }
            else
            {
                foreach (var element in chineseEntries)
                {
                    if (element.RussianWord.Equals(russianInp))
                    {
                        string toneNumber = "";
                        foreach (var symbol in element.PinyinString)
                        {
                            var vowelWasFinded = false;
                            for (int externalCounter = 0; externalCounter <= 4; externalCounter++)
                            {
                                for (int internalCounter = 0; internalCounter <= 5; internalCounter++)
                                {
                                    var currentMassive = PinYinConstants.PinYinVowels[externalCounter];
                                    if (symbol == currentMassive[internalCounter])
                                    {
                                        toneNumber += externalCounter.ToString();
                                        vowelWasFinded = true;
                                        break;
                                    }
                                }
                                if (vowelWasFinded)
                                {
                                    break;
                                }
                            }
                        }
                        Result += " t" + toneNumber;
                    }
                }
            }

            return Result;
        }
    }
}