using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour {

	//在Edit中设置Object，在Awake中设置脚本
	public GameObject UIManagerObject;
	private InterfaceUIManager UIManager;

	private void Awake()
	{
		UIManager = UIManagerObject.GetComponent<InterfaceUIManager>();
	}

	public void OnStart()
	{
		SceneManager.LoadScene(GameGlobal.GameData.maxLevel);
	}

	public void OnLevel()
	{
		UIManager.LevelUI.SetActive(true);
		gameObject.SetActive(false);
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
