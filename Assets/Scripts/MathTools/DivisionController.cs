using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class SubtractionController {
	//21-3=7: 21-Dividend, 3-Divisor, 21-Quotient,0-Remainder
	public enum Operation{ Carry,Subtraction};
	//For holding input integer list for addition
	public List<int> inputList{ get; set; }
	//For holding integer location during addition
	public List<List<int?>> numberLocationList{ get; set; }
	//For Table Column count
	public int tableColumnCount{get; set;}
	public SubtractionController(List<int> currInputList){
		inputList = currInputList;
	}
	public int getTableColumnCount(){
		//Get Column Count for UITable 
		return 0;
	}

	public List<List<int?>> setNumberLocationList(List<int> currInputList,int currTableColumnCount){
		//Get numberLocationList
		List<List<int?>> currNumberLocationList = new List<List<int?>>();
		//Set CarryIntList:numberLocationList[0] of all blank elements and length currTableColumnCount
		if (currInputList.Count == 2) {
			//Performing multiplication for 2 integers only
		}
		currNumberLocationList.Add(new List<int?>(0));
		return currNumberLocationList;
	}
	public List<List<int?>> updateNumberLocationList(List<List<int?>> currNumberLocationList,int operationColumn,Operation operation){
		if (operation == Operation.Carry) {
			//Get updated numberLocationList after carry operation is done on column operationColumn
			//Get element of MultiplierRow
			//Get element of CarryRow
		}else if(operation == Operation.Subtraction){
			//Get updated numberLocationList after Subtraction operation is done on column operationColumn
			//First get element of ProductRow
			//Get element of CarryRow
		}

		return currNumberLocationList;
	}
	public List<int> getCarryInt(List<int> columnNumberList){
		//Get carry to next column based on all numbers in previous columns
		return Enumerable.Repeat(0, 10).ToList();
	}
	public int getSumUnitPlace(List<int> columnNumberList){
		//Get unit place of sum based on all numbers in curent columns
		return 0;
	}
}
