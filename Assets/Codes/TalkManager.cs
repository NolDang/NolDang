using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public int talkDataLength100;
    public int talkDataLength200;
    public int talkDataLength300;
    public int talkDataLength400;
    public int talkDataLength500;
    public int talkDataLength600;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
        talkDataLength100 = GetTalkDataLength(100);
        talkDataLength200 = GetTalkDataLength(200);
        talkDataLength300 = GetTalkDataLength(300);
        talkDataLength400 = GetTalkDataLength(400);
        talkDataLength500 = GetTalkDataLength(500);
        talkDataLength600 = GetTalkDataLength(600);
    }


   void GenerateData()
    {
        
            
            talkData.Add(100, new string[]
            {"작업이 끝나고 해녀는 약속대로 음식을 제공한다.", 
            "자신이 직접 채취한 해산물로 만든 요리라고 한다.", 
            "노비스는 정말 맛있고 또 먹고싶다 말하자",
            "해녀는 이곳에서 북쪽으로 올라가면 해녀박물관과 해녀마을이 있고",
            "해녀에 대한 이야기를 접하며 직접 체험도 할 수 있다고 알려줬다.",
            "구조까지 3일이나 남았기에 노비스는 자신을 여행객이라 얘기하고",
            "해녀에게 박물관 옆에 있는 만장굴을 소개받는다.",
            "내일은 만장굴을 향해 가보고자 한다.",
            ""
            });
            talkData.Add(200, new string[]
            {"노비스는 산으로 가기 전에 먼저 눈에 띄는 자신의 외형을 이곳에 사는 생명체와 동일하게 바꾼다.",
            "그 과정에서 에너지를 소모하며 허기를 느끼게 된다.", 
            "달리 먹을게 없기에 일단 해안을 따라 쭉 걷는다.",
            "해안 절경과 자연 풍경을 감상하며 얼마나 걸었을까.. 지구의 한 생명체와 조우하게 된다.",
            "노비스는 배가 고프다고 먹을 것이 필요하다고 도움을 청한다.",
            "그러자 상대는 자신을 해녀라고 소개하며 먹을거리를 줄테니",
            "생업에 피해를 주는 해파리와 불가사리를 퇴치 작업을 도와달라 한다.",
            ""});
      
            talkData.Add(300, new string[]
            {"황금박쥐는 미개방구간에 서식하고 있다고 한다.", 
            "허탕을 친 노비스는 터덜터덜 만장굴을 나온다.", 
            "직원에게서 기념품으로 미니돌하르방과 돌하르방 공원 책자를 받는다.",
            "내일은 구조대가 오는 날이기에 산으로 이동한다.",
            ""
            });
            talkData.Add(400, new string[]
            {"해녀가 소개해준 만장굴에 도착한다.",
            "동굴 내부로 들어가보니 다양한 크기와 모양의 종유석과 석순들이 보인다.", 
            "시원한 공기와 신비로운 분위기 속에서 여러 조형물과 용암 석주를 구경한다.",
            "주변 사람들로부터 이곳에 황금박쥐가 있다는 말을 듣고 흥미를 느껴 찾아나선다.",
            ""});

            talkData.Add(500, new string[]
            {"한라산 등산에서 나오는 고난과 경치에서 나오는 감탄 대사1111",
            "한라산 등산에서 나오는 고난과 경치에서 나오는 감탄 대사222",
            "한라산 등산에서 나오는 고난과 경치에서 나오는 감탄 대사333",
            "한라산 등산에서 나오는 고난과 경치에서 나오는 감탄 대사444",
            ""});

            talkData.Add(600, new string[]
            {"한라산 입구에 도착한다.",
            "까마득한 높이에 걱정이 들었지만 등산을 시작한다.", 
            "노비스는 귀환할 때 지구인들에게 정체를 들키면 안되기에",
            "정해진 코스로 가지 않고 풀숲으로 들어간다.",
            "그러자 진드기들의 공격과 멧돼지를 마주치게 되는데..",
            ""});

        }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
    public int GetTalkDataLength(int id)
    {
        if (talkData.ContainsKey(id))
        {
            return talkData[id].Length;
        }
        else
        {
            return 0;
        }
    }
    // Update is called once per frame
    void Update()
    {     
        //int talkDataLength = GetTalkDataLength(100);
        Debug.Log("Talk data length for ID 200: " + talkDataLength200);
    }
}
