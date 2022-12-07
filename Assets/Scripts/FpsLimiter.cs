using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLimiter : MonoBehaviour
{
    [SerializeField][Range(30,60)] private int _fps = 60;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = _fps;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
