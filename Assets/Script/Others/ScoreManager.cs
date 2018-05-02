using UnityEngine;
using System.Collections;

public class ScoreManager {

	private static ScoreManager inst;

	private ScoreManager() { }

	public static ScoreManager Instance
	{
		get
		{
			if(inst == null)
			{
				inst = new ScoreManager();
			}
			return inst;
		}
	}

	public int Score { get; set; }
}
