using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempController : GameBehaviour
{
	RaycastHit2D[] leftCastHit, rightCastHit, bottomCastHit, bottomLeftCastHit, bottomRightCastHit;
	#region 射线检测是否检测到了非触发器的碰撞体

	bool haveFence(RaycastHit2D[] hits)
	{
		if (hits == null)
		{
			return false;
		}
		else
		{
			bool flag = false;
			foreach (RaycastHit2D hit in hits)
			{
				if (!hit.collider.isTrigger)
				{
					if (hit.collider.tag != "ColorfulPlatform")
						flag = true;
					//若目标平台是有颜色需求的
					else
					{
						//获取目标平台脚本控件
						ColorfulPlatform platInstance = hit.collider.transform.parent.parent.GetComponent<ColorfulPlatform>();

						RayLight playerLight = RayLight.GetLight(PlayerParticleController.lightQuantity);
						RayLight.LightColor platformColor = hit.collider.transform.parent.parent.GetComponent<ColorfulPlatform>().platformColor;
						//若颜色一样
						if (playerLight.lightColor == platformColor)
						{
							flag = true;
						}
						//反之
						else
						{
							if (playerLight.lightColor == RayLight.LightColor.white
								&& playerLight.LightQuantity != 0)
							{
								flag = true;
							}
							else
							{
								flag = false;
								platInstance.OnScene();
							}		
						}
					}
				}
			}
			return flag;
		}
	}
	bool haveLeftFence
	{
		get
		{
			return haveFence(leftCastHit);
		}
	}
	bool haveRightFence
	{
		get
		{
			return haveFence(rightCastHit);
		}
	}
	bool haveBottomFence
	{
		get
		{
			return haveFence(bottomCastHit);
		}
	}
	bool haveBottomLeftFence
	{
		get
		{
			return haveFence(bottomLeftCastHit);
		}
	}
	bool haveBottomRightFence
	{
		get
		{
			return haveFence(bottomRightCastHit);
		}
	}
	#endregion

	#region 重生相关功能

	GameObject respawnPlatform;

	int heart = 0;
	[Header("初始生命值")]
	public int initHeart = 3;

	//更新重生点
	void UpdateRespawnPosition()
	{
		if (haveBottomFence)
		{
			RaycastHit2D[] hits = bottomCastHit;
			if (hits != null)
				foreach (RaycastHit2D hit in hits)
				{
					if (!hit.collider.isTrigger)
					{
						if (hit.collider.tag == "Untagged")
						{
							respawnPlatform = hit.collider.transform.parent.parent.gameObject;
						}
					}
				}
		}
	}

	//重生
	void Respawn()
	{
		//判断人物是否死亡
		if (transform.position.y > -20)
			return;
		//减少生命值，若无生命则无法复活
		if (heart > 0)
			heart--;
		else
		{
			//死亡（未完成）
			return;
		}
		//确定相机移动的起始点和终止点
		Vector3 respawnPosition = respawnPlatform.transform.position + new Vector3(0, 2, 0);

		Vector3 startPos = PlayerCamera.instance.transform.position;
		Vector3 endPos = respawnPosition + PlayerCamera.instance.transform.localPosition;
		//重置人物位置坐标及速度
		transform.position = respawnPosition;
		verticalVelocity = 0;
		//开始相机移动
		PlayerCamera.Move(startPos, endPos, false);
	}

	#endregion


	/// <summary>
	/// 游戏对象是否暂停。
	/// 一种情况是，当场景转换的过程中，人物无法移动，也无法操作
	/// </summary>
	public bool isPausing = false;

	bool onGround = true;
	int pressJumpCount;
	int maxJumpCount = 1;
	[SerializeField]
	[Range(0.1f, 10f)]
	private float walkSpeed = 5f;
	private int G = 12;

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

	GameObject playerParticle;
	GameObject[] playerTrail;

	private void Start()
	{
		playerParticle = GameObject.Find("PlayerPS");
		playerTrail = GameObject.FindGameObjectsWithTag("PlayerTrail");
		heart = initHeart;
	}

	void FixedUpdate()
	{

	}

	private void Update()
	{
		//基本移动
		if (!isPausing)
		{
			LaunchRaycast();
			ChangeState();
		}

		//人物移动
		if (!isPausing)
		{
			PlayerWalk();
			PlayerJump();
		}

		//调整人物坐标与当前脚下的物体坐标一样，从而使转场的时候人物不至于下落
		if (haveBottomFence)
		{
			transform.position = new Vector3(
					transform.position.x,
					transform.position.y,
					bottomCastHit[bottomCastHit.Length - 1].collider.transform.position.z
				);
		}

		//场景旋转
		if (isLevelRotating)
		{
			OnLevelRotate();
		}

		//操作物体
		if(!isPausing)
		{
			PlayerAllOperate();
		}

		//重生相关
		UpdateRespawnPosition();
		Respawn();
	}

	void LaunchRaycast()
	{
		leftCastHit = Physics2D.RaycastAll(transform.position + new Vector3(0, -bottom, 0), new Vector3(-leftRange, 0, 0), new Vector3(-leftRange, 0, 0).magnitude);
		rightCastHit = Physics2D.RaycastAll(transform.position + new Vector3(0, -bottom, 0), new Vector3(rightRange, 0, 0), new Vector3(rightRange, 0, 0).magnitude);
		bottomCastHit = Physics2D.RaycastAll(transform.position + new Vector3(0, -bottom, 0), new Vector3(0, -bottomRange, 0), new Vector3(0, bottom - bottomRange, 0).magnitude);
		bottomLeftCastHit = Physics2D.RaycastAll(transform.position + new Vector3(-bottomEdge, -bottom, 0), new Vector3(0, -bottomRange, 0), new Vector3(0, bottom - bottomRange, 0).magnitude);
		bottomRightCastHit = Physics2D.RaycastAll(transform.position + new Vector3(bottomEdge, -bottom, 0), new Vector3(0, -bottomRange, 0), new Vector3(0, bottom - bottomRange, 0).magnitude);
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
			this.transform.Translate(new Vector3(walkSpeed * Time.deltaTime, 0, 0));
			for(int i = 0; i < playerTrail.Length; i++)
			{

				if (playerTrail[i].gameObject.transform.position.x - this.transform.position.x > 0) { playerTrail[i].gameObject.transform.position = new Vector3(2 * this.transform.position.x - playerTrail[i].gameObject.transform.position.x, playerTrail[i].gameObject.transform.position.y, playerTrail[i].gameObject.transform.position.z); }
			}
		}
		else if (h < 0 && !haveLeftFence)
		{
			this.transform.Translate(new Vector3(-walkSpeed * Time.deltaTime, 0, 0));
			for (int i = 0; i < playerTrail.Length; i++)
			{
				if (playerTrail[i].gameObject.transform.position.x - this.transform.position.x < 0) { playerTrail[i].gameObject.transform.position = new Vector3(2 * this.transform.position.x - playerTrail[i].gameObject.transform.position.x, playerTrail[i].gameObject.transform.position.y, playerTrail[i].gameObject.transform.position.z); }
			}
		}
	}

	void PlayerJump()
	{
		bool pressJump = Input.GetButtonDown("Jump");
		if (pressJump == true)
		{
			//Debug.Log(cot++);
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
				//跳跃次数清零
				pressJumpCount = 0;
			}
		}
		else
		{
			if (pressJump)
			{
				//判断多段跳段数
				if (pressJumpCount < 1)
				{
					maxJumpCount = PlayerParticleController.lightQuantity / 5 + 1;
				}
				if (pressJumpCount < maxJumpCount)
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
					if (PlayerParticleController.lightQuantity >= 5)
					{
						PlayerParticleController.lightQuantity -= 5;
						playerParticle.GetComponent<PlayerParticleController>().UpdateParticle();
					}
				}
				else
				{
					if (verticalVelocity > 0)
					{
						TranslatePlayer(G);
						verticalVelocity -= Time.deltaTime * G;
					}
					else
					{
						if (!haveBottomLeftFence && !haveBottomRightFence)
						{
							TranslatePlayer(G);
							verticalVelocity -= Time.deltaTime * G;
						}
					}
				}
			}
			else
			{
				if (verticalVelocity > 0)
				{
					TranslatePlayer(G);
					verticalVelocity -= Time.deltaTime * G;
				}
				else
				{
					if (!haveBottomLeftFence && !haveBottomRightFence)
					{
						TranslatePlayer(G);
						verticalVelocity -= Time.deltaTime * G;
					}
				}
			}
		}
	}

	void TranslatePlayer(float G)
	{
		transform.Translate(new Vector3(0, verticalVelocity * Time.deltaTime - G * Time.deltaTime * Time.deltaTime / 2, 0));
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

		transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
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
		isLevelRotating = false;

		transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
	}

	#endregion

	#region 场景交互相关的代码

	OperateInterface operateInterface;          //获取场景可交互物体的操作接口
	public bool isPickMode = false;                 //拾取模式是否开启,在光球脚本中被访问
	float lastTimeOfGettingLightElement = 0;	//为了放止在光球被销毁之前发生二次拾取

	public void OnLighting(RaycastHit2D hit, Vector3 direction, RayLight light)
	{
		//判断人物是否能随光线移动
		bool flag = false;
		
		if(PlayerParticleController.lightQuantity > 0)
		{
			RayLight.LightColor playerColor = RayLight.GetLight(PlayerParticleController.lightQuantity).lightColor;
			RayLight.LightColor lightColor = light.lightColor;
			if(playerColor == RayLight.LightColor.white)
			{
				flag = true;
			}
			else
			{
				if (playerColor == lightColor)
					flag = true;
			}
		}
		//移动人物
		if(flag)
		{
			transform.Translate(-direction * Time.deltaTime * velocityOnLighting);
			if (verticalVelocity < 0) verticalVelocity = 0;
		}
	}

	//获取玩家所有操作
	void PlayerAllOperate()
	{
		PlayerOperate();
		PlayerOperating();
		PlayerPickMode();
	}

	//操作物体
	void PlayerOperate()
	{
		if (operateInterface == null)
			return;
		if (GetInput.Operate0)
		{
			if (PlayerParticleController.lightQuantity >= operateInterface.lightNeed)
			{
				operateInterface.Operate0(PlayerParticleController.lightQuantity);
				if (operateInterface.deltaLightQuantity != 0)
				{
					PlayerParticleController.lightQuantity += operateInterface.deltaLightQuantity;
					playerParticle.GetComponent<PlayerParticleController>().UpdateParticle();
				}
			}
		}
		if (GetInput.Operate1)
		{
			if (PlayerParticleController.lightQuantity >= operateInterface.lightNeed)
			{
				operateInterface.Operate1(PlayerParticleController.lightQuantity);
				if (operateInterface.deltaLightQuantity != 0)
				{
					PlayerParticleController.lightQuantity += operateInterface.deltaLightQuantity;
					playerParticle.GetComponent<PlayerParticleController>().UpdateParticle();
				}
			}
		}
		if (GetInput.Operate2)
		{
			if (PlayerParticleController.lightQuantity >= operateInterface.lightNeed)
			{
				operateInterface.Operate2(PlayerParticleController.lightQuantity);
				if (operateInterface.deltaLightQuantity != 0)
				{
					PlayerParticleController.lightQuantity += operateInterface.deltaLightQuantity;
					playerParticle.GetComponent<PlayerParticleController>().UpdateParticle();
				}
			}
		}
	}

	//持续操作物体
	void PlayerOperating()
	{
		if (operateInterface == null)
			return;
		if (GetInput.Operating0)
		{
			if (PlayerParticleController.lightQuantity >= operateInterface.lightNeed)
			{
				operateInterface.Operating0(PlayerParticleController.lightQuantity);
				if (operateInterface.deltaLightQuantity != 0)
				{
					PlayerParticleController.lightQuantity += operateInterface.deltaLightQuantity;
					playerParticle.GetComponent<PlayerParticleController>().UpdateParticle();
				}
			}
		}
		if (GetInput.Operating1)
		{
			if (PlayerParticleController.lightQuantity >= operateInterface.lightNeed)
			{
				operateInterface.Operating1(PlayerParticleController.lightQuantity);
				if (operateInterface.deltaLightQuantity != 0)
				{
					PlayerParticleController.lightQuantity += operateInterface.deltaLightQuantity;
					playerParticle.GetComponent<PlayerParticleController>().UpdateParticle();
				}
			}
		}
		if (GetInput.Operating2)
		{
			if (PlayerParticleController.lightQuantity >= operateInterface.lightNeed)
			{
				operateInterface.Operating2(PlayerParticleController.lightQuantity);
				if (operateInterface.deltaLightQuantity != 0)
				{
					PlayerParticleController.lightQuantity += operateInterface.deltaLightQuantity;
					playerParticle.GetComponent<PlayerParticleController>().UpdateParticle();
				}
			}
		}
	}

	//拾取模式
	void PlayerPickMode()
	{
		if(GetInput.PickMode)
		{
			isPickMode = !isPickMode;
		}
	}

	//获取操作实例
	private void OnTriggerEnter2D(Collider2D collision)
	{

		switch (collision.tag)
		{
			case "OperatedInterface":
				operateInterface = collision.transform.parent.parent.GetComponent<OperateInterface>();
				Debug.Log(1);
				break;
			case "LightElement":
				//问题：不能在极短时间（<0.1s）内连续接光球
				if(isPickMode && Time.time - lastTimeOfGettingLightElement > 0.1f)
				{
					lastTimeOfGettingLightElement = Time.time;
					GameObject lightElement = collision.transform.parent.parent.gameObject;
					PlayerParticleController.lightQuantity++;
					playerParticle.GetComponent<PlayerParticleController>().UpdateParticle();
					Destroy(lightElement);
				}
				break;
			case "TriggerEvent":
				collision.transform.parent.parent.GetComponent<TriggerEvent>().ActivateEvent();
				break;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "OperatedInterface":
				operateInterface = null;
				break;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "OperatedInterface":
				operateInterface = collision.transform.parent.parent.GetComponent<OperateInterface>();
				break;
		}
	}

	#endregion

}
