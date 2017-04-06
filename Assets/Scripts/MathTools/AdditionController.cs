using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class AdditionController {
	//25+3=24: 25-Augend, 3-Addend, 75-Sum
	//For holding input integer list for addition
	public List<int> inputList{ get; set; }
	//For holding integer location during addition
	public List<List<int?>> numberLocationList{ get; set; }
	//For Table Column count
	public int tableColumnCount{get; set;}
	public AdditionController(List<int> currInputList){
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

		currNumberLocationList.Add(new List<int?>(0));
		return currNumberLocationList;
	}
	public List<List<int?>> updateNumberLocationList(List<List<int?>> currNumberLocationList,int operationColumn){
		
		//Get updated numberLocationList after addition operation is done on column operationColumn
		//First get element of SumRow
		//Get element of CarryRow
		return currNumberLocationList;
	}
	public void getCarryIntList(List<List<int?>> currNumberLocationList){
		//Check for null value by num.HasValue property

	}
	public int getCarryInt(List<int> columnNumberList){
		//Get carry to next column based on all numbers in previous columns
		return 0;
	}
	public int getSumUnitPlace(List<int> columnNumberList){
		//Get unit place of sum based on all numbers in curent columns
		return 0;
	}
}
