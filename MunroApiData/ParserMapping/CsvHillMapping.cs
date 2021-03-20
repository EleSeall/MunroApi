using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace MunroApiData.ParserMapping
{
    class CsvHillMapping : CsvMapping<CsvHill>
    {
        public CsvHillMapping()
            : base()
        {
            MapProperty(0, x => x.RunningNo);
            MapProperty(1, x => x.DoBIHNumber);
            MapProperty(2, x => x.Streetmap);
            MapProperty(3, x => x.Geograph);
            MapProperty(4, x => x.Hillbagging);
            MapProperty(5, x => x.Name);
            MapProperty(6, x => x.SMCSection);
            MapProperty(7, x => x.RHBSection);
            MapProperty(8, x => x.Section);
            MapProperty(9, x => x.HeightM);
            MapProperty(10, x => x.HeightFt);
            MapProperty(11, x => x.Map1to50);
            MapProperty(12, x => x.Map1to25);
            MapProperty(13, x => x.GridRef);
            MapProperty(14, x => x.GridRefXY);
            MapProperty(15, x => x.Xcoord);
            MapProperty(16, x => x.Ycoord);
            MapProperty(17, x => x.Y1891);
            MapProperty(18, x => x.Y1921);
            MapProperty(19, x => x.Y1933);
            MapProperty(20, x => x.Y1953);
            MapProperty(21, x => x.Y1969);
            MapProperty(22, x => x.Y1974);
            MapProperty(23, x => x.Y1981);
            MapProperty(24, x => x.Y1984);
            MapProperty(25, x => x.Y1990);
            MapProperty(26, x => x.Y1997);
            MapProperty(27, x => x.Post1997);
            MapProperty(28, x => x.Comments);
        }
    }
}
