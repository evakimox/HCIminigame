using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFocus : MonoBehaviour
{
    public float StartX;
    public float StartY;
    public float EndX;
    public float EndY;
    public SpriteRenderer black;
    public float targetCameraSize;
    private Camera OrthographicCamera;
    public int scaleSpeed = 10;
    private Vector2 startPosition;
    private Vector2 targetPosition; 

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        startPosition = new Vector3(StartX, StartY, transform.position.z);
        targetPosition = new Vector3(EndX, EndY, transform.position.z);
        OrthographicCamera = GetComponent<Camera>();
        float diffScale = (OrthographicCamera.orthographicSize - targetCameraSize)/scaleSpeed;
        float diffX = (StartX - EndX) / scaleSpeed;
        float diffY = (StartY-EndY) / scaleSpeed;
        StartCoroutine(CameraMove(diffScale, diffX, diffY));
       
    }

    private IEnumerator CameraMove(float diffScale, float diffX, float diffY)
    {
        bool scale = true;
        bool positionX = true;
        bool positionY = true; 
        yield return new WaitForSeconds(3.5f);
        while (scale || positionX || positionY)
        {

            if (Mathf.Abs(OrthographicCamera.orthographicSize - targetCameraSize) < 0.1f)
            {
                scale = false;
            }
            if (Mathf.Abs(StartX - EndX) < 0.1f)
            {
                positionX = false;
            }
            if (Mathf.Abs(StartY - EndY) < 0.1f)
            {
                positionY = false;
            }
            //Debug.Log(OrthographicCamera.orthographicSize);
            OrthographicCamera.orthographicSize -= diffScale;
            StartX -= diffX;
            StartY -= diffY;
            transform.position = new Vector3(StartX,StartY, transform.position.z);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(TurnBlack());
    }

    private IEnumerator TurnBlack()
    {
        while (black.color.a <= 1)
        {
            Color newAlpha = black.color;
            newAlpha.a += 0.05f;
            black.color = newAlpha;
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene("DevilTransform");
    }
}
