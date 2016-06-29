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


        public string LetterGrade
        {
            get
            {
                string result = "";
                if (AverageGrade >= 90)
                {
                    result = "A";
                }
                else if(AverageGrade >= 80)
                {
                    result = "B";
                }
                else if(AverageGrade >= 70)
                {
                    result = "C";
                }
                else if(AverageGrade >= 60)
                {
                    result = "D";
                }
                else if(AverageGrade >= 59 && AverageGrade != 0)
                {
                    result = "F";
                }
                else
                {
                    result = "Average grade has not been computed.";
                }
                return result;
            }
        }

        public string GradeDescription
        {
            get
            {
                string result = "";
                switch(LetterGrade)
                {
                    case "A":
                        result = "Excellent";
                        break;
                    case "B":
                        result = "Above-Average";
                        break;
                    case "C":
                        result = "Average";
                        break;
                    case "D":
                        result = "Below-Average";
                        break;
                    case "F":
                        result = "Failing";
                        break;
                    default:
                        result = "Unable to retrieve description";
                        break;
                }
                return result;
            }
        }

        public GradeBookStatistics()
        {
            AverageGrade = 0;
            HighestGrade = 0;
            LowestGrade = float.MaxValue;
        }
    }
}
