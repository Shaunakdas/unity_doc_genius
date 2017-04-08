using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MultiplicationAnimator : MonoBehaviour {
	public GameObject NumberTableGO;
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
		MultiplicationController multiplicationCtrl = new MultiplicationController(currInputList);
		//Updating NumberLocationList with new inputList
		//Filling TableGO with Number at their location wrt straight Number Location List
		updateTableGO(multiplicationCtrl.singleNumberLocationList);
		NumberTableGO.GetComponent<UIGrid> ().maxPerLine = multiplicationCtrl.tableColumnCount;
		animationStepList (multiplicationCtrl);
	}
	public void updateTableGO(List<int?> singleNumberLocationList){
		NumberTableGO.transform.DestroyChildren ();
		//Check if any int? has value, based on it, fill the cell
		foreach (int? number in singleNumberLocationList) {
			GameObject textCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
			if (number.HasValue)
//				Debug.Log ("Number is" + number);
			textCell.GetComponent<UILabel>().text = number.ToString ();
		}
		Debug.Log("Time consumed "+Time.realtimeSinceStartup);
	}
	public void animationStepList(MultiplicationController multiplicationCtrl){
		Debug.Log ("AnimationStepList");
		int column = multiplicationCtrl.getTableColumnCount ();
		int multiplicand = InputList [1];
		for (int multiplierIndex = 0; multiplierIndex < multiplier.ToString ().Length; multiplierIndex++) {
			for (int columnIndex = 0; columnIndex < multiplicand.ToString().Length+1; columnIndex++) {
				animationStep (multiplicationCtrl, columnIndex,multiplierIndex);
			}
		}
		multiplicationCtrl.addCarryRow ();
		updateTableGO(multiplicationCtrl.singleNumberLocationList);

		updateTableGO(multiplicationCtrl.updateSumNumberLocationList ());
	}

	public void animationStep (MultiplicationController multiplicationCtrl, int columnIndex,int multiplierIndex){
		Debug.Log ("animationStep at column is "+columnIndex);
		//highlight existing numbers at column index 
		List<int> columnIndexList = multiplicationCtrl.getColumnwiseIntIndex(columnIndex);
		foreach (int index in columnIndexList){
			GameObject targetGO = NumberTableGO.transform.GetChild (index).gameObject;
			if (targetGO.GetComponent<UILabel> ().text.Length > 0) {
				//Got the targetGO

			}
		}
		//Display sum and carry by updating TableGO
		updateTableGO(multiplicationCtrl.updateProductNumberLocationList(multiplierIndex,columnIndex));

//		Debug.Log("Time consumed "+Time.realtimeSinceStartup);
	}
//	public void addProductList(MultiplicationController multiplicationCtrl){
//		int column = multiplicationCtrl.getTableColumnCount ();
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
//		AdditionController productCtrl = new AdditionController (getProductList (multiplicationCtrl, sumNumberLocationList));
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
