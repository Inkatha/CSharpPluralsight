using System;
using System.Collections.Generic;
using System.IO;

namespace Grades
{
    public class GradeBook : GradeTracker
    {
        protected List<float> grades;

        public GradeBook()
        {
            _name = "Empty";
            grades = new List<float>();
        }
        
        public override void WriteGrades(TextWriter destination)
        {
            for (int i = 0; i < grades.Count; i++)
            {
                destination.WriteLine(grades[i]);
            }
        }

        public override GradeBookStatistics ComputeStatistics()
        {
            GradeBookStatistics stats = new GradeBookStatistics();
            float sum = 0;

            foreach (float grade in grades)
            {
                stats.HighestGrade = Math.Max(grade, stats.HighestGrade);
                stats.LowestGrade = Math.Min(grade, stats.LowestGrade);
                sum += grade;
            }
            stats.AverageGrade = sum / grades.Count;

            return stats;
        }
        public override void AddGrade(float grade)
        {
            grades.Add(grade);
        }
    }
}
