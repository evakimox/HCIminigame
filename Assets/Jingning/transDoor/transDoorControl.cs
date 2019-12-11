using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transDoorControl : MonoBehaviour
{
    public GameObject toDoor;
    public GameObject swrilPrefab;
    private bool justArrived;
    // Start is called before the first frame update
    void Start()
    {
        justArrived = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playerArriving()
    {
        this.justArrived = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !justArrived)
        {
            // 传送门无目标地点，或地点就是自己，则不传送
            if(toDoor == gameObject || toDoor == null)
            {
                return;
            }
            toDoor.GetComponent<transDoorControl>().playerArriving();
            // 进入传送门则制造两团漩涡
            GameObject originSwril = Instantiate(swrilPrefab, transform.position,Quaternion.identity);
            GameObject destiSwril = Instantiate(swrilPrefab, toDoor.transform.position, Quaternion.identity);
            originSwril.GetComponent<swrilControl>().isDest = false;
            destiSwril.GetComponent<swrilControl>().isDest = true;
            // 禁用玩家对象，但把玩家的地址传给目标漩涡
            destiSwril.GetComponent<swrilControl>().main = other.gameObject;
            other.gameObject.SetActive(false);
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        this.justArrived = false;
    }
}
