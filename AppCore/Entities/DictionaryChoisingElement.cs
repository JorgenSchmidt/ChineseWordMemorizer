namespace AppCore.Entities
{
    /// <summary>
    /// Расширенная версия сущности DictionaryElement с добавлением свойства типа bool - IsChoised.
    /// Будет определять какие слова включить в генерируемый список, а какие нет (предназначение данной сущности).
    /// Содержит так же поле IsViewed, которое отражает будет ли элемент отображён в интерфейсе пользователя
    /// </summary>
    public class DictionaryChoisingElement : DictionaryElement
    {
        public bool IsChoised { get; set; }
        public bool IsViewed { get; set; }

        public DictionaryChoisingElement() { }

        public DictionaryChoisingElement(string chinese, string russian, string pinyin, bool isChoised, bool isViewed) 
        { 
            ChineseWord = chinese;
            RussianWord = russian;
            PinyinString = pinyin;
            IsChoised = isChoised;
            IsViewed = isViewed;
        }
    }
}