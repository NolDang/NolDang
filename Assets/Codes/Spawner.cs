// Spawner Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    int level;  //소환 레벨
    float timer;

    void Awake() {
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;    //최대 시간에 몬스터 데이터 크기로 나눠 자동으로 구간 시간 계산
    }

   void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;

        // 게임 시간이 끝나기 1분 전이라면 마지막 몬스터로 설정
        if (GameManager.instance.gameTime >= GameManager.instance.maxGameTime - 60f)
        {
            level = spawnData.Length - 1; // 마지막 몬스터
        }
        else
        {
            // 변수를 이용해 자동으로 구간 시간 계산
            level = Mathf.FloorToInt(GameManager.instance.gameTime / levelTime);
            level = Mathf.Min(level, spawnData.Length - 2); // 마지막 몬스터 전까지만 계산
        }

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn() {
        GameObject enemy = GameManager.instance.pool.Get(0);    //프리펩이 하나가 되었으므로 0으로 변경
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // Random Range가 1부터 시작하는 이유는 spawnPoint 초기화 함수 GetComponentsInChildren에 자기 자신(Spawner)도 포함되기 때문에.
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

//직렬화(Serialization) : 개체를 저장/전송하기 위해 변환
[System.Serializable]
public class SpawnData {
    public float spawnTime; //몬스터 소환 시간
    public int spriteType;  //몬스터 스프라이트 타입
    public int health;      //몬스터 체력
    public float speed;     //몬스터 스피드
}