using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            GradeBook book = new GradeBook();
            while (book.Name == "Empty")
            {
                try
                {
                    Console.WriteLine("Enter a name for the gradebook:");
                    book.Name = Console.ReadLine();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Something went wrong!");
                }
            }

            Console.WriteLine(book.Name);

            book.AddGrade(91);
            book.AddGrade(75);
            book.AddGrade(84);

            using (StreamWriter outputFile = File.CreateText("grades.txt"))
            {
                book.WriteGrades(outputFile);
            }

            GradeBookStatistics stats = book.ComputeStatistics();
            WriteResult("Average", stats.AverageGrade);
            WriteResult("Highest", (int) stats.HighestGrade);
            WriteResult("Lowest", stats.LowestGrade);
            WriteResult(stats.GradeDescription, stats.LetterGrade);
        }

        static void WriteResult(string description, float result)
        {
            Console.WriteLine($"{description}: {result}");
        }
        static void WriteResult(string description, string result)
        {
            Console.WriteLine($"{description}: {result}");
        }

        static void OnNameChanged(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine($"Gradebook changing name from {args.ExistingName} to {args.NewName}");
        }
    }
}
