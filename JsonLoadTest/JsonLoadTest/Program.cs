using JsonLoadTest.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JsonLoadTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Importing file!");

            if (FileIO.FileExists(ProjectConstants.LoanFileName))
            {
                var loans = FileIO.GetListOfLoans(ProjectConstants.LoanFileName);

                Console.WriteLine("File imported successfully!");

                if (loans.Count > 0)
                {
                    Console.WriteLine("Generating monthly statistic file");

                    if (MakeMonthlyJsonFile(loans))
                    {
                        Console.WriteLine("Monthly statistic file created!");
                    }
                    else
                    {
                        Console.WriteLine("A monthly statistic file was not created at this time!");
                    }

                    Console.WriteLine("Generating monthly by state statistic file");

                    if (MakeMonthlyByStateJsonFile(loans))
                    {
                        Console.WriteLine("Monthly statistic by state file created!");
                    }
                    else
                    {
                        Console.WriteLine("A monthly statistics by state file was not created at this time!");
                    }
                }
                else
                {
                    Console.WriteLine("No loans to calculate!");
                }

            }
            else
            {
                Console.WriteLine("No file to import!");
            }
        }
        
        private static bool MakeMonthlyJsonFile(List<LoanModel> loans)
        {
            var monthlySummary = new StatisticSummaryModel
            {
                LoanAmountSummary = SummaryCalculator.MakeSummary(ProjectConstants.LoanAmmountSummary, loans),
                SubjectAppraisedAmountSummary = SummaryCalculator.MakeSummary(ProjectConstants.SubjectAppraisedAmountSummary, loans),
                InterestRateSummary = SummaryCalculator.MakeSummary(ProjectConstants.InterestRateSummary, loans),
            };

            if (FileIO.FileExists(ProjectConstants.MonthlyJsonFileName))
            {
                FileIO.Delete(ProjectConstants.MonthlyJsonFileName);
            }

            return FileIO.WriteJsonToFile(ProjectConstants.MonthlyJsonFileName, monthlySummary);
        }

        private static bool MakeMonthlyByStateJsonFile(List<LoanModel> loans) 
        {
            var states = loans.Select(s => s.SubjectState).Distinct().ToList();

            var summary = states.Select(s => new StateSummaryModel 
            { 
               State = s,
               Statistic = MakeStateLoanList(s, loans)
            });


            if (FileIO.FileExists(ProjectConstants.MonthlyByStateJsonFileName))
            {
                FileIO.Delete(ProjectConstants.MonthlyByStateJsonFileName);
            }

            return FileIO.WriteJsonToFile(ProjectConstants.MonthlyByStateJsonFileName, summary);

        }

        private static StatisticSummaryModel MakeStateLoanList(string state, List<LoanModel> loans)
        {
            var stateLoans = loans.Where(l => l.SubjectState.Equals(state)).ToList();

            return new StatisticSummaryModel
            {
                LoanAmountSummary = SummaryCalculator.MakeSummary(ProjectConstants.LoanAmmountSummary, stateLoans),
                SubjectAppraisedAmountSummary = SummaryCalculator.MakeSummary(ProjectConstants.SubjectAppraisedAmountSummary, stateLoans),
                InterestRateSummary = SummaryCalculator.MakeSummary(ProjectConstants.InterestRateSummary, stateLoans),
            };
        }
    }
}
