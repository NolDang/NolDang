//Plyaer Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Vector2 inputVec;   //입력 값 저장 변수
	public float speed; //속도 관리용 변수

    Rigidbody2D rigid;

    void Awake() {  //시작할 때 한번만 실행되는 생명주기 함수
        rigid = GetComponent<Rigidbody2D>();    //GetComponent<T> : 오브젝트에서 컴포넌트 T를 가져오는 함수
    }

    void Update() { //하나의 프레임마다 한 번씩 호출되는 생명주기 함수. 대문자 유의할 것.
		//Input : 유니티에서 받는 모든 입력을 관리하는 클래스. Axis 값은 -1 ~ 1.
		inputVec.x = Input.GetAxisRaw("Horizontal");    //좌,우 방향키 또는 A,D 키
		inputVec.y = Input.GetAxisRaw("Vertical");      //상,하 방향키 또는 W,S 키
	}

    void FixedUpdate() {    //물리 연산 프레임마다 호출되는 생명주기 함수
		//위치 이동
		//normalized : 벡터 값의 크기가 1이 되도록 좌표가 수정된 값. 대각선 이동시 값 고정을 위함.
		Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;    //fixedDeltaTime : 물리 프레임 하나가 소비한 시간. Update에서는 DeltaTime 사용.
        rigid.MovePosition(rigid.position + nextVec);  //현재 위치 + 나아갈 방향 : (x, y) 좌표 활용
    }
}