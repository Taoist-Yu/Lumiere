using System.Collections;
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

	private void BeginEmit(Color color)
	{
		this.color = color;
		isEmitting = true;
	}

	private void EndEmit()
	{
		color = Color.gray;
		isEmitting = false;
	}

	//返回值玩家的光量增量
	public int ChangeEmitStatus(Color color)
	{
		if(isEmitting == true)
		{
			EndEmit();
			return lightQuantity;
		}
		else
		{
			BeginEmit(color);
			return 0;
		}
	}

}
