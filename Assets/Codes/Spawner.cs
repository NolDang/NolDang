using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime; // 소환 레벨 구간을 결정하는 변수 선언
    int level;
    float timer;

    public SpawnData[] defaultSpawnData;
    public bool isSpawningEnabled = false;

    void Awake()
    {
        int spawnPointCount = 4;
        spawnPoint = new Transform[spawnPointCount];

        for (int i = 0; i < spawnPointCount; i++)
        {
            GameObject sp = new GameObject("SpawnPoint" + i);
            sp.transform.parent = transform; // Spawner의 자식으로 설정
            spawnPoint[i] = sp.transform;
        }

        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / (spawnData.Length - 1); 

        defaultSpawnData = new SpawnData[spawnData.Length];
        // spawnData 배열의 길이만큼 defaultSpawnData 배열 생성
        for (int i = 0; i < spawnData.Length; i++)
        {
            defaultSpawnData[i] = new SpawnData
            {
                spawnTime = spawnData[i].spawnTime, // 몬스터의 기본 소환 시간 저장
                spriteType = spawnData[i].spriteType, // 몬스터의 기본 종류 저장
                health = spawnData[i].health, // 몬스터의 기본 체력 저장
                speed = spawnData[i].speed // 몬스터의 기본 속도 저장
            };
        }
    }

    void Update()
    {
        if(!isSpawningEnabled)
        GameManager.instance.gameTime = 0;  
        if (!GameManager.instance.isLive || !isSpawningEnabled)
        return;

        timer += Time.deltaTime;
        UpdateSpawnPoints();

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

    void UpdateSpawnPoints()
{
    Vector3 playerPos = GameManager.instance.player.transform.position;

    float radius = 15f; // 스폰 반경 설정

    for (int i = 0; i < spawnPoint.Length; i++)
    {
        float angle = i * Mathf.PI * 2 / spawnPoint.Length;
        Vector3 newPos = playerPos + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        spawnPoint[i].position = newPos;
    }
}
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0); // 프리펩이 하나가 되었으므로 0으로 변경
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]); // 레벨에 따라 몬스터 소환
        // Spawn에서 Init 함수 사용
    }

    public void SetEasyMode()
    {
        for (int i = 0; i < spawnData.Length; i++)
        {
            // 몬스터의 소환 시간, 체력, 속도를 기본값으로 재설정하여 쉬운 난이도 적용
            spawnData[i].spawnTime = defaultSpawnData[i].spawnTime;
            spawnData[i].health = defaultSpawnData[i].health;
            spawnData[i].speed = defaultSpawnData[i].speed;
        }
    }

    public void SetHardMode()
    {
        for (int i = 0; i < spawnData.Length; i++)
        {
            // 몬스터의 소환 시간을 줄여 더 자주 등장하게 함
            spawnData[i].spawnTime = defaultSpawnData[i].spawnTime / 2.0f;
            // 몬스터의 체력을 1.5배로 증가시켜 더 강하게 만듦
            spawnData[i].health = Mathf.RoundToInt(defaultSpawnData[i].health * 1.5f);
            // 몬스터의 속도를 1.5배로 증가시켜 더 빠르게 움직이게 함
            spawnData[i].speed = defaultSpawnData[i].speed * 1.5f;
        }
    }
}


[System.Serializable]
// 직렬화: 복잡한 형태의 타입을 인스펙터 창에서 보여줌.
public class SpawnData
{
    public float spawnTime; // 몬스터의 소환간격
    public int spriteType; // 몬스터의 종류
    public int health; // 몬스터의 체력
    public float speed; // 몬스터의 스피드
}
