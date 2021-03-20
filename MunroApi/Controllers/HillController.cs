using MunroApiData;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MunroApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HillController : ControllerBase
    {
        private readonly IHillRepository HillRepository;

        public HillController(IHillRepository hillRepository)
        {
            HillRepository = hillRepository;
        }

        //[HttpGet("getbyname")]
        //public Hill GetByName(string name)
        //{
        //    return HillRepository.Get(name);
        //}

        /// <summary>
        /// Get filtered and sorted hill results
        /// </summary>
        /// <param name="category">MUN or TOP</param>
        /// <param name="minHeight">minimum height</param>
        /// <param name="maxHeight">maximum height</param>
        /// <param name="take">number of results</param>
        /// <param name="sortBy">NAME or HEIGHT</param>
        /// <param name="sortDirection">ASC or DESC</param>
        /// <returns></returns>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Hill>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get(string category, int? minHeight, int? maxHeight, int? take, string sortBy, string sortDirection)
        {
            var hillSearch = new HillSearch
            {
                Category = category,
                MinHeight = minHeight ?? 0,
                MaxHeight = maxHeight ?? 0,
                Take = take ?? 0,
                SortBy = sortBy,
                SortDirection = sortDirection
            };

            //do some sanity checks on the passed filters
            var error = hillSearch.Check();

            if (!string.IsNullOrWhiteSpace(error))
            {
                return BadRequest(error);
            }

            return Ok(HillRepository.GetHills(hillSearch));
        }
    }
}
