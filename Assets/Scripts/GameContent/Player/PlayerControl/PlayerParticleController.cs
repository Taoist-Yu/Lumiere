using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour
{
	ParticleSystem particleSys;
	ParticleSystem.Particle[] particleArr;
	List<CircleAttribute> circleList = new List<CircleAttribute>();

	public int count = 5;
	public float size = 1f; // 粒子大小
	public float minRadius = 5.0f;
	public float maxRadius = 12.0f;
	public float maxSpeed = 1f;
	public float minSpeed = 0.5f;

	public static int lightQuantity = 1;

	void Start()
	{
		particleArr = new ParticleSystem.Particle[count];

		// 初始化粒子系统
		particleSys = this.GetComponent<ParticleSystem>();
		particleSys.startSpeed = 0;
		particleSys.startColor = new Color(85f / 255f, 233f / 255f, 255f / 255f);
		particleSys.startLifetime = 5;
		particleSys.startSize = size;
		particleSys.loop = false;
		particleSys.maxParticles = count;
		particleSys.Emit(count);
		particleSys.GetParticles(particleArr);
		RandomlyStart();
	}

	// 初始化各粒子位置
	void RandomlyStart()
	{
		for (int i = 0; i < count; i++)
		{
			RandomCircle();
			particleArr[i].position = circleList[i].position;
		}
		particleSys.SetParticles(particleArr, particleArr.Length);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.G)) AddAParticle();
		for (int i = 0; i < count; i++)
		{
			circleList[i].position = Quaternion.AngleAxis(circleList[i].anglePerFrame, circleList[i].axis) * (circleList[i].position);
			particleArr[i].position = circleList[i].position;
		}
		particleSys.SetParticles(particleArr, particleArr.Length);
	}

	//产生一个粒子
	void AddAParticle()
	{
		particleArr = null; //内存释放

		//添加粒子数量
		count++;
		lightQuantity += 1;
		particleArr = new ParticleSystem.Particle[count];
		particleSys.maxParticles = count;
		particleSys.Emit(1);
		particleSys.GetParticles(particleArr);

		//粒子初始化
		RandomCircle();
		particleArr[count - 1].position = circleList[count - 1].position;

		//转换颜色并重新产生粒子
		for (int i = 0; i < count; i++)
		{
			switch (lightQuantity % 5)
			{
				case 1:
					particleArr[i].startColor = new Color(85f / 255f, 233f / 255f, 255f / 255f);
					break;
				case 2:
					particleArr[i].startColor = new Color(166f / 255f, 255f / 255f, 165f / 255f);
					break;
				case 3:
					particleArr[i].startColor = new Color(255f / 255f, 204f / 255f, 149f / 255f);
					break;
				case 4:
					particleArr[i].startColor = new Color(255f / 255f, 133f / 255f, 132f / 255f);
					break;
				case 0:
					particleArr[i].startColor = Color.white;
					break;
				default: break;
			}
			particleArr[i].remainingLifetime = 0;
		}
		particleSys.SetParticles(particleArr, particleArr.Length);
		for (int i = 0; i < count; i++)
		{
			particleArr[i].remainingLifetime = 5;
		}
		particleSys.SetParticles(particleArr, particleArr.Length);

	}

	//产生一个随机粒子数据并添加到列表末尾
	void RandomCircle()
	{
		// 随机每个粒子距离中心的半径，同时希望粒子集中在平均半径附近
		float midRadius = (maxRadius + minRadius) / 2;
		float minRate = Random.Range(1.0f, midRadius / minRadius);
		float maxRate = Random.Range(midRadius / maxRadius, 1.0f);
		float radius = Random.Range(minRadius * minRate, maxRadius * maxRate);
		float theta = Random.Range(0.0f, 360.0f);

		float anglePerFrame = Random.Range(0.5f, 1.0f);

		Vector3 position = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);

		circleList.Add(new CircleAttribute(position, anglePerFrame)); //在列表末尾插入元素
	}

	public void UpdateParticle(int n)
	{

		int add = n - count;
		for (int i = 0; i < add; i++)
		{
			AddAParticle();
		}
	}
}