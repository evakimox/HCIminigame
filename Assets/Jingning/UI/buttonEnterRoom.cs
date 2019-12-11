using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonEnterRoom : MonoBehaviour
{
    public string destinationRoom;
    bool prevFrameTouching;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<userInteract>();
        prevFrameTouching = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<userInteract>().touchingSelf())
        {
            prevFrameTouching = true;
        }
        else
        {
            if(prevFrameTouching)
            {
                // released
                SceneManager.LoadScene(destinationRoom);
            }
        }
    }
}
