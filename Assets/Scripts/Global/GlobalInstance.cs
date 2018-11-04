using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameGlobal
{
	/*全局单例的基类
	 * 实现了一些基本操作
	 * 单例创建，事件监听，等
	 */
	class GlobalInstance<InstanceClass> where InstanceClass : new()
	{
		public static InstanceClass instance;

		//创建实例并初始化
		public static void CreatInstance()
		{
			/*CreatInstance实现所有GlobalInstance都需要实现的初始化操作
			子类的额外初始化操作应在构造函数中实现*/
			if (instance == null)
				instance = new InstanceClass();

		}

	}
}
