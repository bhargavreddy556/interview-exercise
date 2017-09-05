using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview_Exercise
{
    public class CSVFileHandler
    {
        private string _dataPath
        {
            get; set;
        }
        public CSVFileHandler()
        {
            _dataPath = Path.Combine(
  System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
  "Interview-Exercise");
        }

        public void Add(Country country)
        {

            string filename = country.CountryCode.FirstOrDefault().ToString();

            using (var w = new StreamWriter(_dataPath + "\\" + filename + ".csv", true))
            {
                string countryCode = country.CountryCode;
                string countryName = country.CountryName;
                string line = string.Format("{0},{1}", countryCode, countryName);
                w.WriteLine(line);
                // w.Flush();
            }
        }

        public Country Get(string countryCode)
        {
            Country country = new Country();

            try
            {
                string filename = countryCode.FirstOrDefault().ToString();

                using (var sr = new StreamReader(new FileStream((_dataPath + "\\" + filename + ".csv"), FileMode.Open)))
                {
                    try
                    {
                        string[] vals;
                        var line = sr.ReadLine();
                        while (!sr.EndOfStream)
                        {
                            var nextLine = sr.ReadLine();
                            if (nextLine.Contains(countryCode))
                            {
                                line = nextLine;
                            }                            
                        }
                        vals = line.Split(',');
                        if (vals[0].Equals(countryCode))
                        {
                            country.CountryCode = vals[0];
                            country.CountryName = vals[1];
                        }
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                return new Country();
            }
            return country;
        }


        public void Update(Country country)
        {
            string filename = country.CountryCode.FirstOrDefault().ToString();

            List<string> lines = new List<string>();

            using (var sr = new StreamReader(new FileStream((_dataPath + "\\" + filename + ".csv"), FileMode.Open)))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    lines.Add(line);
                }
            }

            lines = lines.ToList().Select(i => (i.Split(',')[0].Contains(country.CountryCode) == true) ? i.Replace(i.Split(',')[1], country.CountryName) : i.Replace(i.Split(',')[1], i.Split(',')[1])).ToList();

            using (var w = new StreamWriter(_dataPath + "\\" + filename + ".csv", false))
            {
                foreach (string line in lines)
                {
                    w.WriteLine(line);
                }
                w.Flush();
            }
        }

        public void Delete(string countryCode)
        {
            string filename = countryCode.FirstOrDefault().ToString();

            List<string> lines = new List<string>();

            using (var sr = new StreamReader(new FileStream((_dataPath + "\\" + filename + ".csv"), FileMode.Open)))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    lines.Add(line);
                }
            }

            lines = lines.ToList().Where(i => !(i.Split(',')[0].Contains(countryCode))).Select(i => i).ToList();

            using (var w = new StreamWriter(_dataPath + "\\" + filename + ".csv", false))
            {
                foreach (string line in lines)
                {
                    w.WriteLine(line);
                }
                w.Flush();
            }
        }

        public void Clear()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(_dataPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

        }
    }
}
