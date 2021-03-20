using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunroApiData
{
    public class HillSearch
    {
        public string Category { get; set; }

        public int MinHeight { get; set; }

        public int MaxHeight { get; set; }

        public int Take { get; set; }

        public SortBy SortBy { get; set; }

        public SortDirection SortDirection { get; set; }

    }

    public enum SortDirection
    {
        Asc, Desc
    }

    public enum SortBy
    {
        Name, Height
    }
}
