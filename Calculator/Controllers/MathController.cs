//MathController.cs file.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WepAPI_Demo.Controllers
{
    public class MathController : ApiController
    {

        [HttpGet]
        public string Calculate(string txtString)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            // Splits the numbers into a string array and converts it into an integer array
            string[] caseList = txtString.Split(' ');
            int[] caseIntegers = Array.ConvertAll(caseList, s => int.Parse(s));
            double sumScores = 0;
            System.Numerics.BigInteger permutations = 1;
            // iterates through the numbers to see how many numbers (elementScore) are greater than or equal to itself
            // adds (n+1) / (k+1) to sumScores
            // calculates the permutations
            for (var i = 0; i <= caseIntegers.Length - 1; i++)
            {
                int elementScore = caseIntegers.Count(item => item >= caseIntegers[i]);
                sumScores = sumScores + ((double)(caseIntegers.Length + 1) / (double)(elementScore + 1));
                permutations = permutations * (i + 1);
            }
            stopWatch.Stop();
            // Returns detailed string output to client.html as txtResult
            return "Average Score: " + sumScores.ToString("0.00") + ",\n Permutations: " 
                + permutations.ToString("#,###") + " Calculation time: " + stopWatch.ElapsedMilliseconds.ToString();
        }

        [HttpGet]
        public string Get()
        {
            return "default";
        }
    }
}
