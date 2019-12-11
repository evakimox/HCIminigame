using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolveDoorControl : MonoBehaviour
{
    int windSpeed;
    float oneTimeWindHeight;
    public int maxWindSpeed;
    public GameObject wind;
    public GameObject animationObj;
    // Start is called before the first frame update
    void Start()
    {
        windSpeed = 0;
        oneTimeWindHeight = wind.transform.localScale.y;
        wind.SetActive(windSpeed > 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(animationObj.GetComponent<Animator>().enabled && windSpeed == 0)
        {
            animationObj.GetComponent<Animator>().enabled = false;
        }
        if(!animationObj.GetComponent<Animator>().enabled && windSpeed > 0)
        {
            animationObj.GetComponent<Animator>().enabled = true;
        }
    }

    void adjustWind()
    {
        wind.SetActive(windSpeed > 0);
        Vector3 windScale = wind.transform.localScale;
        windScale.y = windSpeed * oneTimeWindHeight;
        wind.transform.localScale = windScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && windSpeed < maxWindSpeed)
        {
            windSpeed += 1;
            animationObj.GetComponent<Animator>().enabled = true;
            animationObj.GetComponent<Animator>().speed = (float) ((float)windSpeed / (float)maxWindSpeed);
        }
        else
        {
            windSpeed = 0;
        }
        adjustWind();
    }
}
