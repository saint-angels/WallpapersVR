using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundFader : SingletonComponent<BackgroundFader> {

    [SerializeField]
    private Image imageFade;

    private float fadeIntensity = 0.5f;
    private bool fadingOut, fadingIn;
    private float fadeTimer = 0;
    private Color fadeColor;

    public void StartFadeOut() {
        if (fadingIn || fadingOut) return;
        fadingOut = true;
        fadeTimer = 0;
        fadeColor.a = 0f;
    }

    public void StartFadeIn() {
        if (fadingIn || fadingOut) return;
        fadingIn = true;
        fadeTimer = 0f;
        fadeColor.a = 1f;
    }

    public void Reset() {
        fadingIn = false;
        fadingOut = false;
        fadeColor.a = 0;
        imageFade.color = fadeColor;
    }

    // Use this for initialization
    void Start () {
        fadeColor = imageFade.color;
    }

    // Update is called once per frame
    void Update () {
		if(fadingOut) {
            fadeTimer += Time.deltaTime;
            fadeColor.a = (fadeTimer / Const.fadeDuration) * fadeIntensity;
            imageFade.color = fadeColor;
            if (fadeColor.a >= fadeIntensity) {
                fadingOut = false;
            }
        }
        else if(fadingIn) {
            fadeTimer += Time.deltaTime;
            fadeColor.a = fadeIntensity - (fadeTimer / Const.fadeDuration) * fadeIntensity;
            imageFade.color = fadeColor;
            if (fadeColor.a <= 0.05f)
            {
                fadingIn = false;
                fadeColor.a = 0;
                imageFade.color = fadeColor;
            }
        }
	}
}
