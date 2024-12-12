using QuizApp.Models;
using System;
using static System.Net.Mime.MediaTypeNames;

public class ProgrammingQuestion : BaseQuestion
{
    public override void DisplayInfo()
    {
        Console.WriteLine($"Programming Question: {Text}");
    }
}
