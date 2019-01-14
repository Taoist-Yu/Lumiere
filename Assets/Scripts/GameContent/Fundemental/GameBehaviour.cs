using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour {

	protected bool isGameBehaviourInitialize = false;		//是否经过初始化

	/*场景控制器的实例*/
	protected LevelController levelController;

	/*所有继承自GameBehaviour的物体都应在Awake中调用该方法*/
	protected virtual void GameBehavierInit()
	{
		if (isGameBehaviourInitialize) return;
		isGameBehaviourInitialize = true;

		levelController = GameObject.Find("LevelRoot").GetComponent<LevelController>();
		levelController.LevelRotateBeginEvent += OnLevelRotateBegin;
		levelController.LevelRotateEndEvent += OnLevelRotateEnd;
	}

	protected virtual void Awake()
	{
		GameBehavierInit();
	}

	protected virtual void OnLevelRotateBegin()
	{ }

	protected virtual void OnLevelRotateEnd()
	{ }

	private void OnDestroy()
	{
		levelController.LevelRotateBeginEvent -= OnLevelRotateBegin;
		levelController.LevelRotateEndEvent -= OnLevelRotateEnd;
	}

}
