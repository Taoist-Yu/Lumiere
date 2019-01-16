﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempController : GameBehaviour
{
	int cot = 0;
	RaycastHit2D[] leftCastHit, rightCastHit, bottomCastHit, bottomLeftCastHit, bottomRightCastHit;
	#region 射线检测是否检测到了非触发器的碰撞体
	bool haveLefeFence
	{
		get
		{
			if (leftCastHit == null)
			{
				return false;
			}
			else
			{
				bool flag = false;
				foreach(RaycastHit2D hit in leftCastHit)
				{
					if (!hit.collider.isTrigger)
						flag = true;
				}
				return flag;
			}
		}
	}
	bool haveRightFence
	{
		get
		{
			if (rightCastHit == null)
			{
				return false;
			}
			else
			{
				bool flag = false;
				foreach (RaycastHit2D hit in rightCastHit)
				{
					if (!hit.collider.isTrigger)
						flag = true;
				}
				return flag;
			}
		}
	}
	bool haveBottomFence
	{
		get
		{
			if (bottomCastHit == null)
			{
				return false;
			}
			else
			{
				bool flag = false;
				foreach (RaycastHit2D hit in bottomCastHit)
				{
					if (!hit.collider.isTrigger)
						flag = true;
				}
				return flag;
			}
		}
	}
	bool haveBottomLeftFence
	{
		get
		{
			if (bottomLeftCastHit == null)
			{
				return false;
			}
			else
			{
				bool flag = false;
				foreach (RaycastHit2D hit in bottomLeftCastHit)
				{
					if (!hit.collider.isTrigger)
						flag = true;
				}
				return flag;
			}
		}
	}
	bool haveBottomRightFence
	{
		get
		{
			if (bottomRightCastHit == null)
			{
				return false;
			}
			else
			{
				bool flag = false;
				foreach (RaycastHit2D hit in bottomRightCastHit)
				{
					if (!hit.collider.isTrigger)
						flag = true;
				}
				return flag;
			}
		}
	}
	#endregion

	/// <summary>
	/// 游戏对象是否暂停。
	/// 一种情况是，当场景转换的过程中，人物无法移动，也无法操作
	/// </summary>
	bool isPausing = false;

	bool onGround = true;
	int pressJumpCount;
	[SerializeField]
	[Range(0.1f, 10f)]
	private float walkSpeed = 5f;
	private int G = 10;

	public float leftRange = 1.5f;
	public float rightRange = 1.5f;
	public float bottom = 1.5f;
	public float bottomRange = 1.5f;
	[Range(0.1f, 10f)]
	public float bottomEdge = 0.2f;

	private float positionOfLand;
	[SerializeField]
	[Range(5f, 20f)]
	private float maxVelo = 10;
	[SerializeField]
	[Range(5f, 20f)]
	private float velocityOnLighting = 10;
	private float verticalVelocity = 0;


	void FixedUpdate()
	{
		//基本移动
		if (!isPausing)
		{
			LaunchRaycast();
			ChangeState();
			PlayerWalk();
			PlayerJump();
		}

		//场景旋转
		if (isLevelRotating)
		{
			OnLevelRotate();
		}
	}

	void LaunchRaycast()
	{
		leftCastHit = Physics2D.RaycastAll(transform.position + new Vector3(0, -bottom, 0), new Vector3(-leftRange, 0, 0), new Vector3(-leftRange, 0, 0).magnitude);
		rightCastHit = Physics2D.RaycastAll(transform.position + new Vector3(0, -bottom, 0), new Vector3(rightRange, 0, 0), new Vector3(rightRange, 0, 0).magnitude);
		bottomCastHit = Physics2D.RaycastAll(transform.position + new Vector3(0, -bottom, 0), new Vector3(0, -bottomRange, 0), new Vector3(0, bottom-bottomRange, 0).magnitude);
		bottomLeftCastHit = Physics2D.RaycastAll(transform.position + new Vector3(-bottomEdge, -bottom, 0), new Vector3(0, -bottomRange, 0), new Vector3(0, bottom-bottomRange, 0).magnitude);
		bottomRightCastHit = Physics2D.RaycastAll(transform.position + new Vector3(bottomEdge, -bottom, 0), new Vector3(0, -bottomRange, 0), new Vector3(0, bottom-bottomRange, 0).magnitude);
		//Debug.Log("RightFence:" + haveRightFence + "   LeftFence:" + haveLeftFence + "   BottomFence:" + haveBottomFence);
	}

	void ChangeState()
	{
		if (haveBottomFence && verticalVelocity < 0)
		{
			onGround = true;
			RaycastHit2D hit = bottomCastHit[bottomCastHit.Length - 1];
			positionOfLand = hit.point.y;
		}
		else onGround = false;
	}

	void PlayerWalk()
	{
		float h = Input.GetAxisRaw("Horizontal");
		if (h > 0 && !haveRightFence)
		{
			this.transform.Translate(new Vector3(walkSpeed * Time.fixedDeltaTime, 0, 0));
		}
		else if (h < 0 && !haveLefeFence)
		{
			this.transform.Translate(new Vector3(-walkSpeed * Time.fixedDeltaTime, 0, 0));
		}
	}

	void PlayerJump()
	{
		bool pressJump = Input.GetButtonDown("Jump");
		if(pressJump == true)
		{
			Debug.Log(cot++);
		}
		//Debug.Log(pressJumpCount);
		if (onGround)
		{
			if (pressJump)
			{
				verticalVelocity = maxVelo;
				TranslatePlayer(G);
				pressJumpCount = 1;
			}
			else
			{
				transform.position = new Vector3(transform.position.x, positionOfLand + bottomRange, transform.position.z);
				verticalVelocity = 0;
				pressJumpCount = 0;
			}
		}
		else
		{
			if (pressJump)
			{
				switch (pressJumpCount)
				{
					case 0:
					case 1:
						{
							if (verticalVelocity >= 0)
							{
								verticalVelocity = maxVelo;
								TranslatePlayer(G);
							}
							else
							{
								verticalVelocity = maxVelo;
								TranslatePlayer(G);
							}
							pressJumpCount++;
							break;
						}
					case 2:
						{
							if (verticalVelocity > 0)
							{
								TranslatePlayer(G);
								verticalVelocity -= Time.fixedDeltaTime * G;
							}
							else
							{
								if (!haveBottomLeftFence && !haveBottomRightFence)
								{
									TranslatePlayer(G);
									verticalVelocity -= Time.fixedDeltaTime * G;
								}
							}
							break;
						}
				}
			}
			else
			{
				if (verticalVelocity > 0)
				{
					TranslatePlayer(G);
					verticalVelocity -= Time.fixedDeltaTime * G;
				}
				else
				{
					if (!haveBottomLeftFence && !haveBottomRightFence)
					{
						TranslatePlayer(G);
						verticalVelocity -= Time.fixedDeltaTime * G;
					}
				}
			}
		}
	}

	void TranslatePlayer(float G)
	{
		transform.Translate(new Vector3(0, verticalVelocity * Time.fixedDeltaTime - G * Time.fixedDeltaTime * Time.fixedDeltaTime / 2, 0));
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position + new Vector3(0, -bottom, 0), transform.position + new Vector3(-leftRange, 0, 0) + new Vector3(0, -bottom, 0));
		Gizmos.DrawLine(transform.position + new Vector3(0, -bottom, 0), transform.position + new Vector3(rightRange, 0, 0) + new Vector3(0, -bottom, 0));
		Gizmos.DrawLine(transform.position + new Vector3(0, -bottom, 0), transform.position + new Vector3(0, -bottomRange, 0));
		Gizmos.DrawLine(transform.position + new Vector3(-bottomEdge, -bottom, 0), transform.position + new Vector3(-bottomEdge, 0, 0) + new Vector3(0, -bottomRange, 0));
		Gizmos.DrawLine(transform.position + new Vector3(bottomEdge, -bottom, 0), transform.position + new Vector3(bottomEdge, 0, 0) + new Vector3(0, -bottomRange, 0));
	}

	#region 响应场景状态变化相关的代码

	bool isLevelRotating = false;

	protected override void OnLevelRotateBegin()
	{
		base.OnLevelRotateBegin();
		isPausing = true;
		isLevelRotating = true;
	}

	/// <summary>
	/// 场景旋转过程中每帧调用
	/// </summary>
	protected void OnLevelRotate()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
	}

	protected override void OnLevelRotateEnd()
	{
		base.OnLevelRotateEnd();
		isPausing = false;
		isLevelRotating = true;
	}

	#endregion

	#region 场景交互相关的代码

	public void OnLighting(RaycastHit2D hit, Vector3 direction, RayLight light)
	{
		transform.Translate(-direction * Time.deltaTime * velocityOnLighting);
		if (verticalVelocity < 0) verticalVelocity = 0;
	}

	#endregion

}
