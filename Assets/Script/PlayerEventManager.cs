using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[DefaultExecutionOrder(-9999)]
public class PlayerEventManager : MonoBehaviour
{
    protected static PlayerEventManager _instance;
    public static PlayerEventManager Instance {
        get {
            if (_instance == null)
            {
                Debug.LogWarning("PlayerEventManager is null");
            }
            return _instance;
        }
        private set { _instance = value; }
    }

    protected Dictionary<string, PlayerInteruption> interuptions = new();
    
    public UnityEvent OnInteruptionChanged = new();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else  if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetInteruption(string key, PlayerInteruption interuption)
    {
        if (!interuptions.ContainsKey(key))
        {
            interuptions.Add(key, interuption);
        }
        else
        {
            interuptions[key] = interuption;
        }
        OnInteruptionChanged.Invoke();
    }

    public bool CheckCanMove()
    {
        return interuptions.Values.Where(x => !x.enableMovement).Count() == 0;
    }
}

public class PlayerInteruption
{
    public bool enableMovement = true;
}
