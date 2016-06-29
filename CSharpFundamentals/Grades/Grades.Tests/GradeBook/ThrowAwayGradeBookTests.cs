using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades.Tests
{
    [TestClass]
    public class ThrowAwayGradeBookTests
    {
        [TestMethod]
        public void DropLowestGrade()
        {
            GradeBook book = InitializeGradeBook();

            GradeBookStatistics stats = book.ComputeStatistics();
            Assert.AreEqual(90, stats.AverageGrade);
        }
        private static GradeBook InitializeGradeBook()
        {
            GradeBook book = new ThrowAwayGradeBook();
            book.AddGrade(90);
            book.AddGrade(0);
            return book;
        }
    }
}
