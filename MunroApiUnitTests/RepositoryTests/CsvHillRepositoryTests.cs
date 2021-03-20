using Microsoft.VisualStudio.TestTools.UnitTesting;
using MunroApiData;
using MunroApiData.Repositories;
using System.Linq;

namespace MunroApiUnitTests
{
    [TestClass]
    public class CsvHillRepositoryTests
    {
        private CsvHillRepository csvHillRespoitory;

        [TestInitialize]
        public void Setup()
        {
            csvHillRespoitory = new CsvHillRepository(@"munrotab_v6.2.csv");
        }

        [TestMethod]
        public void CsvHillRepository_LoadData()
        {
            //get all hills with no search criteria
            var results = csvHillRespoitory.GetHills(new HillSearch());

            Assert.IsTrue(results.Count() > 1);
            //expecting 602 lines from the munro csv
            Assert.AreEqual(results.Count(), 602);
        }

        [TestMethod]
        public void CsvHillRepository_Get_ById_Exists()
        {
            //get hill #42
            var results = csvHillRespoitory.Get(42);

            //expecting a hill with running number 42
            Assert.AreEqual(42, results.RunningNo);
        }

        [TestMethod]
        public void CsvHillRepository_Get_ById_DoesNotExist()
        {
            //get hill #999
            var results = csvHillRespoitory.Get(999);

            //expecting a blank hill object
            Assert.AreEqual(0, results.RunningNo);
        }

        [TestMethod]
        public void CsvHillRepository_Get_ByName_Exists()
        {
            //get hill by name
            var results = csvHillRespoitory.Get("Beinn Alligin - Sgurr Mhor");

            //expecting a hill with matching name
            Assert.AreEqual("Beinn Alligin - Sgurr Mhor", results.Name);
        }

        [TestMethod]
        public void CsvHillRepository_Get_ByName_NoNamePassed()
        {
            //get hill by name
            var results = csvHillRespoitory.Get("");

            //expecting a null name
            Assert.IsNull(results.Name);
        }

        [TestMethod]
        public void CsvHillRepository_Get_ByName_DoesNotExist()
        {
            //get hill by name
            var results = csvHillRespoitory.Get("imaginary hill");

            //expecting a null name
            Assert.IsNull(results.Name);
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_NoSearch()
        {
            //get all hills with no search criteria
            var results = csvHillRespoitory.GetHills(new HillSearch());

            //expecting 602 unfiltered results
            Assert.AreEqual(results.Count(), 602);
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_CategoryFilter_Top()
        {
            var search = new HillSearch
            {
                Category = "TOP"
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting only results with TOP category
            Assert.IsTrue(results.All(x => x.Post1997 == "TOP"));
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_CategoryFilter_Mun()
        {
            var search = new HillSearch
            {
                Category = "MUN"
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting only results with MUN category
            Assert.IsTrue(results.All(x => x.Post1997 == "MUN"));
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_MinHeightFilter()
        {
            var search = new HillSearch
            {
                MinHeight = 1000
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting only results with heights over 1000
            Assert.IsTrue(results.All(x => x.HeightM >= 1000));
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_MaxHeightFilter()
        {
            var search = new HillSearch
            {
                MaxHeight = 1000
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting only results with heights under 1000
            Assert.IsTrue(results.All(x => x.HeightM <= 1000));
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_SortByName_Ascending()
        {
            var search = new HillSearch
            {
                SortBy = SortBy.Name,
                SortDirection = SortDirection.Asc
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting first result to start with an A
            Assert.IsTrue(results.First().Name.StartsWith("A"));
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_SortByName_Descending()
        {
            var search = new HillSearch
            {
                SortBy = SortBy.Name,
                SortDirection = SortDirection.Desc
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting last result to start with an a
            Assert.IsTrue(results.Last().Name.StartsWith("A"));
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_SortByHeight_Ascending()
        {
            var search = new HillSearch
            {
                SortBy = SortBy.Height,
                SortDirection = SortDirection.Asc
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting first result to be lower in height than last result
            Assert.IsTrue(results.First().HeightM < results.Last().HeightM);
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_SortByHeight_Descending()
        {
            var search = new HillSearch
            {
                SortBy = SortBy.Height,
                SortDirection = SortDirection.Desc
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting first result to be greater in height than last result
            Assert.IsTrue(results.First().HeightM > results.Last().HeightM);
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_Take_AscendingByName()
        {
            var search = new HillSearch
            {
                Take = 10,
                SortBy = SortBy.Name,
                SortDirection = SortDirection.Asc
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting first result to start with an a
            Assert.IsTrue(results.First().Name.StartsWith("A"));
            Assert.AreEqual(10, results.Count());
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_Take_DescendingByHeight()
        {
            var search = new HillSearch
            {
                Take = 10,
                SortBy = SortBy.Height,
                SortDirection = SortDirection.Desc
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting first result to be greater in height than last result
            Assert.IsTrue(results.First().HeightM > results.Last().HeightM);
            Assert.AreEqual(10, results.Count());
        }
    }
}
