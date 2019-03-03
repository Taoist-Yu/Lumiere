using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RayLight
{
	public enum LightColor
	{
		blue = 1,
		green = 2,
		yellow = 3,
		red = 4,
		white = 0
	};
	public LightColor lightColor;
	public int lightLevel;

	public int LightQuantity
	{
		get
		{
			return (int)lightColor + 5 * lightLevel;
		}
	}

	public Color Color
	{
		get
		{
			Color color = new Color();
			switch (lightColor)
			{
				case LightColor.blue:
					color = new Color(85f / 255f, 233f / 255f, 255f / 255f);
					break;
				case LightColor.green:
					color = new Color(166f / 255f, 255f / 255f, 165f / 255f);
					break;
				case LightColor.yellow:
					color = new Color(255f / 255f, 204f / 255f, 149f / 255f);
					break;
				case LightColor.red:
					color = new Color(255f / 255f, 133f / 255f, 132f / 255f);
					break;
				case LightColor.white:
					color = Color.white;
					break;
			}
			return color;
		}
	}
	
	public static RayLight GetLight(int LightQuantity)
	{
		RayLight light = new RayLight();
		switch (LightQuantity % 5)
		{
			case 0:
				light.lightColor = LightColor.white;
				break;
			case 1:
				light.lightColor = LightColor.blue;
				break;
			case 2:
				light.lightColor = LightColor.green;
				break;
			case 3:
				light.lightColor = LightColor.yellow;
				break;
			case 4:
				light.lightColor = LightColor.red;
				break;
		}
		light.lightLevel = (LightQuantity - 1) / 5 + 1;
		if (LightQuantity == 0)
			light.lightLevel = 0;
		return light;
	}
	
	public static RayLight GetLight(LightColor color,int level)
	{
		RayLight light = new RayLight();
		light.lightColor = color;
		light.lightLevel = level;
		return light;
	}

}