using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class DivisionController {
	//21-3=7: 21-Dividend, 3-Divisor, 21-Quotient,0-Remainder
	public enum Operation{ Carry,Subtraction};
	//For holding input integer list for addition
	public List<int> inputList{ get; set; }
	//For holding integer location during addition
	public List<int?> singleNumberLocationList{ get; set; }
	//For Table Column count
	public int tableColumnCount{get; set;}

	public int dividend{get; set;}
	public int divisor{get; set;}
	public int quotient{get; set;}
	public List<int> quotientCharList{ get; set; }
	public int remainder{get; set;}
	public DivisionController(List<int> currInputList){
		inputList = currInputList;
		dividend = currInputList [0];
		divisor = currInputList [1];
		tableColumnCount = getTableColumnCount ();
		singleNumberLocationList = new List<int?> ();
		setNumberLocationList ();
	}
	public List<int> getColumnwiseIntIndex(int columnIndex){
		Debug.Log ("getColumnwiseIntIndex");
		List<int> columnwiseIntIndex = new List<int>();
		int index =  tableColumnCount - columnIndex-1;
		Debug.Log ("getColumnwiseIntIndex"+index+singleNumberLocationList.Count);
		while (index < singleNumberLocationList.Count) {
			if (singleNumberLocationList [index].HasValue)
				Debug.Log ("ColumnwiseIntIndex for " + columnIndex + " is " + index);
			columnwiseIntIndex.Add (index);
			index = index + tableColumnCount;
		}
		return columnwiseIntIndex;
	}
	public int getTableColumnCount(){
		//Get Column Count for UITable 
		tableColumnCount = dividend;
		return tableColumnCount;
	}

	public List<int?> setNumberLocationList(){
		//Adding 2 to inputList.Count to handle carry row and sum row
		int quotientSize = charSize(dividend)-charSize(divisor)+1; 
		int listLength = 2 * quotientSize + 1;
		//Removing last row (sum row)from calculations
		for(int rowCount=0; rowCount< 3; rowCount++){
			for (int columnCount = 0; columnCount < tableColumnCount; columnCount++) {
				//For division columnCount goes from left to right
				if (rowCount ==2) {
					//Sum Row. do nothing
					singleNumberLocationList.Add(null);
				} else {
					int inputLength = charSize(inputList [rowCount]);
					if (columnCount < inputLength) {
						Debug.Log ("rowCount == 0:"+rowCount +" , "+ columnCount+".Size of list:"+singleNumberLocationList.Count);
						singleNumberLocationList.Add (int.Parse (inputList [rowCount].ToString () [columnCount].ToString ()));
						//						Debug.Log ("adding non null element:" + int.Parse (inputList [rowCount].ToString () [columnCount - startIndex].ToString ()));
					} else {
						//						Debug.Log ("rowCount == 0:"+rowCount +" , "+ columnCount+".Size of list:"+singleNumberLocationList.Count);
						singleNumberLocationList.Add(null);
					}
				}
			}
		}

		return singleNumberLocationList;
	}
	public List<int?> updateProductNumberLocationList(int multiplierColumn,int operationColumn){
		//OperationColumn is from right hand side
		Debug.Log("Inside updateProductNumberLocationList"+multiplierColumn+""+ operationColumn);
		int rowCount = multiplierColumn+inputList.Count + 1;
		int sumCellIndex = (rowCount * tableColumnCount) + (tableColumnCount - operationColumn-1)-multiplierColumn;
		int carryCellIndex = tableColumnCount - operationColumn - 2;

		int multiplicand = 0;
		int multiplicandIndex = tableColumnCount + (tableColumnCount - operationColumn - 1);
		if (singleNumberLocationList [multiplicandIndex].HasValue) 
			multiplicand = (int)singleNumberLocationList [multiplicandIndex];

		int multiplier = (int)singleNumberLocationList [2*tableColumnCount+(tableColumnCount - multiplierColumn-1)];

		int prevCarry = 0;
		int prevCarryIndex = tableColumnCount - operationColumn - 1;
		if (singleNumberLocationList [prevCarryIndex].HasValue) 
			prevCarry = (int)singleNumberLocationList [prevCarryIndex];

		int product = (multiplicand * multiplier) + prevCarry;
		singleNumberLocationList [sumCellIndex] = int.Parse(product.ToString ().Substring(product.ToString ().Count() - 1));

		if((!singleNumberLocationList [multiplicandIndex].HasValue) && (!singleNumberLocationList [prevCarryIndex].HasValue)){
			Debug.Log ("Null vaue " + (multiplicandIndex) + "" + (prevCarryIndex)+ "" + (sumCellIndex)+ "" + (carryCellIndex));
			singleNumberLocationList [sumCellIndex]=null;
		}
		if (product.ToString ().Count()>1)
			singleNumberLocationList [carryCellIndex] = int.Parse(product.ToString ().Substring(0,(product.ToString ().Count() - 1)));
		return singleNumberLocationList;
	}
	public int charSize(int a){
		return Mathf.Abs(a).ToString ().Length;
	}
}
