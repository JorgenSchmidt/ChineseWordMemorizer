using AppCore.Constants;

namespace AppModel.ConverterService
{
    public class PinyinConverters
    {
        /// <summary>
        /// Переводит стандартную запись пиньинь в упрощённую (тон ставится после гласной).
        /// </summary>
        public static string StandartPinyinToSimple(string StandartString)
        {
            var Result = "";
            bool vowelWasFinded = false;

            // Перебор символов входной строки
            foreach (var symbol in StandartString)
            {
                // Определяет является ли символ гласной, если да, гласная нейтрализуется и добавляется в основную строку, к ней прибавляется номер тона
                // Если нет, то символ просто добавляется в основную строку
                vowelWasFinded = false;

                // Перебор словаря гласных по тонам (с первого по четвёртый, т.к. для нулевого тона нет смысла добавлять особый символ в конец)
                for (int externalCounter = 1; externalCounter <= 4; externalCounter++)
                {
                    // Перебор словаря гласных по символам
                    for (int internalCounter = 0; internalCounter <= 5; internalCounter++)
                    {
                        // Копирование массива символов по соответствующему тону (внешний цикл)
                        var currentMassive = PinYinConstants.PinYinVowels_Tones[externalCounter];
                        // Поиск вхождений введённого символа 
                        if (symbol == currentMassive[internalCounter])
                        {
                            // Инициализация переменной с нейтральными тонами (для последующего добавления соответствующего символа в конечную строку)
                            var neutralMassive = PinYinConstants.PinYinVowels_Tones[0];
                            // Для гласной ü определён свой алгоритм конкатенации, потому если обнаружен такой символ, записывается он по особому, как v - номер тона
                            if (neutralMassive[internalCounter] == 'ü')
                            {
                                Result += "v" + externalCounter.ToString();
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

        /// <summary>
        /// Переводит упрощённую запись пиньинь в стандартную.
        /// </summary>
        public static string SimplifiedPinyinToStandart (string SimplifiedPinyin) 
        {
            string Result = "";

            try
            {
                // Перебор всех символов входной строки
                for (int i = 0; i < SimplifiedPinyin.Length; i++)
                {
                    // Если обнаружен символ, соответствующий цифре, то из итоговой строчки удаляется последний символ и заменяется на 
                    // соответствующий номеру тону предшествующей гласной
                    // Если иначе, то символ добавляется к итоговой строчке
                    if (Char.IsDigit(SimplifiedPinyin[i]))
                    {
                        int toneNumber = SimplifiedPinyin[i] - '0';
                        // Для буквы v, соответствующей в 拼音 гласной ü, алгоритм конкатенации символа к строке будет выглядеть по другому, нежели для остальных
                        if (SimplifiedPinyin[i-1] == 'v')
                        {
                            var charMassive = PinYinConstants.PinYinVowels_Letters['ü'];
                            Result = Result.Remove(Result.Length - 1);
                            Result += charMassive[toneNumber - 1];
                        }
                        else
                        {
                            var charMassive = PinYinConstants.PinYinVowels_Letters[SimplifiedPinyin[i - 1]];
                            Result = Result.Remove(Result.Length - 1);
                            Result += charMassive[toneNumber - 1];
                        }
                    }
                    else
                    {
                        Result += SimplifiedPinyin[i];
                    }
                }
            }
            catch
            {
                Result = "Error.";
            }

            return Result;
        }

        // Добавить к версии 0.0.1.4
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