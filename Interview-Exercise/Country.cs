using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Interview_Exercise
{
    public class Country
    //: IValidatableObject
    {
        private string countryName;

        [MaxLength(3, ErrorMessage = "Invalid Country code Length")]
        [RegularExpression("^(?!.*(AA[A-Z])|(aa[a-z])|(Q[M-Z][A-Z])|(q[m-z][a-z])|(X[A-Z][A-Z])|(x[a-z][a-z])|(ZZ[A-Z])|(zz[a-z])).*$", ErrorMessage = "Invalid Country Code Combination")]
        public string CountryCode { get; set; }

        public string CountryName
        {
            get
            {
                return countryName.ToUpper();

            }
            set
            {
                this.countryName = value.ToUpper();
            }
        }

        //Validate method
        public List<string> Validate()
        {
            List<string> lst = new List<string>();
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();


            var isValid = Validator.TryValidateObject(this, context, results, validateAllProperties: true);

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