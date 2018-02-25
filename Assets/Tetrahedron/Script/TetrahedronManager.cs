using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class TetrahedronManager : MonoBehaviour {

    [SerializeField] private GameObject TubePrefab;
    [SerializeField] private GameObject LegPrefab;
    [SerializeField] private Transform BallPrefab;


    private Vector3[] BallO = new Vector3[1];
    private Vector3[] BallA = new Vector3[1];
    private Vector3[] BallB = new Vector3[1];
    private Vector3[] BallC = new Vector3[1];
   
    private Vector3[] PointCloud = new Vector3[4];

    private Transform[] BallsList = new Transform[4];
    //private Transform[,] BallsList2d = new Transform[6, 2];
    GameObject[] _Tubes = new GameObject[6];

    private GameObject[] _Leg = new GameObject[6];

    private GameObject[] _stng = new GameObject[6];

    [SerializeField] private Slider StretchSlider;
    Transform Slider;

    private void Awake()
    {
        SetPoints();
    }

    // Use this for initialization
    void Start()
    {
        SetBalls();
        SetTubes();
        //SetLegs();
    }

    // Update is called once per frame
    void Update()
    {
       // UpdateLegs();

    }

    void SetPoints()
    {
        
        BallO[0] = PointCloud[0] = new Vector3(0,54,0);
        BallA[0] = PointCloud[1] = new Vector3(-30,6,-17.32f);
        BallB[0] = PointCloud[2] = new Vector3(30,6,-17.32f);
        BallC[0] = PointCloud[3] = new Vector3(0,6,34.64f);
    }


    void SetBalls()
    {
        for (int i = 0; i < PointCloud.Length; i++)
        {
            Transform BA = Instantiate(BallPrefab, transform);
            BA.position = PointCloud[i];
            BA.GetComponent<Balls>().SetIndex(i);
            BallsList[i] = BA;

        }

       
    }

    void SetTubes()
    {
        int index = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                Transform v0;
                Transform v1;
                int to = 0;
               
                if(i+j>4)
                {
                    continue;
                }
                if(i+j<=4)
                {
                    if (i + j == 4 && i == 2 && j == 2)
                    {
                        continue;
                    }


                    GameObject Tube = Instantiate(TubePrefab, transform);
                    v0 = BallsList[i];


                   

                    if(i+j<4)
                    {

                        to = i + j;
                    }   

                    if(i+j==4&&i==3&&j==1)
                    {

                        to = 0;
                    }

                    v1 = BallsList[to];

                   
                    Tube.GetComponent<Tubes>().SetupTube(v0, v1, 5, index);   
                    _Tubes[index] = Tube;
                   
                        index++;

                    print(index); 
                }
               
            }

        }
    }

    //void SetSphere()
    //{
        
    //}

    void updataCube()
    {
        foreach (var s in _stng)
        {
            s.GetComponent<Tubes>().UpdateCubePosition();

        }
    }

    public void ChangeStretchSliderTransform(float _L)
    {
        foreach (var s in _stng)
        {
            s.GetComponent<Tubes>().ChangeSliderValue( _L );
        }
    }

    public void ChangeSliderValue(float StretchLength)
    {
        foreach (GameObject tb in _Tubes )
        {
            tb.GetComponent<Tubes>().ChangeSliderValue(StretchLength) ; 
        }

    }


    public IEnumerable<GameObject> SourceString()
    {
        return _stng;
    }

}
