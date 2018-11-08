using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : GameBehaviour {

	//四面碰撞体数组
	public GameObject[] colliders;
	//管理碰撞体的根物体(一个空物体)
	public GameObject colliderRoot;

	protected override void GameBehavierInit()
	{
		base.GameBehavierInit();
		colliderRoot = GameObject.Find("Colliders");
		colliders = new GameObject[4];
		colliders[0] = GameObject.Find("Colliders/SouthCollider");
		colliders[1] = GameObject.Find("Colliders/EastCollider");
		colliders[2] = GameObject.Find("Colliders/NorthCollider");
		colliders[3] = GameObject.Find("Colliders/WestCollider");
		for (int i = 1; i < 4; i++)
			colliders[i].SetActive(false);

		//实体的受光模式默认为吸收（漫反射）
		scatteringMode = ScatteringMode.diffuse;

	}

	private void Awake()
	{
		GameBehavierInit();
	}

	protected override void OnLevelRotateBegin()
	{
		base.OnLevelRotateBegin();
		foreach(GameObject collider in colliders)
		{
			collider.SetActive(false);
		}
	}

	protected override void OnLevelRotateEnd()
	{
		base.OnLevelRotateEnd();
		colliders[levelController.perspective].SetActive(true);
		colliderRoot.transform.SetPositionAndRotation(
			colliderRoot.transform.position,
			Quaternion.Euler(0, 0, 0)
		);
	}

}
