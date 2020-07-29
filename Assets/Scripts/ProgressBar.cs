using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    private Slider bar;

    public float fillspeed = 0.5f;
    private float targetValue = 0;

    private void Awake()
    {
        bar = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        IncreaseProgress(0.75f);
    }
    private void Update()
    {
        if (bar.value < targetValue)
            bar.value += fillspeed * Time.deltaTime;
    }

    public void IncreaseProgress (float newProgress)
    {
        targetValue = bar.value + newProgress;
    }
}
