using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace QuizApp.Controllers
{
    public class HomeController : Controller
    {
        // Static dictionary to store multiple user credentials
        private static readonly Dictionary<string, string> Users = new Dictionary<string, string>
        {
            { "SandhraStephen", "password123" }
            //{ "JohnDoe", "securepass" },
            //{ "JaneSmith", "mypassword" },
            //{ "AliceBrown", "admin123" }
        };

        // Login Page
        public ActionResult Login()
        {
            return View();
        }

        // Login Submission
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                // Authentication logic here
            }
            ViewBag.Message = "Invalid username or password.";
            return View(user);
        }

        public ActionResult Logout()
        {
            try
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "Logout"));
            }
        }

        // Select Quiz Page
        public ActionResult SelectQuiz()
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login");
                }

                ViewBag.Username = Session["Username"];
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "SelectQuiz"));
            }
        }

        // Start Quiz
        [HttpPost]
        public ActionResult StartQuiz(string quizType)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login");
                }

                if (string.IsNullOrEmpty(quizType))
                {
                    TempData["ErrorMessage"] = "Please select a quiz type.";
                    return RedirectToAction("SelectQuiz");
                }

                return RedirectToAction("Quiz", new { quizType });
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "StartQuiz"));
            }
        }

        // Quiz Page
        public ActionResult Quiz(string quizType)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login");
                }

                var questions = GenerateQuestions(quizType);

                if (questions == null || questions.Count == 0)
                {
                    TempData["ErrorMessage"] = "No questions available for the selected quiz type.";
                    return RedirectToAction("SelectQuiz");
                }

                ViewBag.QuizType = quizType;
                Session["Questions"] = questions;
                return View(questions);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "Quiz"));
            }
        }

        // Submit Quiz
        [HttpPost]
        public ActionResult SubmitQuiz(List<int> answers)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return Content("Session expired. Please log in again.");
                }

                var questions = Session["Questions"] as List<BaseQuestion>;
                if (questions == null)
                {
                    return Content("No questions were found for this quiz session.");
                }

                int score = CalculateScore(questions, answers);

                return Json(new
                {
                    score = score,
                    total = questions.Count
                });
            }
            catch (Exception ex)
            {
                return Content("An error occurred: " + ex.Message);
            }
        }

        // Generate Questions
        private List<BaseQuestion> GenerateQuestions(string quizType)
        {
            if (quizType == "Programming")
            {
                return new List<BaseQuestion>
                {
                    new BaseQuestion { Text = "What does HTML stand for?", Options = new List<string> { "HyperText Markup Language", "HighText Machine Language", "HyperTool Machine Language" }, CorrectOptionIndex = 0 },
                    new BaseQuestion { Text = "Which programming language is primarily used for Android app development?", Options = new List<string> { "Python", "Java", "C#" }, CorrectOptionIndex = 1 }
                };
            }
            else if (quizType == "General Knowledge")
            {
                return new List<BaseQuestion>
                {
                    new BaseQuestion { Text = "Who was the first President of the USA?", Options = new List<string> { "Abraham Lincoln", "George Washington", "Thomas Jefferson" }, CorrectOptionIndex = 1 },
                    new BaseQuestion { Text = "What is the capital of France?", Options = new List<string> { "Berlin", "Madrid", "Paris" }, CorrectOptionIndex = 2 }
                };
            }
            else
            {
                return null;
            }
        }

        // Calculate Score
        private int CalculateScore(List<BaseQuestion> questions, List<int> answers)
        {
            int score = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                if (i < answers.Count && answers[i] == questions[i].CorrectOptionIndex)
                {
                    score++;
                }
            }
            return score;
        }
    }
}
