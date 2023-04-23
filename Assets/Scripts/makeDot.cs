using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class makeDot : MonoBehaviour
{
    public TextMeshProUGUI  text;
    public GameObject Manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float DotNeeded = Manager.GetComponent<Managing>().numberOfDotsNeeded;
        float Dot = Manager.GetComponent<Managing>().numberOfDots;

        text.text = "DotNeeded " + DotNeeded.ToString("0") + " / Dot "+Dot.ToString("0");
    }

}