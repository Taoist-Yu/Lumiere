using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 定义了一些机关需要的属性
 * 机关对玩家操作的响应会基础OperateInterface类
 */
public class Organ : Entity
{
	[Header("激活机关所需要的光")]
	public RayLight.LightColor lightColorNeed;
	public int lightLevelNeed;
	protected RayLight lightNeed;
	[Header("如果该项为false，则任意光都能激活该机关")]
	public bool isLightNeed = false;

	protected override void GameBehavierInit()
	{
		base.GameBehavierInit();
		lightNeed = RayLight.GetLight(lightColorNeed, lightLevelNeed);
	}

	protected virtual void Awake()
	{
		GameBehavierInit();
	}

	public override void OnLighting(RaycastHit2D hit, Vector3 direction, RayLight light)
	{
		base.OnLighting(hit, direction, light);
		if (isLightNeed == true)
		{
			if (lightNeed.lightColor != light.lightColor)
				return;
			if (lightNeed.lightLevel > light.lightLevel)
				return;
		}
	}

}
