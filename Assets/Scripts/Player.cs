using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : SingletonComponent<Player> {

    public Image selectorRadial;
    public Skybox leftEyeSkybox;
    public Skybox rightEyeSkybox;

    [SerializeField]
    private Texture menuBackroundTextureLeft, menuBackroundTextureRight;

    bool hovering;
    float gazeClickDuration = 1.5f;
    float gazeClickTimePassed = 0f;

    float clickTimeout = 1f;
    float currCickTimeout = 0f;


    public OVRScreenFade rightEyeFade;
    public OVRScreenFade leftEyeFade;

    public void SetSkyboxes(Texture leftEyeTexture, Texture rightEyeTexture, float rotation) {
        leftEyeSkybox.material.SetTexture("_Tex", leftEyeTexture);
        rightEyeSkybox.material.SetTexture("_Tex", rightEyeTexture);

        leftEyeSkybox.material.SetFloat("_Rotation", rotation);
        rightEyeSkybox.material.SetFloat("_Rotation", rotation);
    }

    public void SetMenuSkybox()
    {
        leftEyeSkybox.material.SetTexture("_Tex", menuBackroundTextureLeft);
        rightEyeSkybox.material.SetTexture("_Tex", menuBackroundTextureRight);

        leftEyeSkybox.material.SetFloat("_Rotation", 0);
        rightEyeSkybox.material.SetFloat("_Rotation", 0);
    }

    public void FadeOutIn()
    {
        rightEyeFade.FadeOutFadeInStart();
        leftEyeFade.FadeOutFadeInStart();
    }

    // Use this for initialization
    void Start () {
   //     UnityEngine.VR.VRSettings.renderScale = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
   //     ExecuteEvents.ExecuteHierarchy(currentLookAtHandler, pointerEventData, ExecuteEvents.pointerClickHandler);

        /*  if(currCickTimeout <= 0)
          {
              if(hovering)
              {
                  if(CurvedUI.CUI_ZChangeOnHover.hovering) {
                      gazeClickTimePassed += Time.deltaTime;
                      selectorRadial.fillAmount = gazeClickTimePassed / gazeClickDuration;
                      if(selectorRadial.fillAmount >= 0.99f) {
                       //   CurvedUI.CUI_ZChangeOnHover.currentHoverButton.onClick
                          currCickTimeout = clickTimeout;
                          hovering = false;
                          selectorRadial.fillAmount = 0f;
                          gazeClickTimePassed = 0f;
                      }
                  }else {
                      hovering = false;
                      selectorRadial.fillAmount = 0f;
                      gazeClickTimePassed = 0f;
                  }
              }else  {
                  if (CurvedUI.CUI_ZChangeOnHover.hovering)
                  {
                          hovering = true;
                  }
              }

          }else
          {
              currCickTimeout -= Time.deltaTime;
          }
          */
    }





}
