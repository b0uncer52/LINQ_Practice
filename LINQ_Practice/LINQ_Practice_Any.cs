using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LINQ_Practice.Models;
using System.Linq;

namespace LINQ_Practice
{
    [TestClass]
    public class LINQ_Practice_Any
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
        public void DoAnyCohortsHavePrimaryInstructorsBornIn1980s()
        {
            var doAny = PracticeData.Any(c => { int year = c.PrimaryInstructor.Birthday.Year; return year < 1990 && year > 1979; });
            Assert.IsTrue(doAny); 
        }

        [TestMethod]
        public void DoAnyCohortsHaveActivePrimaryInstructors()
        {
            var doAny = PracticeData.Any(c => { return c.PrimaryInstructor.Active; });
            Assert.IsTrue(doAny); 
        }

        [TestMethod]
        public void DoAnyActiveCohortsHave3JuniorInstructors()
        {
            var doAny = PracticeData.Any(c => { return c.Active && c.JuniorInstructors.Count == 3; });
            Assert.IsTrue(doAny); 
        }

        [TestMethod]
        public void AreAnyCohortsBothFullTimeAndNotActive()
        {
            var doAny = PracticeData.Any(c => { return !c.Active && c.FullTime; });
            Assert.IsTrue(doAny); 
        }

        [TestMethod]
        public void AreAnyStudentsInCohort3NotActiveAndBornInOctober()
        {
            var doAny = PracticeData[2].Students.Any(s => { return !s.Active && s.Birthday.Month == 10; });
            Assert.IsFalse(doAny); 
        }

        [TestMethod]
        public void AreAnyJuniorInstructorsInCohort4NotActive()
        {
            var doAny = PracticeData[3].JuniorInstructors.Any(j => { return !j.Active; });
            Assert.IsFalse(doAny);
        }
    }
}
