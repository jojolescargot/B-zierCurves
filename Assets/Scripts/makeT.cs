using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class makeT : MonoBehaviour
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
        float t = Manager.GetComponent<Managing>().t;
        text.text = "t = " + t.ToString("0.00");
    }

}
