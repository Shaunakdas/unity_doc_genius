using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class ExpansionController {
	//25+3=24: 25-Augend, 3-Addend, 75-Sum
	//For holding input integer list for addition
	public int inputNumber{ get; set; }
	//For holding integer location during addition
	public List<int?> singleNumberLocationList{ get; set; }
	public List<string> comparisionTermList = new List<string>(new string[]{"Thousand","Lakhs","Crores"});
	public List<int> comparisionTermIndexList = new List<int>(new int[]{3,2,2});
	//For Table Column count
	public int tableColumnCount{get; set;}
	public ExpansionController(int currInputNumber){
		inputNumber = currInputNumber;
		tableColumnCount = getTableColumnCount ();
		singleNumberLocationList = new List<int?> ();
		setNumberLocationList ();
	}
	public int getTableColumnCount(){
		//Get Column Count for UITable 
		int columnCount = 2*inputNumber.ToString().Count();
		return columnCount;
	}
	public int getCommaCount(){
		if ((inputNumber / ((int)Mathf.Pow (10, 7))) > 0)
			return 3;
		else if ((inputNumber / ((int)Mathf.Pow (10, 5))) > 0)
			return 2;
		else if ((inputNumber / ((int)Mathf.Pow (10, 3))) > 0)
			return 1;
		else
			return 0;
	}
	public List<int?> setNumberLocationList(int currInputNumber,int currTableColumnCount){
		//Get numberLocationList
		inputNumber = currInputNumber;
		return setNumberLocationList ();
	}
	public List<int?> setNumberLocationList(){
		singleNumberLocationList.Add (inputNumber);
		return singleNumberLocationList;
	}
	public List<int?> updateNumberLocationList(int comparisionTermIndex){
		//OperationColumn is from right hand side

		string comparisionTerm = comparisionTermList [comparisionTermIndex];
		int comparisionTermLoc = comparisionTermIndexList [comparisionTermIndex];
		int targetNumber = (int)singleNumberLocationList [0];
		int targetLength = targetNumber.ToString ().Count();
		singleNumberLocationList.RemoveAt(0);
		Debug.Log ( targetLength - comparisionTermLoc);
		Debug.Log (targetNumber.ToString ().Substring (0, targetLength - comparisionTermLoc));
		singleNumberLocationList.Insert (0, int.Parse(targetNumber.ToString ().Substring (0,targetLength - comparisionTermLoc)));
		singleNumberLocationList.Insert (1, null);
		singleNumberLocationList.Insert (2, int.Parse(targetNumber.ToString ().Substring (targetLength - comparisionTermLoc,comparisionTermLoc)));
		return singleNumberLocationList;

	}
}
