using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AdditionAnimator : MonoBehaviour {
	public GameObject NumberTableGO;
	public GameObject TextCellPrefab;
	public List<int> InputList;
	public List<int?> singleNumberLocationList{ get; set; }
	// Use this for initialization
	void Start () {
		if (InputList.Count > 0) {
			//for Debugging purpose
			initiateInputList(InputList);
		}
	}
	public void initiateInputList(List<int> currInputList){
		AdditionController additionCtrl = new AdditionController(currInputList);
		//Updating NumberLocationList with new inputList
		//Filling TableGO with Number at their location wrt straight Number Location List
		updateTableGO(additionCtrl.singleNumberLocationList);
		NumberTableGO.GetComponent<UIGrid> ().maxPerLine = additionCtrl.tableColumnCount;
		animationStepList (additionCtrl);
	}
	public void updateTableGO(List<int?> singleNumberLocationList){
		NumberTableGO.transform.DestroyChildren ();
		//Check if any int? has value, based on it, fill the cell
		foreach (int? number in singleNumberLocationList) {
			GameObject textCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
			if (number.HasValue)
				Debug.Log ("Number is" + number);
			textCell.GetComponent<UILabel>().text = number.ToString ();
		}

	}
	public void animationStepList(AdditionController additionCtrl){
		Debug.Log ("AnimationStepList");
		int column = additionCtrl.getTableColumnCount ();
		for(int i = 0;i<column;i++){
			animationStep (additionCtrl,i);
		}
	}
	public void animationStep (AdditionController additionCtrl, int stepIndex){
		Debug.Log ("animationStep at column is "+stepIndex);
		//highlight existing numbers at column index 
		List<int> columnIndexList = additionCtrl.getColumnwiseIntIndex(stepIndex);
		foreach (int index in columnIndexList){
			GameObject targetGO = NumberTableGO.transform.GetChild (index).gameObject;
			if (targetGO.GetComponent<UILabel> ().text.Length > 0) {
				//Got the targetGO

			}
		}
		//Display sum and carry by updating TableGO
		updateTableGO(additionCtrl.updateNumberLocationList(stepIndex));
	}
	// Update is called once per frame
	void Update () {
	
	}
}
