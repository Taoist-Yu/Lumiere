using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour {

	//在Edit中设置Object，在Awake中设置脚本
	public GameObject UIManagerObject;
	private InterfaceUIManager UIManager;

	private void Awake()
	{
		UIManager = UIManagerObject.GetComponent<InterfaceUIManager>();
	}

	private void OnEnable()
	{
		GameGlobal.GameData.currentLevel = GameGlobal.GameData.maxLevel;
	}

	public void OnNext()
	{
		//未完成
		if (GameGlobal.GameData.currentLevel < GameGlobal.GameData.maxLevel)
			GameGlobal.GameData.currentLevel++;
	}

	public void OnLast()
	{
		//未完成
		if (GameGlobal.GameData.currentLevel > 1)
			GameGlobal.GameData.currentLevel--;
	}

	public void OnStart()
	{
		SceneManager.LoadScene(GameGlobal.GameData.currentLevel);
	}

	public void OnBack()
	{
		UIManager.StartUI.SetActive(true);
		gameObject.SetActive(false);
	}

}
