using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.OpenUI<Level>();
    }

}
