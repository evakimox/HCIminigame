using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userInteract : MonoBehaviour
{
    bool touchingScreen;
    Vector2 touchStartPosition;
    bool touchStarted;
    // Start is called before the first frame update
    void Start()
    {
        touchStarted = false;
        Input.simulateMouseWithTouches = false;
        touchingScreen = false;
    }

    public bool touchedSelf()
    {
        List<GameObject> hitObjects = touchedObjects();
        foreach(GameObject hit in hitObjects)
        {
            if(hit == gameObject)
            {
                return true;
            }
        }
        return false;
    }

    // 连续检测IO，是否正在点自己
    public bool touchingSelf()
    {
        List<GameObject> hitObjects = touchingObjects();
        foreach (GameObject hit in hitObjects)
        {
            if (hit == gameObject)
            {
                return true;
            }
        }
        return false;
    }

    // private helper function
    List<GameObject> allObjectsGivenPosition(Vector2 position)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero);
        List<GameObject> hitObjects = new List<GameObject>();
        for(int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            hitObjects.Add(hit.collider.gameObject);
        }
        return hitObjects;
    }

    public List<GameObject> touchedObjects()
    {
        List<GameObject> touchedObjects = new List<GameObject>();
        // 检测到鼠标点击
        if (Input.GetMouseButton(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!touchStarted)
            {
                touchStarted = true;
                return allObjectsGivenPosition(touchPos);
            }
        }
        // 检测到触屏点击
        else if (Input.touchCount > 0)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            if (!touchStarted)
            {
                touchStarted = true;
                return allObjectsGivenPosition(touchPos);
            }
        }
        // 说明玩家没有在点任何事物
        else if (touchStarted)
        {
            touchStarted = false;
        }
        return new List<GameObject>();
    }

    public Vector2 dragDirection()
    {
        bool mouse = Input.GetMouseButton(0);
        bool touch = Input.touchCount > 0;
        // 如果有鼠标点或者是有手指点
        if(mouse || touch)
        {
            // 如果还暂时没有记录过曾经点击，记录这次点击的位置
            if (!touchingScreen)
            {
                touchingScreen = true;
                if (mouse)
                {
                    touchStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                if (touch)
                {
                    touchStartPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                }
                return Vector2.zero;
            }
            // 如果记录过曾经点击过了，则输出当前位置和初始记录位置向量的单位向量
            else
            {
                Vector2 touchCurrentPosition = Vector2.zero;
                if (mouse)
                {
                    touchCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                if (touch)
                {
                    touchCurrentPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                }
                Vector2 direction = touchCurrentPosition - touchStartPosition;
                return direction.normalized;
            }
        }
        // 如果没有鼠标点，手指点，则直接输出0向量，并且清空touchingScreen状态
        else
        {
            if (touchingScreen)
            {
                touchingScreen = false;
            }
            return Vector2.zero;
        }
    }

    public Vector2 touchingPosition()
    {
        bool mouse = Input.GetMouseButton(0);
        bool touch = Input.touchCount > 0;
        if (mouse || touch)
        {
            if (mouse)
            {
                return Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                return Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            }
        }
        else
        {
            return new Vector2(9999, 9999);
        }
    }

    public List<GameObject> touchingObjects()
    {
        List<GameObject> touchedObjects = new List<GameObject>();
        // 检测到鼠标点击
        if (Input.GetMouseButton(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchStarted = true;
            return allObjectsGivenPosition(touchPos);
        }
        // 检测到触屏点击
        else if (Input.touchCount > 0)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            touchStarted = true;
            return allObjectsGivenPosition(touchPos);
        }
        // 说明玩家没有在点任何事物
        else if (touchStarted)
        {
            touchStarted = false;
        }
        return new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
