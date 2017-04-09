using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SubtractAnimator : MonoBehaviour {
	public GameObject NumberTableGO;
	public GameObject TextCellPrefab;
	public List<int> InputList;
	public List<int?> singleNumberLocationList{ get; set; }
	// Use this for initialization

	void Start () {
		if (InputList.Count > 0) {
			//for Debugging purpose
			//First Sort InputList
			initiateInputList(InputList);
		}
	}
	public void initiateInputList(List<int> currInputList){
		SubtractController subtractCtrl = new SubtractController(currInputList);
		//Updating NumberLocationList with new inputList
		//Filling TableGO with Number at their location wrt straight Number Location List
		updateTableGO(subtractCtrl.singleNumberLocationList);
		NumberTableGO.GetComponent<UIGrid> ().maxPerLine = subtractCtrl.tableColumnCount;
		animationStepList (subtractCtrl);
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
	public void animationStepList(SubtractController subtractCtrl){
		Debug.Log ("AnimationStepList");
		int column = subtractCtrl.getTableColumnCount ();
		for(int i = 0;i<column;i++){
			animationStep (subtractCtrl,i);
		}
	}
	public void animationStep (SubtractController subtractCtrl, int stepIndex){
		Debug.Log ("animationStep at column is "+stepIndex);
		//highlight existing numbers at column index 
		List<int> columnIndexList = subtractCtrl.getColumnwiseIntIndex(stepIndex);
		foreach (int index in columnIndexList){
			GameObject targetGO = NumberTableGO.transform.GetChild (index).gameObject;
			if (targetGO.GetComponent<UILabel> ().text.Length > 0) {
				//Got the targetGO

			}
		}
		//Display sum and carry by updating TableGO
		updateTableGO(subtractCtrl.updateNumberLocationList(stepIndex));
	}
	// Update is called once per frame
	void Update () {
	
	}

}
