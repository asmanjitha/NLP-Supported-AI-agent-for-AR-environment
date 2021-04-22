using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LauncherManager : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadSceneAsync("FloorPlan");
    }
}
