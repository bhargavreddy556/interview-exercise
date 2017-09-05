using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview_Exercise
{
  public class Validation
    {
        public List<string> Validate(Country country)
        {
            List<string> lst = new List<string>();
            var context = new ValidationContext(country, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();


            var isValid = Validator.TryValidateObject(country, context, results, validateAllProperties: true);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    lst.Add(validationResult.ErrorMessage);
                }
            }
            return lst;
        }
    }
}
