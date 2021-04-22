
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomBInitiatorBehaviour : MonoBehaviour
{
    public void GotoRoomB(){
        SceneManager.LoadSceneAsync("RoomB");
    }
}
