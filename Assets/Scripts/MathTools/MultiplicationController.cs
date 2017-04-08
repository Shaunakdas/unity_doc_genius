using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class MultiplicationController {
	//25-3=75: 25-Multiplicand, 3-Multiplier, 75-Product
	//For holding input integer list for addition
	public List<int> inputList{ get; set; }
	//For holding integer location during addition
	public List<int?> singleNumberLocationList{ get; set; }
	//For Table Column count
	public int tableColumnCount{get; set;}
	public MultiplicationController(List<int> currInputList){
		inputList = currInputList;
		tableColumnCount = getTableColumnCount ();
		singleNumberLocationList = new List<int?> ();
		setNumberLocationList ();
	}
	public int getTableColumnCount(){
		//Get Column Count for UITable 
		//Adding One column to carry after largest column
		return (inputList[0]*inputList[1]).ToString().Count();
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
	public List<int?> setNumberLocationList(){
		//Adding 2 to inputList.Count to handle carry row and sum row
		int listLength = tableColumnCount*(inputList.Count+2); 
		int sumRowCount = inputList [1].ToString ().Length + 1;
		//Removing last row (sum row)from calculations
		for(int rowCount=0; rowCount< ((inputList.Count+1)+sumRowCount); rowCount++){
			for (int columnCount = 0; columnCount < tableColumnCount; columnCount++) {
				if (rowCount == 0) {
					//Carry Row. do nothing
//					Debug.Log ("rowCount == 0:"+rowCount +" , "+ columnCount+".Size of list:"+singleNumberLocationList.Count);
					singleNumberLocationList.Add(null);
				} else if (rowCount >= (inputList.Count + 1)) {
					//Sum Row. do nothing
//					Debug.Log ("rowCount == 0:"+rowCount +" , "+ columnCount+".Size of list:"+singleNumberLocationList.Count);
					singleNumberLocationList.Add(null);
				} else {
					int inputLength = inputList [rowCount - 1].ToString ().Length;
					int startIndex = tableColumnCount - inputLength;
					if (columnCount >= startIndex) {
						Debug.Log ("rowCount == 0:"+rowCount +" , "+ columnCount+".Size of list:"+singleNumberLocationList.Count);
						singleNumberLocationList.Add (int.Parse (inputList [rowCount - 1].ToString () [columnCount - startIndex].ToString ()));
//						Debug.Log ("adding non null element:" + int.Parse (inputList [rowCount - 1].ToString () [columnCount - startIndex].ToString ()));
					} else {
//						Debug.Log ("rowCount == 0:"+rowCount +" , "+ columnCount+".Size of list:"+singleNumberLocationList.Count);
						singleNumberLocationList.Add(null);
					}
				}
			}
		}

		return singleNumberLocationList;
	}
	public void addCarryRow(){
		int lowerLimit = tableColumnCount*(inputList.Count+1);
		for (int i =lowerLimit; i < lowerLimit+tableColumnCount; i++) {
			singleNumberLocationList.Insert (i,null);
		}
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
	public List<int?> updateSumNumberLocationList(){
		int column =getTableColumnCount ();
		//Adding products found

		int listLength = singleNumberLocationList.Count;
		int productStartingIndex = column * (inputList.Count + 1);
		List<int?> sumNumberLocationList = new List<int?> (singleNumberLocationList);
		List<int?> remainNumberLocationList = new List<int?> (singleNumberLocationList);
		sumNumberLocationList.RemoveRange (0, productStartingIndex);
		remainNumberLocationList.RemoveRange (productStartingIndex,listLength -productStartingIndex );

		AdditionController productCtrl = new AdditionController (getProductList ( singleNumberLocationList));
		productCtrl.singleNumberLocationList = sumNumberLocationList;	
		productCtrl.tableColumnCount = tableColumnCount;
		int columnLength= productCtrl.tableColumnCount;
		Debug.Log ("ColumnLength of productCtrl"+productCtrl.tableColumnCount);
		for(int stepIndex = 0;stepIndex<columnLength;stepIndex++){
			List<int> columnIndexList = productCtrl.getColumnwiseIntIndex(stepIndex);
			sumNumberLocationList = productCtrl.updateNumberLocationList (stepIndex);
		}
		remainNumberLocationList.AddRange (sumNumberLocationList);
		return remainNumberLocationList;
	}
	
	public List<int> getColumnwiseProduct(List<List<int?>> numberLocationList,int column){
		//Multiplying Multiplicand and en element of multiplier 
		return Enumerable.Repeat(0, 10).ToList();
	}
	public List<int> getElementwiseProduct(int multiplicand, int multiplier){
		//Multiplying an element of multiplicand and one of multiplier
		return Enumerable.Repeat(0, 10).ToList();
	}
	public List<int> getProductList(List<int?> singleNumberLocationList){
		
		List<int> productList = new List<int>();
		int columnCount = tableColumnCount;
		int start = columnCount * (inputList.Count + 1);
		int end = start + columnCount;
		for (int rowCount = (inputList.Count + 2); rowCount < (inputList.Count + 2 + inputList [1].ToString ().Length); rowCount++) {
			string productText="";
			for (int column = 0; column < columnCount; column++) {
				if (singleNumberLocationList [columnCount*rowCount + column].HasValue)
					Debug.Log ("character is " + singleNumberLocationList [columnCount*rowCount + column] + "at " + rowCount + " " + column);
				productText += ""+singleNumberLocationList [columnCount*rowCount + column];
			}

			productList.Add (int.Parse (productText));
			Debug.Log ("character is " + productText + "at " + rowCount);
		}
		return productList;
	}
}
