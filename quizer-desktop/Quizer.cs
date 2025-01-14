using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace quizer_desktop
{
    public class Quizer
    {
        public async static Task<int?> SafeUpdateQuiz(Quiz quiz, List<int> DeletedQuestions)
        {
            var tasks = new List<Task>()
            {
                API.UpdateQuiz(quiz)
            };

            foreach (var question in quiz.questions)
            {
                if (question.id.HasValue)
                {
                    tasks.Add(API.UpdateQuestion(question));
                } else
                {
                    tasks.Add(API.CreateQuestion(quiz.id, question));
                }
            }

            foreach (var question_id in DeletedQuestions)
            {
                tasks.Add(API.DeleteQuestion(question_id));
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                try
                {
                    await tasks[i];
                }
                catch (SvelteError err)
                {
                    Utils.HandleError(err);
                    return i - 1;
                }
                catch (Exception)
                {
                    Utils.ErrorMsg("Unknown error Occured");
                    return i - 1;
                }
                
            }
            return null;
        }
    }
}
