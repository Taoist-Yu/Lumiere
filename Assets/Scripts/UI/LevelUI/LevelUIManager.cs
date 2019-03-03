using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : MonoBehaviour {

	public GameObject MainUI;
	public GameObject OptionUI;
	public GameObject ESCHandlerObject;
	public ESCHandler ESCHandlerInstance;

	private void OnEnable()
	{
		ESCHandlerInstance = ESCHandlerObject.GetComponent<ESCHandler>();
		LevelUIReset();
	}


	private void LevelUIReset()
	{
		MainUI.SetActive(true);
		OptionUI.SetActive(false);
	}

}
