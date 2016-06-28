using System;
using System.Collections.Generic;

namespace Grades
{
    public class GradeBook
    {
        private List<float> grades = new List<float>();
        public string Name;

        public GradeBook()
        {
            
        }
        
        public void AddGrade(float grade)
        {
            grades.Add(grade);
        }

        public GradeBookStatistics ComputeStatistics()
        {
            GradeBookStatistics stats = new GradeBookStatistics();
            stats.HighestGrade = 0;
            stats.LowestGrade = float.MaxValue;
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
    }
}
