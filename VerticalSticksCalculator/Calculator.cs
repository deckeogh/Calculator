using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace VerticalSticksCalculator
{
	/// <summary>
	/// The Vertical Stick calculator class.
	/// </summary>
	/// <remarks>
	/// By keeping this logic in its own library, it's easier to test (see unit tests) and use in other kind of projects (web, desktop, console etc.)
	/// </remarks>
	public class Calculator
    {
		/// <summary>Processes the input using the fast method.</summary>
		/// <param name="textList">The text list.</param>
		/// <param name="counter">The counter.</param>
		/// <param name="caseStats">The case stats.</param>
		/// <returns></returns>
		public static List<string> ProcessFast(string[] textList, int counter, List<string> caseStats)
		{
			double sumScores = 0;
			BigInteger permutations = 1;

			string[] caseList = textList[counter].Split(' ');
			int[] caseIntegers = Array.ConvertAll(caseList, s => int.Parse(s));

			for (var i = 0; i <= caseIntegers.Length - 1; i++)
			{
				int elementScore = caseIntegers.Count(item => item >= caseIntegers[i]);
				sumScores = sumScores + ((double)(caseIntegers.Length + 1) / (double)(elementScore + 1));
				permutations = permutations * (i + 1);
			}
			if (caseIntegers.Length > 20)
			{
				// If there are more than 20 integers, display the number of permutations in scientific notation
				caseStats.Add(textList[counter] + " ... Average Score: " + sumScores.ToString("0.00") + " ... Permutations: " + permutations.ToString("E2"));
			}
			else
			{
				// Otherwise display it as an exact number including thousand separators
				caseStats.Add(textList[counter] + " ... Average Score: " + sumScores.ToString("0.00") + " ... Permutations: " + permutations.ToString("#,###"));
			}
			counter += 2;
			if (counter <= Convert.ToInt32(textList[0]) * 2)
			{
				ProcessFast(textList, counter, caseStats);
			}
			return caseStats;
		}

		/// <summary>Processes the input using the slow method.</summary>
		/// <param name="textList">The text list.</param>
		/// <param name="counter">The counter.</param>
		/// <param name="caseStats">The case stats.</param>
		/// <returns></returns>
		public static List<string> ProcessSlow(string[] textList, int counter, List<string> caseStats)
		{
			Stopwatch stopWatch = new Stopwatch();

			// Splits the numbers into a string array and converts it into an integer array
			string[] caseList = textList[counter].Split(' ');
			int[] caseIntegers = Array.ConvertAll(caseList, s => int.Parse(s));

			System.Numerics.BigInteger permutations = 1;


			// SLOW METHOD...
			// finds all permutations of the list of numbers using 'Permutations'
			// calculates the sum of the Ray Shot Lengths for each permutation using 'RayShotsLength'
			// divides this number by permutations to give the score for the list
			stopWatch.Start();
			int score = 0;
			permutations = 0;
			IList<IList<int>> perms = Permutations(caseIntegers);
			foreach (List<int> perm in perms)
			{
				score = score + RayShotsLength(perm);
				permutations = permutations + 1;
			}

			decimal slowAnswer = (decimal)score / (decimal)permutations;

			caseStats.Add(textList[counter] + " ... Average Score: " + slowAnswer.ToString("0.00")
				+ ", Permutations: " + permutations.ToString("#,###"));

			counter += 2;
			if (counter <= Convert.ToInt32(textList[0]) * 2)
			{
				ProcessSlow(textList, counter, caseStats);
			}

			// Returns detailed string output to client.html as txtResult
			return caseStats;
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

		/// <summary>
		/// Steps through intList (Current Permutation)
		/// <list type="bullet">
		///		<item>
		///			<description>adds 1 to score</description>  
		///		</item>
		///		<item>
		///			<description>checks if the current element is greater than the previous element</description>  
		///		</item>
		///		<item>
		///			<description>if so add 1 to score and compares with the next previous element.</description>  
		///		</item>
		///		<item>
		///			<description>If not greater, goto next element (i)</description>  
		///		</item>
		/// </list>
		/// </summary>
		/// <param name="intList">current permutation to perform calculation on</param>
		/// <returns>score... the sum of ray shot lengths</returns>
		public static int RayShotsLength(List<int> intList)
		{
			int score = 0;
			for (int i = 0; i < intList.Count; i++)
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
