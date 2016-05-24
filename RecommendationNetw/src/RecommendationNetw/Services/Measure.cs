using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Services
{
    public class PearsonMeasure : IMeasure
    {
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

            //if list contain all the same elements. Ex {3,3,3,3,3,3}
            var sourceDistElem = sourceList.Distinct().Count() == 1;
            var otherDistElem = otherList.Distinct().Count() == 1;

            if (sourceDistElem && otherDistElem)
                return GetEuclidCoef(sourceList, otherList); ;

            if (sourceDistElem || otherDistElem)
                return GetCosinusCoef(sourceList, otherList);

            return GetPearsonCoef(sourceList, otherList);
        }

        private double GetPearsonCoef(List<int> x, List<int> y)
        {
            double _x = x.Average();
            double _y = y.Average();
            double count = x.Count;

            double xy = 0;

            for (int i = 0; i < count; i++)
            {
                xy += (x[i] - _x) * (y[i] - _y);
            }

            var disp1 = Dispercy(x, _x);
            var disp2 = Dispercy(y, _y);

            var result = (xy) / (count * Math.Sqrt(disp1) * Math.Sqrt(disp2));

            return Math.Round(result, 4);
        }
        private double GetCosinusCoef(List<int> x, List<int> y)
        {
            return VectorProduct(x, y) / (Math.Sqrt(VectorProduct(x, x) * VectorProduct(y, y)));
        }
        private double GetEuclidCoef(List<int> x, List<int> y)
        {
            return 0;
        }

        private double Dispercy(List<int> list, double normCoef)
        {
            double result = 0;
            foreach (var num in list)
            {
                result += Math.Pow(num, 2);
            }
            return (result / list.Count) - Math.Pow(normCoef, 2);
        }
        private double VectorProduct(List<int> a, List<int> b)
        {
            double result = 0;
            for (int i = 0; i < a.Count; i++)
            {
                result += a[i] * b[i];
            }
            return result;
        }
    }    
}
