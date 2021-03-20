using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunroApiData
{
    public interface IHillRepository
    {

        public Hill Get(int id);

        public IEnumerable<Hill> GetHills(HillSearch search);
    }
}
