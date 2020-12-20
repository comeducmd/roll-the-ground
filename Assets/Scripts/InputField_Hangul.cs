using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;

public class InputField_Hangul : MonoBehaviour
{

    //폰트가 별로여서, 폰트가 이쁜 TMPro로 교체함
    private TMP_InputField TMPinputField;//화면에 보여지는 텍스트1
    public TMP_Text TMPtextComponent;//화면에 보여지는 텍스트2
    private string tmpString;//한글만 추출한 텍스트를 저장하는 임시 string
    public string[] existWords;

    //처음에 public Text tmpText;로 InputField_Hangul밑에 자식으로 있는 tmpText 오브젝트를 끌어와서, text를 수정하려 했으나,
    //출력만 되고, value 수정이 안돼서, Unity docs에 있는 대로, InputField를 가져와서 수정함(이유는 모르겠음. Text에 대해서 좀 더 고찰을 해봐야 함)
    //https://docs.unity3d.com/2018.3/Documentation/ScriptReference/UI.Text.html


    // Start is called before the first frame update
    void Start()
    {
        TMPinputField = gameObject.GetComponent<TMP_InputField>();//gameObject = 이 스크립트를 가진 오브젝트를 가리키는 지시어.
        existWords = GameObject.Find("GameManager").GetComponent<GameManager>().existWords;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
        //Debug.Log(EventSystem.current.currentInputModule.input.compositionString);
        //Debug.Log(Input.compositionString);
        //Debug.Log(TMPinputField.caretPosition);
        enterEvent();//엔터키가 눌렸을 때의 이벤트
    }

    private void enterEvent()//엔터키가 눌렸을 때의 상황을 다루는 모듈
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))//만약 엔터가 눌리면 
        {   
            Debug.Log(existWords[0]);
            Debug.Log(existWords[1]);
            int i = 0;
            while(true){
                if (existWords[i] == "땡" || i > 19){
                    break;
                    //SpeedUp
                }
                if (TMPinputField.text == existWords[i]){
                    int j = i;
                    while(true){
                        existWords[j] = existWords[j+1];
                        if (existWords[j+1] == "땡"){
                            break;
                        }
                        j++;
                    }
                }
                i++;
            }
            Destroy(GameObject.Find(TMPinputField.text));
            TMPinputField.text = "";//입력창 텍스트 초기화

        }

    }

}
