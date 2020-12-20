using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HangulInput : MonoBehaviour

{

    // Use this for initialization

    public float txtfieldX, txtfieldY, txtfieldWidth, txtfieldHeight;
    public float boxX, boxY, boxWidth, boxHeight;
    public float beginAreaX, beginAreaY, beginAreaWidth, beginAreadHeight;
    public GUISkin gskin;

    public static string inputStr = "";

    private Vector2 scrollPosition;
    private Text scoreText;

    private string result = ""; 
    public string[] existWords;
    Coroutine runningCoroutine = null;

    private AudioSource audioSuccess;
    private AudioSource audioIncorct;

    void Start()

    {
        Screen.SetResolution(1280, 720, true);//해상도 고정
        Input.imeCompositionMode = IMECompositionMode.On;//IME사용
        //inputStr = "텍스트를 입력하세요.";
        existWords = GameObject.Find("GameManager").GetComponent<GameManager>().existWords;
        scoreText = GameManager.instance.myScore;
        audioSuccess = GameObject.Find("SaeDong").GetComponent<AudioSource>();
        audioIncorct = GameObject.Find("incorrectBgm").GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {

    }

    void OnGUI()
    {
        GUI.skin = gskin;

        inputStr = GUI.TextField(new Rect(txtfieldX, txtfieldY, txtfieldWidth, txtfieldHeight), inputStr, 80);

        GUI.Box(new Rect(boxX, boxY, boxWidth, boxHeight), "");

        GUILayout.BeginArea(new Rect(beginAreaX, beginAreaY, beginAreaWidth, beginAreadHeight));

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        string[] list = result.Split('*');

        foreach (string entry in list)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(entry);

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();

        GUILayout.EndArea();

        if (Event.current.type == EventType.KeyDown && Event.current.character == '\n' && inputStr.Length > 0)
        {   
            int i = 0;
            int flag = 0;
            while(true){
                if (inputStr == existWords[i]){
                    GameObject.Find("DongDong").GetComponent<DongDong>().RadiusUp();
                    GameObject.Find("SaeDong").GetComponent<AnimationChange>().GetPoint();
                    GameManager.instance.scoreCnt += 100;
                    int j = i;
                    while(true){
                        existWords[j] = existWords[j+1];
                        if (existWords[j+1] == "땡"){
                            break;
                        }
                        j++;
                    }
                    flag = 1;
                    audioSuccess.Play();
                }
                if (i > 18 && flag == 0){
                    if(runningCoroutine != null)
                    {
                        StopCoroutine(runningCoroutine);
                    }
                    runningCoroutine = StartCoroutine(GameObject.Find("GameManager").GetComponent<GameManager>().SpeedUp());
                    audioIncorct.Play();
                    break;
                }
                i++;
                if (i > 19){
                    break;
                }
            }
            Destroy(GameObject.Find(inputStr));
            scoreText.text = "Score : " + GameManager.instance.scoreCnt;

            result += inputStr + "*";
            inputStr = "";
            scrollPosition.y = 1000000;

        }



    }

}



