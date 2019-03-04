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

	//计时相关
	float lightTimeVal = 0;		//随时间递减，光照时重置，当减为0证明当前无光照

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
	
	// Update is called once per fram
	void Update () {

		//计时相关
		lightTimeVal -= Time.deltaTime;
		if (lightTimeVal < 0)
			lightTimeVal = 0;

		//progress达到1，通关
		if(progress > 1)
		{
			
		}

		//减少progress
		progress -= Time.deltaTime;
		if (progress < 0)
			progress = 0.0f;

		//重置受光相关变量
		if(lightTimeVal < 1e-6)
		{
			operatedAllowed = false;
			{
				var module = fogParticle.emission;
				module.rateOverTime = 50;
			}
			{
				var module = photosphereParticle.emission;
				module.rateOverTime = 0;
			}
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
		{
			var module = photosphereParticle.emission;
			module.rateOverTime = 50;
		}

		lightTimeVal = 0.1f;
	}
}
