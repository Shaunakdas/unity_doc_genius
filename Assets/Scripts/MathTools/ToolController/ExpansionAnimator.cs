using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ExpansionAnimator : MonoBehaviour {
	public GameObject NumberTableGO;
	public GameObject TextCellPrefab;
	public GameObject AnswerGO;
	public int InputNumber;
	public List<int?> singleNumberLocationList{ get; set; }
	// Use this for initialization
	void Start () {
			//for Debugging purpose
			initiateInputList(InputNumber);
	}
	public void initiateInputList(int currInputNumber){
		ExpansionController expansionCtrl = new ExpansionController(currInputNumber);
		//Updating NumberLocationList with new inputList
		//Filling TableGO with Number at their location wrt straight Number Location List
		singleNumberLocationList = expansionCtrl.singleNumberLocationList;
		updateTableGO(singleNumberLocationList);
		NumberTableGO.GetComponent<UIGrid> ().maxPerLine = expansionCtrl.tableColumnCount;
		animationStepList (expansionCtrl);
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
			if (number.HasValue) {
				Debug.Log ("Number is" + number);
				textCell.GetComponent<UILabel> ().text = number.ToString ();
			} else {
				textCell.GetComponent<UILabel> ().text = ",";
			}
		}

	}
	public void animationStepList(ExpansionController expansionCtrl){
		Debug.Log ("AnimationStepList"+expansionCtrl.getCommaCount());
		int column = Mathf.Min(expansionCtrl.comparisionTermIndexList.Count,expansionCtrl.getCommaCount());
		for(int i = 0;i<column;i++){
			commaAnimationStep (expansionCtrl,i);
			updateAnswerGO ();
		}
		AnswerGO.GetComponent<UILabel> ().text = HumanFriendlyInteger.NumberToWords (InputNumber);
	}
	public void commaAnimationStep (ExpansionController expansionCtrl, int stepIndex){
		Debug.Log ("animationStep at column is "+stepIndex);
		//highlight existing numbers at column index 
		GameObject targetGO = NumberTableGO.transform.GetChild (0).gameObject;
		int comparisionTermIndex = expansionCtrl.comparisionTermIndexList [stepIndex];
		//Display sum and carry by updating TableGO
		singleNumberLocationList = expansionCtrl.updateNumberLocationList(stepIndex);
		updateTableGO(singleNumberLocationList);
	}
	public void updateAnswerGO (){
		string number = "";
		List<int?> fullTextLocationList = new List<int?> (singleNumberLocationList);
		fullTextLocationList.RemoveAt (0);
		singleNumberLocationList.ForEach (item => number += toText(item));
		AnswerGO.GetComponent<UILabel> ().text = HumanFriendlyInteger.NumberToWords (int.Parse (number));
	}
	public string toText(int? item){
		if (item.HasValue)
			return item.ToString ();
		else
			return "";
	}

	// Update is called once per frame
	void Update () {
	
	}
}
