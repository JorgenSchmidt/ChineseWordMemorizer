namespace AppModel.ChekerService
{
    /// <summary>
    /// Содержит метод проверки введённого пользователем ответа на соответствие
    /// </summary>
    public class RussianCheckService
    {
        /// <summary>
        /// Проверяет соответствует ли введённая пользователем строка генеральной 
        /// </summary>
        public static bool CheckAnswer(string UserAnswer, string General)
        {
            bool Result = false;

            if (string.IsNullOrEmpty(UserAnswer))
            {
                return false;
            }

            if (General.Contains('['))
            {
                var splits = General.Split('[');
                var cleanedstring = splits[0].Remove(splits[0].Length - 1);
                var words = cleanedstring.Split('/');
                foreach (var word in words)
                {
                    if (word.Equals(UserAnswer))
                    {
                        Result = true;
                        break;
                    }
                }
            }
            else
            {
                var splits = General.Split('/');
                foreach(var word in splits)
                {
                    if (word.Equals(UserAnswer))
                    {
                        Result = true;
                        break;
                    }
                }
            }

            return Result;
        }
    }
}