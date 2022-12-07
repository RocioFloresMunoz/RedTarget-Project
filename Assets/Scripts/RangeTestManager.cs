using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RangeTestManager : MonoBehaviour
{
    [SerializeField] private int _points;
    [SerializeField] private TextMeshProUGUI _textPoints;

    // Update is called once per frame
    void Update()
    {
        _textPoints.text = _points.ToString();
    }

    public void SetPoints(int value)
    {
        _points = value;
    }
}
