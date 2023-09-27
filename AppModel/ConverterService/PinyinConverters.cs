using AppCore.Constants;

namespace AppModel.ConverterService
{
    public class PinyinConverters
    {
        /// <summary>
        /// Переводит стандартную запись пиньинь в упрощённую стандартную (тон ставится после гласной).
        /// </summary>
        public static string StandartPinyinToSimple (string StandartString)
        {
            var Result = "";
            bool vowelWasFinded = false;

            foreach (var symbol in StandartString)
            {
                vowelWasFinded = false;
                for (int externalCounter = 1; externalCounter <= 4; externalCounter++)
                {
                    for (int internalCounter = 0; internalCounter <= 5; internalCounter++)
                    {

                        var currentMassive = PinYinConstants.PinYinVowels[externalCounter];
                        if (symbol == currentMassive[internalCounter])
                        {
                            var neutralMassive = PinYinConstants.PinYinVowels[0];
                            if (neutralMassive[internalCounter] == 'ü')
                            {
                                Result += neutralMassive[4] + "'" + externalCounter.ToString();
                            }
                            else
                            {
                                Result += neutralMassive[internalCounter] + externalCounter.ToString();
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
                    Result += symbol;
                }
            }

            return Result;
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