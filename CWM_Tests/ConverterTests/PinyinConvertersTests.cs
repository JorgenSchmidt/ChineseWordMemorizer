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
        public void PrimaryVarStandPIToLocalPITest_1 ()
        {
            var control = "a1e2i3o4u3u'2";
            var changed = PinyinConverters.StandartPinyinToLocalPinyin("āéǐòǔǘ");

            Assert.AreEqual(true, control.Equals(changed));
        }

        [TestMethod]
        public void PrimaryVarStandPIToLocalPITest_2()
        {
            var control = "zhe4ngza4i";
            var changed = PinyinConverters.StandartPinyinToLocalPinyin("zhèngzài");

            Assert.AreEqual(true, control.Equals(changed));
        }
        [TestMethod]
        public void PrimaryVarStandPIToLocalPITest_3()
        {
            var control = "xia3oxue2she1ng";
            var changed = PinyinConverters.StandartPinyinToLocalPinyin("xiǎoxuéshēng");

            Assert.AreEqual(true, control.Equals(changed));
        }
        [TestMethod]
        public void PrimaryVarStandPIToLocalPITest_4()
        {
            var control = "me2i she2nme";
            var changed = PinyinConverters.StandartPinyinToLocalPinyin("méi shénme");

            Assert.AreEqual(true, control.Equals(changed));
        }

    }
}