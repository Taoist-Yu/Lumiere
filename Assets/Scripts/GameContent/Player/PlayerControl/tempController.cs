using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempController : MonoBehaviour {

	bool haveLeftFence, haveRightFence, haveBottomFence, haveBottomLeftFence, haveBottomRightFence;
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
	private float verticalVelocity = 0;

	void FixedUpdate()
	{
		LaunchRaycast();
		ChangeState();
		PlayerWalk();
		PlayerJump();
	}

	void LaunchRaycast()
	{
		haveLeftFence = Physics2D.Raycast(transform.position + new Vector3(0, -bottom, 0), new Vector3(-leftRange, 0, 0), new Vector3(-leftRange, 0, 0).magnitude);
		haveRightFence = Physics2D.Raycast(transform.position + new Vector3(0, -bottom, 0), new Vector3(rightRange, 0, 0), new Vector3(rightRange, 0, 0).magnitude);
		haveBottomFence = Physics2D.Raycast(transform.position, new Vector3(0, -bottomRange, 0), new Vector3(0, -bottomRange, 0).magnitude);
		haveBottomLeftFence = Physics2D.Raycast(transform.position + new Vector3(-bottomEdge, 0, 0), new Vector3(0, -bottomRange, 0), new Vector3(0, -bottomRange, 0).magnitude);
		haveBottomRightFence = Physics2D.Raycast(transform.position + new Vector3(bottomEdge, 0, 0), new Vector3(0, -bottomRange, 0), new Vector3(0, -bottomRange, 0).magnitude);
		//Debug.Log("RightFence:" + haveRightFence + "   LeftFence:" + haveLeftFence + "   BottomFence:" + haveBottomFence);
	}

	void ChangeState()
	{
		if (haveBottomFence)
		{
			onGround = true;
			RaycastHit2D hit = new RaycastHit2D();
			hit = Physics2D.Raycast(transform.position, new Vector3(0, -bottomRange, 0));
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
		else if (h < 0 && !haveLeftFence)
		{
			this.transform.Translate(new Vector3(-walkSpeed * Time.fixedDeltaTime, 0, 0));
		}
	}

	void PlayerJump()
	{
		bool pressJump = Input.GetButtonDown("Jump");
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
								TranslatePlayer(G);
								verticalVelocity -= Time.fixedDeltaTime * G;
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
		Gizmos.DrawLine(transform.position + new Vector3(0, -bottom, 0), transform.position + new Vector3(-leftRange, 0, 0) + new Vector3(0, -bottom, 0));
		Gizmos.DrawLine(transform.position + new Vector3(0, -bottom, 0), transform.position + new Vector3(rightRange, 0, 0) + new Vector3(0, -bottom, 0));
		Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -bottomRange, 0));
		Gizmos.DrawLine(transform.position + new Vector3(-bottomEdge, 0, 0), transform.position + new Vector3(-bottomEdge, 0, 0) + new Vector3(0, -bottomRange, 0));
		Gizmos.DrawLine(transform.position + new Vector3(bottomEdge, 0, 0), transform.position + new Vector3(bottomEdge, 0, 0) + new Vector3(0, -bottomRange, 0));
	}
}
