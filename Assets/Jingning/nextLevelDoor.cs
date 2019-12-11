using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevelDoor : MonoBehaviour
{
    public string nextLevel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AudioSource sound = GetComponent<AudioSource>();
        if (sound.isPlaying)
        {
            if(sound.time >= 0.90f * sound.clip.length)
            {
                Debug.Log("entered transition");
                loadNextScene();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<mainNormalStateMachine>() != null)
            {
                collision.gameObject.GetComponent<mainNormalStateMachine>().setStateFinish();
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                collision.gameObject.transform.position = transform.position;
            }
            GetComponent<AudioSource>().Play();
        }
    }

    void loadNextScene()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
