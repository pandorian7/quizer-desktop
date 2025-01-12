using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quizer_desktop
{
    public class Credentials
    {
        public string username { set; get; }
        public string password { set; get; }
    }

    public class SvelteError : Exception
    {
        public SvelteError(string message) : base(message)
        {
        }
    }
    public class SvelteJSONError
    {
        public string message { set; get; }
    }

    public class QuizJson
    {
        public int id { set; get; }
        public string title { set; get; }
        public string description { set; get; }
        public int points { set; get; }
        public int owner_id { set; get; }
        public string username { set; get; }
    }

    public class QuizResultsJson {
        public List<QuizJson> quizes {  set; get; }
    }

    public class NewQuiz
    {
        public string title { set; get; }
    }

}
