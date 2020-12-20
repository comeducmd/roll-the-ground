using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChange : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("State", true);
 
    }

    public void GetPoint()
    {
        anim.SetTrigger("Happy");
    }

    public void Fail()
    {
        anim.SetBool("State", false);
    }

    public void LoseLife()
    {
        anim.SetTrigger("UnHappy");
    }

    
    void Update()
    {   
        if (Input.GetKey(KeyCode.LeftArrow)){
            Fail();
        }    

        if(Input.GetKey(KeyCode.Space)){
            GetPoint();
        }

        if(Input.GetKey(KeyCode.DownArrow)){
            LoseLife();
        }

    }
    
}
