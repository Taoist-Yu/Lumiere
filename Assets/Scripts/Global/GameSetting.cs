using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameGlobal
{
	class GameSetting : GlobalInstance<GameSetting>
	{
		/*事件定义*/
		public delegate void SettingChangeHandler();
		public event SettingChangeHandler SettingChangeEvent; 


	}
}
