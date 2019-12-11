using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restrictOpcacity : MonoBehaviour
{
    public float maxAlpha;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<SpriteRenderer>().color.a > maxAlpha)
        {
            GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, maxAlpha);
        }
    }
}
