﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOriginalLuncher : RayLuncher {

	protected override void RayLuncherUpdate()
	{
		base.RayLuncherUpdate();
	}

	protected override void RayLuncherStart()
	{
		base.RayLuncherStart();
		isEmitting = false;
	}

	private void Awake()
	{
		RayLuncherAwake();
	}

	// Use this for initialization
	void Start () {
		RayLuncherStart();
	}
	
	// Update is called once per frame
	void Update () {
		RayLuncherUpdate();
	}

	private int BeginEmit(Light light)
	{
		int delta = ((int)light.lightColor + 5 * (light.lightLevel - 1));
		this.light = light;
		isEmitting = true;
		return -delta;
	}

	private int EndEmit()
	{
		int delta = ((int)light.lightColor + 5 * (light.lightLevel - 1));
		light = null;
		isEmitting = false;
		return delta;
	}

	//返回光的增量
	public int ChangeEmitStatus(Light light)
	{
		if(isEmitting == true)
		{
			return EndEmit();
		}
		else
		{
			return BeginEmit(light);
		}
	}

}
