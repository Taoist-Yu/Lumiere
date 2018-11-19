using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GameBehaviour
{
	[SerializeField]
	[Range(0.1f, 1f)]
	float speed = 0.2f;
	[SerializeField]
	[Header("下降加速")]
	float fallJumpMuti = 2.5f;//长按下降加速
	[SerializeField]
	float lowJumpMuti = 2f;//轻按下降加速
	Rigidbody2D playerRd;//刚体组件
	Animator anim;//动画状态机
	int isJumping = 0;//跳跃动作状态标识，0为接触地面，1为已经起跳（按下过一次跳跃键），大于等于2表示已经按下不止一次跳跃键
	bool getToWall = false;
	bool onTheGround = true; //状态改变由地面来实现
	bool isRotating = false; //场景是否在旋转

	Collision2D lastCollision;					//上一个发生碰撞的物体

	GameObject playerRenderer;					//玩家渲染器实例
	OperateInterface operateInterface;          //获取场景可交互物体的操作接口

	private void Awake()
	{
		GameBehavierInit();
		playerRd = this.GetComponent<Rigidbody2D>();//获取刚体

		playerRenderer = transform.Find("PlayerRenderer").gameObject;
		anim = playerRenderer.GetComponent<Animator>();
		anim.speed = 1.5f;
	}

	private void Start()
	{
		playerRenderer.transform.localPosition = new Vector3(0, 0, -100);   //保持人物在屏幕前方
	}

	void Update() {
		if (GetInScene.inScence)
		{
			if (onTheGround)
			{
				PlayerWalk();
			}
			PlayerJump();
			if (getToWall)
			{
				PlayerClimb();
			}
			PlayerOperate();
			PlayerOperating();

			OnRotating();
		}
	}

	private void OnTriggerEnter(Collider wall)
	{
		if (wall.tag == "Ladder")
		{
			getToWall = !getToWall;
		}
	}

	//行走
	void PlayerWalk()
	{
		if (GetInput.HorizonMove != 0)
		{
			anim.SetBool("IsWalking", true);
			float h = GetInput.HorizonMove;
			float v = Input.GetAxis("Vertical");
			if (h > 0)
			{
				playerRenderer.transform.rotation = Quaternion.Euler(0, 180, 0);
			}
			else
			{
				playerRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			if (playerRd.velocity.y > 0.3 || playerRd.velocity.y < -0.3)
			{
				playerRd.velocity = new Vector3(h, playerRd.velocity.y / speed / 8, v) * speed * 8;
			}
			else
			{
				playerRd.velocity = new Vector3(h, playerRd.velocity.y / speed / 3, v) * speed * 3;
			}
		}
		else
		{
			anim.SetBool("IsWalking", false);
		}
	}

	//跳跃
	void PlayerJump()
	{
		if (GetInput.JumpStart && isJumping <= 1)
		{
			playerRd.gravityScale = 1;
			playerRd.velocity = Vector3.up * speed * 40 / 2;
			if (isJumping == 0)
			{
				anim.SetBool("IsJumping", true);
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
			}
			isJumping += 1;//避免多段跳
		}
		if (playerRd.velocity.y < 0)
		{
			gameObject.GetComponent<BoxCollider2D>().enabled = true;
			playerRd.velocity += Vector2.up * Physics.gravity.y * (fallJumpMuti - 1) * Time.deltaTime;
		}
		else if (playerRd.velocity.y > 0 && !GetInput.JumpLast)
		{
			playerRd.velocity += Vector2.up * Physics.gravity.y * (lowJumpMuti - 1) * Time.deltaTime;
		}
		if (playerRd.velocity.y == 0 && isJumping != 0)
		{
			isJumping = 0;
			anim.SetBool("IsJumping", false);
			//留个bug，要是有人能精准掌握按下跳跃键的时间能实现多段跳（还需要欧气加成），目前没想到怎么解决
		}
	}

	//攀爬
	void PlayerClimb()
	{
		if (GetInput.ClimbStart)
		{
			anim.SetBool("IsClimbing", true);
			if (GetInput.ClimbUpward)
			{
				playerRd.gravityScale = 0;
				//playerRd.AddForce(-1*Physics.gravity);
				playerRd.velocity = new Vector3(0, speed * 3, 0);
			}
			else
			{
				//playerRd.AddForce(Physics.gravity);
				playerRd.gravityScale = 0;
				playerRd.velocity = new Vector3(0, -speed * 3, 0);
			}
		}
		if (GetInput.ClimbPause)
		{
			anim.SetBool("IsClimbing", false);
			playerRd.velocity = new Vector3(0, 0, 0);
		}
	}

	//操作物体
	void PlayerOperate()
	{
		if (GetInput.Operate && operateInterface != null)
		{
			if (PlayerParticleController.lightQuantity >= operateInterface.lightNeed)
			{
				operateInterface.Operate(PlayerParticleController.lightQuantity);
				PlayerParticleController.lightQuantity += operateInterface.deltaLightQuantity;
				PlayerParticleController.UpdateParticle(PlayerParticleController.lightQuantity);
			}
		}
	}

	//持续操作物体
	void PlayerOperating()
	{
		if (GetInput.Operating && operateInterface != null)
		{
			Debug.Log(2);
			if (PlayerParticleController.lightQuantity >= operateInterface.lightNeed)
			{
				operateInterface.Operating(PlayerParticleController.lightQuantity);
				PlayerParticleController.lightQuantity += operateInterface.deltaLightQuantity;
				PlayerParticleController.UpdateParticle(PlayerParticleController.lightQuantity);
			}
		}
	}

	//当场景旋转过程中对玩家进行的操作
	private void OnRotating()
	{
		if (isRotating == false)
			return;

		transform.rotation = Quaternion.Euler(0, 0, 0);     //防止人物随场景旋转
		playerRenderer.transform.localPosition = new Vector3(0, 0, -100);	//保持人物在屏幕前方

	}

	protected override void OnLevelRotateBegin()
	{
		base.OnLevelRotateBegin();
		playerRd.gravityScale = 0;
		isRotating = true;
	}

	protected override void OnLevelRotateEnd()
	{
		base.OnLevelRotateEnd();
		playerRd.gravityScale = 1;
		isRotating = false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//当玩家切换陆地时，同步玩家与该物体的Z坐标
		//用与实现玩家和场景一起动

		//如果本次碰撞器与上次相同，证明玩家未切换陆地
		if (lastCollision != null)
			if (collision.transform.parent == lastCollision.transform.parent)
			{
				return;
			}
		//如果本次碰撞器与上次不同，证明玩家切换陆地
		transform.position = new Vector3(
				transform.position.x,
				transform.position.y,
				collision.transform.position.z
			);
		lastCollision = collision;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch(collision.tag)
		{
			case "OperatedInterface":
				operateInterface = collision.transform.parent.parent.GetComponent<OperateInterface>();
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

}
