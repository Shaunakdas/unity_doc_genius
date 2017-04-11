using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class HCFAnimator : MonoBehaviour {
	public GameObject NumberTableGO;
	public GameObject AnswerGO,MessageGO;
	public GameObject TextCellPrefab;
	public GameObject TableRowPrefab;
	public List<int> inputList;
	public List<int> commonFactorList;
	public List<int> commonFactorMinCountList;
	public List<List<int>> inputFactorList;
	public List<List<GameObject>> inputFactorGOList;

	// Use this for initialization
	void Start () {
		if (inputList.Count > 0) {
			//for Debugging purpose
			initiateinputNumber(inputList);
		}

	}
	public void initiateinputNumber(List<int> currInputList){
		HCFController HCFCtrl = new HCFController(currInputList);
		//Updating NumberLocationList with new inputList
		inputFactorGOList = new List<List<GameObject>>();
		inputFactorList = HCFCtrl.inputFactorList;
		NumberTableGO.GetComponent<UIGrid> ().maxPerLine = 3;
		animationStepList (HCFCtrl);
	}

	public void animationStepList(HCFController HCFCtrl){
		Debug.Log ("AnimationStepList");
		//Generating prime factors for each input in input list
		for(int i = 0;i<inputList.Count;i++){
			primeFactorAnimationStep (HCFCtrl,i);
		}
		commonFactorMinCountList = new List<int> ();
		commonFactorList = inputFactorList.SelectMany (d => d).ToList ().Distinct ().ToList ();
		//Highlighting prime factors for each input in input list
		foreach(int prime in commonFactorList){
			checkPrimeFactorPowerAnimationStep (HCFCtrl,prime);
		}
		generateHCF ();
//		checkPrimeFactorPowerAnimationStep (HCFCtrl,2);
//		string answer = "";
//		answer += "Prime factors of " + inputNumber.ToString () + " : ";
//		List<int> factorList = HCFCtrl.primeFactorProcessList [processStepCount - 1];;
//		factorList.Sort ();
//		factorList.ForEach (factor => answer = answer + factor.ToString () + ", ");
//		AnswerGO.GetComponent<UILabel> ().text = answer;
	}
	public void primeFactorAnimationStep (HCFController HCFCtrl, int stepIndex){
		List<GameObject> primeFactorRowGo = new List<GameObject> ();
		Debug.Log ("animationStep at column is "+stepIndex);
		//At every step 
		GameObject inputNumberCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
		inputNumberCell.GetComponent<UILabel> ().text = inputList[stepIndex].ToString();
		GameObject equalSignCell = NGUITools.AddChild (NumberTableGO, TextCellPrefab);
		equalSignCell.GetComponent<UILabel> ().text = "=";

		//Initiating Row for displaying all factors at current step
		List<int> currentRowFactorList = HCFCtrl.inputFactorList[stepIndex];
		GameObject tableRowGO = NGUITools.AddChild (NumberTableGO, TableRowPrefab);
		tableRowGO.GetComponent<UIGrid>().maxPerLine = 2*(currentRowFactorList.Count)-1;
		GameObject firstFactorNumberCell = NGUITools.AddChild (tableRowGO, TextCellPrefab);
		primeFactorRowGo.Add (firstFactorNumberCell);
		firstFactorNumberCell.GetComponent<UILabel> ().text = currentRowFactorList[0].ToString();
		for (int i = 1; i < currentRowFactorList.Count; i++) {
			Debug.Log (i+" "+currentRowFactorList.Count);
			GameObject productSignCell = NGUITools.AddChild (tableRowGO, TextCellPrefab);
			productSignCell.GetComponent<UILabel> ().text = "X";
			GameObject nextFactorNumberCell = NGUITools.AddChild (tableRowGO, TextCellPrefab);
			nextFactorNumberCell.GetComponent<UILabel> ().text = currentRowFactorList[i].ToString();
			primeFactorRowGo.Add (nextFactorNumberCell);
		}
		inputFactorGOList.Add (primeFactorRowGo);
	}
	public void checkPrimeFactorPowerAnimationStep (HCFController HCFCtrl, int prime){
		List<List<int>> primeHighlightIndexList = HCFCtrl.getPrimeHighlightIndexList(inputFactorList,prime);
		for (int i = 0; i < inputFactorGOList.Count; i++) {
			List<int> highlightRow = new List<int> ();
			for (int j = 0; j < inputFactorGOList [i].Count; j++) {
				if (primeHighlightIndexList [i].IndexOf (j) > 0) {
					//Highligh inputFactorGOList[i][j]
				}
			}
			primeHighlightIndexList.Add (highlightRow);
		}
		int minCount = HCFCtrl.getPrimeMinimumCount (inputFactorList, prime);
		string message = "Minimum amout of factor " + prime + ": is " + minCount;
		for (int i = 0; i < minCount; i++) {
			commonFactorMinCountList.Add (prime);
		}
		MessageGO.GetComponent<UILabel>().text = message;
		string answerText = "";

		commonFactorMinCountList.ForEach (item => answerText = answerText + item.ToString () + " X ");
		AnswerGO.GetComponent<UILabel>().text = answerText;
	}
	public int generateHCF (){
		int HCF = 1;
		commonFactorMinCountList.ForEach (item => HCF = HCF*item);
		string answerMessage = AnswerGO.GetComponent<UILabel> ().text;
		AnswerGO.GetComponent<UILabel> ().text = answerMessage + "1 = "+HCF.ToString ();
		return HCF;
	}
	public void updateAnswerGO(GameObject AnswerGO){

	}
	// Update is called once per frame
	void Update () {
	
	}
}
