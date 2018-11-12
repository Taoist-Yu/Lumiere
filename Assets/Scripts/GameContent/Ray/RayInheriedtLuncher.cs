using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayInheriedtLuncher : RayLuncher {

	protected override void RayLuncherAwake()
	{
		base.RayLuncherAwake();
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
		isEmitting = false;
	}

	public override void OnLighting(Vector3 point, Vector3 dirction)
	{
		base.OnLighting(point, dirction);
		isEmitting = true;
	}

}
