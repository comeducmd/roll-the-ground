using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DongDong : MonoBehaviour
{   
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public int flag = 1;
    public AudioSource audioSource;

    void ChangeImage()
    {        
        if (spriteRenderer.sprite == sprites[0])
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if (spriteRenderer.sprite == sprites[1])
        {
            spriteRenderer.sprite = sprites[2];
        }
        else if (spriteRenderer.sprite == sprites[2])
        {
            spriteRenderer.sprite = sprites[3];
            flag = 0;
        }
    }

    public void RadiusUp()
    {   
        transform.localScale += new Vector3(0.03f, 0.03f, 0);
        transform.position += new Vector3(0.04f, 0.03f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];       

    }

    void FixedUpdate()
    {   
        if (flag == 1)
        {
            transform.Rotate(0, 0, -2);
        }

        if(Input.GetKey(KeyCode.Tab))
        { 
            RadiusUp();
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            ChangeImage();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        audioSource.Play();
        ChangeImage();

    }
}
