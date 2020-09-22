using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BillboardAPI.Models;
using BillboardAPI.Repository;
using Microsoft.AspNetCore.Cors;

namespace BillboardAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/charts")]
    public class ChartsController : ControllerBase 
    {
        /// <summary>
        /// Gets billboard chart by name.
        /// </summary>
        /// <param name="name">Name of chart.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{name}")]
        public IEnumerable<ChartListItem> GetChart(string name)
        {
            return ChartRepository.GetChart(name);
        }

        /// <summary>
        /// Gets billboard chart by name and date.
        /// </summary>
        /// <param name="name">Name of chart.</param>
        /// <param name="date">Date of when chart was published.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{name}/{date?}")]
        public IEnumerable<ChartListItem> GetChart(string name, string date)
        {
            return ChartRepository.GetChart(name, date);
        }
    }
}