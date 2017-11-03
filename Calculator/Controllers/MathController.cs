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

        //'***************************************************************************
        //'Purpose:   Generates results of the Vertical Sticks Challenge as described
        //'           on https://www.hackerrank.com/challenges/vertical-sticks/problem
        //'Inputs     list:     single list to permutate
        //'Outputs:   statsResult is returned to txtResult in client.html
        //'           statsResult contains the solution produced with 2 different methods
        //'           the fast method is described on the webpage
        //'           the slow method is the approach suggested on the hackerrank website
        //'***************************************************************************
        [HttpGet]
        public string Calculate(string txtString)
        {
            Stopwatch stopWatch = new Stopwatch();

            // Splits the numbers into a string array and converts it into an integer array
            List<string> caseList = txtString.Split().ToList();
            List<int> intList = caseList.ConvertAll(int.Parse);
            double rayShots = 0;
            System.Numerics.BigInteger permutations = 1;

            // FAST METHOD...
            // iterates through the numbers to see how many numbers (elementScore) are greater than or equal to itself
            // adds (n+1) / (k+1) to sumScores
            // calculates the permutations (which is only used for display purposes!)

            stopWatch.Start();
            for (var i = 0; i <= intList.Count - 1; i++)
            {
                int elementScore = intList.Count(item => item >= intList[i]);
                rayShots = rayShots + ((double)(intList.Count + 1) / (double)(elementScore + 1));
                permutations = permutations * (i + 1);
            }
            stopWatch.Stop();
            
            string statsResult = "Fast Score: " + rayShots.ToString("0.00") + ", Permutations: "
                + permutations.ToString("#,###") + ", Calculation time: " + stopWatch.ElapsedMilliseconds.ToString();

            // SLOW METHOD...
            // finds all permutations of the list of numbers using 'Permutations'
            // calculates the sum of the Ray Shot Lengths for each permutation using 'RayShotsLength'
            // divides this number by permutations to give the score for the list
            stopWatch.Start();
            int score = 0;
            permutations = 0;
            IList<IList<int>> perms = Permutations(intList);
            foreach (List<int> perm in perms)
            {
                
                score = score + RayShotsLength(perm);
                permutations = permutations + 1;
            }

            decimal slowAnswer = (decimal)score / (decimal)permutations;


            stopWatch.Stop();

            statsResult = statsResult + "   ---   Slow score: " + slowAnswer.ToString("0.00") + ", Permutations: "
                + permutations.ToString("#,###") + ", Calculation time: " + stopWatch.ElapsedMilliseconds.ToString();

            // Returns detailed string output to client.html as txtResult
            return statsResult;
        }

        [HttpGet]
        public string Get()
        {
            return "default";
        }

        //'***************************************************************************
        //'Purpose:   Generate all permutations of the array
        //'Inputs     list:     list to permutate
        //'Outputs:   perms:    list of lists
        //'***************************************************************************



        private static IList<IList<T>> Permutations<T>(IList<T> list)
        {
            List<IList<T>> perms = new List<IList<T>>();
            if (list.Count == 0)
                return perms; // Empty list.
            int factorial = 1;
            for (int i = 2; i <= list.Count; i++)
                factorial *= i;
            for (int v = 0; v < factorial; v++)
            {
                List<T> s = new List<T>(list);
                int k = v;
                for (int j = 2; j <= list.Count; j++)
                {
                    int other = (k % j);
                    T temp = s[j - 1];
                    s[j - 1] = s[other];
                    s[other] = temp;
                    k = k / j;
                }
                perms.Add(s);
            }
            return perms;
        }


        //'***************************************************************************
        //'Purpose:   steps through intList (Current Permutation)
        //'              - adds 1 to score
        //'              - checks if the current element is greater than the previous element
        //'              - if so add 1 to score and compares with the next pervious element.
        //'              - If not greater, goto next element (i)
        //'
        //'Inputs:    intList... current permutation to perform calculation on
        //'
        //'Outputs:   score... the sum of ray shot lengths
        //'***************************************************************************

        public int RayShotsLength(List<int> intList)
        {
            int score = 0;
            for(int i = 0; i < intList.Count; i++)
            {
                score = score + 1;
                for (int j = 1; j <= i; j++)
                {
                    if (intList[i] > intList[i - j])
                    {
                        score = score + 1;
                    }
                    else
                    {
                        j = i;
                    }
                }
            }
            return score;
        }
    }
}
