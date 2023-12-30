using JsonLoadTest.models;
using MathNet.Numerics.Statistics;
using System.Collections.Generic;
using System.Linq;

namespace JsonLoadTest
{
    public class SummaryCalculator
    {
        public SummaryCalculator() { }

        public static SummaryModel MakeSummary(string type, List<LoanModel> model)
        {
            var list = new List<double>();
            switch (type)
            {
                case ProjectConstants.LoanAmmountSummary:
                    list = model.Select(s => s.LoanAmount).ToList();
                    return new SummaryModel
                    {
                        Sum = list.Sum(),
                        Average = list.Average(),
                        Median = list.Median(),
                        Maximum = list.Maximum(),
                        Minimum = list.Minimum(),
                    };
                case ProjectConstants.SubjectAppraisedAmountSummary:
                    list = model.Select(s => s.SubjectAppraisedAmount).ToList();
                    return new SummaryModel
                    {
                        Sum = list.Sum(),
                        Average = list.Average(),
                        Median = list.Median(),
                        Maximum = list.Maximum(),
                        Minimum = list.Minimum(),
                    };
                case ProjectConstants.InterestRateSummary:
                    list = model.Select(s => s.InterestRate).ToList();
                    return new SummaryModel
                    {
                        Sum = list.Sum(),
                        Average = list.Average(),
                        Median = list.Median(),
                        Maximum = list.Maximum(),
                        Minimum = list.Minimum(),
                    };
                default:
                    return null;
            }
        }

    }
}
