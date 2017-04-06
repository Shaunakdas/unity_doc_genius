using UnityEngine;
using System.Collections;

public class ComparisionOptionButton : UIDragDropItem {
	public GameObject ComparisionHandler{ get; set; }
	public GameObject ReparentTarget{ get; set; }
	public int order;
	// Use this for initialization
	void Start () {
		order = 0;
	}
//	protected override void OnDragDropStart(){
//		gameObject.transform.parent = ReparentTarget;
//		base.OnDragDropStart ();
//	}
	protected  override void OnDragDropEnd ()
	{
		
		Debug.Log("OnDragDropEnd happened");
		if (gameObject.transform.parent.gameObject == ReparentTarget)
			ComparisionHandler.GetComponent<Comparision>().notifyDragDropEnd (gameObject);
		base.OnDragDropEnd ();
	}
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.parent.gameObject == ReparentTarget) {
			order = gameObject.transform.GetSiblingIndex ();
		}
	}
}
