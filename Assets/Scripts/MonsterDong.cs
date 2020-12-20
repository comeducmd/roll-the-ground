using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDong : MonoBehaviour
{
    public int life;
    private AnimationChange Animation;
    public string[] existWords;
    // Start is called before the first frame update
    void Start()
    {
        life = GameManager.instance.lifeInt;
        existWords = GameManager.instance.existWords;
        Animation = GameObject.Find("SaeDong").GetComponent<AnimationChange>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DongDong")
        {
            // 애니메이션
            if (life > 1)
            {
                Animation.LoseLife();
            }
            else
            {
                Animation.Fail();
            }
            // 라이프 깎기
            GameManager.instance.lifeInt = life - 1;

            // 안녕... 자신을 없애버리는 몬스터똥
            for (int i = 0; i < 20; i++){
                if (existWords[i] == this.name){
                    int j = i;
                    while (true){
                        existWords[j] = existWords[j+1];
                        if (existWords[j+1] == "땡"){
                            break;
                        }
                        j++;
                    }
                }
            }

            Destroy(this.gameObject, 0.5f);
        }
    }
}
