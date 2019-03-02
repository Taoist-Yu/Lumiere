using System;
using System.Collections.Generic;
using UnityEngine;


class GetInput
{

	private static bool operateAllowed = true;
	public static void OperateEnable()
	{
		operateAllowed = true;
	}
	public static void OperateDisable()
	{
		operateAllowed = false;
	}

	//场景左旋转
	public static bool LeftRotate
	{
		get
		{
			if (operateAllowed == false)
				return false;
			return
				Input.GetKeyDown(KeyCode.A);
		}
	}

	//场景右旋转
	public static bool RightRotate
	{
		get
		{
			if (operateAllowed == false)
				return false;
			return
				Input.GetKeyDown(KeyCode.D);
		}
	}

	//人物移动
	public static float HorizonMove
	{
		get
		{
			if (operateAllowed == false)
				return 0;
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
			if (operateAllowed == false)
				return false;
			return
				Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow);
		}
	}

	//攀爬方向控制
	public static bool ClimbUpward
	{
		get
		{
			if (operateAllowed == false)
				return false;
			return
				Input.GetKeyDown(KeyCode.UpArrow);
		}
	}

	//攀爬中止
	public static bool ClimbPause
	{
		get
		{
			if (operateAllowed == false)
				return false;
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
			if (operateAllowed == false)
				return false;
			return
				Input.GetButtonDown("Jump");
		}
	}

	//跳跃中
	public static bool JumpLast
	{
		get
		{
			if (operateAllowed == false)
				return false;
			return
				Input.GetButton("Jump");
		}
	}
	//操作物体0
	public static bool Operate0
	{
		get
		{
			if (operateAllowed == false)
				return false;
			return
				Input.GetKeyDown(KeyCode.F);
		}
	}

	//操作物体0
	public static bool Operate1
	{
		get
		{
			if (operateAllowed == false)
				return false;
			return
				Input.GetKeyDown(KeyCode.Q);
		}
	}

	//操作物体0
	public static bool Operate2
	{
		get
		{
			if (operateAllowed == false)
				return false;
			return
				Input.GetKeyDown(KeyCode.E);
		}
	}

	//持续操作物体0
	public static bool Operating0
	{
		get
		{
			if (operateAllowed == false)
				return false;
			return
				Input.GetKey(KeyCode.F);
		}
	}

	//持续操作物体1
	public static bool Operating1
	{
		get
		{
			if (operateAllowed == false)
				return false;
			return
				Input.GetKey(KeyCode.Q);
		}
	}

	//持续操作物体2
	public static bool Operating2
	{
		get
		{
			if (operateAllowed == false)
				return false;
			return
				Input.GetKey(KeyCode.E);
		}
	}

	//暂停UI
	public static bool ESC
	{
		get
		{
			return
				Input.GetKeyDown(KeyCode.Escape);
		}
	}

	public static bool PickMode
	{
		get
		{
			if (operateAllowed == false)
				return false;
			return
				Input.GetKeyDown(KeyCode.L);
		}
	}

	public static bool Continue
	{
		get
		{
			if (operateAllowed == false)
				return false;
			return
				Input.GetKeyDown(KeyCode.Return);
		}
	}

}

