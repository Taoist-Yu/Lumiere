using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightElementEffect : MonoBehaviour {

	float timeVal = 0;
	private ParticleSystem particleSys;
	private ParticleSystem.Particle[] particleArr;
	[SerializeField]
	float size = 0.7f;

	//漂浮功能相关
	private struct Floating
	{
		public Vector3 initPostion;		//初始Postion
		public Vector3 currentPostion;     //当前的Postion
		public float deltaY;
	};
	Floating floating = new Floating
	{
		initPostion = default(Vector3),
		currentPostion = default(Vector3),
		deltaY = 0
	};

	// Use this for initialization
	void Start () {
		floating.initPostion = transform.position;
		timeVal = UnityEngine.Random.Range(0, 3);
		initParticleEffect();
	}
	
	// Update is called once per frame
	void Update () {
		timeVal += Time.deltaTime;
		FloatingUpdate();
	}

	//上下漂浮
	void FloatingUpdate()
	{
		floating.deltaY = Mathf.Sin(timeVal);

		floating.currentPostion = floating.initPostion;
		floating.currentPostion.y += 0.2f*floating.deltaY;

		transform.position = floating.currentPostion;
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
		main.startSize = size;
		main.maxParticles = 1;
		particleSys.Emit(1);
		particleSys.GetParticles(particleArr);
		particleArr[0].position = Vector3.zero;
		particleSys.SetParticles(particleArr, 1);
	}

}
