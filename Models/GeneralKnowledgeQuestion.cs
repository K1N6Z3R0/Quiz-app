using QuizApp.Models;
using System;
using static System.Net.Mime.MediaTypeNames;

public class GeneralKnowledgeQuestion : BaseQuestion
{
    public override void DisplayInfo()
    {
        Console.WriteLine($"General Knowledge Question: {Text}");
    }
}
