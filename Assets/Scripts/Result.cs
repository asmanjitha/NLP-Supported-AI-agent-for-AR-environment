using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Result
{
    public string classifier_id;
    public string url;
    public string text;
    public string top_class;
    public List<Class> classes;

    public Result(string classifier_id, string url, string text, string top_class, List<Class> classes){
        this.classifier_id = classifier_id;
        this.url = url;
        this.text = text;
        this.top_class = top_class;
        this.classes = classes;
    }   
}