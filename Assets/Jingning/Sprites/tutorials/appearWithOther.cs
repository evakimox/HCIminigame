using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appearWithOther : MonoBehaviour
{
    public GameObject other;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (other.activeSelf)
        {
            // do nothing then
        }
        else
        {
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 0f;
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
