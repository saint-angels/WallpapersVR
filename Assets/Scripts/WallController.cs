using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;


public enum RoomState
{
    UNLOADED,
    OVERVIEW,
    CLOSEUP
};

public enum RoomWallpaperStyle
{
    Classic = 0,
    Modern = 1,
    Kids = 2
};

public enum RoomType
{
    LivingRoom = 0,
    Bedroom = 1,
    KidsRoom = 2
}


public class WallController : ListComponent<WallController>
{

    public RoomState State
    {
        get { return this.state; }
        set
        {
            state = value;
            stylesUI.SetActive(false);
            switchRoomsButton.SetActive(false);
            zoomButton.SetActive(false);
            zoomCanvas.SetActive(false);
            for (int i = 0; i < colorsUIs.Length; i++) colorsUIs[i].gameObject.SetActive(false);
            switch (state)
            {
                case RoomState.UNLOADED:
                    break;
                case RoomState.OVERVIEW:
                    //SetSkyboxes();
                    stylesUI.SetActive(true);
                    colorsUIs[styleIdx].gameObject.SetActive(true);
                    switchRoomsButton.SetActive(true);
                    zoomButton.SetActive(true);
                    break;
                case RoomState.CLOSEUP:
                    zoomCanvas.SetActive(true);
                    break;
            }
        }
    }

    public class LastRoom
    {
        public int roomIdx;
        public int styleIdx;
        public int colorIdx;
    }

    public static LastRoom LastBedroom = new LastRoom();


    [SerializeField]
    private RoomWallpaperStyle wallpaperStyle;
    public RoomWallpaperStyle WallpaperStyle { get { return wallpaperStyle; } private set { wallpaperStyle = value; } }

    [SerializeField]
    private RoomType roomType;
    public RoomType RoomType { get { return roomType; } private set { roomType = value; } }

    [SerializeField]
    private GameObject stylesUI, zoomCanvas, switchRoomsButton, zoomButton;
    [SerializeField]
    private WallpaperColors[] colorsUIs;

    private RoomState state;

    int styleIdx = 0;
    static int colorIdx = 0;

    public int GetCollectionIndexForCollection(CollectionType collection)
    {
        int collectionIndex = 0;
        for (int i = 0; i < colorsUIs.Length; i++)
        {
            if (colorsUIs[i].collection == collection)
            {
                collectionIndex = i;
            }
        }
        return collectionIndex;
    }

    public void PressedSwitchRoomTo(int roomIdx)
    {
        GameController.Instance.PressedSwitchRoomFrom(colorsUIs[styleIdx].collection, roomIdx);
    }


    public void PressedSwitchWallpaperStyle(int wStyleIdx)
    {
        if (GameController.CanMakeAction())
        {
            GameController.currActionCooldown = GameController.actionDuration;
            StartCoroutine(SwitchWallpaperStyle(wStyleIdx, true));
        }
    }

    public void PressedSwitchWallpaper(int wIdx)
    {
        if (GameController.CanMakeAction())
        {
            GameController.currActionCooldown = GameController.actionDuration;
            StartCoroutine(SwitchWallpaper(wIdx));
        }
    }

    public void PressedZoomCurrentWallPaper()
    {
        if (GameController.CanMakeAction())
        {
            GameController.currActionCooldown = GameController.actionDuration;
            StartCoroutine(ZoomCurrentWallPaper());
        }
    }

    public void PressedUnzoomCurrentWallpaper()
    {
        if (GameController.CanMakeAction())
        {
            GameController.currActionCooldown = GameController.actionDuration;
            StartCoroutine(UnZoomCurrentWallpaper());
        }
    }

    public void SetSkyboxes(int roomIdx = -1)
    {
        if (roomType != RoomType.KidsRoom)
        {
            if (roomIdx != -1)
                LastBedroom.roomIdx = roomIdx;
            LastBedroom.styleIdx = styleIdx;
            LastBedroom.colorIdx = colorIdx;
        }
        colorsUIs[styleIdx].SetSelected(colorIdx);
        Player.Instance.SetSkyboxes(colorsUIs[styleIdx].lTextures[colorIdx], colorsUIs[styleIdx].rTextures[colorIdx], colorsUIs[styleIdx].cubemapRotation);
        ChangeRoomSwitchSprites(styleIdx);
    }

    // Use this for initialization
    void Start()
    {
        State = RoomState.UNLOADED;
    }



    // Update is called once per frame
    void Update()
    {




        if (Input.GetKeyDown(KeyCode.Z)) PressedZoomCurrentWallPaper();
        else if (Input.GetKeyDown(KeyCode.C)) PressedUnzoomCurrentWallpaper();

        /*if(Input.GetKeyDown(KeyCode.RightArrow)) {
			colorIdx = ++colorIdx % leftEyeTextures.Length; // Get The next texture idx
            PressedSwitchWallpaper(colorIdx);
		}else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			if(--colorIdx == -1) colorIdx = leftEyeTextures.Length - 1; // Get The next texture idx
			PressedSwitchWallpaper(colorIdx);
		}*/
    }

    public IEnumerator SwitchWallpaperStyle(int newStyleIdx, bool fade, bool preserveColor = false, int roomIdx = -1)
    {
        Debug.Log(preserveColor);
        if (fade)
        {
            Player.Instance.FadeOutIn();
            yield return new WaitForSeconds(Const.fadeDuration);
        }
        colorsUIs[styleIdx].gameObject.SetActive(false);
        styleIdx = newStyleIdx;
        if (!preserveColor)
            colorIdx = 0;
        colorsUIs[styleIdx].gameObject.SetActive(true);
        SetSkyboxes(roomIdx);
    }

    IEnumerator SwitchWallpaper(int wIdx)
    {
        colorIdx = wIdx;
        Player.Instance.FadeOutIn();
        yield return new WaitForSeconds(Const.fadeDuration);
        SetSkyboxes();
    }

    IEnumerator ZoomCurrentWallPaper()
    {
        //Player.Instance.FadeOutIn();
        BackgroundFader.Instance.StartFadeOut();
        yield return new WaitForSeconds(Const.fadeDuration);
        GameController.Instance.SetTitleAndDescription(colorsUIs[styleIdx].collection, colorsUIs[styleIdx].wallpaperSprites[colorIdx]);

        State = RoomState.CLOSEUP;
    }


    IEnumerator UnZoomCurrentWallpaper()
    {
        // Player.Instance.FadeOutIn();
        BackgroundFader.Instance.StartFadeIn();
        yield return new WaitForSeconds(Const.fadeDuration);
        State = RoomState.OVERVIEW;
    }

    public void ChangeRoomSwitchSprites(int styleIdx)
    {
        int lastRoomIdx = LastBedroom.roomIdx;
        if (lastRoomIdx == 0 || lastRoomIdx == 2)
            lastRoomIdx += 1;
        Debug.Log(lastRoomIdx + " " + LastBedroom.styleIdx + " " + LastBedroom.colorIdx);
        if (roomType == RoomType.KidsRoom)
        {
            switchRoomsButton.GetComponent<RoomSwitchButtonsController>().
                SetButtonSprites(GameController.Rooms[lastRoomIdx].colorsUIs[LastBedroom.styleIdx].previewSprites[LastBedroom.colorIdx],
                                 GameController.Rooms[lastRoomIdx - 1].colorsUIs[LastBedroom.styleIdx].previewSprites[LastBedroom.colorIdx]);
            switchRoomsButton.GetComponent<RoomSwitchButtonsController>().ChangeButtonEvents((lastRoomIdx-1) * 10 + LastBedroom.styleIdx, lastRoomIdx * 10 + LastBedroom.styleIdx);
        }
        else
            switchRoomsButton.GetComponent<RoomSwitchButtonsController>().SetMainButtonSprite(colorsUIs[styleIdx].previewSprites[colorIdx]);
    }
}