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

        public SortBy SortBy { get; set; } = SortBy.None;

        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }

    public enum SortDirection
    {
        Asc, Desc
    }

    public enum SortBy
    {
        None, Name, Height
    }
}
