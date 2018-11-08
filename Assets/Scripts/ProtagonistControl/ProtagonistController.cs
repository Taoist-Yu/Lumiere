using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistController : GameBehaviour
{
	[SerializeField]
	[Range(0.1f,1f)]
	float speed = 0.2f;
	[SerializeField]
	[Header("下降加速")]
	float fallJumpMuti = 2.5f;//长按下降加速
	[SerializeField]
	float lowJumpMuti = 2f;//轻按下降加速
	Rigidbody2D proRd;//刚体组件
	Animator anim;//动画状态机
	int isJumping = 0;//跳跃动作状态标识，0为接触地面，1为已经起跳（按下过一次跳跃键），大于等于2表示已经按下不止一次跳跃键
	bool getToWall = false;
	bool onTheGround = true;//状态改变由地面来实现

	private void Awake()
	{
		GameBehavierInit();
		anim = gameObject.GetComponent<Animator>();
	}

	void Start () {
		proRd = this.GetComponent<Rigidbody2D>();//获取刚体
	}
	
	void Update () {
		if (GetInScene.inScence)
		{
			if (onTheGround)
			{
				ProtagoWalk();
			}
			ProtagoJump();
			if (getToWall)
			{
				ProtagoClimb();
			}
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
	void ProtagoWalk()
	{
		if (GetInput.HorizonMove != 0)
		{
			anim.SetBool("IsWalking", true);
			float h = GetInput.HorizonMove;
			float v = Input.GetAxis("Vertical");
			if (h > 0)
			{
				transform.localScale = new Vector3(-1, 1, 1);
			}
			else
			{
				transform.localScale = new Vector3(1, 1, 1);
			}
			proRd.velocity = new Vector3(h, proRd.velocity.y / speed / 3,v) * speed * 3;
		}
		else
		{
			anim.SetBool("IsWalking", false);
		}
	}

	//跳跃
	void ProtagoJump()
	{
		if (GetInput.JumpStart && isJumping <= 1)
		{
			proRd.gravityScale = 1;
			proRd.velocity = Vector3.up * speed * 40;
			if (isJumping == 0)
			{
				anim.SetBool("IsJumping", true);
			}
			isJumping += 1;//避免多段跳
		}
		if (proRd.velocity.y < 0)
		{
			proRd.velocity += Vector2.up * Physics.gravity.y * (fallJumpMuti - 1) * Time.deltaTime;
		}
		else if (proRd.velocity.y > 0 && !GetInput.JumpLast)
		{
			proRd.velocity += Vector2.up * Physics.gravity.y * (lowJumpMuti - 1) * Time.deltaTime;
		}
		if (proRd.velocity.y == 0 && isJumping != 0)
		{
			isJumping = 0;
			anim.SetBool("IsJumping", false);
			//留个bug，要是有人能精准掌握按下跳跃键的时间能实现多段跳（还需要欧气加成），目前没想到怎么解决
		}
	}
	
	//攀爬
	void ProtagoClimb()
	{
		if (GetInput.ClimbStart)
		{
			anim.SetBool("IsClimbing", true);
			if (GetInput.ClimbUpward)
			{
				proRd.gravityScale = 0;
				//proRd.AddForce(-1*Physics.gravity);
				proRd.velocity = new Vector3(0, speed * 3, 0);
			}
			else
			{
				//proRd.AddForce(Physics.gravity);
				proRd.gravityScale = 0;
				proRd.velocity = new Vector3(0, -speed * 3, 0);
			}
		}
		if (GetInput.ClimbPause)
		{
			anim.SetBool("IsClimbing", false);
			proRd.velocity = new Vector3(0, 0, 0);
		}
	}

	protected override void OnLevelRotateBegin()
	{
		base.OnLevelRotateBegin();
		proRd.gravityScale = 0;
	}

	protected override void OnLevelRotateEnd()
	{
		base.OnLevelRotateEnd();
		proRd.gravityScale = 1;
	}
}
