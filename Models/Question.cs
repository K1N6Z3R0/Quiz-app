using System.Collections.Generic;
using System;

public class BaseQuestion
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<string> Options { get; set; }
    public int CorrectOptionIndex { get; set; }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Base Question: {Text}");
    }
}
