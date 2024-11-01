//GameManager Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	[Header("# Game Control")]
	public bool isLive;	//시간 정지 여부 확인 변수
	public float gameTime;	//게임 시간 변수
	public float maxGameTime = 2 * 10f; //최대 게임 시간 변수(20초).
	[Header("# Player Info")]
	public int health;
	public int maxHealth = 100;
    public int level;
	public int kill;
	public int exp;
	public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
	public LevelUp uiLevelUp;

    void Awake() {
		instance = this;
	}

	void Start() {
		health = maxHealth;

		//임시 스크립트 (첫번째 캐릭터 선택)
		uiLevelUp.Select(0);
	}

	void Update() {
		if (!isLive)
			return;

		gameTime += Time.deltaTime;

		if (gameTime > maxGameTime) {
			gameTime = maxGameTime;
		}
	}

	public void GetExp() {
		exp++;

		if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)]) {
			level++;
			exp = 0;
			uiLevelUp.Show();
		}
	}

	public void Stop() {
		isLive = false;
		Time.timeScale = 0;
	}

    public void Resume() {
        isLive = true;
        Time.timeScale = 1;	//값이 1보다 크면 그만큼 시간이 빠르게 흐름. 모바일 게임에서 시간 가속하는 것이 이것..
    }
}