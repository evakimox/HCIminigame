using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public float start;
    public float end;
    public GameObject door;
    public GameObject stairs;
    public GameObject mainGirl;
    private int direction = 0;// 1 = right, 0 = left
    private float Timer;
    public Camera OrthographicCamera;
    public GameObject road;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Timer < 8f)
        {
            if (direction == 0 )
            {
                StartCoroutine(CameraLeftMove(start));
            }
            else if (direction == 1)
            {
                StartCoroutine(CameraRightMove(end));
            }
        }
        Timer += Time.deltaTime;
        if (Timer >= 8f)
        {

            if (road.transform.position.x < -8f)
            {
                road.transform.position = new Vector3(road.transform.position.x + 0.05f, road.transform.position.y, road.transform.position.z);
            }
            if (OrthographicCamera.orthographicSize <= 3.5f)
            {
                OrthographicCamera.orthographicSize += 0.05f;
            }
            if (Timer > 10f && door.activeSelf == false)
            {   
                door.SetActive(true);
            }
        }
    }

    private IEnumerator CameraLeftMove(float startPosition)
    {
        bool left = true;
        while (left)
        {
            if (transform.position.x >= startPosition)
            {
                transform.position = new Vector2(transform.position.x - 0.01f, transform.position.y);
            }
            else
            {
                left = false;
                direction = 1;

            }
            yield return new WaitForSeconds(0.1f);
        }


    }
    private IEnumerator CameraRightMove(float endPosition)
    {
        bool right = true;
        while (right)
        {
            if (transform.position.x <= endPosition)
            {
                transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
            }
            else
            {
                right = false;
                direction = 0;

            }
            yield return new WaitForSeconds(0.1f);
        }

    }
}