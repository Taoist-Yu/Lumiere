using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftingPlatformEffect : MonoBehaviour
{
	[HideInInspector]
	public RayLight rayLight;

	float timeVal = 0;
	float deltaScale;
	float deltaSize;
	private ParticleSystem particleSys;
	private ParticleSystem.Particle[] particleArr;
	[SerializeField]
	float size = 1.0f;

	// Use this for initialization
	void Start()
	{
//		initParticleEffect();
		deltaScale = size / 10;
	}

	// Update is called once per frame
	void Update()
	{
		timeVal += Time.deltaTime;
		deltaSize = deltaScale * Mathf.Sin(timeVal);
		initParticleEffect();
	}

	//粒子效果
	void initParticleEffect()
	{
		particleArr = new ParticleSystem.Particle[1];
		particleSys = this.GetComponent<ParticleSystem>();
		var main = particleSys.main;
		main.startColor = Color.white;
		main.startSpeed = 0;
		main.loop = false;
		main.startSize = size + deltaSize;
		main.maxParticles = 1;
		particleSys.Emit(1);
		particleSys.GetParticles(particleArr);
		particleArr[0].position = Vector3.zero;
		particleSys.SetParticles(particleArr, 1);

	}


}
