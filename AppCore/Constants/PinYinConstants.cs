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
        public readonly static Dictionary<int, char[]> PinYinVowels = new Dictionary<int, char[]>()
        {
            {0, new char[]{ 'a', 'e', 'i', 'o', 'u', 'ü' } },
            {1, new char[]{ 'ā', 'ē', 'ī', 'ō', 'ū', 'ǖ' } },
            {2, new char[]{ 'á', 'é', 'í', 'ó', 'ú', 'ǘ' } },
            {3, new char[]{ 'ǎ', 'ě', 'ǐ', 'ǒ', 'ǔ', 'ǚ' } },
            {4, new char[]{ 'à', 'è', 'ì', 'ò', 'ù', 'ǜ' } }
        };

        public readonly static List<string> PinYinInitials = new List<string>()
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
            "zh",
            "ch",
            "sh",
            "r",
            "w",
            "y",
        };

        public readonly static List<string> PinYinFinals = new List<string>()
        {
            "a",
            "e",
            "i",
            "o",
            "u",
            "ü",
            "ai",
            "ao",
            "ei",
            "ia",
            "iao",
            "ie",
            "iu",
            "iou",
            "ou",
            "ua",
            "uai",
            "üe",
            "ui",
            "uei",
            "uo",
            "an",
            "en",
            "ian",
            "in",
            "uan",
            "üan",
            "un",
            "uen",
            "ün",
            "ang",
            "eng",
            "iang",
            "ing",
            "iong",
            "ong",
            "uang",
            "ueng",
            "er"
        };
    }
}