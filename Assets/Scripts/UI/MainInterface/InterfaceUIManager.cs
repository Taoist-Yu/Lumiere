using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceUIManager : MonoBehaviour {

	//Edit中设置
	public GameObject StartUI;
	public GameObject LevelUI;
	public GameObject OptionUI;

	private void OnEnable()
	{
		InterfaceUIReset();
	}

	private void InterfaceUIReset()
	{
		StartUI.SetActive(true);
		LevelUI.SetActive(false);
		OptionUI.SetActive(false);
	}

}
