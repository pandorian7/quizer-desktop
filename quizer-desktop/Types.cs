using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quizer_desktop
{
    public class Credentials
    {
        public required string username { set; get; }
        public required string password { set; get; }
    }

    public class SvelteError : Exception
    {
        public SvelteError(string message) : base(message)
        {
        }
    }
    public class SvelteJSONError
    {
        public required string message { set; get; }
    }

    public class QuizJson
    {
        public required int id { set; get; }
        public required string title { set; get; }
        public required string description { set; get; }
        public required int points { set; get; }
        public required int owner_id { set; get; }
        public required string username { set; get; }
    }

    public class QuizResultsJson
    {
        public required List<QuizJson> quizes { set; get; }
    }

    public class NewQuiz
    {
        public required string title { set; get; }
    }

    public class Answer
    {
        public int? id { set; get; }
        public required string answer { set; get; }
        public required int is_correct { set; get; }
    }

    public class Question
    {
        public int? id { set; get; }
        public required string question { set; get; }
        public required int multiple_answers { set; get; }
        public required int duration { set; get; }
        public required List<Answer> answers { set; get; }
    }

    public class Quiz
    {
        public int? id { set; get; }
        public required string title { set; get; }
        public string? description { set; get; }
        public required int points { set; get; }
        public required int owner_id { set; get; }
        public required List<Question> questions { set; get; }

    }

}