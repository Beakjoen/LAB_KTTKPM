using System;
using System.Text;
using StudentManagement;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        new StudentUI().Run();
    }
}
