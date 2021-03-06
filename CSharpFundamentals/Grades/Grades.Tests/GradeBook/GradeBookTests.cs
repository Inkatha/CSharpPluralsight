﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grades.Tests
{
    [TestClass]
    public class GradeBookTests
    {
        [TestMethod]
        public void ComputeHighestGrade()
        {
            GradeBook book = InitializeGradeBook();

            GradeBookStatistics result = book.ComputeStatistics();
            Assert.AreEqual(90, result.HighestGrade);
        }

        [TestMethod]
        public void ComputeLowestGrade()
        {
            GradeBook book = InitializeGradeBook();

            GradeBookStatistics result = book.ComputeStatistics();
            Assert.AreEqual(0, result.LowestGrade);
        }
        
        [TestMethod]
        public void ComputeAverageGrade()
        {
            GradeBook book = InitializeGradeBook();

            GradeBookStatistics result = book.ComputeStatistics();
            Assert.AreEqual(45, result.AverageGrade);
        }

        private static GradeBook InitializeGradeBook()
        {
            GradeBook book = new GradeBook();
            book.AddGrade(90);
            book.AddGrade(0);
            return book;
        }
    }
}
