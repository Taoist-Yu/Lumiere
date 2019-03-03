using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMainUI : MonoBehaviour {

	public GameObject levelUIManagerObject;
	private LevelUIManager UIManager;

	private void Awake()
	{
		UIManager = levelUIManagerObject.GetComponent<LevelUIManager>();
	}

	public void OnMainMenu()
	{
		UIManager.ESCHandlerInstance.ESCUIexit();
		SceneManager.LoadScene(0);
	}

	public void OnRestart()
	{
		UIManager.ESCHandlerInstance.ESCUIexit();
		SceneManager.LoadScene(GameGlobal.GameData.currentLevel);
	}

	public void OnOption()
	{
		UIManager.OptionUI.SetActive(true);
		gameObject.SetActive(false);
	}

	public void OnExit()
	{
		Application.Quit();
	}

}
