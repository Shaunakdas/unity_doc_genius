using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DivisiblityTestAnimator : MonoBehaviour {
	public GameObject NumberTableGO;
	public GameObject TextCellPrefab;
	public GameObject AnswerGO;
	public int InputNumber;
	public int div;
	public List<int?> singleNumberLocationList{ get; set; }
	// Use this for initialization
	void Start () {
		if (InputNumber > 0) {
			//for Debugging purpose
			initiateInputList(InputNumber);
		}
	}
	public void initiateInputList(int currInputNumber){
		DivisibilityTestController divisibilityTestCtrl = new DivisibilityTestController(currInputNumber);
		//Updating NumberLocationList with new inputList
		//Filling TableGO with Number at their location wrt straight Number Location List
		updateTableGO(divisibilityTestCtrl.singleNumberLocationList);
		NumberTableGO.GetComponent<UIGrid> ().maxPerLine = divisibilityTestCtrl.tableColumnCount;
		animationStepList (divisibilityTestCtrl);
	}
	public void updateTableGO(List<int?> singleNumberLocationList){
		NumberTableGO.transform.DestroyChildren ();
		GameObject inputNumberCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
		inputNumberCell.GetComponent<UILabel> ().text = InputNumber.ToString();
		GameObject equalSignCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
		equalSignCell.GetComponent<UILabel> ().text = "=";
		//Check if any int? has value, based on it, fill the cell
		foreach (int? number in singleNumberLocationList) {
			GameObject textCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
			if (number.HasValue)
				Debug.Log ("Number is" + number);
			textCell.GetComponent<UILabel>().text = number.ToString ();
		}

	}
	public void animationStepList(DivisibilityTestController divisibilityTestCtrl){
		Debug.Log ("AnimationStepList");
		int column = divisibilityTestCtrl.getTableColumnCount ();
		for(int i = 0;i<column;i++){
			animationStep (divisibilityTestCtrl,i);
		}
	}
	public void animationStep (DivisibilityTestController divisibilityTestCtrl, int stepIndex){
		Debug.Log ("animationStep at column is "+stepIndex);
		//highlight existing numbers at column index 
		List<int> columnIndexList = divisibilityTestCtrl.getHighlightIndexList(stepIndex);
		foreach (int index in columnIndexList){
			GameObject targetGO = NumberTableGO.transform.GetChild (index).gameObject;
			if (targetGO.GetComponent<UILabel> ().text.Length > 0) {
				//Got the targetGO

			}
		}
		//Display sum and carry by updating TableGO
		AnswerGO.GetComponent<UILabel>().text = divisibilityTestCtrl.message;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
