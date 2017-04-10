using UnityEngine;
using System.Collections;

public static class HumanFriendlyInteger
{

	public static string NumberToWords(int number)
	{
		if (number == 0)
			return "zero";

		if (number < 0)
			return "minus " + NumberToWords(Mathf.Abs(number));

		string words = "";
		if ((number / 10000000) > 0)
		{
			words += NumberToWords(number / 10000000) + " crores ";
			number %= 10000000;
		}
		if ((number / 100000) > 0)
		{
			words += NumberToWords(number / 100000) + " lakhs ";
			number %= 1000000;
		}


		if ((number / 1000) > 0)
		{
			words += NumberToWords(number / 1000) + " thousand ";
			number %= 1000;
		}

		if ((number / 100) > 0)
		{
			words += NumberToWords(number / 100) + " hundred ";
			number %= 100;
		}

		if (number > 0)
		{


			var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
			var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

			if (number < 20)
				words += unitsMap[number];
			else
			{
				words += tensMap[number / 10];
				if ((number % 10) > 0)
					words += "-" + unitsMap[number % 10];
			}
		}

		return words;
	}
}
