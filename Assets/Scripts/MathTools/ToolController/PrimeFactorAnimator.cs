using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PrimeFactorAnimator : MonoBehaviour {
	public GameObject NumberTableGO;
	public GameObject AnswerGO;
	public GameObject TextCellPrefab;
	public GameObject TableRowPrefab;
	public int inputNumber;

	// Use this for initialization
	void Start () {
		if (inputNumber > 0) {
			//for Debugging purpose
			initiateinputNumber(inputNumber);
		}
	}
	public void initiateinputNumber(int inputNumber){
		PrimeFactorController primeFactorCtrl = new PrimeFactorController(inputNumber);
		//Updating NumberLocationList with new inputList
		NumberTableGO.GetComponent<UIGrid> ().maxPerLine = 3;
		animationStepList (primeFactorCtrl);
	}

	public void animationStepList(PrimeFactorController primeFactorCtrl){
		Debug.Log ("AnimationStepList");
		primeFactorCtrl.primeFactorProcessList.ForEach (item => Debug.Log (item.ToString()));
		int processStepCount = primeFactorCtrl.primeFactorProcessCount;

		for(int i = 0;i<processStepCount;i++){
			animationStep (primeFactorCtrl,i);
		}
		string answer = "";
		answer += "Prime factors of " + inputNumber.ToString () + " : ";
		List<int> factorList = primeFactorCtrl.primeFactorProcessList [processStepCount - 1];;
		factorList.Sort ();
		factorList.ForEach (factor => answer = answer + factor.ToString () + ", ");
		AnswerGO.GetComponent<UILabel> ().text = answer;
	}
	public void animationStep (PrimeFactorController primeFactorCtrl, int stepIndex){
		Debug.Log ("animationStep at column is "+stepIndex);
		//At every step 
		GameObject inputNumberCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
		inputNumberCell.GetComponent<UILabel> ().text = inputNumber.ToString();
		GameObject equalSignCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
		equalSignCell.GetComponent<UILabel> ().text = "=";

		//Initiating Row for displaying all factors at current step
		List<int> currentRowFactorList = primeFactorCtrl.primeFactorProcessList[stepIndex];
		GameObject tableRowGO = NGUITools.AddChild (NumberTableGO, TableRowPrefab);
		tableRowGO.GetComponent<UIGrid>().maxPerLine = 2*(currentRowFactorList.Count)-1;
		GameObject firstFactorNumberCell = NGUITools.AddChild (tableRowGO, TextCellPrefab);
		firstFactorNumberCell.GetComponent<UILabel> ().text = currentRowFactorList[0].ToString();
		for (int i = 1; i < currentRowFactorList.Count; i++) {
			Debug.Log (i+" "+currentRowFactorList.Count);
			GameObject productSignCell = NGUITools.AddChild (tableRowGO, TextCellPrefab);
			productSignCell.GetComponent<UILabel> ().text = "X";
			GameObject nextFactorNumberCell = NGUITools.AddChild (tableRowGO, TextCellPrefab);
			nextFactorNumberCell.GetComponent<UILabel> ().text = currentRowFactorList[i].ToString();;
		}

	}
	// Update is called once per frame
	void Update () {
	
	}
}
