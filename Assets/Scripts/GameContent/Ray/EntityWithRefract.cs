using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWithRefract : RefractLight
{


	ParticlePrism particlePrism;
	RefractLight prismRefract;
	bool IsOnLight = false;

	RaycastHit2D hitIt;
	Vector3 directionIt;
	RayLight lightIt;


	private void Awake()
	{
		particlePrism = this.GetComponentInChildren<ParticlePrism>();
		GameBehavierInit();
	}

	private void Update()
	{
		DestoryTempLight();
		if (!IsOnLight)
		{
			particlePrism.reEmitParticle(Color.grey);
			particlePrism.changePlayState(false);
		}
		else
		{
			LightReflection(hitIt, lightIt, directionIt);
			particlePrism.reEmitParticle(lightIt.Color);
			particlePrism.changePlayState(true);
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

	public override void OnLighting(RaycastHit2D hit, Vector3 direction, RayLight light)
	{
		hitIt = hit;
		directionIt = direction;
		lightIt = light;
		IsOnLight = true;
	}
}