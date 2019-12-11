using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlCrazyMode : MonoBehaviour
{
    public GameObject shadow;
    int shadowCountFrame;
    int shadowFrame;
    Vector2 touchStartPos;
    bool touchStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        shadowFrame = 2;
        shadowCountFrame = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GetComponent<Rigidbody2D>().velocity.magnitude > 0.05f)
        {
            createShadow();
        }
        
        if(Input.touchCount != 1)
        {
            touchStarted = false;
        }
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                touchStartPos = Input.touches[0].position;
                touchStarted = true;
            }
            if (Input.touches[0].phase == TouchPhase.Moved && touchStarted)
            {
                bool overMoveThreshold = (Input.touches[0].position - touchStartPos).magnitude > 50f;
                if (overMoveThreshold && GetComponent<Rigidbody2D>().velocity.magnitude < 0.05f)
                {
                    Vector3 inDir = (Input.touches[0].position - touchStartPos).normalized;
                    Vector3 moveDir = directionMatching(inDir);
                    GetComponent<Rigidbody2D>().velocity = 12f * moveDir;
                }
            }
        }
    }

    void createShadow()
    {
        shadowCountFrame = shadowCountFrame + 1;
        if(shadowCountFrame >= shadowFrame)
        {
            GameObject curShadow = Instantiate(shadow, transform.position, Quaternion.identity);
            curShadow.transform.localScale = 0.7f * curShadow.transform.localScale;
            Color shadowColor = curShadow.GetComponent<SpriteRenderer>().color;
            shadowColor.a = 0.5f;
            curShadow.GetComponent<SpriteRenderer>().color = shadowColor;
            shadowCountFrame = 0;
        }
        
    }

    Vector3 directionMatching(Vector3 inputDir)
    {
        List<Vector3> directions = new List<Vector3>();
        directions.Add(new Vector3(0, 0));
        directions.Add(new Vector3(1, 0));
        directions.Add(new Vector3(-1, 0));
        directions.Add(new Vector3(0, 1));
        directions.Add(new Vector3(0, -1));

        Vector3 closestDir = directions[0];
        float largestDotProd = 0;
        foreach (Vector3 direction in directions)
        {
            float myDotProd = Vector3.Dot(direction, inputDir);
            if (myDotProd > largestDotProd)
            {
                largestDotProd = myDotProd;
                closestDir = direction;
            }
        }
        return closestDir;
    }
}
