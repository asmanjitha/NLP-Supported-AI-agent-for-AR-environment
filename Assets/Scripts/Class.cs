using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Class 
{
    public string class_name;

    public float confidence;

    public Class(string class_name, float confidence){
        this.class_name = class_name;
        this.confidence = confidence;
    }
}
