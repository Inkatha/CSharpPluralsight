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
            GradeTracker book = CreateGradeBook();

            GetBookName(book);
            AddGrades(book);
            WriteToFile(book);
            DisplayResults(book);
        }

        private static GradeTracker CreateGradeBook()
        {
            return new ThrowAwayGradeBook();
        }

        private static void DisplayResults(GradeTracker book)
        {
            GradeBookStatistics stats = book.ComputeStatistics();
            WriteResult("Average", stats.AverageGrade);
            WriteResult("Highest", (int)stats.HighestGrade);
            WriteResult("Lowest", stats.LowestGrade);
            WriteResult(stats.GradeDescription, stats.LetterGrade);
        }

        private static void WriteToFile(GradeTracker book)
        {
            using (StreamWriter outputFile = File.CreateText("grades.txt"))
            {
                book.WriteGrades(outputFile);
            }
        }

        private static void AddGrades(GradeTracker book)
        {
            book.AddGrade(91);
            book.AddGrade(75);
            book.AddGrade(84);
        }

        private static void GetBookName(GradeTracker book)
        {
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
