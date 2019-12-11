using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sleepControl : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer Awaken;
    public SpriteRenderer Sleepy;
    void Start()
    {
        StartCoroutine(closeEyes());
    }

    private IEnumerator closeEyes()
    {
        yield return new WaitForSeconds(1f);
        while (Awaken.color.a > 0)
        {
            Color newAlpha = Awaken.color;
            Color newAlpha2 = Sleepy.color;
            newAlpha.a -= 0.1f;
            newAlpha2.a += 0.1f;
            //Awaken.color = newAlpha;
            Sleepy.color = newAlpha2;
            yield return new WaitForSeconds(0.1f);
        }
    }
}