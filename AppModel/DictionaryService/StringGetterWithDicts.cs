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
        public static string GetChangeStringByDefaultList (    string chineseInp, 
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
                foreach (var el in chineseEntries)
                {
                    if (el.RussianWord.Equals(russianInp))
                    {
                        string toneNumber = "";
                        foreach (var chr in el.PinyinString)
                        {
                            var vowelWasFinded = false;
                            for (int ce = 0; ce <= 4; ce++)
                            {
                                for (int ci = 0; ci <= 5; ci++)
                                {
                                    var currentMassive = PinYinConstants.PinYinVowels[ce];
                                    if (chr == currentMassive[ci])
                                    {
                                        toneNumber += ce.ToString();
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