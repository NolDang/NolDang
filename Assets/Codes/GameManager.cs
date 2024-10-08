using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // static으로 선언된 변수는 인스펙터에 나타나지 않음.
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public Player player;
    public PoolManager pool;

    void Awake(){
        instance = this;

    }
    void Update() {
		gameTime += Time.deltaTime;

		if (gameTime > maxGameTime) {
			gameTime = maxGameTime;
		}
	}
}
