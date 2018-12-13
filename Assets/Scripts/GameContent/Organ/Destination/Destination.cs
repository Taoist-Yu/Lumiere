using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : Organ {

	//粒子相关
	//编辑器中赋值
	public GameObject fog;
	public GameObject photosphere;
	//Awake中获得
	private ParticleSystem fogParticle;
	private ParticleSystem photosphereParticle;

	//此刻是否允许玩家操作
	public bool operatedAllowed
	{
		get;
		private set;
	}
	//终点开启进度
	private float progress = 0;

	protected override void Awake()
	{
		base.Awake();
		//获取粒子系统实例
		fogParticle = fog.GetComponent<ParticleSystem>();
		photosphereParticle = photosphere.GetComponent<ParticleSystem>();
	}

	// Use this for initialization
	void Start () {
		//设置粒子颜色
		{
			var module = fogParticle.main;
			module.startColor = lightNeed.Color;
		}
		{
			var module = photosphereParticle.main;
			module.startColor = lightNeed.Color;
		}

	}
	
	// Update is called once per frame
	void Update () {

		//progress达到1，通关
		if(progress > 1)
		{

		}

		//减少progress
		progress -= Time.deltaTime;
		if (progress < 0)
			progress = 0.0f;

		//重置受光相关变量
		operatedAllowed = false;
		{
			var module = fogParticle.emission;
			module.rateOverTime = 50;
		}

		//调试输出
		Debug.Log(progress);

	}

	public void AddProgress()
	{
		progress += 10 * Time.deltaTime;
	}

	public override void OnLighting(RaycastHit2D hit, Vector3 direction, RayLight light)
	{
		base.OnLighting(hit, direction, light);

		operatedAllowed = true;
		{
			var module = fogParticle.emission;
			module.rateOverTime = 0;
		}
	}
}
