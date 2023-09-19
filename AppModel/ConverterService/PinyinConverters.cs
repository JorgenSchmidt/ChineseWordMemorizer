using AppCore.Constants;

namespace AppModel.ConverterService
{
    public class PinyinConverters
    {
        /// <summary>
        /// Переводит стандартную запись пиньинь в принятую в программе.
        /// </summary>
        public static string StandartPinyinToLocalPinyin (string standartString)
        {
            bool vowelWasFinded = false;
            var Answer = "";

            foreach (var chr in standartString)
            {
                vowelWasFinded = false;
                for (int ce = 1; ce <= 4; ce++)
                {
                    for (int ci = 0; ci <= 5; ci++)
                    {
                        var currentMassive = PinYinConstants.PinYinVowels[ce];
                        if (chr == currentMassive[ci])
                        {
                            var neutralMassive = PinYinConstants.PinYinVowels[0];
                            if (neutralMassive[ci] == 'ü')
                            {
                                Answer += neutralMassive[4] + "'" + ce.ToString();
                            }
                            else
                            {
                                Answer += neutralMassive[ci] + ce.ToString();
                            }
                            
                            vowelWasFinded = true;
                            break;
                        }
                    }
                    if (vowelWasFinded)
                    {
                        break;
                    }
                }
                if (!vowelWasFinded)
                {
                    Answer += chr;
                }
            }

            return Answer;
        }

        /*/// <summary>
        /// Нейтрализует входную строку до 0 тона
        /// </summary>
        public static string NeutralizedStandartPinyinString(string standartString)
        {
            var Answer = "";
            bool vowelWasFinded = false;

            foreach (var chr in standartString)
            {
                vowelWasFinded = false;
                for (int ce = 1; ce <= 4; ce++)
                {
                    for (int ci = 0; ci <= 5; ci++)
                    {
                        var currentMassive = PinYinConstants.PinYinVowels[ce];
                        if (chr == currentMassive[ci])
                        {
                            var neutralMassive = PinYinConstants.PinYinVowels[0];
                            Answer += neutralMassive[ci];
                            vowelWasFinded = true;
                            break;
                        }
                    }
                    if (vowelWasFinded)
                    {
                        break;
                    }
                }
                if (!vowelWasFinded)
                {
                    Answer += chr;
                }
            }

            return Answer;
        }*/
    }

    /*internal class InitialAndFinalElement
    {
        internal string? Initial;
        internal string? Final;
    }*/
}