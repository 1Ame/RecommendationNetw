using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Services
{
    public class PearsonMeasure : ISimilarityMeasure
    {
        public double SimilarityLimit { get; }

        public PearsonMeasure()
        {
            SimilarityLimit = 0.5;
        }

        public double Calculate(IDictionary<string, int> sourceDict, IDictionary<string, int> otherDict)
        {
            var sourceList = new List<int>();
            var otherList = new List<int>();

            // find values with the same keys in both dictionaries, and put it in lists
            // for each question same position in list
            foreach (var item in sourceDict)
            {
                if (otherDict.ContainsKey(item.Key))
                {
                    sourceList.Add(item.Value);
                    otherList.Add(otherDict[item.Key]);
                }
            }

            //if list contain all the same elements. Ex: {3,3,3,3,3,3}
            if (sourceList.Distinct().Count() == 1 || otherList.Distinct().Count() == 1)
                return GetTanimotoCoef(sourceList, otherList);

            return GetPearsonCoef(sourceList, otherList);
        }

        private double GetPearsonCoef(IEnumerable<int> x, IEnumerable<int> y)
        {
            double _x = x.Average();
            double _y = y.Average();
            double count = x.Count();

            double xy = 0;

            for (int i = 0; i < count; i++)
            {
                xy += (x.ElementAt(i) - _x) * (y.ElementAt(i) - _y);
            }

            var disp1 = Dispercy(x, _x);
            var disp2 = Dispercy(y, _y);

            var result = (xy) / (count * Math.Sqrt(disp1) * Math.Sqrt(disp2));

            return Math.Round(result, 4);
        }
        private double GetTanimotoCoef(IEnumerable<int> x, IEnumerable<int> y)
        {
            int a = x.Count();
            int b = y.Count();
            int c = 0;
            for (int i = 0; i < a; i++)
            {
                if (TanimotoCompare(x.ElementAt(i),y.ElementAt(i)))
                    c++;
            }
            return (double)c / (a + b - c);
        }

        private double Dispercy(IEnumerable<int> list, double normCoef)
        {
            double result = 0;
            foreach (var num in list)
            {
                result += Math.Pow(num, 2);
            }
            return (result / list.Count()) - Math.Pow(normCoef, 2);
        }
        private bool TanimotoCompare(double a, double b)
        {
            if (a >= (b - 2) && a <= (b + 2))
                return true;

            return false;
        }
    }    
}
