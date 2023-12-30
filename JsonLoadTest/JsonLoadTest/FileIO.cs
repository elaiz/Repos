using JsonLoadTest.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JsonLoadTest
{
    public class FileIO
    {
        public static bool FileExists(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                    return true;
                else 
                    return false;
            }
            catch (Exception ex)
            {
                // log exception
                return false;
            }
        }

        public static List<LoanModel> GetListOfLoans(string fileName)
        {
            try
            {
                using (var reader = new StreamReader(fileName))
                {
                    var jsonFileString = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<LoanModel>>(jsonFileString);
                }
            }
            catch (Exception)
            {
                // log exception
                return null;
            }
        }

        public static void Delete(string _FileName)
        {
            try
            {
                File.Delete(_FileName);
            }
            catch (IOException)
            {
                // log exception
            }
            catch (Exception)
            {
                // log exception
            }
        }

        public static bool WriteJsonToFile(string fileName, string json)
        {
            try
            {
                using (var writer = new StreamWriter(fileName)) { writer.Write(json); }

                return true;
            }
            catch (Exception)
            {
                // log exception
                return false;
            }
        }

        public static bool WriteJsonToFile(string fileName, StatisticSummaryModel statisticSummary)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("{");
                sb.AppendLine(WriteSummaryStatistics(statisticSummary));
                sb.AppendLine("}");

                return WriteJsonToFile(fileName, sb.ToString());
            }
            catch (Exception)
            {
                // log error
                return false;
            }
        }

        public static bool WriteJsonToFile(string fileName, IEnumerable<StateSummaryModel> stateSummaryList)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("{");

                foreach (var summary in stateSummaryList)
                {
                    sb.AppendLine(string.Concat("\t\"", summary.State, "\": {"));
                    sb.Append(WriteSummaryStatistics(summary.Statistic));
                    sb.AppendLine("\t},");
                }

                sb.AppendLine("}");
                var json = sb.ToString();
                var lastComma = json.LastIndexOf(',');

                return WriteJsonToFile(fileName, json.Remove(lastComma, 1));
            }
            catch (Exception)
            {
                // log exception
                return false;
            }
        }

        public static string WriteSummaryStatistics(StatisticSummaryModel summary)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Concat("\t\t\"", "LoanAmountSummary", "\": {"));
            sb.AppendLine(string.Concat("\t\t\t\"", "Sum", "\": ", " ", Math.Round(summary.LoanAmountSummary.Sum, 2).ToString("0.00"), ","));
            sb.AppendLine(string.Concat("\t\t\t\"", "Average", "\": ", " ",Math.Round(summary.LoanAmountSummary.Average, 2).ToString("0.00"), ","));
            sb.AppendLine(string.Concat("\t\t\t\"", "Median", "\": ", " ",Math.Round(summary.LoanAmountSummary.Median, 2).ToString("0.00"), ","));
            sb.AppendLine(string.Concat("\t\t\t\"", "Minimum", "\": ", " ",Math.Round(summary.LoanAmountSummary.Minimum, 2).ToString("0.00"), ","));
            sb.AppendLine(string.Concat("\t\t\t\"", "Maximum", "\": ", " ",Math.Round(summary.LoanAmountSummary.Maximum, 2).ToString("0.00")));
            sb.AppendLine("\t\t},");
            sb.AppendLine(string.Concat("\t\t\"", "SubjectAppraisedAmountSummary", "\": {"));
            sb.AppendLine(string.Concat("\t\t\t\"", "Sum", "\": ", " ",Math.Round(summary.SubjectAppraisedAmountSummary.Sum, 2).ToString("0.00"), ","));
            sb.AppendLine(string.Concat("\t\t\t\"", "Average", "\": ", " ",Math.Round(summary.SubjectAppraisedAmountSummary.Average, 2).ToString("0.00"), ","));
            sb.AppendLine(string.Concat("\t\t\t\"", "Median", "\": ", " ",Math.Round(summary.SubjectAppraisedAmountSummary.Median, 2).ToString("0.00"), ","));
            sb.AppendLine(string.Concat("\t\t\t\"", "Minimum", "\": ", " ",Math.Round(summary.SubjectAppraisedAmountSummary.Minimum, 2).ToString("0.00"), ","));
            sb.AppendLine(string.Concat("\t\t\t\"", "Maximum", "\": ", " ",Math.Round(summary.SubjectAppraisedAmountSummary.Maximum, 2).ToString("0.00")));
            sb.AppendLine("\t\t},");
            sb.AppendLine(string.Concat("\t\t\"", "InterestRateSummary", "\": {"));
            sb.AppendLine(string.Concat("\t\t\t\"", "Sum", "\": ", " ",Math.Round(summary.InterestRateSummary.Sum, 2).ToString("0.00"), ","));
            sb.AppendLine(string.Concat("\t\t\t\"", "Average", "\": ", " ",Math.Round(summary.InterestRateSummary.Average, 2).ToString("0.00"), ","));
            sb.AppendLine(string.Concat("\t\t\t\"", "Median", "\": ", " ",Math.Round(summary.InterestRateSummary.Median, 2).ToString("0.00"), ","));
            sb.AppendLine(string.Concat("\t\t\t\"", "Minimum", "\": ", " ",Math.Round(summary.InterestRateSummary.Minimum, 2).ToString("0.00"), ","));
            sb.AppendLine(string.Concat("\t\t\t\"", "Maximum", "\": ", " ",Math.Round(summary.InterestRateSummary.Maximum, 2).ToString("0.00")));
            sb.AppendLine("\t\t}");

            return sb.ToString();
        }
    }
}
