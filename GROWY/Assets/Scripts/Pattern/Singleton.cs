using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("DONT DESTROY")] 
    public bool dontDestroy;

    void Awake()
    {
        if (dontDestroy)
            DontDestroyOnLoad(this.gameObject);
    }

    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = GameObject.Find(typeof(T).Name);

                if (obj == null)
                {
                    if (Debug.isDebugBuild)
                    {
                        Debug.LogFormat("Create New Singleton Instance : {0}", typeof(T).Name);
                    }

                    obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
                else
                {
                    instance = obj.GetComponent<T>();
                }
            }

            return instance;
        }
    }

    public static void DestroyInstance()
    {
        if (Instance == null)
        {
            return;
        }

        // 즉시 제거가 안되어서 제거될 오브젝트가 재참조 되는 현상 존재
        DestroyImmediate(Instance.gameObject);
    }

    public virtual void OnDestroy()
    {
        instance = null;
    }
}
