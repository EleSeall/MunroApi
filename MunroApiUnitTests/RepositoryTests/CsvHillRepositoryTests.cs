using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var results = csvHillRespoitory.GetHills(new MunroApiData.HillSearch());

            Assert.IsTrue(results.Count() > 1);
            //expecting 602 lines from the munro csv
            Assert.AreEqual(results.Count(), 602);
        }
    }
}
