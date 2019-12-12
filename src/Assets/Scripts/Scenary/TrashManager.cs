using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager: MonoBehaviour
{
    private static bool started = false;
    private static bool[] garbageState = { };

    public static void reset()
    {
        started = false;
    }

    void Start()
    {
        if (!started)
        {
            garbageState = new bool[transform.childCount];
            started = true;
        }
        foreach (Transform child in transform)
        {
            //Debug.Log(child.GetSiblingIndex());
            if (garbageState[child.GetSiblingIndex()])
                child.gameObject.SetActive(false);
        }

    }

    public static void collectGarbage(int index)
    {
        garbageState[index] = true;
    }

}
