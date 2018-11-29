using System;
using System.Collections.Generic;
using UnityEngine;


class GetInput
{

	//场景左旋转
	public static bool LeftRotate
	{
		get
		{
			return
				Input.GetKeyDown(KeyCode.A);
		}
	}

	//场景右旋转
	public static bool RightRotate
	{
		get
		{
			return
				Input.GetKeyDown(KeyCode.D);
		}
	}

	//人物移动
	public static float HorizonMove
	{
		get
		{
			return
				Input.GetAxis("Horizontal");
		}
	}

	//人物攀爬
	//攀爬开始
	public static bool ClimbStart
	{
		get
		{
			return
				Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow);
		}
	}

	//攀爬方向控制
	public static bool ClimbUpward
	{
		get
		{
			return
				Input.GetKeyDown(KeyCode.UpArrow);
		}
	}

	//攀爬中止
	public static bool ClimbPause
	{
		get
		{
			return
				Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow);
		}
	}

	//跳跃
	//跳跃开始
	public static bool JumpStart
	{
		get
		{
			return
				Input.GetButtonDown("Jump");
		}
	}

	//跳跃中
	public static bool JumpLast
	{
		get
		{
			return
				Input.GetButton("Jump");
		}
	}
	//操作物体0
	public static bool Operate0
	{
		get
		{
			return
				Input.GetKeyDown(KeyCode.F);
		}
	}

	//操作物体0
	public static bool Operate1
	{
		get
		{
			return
				Input.GetKeyDown(KeyCode.Q);
		}
	}

	//操作物体0
	public static bool Operate2
	{
		get
		{
			return
				Input.GetKeyDown(KeyCode.E);
		}
	}

	//持续操作物体0
	public static bool Operating0
	{
		get
		{
			return
				Input.GetKey(KeyCode.F);
		}
	}

	//持续操作物体1
	public static bool Operating1
	{
		get
		{
			return
				Input.GetKey(KeyCode.Q);
		}
	}

	//持续操作物体2
	public static bool Operating2
	{
		get
		{
			return
				Input.GetKey(KeyCode.E);
		}
	}
}

