using Microsoft.VisualStudio.TestTools.UnitTesting;
using MunroApiData;

namespace MunroApiUnitTests
{
    [TestClass]
    public class HillSearchCheck
    {

        [TestMethod]
        public void HillSearch_Check_BlankFilters_NoErrors()
        {
            var hillSearch = new HillSearch
            {
                Category = "",
                MaxHeight = 0,
                MinHeight = 0,
                Take = 0,
                SortBy = "",
                SortDirection = ""
            };

            var result = hillSearch.Check();

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void HillSearch_Check_Category_InvalidCategory()
        {
            var hillSearch = new HillSearch
            {
                Category = "asdfs"
            };

            var result = hillSearch.Check();

            Assert.AreEqual("Category can be either MUN, TOP or left blank for both", result);
        }

        [TestMethod]
        public void HillSearch_Check_Category_MunCategory()
        {
            var hillSearch = new HillSearch
            {
                Category = "Mun"
            };

            var result = hillSearch.Check();

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void HillSearch_Check_Category_TopCategory()
        {
            var hillSearch = new HillSearch
            {
                Category = "Top"
            };

            var result = hillSearch.Check();

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void HillSearch_Check_Sort_Invalid()
        {
            var hillSearch = new HillSearch
            {
                SortBy = "Mun"
            };

            var result = hillSearch.Check();

            Assert.AreEqual("Sort by can be either NAME or HEIGHT", result);
        }

        [TestMethod]
        public void HillSearch_Check_Sort_Name()
        {
            var hillSearch = new HillSearch
            {
                SortBy = "Name"
            };

            var result = hillSearch.Check();

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void HillSearch_Check_Sort_Height()
        {
            var hillSearch = new HillSearch
            {
                SortBy = "Height"
            };

            var result = hillSearch.Check();

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void HillSearch_Check_MaxHeightLessThanMinHeight()
        {
            var hillSearch = new HillSearch
            {
                MaxHeight = 500,
                MinHeight = 1000
            };

            var result = hillSearch.Check();

            Assert.AreEqual("Max height must be greater than min height", result);
        }

        [TestMethod]
        public void HillSearch_Check_SortDirection_Invalid()
        {
            var hillSearch = new HillSearch
            {
                SortDirection = "Height"
            };

            var result = hillSearch.Check();

            Assert.AreEqual("Sort direction can be either ASC or DESC. Defaults to ASC if left blank.", result);
        }

        [TestMethod]
        public void HillSearch_Check_SortDirection_Ascending()
        {
            var hillSearch = new HillSearch
            {
                SortDirection = "asc"
            };

            var result = hillSearch.Check();

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void HillSearch_Check_SortDirection_Descending()
        {
            var hillSearch = new HillSearch
            {
                SortDirection = "desc"
            };

            var result = hillSearch.Check();

            Assert.AreEqual("", result);
        }
    }
}
