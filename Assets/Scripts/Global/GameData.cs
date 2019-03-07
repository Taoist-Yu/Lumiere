using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameGlobal
{
	class GameData : GlobalInstance<GameData>
	{
		//当前最新关卡
		public static int maxLevel = 1;
		//当前关卡
		public static int currentLevel;

		//游戏是否暂停
		public static bool isPausing;

		//是否通关
		public static bool isVictory = false;
	}
}
