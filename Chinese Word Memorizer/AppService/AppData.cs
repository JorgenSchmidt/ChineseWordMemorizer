using AppCore.Entities;
using System.Collections.Generic;

namespace Chinese_Word_Memorizer.AppService
{
    public class AppData
    {
        public static bool WindowOpeningIsAllow = false;

        public static List<DictionaryElement>? CurrentAppDictionary = null;
    }
}