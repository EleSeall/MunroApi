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
            var results = csvHillRespoitory.GetAll();

            Assert.IsTrue(results.Count() > 1);
            //expecting 602 lines from the munro csv
            Assert.AreEqual(results.Count(), 602);
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

            //expecting 509 results (exclude blanks from Post1997 category column)
            Assert.AreEqual(results.Count(), 509);
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
            Assert.IsTrue(results.All(x => x.Category == "TOP"));
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
            Assert.IsTrue(results.All(x => x.Category == "MUN"));
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
            Assert.IsTrue(results.All(x => x.Height >= 1000));
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
            Assert.IsTrue(results.All(x => x.Height <= 1000));
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_SortByName_Ascending()
        {
            var search = new HillSearch
            {
                SortBy = "Name",
                SortDirection = "Asc"
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
                SortBy = "Name",
                SortDirection = "Desc"
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
                SortBy = "Height",
                SortDirection = "Asc"
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting first result to be lower in height than last result
            Assert.IsTrue(results.First().Height < results.Last().Height);
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_SortByHeight_Descending()
        {
            var search = new HillSearch
            {
                SortBy = "Height",
                SortDirection = "Desc"
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting first result to be greater in height than last result
            Assert.IsTrue(results.First().Height > results.Last().Height);
        }

        [TestMethod]
        public void CsvHillRepository_GetHills_Take_AscendingByName()
        {
            var search = new HillSearch
            {
                Take = 10,
                SortBy = "Name",
                SortDirection = "Asc"
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
                SortBy = "Height",
                SortDirection = "Desc"
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting first result to be greater in height than last result
            Assert.IsTrue(results.First().Height > results.Last().Height);
            Assert.AreEqual(10, results.Count());
        }


        [TestMethod]
        public void CsvHillRepository_GetHills_MultipleFilters()
        {
            var search = new HillSearch
            {
                Category = "MUN",
                MinHeight = 500,
                MaxHeight = 1000,
                Take = 10,
                SortBy = "Height",
                SortDirection = "Desc"
            };

            //get all hills with search criteria
            var results = csvHillRespoitory.GetHills(search);

            //expecting first result to be greater in height than last result
            Assert.IsTrue(results.First().Height > results.Last().Height);

            //heights over 500
            Assert.IsTrue(results.All(x => x.Height >= 500));

            //heights under 1000
            Assert.IsTrue(results.All(x => x.Height <= 1000));

            //only munroes
            Assert.IsTrue(results.All(x=>x.Category == "MUN"));

            //take 10 only
            Assert.AreEqual(10, results.Count());
        }
    }
}
