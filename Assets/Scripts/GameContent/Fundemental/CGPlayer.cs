using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGPlayer : MonoBehaviour {

	//CG对象数组，CG以精灵图的形式挂载到游戏对象上
	public GameObject[] CGs;
	public GameObject visualBarrier;

	private Animator animator;
	private LevelManager levelManager;

	private int currentCG;  //当前播放的CG
	public float aniValue;  //从数值动画中获取的变量

	private float timeVal;  //计时器变量

	//CG播放的状态变量
	private enum State
	{
		unplay,
		play,
		wait,
		postPlay
	};
	State state = State.unplay;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		levelManager = GetComponent<LevelManager>(); 
	}

	private void OnEnable()
	{
		PlayCG(0);
	}

	private void Update()
	{
		OnPlaying();
	}

	public void PlayCG(int number)
	{
		currentCG = number;
		PrePlayBegin();
	}

	void PrePlayBegin()
	{
		CGs[currentCG].SetActive(true);
		visualBarrier.SetActive(true);
		state = State.play;
		animator.Play("UsualValue");
	}

	void WaitBegin()
	{
		animator.Play("Wait");
		timeVal =5;
		state = State.wait;
	}

	void PostPlayBegin()
	{
		state = State.postPlay;
		animator.Play("UsualValue");
	}

	void PlayEnd()
	{
		animator.Play("Wait");
		state = State.unplay;
		CGs[currentCG].SetActive(false);
		visualBarrier.SetActive(false);
		levelManager.OnCGPlayEnd();
	}

	void OnPlaying()
	{
		Color color = visualBarrier.GetComponent<SpriteRenderer>().color;
		switch (state)
		{
			case State.unplay:
				break;
			case State.play:
				color.a = (1 - aniValue);
				visualBarrier.GetComponent<SpriteRenderer>().color = color;
				if (aniValue >= 0.9f)
					WaitBegin();
				break;
			case State.wait:
				timeVal -= Time.deltaTime;
				if (timeVal < 0)
					PostPlayBegin();
				break;
			case State.postPlay:
				color.a = aniValue;
				visualBarrier.GetComponent<SpriteRenderer>().color = color;
				if (aniValue >= 0.9f)
					PlayEnd();
				break;
		}
	}

}
