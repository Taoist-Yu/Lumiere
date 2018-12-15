using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGameAndExitMenu()
	{
		GameObject.Find("StartCanvas").gameObject.SetActive(false);
		GameObject.Find("UICamera").gameObject.SetActive(false);
	}

	public void ExitGameAndExitMenu()
	{
		Application.Quit();
	}
}
