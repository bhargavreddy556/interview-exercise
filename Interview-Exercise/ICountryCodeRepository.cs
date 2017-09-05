using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview_Exercise
{
   public interface ICountryCodeRepository
    {
        void Add(Country country);
        void Update(Country country);
        void Delete(string countryCode);
        Country Get(string countryCode);
        void Clear();
    }
}
