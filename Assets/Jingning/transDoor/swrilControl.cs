using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swrilControl : MonoBehaviour
{
    float count;
    float countMax;
    public bool isDest;
    public GameObject main;
    // Start is called before the first frame update
    void Start()
    {
        countMax = 100;
        count = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count = count + 1;
        if(count > countMax)
        {
            Destroy(gameObject);
        }
        transform.Rotate(0, 0, 100f * Time.deltaTime);
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = (float)((countMax - count) / countMax);
        GetComponent<SpriteRenderer>().color = color;
    }

    private void OnDestroy()
    {
        if (isDest)
        {
            main.SetActive(true);
            main.transform.position = transform.position;    
        }
    }
}
