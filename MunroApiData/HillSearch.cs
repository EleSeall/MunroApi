using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunroApiData
{
    public class HillSearch
    {
        public string Category { get; set; } = "";

        public int MinHeight { get; set; } = 0;

        public int MaxHeight { get; set; } = 0;

        public int Take { get; set; } = 0;

        public string SortBy { get; set; } = "";

        public string SortDirection { get; set; } = "ASC";

        /// <summary>
        /// Sanity checks the filter
        /// </summary>
        /// <returns>Error message, or blank if ok</returns>
        public string Check()
        {
            if (!string.IsNullOrWhiteSpace(Category) &&
                Category.ToUpper() != "MUN" && Category.ToUpper() != "TOP")
            {
                return "Category can be either MUN, TOP or left blank for both";
            }

            if (!string.IsNullOrWhiteSpace(SortBy) &&
                SortBy.ToUpper() != "NAME" && SortBy.ToUpper() != "HEIGHT")
            {
                return "Sort by can be either NAME or HEIGHT";
            }

            if (!string.IsNullOrWhiteSpace(SortDirection) &&
                SortDirection.ToUpper() != "ASC" && SortDirection.ToUpper() != "DESC")
            {
                return "Sort direction can be either ASC or DESC. Defaults to ASC if left blank.";
            }

            if (MaxHeight > 0
                && MinHeight > 0
                && MaxHeight < MinHeight)
            {
                return "Max height must be greater than min height";
            }

            return "";
        }
    }
}
