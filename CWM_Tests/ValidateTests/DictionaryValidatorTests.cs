using AppCore.Entities;
using AppModel.ValidateService;

namespace CWM_Tests.ValidateTests
{
    [TestClass]
    public class DictionaryValidatorTests
    {
        #region Cyrrilic tests
        [TestMethod]
        public void CyrTest_1 ()
        {
            Assert.AreEqual(true, DictionaryValidator.IsCyrrilicWord("абвяАБВЯ"));
        }
        [TestMethod]
        public void CyrTest_2()
        {
            Assert.AreEqual(false, DictionaryValidator.IsCyrrilicWord("абвяАБВЯA"));
        }
        [TestMethod]
        public void CyrTest_3()
        {
            Assert.AreEqual(false, DictionaryValidator.IsCyrrilicWord("абвяАБВЯ4"));
        }
        [TestMethod]
        public void CyrTest_4()
        {
            Assert.AreEqual(false, DictionaryValidator.IsCyrrilicWord("абвяАБВЯ%"));
        }
        [TestMethod]
        public void CyrTest_5()
        {
            Assert.AreEqual(true, DictionaryValidator.IsCyrrilicWord("любить/любовь/любимый"));
        }
        [TestMethod]
        public void CyrTest_6()
        {
            Assert.AreEqual(false, DictionaryValidator.IsCyrrilicWord("любить/любовь/любимыйf"));
        }
        [TestMethod]
        public void CyrTest_7()
        {
            Assert.AreEqual(false, DictionaryValidator.IsCyrrilicWord(""));
        }
        [TestMethod]
        public void CyrTest_8()
        {
            Assert.AreEqual(true, DictionaryValidator.IsCyrrilicWord("любить/любовь/любимый []"));
        }
        [TestMethod]
        public void CyrTest_9()
        {
            Assert.AreEqual(true, DictionaryValidator.IsCyrrilicWord("любить/любовь/любимый [слово]"));
        }
        [TestMethod]
        public void CyrTest_10()
        {
            Assert.AreEqual(true, DictionaryValidator.IsCyrrilicWord("воскресенье*"));
        }
        #endregion

        #region Chinese tests
        [TestMethod]
        public void ZhongCiTest_1 ()
        {
            Assert.AreEqual(true, DictionaryValidator.IsChineseHieroglyph("我鉤畫"));
        }
        [TestMethod]
        public void ZhongCiTest_2()
        {
            Assert.AreEqual(false, DictionaryValidator.IsChineseHieroglyph("我鉤畫т"));
        }
        [TestMethod]
        public void ZhongCiTest_3()
        {
            Assert.AreEqual(false, DictionaryValidator.IsChineseHieroglyph("我鉤畫m"));
        }
        [TestMethod]
        public void ZhongCiTest_4()
        {
            Assert.AreEqual(false, DictionaryValidator.IsChineseHieroglyph(""));
        }
        #endregion

        #region Pinyin-string (standart) tests
        [TestMethod]
        public void StandartPinyinTest_1()
        {
            Assert.AreEqual(false, DictionaryValidator.IsPinyinString(""));
        }
        [TestMethod]
        public void StandartPinyinTest_2()
        {
            Assert.AreEqual(true, DictionaryValidator.IsPinyinString("àihǎo"));
        }
        [TestMethod]
        public void StandartPinyinTest_3()
        {
            Assert.AreEqual(true, DictionaryValidator.IsPinyinString("bāngmáng"));
        }
        [TestMethod]
        public void StandartPinyinTest_4()
        {
            Assert.AreEqual(true, DictionaryValidator.IsPinyinString("nǚ’ér"));
        }
        [TestMethod]
        public void StandartPinyinTest_5()
        {
            Assert.AreEqual(true, DictionaryValidator.IsPinyinString("kāi wánxiào"));
        }
        [TestMethod]
        public void StandartPinyinTest_6()
        {
            Assert.AreEqual(false, DictionaryValidator.IsPinyinString("bāngmáng8"));
        }
        [TestMethod]
        public void StandartPinyinTest_7()
        {
            Assert.AreEqual(false, DictionaryValidator.IsPinyinString("nǚ’ér*"));
        }
        [TestMethod]
        public void StandartPinyinTest_8()
        {
            Assert.AreEqual(false, DictionaryValidator.IsPinyinString("!kāi wánxiào"));
        }
        #endregion

        #region Pinyin-compstring tests
        [TestMethod]
        public void PinyinCompTest_1()
        {
            Assert.AreEqual(true, DictionaryValidator.IsSimplifiedPinyin("pu3tong1hua4!"));
        }
        [TestMethod]
        public void PinyinCompTest_2()
        {
            Assert.AreEqual(false, DictionaryValidator.IsSimplifiedPinyin("!pu3tong1hua4!"));
        }
        [TestMethod]
        public void PinyinCompTest_3()
        {
            Assert.AreEqual(false, DictionaryValidator.IsSimplifiedPinyin("2pu3tong1hua4!"));
        }
        [TestMethod]
        public void PinyinCompTest_4()
        {
            Assert.AreEqual(true, DictionaryValidator.IsSimplifiedPinyin("bu4"));
        }
        [TestMethod]
        public void PinyinCompTest_5()
        {
            Assert.AreEqual(false, DictionaryValidator.IsSimplifiedPinyin("bue"));
        }
        [TestMethod]
        public void PinyinCompTest_6()
        {
            Assert.AreEqual(false, DictionaryValidator.IsSimplifiedPinyin("5433"));
        }
        [TestMethod]
        public void PinyinCompTest_7()
        {
            Assert.AreEqual(false, DictionaryValidator.IsSimplifiedPinyin(""));
        }
        [TestMethod]
        public void PinyinCompTest_8()
        {
            Assert.AreEqual(false, DictionaryValidator.IsSimplifiedPinyin("bUe4"));
        }
        #endregion

        #region Dict validator tests
        [TestMethod]
        public void DictValidatorTest_1 ()
        {
            var Control = new List<DictionaryElement> { 
                new DictionaryElement
                {
                    RussianWord = "АБВГЯабвгя",
                    ChineseWord = "词",
                    PinyinString = "kāihuì"
                },
                new DictionaryElement
                {
                    RussianWord = "АБВГЯабвгя",
                    ChineseWord = "不",
                    PinyinString = "kàn"
                }
            };
            var TestObject = DictionaryValidator.DictIsCorrect(Control);

            Assert.AreEqual(true, TestObject.IsValide);
        }
        [TestMethod]
        public void DictValidatorTest_2()
        {
            var Control = new List<DictionaryElement> {
                new DictionaryElement
                {
                    RussianWord = "АБВГЯабвгя",
                    ChineseWord = "词",
                    PinyinString = "ci2"
                },
                new DictionaryElement
                {
                    RussianWord = "АБВГЯабвгя",
                    ChineseWord = "不",
                    PinyinString = "bu4"
                }
            };
            var TestObject = DictionaryValidator.DictIsCorrect(Control);

            Assert.AreEqual(false, TestObject.IsValide);
        }
        public void DictValidatorTest_3()
        {
            var Control = new List<DictionaryElement> {
                new DictionaryElement
                {
                    RussianWord = "АБВГЯабвгя",
                    ChineseWord = "词f",
                    PinyinString = "kāihuì"
                },
                new DictionaryElement
                {
                    RussianWord = "АБВГЯабвгя",
                    ChineseWord = "不",
                    PinyinString = "kàn3"
                }
            };
            var TestObject = DictionaryValidator.DictIsCorrect(Control);

            Assert.AreEqual(false, TestObject.IsValide);
        }
        public void DictValidatorTest_4()
        {
            var Control = new List<DictionaryElement> {
                new DictionaryElement
                {
                    RussianWord = "АБВГЯабвгя",
                    ChineseWord = "词",
                    PinyinString = "kāihuì"
                },
                new DictionaryElement
                {
                    RussianWord = "АБВГЯабвгя",
                    ChineseWord = "不",
                    PinyinString = "kàn"
                }
            };
            var TestObject = DictionaryValidator.DictIsCorrect(Control);

            Assert.AreEqual(false, TestObject.IsValide);
        }
        #endregion
    }
}