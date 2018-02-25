using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Slider StretchChanger;

    [SerializeField] private Text LengthText;

    private List<GameObject> SourcePos;

     float Defualt = 6.0f;

    private void Awake()
    {
        
    }


    // Use this for initialization
	void Start ()
	{
	    StretchChanger.value = Defualt;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    LengthText.text = StretchChanger.value.ToString();
	}

    public float SliderValue()
    {
        return StretchChanger.value;
    }
}
