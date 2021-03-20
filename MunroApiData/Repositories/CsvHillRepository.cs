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
        private IEnumerable<CsvHill> _hills;

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

            if (hill != null)
            {
                //could use something like automapper here
                return new Hill
                {
                    Name = hill.Name,
                    Height = hill.HeightM,
                    Category = hill.Post1997,
                    GridReference = hill.GridRef,
                };
            }

            //send a default/blank object back instead
            return new Hill();
        }

        /// <summary>
        /// Get hills matching search criteria
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<Hill> GetHills(HillSearch search)
        {
            if (string.IsNullOrWhiteSpace(search.SortDirection) || search.SortDirection.ToUpper() != "DESC")
            {
                //default sort ascending
                search.SortDirection = "ASC";
            }

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
            if (!string.IsNullOrWhiteSpace(search.SortBy))
            {
                hills = hills.ToList();

                switch(search.SortBy.ToUpper())
                {
                    case "NAME":
                        hills = hills.ToList().OrderBy(x => x.Name);
                        break;
                    case "HEIGHT":
                        hills = hills.ToList().OrderBy(x => x.HeightM);
                        break;
                }   
            }

            //sort direction
            if (search.SortDirection.ToUpper() == "DESC")
            {
                hills = hills.Reverse();
            }

            //take specifies a limit to the results
            if (search.Take > 0)
            {
                hills = hills.Take(search.Take);
            }

            var results = new List<Hill>();

            foreach(var hill in hills)
            {
                //could use something like automapper here
                results.Add(new Hill
                {
                    Name = hill.Name,
                    Height = hill.HeightM,
                    Category = hill.Post1997,
                    GridReference = hill.GridRef,
                });
            }

            return results;
        }

        /// <summary>
        /// Parse data from the given csv filepath into a list of hills, 
        /// which can then be retrieved using the public methods
        /// </summary>
        /// <param name="filePath"></param>
        private void LoadFromFile(string filePath)
        {
            var hills = new List<CsvHill>();

            //set up parser options - is there a header row, and the separator character
            var csvParserOptions = new CsvParserOptions(true, ',');

            //set up field mappings
            var csvMapper = new CsvHillMapping();

            //create a csv parser
            var csvParser = new CsvParser<CsvHill>(csvParserOptions, csvMapper);

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
