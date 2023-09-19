namespace AppModel.ChekerService
{
    /// <summary>
    /// Содержит метод проверки введённого пользователем ответа на соответствие
    /// </summary>
    public class RussianCheckService
    {
        /// <summary>
        /// Метод проверки введённого пользователем ответа на соответствие
        /// </summary>
        public static bool CheckAnswer(string userAnswer, string general)
        {
            bool Answer = false;

            if (string.IsNullOrEmpty(userAnswer))
            {
                return false;
            }

            if (general.Contains('['))
            {
                var splits = general.Split('[');
                var cleanedstring = splits[0].Remove(splits[0].Length - 1);
                var words = cleanedstring.Split('/');
                foreach (var word in words)
                {
                    if (word.Equals(userAnswer))
                    {
                        Answer = true;
                        break;
                    }
                }
            }
            else
            {
                var splits = general.Split('/');
                foreach(var word in splits)
                {
                    if (word.Equals(userAnswer))
                    {
                        Answer = true;
                        break;
                    }
                }
            }

            return Answer;
        }
    }
}