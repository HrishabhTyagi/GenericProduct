using SqlServerEntity.EntityModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class CountryDataAccess : BaseDataAccess
    {
        /// <summary>
        /// Fetch all Country list
        /// </summary>
        /// <returns></returns>
        public List<Country> GetCountryList()
        {

            List<Country> Countries = cache.GetValue<List<Country>>(CacheKeys.Country);

            if (Countries != null && Countries.Count > 0)
                return Countries;

            Countries = _context.Countries.ToList();
            cache.Add(CacheKeys.Country, Countries);
            return Countries;
        }

        /// <summary>
        /// Get specific country by id
        /// </summary>
        /// <returns></returns>
        public Country GetCountry(int CountryId)
        {
            List<Country> Countries = cache.GetValue<List<Country>>(CacheKeys.Country);

            if (Countries != null && Countries.Count > 0)
                return Countries.FirstOrDefault(iv => iv.CountryId == CountryId);

            Countries = _context.Countries.ToList();
            cache.Add(BaseDataAccess.CacheKeys.Country, Countries);
            return _context.Countries.FirstOrDefault(Country => Country.CountryId == CountryId);
        }
    }
}
