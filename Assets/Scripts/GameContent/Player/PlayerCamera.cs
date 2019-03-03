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

	private void Awake()
	{
		player = transform.parent.GetComponent<tempController>();
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		if(isMoving)
		{
			transform.position = (endPos - startPos) * moveRatio + startPos;
		}
	}

	//调用此函数来开启一次移动
	public void CameraMove(Vector3 startPos, Vector3 endPos,bool isReturned)
	{
		moveRatio = 0;
		this.startPos = startPos;
		this.endPos = endPos;
		this.isReturned = isReturned;
		anim.Play("Move");
	}

	//事件函数
	public void CameraMoveBegin()
	{
		isMoving = true;
		player.isPausing = true;
	}

	//事件函数
	public void CameraMoveEnd()
	{
		if(isReturned)
		{
			CameraMove(endPos, startPos, false);
		}

		isMoving = false;
		player.isPausing = false;
		isReturned = false;
	}

}
