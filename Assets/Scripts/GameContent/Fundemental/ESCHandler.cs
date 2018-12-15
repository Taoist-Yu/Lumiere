using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCHandler : MonoBehaviour {

	private bool isEscUI = false;

	//edit中赋值
	public GameObject ESCUI;
	public GameObject playerCamera;

	// Update is called once per frame
	void Update () {
		if (GetInput.ESC)
		{
			if(isEscUI == true)
			{
				ESCUIexit();
			}
			else
			{
				ESCUIEnter();
			}
		}
	}

	public void ESCUIEnter()
	{
		Time.timeScale = 0;
		isEscUI = true;
		ESCUI.SetActive(true);
		
	}

	public void ESCUIexit()
	{
		Time.timeScale = 1;
		isEscUI = false;
		ESCUI.SetActive(false);
	}

}
