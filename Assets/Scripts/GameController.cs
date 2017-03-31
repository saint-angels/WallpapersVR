using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    MENU,
    INROOM
};


public class GameController : SingletonComponent<GameController> {


    public GameState State
    {
        get { return this.state; }
        set
        {
            state = value;
            switch (state)
            {
                case GameState.MENU:
                    rooms[currRoomIdx].State = RoomState.UNLOADED;
                    Player.Instance.SetMenuSkybox();
                    mainMenuCanvas.SetActive(true);
                    break;
                case GameState.INROOM:
                    mainMenuCanvas.SetActive(false);
                    break;
            }
        }
    }

    [SerializeField]
    private WallController[] rooms;
    [SerializeField]
    private GameObject mainMenuCanvas;
    [SerializeField]
    Text zoomTitle, zoomDescription;
    [SerializeField]
    Image zoomImage;

    public TMPro.TMP_FontAsset font;

    private int currRoomIdx = 0;
    private GameState state;

    public static float actionDuration = Const.fadeDuration * 2.3f;
    public static float currActionCooldown = 0;

    static GameController instance;
    public static WallController[] Rooms { get { return instance.rooms; } private set { } }

    public void PressedUnzoom() {
        rooms[currRoomIdx].PressedUnzoomCurrentWallpaper();
    }

    public void SetTitleAndDescription(CollectionType collection, Sprite sprite) {
        zoomTitle.text = Const.TitleForCollection(collection);        
        zoomDescription.text = Const.DescriptionForCollection(collection);

        float aspectRaio = sprite.bounds.size.x / sprite.bounds.size.y;
        zoomImage.GetComponent<AspectRatioFitter>().aspectRatio = aspectRaio;
        zoomImage.sprite = sprite;
    }

    public static bool CanMakeAction()
    {
        if (currActionCooldown > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public void MainMenuPressedSwitchRoomTo(int roomIdx)
    {
        BackgroundFader.Instance.Reset();
        SwitchRoom(roomIdx);
    }

    public void PressedSwitchRoomTo(int roomIdx)
    {
        SwitchRoom(roomIdx, true);
    }

    public void SwitchRoom(int roomIdx, bool preserve = false)
    {
        if (CanMakeAction())
        {
            currActionCooldown = actionDuration;
            int styleIdx = roomIdx % 10;
            roomIdx = roomIdx / 10;
            StartCoroutine(SwitchRoomTo(roomIdx, styleIdx, preserve));
        }
    }

    public void PressedSwitchRoomFrom(CollectionType sourceCollection, int roomIdx) {
        if (CanMakeAction()){
            currActionCooldown = actionDuration;
            roomIdx = roomIdx / 10;
            int styleIdx = rooms[roomIdx].GetCollectionIndexForCollection(sourceCollection);
            StartCoroutine(SwitchRoomTo(roomIdx, styleIdx, true));
        }
    }


	// Use this for initialization
	void Start () {
        instance = this;
      //  UnityEngine.VR.VRSettings.renderScale = 2f;
        State = GameState.MENU;
     //   rooms[currRoomIdx].State = RoomState.OVERVIEW;
    }

    int userNotPresentStamp = System.DateTime.Now.Second;

	void Update () {

        if (currActionCooldown > 0) {
            currActionCooldown -= Time.deltaTime;
        }



        if (Input.GetKeyDown(KeyCode.A))
        {
            int nextRoomIdx = currRoomIdx - 1;
            if (nextRoomIdx == -1) nextRoomIdx = rooms.Length - 1; // Get The next texture idx
            PressedSwitchRoomTo(nextRoomIdx);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            int nextRoomIdx = (currRoomIdx + 1) % rooms.Length; // Get The next room idx
            PressedSwitchRoomTo(nextRoomIdx);

        }
        if (Input.GetMouseButtonDown(1)) {//Pressed back button
           /* if (rooms[currRoomIdx].State == RoomState.CLOSEUP) {
                rooms[currRoomIdx].PressedUnzoomCurrentWallpaper();
            }else {
            */
                StartCoroutine(SwitchToMenu());
            //}
        }
#if !UNITY_EDITOR
        if (!OVRPlugin.userPresent)
        {
            if (System.DateTime.Now.Second - userNotPresentStamp >= 30)
            {
                userNotPresentStamp = System.DateTime.Now.Second;
                SwitchToMenuNoFade();
            }
        }
        else
            userNotPresentStamp = System.DateTime.Now.Second;
#endif
    }
    IEnumerator SwitchRoomTo(int roomIdx, int styleIdx, bool preserve = false) {
        Player.Instance.FadeOutIn();
        yield return new WaitForSeconds(Const.fadeDuration);
        State = GameState.INROOM;
        rooms[currRoomIdx].State = RoomState.UNLOADED;
        currRoomIdx = roomIdx;
        rooms[currRoomIdx].State = RoomState.OVERVIEW;
        rooms[currRoomIdx].StartCoroutine(rooms[currRoomIdx].SwitchWallpaperStyle(styleIdx, false, preserve, roomIdx));
    }

    IEnumerator SwitchToMenu()
    {
        Player.Instance.FadeOutIn();
        yield return new WaitForSeconds(Const.fadeDuration);
        State = GameState.MENU;
    }

    void SwitchToMenuNoFade()
    {
        //Player.Instance.FadeOutIn();
        //yield return new WaitForSeconds(Const.fadeDuration);
        State = GameState.MENU;
    }
}