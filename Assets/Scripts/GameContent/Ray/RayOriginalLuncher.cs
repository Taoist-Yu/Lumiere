using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOriginalLuncher : RayLuncher {

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

}
