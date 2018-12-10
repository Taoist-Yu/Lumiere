using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
