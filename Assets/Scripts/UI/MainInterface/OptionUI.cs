using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : MonoBehaviour {

	//在Edit中设置Object，在Awake中设置脚本
	public GameObject UIManagerObject;
	private InterfaceUIManager UIManager;

	private void Awake()
	{
		UIManager = UIManagerObject.GetComponent<InterfaceUIManager>();
	}


	public void OnBack()
	{
		UIManager.StartUI.SetActive(true);
		gameObject.SetActive(false);
	}

}
