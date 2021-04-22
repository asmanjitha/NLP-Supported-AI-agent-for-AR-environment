using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomAInitiatorBehaviour : MonoBehaviour
{
    public void GotoRoomA(){
        SceneManager.LoadSceneAsync("RoomA");
    }
}
