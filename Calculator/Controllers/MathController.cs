//MathController.cs file.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WepAPI_Demo.Controllers
{
    public class MathController : ApiController
    {
        [HttpGet]
        public string Add(int value1, int value2)
        {
            return (value1 + value2).ToString();
        }

        [HttpGet]
        public int Substract(int value1, int value2)
        {
            return value1 - value2;
        }

        [HttpGet]
        public int Multiply(int value1, int value2)
        {
            return value1 * value2;
        }

        [HttpGet]
        public int Divide(int value1, int value2)
        {
            return value1 / value2;
        }

        [HttpGet]
        public string Calculate(string txtString)
        {
            string[] caseList = txtString.Split(' ');
            int[] caseIntegers = Array.ConvertAll(caseList, s => int.Parse(s));
            double sumScores = 0;
            string[] stats = { "", "" };
            System.Numerics.BigInteger permutations = 1;
            // int permutations = 1;
            for (var i = 0; i <= caseIntegers.Length - 1; i++)
            {
                int elementScore = caseIntegers.Count(item => item >= caseIntegers[i]);
                sumScores = sumScores + ((double)(caseIntegers.Length + 1) / (double)(elementScore + 1));
                permutations = permutations * (i + 1);
                stats = new string[] { sumScores.ToString("0.00"), permutations.ToString("#,###") };
            }
            //return permutations;
            return "Average Score: " + sumScores.ToString("0.00") + ",\n Permutations: " + permutations.ToString("#,###");
        }

        [HttpGet]
        public string Get()
        {
            return "default";
        }
    }
}
