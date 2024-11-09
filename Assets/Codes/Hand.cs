//Hand Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.45f, -0.25f, 0);
    Vector3 rightPosReverse = new Vector3(-0.25f, -0.25f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -90);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -90);

    void Awake() {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate() {
        bool isReverse = player.flipX;

        if (isLeft) {   //근접 무기
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
        else {  //원거리 무기
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
    }
}