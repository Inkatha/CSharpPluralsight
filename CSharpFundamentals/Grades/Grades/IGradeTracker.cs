using System.Collections;
using System.IO;

namespace Grades
{
    internal interface IGradeTracker : IEnumerable
    {
        GradeBookStatistics ComputeStatistics();
        void WriteGrades(TextWriter destination);
        void AddGrade(float grade);
        string Name { get; set; }
    }
}