using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class ComparisionController {
	//25+3=24: 25-Augend, 3-Addend, 75-Sum
	//For holding input integer list for addition
	public List<int> inputList{ get; set; }
	public int rowCount{ get; set;}
	//For holding integer location during addition
	public List<List<int?>> numberLocationList{ get; set; }

	//For Table Column count
	public int tableColumnCount{get; set;}
	public ComparisionController(List<int> currInputList){
		inputList = currInputList;
		tableColumnCount = getTableColumnCount ();
		numberLocationList = new List<List<int?>> ();
		setNumberLocationList ();
		rowCount = inputList.Count + 1;
	}
	public int getTableColumnCount(){
		//Get Column Count for UITable 
		//Adding One column to carry after largest column
		int columnCount = inputList.Max().ToString().Count();
		return columnCount+1;
	}

	public List<List<int?>> setNumberLocationList(){
		//Adding 2 to inputList.Count to handle carry row and sum row
		//Removing last row (sum row)from calculations
		for(int rowCount=0; rowCount< (inputList.Count); rowCount++){
			List<int?> numberLocationRow = new List<int?> ();
			numberLocationRow.Add(inputList[rowCount]);
			for (int columnCount = 1; columnCount < tableColumnCount; columnCount++) {
				int inputLength = inputList [rowCount].ToString ().Length;
				int startIndex = tableColumnCount - inputLength;
				if (columnCount >= startIndex) {
					numberLocationRow.Add (int.Parse (inputList [rowCount].ToString () [columnCount - startIndex].ToString ()));
				} else {
					numberLocationRow.Add(null);
				}

			}
			numberLocationList.Add (numberLocationRow);
		}
		return numberLocationList;
	}

	public List<List<int?>> compareNumberLocationList(int operationColumn){
		int div = (int)Mathf.Pow( 10,(tableColumnCount - operationColumn - 1));
		if (div == 1)
			div = 0;
		Debug.Log ("Div id " + div.ToString());
//		numberLocationList.OrderBy (x => x [0] % div);
		numberLocationList.Sort((x,y)=>digitComparer((int)x[0],div).CompareTo(digitComparer((int)y[0],div)));
		return numberLocationList;
	}
	public int digitComparer(int integer, int div){
		return integer - (integer % div);
	}
}
