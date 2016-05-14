using UnityEngine;
using System.Collections;

public static class GameEvent
{
	public const string SHOW_UI = "SHOW_UI";
	public const string HIDE_UI = "HIDE_UI";
	public const string NEAR_INTERACTIVE = "NEAR_INTERACTIVE";
	public const string FAR_INTERACTIVE = "FAR_INTERACTIVE";
	public const string ADD_ITEM = "ADD_ITEM";
	public const string REMOVE_ITEM = "REMOVE_ITEM";
	public static bool GameOver = false;
	public static bool isUiEnabled = false;
	public static bool isPause = false;

	public static void Pause ()
	{
		isPause = true;
		isUiEnabled = true;
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;
	}

	public static void UnPause ()
	{
		isPause = false;
		isUiEnabled = false;
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;
	}
}
