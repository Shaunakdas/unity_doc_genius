using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UpdateTextFromButton : MonoBehaviour {
	public enum InputType{InputSingleCorrect,InputMultipleCorrect,Toggle};
	public string fullText;
	public GameObject targetInputObject;
	public GameObject currentQuestionStep;
	public List<GameObject> targetInputObjectList;
	public GameObject QuestionGridScrollView;
	public GameObject QuestionGrid;
	// Use this for initialization
	void Start () {
		//Starting with blank text
		fullText = "";


//		currentQuestionStep = QuestionGrid.transform.GetChild (0).gameObject;
//		targetInputObject.GetComponent<TweenAlpha> ().Play ();
	}
	public void setKeyboard(){
		fullText = "";
		targetInputObjectList = gameObject.GetComponent<TextImporter> ().targetInputObjectList;
		if (targetInputObjectList.Count > 0) {
			//Initiating targetInputObject with first element of targetInputObjectList
			targetInputObject = targetInputObjectList [0];
		}
	}
	public void UpdateText(string number){
		//Checking for current status
		Debug.Log ("Adding number " + number + " ; Current Full Text" + getChildGameObject(targetInputObject,"Label").GetComponent<UILabel> ().text);
		//Adding numpad keyboard entry to existing text
		fullText += number;

		//Setting Label of targetInputObject based on numpad keyboard entry
		getChildGameObject(targetInputObject,"Label").GetComponent<UILabel> ().text = fullText;
	}
	public void ClearText(){
		Debug.Log ("Deleting last element ");
		if(fullText.Length>0) fullText = fullText.Substring(0,fullText.Length -1);
		getChildGameObject(targetInputObject,"Label").GetComponent<UILabel> ().text = fullText;
	}



	public void setTargetGameObject(GameObject currentGO){
		Debug.Log ("setTargetGameObject");
		//Setting targetInputObject if user clicks on any inputObject
		targetInputObject.GetComponent<TweenAlpha> ().enabled= false;
		if (targetInputObject.transform.parent.gameObject.GetComponent<InputParentValidator> ().childButtonType != InputParentValidator.InputType.Toggle) {
			targetInputObject = currentGO;
			targetInputObject.GetComponent<TweenAlpha> ().Play ();
			Debug.Log ("Text of touched label is " + getChildGameObject (targetInputObject, "Label").GetComponent<UILabel> ().text);
		}
	}


	public Solution UserSolution(){
		Debug.Log ("UserSolution");
		Solution userSolution = new Solution();
		Debug.Log ("Validating Text");
		if (targetInputObject.transform.parent.gameObject.GetComponent<InputParentValidator> ().childButtonType == InputParentValidator.InputType.InputMultipleCorrect) {
			//Validating Input with MultipleCorrect List
			List<string> correctValueList = targetInputObject.transform.parent.gameObject.GetComponent<InputParentValidator> ().correctValueList;
			int userInputTextIndex = correctValueList.IndexOf (fullText);
			if (userInputTextIndex > -1) {
				userSolution.correctMatch = true;
				userSolution.correctAnswer = fullText;
				correctValueList.RemoveAt (userInputTextIndex);
			} else {
				userSolution.correctMatch = false;
				userSolution.correctAnswer = correctValueList[0].ToString();
			}
		} else if(targetInputObject.transform.parent.gameObject.GetComponent<InputParentValidator> ().childButtonType == InputParentValidator.InputType.Toggle){
			//Validating Toggle Button List
			List<bool> correctBoolList = targetInputObject.transform.parent.gameObject.GetComponent<InputParentValidator> ().correctToggleList;
			int currentInputIndex = targetInputObjectList.IndexOf (targetInputObject);
			int counter = 0;
			//Updating ToggleButton List with correct Toggle
			while (targetInputObjectList [currentInputIndex+counter].transform.parent.parent == targetInputObject.transform.parent.parent) {
				targetInputObjectList [currentInputIndex + counter].GetComponent<UIToggle> ().value = correctBoolList [counter];
				counter++;
			}
		} else {
			userSolution.correctMatch = (targetInputObject.GetComponent<InputValidator> ().correctValue.ToString () == fullText);
			userSolution.correctAnswer = targetInputObject.GetComponent<InputValidator> ().correctValue.ToString ();
		}
		return userSolution;
	}

	public void PostValidation(){
		Debug.Log ("PostValidation");
		Solution finalSolution = new Solution ();
		finalSolution = UserSolution ();
		if  (finalSolution.correctMatch){
			//User answered correctly
		} else {
			//User answered incorrectly
			getChildGameObject(targetInputObject,"Label").GetComponent<UILabel> ().text = finalSolution.correctAnswer;
		}
		answerCompleteAnimation (targetInputObject);
		//Checking if we are on last element of questionStepList
		if (!IsQuestionFinished()){
			//Checking if we need to go to next step or stay in same step. based on where does next inputObject will come.
			if (checkForNextStep ())
				//Moving to next question step
				moveToNextQuestionStep ();
			//Checking if new question step contains a inputObject or not. 
			if (checkIfCurrentQuestionStepContainsInput())
				//If new question step contains inputObject, currentInputObject should go to next item on list
				nextTargetGameObject ();


		}
	}
	public bool checkIfCurrentQuestionStepContainsInput(){
		GameObject nextInputObject = targetInputObjectList [targetInputObjectList.IndexOf (targetInputObject) + 1];
		bool output = (nextInputObject.transform.parent.parent.parent == currentQuestionStep.transform);
		Debug.Log("Output of checkIfCurrentQuestionStepContainsInput "+ output+nextInputObject.transform.parent.parent.parent.parent.gameObject.name);
		return (nextInputObject.transform.parent.parent.parent == currentQuestionStep.transform);
	}
	public bool checkForNextStep(){
		Debug.Log ("checkForNextStep");
		if (checkIfCurrentQuestionStepContainsInput()) {
			//currentQuestionStep contains an inputObject
			GameObject currentInputObject = targetInputObject;
			GameObject nextInputObject = targetInputObjectList [targetInputObjectList.IndexOf (targetInputObject) + 1];
			if (currentInputObject.transform.parent.parent == nextInputObject.transform.parent.parent) {
				//next targetInput is in same questionstep
				return false;
			} else
				//next targetInput is in next questionstep
				return true;
		} else
			//currentQuestionStep doesn't contain an inputObject, so no need to check if next inputObject is in same question step
			return true;
		
	}
	public bool IsQuestionFinished(){
		Debug.Log ("IsQuestionFinished");
		//checking if any InputObject is left
		return currentQuestionStep == QuestionGrid.transform.GetChild(QuestionGrid.transform.childCount - 1);
	}
	public void moveToNextQuestionStep(){
		Debug.Log ("moveToNextQuestionStep");
		//Getting position of QuestionBubble on QuestionGrid
		int position = QuestionGrid.GetComponent<UIGrid> ().GetIndex (currentQuestionStep.transform);
		Debug.Log ("Position of QuestionBubble in QuestionGrid" + position);
		float cellHeight = QuestionGrid.GetComponent<UIGrid> ().cellHeight;
		//Moving to next QuestionBubble with animation
		SpringPanel.Begin (QuestionGridScrollView.gameObject, new Vector3 (0f, (position)*cellHeight, 0f), 8f);
		currentQuestionStep = QuestionGrid.transform.GetChild (position + 1).gameObject;
	}
	public void nextTargetGameObject(){
		Debug.Log ("nextTargetGameObject"+targetInputObjectList.IndexOf (targetInputObject));
		targetInputObject.GetComponent<TweenAlpha> ().enabled = false;
		//Setting next TargetInputObject
		if (targetInputObject.transform.parent.gameObject.GetComponent<InputParentValidator> ().childButtonType == InputParentValidator.InputType.Toggle) {
			//Current InputObject is from Toggle Button List
			int currentInputIndex = targetInputObjectList.IndexOf (targetInputObject);
			int counter = 0;
			//Updating ToggleButton List with correct Toggle
			while (targetInputObjectList [currentInputIndex + counter].transform.parent.parent == targetInputObject.transform.parent.parent) {
				counter++;
			}
			targetInputObject = targetInputObjectList [targetInputObjectList.IndexOf (targetInputObject) + counter];
		} else {
			//Not a toggle group
			targetInputObject = targetInputObjectList [targetInputObjectList.IndexOf (targetInputObject) + 1];
		}

		//Setting Animation for next TargetInputObject
		if (targetInputObject.transform.parent.gameObject.GetComponent<InputParentValidator> ().childButtonType == InputParentValidator.InputType.Toggle) {
			int currentInputIndex = targetInputObjectList.IndexOf (targetInputObject);
			int counter = 0;
			//Updating ToggleButton List with correct Toggle
			while (targetInputObjectList [currentInputIndex + counter].transform.parent.parent == targetInputObject.transform.parent.parent) {
				targetInputObjectList [currentInputIndex + counter].GetComponent<TweenAlpha> ().PlayForward ();
				counter++;
			}
		} else {
			targetInputObject.GetComponent<TweenAlpha> ().PlayForward ();
		}
		fullText = "";
	}
	// Update is called once per frame
	void Update () {
		
	}
	static public GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		//Author: Isaac Dart, June-13.
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}
	void answerCompleteAnimation(GameObject inputObject){
		Debug.Log ("answerCompleteAnimation");
		inputObject.GetComponent<UISprite> ().enabled = false;
	}
}
public class Solution{
	public bool correctMatch{ get; set; }
	public string correctAnswer{ get; set; }
	public Solution(){
		correctMatch = false;
		correctAnswer = "";
	}
}