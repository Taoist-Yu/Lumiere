using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWithRefract : RefractLight
{


	ParticlePrism particlePrism;
	RefractLight prismRefract;
	bool IsOnLight = false;


	private void Awake()
	{
		particlePrism = this.GetComponentInChildren<ParticlePrism>();
		GameBehavierInit();
	}

	private void Update()
	{
		DestoryTempLight();
		if (IsOnLight)
		{
			particlePrism.changePlayState(false);
			particlePrism.reEmitParticle(Color.grey);
		}
		IsOnLight = false;
	}

	//销毁临时色散线
	public void DestoryTempLight()
	{
		GameObject[] destoryedObject = GameObject.FindGameObjectsWithTag("TempLight");
		foreach (GameObject obj in destoryedObject)
		{
			Destroy(obj);
		}
	}

	public override void OnLighting(RaycastHit2D hit, Vector3 direction, Light light)
	{
		LightReflection(hit, light, direction);
		particlePrism.changePlayState(true);
		particlePrism.reEmitParticle(light.Color);
		IsOnLight = true;
	}
}