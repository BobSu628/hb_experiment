using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class BodyPart{
    public string bodyName="";
    public GameObject[] bodyParts;
    public Button nextBodyPart;
    public Button previosBodyPart;
    public Text bodyNameDisplay;
    public int currentIndex=0;
}
public class Customizations : MonoBehaviour
{
    public List<BodyPart> allParts=new List<BodyPart>();

    void Start(){
        foreach(BodyPart b in allParts){
            foreach(GameObject g in b.bodyParts){
                g.SetActive(false);
            }
            b.bodyParts[b.currentIndex].SetActive(true);
            b.nextBodyPart.onClick.AddListener(()=>{ChangePart(b,true);});
            b.previosBodyPart.onClick.AddListener(()=>{ChangePart(b,false);});
            b.bodyNameDisplay.text=b.bodyParts[b.currentIndex].gameObject.name;
        }
    }
    void ChangePart(BodyPart b, bool increase)
    {
        b.bodyParts[b.currentIndex].SetActive(false);
        b.currentIndex += increase ? 1 : -1;
        if (b.currentIndex > b.bodyParts.Length - 1)
            b.currentIndex = 0;
        if (b.currentIndex < 0)
            b.currentIndex = b.bodyParts.Length - 1;
        b.bodyParts[b.currentIndex].SetActive(true);
        b.bodyNameDisplay.text = b.bodyParts[b.currentIndex].gameObject.name;
    }
}
