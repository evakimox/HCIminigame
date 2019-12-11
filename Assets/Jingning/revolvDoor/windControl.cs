using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "mainNormal")
        {
            //这边最好还是写到角色里面去
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
            Vector3 pos = collision.gameObject.transform.position;
            pos.y = pos.y + 0.1f;
            collision.gameObject.transform.position = pos;
        }
    }
}
