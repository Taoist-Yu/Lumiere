using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGPlayer : MonoBehaviour {

	SpriteRenderer sr;
	Animator anim;
	float alpha = 0;
	public const float speed = 0.2f;        //CG播放速度

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}

	private void Start()
	{
		sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.0f);
	}

	public void PlayCG()
	{
		anim.speed = speed;
		anim.Play("Play");
	}

	public void OnCGEnd()
	{
		StartCoroutine(LevelComplete());
	}

	//通关
	IEnumerator LevelComplete()
	{
		yield return new WaitForSeconds(3);
		GameObject.Find("FateMask").GetComponent<Animator>().Play("FateIn", 0, 0f);
		yield return new WaitForSeconds(1.1f);
		if(GameGlobal.GameData.currentLevel < GameGlobal.GameData.maxLevel)
			SceneManager.LoadScene(++GameGlobal.GameData.currentLevel);
		else
		{
			GameGlobal.GameData.isVictory = true;
			SceneManager.LoadScene(GameGlobal.GameData.currentLevel = 0);
		}
	}

}
