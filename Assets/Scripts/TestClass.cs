using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestClass : Button {

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick (eventData);
	}
		
//	public void onclick()
//	{
//		Debug.Log ("Arslan::onclick");
//		AdHandler.GetInstance().showInterstatial ();
//	}
//
//	public void onclick1()
//	{
//		Debug.Log ("Arslan::onclick1");
//		AdHandler.GetInstance().showInterstatial ();
//
//	}
//
//	public void onclick2()
//	{
//		Debug.Log ("Arslan::onclick2");
//		AdHandler.GetInstance().showAdmobBanner ();
//	}
//
//	public void onclick3()
//	{
//		Debug.Log ("Arslan::onclick3");
//		AdHandler.GetInstance().hideAdmobBanner ();
//	}
}
