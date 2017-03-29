using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChangerDebug : MonoBehaviour {


    [InspectorButton("OnButtonClicked")]
    public bool clickMe;

    private void OnButtonClicked()
    {
        Debug.Log("Clicked!");
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
