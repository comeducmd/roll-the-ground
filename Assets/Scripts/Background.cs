using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float bgSpeed = 0.2f;
    Material myMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()//고정 프레임
    {
        /*
        pos = transform.position;
        transform.Translate(new Vector2(-bgSpeed, 0));
        if (pos.x < -18)
        {
            pos.x = 18;
            transform.position = pos;
        }
        */
        Vector2 newOffset = myMaterial.mainTextureOffset;
        newOffset.Set(newOffset.x + (bgSpeed * Time.deltaTime),0);
        myMaterial.mainTextureOffset = newOffset;
    }
}
