using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Tubes : MonoBehaviour {


    [SerializeField] Transform LegPrefab;
    [SerializeField] Transform CubePrefab;
    private Vector3[] VerticesInTube = new Vector3[2];
    Transform[] _BallsInTube = new Transform[2];

    private GameObject LengthController;


    CharacterJoint[] HJ = new CharacterJoint[2];
    ConfigurableJoint[] CJ = new ConfigurableJoint[1];
    SpringJoint[] SJ = new SpringJoint[2];
    FixedJoint[] FJ = new FixedJoint[3];

    //private GameObject LengthController;


    [SerializeField] private GameObject _CanvasPrefab;
    Transform StretchSlider;
    private GameObject _Canvas;

    Transform Leg;
    Transform Slider;
    //float Length;

    private float StretchLength;
    private int index;

    private GameObject[] _stng = new GameObject[6];

    //private float L;



    public void SetupTube(Transform B0, Transform B1, float Thickness, int _index)
    {
        VerticesInTube[0] = B0.position;
        VerticesInTube[1] = B1.position;
       
        _BallsInTube[0] = B0;
        _BallsInTube[1] = B1;

        Leg = Instantiate(LegPrefab,transform.parent);
        Slider = Instantiate(CubePrefab, transform.parent);
        var SliderForm = Slider.GetComponent<Transform>();
        _Canvas = Instantiate(_CanvasPrefab, SliderForm);

        // LengthController
        //LengthController = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //LengthController.GetComponent<MeshRenderer>().material.color = Color.black;
        //var ControllerForm = LengthController.GetComponent<Transform>();
        //_Canvas = Instantiate(_CanvasPrefab, ControllerForm);
        //var CvsForm = _Canvas.GetComponent<RectTransform>();
        //LengthController.GetComponent<Rigidbody>();
        //FJ[0] = LengthController.AddComponent<FixedJoint>();
        //FJ[0].connectedBody = _Leg[0];
        //LengthController.GetComponent<BoxCollider>().enabled = false;
        //LengthController.GetComponent<Rigidbody>().useGravity = false;
        //LengthController.GetComponent<Transform>().localScale = new Vector3(3.5f, 3.5f, 3.5f);

        var T = gameObject.GetComponent<Transform>();
        var d = VerticesInTube[1] - VerticesInTube[0];
        var TubeLength = gameObject.transform.localScale.y;
        var L = d.magnitude;
       
        T.localScale = new Vector3(Thickness, 0.41f*L , Thickness);
        T.position =  (VerticesInTube[0] + VerticesInTube[1])*0.5f;
        T.localRotation = Quaternion.FromToRotation(T.up, d);

        Leg.localScale = new Vector3(0.6f * Thickness, 0.38f*(5f/6f*L), 0.6f * Thickness);
        Leg.localRotation = T.localRotation;
        Leg.localPosition  =  T.localPosition;
        Leg.localPosition =  Leg.localPosition + d * 1f / 7f;//(L - 5f / 6f * L) / L;

        Slider.localScale = new Vector3(6, 6, 6);
        Slider.localRotation = T.localRotation;
        Slider.position = T.position;
        Slider.localPosition = T.localPosition;

        FJ[0] = Slider.gameObject.AddComponent<FixedJoint>();
        FJ[0].connectedBody = T.GetComponent<Rigidbody>();
        Slider.GetComponent<BoxCollider>().enabled = false;
        Slider.GetComponent<Rigidbody>().useGravity = false;

        FJ[1] = T.gameObject.AddComponent<FixedJoint>();
        FJ[1].connectedBody = B0.GetComponent<Rigidbody>();

        FJ[2] = Leg.gameObject.AddComponent<FixedJoint>();
        FJ[2].connectedBody = B1.GetComponent<Rigidbody>();

        //HJ[0] = T.gameObject.AddComponent<CharacterJoint>();
        //HJ[0].connectedBody = B0.GetComponent<Rigidbody>();
        //HJ[0].autoConfigureConnectedAnchor = false;
        //HJ[0].anchor = new Vector3(0, -1.05f, 0);
        //HJ[0].connectedAnchor = new Vector3(0, 0, 0);
        //HJ[0].axis = new Vector3(0, 1, 0);
        //HJ[0].swingAxis = new Vector3(0, 1, 0);

        //HJ[1] = Leg.gameObject.AddComponent<CharacterJoint>();
        //HJ[1].connectedBody = B1.GetComponent<Rigidbody>();
        //HJ[1].autoConfigureConnectedAnchor = false;
        //HJ[1].anchor = new Vector3(0, 1.05f, 0);
        //HJ[1].connectedAnchor = new Vector3(0, 0, 0);
        //HJ[1].axis = new Vector3(0, 1, 0);
        //HJ[1].swingAxis = new Vector3(0, 1, 0);

        CJ[0] = T.gameObject.AddComponent<ConfigurableJoint>();
        CJ[0].connectedBody = Leg.GetComponent<Rigidbody>();
        CJ[0].autoConfigureConnectedAnchor = false;
        CJ[0].anchor = new Vector3(0, 0, 0);
        CJ[0].connectedAnchor = new Vector3(0, 0.1f, 0);
        CJ[0].axis = new Vector3(1, 0, 1);
        CJ[0].secondaryAxis = new Vector3(0, 0, 0);
        CJ[0].xMotion = ConfigurableJointMotion.Locked;
        CJ[0].yMotion = ConfigurableJointMotion.Free;
        CJ[0].zMotion = ConfigurableJointMotion.Locked;
        CJ[0].angularXMotion = ConfigurableJointMotion.Locked;
        CJ[0].angularYMotion = ConfigurableJointMotion.Limited;
        CJ[0].angularZMotion = ConfigurableJointMotion.Locked;
        CJ[0].linearLimitSpring.spring.CompareTo(10);
        CJ[0].linearLimit.limit.Equals(T);
        CJ[0].yDrive.positionSpring.Equals(T);

        index = _index;

        StretchSlider = _Canvas.GetComponent<RectTransform>().GetChild(0);
        //StretchLength = 0.2f * L;
        //StretchSlider.GetComponent<Slider>().value = StretchLength;
        //var CvsForm = _Canvas.GetComponent<RectTransform>();

    }



   
    //public float GetLength()
    //{
    //    return Length;
    //}

    public int GetIndex()
    {
        return index;
    }

    public Vector3 GetBall(int _index)
    {
        return VerticesInTube[_index];
    }

    public Transform GetTransform(int _index)
    {
        return _BallsInTube[_index];
    }

    public void ChangeSliderValue(float StretchLength)
    {
        //Vector3 position = Slider.transform.localPosition;

        //position.y = StretchLength;

        //Slider.transform.position = position;

        ////StretchLength = 0.3f * L;
        StretchSlider.GetComponent<Slider>().value = StretchLength;
    }




    public void UpdateCubePosition()
    {
        
        var d = VerticesInTube[1] - VerticesInTube[0];


       var L= StretchSlider.GetComponent<Slider>().value;

        var T = gameObject.GetComponent<Transform>();
        T.position = (VerticesInTube[0] + VerticesInTube[1]) * 0.5f;
        T.localRotation = Quaternion.FromToRotation(T.up, d);
        Slider.localRotation = T.localRotation;

        Slider.localPosition = new Vector3(T.localPosition.x, L, T.localPosition.z);


        //Slider.localPosition = T.position;


    }

    void UpdateTubePosition()
    {
        var T = gameObject.GetComponent<Transform>();

        var d = _BallsInTube[1].transform.position - _BallsInTube[0].transform.position;
        var L = d.magnitude;
        var _L = T.localScale.y;
        var _L1 = Leg.localScale.y;

        Vector3 FWD = Vector3.Cross(Vector3.down, d); 

        T.localRotation = Quaternion.LookRotation (FWD, d);
        //T.position =((_BallsInTube[0].transform.position + _BallsInTube[1].transform.position)*0.5f+_BallsInTube[0].transform.position)*0.5f;
        T.position = (_BallsInTube[0].transform.position + _BallsInTube[1].transform.position) * 0.5f;
        Leg.localRotation = T.localRotation;
        Leg.localPosition = Leg.localPosition + (1f / 7f) * ((_BallsInTube[0].transform.position + _BallsInTube[1].transform.position) * 0.5f);
       // Leg.position =(_BallsInTube[0].transform.position - _BallsInTube[1].transform.position) * (_L1 / L);
    }


    private void Update()
    {
        //UpdateTubePosition(); 
        UpdateCubePosition();


    }

    public Transform GetStrechSlider()
    {
        
        return StretchSlider;
    }
}

