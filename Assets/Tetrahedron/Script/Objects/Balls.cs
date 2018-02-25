using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    private int index;
    private int TubeIndex;
    //private int StartEndIndex;

    public void SetIndex(int _index)
    {
        index = _index;
    }

    //public void SetIndex2d(int _Bi, int _Si)
    //{
    //    TubeIndex = -_Bi;
    //    StartEndIndex = _Si;
    //}

    public int GetIndex()
    {
        return index;
    }

    public int GetTubesIndex()
    {
        return TubeIndex;
    }

    //public int GetStartEndIndex()
    //{
    //    return StartEndIndex;
    //}
}
