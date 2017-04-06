using UnityEngine;
using System.Collections;
using HtmlAgilityPack;
using System.Collections.Generic;
public class TextImporter : MonoBehaviour {
	public GameObject QuestionStepListPrefab;
	public GameObject QuestionStepPrefab;
	public GameObject TextLinePrefab,LatexLinePrefab,TableLinePrefab,NumLineSelectableLinePrefab,NumLineDropLinePrefab,NumLineStraightPrefab,NumLineCrossPrefab,DragSourceLinePrefab,NumLineCrossTogglePrefab;
	public GameObject TextCellPrefab,LatexCellPrefab,CheckboxCellPrefab,NumberInputCellPrefab,DropZoneCellPrefab,DragSourceCellPrefab,SelectableButtonCellPrefab;
	public List<GameObject> targetInputObjectList;
	public int BodyIndex=0;
	public GameObject QuestionStepListGO;HtmlNodeCollection question_step_list;
	string body_tag = "body";
	string question_step_tag = "p";
	string line_tag = "line";
	string cell_tag = "cell";
	string attr_type = "type";
	string attr_answer = "answer";
	string attr_correct_type = "correctType";
	string attr_label_count = "labelCount";
	string attr_label_index = "labelIndex";
	IEnumerator LoadInventory() {


		yield return new WaitForEndOfFrame();
		GameObject QuestionStepListGO = (GameObject)Instantiate(QuestionStepListPrefab,transform);
		QuestionStepListGO.transform.localScale = new Vector3 (1f, 1f, 1f);
		GameObject QuestionStepGO = (GameObject)Instantiate(QuestionStepPrefab,QuestionStepListGO.transform.GetChild(0));
		QuestionStepGO.transform.localScale = new Vector3 (1f, 1f, 1f);
		QuestionStepListGO.transform.GetChild (0).gameObject.GetComponent<UIGrid> ().Reposition();
	}


	// Use this for initialization
	void Start () {

		string text = System.IO.File.ReadAllText (@"Assets/Data/class_6_list.html");
		var html = new HtmlDocument ();
		html.LoadHtml (@text);
		HtmlNodeCollection question_list = html.DocumentNode.SelectNodes ("//" + body_tag);
		Debug.Log ("There are " + question_list.Count + " nodes of type: body");


		HtmlNode body = question_list [BodyIndex];
		if (body.InnerHtml.ToString ().Length > 0) {
//		foreach (HtmlNode body in question_list) {
			//Iterating through each body node in html node

			question_step_list = body.ChildNodes;
			Debug.Log ("There are " + question_step_list.Count + " nodes of type: p");
			
			//Adding Grid GameObject "QuestionStepListPrefab" for whole scene

			QuestionStepListGO = instantiateNGUIGameObject (QuestionStepListPrefab, transform);
			//Initiating Input Element Setting
			gameObject.GetComponent<UserInputValidator> ().BodyLoaded ();
//			int index = 0;
//			foreach (HtmlNode question_step in question_step_list) {
//				//Iterating through each question_step node in body node
//				getQuestionStep (index);
//				index++;
//
//			}


		}
	}
			

	public bool getQuestionStep(int stepIndex){
		//Add Gameobject "QuestionStepPrefab"
		//				GameObject QuestionStepGO = (GameObject)Instantiate(QuestionStepPrefab,QuestionStepListGO.transform.GetChild(0));
		//				QuestionStepGO.transform.localScale = new Vector3 (1f, 1f, 1f);
		if (stepIndex < question_step_list.Count) {
			HtmlNode question_step = question_step_list [stepIndex];
			GameObject QuestionStepGO = instantiateNGUIGameObject (QuestionStepPrefab, QuestionStepListGO.transform.GetChild (0));
			if (question_step.Attributes [attr_type].Value == "answer_step" && question_step.Attributes [attr_correct_type].Value == "multiple_correct"){
				QuestionStepGO.GetComponent<QuestionStep> ().SingleCorrect = false;
				Debug.Log ("SingleCorrect false");
			}else
				QuestionStepGO.GetComponent<QuestionStep> ().SingleCorrect = true;
			HtmlNodeCollection line_list = question_step.ChildNodes;
			Debug.Log ("There are " + line_list.Count + " nodes of type: line");

			foreach (HtmlNode line in line_list) {
				//					Debug.Log ("line.InnerHtml:  " + line.InnerHtml);
				//Iterating through each line node in question_step node
				Debug.Log("Line inner html is "+line.InnerHtml);
				switch (line.Attributes [attr_type].Value) {
				case "text":
					Debug.Log ("Adding line of type text with content" + line.InnerText);
				//Add Gameobject "TextLinePrefab" of content line.InnerText
					GameObject TextLineGO = instantiateNGUIGameObject (TextLinePrefab, getChildGameObject (QuestionStepGO, "QuestionStepTable").transform);
					TextLineGO.GetComponent<UILabel> ().text = line.InnerText;
					break;

				case "text_tex":
					Debug.Log ("Adding line of type Latex text with content" + line.InnerText);
				//Add TexDraw Gameobject "LatexLinePrefab" of content line.InnerText
					GameObject LatexLineGO = instantiateNGUIGameObject (LatexLinePrefab, getChildGameObject (QuestionStepGO, "QuestionStepTable").transform);
					LatexLineGO.GetComponent<TEXDrawNGUI> ().text = line.InnerText;
				//						TextLineGO.GetComponent<TEXDrawNGUI>().alignment = TextAlignme
					break;
				case "table":
					Debug.Log ("Adding table");

					HtmlNodeCollection cell_list = line.ChildNodes;
					Debug.Log ("There are " + cell_list.Count + " nodes of type: cell");
				//Add Gameobject "TableLinePrefab" of column count cell_list.Count
					GameObject TableLineGO = instantiateNGUIGameObject (TableLinePrefab, getChildGameObject (QuestionStepGO, "QuestionStepTable").transform);
					TableLineGO.GetComponent<UITable> ().columns = cell_list.Count;

					foreach (HtmlNode cell in cell_list) {
						//Iterating through each cell node in line node
						switch (cell.Attributes [attr_type].Value) {
						case "text":
						//Add Gameobject "TextCellPrefab" of content cell.innerText
							Debug.Log ("Ading Label with content " + cell.InnerText);
							GameObject TextCellGO = instantiateNGUIGameObject (TextCellPrefab, TableLineGO.transform);
							TextCellGO.GetComponent<UILabel> ().text = cell.InnerText;

							break;

						case "text_tex":
						//Add Gameobject "LatexCellPrefab" of content cell.innerText
							Debug.Log ("Ading TexDraw with content " + cell.InnerText);
							GameObject LatexCellGO = instantiateNGUIGameObject (LatexCellPrefab, TableLineGO.transform);
							LatexCellGO.GetComponent<UILabel> ().text = cell.InnerText;
							break;
						case "checkbox":
						//Add Gameobject "CheckboxCellPrefab" of answer cell.Attributes[attr_answer].Value
							Debug.Log ("Adding checkbox of Answer " + cell.Attributes [attr_answer].Value);
						//Adding Input buttons to Input List
							break;

						case "number_input":
						//Add Gameobject "NumberInputCellPrefab" of answer cell.Attributes[attr_answer].Value
							Debug.Log ("Adding number input of Answer " + cell.Attributes [attr_answer].Value);
							GameObject NumberInputCellGO = instantiateNGUIGameObject (NumberInputCellPrefab, TableLineGO.transform);
							getChildGameObject (NumberInputCellGO, "Label").GetComponent<UILabel> ().text = cell.InnerText;
//							NumberInputCellGO.GetComponent<InputValidator> ().correctValue = int.Parse (cell.Attributes [attr_answer].Value);
							NumberInputCellGO.GetComponent<TextInput>().correctInt = int.Parse (cell.Attributes [attr_answer].Value);
							NumberInputCellGO.GetComponent<TextInput>().QuestionStepGO = QuestionStepGO;

						//Adding Input buttons to Input List
							targetInputObjectList.Add (NumberInputCellGO);
							break;

						case "drop_zone":
						//Add Gameobject "DropZoneCellPrefab" of answer cell.Attributes[attr_answer].Value
							Debug.Log ("Adding checkbox of Answer " + cell.Attributes [attr_answer].Value);
//							GameObject DropZoneCellGO = instantiateNGUIGameObject (DropZoneCellPrefab, TableLineGO.transform);
							GameObject DropZoneCellGO = NGUITools.AddChild (TableLineGO,DropZoneCellPrefab);
							DropZoneCellGO.GetComponent<DropZone>().QuestionStepGO = QuestionStepGO;
							DropZoneCellGO.GetComponent<DropZone> ().actionRequired = true;
							DropZoneCellGO.GetComponent<DropZone> ().correctText = cell.Attributes [attr_answer].Value;
							targetInputObjectList.Add (DropZoneCellGO);
							//Adding Input buttons to Input List
							break;

						case "drag_source":
						//Add Gameobject "DragSourceCellPrefab" of answer cell.Attributes[attr_answer].Value
//							Debug.Log ("Adding checkbox of Answer " + bool.Parse(cell.Attributes [attr_answer].Value));
							GameObject DragSourceCellGO = instantiateNGUIGameObject (DragSourceCellPrefab, TableLineGO.transform);
							DragSourceCellGO.GetComponent<DragSource> ().LabelGO.GetComponent<UILabel> ().text = cell.InnerText;
						//Adding Input buttons to Input List
							break;

						case "selectable_button":
						//Add Gameobject "SelectableButtonCellPrefab" of answer cell.Attributes[attr_answer].Value
							Debug.Log ("Adding checkbox of Answer " + cell.Attributes [attr_answer].Value);
							GameObject SelectableButtonCellGO = instantiateNGUIGameObject (SelectableButtonCellPrefab, TableLineGO.transform);
//							getChildGameObject (SelectableButtonCellGO, "Label").GetComponent<UILabel> ().text = cell.InnerText;
						//								NumberInputCellGO.GetComponent<InputValidator> ().correctValue = cell.Attributes [attr_answer].Value;

							SelectableButtonCellGO.GetComponent<ToggleButton>().QuestionStepGO = QuestionStepGO;
							SelectableButtonCellGO.GetComponent<ToggleButton>().LabelGO.GetComponent<UILabel> ().text = cell.InnerText;
							SelectableButtonCellGO.GetComponent<ToggleButton>().correctBool = bool.Parse(cell.Attributes [attr_answer].Value);

						//Adding Input buttons to Input List
							targetInputObjectList.Add (SelectableButtonCellGO);
							break;

						default:
							Debug.Log ("Didn't cover scenario with content" + cell.InnerText);
							break;
						}
					}
					TableLineGO.GetComponent<UITable> ().Reposition ();
					break;

				case "number_line_selectable":
				//Add Gameobject "NumLineSelectableLinePrefab"
					Debug.Log ("Adding Selectable Number Line");
					GameObject NumLineSelectableLineGO = instantiateNGUIGameObject (NumLineSelectableLinePrefab, getChildGameObject (QuestionStepGO, "QuestionStepTable").transform);
				//Adding Input buttons to Input List
					GameObject NumLineToggleTableGO = NumLineSelectableLineGO.GetComponent<NumLineDrop> ().NumLineTableGO;

					HtmlNodeCollection numline_toggle_cell_list = line.ChildNodes;
					Debug.Log ("There are " + numline_toggle_cell_list.Count + " nodes of type: cell");

					int labelToggleCount = int.Parse (line.Attributes [attr_label_count].Value);
					NumLineToggleTableGO.GetComponent<UITable>().columns = 2*labelToggleCount;
					for (int i = 0; i < labelToggleCount; i++) {
						//Setting Number Line view
						GameObject NumLineCrossGO = instantiateNGUIGameObject (NumLineCrossTogglePrefab, NumLineToggleTableGO.transform);

						instantiateNGUIGameObject (NumLineStraightPrefab, NumLineToggleTableGO.transform);
						//Setting Number line input object list
						targetInputObjectList.Add(NumLineCrossGO.GetComponent<NumLineCrossing>().ToggleGO);
						NumLineCrossGO.GetComponent<NumLineCrossing>().ToggleGO.GetComponent<ToggleButton>().QuestionStepGO = QuestionStepGO;
					}


					foreach (HtmlNode cell in numline_toggle_cell_list) {
						//Iterating through each cell node in number_line_drop node
						switch (cell.Attributes [attr_type].Value) {
						case "number_line_label":
							//Add Gameobject "TextCellPrefab" of content cell.innerText
							Debug.Log ("Adding Label with content " + cell.InnerText);
							Debug.Log ("Adding Label at index " + int.Parse (cell.Attributes [attr_label_index].Value));
							targetInputObjectList [int.Parse (cell.Attributes [attr_label_index].Value)].GetComponent<ToggleButton>().LabelGO.GetComponent<UILabel>().text = cell.InnerText;
							break;
						case "number_line_label_answer":
							//Add Gameobject "TextCellPrefab" of content cell.innerText
							Debug.Log ("Adding Label with content " + cell.InnerText);
							Debug.Log ("Adding Label at index " + int.Parse (cell.Attributes [attr_label_index].Value));
							targetInputObjectList [int.Parse (cell.Attributes [attr_label_index].Value)].GetComponent<ToggleButton>().correctBool = bool.Parse(cell.InnerText);
							break;
						}
					}
					break;
				case "number_line_drop":
				//Add Gameobject "NumLineDropLinePrefab"
					Debug.Log ("Adding Dropable Number Line");
					GameObject NumLineDropLineGO = NGUITools.AddChild (getChildGameObject (QuestionStepGO, "QuestionStepTable"), NumLineDropLinePrefab);
					//GameObject NumLineDropLineGO = instantiateNGUIGameObject (NumLineDropLinePrefab, getChildGameObject (QuestionStepGO, "QuestionStepTable").transform);
					//Adding Input buttons to Input List
					GameObject NumLineTableGO = NumLineDropLineGO.GetComponent<NumLineDrop> ().NumLineTableGO;


					HtmlNodeCollection numline_drop_cell_list = line.ChildNodes;
					Debug.Log ("There are " + numline_drop_cell_list.Count + " nodes of type: cell");


					int labelCount = int.Parse (line.Attributes [attr_label_count].Value);
					NumLineTableGO.GetComponent<UITable>().columns = 2*labelCount;
					for (int i = 0; i < labelCount; i++) {
						//Setting Number Line view
						GameObject NumLineCrossGO = instantiateNGUIGameObject (NumLineCrossPrefab, NumLineTableGO.transform);
						NumLineCrossGO.GetComponent<NumLineCrossing> ().actionRequired = false;
						instantiateNGUIGameObject (NumLineStraightPrefab, NumLineTableGO.transform);
						//Setting Number line input object list
						targetInputObjectList.Add(NumLineCrossGO);
						NumLineCrossGO.GetComponent<NumLineCrossing>().QuestionStepGO = QuestionStepGO;

					}


					foreach (HtmlNode cell in numline_drop_cell_list) {
						//Iterating through each cell node in number_line_drop node
						switch (cell.Attributes [attr_type].Value) {
						case "number_line_label":
							//Add Gameobject "TextCellPrefab" of content cell.innerText
							Debug.Log ("Adding Label with content " + cell.InnerText);
							Debug.Log ("Adding Label at index " + int.Parse (cell.Attributes [attr_label_index].Value));
							Debug.Log (targetInputObjectList [int.Parse (cell.Attributes [attr_label_index].Value)].transform.GetSiblingIndex());
							//Debug.Log (targetInputObjectList [int.Parse (cell.Attributes [attr_label_index].Value)].GetComponent<NumLineCrossing>().LabelGO.transform.GetSiblingIndex());
							targetInputObjectList [int.Parse (cell.Attributes [attr_label_index].Value)].GetComponent<NumLineCrossing>().LabelGO.GetComponent<UILabel>().text =(cell.InnerText);
							break;
						case "number_line_label_answer":
							//Add Gameobject "TextCellPrefab" of content cell.innerText
							Debug.Log ("Adding Label with content " + cell.InnerText);
							Debug.Log ("Adding Label at index " + int.Parse (cell.Attributes [attr_label_index].Value));
							targetInputObjectList [int.Parse (cell.Attributes [attr_label_index].Value)].GetComponent<NumLineCrossing> ().actionRequired = true;
							targetInputObjectList [int.Parse (cell.Attributes [attr_label_index].Value)].GetComponent<NumLineCrossing>().correctInt = int.Parse(cell.InnerText);
							break;
						}

					}
					break;
				case "drag_source_line":
					Debug.Log ("Adding Drag Source Line");

					HtmlNodeCollection drag_cell_list = line.ChildNodes;
					Debug.Log ("There are " + drag_cell_list.Count + " nodes of type: cell");
				//Add Gameobject "DragSourceLinePrefab" of column count cell_list.Count
					GameObject DragSourceLineGO = instantiateNGUIGameObject (DragSourceLinePrefab, getChildGameObject (QuestionStepGO, "QuestionStepTable").transform);
					foreach (HtmlNode cell in drag_cell_list) {
						//Iterating through each cell node in line node
						switch (cell.Attributes [attr_type].Value) {
						case "text":
						//Add Gameobject "DragSourceCellPrefab" of label child with content cell.innerText
							Debug.Log ("Ading Label with content " + cell.InnerText);
							GameObject TextCellGO = instantiateNGUIGameObject (TextCellPrefab, getChildGameObject (DragSourceLineGO, "DragSourceTable").transform);
							TextCellGO.GetComponent<UILabel> ().text = cell.InnerText;

						//Adding UI Drag Scroll View and attach it to parent Scroll View
							TextCellGO.AddComponent<UIDragScrollView> ();
							TextCellGO.GetComponent<UIDragScrollView> ().scrollView = DragSourceLineGO.GetComponent<UIScrollView> ();

						//Adding Input buttons to Input List
							targetInputObjectList.Add (TextCellGO);
							break;

						case "text_tex":
						//Add Gameobject "DragSourceCellPrefab" of TexDraw child with content cell.innerText
							Debug.Log ("Ading TexDraw with content " + cell.InnerText);
							GameObject LatexCellGO = instantiateNGUIGameObject (LatexCellPrefab, getChildGameObject (DragSourceLineGO, "DragSourceTable").transform);
							LatexCellGO.GetComponent<UILabel> ().text = cell.InnerText;
						//Adding UI Drag Scroll View and attach it to parent Scroll View
						//Adding Input buttons to Input List
							break;

						default:
							Debug.Log ("Didn't cover scenario with content" + cell.InnerText);
							break;
						}
					}

					break;
				default:
					Debug.Log ("Didn't cover scenario with content" + line.InnerText);
					break;
				}

				Debug.Log ("Finishing Line"+line.InnerHtml);
			}
			Debug.Log ("Finished Line");
			return true;
		}else
			return false;
	}
	GameObject instantiateNGUIGameObject(GameObject prefabHolder,Transform parentTransform){
		GameObject currentGO = (GameObject)Instantiate(prefabHolder,parentTransform);
		currentGO.transform.localScale = new Vector3 (1f, 1f, 1f);
		return currentGO;
	}
	GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		//Author: Isaac Dart, June-13.
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
