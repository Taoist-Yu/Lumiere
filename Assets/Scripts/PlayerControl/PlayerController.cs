using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GameBehaviour
{
	[SerializeField]
	[Range(0.1f,1f)]
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
	bool onTheGround = true;//状态改变由地面来实现

	OperateInterface operateInterface;			//获取场景可交互物体的操作接口

	private void Awake()
	{
		GameBehavierInit();
		anim = gameObject.GetComponent<Animator>();
		playerRd = this.GetComponent<Rigidbody2D>();//获取刚体
		anim.speed = 1.5f;
	}

	void Update () {
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
				transform.rotation = Quaternion.Euler(0,180,0);
			}
			else
			{
				transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			if (playerRd.velocity.y > 0.3|| playerRd.velocity.y < -0.3)
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
			playerRd.velocity = Vector3.up * speed * 40/2;
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
		if(GetInput.Operate && operateInterface != null)
		{
			if(ParticleController.numOfParticle >= operateInterface.LightNeed )
			{
				operateInterface.Operate(ParticleController.numOfParticle);
				//ParticleController.numOfParticle -= operateInterface.LightExpend;
			}
		}
	}


	protected override void OnLevelRotateBegin()
	{
		base.OnLevelRotateBegin();
		playerRd.gravityScale = 0;
	}

	protected override void OnLevelRotateEnd()
	{
		base.OnLevelRotateEnd();
		playerRd.gravityScale = 1;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision.tag);
		switch(collision.tag)
		{
			case "OperatedInterface":
				Debug.Log(2);
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

}
