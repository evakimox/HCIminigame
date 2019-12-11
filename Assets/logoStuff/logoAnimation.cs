using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class logoAnimation : MonoBehaviour
{
    public string entranceSceneName;
    public float timeToAppear = 100;
    float alpha;
    // Start is called before the first frame update
    void Start()
    {
        alpha = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(alpha <= timeToAppear)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha / timeToAppear);
        }
        else if(alpha > 150 && entranceSceneName.Length > 0)
        {
            SceneManager.LoadScene(entranceSceneName);
        }
        alpha = alpha + 1f;
    }
}
