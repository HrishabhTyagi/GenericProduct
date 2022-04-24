using Common.RequestModel;
using Common.ResponseModel;
using DAL;
using SqlServerEntity.EntityModel;
using System.Collections.Generic;

namespace BAL
{
    public class CountryBusiness : BaseBusiness
    {
        private readonly CountryDataAccess countryDataAccess;

        public CountryBusiness()
        {
            countryDataAccess = new CountryDataAccess();
        }

        public List<CountryResponse> GetCountryList()
        {
            var countryList = countryDataAccess.GetCountryList();
            var countryListResponses = ListMapping<Country, CountryResponse>(countryList);
            return countryListResponses;
        }
        public CountryResponse GetCountry(int CountryId)
        {
            var countryList = countryDataAccess.GetCountry(CountryId);
            var countryListResponses = ObjectMapping<Country, CountryResponse>(countryList);
            return countryListResponses;
        }
    }
}
