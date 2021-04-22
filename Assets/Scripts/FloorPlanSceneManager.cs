using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FloorPlanSceneManager : MonoBehaviour
{
    public TypeWriter kyleChat;
    public Button roomButtonA;
    public Button roomButtonB;

    void Start()
    {
        roomButtonA.interactable = false;
        roomButtonB.interactable = false;
        Invoke("KyleIntroduction1", 3.0f);
    }

    private void KyleIntroduction1(){
        kyleChat.TypeWrite("Hello  I'm Kyle \n I'm your assistant AI");
        Invoke("SetKyleTyping", 5.0f);
        Invoke("KyleIntroduction2", 8.0f);
    }

    private void KyleIntroduction2(){
        kyleChat.TypeWrite("I will guide you to solve the mystery happened recently.");
        Invoke("SetKyleTyping", 6.0f);
        Invoke("KyleAskForSelectRoom", 10.0f);
    }

    private void SetKyleTyping(){
        kyleChat.ShowTypingAnimation();
    }

    private void KyleAskForSelectRoom(){
        kyleChat.TypeWrite("Please seleact a room from the floor plan to enter the room");
        ActivateRoomButtons();
    }

    private void ActivateRoomButtons(){
        roomButtonA.interactable = true;
        roomButtonB.interactable = true;
    }


    public void GotoRoomA(){
        SceneManager.LoadSceneAsync("RoomAInitiator");
    }

    public void GotoRoomB(){
        SceneManager.LoadSceneAsync("RoomBInitiator");
    }
}
