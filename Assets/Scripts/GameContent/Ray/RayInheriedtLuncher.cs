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
		color = Color.gray;
	}

	public override void OnLighting(Vector3 point, Vector3 dirction, Color color)
	{
		base.OnLighting(point, dirction, color);
		this.color = color;
		isEmitting = true;
	}

}
