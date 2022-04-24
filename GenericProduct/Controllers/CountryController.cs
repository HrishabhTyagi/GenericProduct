using BAL;
using Common.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MigrationToolApi.Filters;

namespace MigrationToolApi.Controllers
{
    public class CountryController : AdminBaseController
    {
        private readonly CountryBusiness countryBusiness;

        public CountryController()
        {
            countryBusiness = new CountryBusiness();
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<CountryResponse>> GetCountry()
        {
            return Ok(countryBusiness.GetCountryList());
        }
    }
}
