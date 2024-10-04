using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // static으로 선언된 변수는 인스펙터에 나타나지 않음.
    public Player player;

    void Awake(){
        instance = this;

    }
}
