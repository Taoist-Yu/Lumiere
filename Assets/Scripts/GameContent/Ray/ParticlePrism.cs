using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePrism : GameBehaviour
{

	int count = 4;       // 粒子数量
	float size = 0.3f;      // 粒子大小
	float minRadius = 0.5f;  // 最小半径
	float maxRadius = 0.6f; // 最大半径
	public bool clockwise = false;   // 顺时针|逆时针
	public float speed = 5f;        // 速度
	public float pingPong = 0.02f;  // 游离范围
	bool playingParticle = false;  //粒子效果开启|关闭

	private ParticleSystem particleSys;  // 粒子系统
	private ParticleSystem.Particle[] particleArr;  // 粒子数组
	private CirclePosition[] circle; // 极坐标数组

	// Use this for initialization

	private void Awake()
	{
		GameBehavierInit();
	}

	void Start()
	{
		particleArr = new ParticleSystem.Particle[count];
		circle = new CirclePosition[count];

		// 初始化粒子系统
		particleSys = this.GetComponent<ParticleSystem>();
		var main = particleSys.main;
		main.startColor = Color.grey;
		main.startSpeed = 0;            // 粒子位置由程序控制
		main.startSize = size;          // 设置粒子大小
		main.loop = false;
		main.maxParticles = count;      // 设置最大粒子量
		particleSys.Emit(count);               // 发射粒子
		particleSys.GetParticles(particleArr);

		RandomlySpread();
	}

	private int tier = 4;  // 速度差分层数

	void Update()
	{
		if (playingParticle)
		{
			for (int i = 0; i < count; i++)
			{
				if (clockwise)  // 顺时针旋转
					circle[i].angle -= (i % tier + 1) * (speed / circle[i].radius / tier);
				else            // 逆时针旋转
					circle[i].angle += (i % tier + 1) * (speed / circle[i].radius / tier);

				// 保证angle在0~360度
				circle[i].angle = (360.0f + circle[i].angle) % 360.0f;
				float theta = circle[i].angle / 180 * Mathf.PI;
				if (levelController.perspective == 0 || levelController.perspective == 2)
				{
					particleArr[i].position = new Vector3(circle[i].radius * Mathf.Cos(theta), particleArr[i].position.y, circle[i].radius * Mathf.Sin(theta));
				}
				else
				{
					particleArr[i].position = new Vector3(particleArr[i].position.x, circle[i].radius * Mathf.Cos(theta), circle[i].radius * Mathf.Sin(theta));
				}

					
			}
			particleSys.SetParticles(particleArr, particleArr.Length);
		}
	}


	void RandomlySpread()
	{
		for (int i = 0; i < count; ++i)
		{   // 随机每个粒子距离中心的半径，同时希望粒子集中在平均半径附近
			float midRadius = (maxRadius + minRadius) / 2;
			float minRate = Random.Range(1.0f, midRadius / minRadius);
			float maxRate = Random.Range(midRadius / maxRadius, 1.0f);
			float radius = Random.Range(minRadius * minRate, maxRadius * maxRate);

			// 随机每个粒子的角度
			float angle = Random.Range(0.0f, 360.0f);
			float theta = angle / 180 * Mathf.PI;

			// 随机每个粒子的游离起始时间
			float time = Random.Range(0.0f, 360.0f);

			circle[i] = new CirclePosition(radius, angle, time);
			particleArr[i].position = new Vector3(circle[i].radius * Mathf.Cos(theta), particleArr[i].position.y, circle[i].radius * Mathf.Sin(theta));
		}

		particleSys.SetParticles(particleArr, particleArr.Length);
	}

	public void reEmitParticle(Color colorOfParticle)
	{
		for (int i = 0; i < count; i++)
		{
			particleArr[i].startColor = colorOfParticle;
		}
		particleSys.Clear();
		particleSys.Emit(count);
		particleSys.SetParticles(particleArr, particleArr.Length);
	}

	public void changePlayState(bool playing)
	{
		playingParticle = playing;
	}

	protected override void OnLevelRotateEnd()
	{
		if(levelController.perspective==1|| levelController.perspective == 3)
		{
			Debug.Log(levelController.perspective);
		}
	}
}

public class CirclePosition
{
	public float radius = 0f, angle = 0f, time = 0f;
	public CirclePosition(float radius, float angle, float time)
	{
		this.radius = radius;   // 半径
		this.angle = angle;     // 角度
		this.time = time;       // 时间
	}
}