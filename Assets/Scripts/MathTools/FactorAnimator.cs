using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FactorAnimator : MonoBehaviour {
	public GameObject NumberTableGO;
	public GameObject AnswerGO;
	public GameObject TextCellPrefab;
	public int inputNumber;
	// Use this for initialization
	void Start () {
		if (inputNumber > 0) {
			//for Debugging purpose
			initiateinputNumber(inputNumber);
		}
	}
	public void initiateinputNumber(int inputNumber){
		FactorController factorCtrl = new FactorController(inputNumber);
		//Updating NumberLocationList with new inputList
		NumberTableGO.GetComponent<UIGrid> ().maxPerLine = 5;
		animationStepList (factorCtrl);
	}

	public void animationStepList(FactorController factorCtrl){
		Debug.Log ("AnimationStepList");
		int rows = factorCtrl.FactorStepCount (inputNumber);
		for(int i = 0;i<rows;i++){
			animationStep (factorCtrl,i);
		}
		string answer = "";
		answer += "Factors of " + inputNumber.ToString () + " : ";
		List<int> factorList = factorCtrl.Factor (inputNumber);
		factorList.Sort ();
		factorList.ForEach (factor => answer = answer + factor.ToString () + ", ");
		AnswerGO.GetComponent<UILabel> ().text = answer;
	}
	public void animationStep (FactorController factorCtrl, int stepIndex){
		Debug.Log ("animationStep at column is "+stepIndex);
		List<int> factorList = factorCtrl.Factor (inputNumber);
		//At every step 
		GameObject inputNumberCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
		inputNumberCell.GetComponent<UILabel> ().text = inputNumber.ToString();
		GameObject equalSignCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
		equalSignCell.GetComponent<UILabel> ().text = "=";
		GameObject firstFactorNumberCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
		firstFactorNumberCell.GetComponent<UILabel> ().text = factorList[2*stepIndex].ToString();
		GameObject productSignCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
		productSignCell.GetComponent<UILabel> ().text = "X";
		GameObject secondFactorNumberCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
		if(((2*stepIndex)+1)==factorList.Count)
			//Case where factor is root of inputNumber
			secondFactorNumberCell.GetComponent<UILabel> ().text = factorList[(2*stepIndex)].ToString();
		else
			secondFactorNumberCell.GetComponent<UILabel> ().text = factorList[(2*stepIndex)+1].ToString();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
