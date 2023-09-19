namespace AppCore.Entities
{
    /// <summary>
    /// Элемент словаря, содержащий строку на кириллице, китайский иероглиф и строку 拼音
    /// </summary>
    public class DictionaryElement
    {
        public string RussianWord { get; set; }
        public string ChineseWord { get; set; }
        public string PinyinString { get; set; }
    }
}