using MunroApiData.ParserMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCsvParser;

namespace MunroApiData.Repositories
{
    public class CsvHillRepository : IHillRepository, IDisposable
    {
        private IEnumerable<Hill> _hills;

        public CsvHillRepository(string filePath)
        {
            LoadFromFile(filePath);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Clean-up
            // if we had anything - csvparser doesn't appear to have any dispose or close options
        }

        /// <summary>
        /// Retrieve hill by running number
        /// </summary>
        /// <param name="id">running number</param>
        /// <returns></returns>
        public Hill Get(int id)
        {
            var hill = _hills.FirstOrDefault(x => x.RunningNo == id);

            return hill ?? new Hill();
        }

        /// <summary>
        /// Get hill where name exactly matches given parameter
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Hill Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new Hill();
            }

            var hill = _hills.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper());

            return hill ?? new Hill();
        }

        /// <summary>
        /// Get hills matching search criteria
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<Hill> GetHills(HillSearch search)
        {
            var hills = _hills;

            if (!string.IsNullOrWhiteSpace(search.Category))
            {
                hills = hills.Where(x => x.Post1997.ToUpper() == search.Category.ToUpper()).ToList();
            }

            if (search.MinHeight > 0)
            {
                hills = hills.Where(x => x.HeightM >= search.MinHeight).ToList();
            }

            if (search.MaxHeight > 0)
            {
                hills = hills.Where(x => x.HeightM <= search.MaxHeight).ToList();
            }

            //sorting by name or height(m)
            if (search.SortBy != SortBy.None)
            {
                hills = hills.ToList();

                switch(search.SortBy)
                {
                    case SortBy.Name:
                        hills = hills.ToList().OrderBy(x => x.Name);
                        break;
                    case SortBy.Height:
                        hills.ToList().Sort((x, y) => decimal.Compare(x.HeightM, y.HeightM));
                        break;
                }   
            }

            //sort direction
            if (search.SortDirection == SortDirection.Desc)
            {
                hills = hills.Reverse();
            }

            //take specifies a limit to the results
            if (search.Take > 0)
            {
                hills = hills.Take(search.Take);
            }

            return hills;
        }

        /// <summary>
        /// Parse data from the given csv filepath into a list of hills, 
        /// which can then be retrieved using the public methods
        /// </summary>
        /// <param name="filePath"></param>
        private void LoadFromFile(string filePath)
        {
            var hills = new List<Hill>();

            //set up parser options - is there a header row, and the separator character
            var csvParserOptions = new CsvParserOptions(true, ',');

            //set up field mappings
            var csvMapper = new CsvHillMapping();

            //create a csv parser
            var csvParser = new CsvParser<Hill>(csvParserOptions, csvMapper);

            //load data from csv
            var results = csvParser.ReadFromFile(filePath, Encoding.ASCII).ToList();

            foreach (var details in results)
            {
                //check for invalid rows and discard
                if (details.Result != null)
                { 
                    hills.Add(details.Result);
                }
            }

            _hills = hills;
        }

    }
}
