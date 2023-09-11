using AppCore.Entities;
using AppCore.Responses;
using AppModel.DictionaryService;
using System.Diagnostics.Metrics;

namespace CWM_Tests.ModelTests
{
    [TestClass]
    public class DictionaryGettersTests
    {
        #region GetElementsByUserList tests
        [TestMethod]
        public void GetElementsByUserListTest_LT1 ()
        {
            List<DictionaryElement> InitialData = new List<DictionaryElement>()
            {
                new DictionaryElement
                {
                    RussianWord = "Тест_1",
                    ChineseWord = "你",
                    PinyinString = "ni3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_2",
                    ChineseWord = "好",
                    PinyinString = "hao3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_3",
                    ChineseWord = "了",
                    PinyinString = "le0"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_4",
                    ChineseWord = "有",
                    PinyinString = "you3"
                }
            };

            HashSet<string> UserData = new HashSet<string>()
            {
                "Тест_1", "Тест_4"
            };

            List<DictionaryElement> Control = new List<DictionaryElement>()
            {
                new DictionaryElement
                {
                    RussianWord = "Тест_1",
                    ChineseWord = "你",
                    PinyinString = "ni3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_4",
                    ChineseWord = "有",
                    PinyinString = "you3"
                }
            };

            var methAns = DictionaryGetter.GetElementsByUserList(UserData, InitialData);

            Assert.AreEqual(
                OutputListData_DE_ToString(methAns.Data), OutputListData_DE_ToString(Control)
            );
        }

        [TestMethod]
        public void GetElementsByUserListTest_BL1()
        {
            List<DictionaryElement> InitialData = new List<DictionaryElement>()
            {
                new DictionaryElement
                {
                    RussianWord = "Тест_1",
                    ChineseWord = "你",
                    PinyinString = "ni3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_2",
                    ChineseWord = "好",
                    PinyinString = "hao3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_3",
                    ChineseWord = "了",
                    PinyinString = "le0"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_4",
                    ChineseWord = "有",
                    PinyinString = "you3"
                }
            };

            HashSet<string> UserData = new HashSet<string>()
            {
                "Тест_1", "Тест_4"
            };

            List<DictionaryElement> Control = new List<DictionaryElement>()
            {
                new DictionaryElement
                {
                    RussianWord = "Тест_1",
                    ChineseWord = "你",
                    PinyinString = "ni3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_4",
                    ChineseWord = "有",
                    PinyinString = "you3"
                }
            };

            var methAns = DictionaryGetter.GetElementsByUserList(UserData, InitialData);

            Assert.AreEqual(true, methAns.IsSucsess);

        }

        [TestMethod]
        public void GetElementsByUserListTest_EM1()
        {
            List<DictionaryElement> InitialData = new List<DictionaryElement>()
            {
                new DictionaryElement
                {
                    RussianWord = "Тест_1",
                    ChineseWord = "你",
                    PinyinString = "ni3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_2",
                    ChineseWord = "好",
                    PinyinString = "hao3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_3",
                    ChineseWord = "了",
                    PinyinString = "le0"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_4",
                    ChineseWord = "有",
                    PinyinString = "you3"
                }
            };

            HashSet<string> UserData = new HashSet<string>()
            {
                "Тест_1", "Тест_4"
            };

            List<DictionaryElement> Control = new List<DictionaryElement>()
            {
                new DictionaryElement
                {
                    RussianWord = "Тест_1",
                    ChineseWord = "你",
                    PinyinString = "ni3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_4",
                    ChineseWord = "有",
                    PinyinString = "you3"
                }
            };

            var methAns = DictionaryGetter.GetElementsByUserList(UserData, InitialData);

            Assert.AreEqual("", methAns.ErrorMessage);
        }

        [TestMethod]
        public void GetElementsByUserListTest_LT2()
        {
            List<DictionaryElement> InitialData = new List<DictionaryElement>()
            {
                new DictionaryElement
                {
                    RussianWord = "Тест_1",
                    ChineseWord = "你",
                    PinyinString = "ni3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_2",
                    ChineseWord = "好",
                    PinyinString = "hao3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_3",
                    ChineseWord = "了",
                    PinyinString = "le0"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_4",
                    ChineseWord = "有",
                    PinyinString = "you3"
                }
            };

            HashSet<string> UserData = new HashSet<string>()
            {
                "Тест_1", "Тест_4", "Тест_5"
            };

            List<DictionaryElement> Control = new List<DictionaryElement>()
            {
                new DictionaryElement
                {
                    RussianWord = "Тест_1",
                    ChineseWord = "你",
                    PinyinString = "ni3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_4",
                    ChineseWord = "有",
                    PinyinString = "you3"
                }   
            };

            var methAns = DictionaryGetter.GetElementsByUserList(UserData, InitialData);

            Assert.AreEqual(
                OutputListData_DE_ToString(methAns.Data), OutputListData_DE_ToString(Control)
            );
        }

        [TestMethod]
        public void GetElementsByUserListTest_BL2()
        {
            List<DictionaryElement> InitialData = new List<DictionaryElement>()
            {
                new DictionaryElement
                {
                    RussianWord = "Тест_1",
                    ChineseWord = "你",
                    PinyinString = "ni3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_2",
                    ChineseWord = "好",
                    PinyinString = "hao3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_3",
                    ChineseWord = "了",
                    PinyinString = "le0"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_4",
                    ChineseWord = "有",
                    PinyinString = "you3"
                }
            };

            HashSet<string> UserData = new HashSet<string>()
            {
                "Тест_1", "Тест_4", "Тест_5"
            };

            List<DictionaryElement> Control = new List<DictionaryElement>()
            {
                new DictionaryElement
                {
                    RussianWord = "Тест_1",
                    ChineseWord = "你",
                    PinyinString = "ni3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_4",
                    ChineseWord = "有",
                    PinyinString = "you3"
                }
            };

            var methAns = DictionaryGetter.GetElementsByUserList(UserData, InitialData);

            Assert.AreEqual(false, methAns.IsSucsess);

        }

        [TestMethod]
        public void GetElementsByUserListTest_EM2()
        {
            List<DictionaryElement> InitialData = new List<DictionaryElement>()
            {
                new DictionaryElement
                {
                    RussianWord = "Тест_1",
                    ChineseWord = "你",
                    PinyinString = "ni3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_2",
                    ChineseWord = "好",
                    PinyinString = "hao3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_3",
                    ChineseWord = "了",
                    PinyinString = "le0"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_4",
                    ChineseWord = "有",
                    PinyinString = "you3"
                }
            };

            HashSet<string> UserData = new HashSet<string>()
            { 
                "Тест_1", "Тест_4", "Тест_5" 
            };

            List<DictionaryElement> Control = new List<DictionaryElement>()
            {
                new DictionaryElement
                {
                    RussianWord = "Тест_1",
                    ChineseWord = "你",
                    PinyinString = "ni3"
                },
                new DictionaryElement
                {
                    RussianWord = "Тест_4",
                    ChineseWord = "有",
                    PinyinString = "you3"
                }
            };

            var methAns = DictionaryGetter.GetElementsByUserList(UserData, InitialData);

            Assert.AreEqual("Ошибка в строке #" + 3 + ". Обнаружен элемент пользовательского списка, которого нет в словаре.", methAns.ErrorMessage);
        }

        #endregion

        #region Вспомогательные методы
        private string OutputListData_DE_ToString (List<DictionaryElement> input)
        {
            string answer = "";
            foreach (var curObj in input)
            {
                answer += curObj.RussianWord + "\t" + curObj.ChineseWord + "\t" + curObj.PinyinString + "\n";
            }

            return answer;
        }
        #endregion
    }
}