using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureController : GameBehaviour {

	MeshRenderer meshRenderer;

	private void Awake()
	{
		GameBehavierInit();

		meshRenderer = GetComponent<MeshRenderer>();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update()
	{
		SetTextureScale();
	}

	void SetTextureScale()
	{
		float scalex = 0, scaley = 0;
		scaley = transform.localScale.y;
		switch(levelController.perspective % 2)
		{
			case 0:
				scalex = transform.localScale.x;
				break;
			case 1:
				scalex = transform.localScale.z;
				break;
		}
		meshRenderer.material.SetTextureScale(
				"_MainTex",
				new Vector2(scalex, scaley)
			);
	}

}
