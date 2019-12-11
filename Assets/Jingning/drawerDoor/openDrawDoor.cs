using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDrawDoor : MonoBehaviour
{
    public GameObject openedDoor;
    public GameObject closedDoor;
    public bool initOpenDoor;
    private bool touched;
    // Start is called before the first frame update
    void Start()
    {
        touched = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        if (initOpenDoor)
        {
            openedDoor.SetActive(true);
            closedDoor.SetActive(false);
        }
        else
        {
            openedDoor.SetActive(false);
            closedDoor.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.touchCount == 0)
        {
            touched = false;
        }
        if (Input.touchCount == 1)
        {
            if (!touched)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);
                if(hit.collider != null)
                {
                    if(hit.collider.gameObject == closedDoor)
                    {
                        closedDoor.SetActive(false);
                        openedDoor.SetActive(true);
                        touched = true;
                        GetComponent<AudioSource>().Play();
                    }
                    else if(hit.collider.gameObject == openedDoor)
                    {
                        closedDoor.SetActive(true);
                        openedDoor.SetActive(false);
                        touched = true;
                        GetComponent<AudioSource>().Play();
                    }
                }
            }
            
        }
    }
}
