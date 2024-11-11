using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private PaintWall paintWallScript;
    [SerializeField] private Slider brushSizeSlider;
    [SerializeField] private Button redButton;
    [SerializeField] private Button blueButton;
    [SerializeField] private Button yellowButton;
    [SerializeField] private TextMeshProUGUI percentageText;

    private void Start()
    {
        brushSizeSlider.onValueChanged.AddListener(SetBrushSize);
        redButton.onClick.AddListener(() => SetBrushColor(Color.red));
        blueButton.onClick.AddListener(() => SetBrushColor(Color.blue));
        yellowButton.onClick.AddListener(() => SetBrushColor(Color.yellow));

        paintWallScript.PaintedPercentageChanged += UpdatePercentage;
        UpdatePercentage(0);
    }

    public void SetBrushSize(float size)
    {
        paintWallScript.SetBrushSize(size);
    }

    public void SetBrushColor(Color color)
    {
        paintWallScript.SetBrushColor(color);
    }

    public void UpdatePercentage(float percentage)
    {
        percentageText.text = $"{percentage:0}%";
    }
}
