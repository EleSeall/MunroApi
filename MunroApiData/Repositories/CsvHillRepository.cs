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

        public Hill Get(int id)
        {
            var hill = _hills.FirstOrDefault(x => x.RunningNo == id);

            return hill ?? new Hill();
        }

        public IEnumerable<Hill> GetHills(HillSearch search)
        {
            var hills = _hills;

            if (!string.IsNullOrWhiteSpace(search.Category))
            {
                hills = _hills.Where(x => x.Post1997.ToUpper() == search.Category.ToUpper()).ToList();
            }

            //sorting

            return hills;
        }

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
