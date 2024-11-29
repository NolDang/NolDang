using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public TalkManager talkManager;
    public Text talkText;
    public GameObject talkPanel;
    public bool isAction;
    public int talknum;
    public int talkIndex;
    public bool talkflag;
    public Image character1;
    public Image character2;
    public Image story;
    public GameObject gamestart;
    public GameObject select;
    public Image mainstory;
    public Text title;
    public Slider healthbar;

    public void Action()
    {
        if (isAction){
            isAction = false;
        }
        else{
            isAction = true;
            Talk(talknum, talkIndex);
        }
        talkPanel.SetActive(isAction);
        mainstory.gameObject.SetActive(false);
    }

    void Talk(int id, int talkIndex)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        talkText.text = talkData;
    }
    public void setnum(int num)
    {
            talknum = num;
    }
    void Update()
    {
        setnum(talknum);
        if(isAction){
            if (Input.GetButtonDown("Jump")){
                talkflag = true;
            }
        }
        if (talkflag){
            talkIndex++;
            talkflag = false;
            Talk(talknum, talkIndex);
        }
        if (talknum == 100 || talknum == 300 || talknum == 500){
            mainstory.gameObject.SetActive(false);
            healthbar.gameObject.SetActive(false);
            int flag = 1;
            if(talknum == 300){
                flag = 2;
            }
            if(talknum == 500){
                flag = 3;
            }
            if(flag == 1){ 
                if (talkIndex >= talkManager.talkDataLength100-1){
                    talkIndex = 0;
                    SceneManager.LoadScene(1);
                }
            }
            if(flag == 2){
                if(talkIndex >= talkManager.talkDataLength300-1){
                    talkIndex = 0;
                    SceneManager.LoadScene(2);
                }
            }

            if(flag == 3){
                if(talkIndex >= talkManager.talkDataLength500-1){
                    talkIndex = 0;
                    SceneManager.LoadScene(2);
                }
            }
            switch(talkIndex){
                case 0: case 2: case 5: case 7:
                    character1.gameObject.SetActive(true);
                    character2.gameObject.SetActive(false);
                    break;
                case 1: case 4: case 6:
                    character1.gameObject.SetActive(false);
                    character2.gameObject.SetActive(true);
                    break;
            }
        }
        if (talknum == 200 || talknum == 400 || talknum == 600){
            character2.gameObject.SetActive(false);
             int flag = 1;
            if(talknum == 400){
                flag = 2;
            }
            if(talknum == 600){
                flag = 3;
            }
            if(flag == 1){
            if (talkIndex >= talkManager.talkDataLength200-1){
                talkIndex = 0;
                story.gameObject.SetActive(false);
                mainstory.gameObject.SetActive(false);
                gamestart.gameObject.SetActive(true);
                select.gameObject.SetActive(true);
                title.gameObject.SetActive(true);
                isAction = false;
                }
            }
            if(flag == 2){
            if(talkIndex >= talkManager.talkDataLength400-1){
                talkIndex = 0;
                story.gameObject.SetActive(false);
                mainstory.gameObject.SetActive(false);
                gamestart.gameObject.SetActive(true);
                select.gameObject.SetActive(true);
                title.gameObject.SetActive(true);
                isAction = false;
            }
            }
            if(flag == 3){
                if(talkIndex >= talkManager.talkDataLength600-1){
                talkIndex = 0;
                story.gameObject.SetActive(false);
                mainstory.gameObject.SetActive(false);
                gamestart.gameObject.SetActive(true);
                select.gameObject.SetActive(true);
                title.gameObject.SetActive(true);
                isAction = false;
                }
            }
        }

        
    Debug.Log("talknum: " + talknum);
    Debug.Log("talkindex: " + talkIndex);
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public TalkManager talkManager;
    public Text talkText;
    public GameObject talkPanel;
    public bool isAction;
    public int talkIndex;
    public bool talkflag;
    public Image character1;
    public Image character2;

    public void Action()
    {
        if (isAction){
            isAction = false;
        }
        else{
            isAction = true;
            Talk(100, talkIndex);
        }
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, int talkIndex)
    {
        //string[] talkData = talkManager.GetTalk(talkIndex);
        //talkText.text = string.Join("\n", talkData);
        string talkData = talkManager.GetTalk(id, talkIndex);
        talkText.text = talkData;
    }
    void Update()
    {
        if(isAction){
            if (Input.GetButtonDown("Jump")){
                talkflag = true;
            }
        }
        if (talkflag){
            talkIndex++;
            talkflag = false;
            Talk(100, talkIndex);
        }
        if (talkIndex >= talkManager.talkDataLength)
            SceneManager.LoadScene(0);
        switch(talkIndex){
            case 0: case 2:
                character1.gameObject.SetActive(true);
                character2.gameObject.SetActive(false);
                break;
            case 1:
                character1.gameObject.SetActive(false);
                character2.gameObject.SetActive(true);
                break;
        }
    }
}
*/