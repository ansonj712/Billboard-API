using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BillboardAPI.Models;
using BillboardAPI.Repository;

namespace BillboardAPI.Controllers
{
    [ApiController]
    [Route("api/charts")]
    public class ChartsController : ControllerBase 
    {
        [HttpGet]
        [Route("{name}/{date?}")]
        public IEnumerable<ChartListItem> GetChart(string name, string date)
        {
            return ChartRepository.GetChart(name, date);
        }
    }
}