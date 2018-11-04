using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavier : MonoBehaviour {

	protected LevelController levelController;

	protected virtual void GameBehavierInit()
	{
		levelController = GameObject.Find("LevelRoot").GetComponent<LevelController>();
		levelController.LevelRotateBeginEvent += OnLevelRotateBegin;
		levelController.LevelRotateEndEvent += OnLevelRotateEnd;
	}

	private void Awake()
	{
		GameBehavierInit();
	}

	protected virtual void OnLevelRotateBegin()
	{ }

	protected virtual void OnLevelRotateEnd()
	{ }

}
