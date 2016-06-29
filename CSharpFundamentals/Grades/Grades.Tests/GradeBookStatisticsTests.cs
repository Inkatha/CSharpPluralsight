using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades.Tests
{
    [TestClass]
    public class GradeBookStatisticsTests
    {
        [TestMethod]
        public void LetterGrade()
        {
            GradeBook book = new GradeBook();
            book.AddGrade(90);
            book.AddGrade(91);
            book.AddGrade(99);

            GradeBookStatistics stats = book.ComputeStatistics();
            Assert.AreEqual("A", stats.LetterGrade);
        }

        [TestMethod]
        public void LetterGradeNotSet()
        {
            GradeBook book = new GradeBook();

            GradeBookStatistics stats = book.ComputeStatistics();
            Assert.AreEqual("Average grade has not been computed.", stats.LetterGrade);
        }

        [TestMethod]
        public void GradeDescription()
        {
            GradeBook book = new GradeBook();
            book.AddGrade(85);
            book.AddGrade(90);

            GradeBookStatistics stats = book.ComputeStatistics();
            Assert.AreEqual("Above-Average", stats.GradeDescription);
        }

        [TestMethod]
        public void GradeDescriptionNotSet()
        {
            GradeBook book = new GradeBook();

            GradeBookStatistics stats = book.ComputeStatistics();
            Assert.AreEqual("Unable to retrieve description", stats.GradeDescription);
        }
    }
}
