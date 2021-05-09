using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {

public static GameMgr Instance { get; private set; }

    public double FallersDestroyed=0;


 private void Awake()
    {
        Instance = this;
    }
	// Use this for initialization
}
