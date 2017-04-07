using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class SubtractController {
//25-3=21: 25-Minuend, 3-subtraend, 21-difference
	//For holding input integer list for Subtraction
	public List<int> inputList{ get; set; }
	//For holding integer location during Subtraction
	//public List<List<int?>> numberLocationList{ get; set; }
	public List<int?> singleNumberLocationList{ get; set; }

	//For Table Column count
	public int tableColumnCount{get; set;}
	public SubtractController(List<int> currInputList){
		inputList = currInputList;
		tableColumnCount = getTableColumnCount ();
		singleNumberLocationList = new List<int?> ();
		setNumberLocationList ();
	}
	public int getTableColumnCount(){
		//Get Column Count for UITable 
		//Adding One column to carry after largest column
		int columnCount = inputList.Max().ToString().Count();
		return columnCount;
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

	public List<int?> setNumberLocationList(List<int> currInputList,int currTableColumnCount){
		//Get numberLocationList
		inputList = currInputList;
		return setNumberLocationList ();
	}
	public List<int?> setNumberLocationList(){
		//Adding 2 to inputList.Count to handle carry row and sum row
		int listLength = tableColumnCount*(inputList.Count+2); 
		//Removing last row (sum row)from calculations
		for(int rowCount=0; rowCount< (inputList.Count+1); rowCount++){
			for (int columnCount = 0; columnCount < tableColumnCount; columnCount++) {
				if (rowCount == (inputList.Count + 1)) {
					//Sum Row. do nothing
					Debug.Log ("rowCount == 0:"+rowCount +" , "+ columnCount+".Size of list:"+singleNumberLocationList.Count);
					singleNumberLocationList.Add(null);
				} else {
					int inputLength = inputList [rowCount - 1].ToString ().Length;
					int startIndex = tableColumnCount - inputLength;
					if (columnCount >= startIndex) {
						Debug.Log ("rowCount == 0:"+rowCount +" , "+ columnCount+".Size of list:"+singleNumberLocationList.Count);
						singleNumberLocationList.Add (int.Parse (inputList [rowCount - 1].ToString () [columnCount - startIndex].ToString ()));
						Debug.Log ("adding non null element:" + int.Parse (inputList [rowCount - 1].ToString () [columnCount - startIndex].ToString ()));
					} else {
						Debug.Log ("rowCount == 0:"+rowCount +" , "+ columnCount+".Size of list:"+singleNumberLocationList.Count);
						singleNumberLocationList.Add(null);
					}
				}
			}
		}

		return singleNumberLocationList;
	}


	public List<int?> updateNumberLocationList(List<int?> currNumberLocationList,int operationColumn){
		singleNumberLocationList = currNumberLocationList;
		singleNumberLocationList = updateNumberLocationList(operationColumn);
		//Get updated numberLocationList after Subtraction operation is done on column operationColumn
		//First get element of SumRow
		//Get element of CarryRow
		return singleNumberLocationList;
	}
	public List<int?> updateNumberLocationList(int operationColumn){
		//OperationColumn is from right hand side
		int rowCount = inputList.Count + 1;
		int differenceCellIndex = (rowCount * tableColumnCount) + (tableColumnCount - operationColumn-1);
		int carryCellIndex = tableColumnCount - operationColumn - 2;
		int minuendCellIndex = tableColumnCount - operationColumn-1;
		int sum=0;
		int minuend = (int)singleNumberLocationList [getColumnwiseIntIndex (operationColumn) [0]];
		int subtraend = (int)singleNumberLocationList [getColumnwiseIntIndex (operationColumn) [1]];
		int difference = minuend - subtraend;
		if ((operationColumn == tableColumnCount) && (difference<0)) {
			//First digit of minuend is less than that of subtraend
			singleNumberLocationList[differenceCellIndex] = -difference;
		} else if (difference<0) {
			singleNumberLocationList [carryCellIndex] = singleNumberLocationList [carryCellIndex] - 1;
			singleNumberLocationList [minuendCellIndex] = 10 + singleNumberLocationList [minuendCellIndex];
			singleNumberLocationList [differenceCellIndex] = 10 - difference;
		} else if (difference>0) {
			singleNumberLocationList [differenceCellIndex] = difference;
		}
		Debug.Log ("Sum of column" + operationColumn + " is " + sum);
		return singleNumberLocationList;
	}

}
