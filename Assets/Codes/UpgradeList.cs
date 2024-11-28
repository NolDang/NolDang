using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeList : MonoBehaviour
{
 
 public static int Hp
    {
        get { switch (GameManager.instance.hplevel)
                {
                case 0: return 0;
                case 1: return 10;
                case 2: return 20;
                case 3: return 30;
                case 4: return 40;
                default: return 50;
                }
            }
    }
    public static float Damage
    {
        get { switch (GameManager.instance.damagelevel)
                {
                case 0: return 1f;
                case 1: return 1.1f;
                case 2: return 1.2f;
                case 3: return 1.3f;
                case 4: return 1.4f;
                default: return 1.5f;
                } 
            }
    }
    public static float Playerspeed
    {
        get { switch (GameManager.instance.playerspeedlevel)
                {
                case 0: return 1f;
                case 1: return 1.05f;
                case 2: return 1.1f;
                case 3: return 1.15f;
                case 4: return 1.2f;
                default: return 1.25f;
                }
            }
    }
    public static float Attackspeed
    {
        get { switch (GameManager.instance.attackspeedlevel)
                {
                case 0: return 1f;
                case 1: return 0.9f;
                case 2: return 0.8f;
                case 3: return 0.7f;
                case 4: return 0.6f;
                default: return 0.5f;
                }
            }
    }
    public static int Count
    {
        get { switch (GameManager.instance.countlevel)
                {
                case 0: return 0;
                case 1: return 1;
                case 2: return 2;
                case 3: return 3;
                case 4: return 4;
                default: return 5;
                }
            }
    }
    public static int Range
    {
        get { switch (GameManager.instance.rangelevel)
                {
                case 0: return 0;
                case 1: return 1;
                case 2: return 2;
                default: return 3;
                }
            }
    }
}
