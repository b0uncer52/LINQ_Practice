using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LINQ_Practice.Models;
using System.Linq;

namespace LINQ_Practice
{
    [TestClass]
    public class LINQ_Practice_All
    {

        public List<Cohort> PracticeData { get; set; }
        public CohortBuilder CohortBuilder { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            CohortBuilder = new CohortBuilder();
            PracticeData = CohortBuilder.GenerateCohorts();
        }

        [TestCleanup]
        public void TearDown()
        {
            CohortBuilder = null;
            PracticeData = null;
        }

        [TestMethod]
        public void DoAllCohortsHaveTwoOrMoreJuniorInstructors()
        {
            var doAll = PracticeData.All(c => { return c.JuniorInstructors.Count >= 2; });
            Assert.IsTrue(doAll); 
        }

        [TestMethod]
        public void DoAllCohortsFiveStudents()
        {
            var doAll = PracticeData.All(c => { return c.Students.Count == 5; });
            Assert.IsTrue(doAll);
        }

        [TestMethod]
        public void DoAllCohortsHavePrimaryInstructorsBornIn1980s()
        {
            var doAll = PracticeData.All(c => { return c.PrimaryInstructor.Birthday.Year > 1979 && c.PrimaryInstructor.Birthday.Year < 1990; });
            Assert.IsFalse(doAll); 
        }

        [TestMethod]
        public void DoAllCohortsHaveActivePrimaryInstructors()
        {
            var doAll = PracticeData.All(c => { return c.PrimaryInstructor.Active; });
            Assert.IsTrue(doAll); 
        }

        [TestMethod]
        public void DoAllStudentsInCohort1HaveFirstNamesThatContainTheLetterE()
        {
            var doAll = PracticeData[0].Students.All(s => { return s.FirstName.ToUpper().Contains("E"); }); 
            Assert.IsTrue(doAll); 
        }

        [TestMethod]
        public void DoAllActiveCohortsHavePrimaryInstructorsWithFirstNamesThatContainTheLetterA()
        {
            var doAll = PracticeData.FindAll(c => { return c.Active; }).All(c => { return c.PrimaryInstructor.FirstName.ToUpper().Contains("A"); });
            Assert.IsFalse(doAll); 
        }
    }
}
