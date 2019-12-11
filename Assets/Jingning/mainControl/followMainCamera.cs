using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject main = GameObject.FindGameObjectWithTag("Player");
        if (main != null)
        {
            Vector2 mainPos = main.transform.position;
            Vector2 cameraPos = transform.position;
            adjustInX(mainPos, cameraPos);
            adjustInY(mainPos, cameraPos);
        }
        
    }

    void adjustInX(Vector2 mainPos, Vector2 camPos)
    {
        if(Mathf.Abs(mainPos.x - camPos.x) > 6)
        {
            float moveDist = Mathf.Sign(mainPos.x - camPos.x)
                                * (Mathf.Abs(mainPos.x - camPos.x) - 6);
            Vector3 myPos = transform.position;
            myPos.x = myPos.x + moveDist;
            transform.position = myPos;
        }
    }

    void adjustInY(Vector2 mainPos, Vector2 camPos)
    {
        float threshold = 3f;
        if (Mathf.Abs(mainPos.y - camPos.y) > threshold)
        {
            float moveDist = Mathf.Sign(mainPos.y - camPos.y)
                                * (Mathf.Abs(mainPos.y - camPos.y) - threshold);
            Vector3 myPos = transform.position;
            myPos.y = myPos.y + moveDist;
            transform.position = myPos;
        }
    }
}
