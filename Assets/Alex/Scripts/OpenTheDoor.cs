using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenTheDoor : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    private SpriteRenderer door;
    private SpriteRenderer girlDispear;
    public SpriteRenderer black;
    public GameObject girl;
    public GameObject stairs;

    void Start()
    {
        door = GetComponent<SpriteRenderer>();
        girlDispear = girl.GetComponent<SpriteRenderer>();
        girl.GetComponent<Animator>().Rebind();
        girl.GetComponent<Animator>().enabled = false;
        StartCoroutine(FoundTheDoor());
    }

    // Update is called once per frame
    void Update()
    {
        if (door.color.a >=1)
        {
            timer += Time.deltaTime;
            if (timer < 1f)
            {
                transform.Rotate(Vector3.up, Space.Self);
            }


            if( timer > 1.5f)
            {

                girl.GetComponent<Animator>().enabled = true;
                if (girl.transform.position.x > -14f)
                {
                    girl.transform.position = new Vector3(girl.transform.position.x - 0.05f, girl.transform.position.y, girl.transform.position.z);
                }

                else {
                    if (girlDispear.color.a > 0)
                    {
                        Color newAlpha = girlDispear.color;
                        newAlpha.a -= 0.05f;
                        girlDispear.color = newAlpha;
                    }
                    if (girlDispear.color.a <= 0)
                    {
                        if (black.color.a <= 1)
                        {
                            Color newAlpha = black.color;
                            newAlpha.a += 0.05f;
                            black.color = newAlpha;
                        }
                        else {
                            SceneManager.LoadScene("welcome");
                        }
                    }

                } 
            }
        }

    }
    private IEnumerator FoundTheDoor()
    {
        while (door.color.a <= 1)
        {
            Color newAlpha = door.color;
            newAlpha.a += 0.05f;
            door.color = newAlpha;
            yield return new WaitForSeconds(0.05f);

        }

    }


}
