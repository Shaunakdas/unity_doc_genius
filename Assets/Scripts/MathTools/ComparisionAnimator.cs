using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ComparisionAnimator : MonoBehaviour {
	public GameObject NumberTableGO;
	public GameObject TextCellPrefab;
	public List<int> InputList;
	public List<List<GameObject>> InputGOList;
	// Use this for initialization
	void Start () {
		if (InputList.Count > 0) {
			//for Debugging purpose
			initiateInputList(InputList);
		}
	}
	public void initiateInputList(List<int> currInputList){
		ComparisionController comparisionCtrl = new ComparisionController(currInputList);
		//Updating NumberLocationList with new inputList
		//Filling TableGO with Number at their location wrt straight Number Location List
		InputGOList = new List<List<GameObject>>();
		updateTableGO(comparisionCtrl.numberLocationList);
		NumberTableGO.GetComponent<UITable> ().columns = comparisionCtrl.tableColumnCount;
		animationStepList (comparisionCtrl);
	}
	public void updateTableGO(List<List<int?>> numberLocationList){
		NumberTableGO.transform.DestroyChildren ();
		for (int i = 0; i < numberLocationList.Count; i++) {
			List<GameObject> numberGORow = new List<GameObject> ();
			for (int j = 0; j < numberLocationList [i].Count; j++) {
				GameObject textCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
				if (numberLocationList [i] [j].HasValue) {
					textCell.GetComponent<UILabel> ().text = numberLocationList [i] [j].ToString ();
					Debug.Log (i + " " + j+"  "+numberLocationList [i] [j].ToString());
				}
				numberGORow.Add (textCell);
			}
			InputGOList.Add (numberGORow);
		}

	}
	public void animationStepList(ComparisionController comparisionCtrl){
		Debug.Log ("AnimationStepList");
		int column = comparisionCtrl.getTableColumnCount ();
		for(int i = 1;i<column;i++){
			animationStep (comparisionCtrl,i);
		}
	}
	public void animationStep (ComparisionController comparisionCtrl, int stepIndex){
		Debug.Log ("animationStep at column is "+stepIndex);
		//highlight existing numbers at column index 
		foreach (List<int?> numberRow in comparisionCtrl.numberLocationList){
			if (numberRow [stepIndex].HasValue) {
			}
		}
		//Display sum and carry by updating TableGO
		updateTableGO(comparisionCtrl.compareNumberLocationList(stepIndex));
	}
	// Update is called once per frame
	void Update () {
	
	}
}
