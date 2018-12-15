using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameGlobal
{
	/* 静态类
	 * 实现了基本的全局逻辑
	 * 管理全局单例
	 */
	class GameLogic
	{
		/*事件定义*/
		//游戏程序启动
		public delegate void GameAwakeHandler();
		public static event GameAwakeHandler GameAwakeEvent;
		//游戏开始(一局)
		public delegate void GameBeginHandler();
		public static event GameBeginHandler GameBeginEvent;
		//游戏结束(一局)
		public delegate void GameEndHandler();
		public static event GameEndHandler GameEndEvent;
		//游戏程序退出
		public delegate void GameDestoryHandler();
		public static event GameDestoryHandler GameDestoryEvent;

		//游戏全局初始化
		public static void GameGlobalInit()
		{
			if (isInit == true)
				return;

			//创建单例
			GameSetting.CreatInstance();
			GameData.CreatInstance();
			AudioSystem.CreatInstance();

			//
			isInit = true;

			//发布GameAwakeEvent
			GameAwakeEvent();

		}

		//初始化是否完成
		private static bool isInit = false;

	}
}
