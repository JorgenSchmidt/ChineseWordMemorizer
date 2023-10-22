using AppModel.ConverterService;

namespace CWM_Tests.ConverterTests
{
    [TestClass]
    public class PinyinConvertersTests
    {
/*        [TestMethod]
        public void NeutralizedStandartTest_1()
        {
            var control = "aeiouü";
            var changed = PinyinConverters.NeutralizedStandartPinyinString ("āéǐòǔǘ");

            Assert.AreEqual (true, control.Equals(changed));
        }
        [TestMethod]
        public void NeutralizedStandartTest_2()
        {
            var control = "zhengzai";
            var changed = PinyinConverters.NeutralizedStandartPinyinString("zhèngzài");

            Assert.AreEqual(true, control.Equals(changed));
        }
        [TestMethod]
        public void NeutralizedStandartTest_3()
        {
            var control = "xiaoxuesheng";
            var changed = PinyinConverters.NeutralizedStandartPinyinString("xiǎoxuéshēng");

            Assert.AreEqual(true, control.Equals(changed));
        }
        [TestMethod]
        public void NeutralizedStandartTest_4()
        {
            var control = "mei shenme";
            var changed = PinyinConverters.NeutralizedStandartPinyinString("méi shénme");

            Assert.AreEqual(true, control.Equals(changed));
        }*/

        [TestMethod]
        public void StandartPinyinToSimpleTest_1()
        {
            var control = "a1e2i3o4u3v2";
            var changed = PinyinConverters.StandartPinyinToSimple("āéǐòǔǘ");

            Assert.AreEqual(true, control.Equals(changed));
        }

        [TestMethod]
        public void StandartPinyinToSimpleTest_2()
        {
            var control = "zhe4ngza4i";
            var changed = PinyinConverters.StandartPinyinToSimple("zhèngzài");

            Assert.AreEqual(true, control.Equals(changed));
        }
        [TestMethod]
        public void StandartPinyinToSimpleTest_3()
        {
            var control = "xia3oxue2she1ng";
            var changed = PinyinConverters.StandartPinyinToSimple("xiǎoxuéshēng");

            Assert.AreEqual(true, control.Equals(changed));
        }
        [TestMethod]
        public void StandartPinyinToSimpleTest_4()
        {
            var control = "me2i she2nme";
            var changed = PinyinConverters.StandartPinyinToSimple("méi shénme");

            Assert.AreEqual(true, control.Equals(changed));
        }

        [TestMethod]
        public void SimpleToStandartPinyinTest_1()
        {
            var control = "āéǐòǔǘ";
            var changed = PinyinConverters.SimplifiedPinyinToStandart("a1e2i3o4u3v2");

            Assert.AreEqual(true, control.Equals(changed));
        }
        [TestMethod]
        public void SimpleToStandartPinyinTest_2()
        {
            var control = "zhèngzài";
            var changed = PinyinConverters.SimplifiedPinyinToStandart("zhe4ngza4i");

            Assert.AreEqual(true, control.Equals(changed));
        }
        [TestMethod]
        public void SimpleToStandartPinyinTest_3()
        {
            var control = "xiǎoxuéshēng";
            var changed = PinyinConverters.SimplifiedPinyinToStandart("xia3oxue2she1ng");

            Assert.AreEqual(true, control.Equals(changed));
        }
        [TestMethod]
        public void SimpleToStandartPinyinTest_4()
        {
            var control = "méi shénme";
            var changed = PinyinConverters.SimplifiedPinyinToStandart("me2i she2nme");

            Assert.AreEqual(true, control.Equals(changed));
        }


    }
}