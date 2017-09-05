using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interview_Exercise;
using NUnit.Framework;
using Rhino.Mocks;

namespace Interview_Exercise_Tests.UnitTests
{
    public class CountryUnitTests
    {

        //Test case for - Codes should be a 3-character string of letters (e.g. USA, CAN, MEX).
        public class CountryCodeMaxLengthTestFail : TestFixtureBase
        {
            private bool _actualResult;

            protected override void Act()
            {
                Country mCountry = new Country();
                mCountry.CountryCode = "AAAA";
                mCountry.CountryName = "AAAA";
                List<string> mResults = mCountry.Validate();
                if (mResults.Contains("Invalid Country code Length"))
                {
                    _actualResult = false;
                }
            }

            [Test]
            public void CountryCodeMaxLength_Fail()
            {
                Assert.That(_actualResult, Is.False);
            }
        }

        //Test case for - Codes should be a 3-character string of letters (e.g. USA, CAN, MEX).
        public class CountryCodeMaxLengthTestSuccess : TestFixtureBase
        {
            private bool _actualResult;

            protected override void Act()
            {
                Country mCountry = new Country();
                mCountry.CountryCode = "AAA";
                mCountry.CountryName = "AAA";
                List<string> mResults = mCountry.Validate();
                if (!(mResults.Contains("Invalid Country code Length")))
                {
                    _actualResult = true;
                }
            }

            [Test]
            public void CountryCodeMaxLength_Success()
            {
                Assert.That(_actualResult, Is.True);
            }
        }

        //Test case for - User-assigned codes cannot be added to the repository. User-assigned codes are defined by the following ranges: AAA to AAZ, QMA to QZZ, XAA to XZZ, and ZZA to ZZZ
        public class CountryCodeWhenNotStringFail : TestFixtureBase
        {
            private bool _actualResult;

            protected override void Act()
            {
                Country mCountry = new Country();
                mCountry.CountryCode = "123";
                mCountry.CountryName = "123";
                List<string> mResults = mCountry.Validate();
                if (mResults.Contains("Invalid Country Code Combination"))
                {
                    _actualResult = false;
                }
            }

            [Test]
            public void CountryCodeWhenNotString_Fail()
            {
                Assert.That(_actualResult, Is.False);
            }
        }

        //Test case for - country codes can be anything other than User-assigned codes.
        public class CountryCodeWhenStringSuccess : TestFixtureBase
        {
            private bool _actualResult;

            protected override void Act()
            {
                Country mCountry = new Country();
                mCountry.CountryCode = "USA";
                mCountry.CountryName = "USA";
                List<string> mResults = mCountry.Validate();
                if (!(mResults.Contains("Invalid Country Code Combination")))
                {
                    _actualResult = true;
                }
            }

            [Test]
            public void CountryCodeWhenString_Success()
            {
                Assert.That(_actualResult, Is.True);
            }
        }

        //Test case for Constraint - Codes should be unique across all Countries in the repository.
        public class CountryCodeDuplicateFail : TestFixtureBase
        {
            protected override void Act()
            {
                Country mCountry = new Country();
                mCountry.CountryCode = "USA";
                mCountry.CountryName = "USA";

                CSVFileHandler cSVFileHandler = new CSVFileHandler();
                CountryCodeRepository mCountryCodeRepository = new CountryCodeRepository(cSVFileHandler);

                //Clearing existing files
                mCountryCodeRepository.Clear();

                //Adding USA first time
                mCountryCodeRepository.Add(mCountry);

                //Adding USA second time
                mCountryCodeRepository.Add(mCountry);

            }

            [Test]
            public void CountryCodeDuplicate_Fail()
            {
                Assert.That(ActualException.Message, Is.EqualTo("Already exists with this code"));
            }
        }

        //Test case for - The repository should immediately save changes to disk in a text file using a CSV (Comma Separated Values) format.
        public class CountryCodeAddSuccess : TestFixtureBase
        {
            private string _actualFirstGetResult;

            protected override void Act()
            {
                Country mCountry = new Country();
                mCountry.CountryCode = "IND";
                mCountry.CountryName = "IND";

                CSVFileHandler cSVFileHandler = new CSVFileHandler();
                CountryCodeRepository mCountryCodeRepository = new CountryCodeRepository(cSVFileHandler);
                
                //Clearing existing files
                mCountryCodeRepository.Clear();

                //Adding USA first time
                mCountryCodeRepository.Add(mCountry);
                _actualFirstGetResult = mCountryCodeRepository.Get("IND").CountryName;
            }

            [Test]
            public void CountryCodeAdd_Success()
            {
                Assert.That(_actualFirstGetResult, Is.EqualTo("IND"));
            }
        }

        //Test case for Repository update
        public class CountryCodeUpdateFail : TestFixtureBase
        {
            protected override void Act()
            {
                Country mCountry = new Country();
                mCountry.CountryCode = "UFA";
                mCountry.CountryName = "UFA";

                CSVFileHandler cSVFileHandler = new CSVFileHandler();
                CountryCodeRepository mCountryCodeRepository = new CountryCodeRepository(cSVFileHandler);

                //Clearing existing files
                mCountryCodeRepository.Clear();

                //Trying to update invalid country.
                mCountryCodeRepository.Update(mCountry);

            }

            [Test]
            public void CountryCodeUpdate_Fail()
            {
                Assert.That(ActualException.Message, Is.EqualTo("Country code not found"));
            }
        }

        //Test case for Repository update
        public class CountryCodeUpdateSuccess : TestFixtureBase
        {
            private string _actualResult;
            protected override void Act()
            {
                Country mCountry = new Country();
                mCountry.CountryCode = "USA";
                mCountry.CountryName = "USA";

                CSVFileHandler cSVFileHandler = new CSVFileHandler();
                CountryCodeRepository mCountryCodeRepository = new CountryCodeRepository(cSVFileHandler);

                //Clearing existing files
                mCountryCodeRepository.Clear();

                //Adding country code
                mCountryCodeRepository.Add(mCountry);

                //Trying to update USA country's name to USS.
                mCountry.CountryCode = "USA";
                mCountry.CountryName = "USS";
                mCountryCodeRepository.Update(mCountry);

                _actualResult = mCountryCodeRepository.Get("USS").CountryName;

            }

            [Test]
            public void CountryCodeUpdate_Success()
            {
                Assert.That(_actualResult, Is.EqualTo("USS"));
            }
        }

        //Test case for Repository delete
        public class CountryCodeDeleteFail : TestFixtureBase
        {
            protected override void Act()
            {
                CSVFileHandler cSVFileHandler = new CSVFileHandler();
                CountryCodeRepository mCountryCodeRepository = new CountryCodeRepository(cSVFileHandler);

                //Clearing existing files
                mCountryCodeRepository.Clear();

                //Trying to delete non existent country.
                mCountryCodeRepository.Delete("UNL");
            }

            [Test]
            public void CountryCodeDelete_Fail()
            {
                Assert.That(ActualException.Message, Is.EqualTo("Code doesnt exist"));
            }
        }

        //Test case for Repository delete
        public class CountryCodeDeleteSuccess : TestFixtureBase
        {
            private string _actualFirstGetResult;
            protected override void Act()
            {
                Country mCountry = new Country();
                mCountry.CountryCode = "IND";
                mCountry.CountryName = "IND";

                CSVFileHandler cSVFileHandler = new CSVFileHandler();
                CountryCodeRepository mCountryCodeRepository = new CountryCodeRepository(cSVFileHandler);

                //Clearing existing files
                mCountryCodeRepository.Clear();

                mCountryCodeRepository.Add(mCountry);

                _actualFirstGetResult = mCountryCodeRepository.Get("IND").CountryName;

                //Trying to delete non existent country.
                mCountryCodeRepository.Delete("IND");

                //Read deleted and non existent country
                mCountry = mCountryCodeRepository.Get("IND");
            }

            [Test]
            public void CountryCodeDelete_Success()
            {
                AssertAll(() => Assert.That(_actualFirstGetResult, Is.EqualTo("IND")), () => Assert.That(ActualException.Message, Is.EqualTo("code doesnt exist ")));
            }
        }

        //Test case for - Codes should be handled in a case-insensitive fashion, but be stored and return as upper-case.
        public class CountryCodeUpperCase : TestFixtureBase
        {
            private string _actualFirstGetResult;
            protected override void Act()
            {
                Country mCountry = new Country();
                mCountry.CountryCode = "ind";
                mCountry.CountryName = "ind";

                CSVFileHandler cSVFileHandler = new CSVFileHandler();
                CountryCodeRepository mCountryCodeRepository = new CountryCodeRepository(cSVFileHandler);

                //Clearing existing files
                mCountryCodeRepository.Clear();

                mCountryCodeRepository.Add(mCountry);

                _actualFirstGetResult = mCountryCodeRepository.Get("IND").CountryName;

            }

            [Test]
            public void CountryCodeUpperCase_Success()
            {
                Assert.That(_actualFirstGetResult, Is.EqualTo("IND"));
            }
        }

    }
}
