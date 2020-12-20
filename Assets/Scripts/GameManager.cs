using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //int,float,Vector2 등은 primitive 타입 call by value 

    public static GameManager instance;
    private int cnt;
    private float[] yPosList = new float[4] { 0, -0.1f, -0.2f, -0.3f};
    private float yPos;
    private string[] wordAray = new string[40] {
        "자바스크립트", "유니티", "파이썬", "썬크림", "해질녘", 
        "로또", "주식", "성균관대학교", "컴퓨터교육과", "오프라인", 
        "온라인", "멋쟁이사자처럼", "쇠똥구리", "맨드라미", "귀뚜라미", 
        "여치", "비단제비나방", "똥", "바나나똥", "먹구름",
        "씨엠디", "개미", "호랑나비", "소나기", "적란운",
        "필통", "손세정제", "코로나", "마스크", "학생증",
        "비누", "아이스크림", "누가바", "메로나", "비비빅",
        "더위사냥", "쿠앤크", "스크류바", "옥동자", "구슬아이스크림"
    };//하드코딩 문자열 데이터
    public string[] existWords = new string[40]{
        "땡", "땡", "땡", "땡", "땡", "땡", "땡", "땡", "땡", "땡",
        "땡", "땡", "땡", "땡", "땡", "땡", "땡", "땡", "땡", "땡",
        "땡", "땡", "땡", "땡", "땡", "땡", "땡", "땡", "땡", "땡",
        "땡", "땡", "땡", "땡", "땡", "땡", "땡", "땡", "땡", "땡"
    };

    public int level;
    public int Speedy;
    public int lifeInt;
    private Text myTime;
    private Text myLife;
    public Text myScore;
    private float timeCnt = 0;
    public int scoreCnt;
    
    public GameObject Dong;
    public string lifeString;
    private string resultString;
    public Button restartBtn;
  

    public IEnumerator SpeedUp()
    {   
        var bgSpeed = GameObject.Find("Background").GetComponent<Background>();
        //var DongSpeed = GameObject.Find("MonsterDong").GetComponent<MonsterDong>();

        GameObject.Find("background1").GetComponent<AudioSource>().pitch = 1.1f;
        bgSpeed.bgSpeed = 0.3f;
        //DongSpeed.DongSpeed = 2f * DongSpeed.DongSpeed;
        Speedy = 1;

        yield return new WaitForSecondsRealtime(5f);

        GameObject.Find("background1").GetComponent<AudioSource>().pitch = 1.0f;
        bgSpeed.bgSpeed = 0.2f;
        //DongSpeed.DongSpeed = 0.1f;
        Speedy = 0;
    } 

    // 게임 끝났을때 호출되는 함수. 나중에 더 멋지게 만들자.
    void Stop()
    {   
        GameObject.Find("background1").GetComponent<AudioSource>().Stop();
        GameObject.Find("gameover").GetComponent<AudioSource>().Play();
        resultString = "Your score is " + scoreCnt;
        Time.timeScale = 0;
        GameObject.Find("ScoreResult").GetComponent<Text>().text = resultString;
        /*
        Color temp = GameObject.Find("GameOverPanel").GetComponent<Image>().color;
        temp.a = 1;
        GameObject.Find("GameOverPanel").GetComponent<Image>().color = temp;
        */
        GameObject.Find("Canvas").transform.Find("GameOverPanel").gameObject.SetActive(true);
        restartBtn = GameObject.Find("RestartBtn").GetComponent<Button>();
        restartBtn.onClick.AddListener(()=> { LoadHome(); });
    }

    void LoadHome()
    {   
        Time.timeScale = 1;
        SceneManager.LoadScene("start");
    }

    // Start is called before the first frame update
    void Start()
    {   
        Speedy= 0;
        
        cnt = 0;
        lifeInt = 3;
        scoreCnt = 0;
        myTime = GameObject.Find("Time").GetComponent<Text>();
        myLife = GameObject.Find("Life(Text)").GetComponent<Text>();
        myScore = GameObject.Find("Score").GetComponent<Text>();
        myScore.text = "Score : " + scoreCnt;
        instance = this; // 외부에서 instance를 참조하기 위해
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        level = (cnt*cnt)/360000;//period

        if (cnt % 150-level == 0)
        {
            yPos = yPosList[(cnt % 57)%4];
            GameObject curDong = Instantiate(Dong, new Vector3(2, yPos,1), Quaternion.identity);

            if (Speedy == 1)
            {
                curDong.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f, 0);
            }
            else
            {
                curDong.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 0);
            }
            
            //curDong.name = wordAray[(cnt%77)%40];
            curDong.name = wordAray[Random.Range(0, 40)];
            for (int i = 0; i < 20; i++)
            {
                if (existWords[i] == "땡"){
                    existWords[i] = curDong.name;
                    break;
                }
            }

            TextMesh textContentofDong=curDong.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
            textContentofDong.text = curDong.name;
        }
        
        cnt++;
        timeCnt += Time.deltaTime;
        myTime.text = "Time : " + timeCnt.ToString("N2");
        lifeString = "";
        for(int i = 0; i < lifeInt; i++)
        {
            lifeString += "♥";
        }
        for(int i = 0; i < 3 - lifeInt; i++)
        {
            lifeString += "♡";
        }
        myLife.text = lifeString;

        // 목숨 0이면 1.5초 뒤에 게임 중지
        if(lifeInt==0)
        {   
            Invoke("Stop", 1.5f);
        }
       
    }


}
