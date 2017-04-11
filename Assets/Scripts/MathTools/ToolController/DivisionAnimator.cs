using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DivisionAnimator : MonoBehaviour {
	public GameObject DividendTableGO,DivisorTableGO,QuotientTableGO;
	public GameObject TextCellPrefab;
	public List<int> InputList;
	public List<int?> singleNumberLocationList{ get; set; }
	int multiplier;
	// Use this for initialization
	void Start () {
		multiplier = 0;
		if (InputList.Count > 0) {
			//for Debugging purpose
			multiplier = InputList[1];
			initiateInputList(InputList);
		}
	}
	public void initiateInputList(List<int> currInputList){
		Debug.Log("Time consumed "+Time.realtimeSinceStartup);
		DivisionController divisionCtrl = new DivisionController(currInputList);
		//Updating NumberLocationList with new inputList
		//Filling TableGO with Number at their location wrt straight Number Location List
		updateTableGO(divisionCtrl.singleNumberLocationList);
		DividendTableGO.GetComponent<UIGrid> ().maxPerLine = divisionCtrl.tableColumnCount;
		animationStepList (divisionCtrl);
	}
	public void updateTableGO(List<int?> singleNumberLocationList){
		DividendTableGO.transform.DestroyChildren ();
		//Check if any int? has value, based on it, fill the cell
		foreach (int? number in singleNumberLocationList) {
			GameObject textCell = NGUITools.AddChild (DividendTableGO, TextCellPrefab);
			if (number.HasValue)
//				Debug.Log ("Number is" + number);
			textCell.GetComponent<UILabel>().text = number.ToString ();
		}
		Debug.Log("Time consumed "+Time.realtimeSinceStartup);
	}
	public void animationStepList(DivisionController divisionCtrl){
		Debug.Log ("AnimationStepList");
		int column = divisionCtrl.getTableColumnCount ();
		int multiplicand = InputList [1];
		for (int multiplierIndex = 0; multiplierIndex < multiplier.ToString ().Length; multiplierIndex++) {
			for (int columnIndex = 0; columnIndex < multiplicand.ToString().Length+1; columnIndex++) {
				animationStep (divisionCtrl, columnIndex,multiplierIndex);
			}
		}

	}

	public void animationStep (DivisionController divisionCtrl, int columnIndex,int multiplierIndex){
		Debug.Log ("animationStep at column is "+columnIndex);
		//highlight existing numbers at column index 
		List<int> columnIndexList = divisionCtrl.getColumnwiseIntIndex(columnIndex);
		foreach (int index in columnIndexList){
			GameObject targetGO = DividendTableGO.transform.GetChild (index).gameObject;
			if (targetGO.GetComponent<UILabel> ().text.Length > 0) {
				//Got the targetGO

			}
		}
		//Display sum and carry by updating TableGO
		updateTableGO(divisionCtrl.updateProductNumberLocationList(multiplierIndex,columnIndex));

//		Debug.Log("Time consumed "+Time.realtimeSinceStartup);
	}
//	public void addProductList(DivisionController divisionCtrl){
//		int column = divisionCtrl.getTableColumnCount ();
//		//Adding products found
//
//
//
//		int listLength = singleNumberLocationList.Count;
//		int productStartingIndex = column * (InputList.Count + 1);
//		List<int?> sumNumberLocationList = singleNumberLocationList;
//		sumNumberLocationList.RemoveRange (0, productStartingIndex);
//		List<int?> remainNumberLocationList = singleNumberLocationList;
//		remainNumberLocationList.RemoveRange (productStartingIndex,listLength -productStartingIndex );
//		AdditionController productCtrl = new AdditionController (getProductList (divisionCtrl, sumNumberLocationList));
//
//		int columnLength= productCtrl.getTableColumnCount ();
//		for(int stepIndex = 0;stepIndex<columnLength;stepIndex++){
//			List<int> columnIndexList = productCtrl.getColumnwiseIntIndex(stepIndex);
//			sumNumberLocationList = productCtrl.updateNumberLocationList (stepIndex);
//		}
//		updateTableGO(remainNumberLocationList.AddRange(sumNumberLocationList));
//	}

	// Update is called once per frame
	void Update () {
//		Debug.Log("Time consumed "+Time.realtimeSinceStartup);
	}
}
