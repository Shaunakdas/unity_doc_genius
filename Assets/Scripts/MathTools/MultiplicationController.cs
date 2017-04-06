using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class MultiplicationController {
	//25-3=75: 25-Multiplicand, 3-Multiplier, 75-Product
	//For holding input integer list for addition
	public List<int> inputList{ get; set; }
	//For holding integer location during addition
	public List<List<int?>> numberLocationList{ get; set; }
	//For Table Column count
	public int tableColumnCount{get; set;}
	public MultiplicationController(List<int> currInputList){
		inputList = currInputList;
	}
	public int getTableColumnCount(){
		//Get Column Count for UITable 
		return 0;
	}

	public List<List<int?>> setNumberLocationList(List<int> currInputList,int currTableColumnCount){
		//Get numberLocationList
		List<List<int?>> currNumberLocationList = new List<List<int?>>();
		if (currInputList.Count == 2) {
			//Performing multiplication for 2 integers only
		}
		//Set CarryIntList:numberLocationList[0] of all blank elements and length currTableColumnCount
		currNumberLocationList.Add(new List<int?>(0));
		return currNumberLocationList;
	}
	public List<List<int?>> updateNumberLocationList(List<List<int?>> currNumberLocationList,int operationColumn){
		//Get updated numberLocationList after multiplication operation is done on column operationColumn
		//First get element of ProductRow
		//Get element of CarryRow
		return currNumberLocationList;
	}
	public void getCarryIntList(List<List<int?>> currNumberLocationList){
	}
	public int getCarryInt(List<int> columnNumberList){
		//Get carry to next column based on all numbers in previous column
		return 0;
	}
	public int getProductUnitPlace(List<int> columnNumberList){
		//Get unit place of sum based on all numbers in curent columns
		return 0;
	}
	public List<int> getColumnwiseProduct(List<List<int?>> numberLocationList,int column){
		//Multiplying Multiplicand and en element of multiplier 
		return Enumerable.Repeat(0, 10).ToList();
	}
	public List<int> getElementwiseProduct(int multiplicand, int multiplier){
		//Multiplying an element of multiplicand and one of multiplier
		return Enumerable.Repeat(0, 10).ToList();
	}
}
