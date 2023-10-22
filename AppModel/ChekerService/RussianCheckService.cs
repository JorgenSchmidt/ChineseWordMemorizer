namespace AppModel.ChekerService
{
    /// <summary>
    /// Содержит метод проверки введённого пользователем ответа на соответствие
    /// </summary>
    public class RussianCheckService
    {
        /// <summary>
        /// Проверяет соответствует ли введённая пользователем строка словарной
        /// </summary>
        public static bool CheckAnswer(string UserAnswer, string General)
        {
            bool Result = false;

            // Проверка строки на пустоту
            if (string.IsNullOrEmpty(UserAnswer))
            {
                return false;
            }

            // Проверяет содержит ли строка пояснения, упакованные в "['comment']".
            // Если пояснения обнаружены - всё что в пределах [] - удаляется, 
            // после чего сравнивается одно из словарных значений, разделённых "/" с вводом пользователя
            if (General.Contains('['))
            {
                var splits = General.Split('[');
                var cleanedstring = splits[0].Remove(splits[0].Length - 1);
                var words = cleanedstring.Split('/');
                Result = IsStringsEquals(UserAnswer, splits);
            }
            else
            {
                var splits = General.Split('/');
                Result = IsStringsEquals(UserAnswer, splits);
            }

            return Result;
        }
        /// <summary>
        /// Проверяет соответствует ли хотя бы одно значение из входного массива вводу пользователя
        /// </summary>
        private static bool IsStringsEquals(string UserAnswer, string[] InputStrings)
        {
            bool IsEquals = false;

            foreach (var word in InputStrings)
            {
                if (word.Equals(UserAnswer))
                {
                    IsEquals = true;
                    break;
                }
            }

            return IsEquals;
        }
    }
}