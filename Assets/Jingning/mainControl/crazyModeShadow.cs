using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crazyModeShadow : MonoBehaviour
{
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Color curColor = sr.color;
        if(curColor.a <= 0)
        {
            Destroy(gameObject);
        }
        curColor.a = curColor.a - 0.025f;
        GetComponent<SpriteRenderer>().color = curColor;
    }
}
