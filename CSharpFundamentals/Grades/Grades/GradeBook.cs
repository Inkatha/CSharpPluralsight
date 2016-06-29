using System;
using System.Collections.Generic;

namespace Grades
{
    public class GradeBook
    {
        private List<float> grades;
        private string _name;

        public event NameChangedDelegate NameChanged;

        public GradeBook()
        {
            _name = "Empty";
            grades = new List<float>();
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (_name != value)
                    {
                        NameChangedEventArgs args = new NameChangedEventArgs();
                        args.ExistingName = _name;
                        args.NewName = value;
                        NameChanged(this, args);
                    }
                    _name = value;
                }
            }
        }

        public void AddGrade(float grade)
        {
            grades.Add(grade);
        }

        public GradeBookStatistics ComputeStatistics()
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
    }
}
