//GameManager Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	//장면 관리(Scene Manager 같은)를 사용하기 위한 네임스페이스.
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	[Header("# Game Control")]
	public bool isLive;	//시간 정지 여부 확인 변수
	public float gameTime;	//게임 시간 변수
	public float maxGameTime = 2 * 10f; //최대 게임 시간 변수(20초).
	[Header("# Player Info")]
	public int playerId;
	public int upgradeId;
	public float health;
	public float maxHealth = 100;
    public int level;
	public int kill;

	public int gold;
	public int hplevel;
    public int damagelevel;
    public int playerspeedlevel;
    public int attackspeedlevel;
    public int countlevel;
    public int rangelevel;
	public int exp;
	public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
	public LevelUp uiLevelUp;
	public Result uiResult;
	public GameObject enemyCleaner;
	public GameObject UpgradeGroup;
	public Text money;

    void Awake()
    {
        gold = PlayerPrefs.GetInt("PlayerGold", 20001);
        hplevel = PlayerPrefs.GetInt("HpLevel", 0);
        damagelevel = PlayerPrefs.GetInt("DamageLevel", 0);
        playerspeedlevel = PlayerPrefs.GetInt("PlayerspeedLevel", 0);
        attackspeedlevel = PlayerPrefs.GetInt("AttackspeedLevel", 0);
        countlevel = PlayerPrefs.GetInt("CountLevel", 0);
        rangelevel = PlayerPrefs.GetInt("RangeLevel", 0);
        instance = this;
        // gold = 50001;
        upgradetext();
    }

	public void GameStart(int id) {
		playerId = id;
		health = maxHealth + UpgradeList.Hp;

		player.gameObject.SetActive(true);
		uiLevelUp.Select(playerId % 2);
		Resume();

		AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

	public void UpgradeEnhance(int num)
    {
        upgradeId = num;
        if (gold > 1000){
            switch (upgradeId){
                case 0:
                    if (hplevel < 5){
                        gold -= 1000;
                        hplevel++;
                        PlayerPrefs.SetInt("HpLevel", hplevel);
                        PlayerPrefs.Save();
                        UpgradeGroup.transform.Find("hp/cost").GetComponent<Text>().text = "1000+(" + hplevel * 10 + ")";
                    }
                    break;
                case 1:
                    if (damagelevel < 5){
                        gold -= 1000;
                        damagelevel++;
                        PlayerPrefs.SetInt("DamageLevel", damagelevel);
                        PlayerPrefs.Save();
                        UpgradeGroup.transform.Find("damage/cost").GetComponent<Text>().text = "1000+(" + damagelevel * 10 + ")";
                    }
                    break;
                case 2:
                    if (playerspeedlevel < 5){
                        gold -= 1000;
                        playerspeedlevel++;
                        PlayerPrefs.SetInt("PlayerspeedLevel", playerspeedlevel);
                        PlayerPrefs.Save();
                        UpgradeGroup.transform.Find("playerspeed/cost").GetComponent<Text>().text = "1000+(" + playerspeedlevel * 5 + "%)";
                    }
                    break;
					case 3:
                    if (attackspeedlevel < 5){
                        gold -= 1000;
                        attackspeedlevel++;
                        PlayerPrefs.SetInt("AttackspeedLevel", attackspeedlevel);
                        PlayerPrefs.Save();
                        UpgradeGroup.transform.Find("attackspeed/cost").GetComponent<Text>().text = "1000+(" + attackspeedlevel * 10 + "%)";
                    }
                    break;
                case 4:
                    if (countlevel < 5){
                        gold -= 1000;
                        countlevel++;
                        PlayerPrefs.SetInt("CountLevel", countlevel);
                        PlayerPrefs.Save();
                        UpgradeGroup.transform.Find("count/cost").GetComponent<Text>().text = "1000+(" + countlevel + ")";
                    }
                    break;
                case 5:
                    if (rangelevel < 3){
                        gold-= 1000;
                        rangelevel++;
                        PlayerPrefs.SetInt("RangeLevel", rangelevel);
                        PlayerPrefs.Save();
                        UpgradeGroup.transform.Find("range/cost").GetComponent<Text>().text = "1000+(" + rangelevel + ")";
                    }
                    break;
            }
        }
	}

		public void upgradetext()
    {
            UpgradeGroup.transform.Find("hp/cost").GetComponent<Text>().text = "1000+(" + hplevel * 10 + ")";
            UpgradeGroup.transform.Find("damage/cost").GetComponent<Text>().text = "1000+(" + damagelevel * 10 + ")";
            UpgradeGroup.transform.Find("playerspeed/cost").GetComponent<Text>().text = "1000+(" + playerspeedlevel * 5 + "%)";
            UpgradeGroup.transform.Find("attackspeed/cost").GetComponent<Text>().text = "1000+(" + attackspeedlevel * 10 + "%)";
            UpgradeGroup.transform.Find("count/cost").GetComponent<Text>().text = "1000+(" + countlevel + ")";
            UpgradeGroup.transform.Find("range/cost").GetComponent<Text>().text = "1000+(" + rangelevel + ")";
    }

	public void GameOver() {
		StartCoroutine(GameOverRoutine());
	}

	IEnumerator GameOverRoutine() {
		isLive = false;
		
		yield return new WaitForSeconds(0.5f);

		uiResult.gameObject.SetActive(true);
		uiResult.Lose();
		Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory() {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine() {
        isLive = false;
		enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

 public void GameRetry() {
    string currentSceneName = SceneManager.GetActiveScene().name;
    SceneManager.LoadScene(currentSceneName);
}


	void Update() {
		money.text = "Gold : " + gold;
		if (!isLive)
			return;

		gameTime += Time.deltaTime;

		if (gameTime > maxGameTime) {
			gameTime = maxGameTime;
			GameVictory();
		}
	}

	public void GetExp() {
		if (!isLive)	//EnemyCleaner로 경험치를 못얻게 하기 위함
			return;

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