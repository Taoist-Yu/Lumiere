using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	Vector3 startPos;
	Vector3 endPos;
	public float moveRatio;
	bool isMoving = false;
	bool isReturned = false;

	tempController player;
	Animator anim;

	public static PlayerCamera instance;
	
	//事件委托，额外的事件函数
	public delegate void StartEventHandler();
	StartEventHandler startEvent;
	public delegate void EndEventHandler();
	EndEventHandler endEvent;

	private void Awake()
	{
		player = transform.parent.GetComponent<tempController>();
		anim = GetComponent<Animator>();
		instance = this;
	}

	private void Update()
	{
		if(isMoving)
		{
			transform.position = (endPos - startPos) * moveRatio + startPos;
		}
	}

	//调用此函数来开启一次移动
	public static void Move(Vector3 startPos, Vector3 endPos, bool isReturned)
	{
		instance.CameraMove(startPos, endPos, isReturned);
	}

	public static void Move(Vector3 startPos, Vector3 endPos, bool isReturned, float speed, StartEventHandler startEvent, EndEventHandler endEvent)
	{
		//注册事件
		instance.startEvent = startEvent;
		instance.endEvent = endEvent;

		//设置动画播放速度
		instance.anim.speed = speed;

		instance.CameraMove(startPos, endPos, isReturned);
	}

	void CameraMove(Vector3 startPos, Vector3 endPos,bool isReturned)
	{
		moveRatio = 0;
		this.startPos = startPos;
		this.endPos = endPos;
		this.isReturned = isReturned;
		anim.Play("Move", 0, 0f);
	}

	//事件函数
	public void CameraMoveBegin()
	{
		isMoving = true;
		player.isPausing = true;

		//广播并清除事件
		if(startEvent != null)
			startEvent();
		startEvent = null;
	}

	//事件函数
	public void CameraMoveEnd()
	{
		if(isReturned)
		{
			//暂时不触发额外事件，等到返回的CameraMoveEnd再行触发
			StartCoroutine(CameraReturn());
			return;
		}

		//广播并清除事件，重置速度
		if (isReturned == false)
		{
			if (endEvent != null)
				endEvent();
			endEvent = null;
			anim.speed = 1;
		}

		isMoving = false;
		player.isPausing = false;
		isReturned = false;
	}

	IEnumerator CameraReturn()
	{
		yield return new WaitForSeconds(2);
		CameraMove(endPos, startPos, false);
	}

}
