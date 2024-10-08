// Enemy Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
     public RuntimeAnimatorController[] animCon; //애니메이터 컨트롤러
    public float health;    //몬스터 현재 체력
    public float maxHealth; //몬스터 최대 체력
    public float speed;
    public Rigidbody2D target;

    bool isLive;
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake() {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        if (!isLive)
            return;
        Vector2 dirVec = target.position - rigid.position;  // 방향 = 위치 차이의 정규화(Normalized). 위치 차이 = 타겟 위치 - 나의 위치.
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);   //플레이어의 키 입력값을 더한 이동 = 몬스터의 방향 값을 더한 이동
        rigid.velocity = Vector2.zero; //물리 속도가 이동에 영향을 주지 않도록 속도 제거
    }

    void LateUpdate() {
        if (!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

        void OnEnable() {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();   //플레이어 할당
        isLive = true;  //생존 상태 true
        health = maxHealth; //최대 체력으로 설정
    }

    //데이터를 가져오기 위한 초기화 함수
    public void Init(SpawnData data) {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

}