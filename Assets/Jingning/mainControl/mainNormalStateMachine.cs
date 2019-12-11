using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum mainState
{
    grounded = 0,
    air = 1,
    finishedStage = 2
}


public class mainNormalStateMachine : MonoBehaviour
{

    public float movingSpeed = 7f;
    /*
     state: 
        0站立：听取上左右控制
        1腾空：听取左右控制
        2落地但正在左右跑：听取上左右，但是animation不同 //待定
    */
    int state;

    // 是否已经检查到玩家把手指放在屏幕上
    bool touchStarted;
    Vector2 touchStartPos;

    void Start()
    {   
        // 刚进场景一般都是悬浮的
        state = (int)mainState.air;

        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(state == (int)mainState.finishedStage)
        {
            // 如果已经到目标门了，就不做任何事情，等着进下一关
            return;
        }
        checkOnGround();

        // 控制：如果没有检测到玩家有把手放屏幕上，就当没点过处理
        if (Input.touchCount != 1)
        {
            touchStarted = false;
            GetComponent<Animator>().ResetTrigger("running");
        }

        if (state == (int)mainState.air)
        {
            // 只听取左右
            leftRightCtrl();
        }
        if(state == (int)mainState.grounded)
        {
            leftRightCtrl();
            jumpCtrl();
        }
    }

    // 根据左右脚检查人物是否着地
    void checkOnGround()
    {
        Bounds bounds = GetComponent<Collider2D>().bounds;
        Vector3 leftExtent = bounds.extents;
        Vector3 rightExtent = bounds.extents;
        rightExtent.x = -rightExtent.x;
        Vector2 leftFoot = bounds.center - leftExtent;
        Vector2 rightFoot = bounds.center - rightExtent;
        // 左脚或右脚着地，均视为着地，更改air态为grounded态
        if (ifOnGroudAtPoint(leftFoot) || ifOnGroudAtPoint(rightFoot))
        {
            state = (int)mainState.grounded;
        }
        else
        {
            state = (int)mainState.air;
        }
        
    }

    // 分别用于检查左右脚哪只着地的脚本
    bool ifOnGroudAtPoint(Vector2 point)
    {
        RaycastHit2D[] leftHits = Physics2D.RaycastAll(point, -Vector2.up, 0.1f);
        foreach (RaycastHit2D leftHit in leftHits)
        {
            if (leftHit.collider != null)
            {
                if (leftHit.collider.gameObject.tag == "barrier")
                {
                    return true;
                }
            }
        }
        return false;
    }

    // 用于控制主角动画方向的脚本
    void checkSpriteDirection()
    {
        Vector2 localScale = transform.localScale;
        if (GetComponent<Rigidbody2D>().velocity.x > 0.05f)
        {
            localScale.x = -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
        else if (GetComponent<Rigidbody2D>().velocity.x < -0.05f)
        {
            localScale.x = Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
    }

    // 根据玩家操作计算的单位向量给出拟合动作的标准向量
    Vector3 directionMatching(Vector3 inputDir)
    {
        List<Vector3> directions = new List<Vector3>();
        directions.Add(new Vector3(0, 0));
        directions.Add(new Vector3(movingSpeed, 0));
        directions.Add(new Vector3(-movingSpeed, 0));
        directions.Add(new Vector3(0, movingSpeed));
        directions.Add(new Vector3(0, -movingSpeed));
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
        closestDir.x = closestDir.x * 0.5f;
        return closestDir;
    }

    // 左右控制人物运动
    void leftRightCtrl()
    {
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                touchStartPos = Input.touches[0].position;
                touchStarted = true;
            }
            if (touchStarted)
            {
                bool overMoveThreshold = (Input.touches[0].position - touchStartPos).magnitude > 50f;
                
                // 拖动超出50像素，认定为有效拖拽，根据方向给主角水平速度
                if (overMoveThreshold)
                {
                    checkSpriteDirection();
                    // 动画是否切换跑步状
                    if (state == (int)mainState.grounded)
                    {
                        GetComponent<Animator>().SetTrigger("running");
                    }
                    Vector3 inDir = (Input.touches[0].position - touchStartPos).normalized;
                    Vector3 useDir = directionMatching(inDir);
                    if(useDir.y == 0)
                    {
                        Vector3 curVelocity = GetComponent<Rigidbody2D>().velocity;
                        curVelocity.x = useDir.x;
                        GetComponent<Rigidbody2D>().velocity = curVelocity;
                    }
                    
                }
            }
        }
    }

    // 上下控制人物运动
    void jumpCtrl()
    {
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                touchStartPos = Input.touches[0].position;
                touchStarted = true;
            }
            if (touchStarted)
            {
                bool overMoveThreshold = (Input.touches[0].position - touchStartPos).magnitude > 50f;
                // 拖动超出50像素，认定为有效拖拽，根据方向给主角起跳速度
                if (overMoveThreshold)
                {
                    Vector3 inDir = (Input.touches[0].position - touchStartPos).normalized;
                    Vector3 useDir = directionMatching(inDir);
                    if (useDir.x == 0)
                    {
                        Vector3 curVelocity = GetComponent<Rigidbody2D>().velocity;
                        curVelocity.y = useDir.y;
                        GetComponent<Rigidbody2D>().velocity = curVelocity;
                    }
                }
            }
        }

    }

    // 有关主角状态机的接口
    public bool isStateFinish()
    {
        return (state == (int)mainState.finishedStage);
    }

    public void setStateFinish()
    {
        state = (int)mainState.finishedStage;
        GetComponent<Animator>().ResetTrigger("running");
    }
}
