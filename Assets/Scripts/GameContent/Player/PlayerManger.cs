using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManger : MonoBehaviour {

	GameObject player;						//玩家
	GameObject playerStart;					//玩家起点
	GameObject playerCamera;                //玩家相机
	PlayerController playerController;              //玩家控制器

	bool isCameraMoving;                             //是否在相机动画（该动画不通过动画组件）
	Vector3 startPos;
	Vector3 endPos;

	private void Awake()
	{
		playerStart = transform.Find("PlayerStart").gameObject;
		player = transform.Find("Player").gameObject;
		playerCamera = player.transform.Find("PlayerCamera").gameObject;
		playerController = player.GetComponent<PlayerController>();
	}

	private void Start()
	{
		Respawn();
	}

	// Update is called once per frame
	void Update () {
		transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
		if(player.transform.position.y < -20.0f)
		{
			Respawn();
		}	
		if(isCameraMoving)
		{
			MoveCamera();
		}
	}

	void Respawn()
	{
		player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		playerController.enabled = false;
		startPos = playerCamera.transform.position;
		endPos = playerStart.transform.position + 3 * Vector3.up;
		startPos.z = -200;
		endPos.z = -200;
		isCameraMoving= true;
		player.transform.SetPositionAndRotation(
			playerStart.transform.position,
			Quaternion.Euler(0, 0, 0)
		);
	}

	void MoveCamera()
	{
		startPos = Vector3.Lerp(startPos, endPos, 2*Time.deltaTime);
		if (Vector3.Distance(startPos, endPos) < 0.1f)
		{
			startPos = endPos;
			isCameraMoving = false;
			playerController.enabled = true;
		}

		playerCamera.transform.position = startPos;
	}

}
