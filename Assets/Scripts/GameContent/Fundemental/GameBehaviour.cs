using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour {

	/*场景控制器的实例*/
	protected LevelController levelController;

	/*物体是如何响应射线光的，折射，反射等*/
	protected enum ScatteringMode
	{
		diffuse,			//漫反射，吸收光
		refreaction,		//折射，改变光的方向（如棱镜）
		transmission,		//透射，光的方向不变直接穿过
		specular			//镜面反射
	}
	protected ScatteringMode scatteringMode = ScatteringMode.transmission;		//默认为透射

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

	/*由光线类调用，不是事件函数*/
	public virtual void OnLighting()
	{ }

}
