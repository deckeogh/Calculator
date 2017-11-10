using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using static VerticalSticksCalculator.Calculator;

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
        
        [HttpPost]
        public List<string> CalculateFast([FromBody]string txtString)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                string[] textList = txtString.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string tempString = textList[0];
            
                List<string> txtAns = new List<string>() { "Fast Method" };
                List<string> statsResult = ProcessFast(textList, 2, txtAns);
            
                stopWatch.Stop();

                statsResult.Add("Calculation time: " + stopWatch.ElapsedMilliseconds.ToString() + " milliseconds!");

                //// Returns detailed string output to client.html as txtResult
                return statsResult;
            }
            catch (System.Exception excep)
            {
                List<string> txtAns = new List<string>() { "Error", excep.Message };
                return txtAns;
            }
            
        }

        [HttpPost]
        public List<string> CalculateSlow([FromBody]string txtString)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                string[] textList = txtString.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string tempString = textList[0];

                List<string> txtAns = new List<string>() { "Slow Method" };
                List<string> statsResult = ProcessSlow(textList, 2, txtAns);

                stopWatch.Stop();

                statsResult.Add("Calculation time: " + stopWatch.ElapsedMilliseconds.ToString() + " milliseconds!");

                //// Returns detailed string output to client.html as txtResult
                return statsResult;
            }
            catch (System.Exception excep)
            {
                List<string> txtAns = new List<string>() { "Error", excep.Message };
                return txtAns;
            }
        }

        [HttpGet]
        public string Get()
        {
            return "default";
        }
    }
}