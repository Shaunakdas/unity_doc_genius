using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UserInputValidator : MonoBehaviour {
	GameObject currentInputObject,currentQuestionStep,currentLine;
	string currentInputTag;
	public List<GameObject> targetInputObjectList;
	int questionStepIndex = 0;
	// Use this for initialization
	void Start () {
		

	}
	public void BodyLoaded(){
		initiateQuestionStep(0);
	}

	//Initiation Scripts
	public void initiateQuestionStep(int index){
		if (gameObject.GetComponent<TextImporter> ().getQuestionStep (index)) {
			targetInputObjectList = gameObject.GetComponent<TextImporter> ().targetInputObjectList;
			currentQuestionStep = gameObject.GetComponent<TextImporter> ().QuestionStepListGO.transform.GetChild(0).GetChild (index).gameObject;
			currentQuestionStep.transform.parent.gameObject.GetComponent<UIGrid> ().Reposition();
			if (targetInputObjectList.Count > 0) {
				//Current Question Step contains an input
				initiateInput ();

			}
			gameObject.GetComponent<UpdateTextFromButton> ().setKeyboard ();


			if (index > 0) {
				//GameObject QuestionGridView = currentQuestionStep.transform.parent.parent.gameObject;
				float cellHeight = currentQuestionStep.transform.parent.gameObject.GetComponent<UIGrid> ().cellHeight;
				//Moving to next QuestionBubble with animation
				Debug.Log("SpringPanel Animation"+(index) * cellHeight);
				SpringPanel.Begin (currentQuestionStep.transform.parent.parent.gameObject, new Vector3 (0f, (index) * cellHeight, 0f), 4f);
			}

		} else
			EndOfQuestionAnimation ();
	}
	public void initiateInput(){
		//Initiating input
		currentInputObject = targetInputObjectList[0];

		//Checking if question is single correct or multiple correct
//		setRelatedGameObjects(currentInputObject);
		Debug.Log(currentQuestionStep.transform.GetSiblingIndex());
		if (currentQuestionStep.GetComponent<QuestionStep>().SingleCorrect) {
			Debug.Log ("Activating single Inputs");
			getSprite(currentInputObject).GetComponent<TweenAlpha> ().PlayForward ();
		} else {
			foreach (GameObject inputObject in targetInputObjectList) {
				Debug.Log ("Activating multiple Inputs"+getSprite(inputObject).gameObject.GetComponent<TweenAlpha> ().style.ToString());
				getSprite(inputObject).GetComponent<TweenAlpha> ().PlayForward();
			}
		}
	}


	// Update is called once per frame
	void Update () {
		
	}

	//Button Triggers
	public void SubmitButtonTrigger(){
		Debug.Log ("Submit Buton Trigger");
		if (targetInputObjectList.Count > 0) {
			//Wait for solution if incorrect
			ValidateQuestionStep ();
		}
		if (targetInputObjectList.Count == 0) {
			//If input List is finished go to next Question Step
			nextButtonTrigger ();
		} else {
			initiateInput ();
		}
	}
	public void nextButtonTrigger(){
		questionStepIndex++;
		initiateQuestionStep (questionStepIndex);
	}
	public void EndOfQuestionAnimation (){
		Debug.Log ("Question Finished");
	}


	//Validating Scripts
	public void ValidateQuestionStep(){
		Debug.Log ("Validating Question Step");
		if (currentQuestionStep.GetComponent<QuestionStep>().SingleCorrect) {
			ValidateInput (currentInputObject);
			removeInputFromList (currentInputObject);
		} else {
			foreach (GameObject inputObject in targetInputObjectList) {
				ValidateInput (inputObject);
			}
			targetInputObjectList.Clear ();
		}
	}
	public void ValidateInput(GameObject input){
		Debug.Log ("Validating Input");
		switch (input.tag) {

		case "TextInput":
			input.GetComponent<TextInput> ().postValidation();
			break;
		case "Toggle":
			Debug.Log ("Validating Toggle");
			input.GetComponent<ToggleButton> ().postValidation();
			break;
		case "DropZone":
			input.GetComponent<DropZone> ().postValidation();
			break;
		case "DragSource":
//			input.GetComponent<DragSource> ().postValidation();
			break;
		case "NumLineCrossing":
			//Debug.Log ("NumLineCrossing Sprite"+input.gameObject.name);
			input.GetComponent<NumLineCrossing> ().postValidation ();
			break;
		}

	}
	public void removeInputFromList(GameObject input){
		targetInputObjectList.Remove(input);
		gameObject.GetComponent<TextImporter> ().targetInputObjectList.Remove(input);
		gameObject.GetComponent<UpdateTextFromButton> ().setKeyboard ();
	}


	public void setRelatedGameObjects(GameObject input){
		currentInputTag = input.tag;
		switch (input.tag) {

		case "TextInput":
			currentQuestionStep = input.GetComponent<TextInput> ().QuestionStepGO;
			break;
		case "ToggleButton":
			currentQuestionStep = input.GetComponent<ToggleButton> ().QuestionStepGO;
			break;
		case "DropZone":
			currentQuestionStep = input.GetComponent<DropZone> ().QuestionStepGO;
			break;
		case "DragSource":
			currentQuestionStep = input.GetComponent<DragSource> ().QuestionStepGO;
			break;
		case "NumLineCrossing":
			currentQuestionStep = input.GetComponent<NumLineCrossing> ().QuestionStepGO;
			break;
		}
	}
	public GameObject getSprite(GameObject input){
		currentInputTag = input.tag;
		switch (input.tag) {

		case "TextInput":
			return input.GetComponent<TextInput> ().currentSprite.gameObject;

		case "ToggleButton":
			return input.GetComponent<ToggleButton> ().currentSprite.gameObject;

		case "DropZone":
			return input.GetComponent<DropZone> ().ContainerGO;
		
		case "DragSource":
			return input.GetComponent<DragSource> ().currentSprite.gameObject;
		
		case "NumLineCrossing":
			//Debug.Log ("NumLineCrossing Sprite"+input.gameObject.name);
			return input.GetComponent<NumLineCrossing> ().ContainerGO;
		default:
			return input.GetComponent<UISprite> ().gameObject;
		}

	}

}
