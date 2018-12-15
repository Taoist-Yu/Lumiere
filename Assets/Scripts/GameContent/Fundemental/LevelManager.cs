using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public GameObject LevelRoot;
	public GameObject CGCamera;
	private AudioSource bgmSource;
	private AudioClip bgm;
	private CGPlayer cgPlayer;

	//关卡状态
	enum State
	{
		preCG,				//关卡前播放CG
		playing,			//游戏阶段
		passing				//通关后
	}
	State state = State.preCG;

	private void Awake()
	{
		cgPlayer = GetComponent<CGPlayer>();
	}

	public void LevelPassed()
	{
		LevelRoot.SetActive(false);
		CGCamera.SetActive(true);
		state = State.passing;
		cgPlayer.PlayCG(1);
	}

	public void OnCGPlayEnd()
	{
		if(state == State.preCG)
		{
			LevelRoot.SetActive(true);
			state = State.playing;
			CGCamera.SetActive(false);
		}
		else if(state == State.passing)
		{
			if(GameGlobal.GameData.currentLevel < GameGlobal.GameData.maxLevel)
			{
				GameGlobal.GameData.currentLevel++;
				SceneManager.LoadScene(GameGlobal.GameData.currentLevel);
			}
			else
			{
				SceneManager.LoadScene(0);
			}
		}
	}

}
