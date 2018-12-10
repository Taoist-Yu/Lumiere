using UnityEngine;
using System.Collections;



[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CubeUV : MonoBehaviour
{
	public enum Direction
	{
		ClockWise,
		Anti_ClockWise
	}

	public Material mat;

	public Direction direction;

	// Use this for initialization
	void Start()
	{
		switch (direction)
		{
			case Direction.ClockWise:
				DrawClockWiseCube();
				break;
			case Direction.Anti_ClockWise:
				DrawAnti_ClockWiseCube();
				break;
		}
	}

	/// <summary>
	/// 按顺时针画立方体
	/// </summary>
	void DrawClockWiseCube()
	{
		gameObject.GetComponent<MeshRenderer>().material = mat;

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();

		//设置顶点
		mesh.vertices = new Vector3[]
		{   new Vector3(0, 0, 0),
			new Vector3(1, 0, 0),
			new Vector3(1, 1, 0),
			new Vector3(0, 1, 0),
			new Vector3(0, 1, 1),
			new Vector3(1, 1, 1),
			new Vector3(1, 0, 1),
			new Vector3(0, 0, 1),
		};

		//顺时针设置三角形的方向
		mesh.triangles = new int[]
	   {
			3, 0, 1,
			3,1,2,
			2,4,3,
			2,5,4,
			7,4,5,
			7,5,6,
			0,7,6,
			0,6,1,
			3,4,7,
			3,7,0,
			1,6,5,
			1,5,2

	   };

	}

	/// <summary>
	/// 按逆时针画立方体
	/// </summary>
	void DrawAnti_ClockWiseCube()
	{
		gameObject.GetComponent<MeshRenderer>().material = mat;

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();

		//设置顶点
		mesh.vertices = new Vector3[]
		{   new Vector3(0, 0, 0),
			new Vector3(1, 0, 0),
			new Vector3(1, 1, 0),
			new Vector3(0, 1, 0),
			new Vector3(0, 1, 1),
			new Vector3(1, 1, 1),
			new Vector3(1, 0, 1),
			new Vector3(0, 0, 1),
		};

		//逆时针设置
		mesh.triangles = new int[]
		{
			3,1,0,
			3,2,1,
			2,3,4,
			2,4,5,
			7,5,4,
			7,6,5,
			0,6,7,
			0,1,6,
			3,7,4,
			3,0,7,
			1,5,6,
			1,2,5
		};
	}

}
