using AppCore.Entities;
using AppModel.DictionaryService;

namespace CWM_Tests.ModelTests
{
    [TestClass]
    public class StringGetterTests
    {
        [TestMethod]
        public void GetChangeStringByDefaultListTest_1 ()
        {
            var zhongci = "吗";
            var rus = "что?/какой? [вм.  шенме?]";

            var dict = new List<DictionaryElement>()
            {
                new DictionaryElement()
                {
                    RussianWord = "немедленно",
                    ChineseWord = "马上",
                    PinyinString = "mǎshàng"
                },
                new DictionaryElement()
                {
                    RussianWord = "что?/какой? [вм.  шенме?]",
                    ChineseWord = "吗",
                    PinyinString = "má"
                },
                new DictionaryElement()
                {
                    RussianWord = "конечная частица в вопросе",
                    ChineseWord = "吗",
                    PinyinString = "ma"
                },
                new DictionaryElement()
                {
                    RussianWord = "купить/покупать",
                    ChineseWord = "买",
                    PinyinString = "mǎi"
                },

            };

            var control = "吗 t2";
            var res = StringGetterWithDicts.GetChangeStringByDefaultList(zhongci, rus, dict);

            Assert.AreEqual(true, res.Equals(control));
        }
        [TestMethod]
        public void GetChangeStringByDefaultListTest_2()
        {
            var zhongci = "马上";
            var rus = "немедленно";

            var dict = new List<DictionaryElement>()
            {
                new DictionaryElement()
                {
                    RussianWord = "немедленно",
                    ChineseWord = "马上",
                    PinyinString = "mǎshàng"
                },
                new DictionaryElement()
                {
                    RussianWord = "что?/какой? [вм.  шенме?]",
                    ChineseWord = "吗",
                    PinyinString = "ma"
                },
                new DictionaryElement()
                {
                    RussianWord = "конечная частица в вопросе",
                    ChineseWord = "吗",
                    PinyinString = "má"
                },
                new DictionaryElement()
                {
                    RussianWord = "купить/покупать",
                    ChineseWord = "买",
                    PinyinString = "mǎi"
                },

            };

            var control = "马上";
            var res = StringGetterWithDicts.GetChangeStringByDefaultList(zhongci, rus, dict);

            Assert.AreEqual(true, res.Equals(control));
        }
        [TestMethod]
        public void GetChangeStringByDefaultListTest_3()
        {
            var zhongci = "女人";
            var rus = "жена";

            var dict = new List<DictionaryElement>()
            {
                new DictionaryElement()
                {
                    RussianWord = "немедленно",
                    ChineseWord = "马上",
                    PinyinString = "mǎshàng"
                },
                new DictionaryElement()
                {
                    RussianWord = "что?/какой? [вм.  шенме?]",
                    ChineseWord = "吗",
                    PinyinString = "ma"
                },
                new DictionaryElement()
                {
                    RussianWord = "конечная частица в вопросе",
                    ChineseWord = "吗",
                    PinyinString = "má"
                },
                new DictionaryElement()
                {
                    RussianWord = "купить/покупать",
                    ChineseWord = "买",
                    PinyinString = "mǎi"
                },
                new DictionaryElement()
                {
                    RussianWord = "жена",
                    ChineseWord = "女人",
                    PinyinString = "nǚren"
                },
                new DictionaryElement()
                {
                    RussianWord = "женщина",
                    ChineseWord = "女人",
                    PinyinString = "nǚrén"
                }
            };

            var control = "女人 t30";
            var res = StringGetterWithDicts.GetChangeStringByDefaultList(zhongci, rus, dict);

            Assert.AreEqual(true, res.Equals(control));
        }
    }
}