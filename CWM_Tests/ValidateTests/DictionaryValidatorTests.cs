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
        #endregion

        #region Putonghua tests
        [TestMethod]
        public void PutonghuaTest_1 ()
        {
            Assert.AreEqual(true, DictionaryValidator.IsSimplifiedPinyin("pu3tong1hua4!"));
        }
        [TestMethod]
        public void PutonghuaTest_2()
        {
            Assert.AreEqual(false, DictionaryValidator.IsSimplifiedPinyin("!pu3tong1hua4!"));
        }
        [TestMethod]
        public void PutonghuaTest_3()
        {
            Assert.AreEqual(false, DictionaryValidator.IsSimplifiedPinyin("2pu3tong1hua4!"));
        }
        [TestMethod]
        public void PutonghuaTest_4()
        {
            Assert.AreEqual(true, DictionaryValidator.IsSimplifiedPinyin("bu4"));
        }
        [TestMethod]
        public void PutonghuaTest_5()
        {
            Assert.AreEqual(false, DictionaryValidator.IsSimplifiedPinyin("bue"));
        }
        [TestMethod]
        public void PutonghuaTest_6()
        {
            Assert.AreEqual(false, DictionaryValidator.IsSimplifiedPinyin("5433"));
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

            Assert.AreEqual(true, TestObject.IsValide);
        }
        [TestMethod]
        public void DictValidatorTest_2()
        {
            var Control = new List<DictionaryElement> {
                new DictionaryElement
                {
                    RussianWord = "АБВГЯабвгfя",
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
        #endregion
    }
}