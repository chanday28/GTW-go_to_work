using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider bar;
    public Gradient gradient;
    public Image fill;


    public void SetMaxGoal(int progress)
    {
        bar.maxValue = progress;

        fill.color = gradient.Evaluate(1f);
    }


    public void SetProgress(int progress)
    {
        bar.value = progress;

        fill.color = gradient.Evaluate(bar.normalizedValue);
    }
}
