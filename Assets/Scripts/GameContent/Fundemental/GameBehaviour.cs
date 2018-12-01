using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour {

	/*场景控制器的实例*/
	protected LevelController levelController;

	/*所有继承自GameBehaviour的物体都应在Awake中调用该方法*/
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

	private void OnDestroy()
	{
		Debug.Log("Destory");
		levelController.LevelRotateBeginEvent -= OnLevelRotateBegin;
		levelController.LevelRotateEndEvent -= OnLevelRotateEnd;
	}

}
