using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public class GradeBookStatistics
    {
        public float AverageGrade;
        public float HighestGrade;
        public float LowestGrade;

        public GradeBookStatistics()
        {
            AverageGrade = 0;
            HighestGrade = 0;
            LowestGrade = 0;
        }
    }
}
