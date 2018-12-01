using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAwake : MonoBehaviour {

	private void Awake()
	{
		GameGlobal.GameLogic.GameGlobalInit();
	}

}
