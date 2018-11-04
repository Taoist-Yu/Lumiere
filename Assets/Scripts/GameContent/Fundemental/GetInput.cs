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
				Input.GetKeyDown(KeyCode.Q);
		}
	}

	//场景右旋转
	public static bool RightRotate
	{
		get
		{
			return
				Input.GetKeyDown(KeyCode.E);
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

	//跳跃
	public static bool Jump
	{
		get
		{
			return
				Input.GetButtonDown("Jump");
		}
	}


}

