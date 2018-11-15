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

	public override void OnLighting(Vector3 point, Vector3 dirction, Light light)
	{
		base.OnLighting(point, dirction, light);
		this.light = light;
		isEmitting = true;
	}

}
