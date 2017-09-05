using Interview_Exercise;
using System;

namespace Interview_Exercise
{
    public class CountryCodeRepository : ICountryCodeRepository
    {
        // just injecting NO DI
        private Validation _validation;
        private CSVFileHandler _cSVFileHandler;
        public CountryCodeRepository(CSVFileHandler cSVFileHandler)
        {
            _cSVFileHandler = cSVFileHandler;
             _validation = new Validation();
        }
        public void Add(Country country)
        {
            try
            {
                if (_validation.Validate(country).Count > 0)
                {
                    // write proper hanlder data
                    throw new Exception("invalid data");
                }
                // check if already exists
                if (_cSVFileHandler.Get(country.CountryCode).CountryCode == country.CountryCode)
                {
                    throw new Exception("Already exists with this code");
                }
                _cSVFileHandler.Add(country);
            }
            catch (Exception e)
            {
                throw;
                //or log whatever
            }
        }

        public void Clear()
        {
            _cSVFileHandler.Clear();
        }

        public void Delete(string countryCode)
        {
            try
            {
                if (_cSVFileHandler.Get(countryCode).CountryCode != countryCode)
                {
                    throw new Exception("Code doesnt exist");
                }
                _cSVFileHandler.Delete(countryCode);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Country Get(string countryCode)
        {
            try
            {
                var country = _cSVFileHandler.Get(countryCode);
                if (null == country)
                { throw new Exception("code doesnt exist "); }
                return country;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Update(Country country)
        {
            try
            {
                if (_cSVFileHandler.Get(country.CountryCode).CountryCode != country.CountryCode)
                {
                    throw new Exception("Country code not found");
                }
                _cSVFileHandler.Update(country);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
