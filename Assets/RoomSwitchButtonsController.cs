using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSwitchButtonsController : MonoBehaviour {

    [SerializeField]
    private Image buttonTargetGraphics;
    [SerializeField]
    private Button mainButton;

    public void SetButtonSprites(Sprite mainButtonSprite)
    {
        buttonTargetGraphics.sprite = mainButtonSprite;
    }
}
