using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameGlobal
{
	/*所有游戏系统组件单例的基类
	 */
	class GameSystem<InstanceClass> : GlobalInstance<InstanceClass> where InstanceClass : new()
	{
		public GameSystem()
		{
			//注册事件
			GameLogic.GameAwakeEvent += OnGameAwake;
			GameLogic.GameBeginEvent += OnGameBegin;
			GameLogic.GameDestoryEvent += OnGameDestroy;
			GameLogic.GameEndEvent += OnGameEnd;
			GameSetting.instance.SettingChangeEvent += OnSettingChanged;
			
		}

		protected virtual void OnGameBegin()
		{ }
		protected virtual void OnGameEnd()
		{ }
		protected virtual void OnGameDestroy()
		{ }
		protected virtual void OnSettingChanged()
		{ }
		protected virtual void OnGameAwake()
		{ }

	}
}
