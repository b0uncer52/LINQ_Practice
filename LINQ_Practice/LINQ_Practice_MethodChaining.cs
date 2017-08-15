using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LINQ_Practice.Models;
using System.Linq;

namespace LINQ_Practice
{
    [TestClass]
    public class LINQ_Practice_MethodChaining
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
        public void GetAllCohortsWithZacharyZohanAsPrimaryOrJuniorInstructor()
        {
            var ActualCohorts = (from c in PracticeData where c.JuniorInstructors.Any(j => { return j.FirstName == "Zachary"; }) select c).Union(from c in PracticeData where c.PrimaryInstructor.FirstName == "Zachary" select c).OrderBy(c => { return c.Name; }).ToList();
            CollectionAssert.AreEqual(ActualCohorts, new List<Cohort> { CohortBuilder.Cohort2, CohortBuilder.Cohort3 });
        }

        [TestMethod]
        public void GetAllCohortsWhereFullTimeIsFalseAndAllInstructorsAreActive()
        {
            var ActualCohorts = (from c in PracticeData where !c.FullTime && c.PrimaryInstructor.Active && c.JuniorInstructors.All(j => { return j.Active; }) select c).ToList();
            CollectionAssert.AreEqual(ActualCohorts, new List<Cohort> { CohortBuilder.Cohort1 });
        }

        [TestMethod]
        public void GetAllCohortsWhereAStudentOrInstructorFirstNameIsKate()
        {
            var ActualCohorts = (from c in PracticeData where c.PrimaryInstructor.FirstName == "Kate" || c.JuniorInstructors.Any(j => { return j.FirstName == "Kate"; }) || c.Students.Any(s => { return s.FirstName == "Kate"; }) select c).ToList();
            CollectionAssert.AreEqual(ActualCohorts, new List<Cohort> { CohortBuilder.Cohort1, CohortBuilder.Cohort3, CohortBuilder.Cohort4 });
        }

        [TestMethod]
        public void GetOldestStudent()
        {
            var student = PracticeData.SelectMany(c => c.Students).Single(s => { return s.Birthday == PracticeData.SelectMany(c => { return c.Students; }).Min(st => { return st.Birthday; });});
            Assert.AreEqual(student, CohortBuilder.Student18);
        }

        [TestMethod]
        public void GetYoungestStudent()
        {
            var student = PracticeData.SelectMany(c => c.Students).Single(s => { return s.Birthday == PracticeData.SelectMany(c => { return c.Students; }).Max(st => { return st.Birthday; }); });
            Assert.AreEqual(student, CohortBuilder.Student3);
        }

        [TestMethod]
        public void GetAllInactiveStudentsByLastName()
        {
            var ActualStudents = PracticeData.SelectMany(c => { return c.Students; }).Where(s => { return !s.Active; }).OrderBy(s => { return s.LastName; }).ToList();
            CollectionAssert.AreEqual(ActualStudents, new List<Student> { CohortBuilder.Student2, CohortBuilder.Student11, CohortBuilder.Student12, CohortBuilder.Student17 });
        }
    }
}
