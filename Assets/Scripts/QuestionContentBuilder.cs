using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HtmlAgilityPack;

public class QuestionContentBuilder : MonoBehaviour {
	//Prefabs
	public GameObject StepQuestionLinePF;

	// Use this for initialization
	void Start () {
//		GameObject quesContentLine = Instantiate (StepQuestionLinePF,this.transform ) as GameObject;
//		quesContentLine.GetComponent<Text> ().text = "Finally, find out sum of predecessor and successor of 101";
		var html = new HtmlDocument();
		html.LoadHtml(@"<html><body><p id='haha'>Lets find out predecessor of 101</p><p>i.e. Subtract 1 from 101</p><p> = 101-1</p><p>=<input id='i1'/></p></html></body>");
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].InnerHtml);
//		Debug.Log (html.DocumentNode.SelectNodes("//p")[0].InnerHtml);
//		Debug.Log (html.DocumentNode.SelectNodes("//p")[1].InnerHtml);
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].ChildNodes[0].Attributes.ToString());
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].ChildNodes[0].OuterHtml);
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].ChildNodes[0].Name);
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].ChildNodes[0].InnerText);
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].ChildNodes[0].InnerHtml);
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].ChildNodes[0].Id);
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].ChildNodes[0].GetType().ToString());
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].ChildNodes[0].Attributes[0].ToString());
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].ChildNodes[0].Attributes[0].Name);
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].ChildNodes[0].Attributes[0].GetType().ToString());
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].ChildNodes[0].Attributes[0].Value);
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].ChildNodes[0].ChildAttributes("//id").ToString());
		Debug.Log (html.DocumentNode.SelectNodes("//body")[0].SelectNodes("//p")[1].InnerHtml);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
