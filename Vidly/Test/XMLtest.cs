using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Vidly.Test
{
    public class XmLtest
    {

        private static void ThirdApproach()
        {
            int sumLoan = 0, sumIntrest = 0;
            string xml = @"<loans>
                            <loan>
                                  <customerId>001</customerId>
                                  <loanAmount>250</loanAmount>
                                  <intrestRate>5</intrestRate>
                               </loan>
                               <loan>
                                  <customerId>25</customerId>
                                  <loanAmount>420</loanAmount>
                                  <intrestRate>10</intrestRate>
                               </loan>
                               <loan>
                                  <customerId>30</customerId>
                                  <loanAmount>5000</loanAmount>
                                  <intrestRate>3</intrestRate>
                               </loan>
                            </loans>";

            var xElement = XElement.Parse(xml);
            // Add Application Config file and also add namespace System.Configurations
            //<add key="xmlPath" value="C:\Temp\loans.xml"/>
            List<Customer> customers = (from message in xElement.Elements("loan")
                                        select new Customer()
                                        {
                                            CustomerId = message.Element("customerId").Value,
                                            LoanAmount = message.Element("loanAmount").Value,
                                            IntrestRate = message.Element("intrestRate").Value
                                        }).ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine("CustomerId = {0}, LoanAmoutn= {1}, IntrestRate={2}", customer.CustomerId, customer.LoanAmount, customer.IntrestRate);
                sumLoan += int.Parse(customer.LoanAmount);
                sumIntrest += int.Parse(customer.IntrestRate);
            }
            Console.WriteLine("Toltal Loan Amount:{0}, Total Intrest Rates:{1}", sumLoan, sumIntrest);
        }
        class Customer
        {
            public string CustomerId { get; set; }
            public string LoanAmount { get; set; }
            public string IntrestRate { get; set; }
        }

        public static void SecondApproach()
        {
            string xml = @" < messages> 
                  <message subclass=""a"" context=""d"" key=""g""/> 
                  <message subclass=""b"" context=""e"" key=""h""/> 
                  <message subclass=""c"" context=""f"" key=""i""/> 
               </messages>";

            var messagesElement = XElement.Parse(xml);
            var messagesList = (from message in messagesElement.Elements("message")
                                select new
                                {
                                    Subclass = message.Attribute("subclass").Value,
                                    Context = message.Attribute("context").Value,
                                    Key = message.Attribute("key").Value
                                }).ToList();

            foreach (var message in messagesList)
            {
                Console.WriteLine(message);
            }

        }


        public static void FirstApproach()
        {
            string xml =
                           "<Root>" +
                           "<level1 title=\"Personal Banking\" controller=\"PersonalBanking\" description=\"Personal Banking\"  MainNavOrder=\"1\">" +
                               "<level2 title=\"Personal Deposits\" controller=\"PersonalBanking\" action=\"PersonalDeposits\" MainNavOrder=\"10\">" +
                                 "<level3 title=\"Checking Accounts\" controller=\"PersonalBanking\" action=\"PersonalDeposits\" page=\"CheckingAccounts\"/>" +
                                 "<level3 title=\"Savings Accounts\" controller=\"PersonalBanking\" action=\"PersonalDeposits\" page=\"SavingsAccounts\"/>" +
                                 "<level3 title=\"Certificates Of Deposit\" controller=\"PersonalBanking\" action=\"PersonalDeposits\" page=\"CertificatesOfDeposit\"/>" +
                               "</level2>" +
                               "<level2 title=\"Compare Current Rates\" controller=\"PersonalBanking\" action=\"Rates\" MainNavOrder=\"20\">" +
                                 "<level3 title=\"Checking Rates\" controller=\"PersonalBanking\" action=\"Rates\" page=\"CheckingAccounts\"/>" +
                               "</level2>" +
                               "<level2 title=\"Consumer Lending\" controller=\"PersonalBanking\" action=\"ConsumerLending\" MainNavOrder=\"30\">" +
                                 "<level3 title=\"Auto Loans\" controller=\"PersonalBanking\" action=\"ConsumerLending\" page=\"AutoLoans\"/>" +
                                 "<level3 title=\"Recreational Loans\" controller=\"PersonalBanking\" action=\"ConsumerLending\" page=\"RecreationalLoans\"/>" +
                                 "<level3 title=\"Home Equity Loans\" controller=\"PersonalBanking\" action=\"ConsumerLending\" page=\"HomeEquityLoans\"/>" +
                                 "<level3 title=\"Other Loans\" controller=\"PersonalBanking\" action=\"ConsumerLending\" page=\"OtherLoans\"/>" +
                               "</level2>" +
                               "<level2 title=\"Personal Services\" controller=\"PersonalBanking\" action=\"PersonalServices\"  MainNavOrder=\"40\">" +
                                 "<level3 title=\"ATM &amp; Debit Cards\" controller=\"PersonalBanking\" action=\"PersonalServices\" page=\"ATMDebitCards\"/>" +
                                 "<level3 title=\"Online Banking &amp; Bill Payment\" controller=\"PersonalBanking\" action=\"PersonalServices\" page=\"OnlineBankingBillPayment\"/>" +
                                 "<level3 title=\"Overdraft Protection\" controller=\"PersonalBanking\" action=\"PersonalServices\" page=\"OverdraftProtection\"/>" +
                               "</level2>" +
                               "<level2 title=\"Education Center\" controller=\"EducationCenter\" MainNavOrder=\"50\"/>" +
                             "</level1>" +
                             "</Root>";


            XElement sitemapData = XElement.Parse(xml);

            var pages = sitemapData.Descendants("level1")
                        .Select(p1 => SitemapNode.GetNode(p1, 1)
            );
            List<SitemapNode> sitemap = new List<SitemapNode>();
            sitemap.AddRange(pages.ToList<SitemapNode>());

            foreach (var node in sitemap)
            {
                Console.WriteLine(node.Title);
            }

        }

        public class SitemapNode
        {
            public string Title { get; set; }
            public string Action { get; set; }
            public string Controller { get; set; }
            public string Page { get; set; }
            public string Description { get; set; }
            public bool Blank { get; set; }
            public int Level { get; set; }
            public int? MainNavOrder { get; set; }
            public string[] Sections { get; set; }
            public SitemapNode[] SubNodes { get; set; }

            public static SitemapNode GetNode(XElement p3, int level)
            {
                return new SitemapNode
                {
                    Title = (string)p3.Attribute("title"),
                    Action = (string)p3.Attribute("action") ?? "Index",
                    Controller = (string)p3.Attribute("controller"),
                    Page = (string)p3.Attribute("page") ?? "",
                    Description = (string)p3.Attribute("description") ?? "",
                    Blank = bool.Parse((string)p3.Attribute("blank") ?? "false"),
                    Level = p3.Ancestors().Count(),
                    MainNavOrder = (int?)p3.Attribute("MainNavOrder") ?? 0,
                    Sections = p3.Elements("alternativeSection")
                                .Select(n => (string)n.Attribute("name")).ToArray(),
                    SubNodes = p3.Elements("level" + (level + 1).ToString()).Select(pn => GetNode(pn, level + 1)).ToArray()
                };

            }
        }

    }
}