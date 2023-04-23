using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managing : MonoBehaviour
{
    Dictionary<int, List<int>> COLOR = new Dictionary<int, List<int>>();
    Camera cam;

    bool setUpNumberOfDots = true;
    public int numberOfDotsNeeded;

    public GameObject DotPrefab;
    GameObject DotObject;
    public GameObject DotCourbePrefab;
    List<GameObject> listOfStructureDot = new List<GameObject>();
    List<GameObject> listOfMovingDot = new List<GameObject>();
    List<GameObject> listOfWorkingDot = new List<GameObject>();
    List<GameObject> preparingListOfWorkingDot = new List<GameObject>();


    List<Vector3> listOfCourbePoint = new List<Vector3>();
    
    public LineRenderer LinePrefab;
    LineRenderer LineObject;

    List<GameObject> listOfLine = new List<GameObject>();

    List<Vector3> listOfCoupleForLine = new List<Vector3>();

    bool first = true;
    [HideInInspector]
    public int numberOfDots = 0;
    [HideInInspector]
    public float t = 0F;
    public float addToT = 0.1F;
    int colorNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        COLOR.Add(1, new List<int>() { 39, 3, 42 });
        COLOR.Add(2, new List<int>() { 75, 8, 61 });
        COLOR.Add(3, new List<int>() { 115, 17, 68 });
        COLOR.Add(4, new List<int>() { 137,	12,	56 });
        COLOR.Add(5, new List<int>() { 171, 10, 42 });
        COLOR.Add(6, new List<int>() { 190, 32, 40 });
        COLOR.Add(7, new List<int>() { 207, 73, 44 });
        COLOR.Add(8, new List<int>() { 227, 100, 51 });
        COLOR.Add(9, new List<int>() { 227, 136, 78 });
        COLOR.Add(10, new List<int>() { 236, 181, 95 });
        COLOR.Add(11, new List<int>() { 238, 214, 123 });
        COLOR.Add(12, new List<int>() { 244, 239, 174 });
        COLOR.Add(13, new List<int>() { 255, 253, 217 });
        COLOR.Add(14, new List<int>() { 251, 251, 42 }); 
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(setUpNumberOfDots)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                numberOfDotsNeeded += 1;

            }
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                numberOfDotsNeeded -= 1;
                
            }
            if(Input.GetKeyDown(KeyCode.Return))
            { 
                setUpNumberOfDots = false;
            }
        }
        if(!setUpNumberOfDots)
        {
            if(listOfStructureDot.Count == numberOfDotsNeeded)
            {
                if(first)
                {
                    MakeTheStructureLine();
                    first = false;
                }
                if(t>1)
                {
                    t = 0;
                    foreach(Vector3 index in listOfCourbePoint)
                    { 
                        DotObject = Instantiate(DotCourbePrefab,index,Quaternion.identity);
                        DotObject.GetComponent<Renderer>().material.color = MakeListIntoColor(COLOR[14]);
                        DotObject.GetComponent<Renderer>().sortingOrder = 17;
                        Destroy(DotObject, 4);


                    }
                }
                t = t + addToT;
                listOfWorkingDot.Clear();
                listOfWorkingDot.AddRange(listOfStructureDot);
                colorNumber = 1;
                while(listOfWorkingDot.Count > 1)
                {
                    
                    for(int index = 0; index < listOfWorkingDot.Count - 1; index++)
                    { 
                        Vector3 positionPoint = Vector3.Lerp(listOfWorkingDot[index].transform.position,listOfWorkingDot[index+1].transform.position, t);
                        DotObject = Instantiate(DotPrefab, positionPoint , Quaternion.identity);
                        DotObject.GetComponent<Renderer>().material.color = MakeListIntoColor(COLOR[colorNumber]);
                        preparingListOfWorkingDot.Add(DotObject);
                        Destroy(DotObject, 0.05F);
                        DotObject.GetComponent<Renderer>().sortingOrder = colorNumber;

                    }
                    colorNumber += 1;
                    listOfWorkingDot.Clear();
                    listOfWorkingDot.AddRange(preparingListOfWorkingDot);
                    if(listOfWorkingDot.Count == 1)
                    {
                        listOfCourbePoint.Add(listOfWorkingDot[0].transform.position);
                    }
                    else 
                    {
                    for(int index = 0; index < listOfWorkingDot.Count - 1; index++ )
                    {
                        LineObject = Instantiate(LinePrefab, new Vector3(0,0,0), Quaternion.identity);
                        LineObject.SetPosition (0,listOfWorkingDot[index].transform.position);
                        LineObject.SetPosition (1, listOfWorkingDot[index + 1].transform.position);
                        LineObject.SetColors( MakeListIntoColor(COLOR[colorNumber]),  MakeListIntoColor(COLOR[colorNumber]));  
                        Destroy(LineObject, 0.05F);
                        LineObject.GetComponent<Renderer>().sortingOrder = colorNumber;
                        

                    }
                    }
                    preparingListOfWorkingDot.Clear();
                }
            }




        else if(Input.GetMouseButtonDown(0))
        {
            DotObject = Instantiate(DotPrefab,  cam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10), Quaternion.identity);
            DotObject.GetComponent<Renderer>().material.color = MakeListIntoColor(COLOR[1]);
            listOfStructureDot.Add(DotObject);
            numberOfDots += 1;
        }

        }
    }






    Color MakeListIntoColor(List<int> intList)
    {
        return(new Color(intList[0] / 255f, intList[1] / 255f, intList[2] / 255f));
    }
    void MakeTheStructureLine()
    {
        for(int index = 0 ; index != listOfStructureDot.Count - 1; index++)
        {
            LineObject = Instantiate(LinePrefab, new Vector3(0,0,0), Quaternion.identity);
            LineObject.SetPosition (0,listOfStructureDot[index].transform.position);
            LineObject.SetPosition (1, listOfStructureDot[index + 1].transform.position);
            LineObject.SetColors( MakeListIntoColor(COLOR[1]),  MakeListIntoColor(COLOR[1]));  
        }
    }

}
