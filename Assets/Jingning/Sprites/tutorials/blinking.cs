using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinking : MonoBehaviour
{
    public float blinkingSpeed = 17;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float alpha = gameObject.GetComponent<SpriteRenderer>().color.a;
        if(alpha > 0.00005f)
        {
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            color.a = color.a - 1f/blinkingSpeed;
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 1f;
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
