using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashMenu : MonoBehaviour
{
    [SerializeField]
    GameObject image1;
    [SerializeField]
    GameObject image2;
    [SerializeField]
    GameObject image3;
    int imagecounter = 1;

    private void Start()
    {
        image1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        image2.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        image3.GetComponent<Image>().color = new Color(1, 1, 1, 0);

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (imagecounter == 1)
            {
                StartCoroutine(FadeImage2(image1, true));
                StartCoroutine(FadeImage2(image2, false));
                imagecounter++;
            } else if(imagecounter == 2)
            {
                StartCoroutine(FadeImage2(image2, true));
                StartCoroutine(FadeImage2(image3, false));
                imagecounter++;
            } else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    IEnumerator FadeImage2(GameObject toFade, bool fadeAway)
    {
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                toFade.GetComponent<Image>().color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                toFade.GetComponent<Image>().color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
