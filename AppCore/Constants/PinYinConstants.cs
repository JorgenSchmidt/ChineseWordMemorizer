namespace AppCore.Constants
{
    /// <summary>
    /// Основные константы для работы с пиньинь транскрипцией
    /// </summary>
    public class PinYinConstants
    {
        /// <summary>
        /// Словарь значений key - номер тона, value - список гласных
        /// </summary>
        public readonly static Dictionary<int, char[]> PinYinVowels_Tones = new Dictionary<int, char[]>()
        {
            {0, new char[]{ 'a', 'e', 'i', 'o', 'u', 'ü' } },
            {1, new char[]{ 'ā', 'ē', 'ī', 'ō', 'ū', 'ǖ' } },
            {2, new char[]{ 'á', 'é', 'í', 'ó', 'ú', 'ǘ' } },
            {3, new char[]{ 'ǎ', 'ě', 'ǐ', 'ǒ', 'ǔ', 'ǚ' } },
            {4, new char[]{ 'à', 'è', 'ì', 'ò', 'ù', 'ǜ' } }
        };

        /// <summary>
        /// Словарь значений key - нейтральная гласная, value - гласные, имеющие тон
        /// </summary>
        public readonly static Dictionary<char, char[]> PinYinVowels_Letters = new Dictionary<char, char[]>()
        {
            {'a', new char[]{ 'ā', 'á', 'ǎ', 'à' } },
            {'e', new char[]{ 'ē', 'é', 'ě', 'è' } },
            {'i', new char[]{ 'ī', 'í', 'ǐ', 'ì' } },
            {'o', new char[]{ 'ō', 'ó', 'ǒ', 'ò' } },
            {'u', new char[]{ 'ū', 'ú', 'ǔ', 'ù' } },
            {'ü', new char[]{ 'ǖ', 'ǘ', 'ǚ', 'ǜ' } }
        };

        /// <summary>
        /// Список инициалей китайского языка, ключ - количество символов в инициалях, значение - список финалей
        /// </summary>
        public readonly static Dictionary<int, string[]> PinYinInitials = new Dictionary<int, string[]>() // Для будущих изменений логики работы с 拼音 строками (0.0.1.4)
        {
            {   
                2, new string[]
                {
                    "zh",
                    "ch",
                    "sh"
                }
            },
            {
                1, new string[]
                {
                    "b",
                    "p",
                    "m",
                    "f",
                    "d",
                    "t",
                    "n",
                    "l",
                    "g",
                    "k",
                    "h",
                    "j",
                    "q",
                    "x",
                    "z",
                    "c",
                    "s",
                    "r",
                    "w",
                    "y"
                }
            }
        };

        /// <summary>
        /// Список финалей китайского языка, ключ - количество символов в финалях, значение - список финалей (нулевой тон)
        /// </summary>
        public readonly static Dictionary<int, string[]> PinYinFinals = new Dictionary<int, string[]>() // Для будущих изменений логики работы с 拼音 строками (0.0.1.4)
        {
            {
                4, new string[]
                {
                    "uang",
                    "ueng",
                    "iang",
                    "iong"
                }
            },
            {
                3, new string[]
                {

                    "iao",
                    "iou",
                    "uai",
                    "uei",
                    "ian",
                    "uan",
                    "üan",
                    "uen",
                    "ang",
                    "eng",
                    "ing",
                    "ong"
                }
            },
            {
                2, new string[]
                {
                    "ai",
                    "ao",
                    "ei",
                    "ia",
                    "ou",
                    "ua",
                    "üe",
                    "ui",
                    "ie",
                    "iu",
                    "uo",
                    "an",
                    "en",
                    "in",
                    "un",
                    "ün",
                    "er"
                }
            },
            {
                1, new string[]
                {
                    "a",
                    "e",
                    "i",
                    "o",
                    "u",
                    "ü"
                }
            }
        };
    }
}