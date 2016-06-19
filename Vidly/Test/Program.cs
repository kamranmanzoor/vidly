using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Configuration;

namespace LoanStats
{
    public class Program
    {
        #region Local Variables
        static string _xmlFileLocation = ConfigurationManager.AppSettings["xmlPath"];
        static string _csvFileLocation = ConfigurationManager.AppSettings["csvPath"];
        #endregion

        #region Main Method
        static void Main(string[] args)
        {
            DisplayData(LoadXml(), "XML Source");
            DisplayData(LoadCsv(), "CSV Source");
            Console.ReadKey();
        }
        #endregion

        #region Load XML Data
        private static IEnumerable<Customer> LoadXml()
        {
            IEnumerable<Customer> customers = null;
            XmlDocument doc = new XmlDocument();

            if (File.Exists(_xmlFileLocation))
            {
                doc.Load(_xmlFileLocation);
                string xml = doc.InnerXml;

                var xElement = XElement.Parse(xml);

                customers = (from message in xElement.Elements("loan")
                             select new Customer()
                             {
                                 CustomerId = int.Parse(message.Element("customerid").Value),
                                 LoanAmount = decimal.Parse(message.Element("amount").Value),
                                 IntrestRate = decimal.Parse(message.Element("interestrate").Value)
                             }).ToList();
            }
            else
            {
                Console.WriteLine("Invalid file path specified for XML file");
            }
            return customers;
        }
        #endregion

        #region Load CSV Data
        private static IEnumerable<Customer> LoadCsv()
        {
            IEnumerable<Customer> customers = null;

            if (File.Exists(_csvFileLocation))
            {
                customers = File.ReadAllLines(_csvFileLocation)
                                               .Skip(1)
                                               .Select(v => SplitData(v))
                                               .ToList();
            }
            else
            {
                Console.WriteLine("Invalid file path specified for CSV file");
            }
            return customers;
        }
        #endregion

        #region Display Loan Stats
        private static void DisplayData(IEnumerable<Customer> customers, string source)
        {
            decimal totalAmountLent = 0m, expectedReturn = 0m;

            Console.WriteLine("\n*******  Loan Stats from {0} Source  *******\n", source);
            foreach (var customer in customers)
            {
                Console.WriteLine("CustomerId = {0}, LoanAmoutn= {1}, IntrestRate={2}", customer.CustomerId, customer.LoanAmount, customer.IntrestRate);
                totalAmountLent += customer.LoanAmount;
                expectedReturn += (customer.LoanAmount) * (customer.IntrestRate / 100);
            }
            Console.WriteLine("\nToltal Loan Amount:{0:c}, Total Expected Return:{1:c}", totalAmountLent, expectedReturn + totalAmountLent);
        } 
        #endregion
        
        #region Helper Method SplitData
        private static Customer SplitData(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Customer dailyValues = new Customer();
            dailyValues.CustomerId = int.Parse(values[0]);
            dailyValues.LoanAmount = decimal.Parse(values[1]);
            dailyValues.IntrestRate = decimal.Parse(values[2]);
            return dailyValues;
        }
        #endregion
    }
}
