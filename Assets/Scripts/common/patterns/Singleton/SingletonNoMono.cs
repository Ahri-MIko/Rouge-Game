using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonNoMono<T> where T:class,new()
{
    private static T _instance;
    private static object _lock = new object(); 

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                lock(_lock)
                {
                    _instance = new T();
                }
            }
            return _instance;
        }
    }
       


    
    
}
