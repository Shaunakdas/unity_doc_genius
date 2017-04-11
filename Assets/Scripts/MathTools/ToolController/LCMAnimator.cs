using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class LCMAnimator : HCFAnimator {

	// Use this for initialization
	void Start () {
		if (inputList.Count > 0) {
			//for Debugging purpose
			initiateinputNumber(base.inputList);
		}

	}
	public void initiateinputNumber(List<int> currInputList){
		LCMController LCMCtrl = new LCMController(currInputList);
		//Updating NumberLocationList with new inputList
		base.inputFactorGOList = new List<List<GameObject>>();
		base.inputFactorList = LCMCtrl.inputFactorList;
		base.NumberTableGO.GetComponent<UIGrid> ().maxPerLine = 3;
		animationStepList (LCMCtrl);
	}

	public void animationStepList(LCMController LCMCtrl){
		Debug.Log ("AnimationStepList");
		//Generating prime factors for each input in input list
		for(int i = 0;i<inputList.Count;i++){
			primeFactorAnimationStep (LCMCtrl,i);
		}
		commonFactorMinCountList = new List<int> ();
		commonFactorList = inputFactorList.SelectMany (d => d).ToList ().Distinct ().ToList ();
		//Highlighting prime factors for each input in input list
		foreach(int prime in commonFactorList){
			checkPrimeFactorPowerAnimationStep (LCMCtrl,prime);
		}
		generateHCF ();
	}
	public void checkPrimeFactorPowerAnimationStep (LCMController LCMCtrl, int prime){
		List<List<int>> primeHighlightIndexList = LCMCtrl.getPrimeHighlightIndexList(inputFactorList,prime);
		for (int i = 0; i < inputFactorGOList.Count; i++) {
			List<int> highlightRow = new List<int> ();
			for (int j = 0; j < inputFactorGOList [i].Count; j++) {
				if (primeHighlightIndexList [i].IndexOf (j) > 0) {
					//Highligh inputFactorGOList[i][j]
				}
			}
			primeHighlightIndexList.Add (highlightRow);
		}
		int minCount = LCMCtrl.getPrimeMaximumCount (inputFactorList, prime);
		string message = "Maxiimum amout of factor " + prime + ": is " + minCount;
		for (int i = 0; i < minCount; i++) {
			commonFactorMinCountList.Add (prime);
		}
		MessageGO.GetComponent<UILabel>().text = message;
		string answerText = "";

		commonFactorMinCountList.ForEach (item => answerText = answerText + item.ToString () + " X ");
		AnswerGO.GetComponent<UILabel>().text = answerText;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
