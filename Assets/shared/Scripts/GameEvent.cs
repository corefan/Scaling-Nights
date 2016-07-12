using UnityEngine;
using System.Collections;

public static class GameEvent
{

	public const string SHOW_CURSOR = "SHOW_CURSOR";
	public const string HIDE_CURSOR = "HIDE_CURSOR";

	public const string SHOW_INVENTORY = "SHOW_INVENTORY";

	public const string SHOW_DIALOG = "SHOW_DIALOG";
	public const string HIDE_DIALOG = "HIDE_DIALOG";

	public const string ADD_ITEM = "ADD_ITEM";
	public const string REMOVE_ITEM = "REMOVE_ITEM";

	public static bool gameOver = false;
	public static bool isCursorEnabled = false;
	public static bool isUiEnabled = false;
	public static bool isPause = false;

	public static void Pause ()
	{
		if (isPause) {
			isPause = false;
			isUiEnabled = false;
			Cursor.lockState = CursorLockMode.Locked;
			Time.timeScale = 1;
			Messenger.Broadcast (GameEvent.HIDE_CURSOR);

		} else {
			isPause = true;
			isUiEnabled = true;
			Cursor.lockState = CursorLockMode.None;
			Messenger<int>.Broadcast (GameEvent.SHOW_CURSOR, 0);
		}
	}

	public static void GameOver ()
	{
		gameOver = true;
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;
	}
}
