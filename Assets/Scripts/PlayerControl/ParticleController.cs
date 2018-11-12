using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : GameBehaviour
{
	float t_time = 0;
	public  static int numOfParticle = 1;
	ParticleSystem ps;
	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
		var col = ps.colorOverLifetime;
		col.enabled = true;
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
			numOfParticle += 1;
			ChangeParticleColor();
			var num = ps.main;
			num.maxParticles += 1;
			ps.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
			ps.Play();
			t_time = 0;
		}
		/*if (Input.GetAxis("Horizontal") > 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else
		{
			transform.localScale = new Vector3(1, 1, 1);
		}*/
		t_time += Time.deltaTime;
	}

	//变色
	void ChangeParticleColor()
	{


		//Gradient grad = new Gradient();
		//grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
		var col = ps.colorOverLifetime;
		col.color = Color.blue;
		int i = numOfParticle % 6;
		switch (i)
		{
			case 2:
				col.color = new Color(85f / 255f, 233f / 255f, 255f / 255f);
				break;
			case 3:
				col.color = new Color(166f / 255f, 255f / 255f, 165f / 255f);
				break;
			case 4:
				col.color = new Color(255f / 255f, 204f / 255f, 149f / 255f);
				break;
			case 5:
				col.color = new Color(255f / 255f, 133f / 255f, 132f / 255f);
				break;
			case 0:
				col.color = Color.white;
				break;
			case 1:
				col.color = new Color(109f / 255f, 109f / 255f, 109f / 255f);
				break;
			default: break;
		}
	}
}
