using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayInheriedtLuncher : RayLuncher {

	protected override void RayLuncherAwake()
	{
		base.RayLuncherAwake();
	}

	protected override void RayLuncherUpdate()
	{
		base.RayLuncherUpdate();
		isEmitting = false;
		light = null;
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

	public override void OnLighting(RaycastHit2D hit, Vector3 direction, RayLight light)
	{
		base.OnLighting(hit, direction, light);
		if (this.light == null)
		{
			this.light = light;
		}
		else
		{
			this.light = RayLight.GetLight(light.LightQuantity + this.light.LightQuantity);
		}

		isEmitting = true;
	}

}
