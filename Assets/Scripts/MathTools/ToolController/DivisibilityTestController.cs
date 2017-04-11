using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class DivisibilityTestController {
	//25+3=24: 25-Augend, 3-Addend, 75-Sum
	//For holding input integer list for addition
	public int inputNumber{ get; set; }
	public int rowCount{ get; set;}
	//For holding integer location during addition
	public List<int?> singleNumberLocationList{ get; set; }
	public string message{ get; set; }
	public int div;

	//For Table Column count
	public int tableColumnCount{get; set;}
	public DivisibilityTestController(int currInputNumber){
		inputNumber = currInputNumber;
		tableColumnCount = getTableColumnCount ();
		singleNumberLocationList = new List<int?> ();
		setNumberLocationList ();
	}
	public int getAnimationStepCount(int div){
		switch (div) {
		case 2:
			return 1;

		case 3:
			return 1;

		case 4:
			return 1;

		case 5:
			return 1;

		case 6:
			return 1;

		case 8:
			return 1;

		case 9:
			return 1;

		case 10:
			return 1;

		case 11:
			return 1;
		default:
			return 0;
		}
	}
	public string tutorialMessage(int stepCount,List<int?> singleNumberLocationList){
		string customText = "";
		string standardText = "";
		int sumOfDigits = 0;
		int digitCount = singleNumberLocationList.Count ();
		switch (div) {
		case 2:
			customText = singleNumberLocationList [digitCount].ToString ();
			standardText = "a number is divisible by 2 if it has any of the digits 0, 2, 4, 6 or 8 in its ones place";
			return "";
			break;
		case 3:
			singleNumberLocationList.ForEach (item => sumOfDigits=sumOfDigits + (int)item);
			customText = sumOfDigits.ToString ();
			standardText = "if the sum of the digits is a multiple of 3, then the number is divisible by 3.";
			return "";

		case 4:
			customText = singleNumberLocationList [digitCount].ToString ()+ singleNumberLocationList [digitCount-1].ToString ();
			standardText = "a number with 3 or more digits is divisible by 4 if the number formed by its last two digits (i.e. ones and tens) is divisible by 4";
			return "";

		case 5:
			customText = singleNumberLocationList [digitCount].ToString ();
			standardText = "a number which has either 0 or 5 in its ones place is divisible by 5";
			return "";

		case 6:
			singleNumberLocationList.ForEach (item => sumOfDigits += (int)item);
			customText = sumOfDigits.ToString ();
			customText = "";
			standardText = "if a number is divisible by 2 and 3 both then it is divisible by 6 also";
			return "";

		case 8:
			customText =singleNumberLocationList [digitCount].ToString ()+ singleNumberLocationList [digitCount-1].ToString ()+singleNumberLocationList [digitCount-2].ToString ();
			standardText = "a number with 4 or more digits is divisible by 8, if the number formed by the last three digits is divisible by 8.";
			return "";

		case 9:
			singleNumberLocationList.ForEach (item => sumOfDigits += (int)item);
			customText = sumOfDigits.ToString ();
			customText = "";
			standardText = "if the sum of the digits of a number is divisible by 9, then the number itself is divisible by 9";
			return "";

		case 10:
			customText = singleNumberLocationList [digitCount].ToString ();
			standardText = "If a number has 0 in the ones place then it is divisible by 10";
			return "";

		case 11:
			customText = "";
			standardText = "find the difference between the sum of the digits at odd places (from the right) and the sum of the digits at even places (from the right) of the number. If the difference is either 0 or divisible by 11, then the number is divisible by 11";
			return "";
			break;
		default:
			return "";
		}
	}
	public int getTableColumnCount(){
		//Get Column Count for UITable 
		//Adding One column to carry after largest column
		int columnCount = inputNumber.ToString().Count();
		return columnCount+1;
	}
	public List<int?> setNumberLocationList(){
		List<char> singleTextLocationList =inputNumber.ToString ().ToCharArray ().ToList ();
		singleTextLocationList.ForEach (item => singleNumberLocationList.Add ((int)item));
		return singleNumberLocationList;
	}
	public List<int> getHighlightIndexList(int stepCount){
		List<int> highlightIndexList = new List<int> ();
		return highlightIndexList;
	}

}
