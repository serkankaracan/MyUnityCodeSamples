using UnityEngine;
using UnityEngine.UI;

public abstract class SliderMain : MonoBehaviour, ISliderMain
{
    public Slider targetSlider;
    public Image Icon;
    public Sprite onIcon;
    public Sprite offIcon;
    float _sValue;

    private float TargetSliderValue
    {
        get { return _sValue; }
        set
        {
            _sValue = value;
            Icon.sprite = (targetSlider.value != 0) ? onIcon : offIcon;
        }
    }

    public void Initialize()
    {
        TargetSliderValue = targetSlider.value;
        targetSlider.onValueChanged.AddListener(OnTargetSliderValueChanged);
    }

    public void OnTargetSliderValueChanged(float sValue)
    {
        TargetSliderValue = sValue;
        OnSliderValue(TargetSliderValue);
    }

    void Start()
    {
        Initialize();
    }

    public abstract void OnSliderValue(float sliderValue);
}
