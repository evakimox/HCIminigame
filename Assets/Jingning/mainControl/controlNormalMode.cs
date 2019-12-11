using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlNormalMode : MonoBehaviour
{
    Vector2 touchStartPos;
    bool touchStarted = false;
    bool onGround;
    
    void Start()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
        onGround = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkOnGround();
        if (Input.touchCount != 1)
        {
            touchStarted = false;
        }
        if (Input.touchCount == 1 && onGround)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                touchStartPos = Input.touches[0].position;
                touchStarted = true;
            }
            if(Input.touches[0].phase == TouchPhase.Moved && touchStarted)
            {
                bool overMoveThreshold = (Input.touches[0].position - touchStartPos).magnitude > 50f;
                if (overMoveThreshold)
                {
                    checkSpriteDirection();
                    Vector3 inDir = (Input.touches[0].position - touchStartPos).normalized;
                    Vector3 moveDir = directionMatching(inDir);
                    GetComponent<Rigidbody2D>().velocity = 8.5f * moveDir;
                }
            }            
        }
    }

    void checkSpriteDirection()
    {
        Vector2 localScale = transform.localScale;
        if(GetComponent<Rigidbody2D>().velocity.x > 0.05f)
        {
            localScale.x = -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
        else if(GetComponent<Rigidbody2D>().velocity.x < -0.05f)
        {
            localScale.x = Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
    }

    void checkOnGround()
    {
        if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 0.05f)
        {
            onGround = false;
        }
        else
        {
            onGround = true;
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
        foreach(Vector3 direction in directions)
        {
            float myDotProd = Vector3.Dot(direction, inputDir);
            if (myDotProd > largestDotProd)
            {
                largestDotProd = myDotProd;
                closestDir = direction;
            }
        }
        if (!onGround)
        {
            closestDir.y = 0f;
        }
        closestDir.x = closestDir.x * 0.5f;
        return closestDir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.collider.gameObject;
        if (obj.tag == "barrier" && transform.position.y > obj.transform.position.y)
        {
            onGround = true;
        }
    }
}
