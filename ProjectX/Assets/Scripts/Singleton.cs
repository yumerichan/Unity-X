using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);

                if (instance == null)
                {
                    Debug.LogError(t + " Singletoné∏îs");
                }
            }

            return instance;
        }
    }
}