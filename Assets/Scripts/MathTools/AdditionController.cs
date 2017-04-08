using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class AdditionController {
	//25+3=24: 25-Augend, 3-Addend, 75-Sum
	//For holding input integer list for addition
	public List<int> inputList{ get; set; }
	public int rowCount{ get; set;}
	//For holding integer location during addition
	//public List<List<int?>> numberLocationList{ get; set; }
	public List<int?> singleNumberLocationList{ get; set; }

	//For Table Column count
	public int tableColumnCount{get; set;}
	public AdditionController(List<int> currInputList){
		inputList = currInputList;
		tableColumnCount = getTableColumnCount ();
		singleNumberLocationList = new List<int?> ();
		setNumberLocationList ();
		rowCount = inputList.Count + 1;
	}
	public int getTableColumnCount(){
		//Get Column Count for UITable 
		//Adding One column to carry after largest column
		int columnCount = inputList.Max().ToString().Count();
		return columnCount+1;
	}
	public List<int> getColumnwiseIntIndex(int columnIndex){
//		Debug.Log ("getColumnwiseIntIndex");
		List<int> columnwiseIntIndex = new List<int>();
		int index =  tableColumnCount - columnIndex-1;
//		Debug.Log ("getColumnwiseIntIndex"+index+singleNumberLocationList.Count);
		while (index < singleNumberLocationList.Count) {
			if (singleNumberLocationList [index].HasValue)
//				Debug.Log ("ColumnwiseIntIndex for " + columnIndex + " is " + index);
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
		for(int rowCount=0; rowCount< (inputList.Count+2); rowCount++){
			for (int columnCount = 0; columnCount < tableColumnCount; columnCount++) {
				if (rowCount == 0) {
					//Carry Row. do nothing
					Debug.Log ("rowCount == 0:"+rowCount +" , "+ columnCount+".Size of list:"+singleNumberLocationList.Count);
					singleNumberLocationList.Add(null);
				} else if (rowCount == (inputList.Count + 1)) {
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
		//Get updated numberLocationList after addition operation is done on column operationColumn
		//First get element of SumRow
		//Get element of CarryRow
		return singleNumberLocationList;
	}
	public List<int?> updateNumberLocationList(int operationColumn){
		//OperationColumn is from right hand side
		rowCount = inputList.Count + 1;
		int sumCellIndex = (rowCount * tableColumnCount) + (tableColumnCount - operationColumn-1);
		int carryCellIndex = tableColumnCount - operationColumn - 2;
		Debug.Log ("Changing int with" + sumCellIndex + " " + carryCellIndex);
		int sum=0;
		foreach (int index in getColumnwiseIntIndex(operationColumn)) {
			if (singleNumberLocationList [index].HasValue) {
				Debug.Log ("Adding at column" + operationColumn + " is " + index);
				sum = sum + (int)singleNumberLocationList [index];
			}
		}
		Debug.Log ("Sum of column" + operationColumn + " is " + sum);
		singleNumberLocationList [sumCellIndex] = int.Parse(sum.ToString ().Substring(sum.ToString ().Count() - 1));
		if (sum.ToString ().Count()>1)
			singleNumberLocationList [carryCellIndex] = int.Parse(sum.ToString ().Substring(0,(sum.ToString ().Count() - 1)));
		if ((sum == 0) && (operationColumn == tableColumnCount - 1))
			singleNumberLocationList [sumCellIndex] = null;
		return singleNumberLocationList;
	}
		
}
