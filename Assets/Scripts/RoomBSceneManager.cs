using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomBSceneManager : MonoBehaviour
{
    public TypeWriter kyleChatTypeWriter;
    public GameObject robotKyle;
    public GameObject arCamera;
    public KyleChat kyle;
    public TextClassifier classifier;
    private bool reload = false;
    public GameObject reloadPanel;
    public GameObject kylesChatBox;
    public GameObject playersChatBox;
    public GameObject maskingPanel;
    public InputField playerInputField;
    public Result result;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("KyleAskToPlaceHimOnTheFoor", 2.0f);
        Invoke("CheckTextClassifier", 7.5f);
    }


    public void CheckTextClassifier(){
        if(!classifier.serviceInitiated){
            reload = true;
            reloadPanel.SetActive(true);
        }
    }

    public void AfterContentPlaced(){
        Invoke("KyleAskTOTypeQuestion", 2.0f);
    }

    private void KyleAskTOTypeQuestion(){
        kyleChatTypeWriter.TypeWrite("Please type your question here, If there is any clue related to your question I can give you the anser.");
        Invoke("ActivateInputField", 8.0f);
    }

    public void ActivateInputField(){
        kylesChatBox.SetActive(false);
        playersChatBox.SetActive(true);

    }

    private void KyleAskToPlaceHimOnTheFoor(){
        kyleChatTypeWriter.TypeWrite("Place me on the floor, then I can take a look at this room. ");
        Invoke("RemoveMaskPanel", 5.0f);
    }

    private void RemoveMaskPanel(){
        maskingPanel.SetActive(false);
    }


    public void ClassifyText(string str){
        classifier.RunClassification(str);
    }

    public void ClassifyText(){
        classifier.RunClassification();
    }

    public void ReloadScene(){
        SceneManager.LoadSceneAsync("RoomB");
    }

    public void SendTextToClassifier(){
        string text = playerInputField.text;
        ClassifyText(text);
    }

    public void ProcessResult(){
        string kylyResponse;
        if(result != null){
            if(ValidResult(result.classes)){
                kylyResponse = GetClue(result.top_class);
            }else{
                kylyResponse = "I don't have any clue related to your question, Please ask another question";
            }
            Debug.Log(kylyResponse);
            kyle.TypeWrite(kylyResponse);
        }
    }

    private bool ValidResult(List<Class> classes){
        foreach(Class cls in classes){
            if(cls.confidence > 0.9){
                return true;
            }
        }
        return false;
    }

    private string GetClue(string topClass){
        switch(topClass)
        {
            case "fingerprint":
                return "There are fingerprints on the door handle and the table";
                break;
            case "footprint":
                return "There are several footprints around the room";
                break;
            case "knife":
                return "There is a knife beside the body";
                break;
            default:
                return "I don't have any clue related to your question, Please ask another question";
                break;
        }
    }

    public void FixRobotLookingDirection(){
        Vector3 targetPosition = new Vector3(arCamera.transform.position.x, robotKyle.transform.position.y, arCamera.transform.position.z);
        robotKyle.transform.LookAt(targetPosition);
        // var lookPos = arCamera.transform.position - robotKyle.transform.position;
        // lookPos.y = 0;
        // var rotation = Quaternion.LookRotation(lookPos);
        // robotKyle.transform.rotation = Quaternion.RotateTowards(robotKyle.transform.rotation, rotation, Time.deltaTime * 0.5f);
    }

    public void ExitFromAR(){
        SceneManager.LoadSceneAsync("FloorPlan");
    }
}
