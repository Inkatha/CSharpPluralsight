using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public abstract class GradeTracker
    {
        public abstract GradeBookStatistics ComputeStatistics();
        public abstract void WriteGrades(TextWriter destination);
        public abstract void AddGrade(float grade);
        public event NameChangedDelegate NameChanged;
        protected string _name;
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
    }
}
