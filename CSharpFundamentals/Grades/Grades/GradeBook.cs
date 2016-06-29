using System;
using System.Collections.Generic;
using System.IO;

namespace Grades
{
    public class GradeBook
    {
        protected List<float> grades;
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
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("The gradebooks name cannot be empty.");
                }

                if (_name != value)
                {
                    NameChangedEventArgs args = new NameChangedEventArgs();
                    args.ExistingName = _name;
                    args.NewName = value;
                    NameChanged?.Invoke(this, args);
                }
                _name = value;
            }
        }
        public void WriteGrades(TextWriter destination)
        {
            for (int i = 0; i < grades.Count; i++)
            {
                destination.WriteLine(grades[i]);
            }
        }

        public void AddGrade(float grade)
        {
            grades.Add(grade);
        }

        public virtual GradeBookStatistics ComputeStatistics()
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
