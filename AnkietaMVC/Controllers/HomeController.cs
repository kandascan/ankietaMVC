using AnkietaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnkietaMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new AnkietaEntities();

            var section = db.Section.ToList();
            return View(section);
        }

        public ActionResult GetAnswers(FormCollection answers)
        {
            var userAnswers = new List<UserAnswer>();
            var db = new AnkietaEntities();
            var questions = db.Question.ToList();

            foreach (string answer in answers.AllKeys)
            {
                if (answers[answer].Contains("true"))
                {
                    var splitAnswers = answers[answer].Split(',');
                    var answerList = new List<bool>();
                    var boolToString = "";
                    var id = Convert.ToInt32(answer);
                    var queryAnswer = db.Answer.Where(x => x.QuestionId == id).ToList();
                    var j = 0;
                    for (var i = 0; i < splitAnswers.Length; i++)
                    {

                        if (!Convert.ToBoolean(splitAnswers[i]))
                        {
                            answerList.Add(false);
                        }
                        else
                        {
                            answerList.Add(true);
                            i++;
                        }
                    }
                    foreach (var boolAnswer in answerList)
                    {
                        if (boolAnswer)
                        {
                            boolToString  += queryAnswer[j].Value + ", ";
                        }
                        //boolToString += boolAnswer + ", ";
                        j++;
                    }
                    userAnswers.Add(new UserAnswer
                    {
                        QuestionId = answer,
                        Question = questions.SingleOrDefault(x => x.Id == Convert.ToInt32(answer)).Title,
                        Answer = boolToString
                    });
                }
                else if (answers[answer].Contains("false"))
                {
                    continue;
                }
                else
                {
                    userAnswers.Add(new UserAnswer
                    {
                        QuestionId = answer,
                        Question = questions.SingleOrDefault(x => x.Id == Convert.ToInt32(answer)).Title,
                        Answer = answers[answer]
                    });
                }
            }

            return View(userAnswers);
        }
    }
}