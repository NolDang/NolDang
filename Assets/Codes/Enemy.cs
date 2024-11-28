//Enemy Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public RuntimeAnimatorController[] animCon;
    public float health;
    public float maxHealth;
    public float speed;
    public Rigidbody2D target;  //쫓아갈 타겟(플레이어)

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate() {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))   //GetCurrentAnimationStateInfo : 현재 상태 정보를 가져오는 함수
            return;

        Vector2 dirVec = target.position - rigid.position;  // 방향 = 위치 차이의 정규화(Normalized). 위치 차이 = 타겟 위치 - 나의 위치.
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);   //플레이어의 키 입력값을 더한 이동 = 몬스터의 방향 값을 더한 이동
        rigid.velocity = Vector2.zero; //물리 속도가 이동에 영향을 주지 않도록 속도 제거
    }

    void LateUpdate() {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable() {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();   //플레이어 할당
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    //데이터를 가져오기 위한 초기화 함수
    public void Init(SpawnData data)
    //Spawner의 SpawnData에서 지정한 데이터를 받아주는 함수
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        // 지정한 스프라이트 타입을 animCon 배열의 인덱스로 활용
        speed = data.speed; // 지정한 스피드
        maxHealth = data.health; // 최대체력 = 지정한 체력
        health = data.health;

        // 세 번째 몬스터인지 확인
        if (data.spriteType == 2) // spriteType이 2라면 세 번째 몬스터
        {
            // Rigidbody2D 크기 조정
            rigid.mass = 2f; // 필요하면 mass도 변경
            rigid.drag = 0.5f; // 필요하면 물리 특성 변경

            // Collider2D 크기 조정 (CapsuleCollider2D 사용)
            CapsuleCollider2D capsule = coll as CapsuleCollider2D;
            if (capsule != null)
            {
                capsule.size = new Vector2(2f, 4f); // Collider 크기 변경
                capsule.offset = new Vector2(0f, 1f); // Collider 위치 조정
            }
        }
        else
        {
            // 기본 크기 복원
            CapsuleCollider2D capsule = coll as CapsuleCollider2D;
            if (capsule != null)
            {
                capsule.size = new Vector2(1f, 2f); // 기본 Collider 크기
                capsule.offset = Vector2.zero; // 기본 Collider 위치
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;  //총알 데미지만큼 Enemy 체력 감소
        StartCoroutine(KnockBack());

        if(health > 0) {
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;    //rigidbody의 물리적 비활성화는 simulated를 false로 설정.
            spriter.sortingOrder = 1;   //SpriteRenderer의 Sorting Order를 감소시켜 다른 몬스터를 가리지 않게 함.
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.gold += 1;
            GameManager.instance.GetExp();
            PlayerPrefs.SetInt("PlayerGold", GameManager.instance.gold);
            PlayerPrefs.Save();
            
            if (GameManager.instance.isLive)    //게임 승리시 사운드 테러 방지
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
        }
    }

    IEnumerator KnockBack() {
        yield return wait;  //다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead() {
        gameObject.SetActive(false);
    }
}