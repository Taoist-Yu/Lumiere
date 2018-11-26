using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : GameBehaviour
{
	static float t_time = 0;
	public static int lightQuantity = 1;
	static ParticleSystem ps;
	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
		var col = ps.colorOverLifetime;
		col.enabled = true;
		UpdateParticle(lightQuantity);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (t_time < 0.6 && t_time > 0.4)
		{
			ps.Pause();
		}
		//G键控制粒子数量，待更改
		if (Input.GetKeyDown(KeyCode.G))
		{
			lightQuantity += 1;
			UpdateParticle(lightQuantity);
		}

		t_time += Time.deltaTime;
	}

	//n为粒子改变量
	public static void UpdateParticle(int n)
	{

		lightQuantity = n;
		//Gradient grad = new Gradient();
		//grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

		//变色
		var col = ps.colorOverLifetime;
		switch (lightQuantity % 5)
		{
			case 1:
				col.color = new Color(85f / 255f, 233f / 255f, 255f / 255f);
				break;
			case 2:
				col.color = new Color(166f / 255f, 255f / 255f, 165f / 255f);
				break;
			case 3:
				col.color = new Color(255f / 255f, 204f / 255f, 149f / 255f);
				break;
			case 4:
				col.color = new Color(255f / 255f, 133f / 255f, 132f / 255f);
				break;
			case 0:
				col.color = Color.white;
				break;
			default: break;
		}

		//粒子操作
		var num = ps.main;
		num.maxParticles = lightQuantity;
		ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
		ps.Play();
		t_time = 0;
	}

}
