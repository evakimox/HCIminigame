using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevilTransform : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer black;
    public SpriteRenderer Devil1;
    public SpriteRenderer Devil2;
    public SpriteRenderer Devil3;
    public SpriteRenderer Devil4;
    public SpriteRenderer Devil5;
    public SpriteRenderer Devil6;
    public SpriteRenderer Devil7;

    private int envolve = 0;
    void Start()
    {
        StartCoroutine(TurnOnTheLight());

    }

    private IEnumerator DevilsTransform()
    {
        yield return new WaitForSeconds(1f);
        while (Devil1.color.a > 0)
        {
            Color newAlpha = Devil1.color;

            newAlpha.a -= 0.1f;

            Devil1.color = newAlpha;

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        while (Devil2.color.a > 0)
        {
            Color newAlpha = Devil2.color;

            newAlpha.a -= 0.1f;

            Devil2.color = newAlpha;

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        while (Devil3.color.a > 0)
        {
            Color newAlpha = Devil3.color;

            newAlpha.a -= 0.2f;

            Devil3.color = newAlpha;

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        while (Devil4.color.a > 0)
        {
            Color newAlpha = Devil4.color;

            newAlpha.a -= 0.2f;

            Devil4.color = newAlpha;

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        while (Devil5.color.a > 0)
        {
            Color newAlpha = Devil5.color;

            newAlpha.a -= 0.2f;

            Devil5.color = newAlpha;

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(TurnBlack());
    }

    private IEnumerator TurnOnTheLight()
    {
        while (black.color.a >= 0)
        {
            Color newAlpha = black.color;
            newAlpha.a -= 0.02f;
            black.color = newAlpha;
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(DevilsTransform());
    }
    private IEnumerator TurnBlack()
    {
        while (black.color.a <= 1)
        {
            Color newAlpha = black.color;
            newAlpha.a += 0.1f;
            black.color = newAlpha;
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene("Tracing");
    }
}
