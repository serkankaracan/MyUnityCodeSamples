using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ToggleMain : MonoBehaviour, IToggleMain
{
    public Toggle targetToggle;
    public Image Background;
    public Image Handle;
    public Sprite onHandle;
    public Sprite offHandle;
    public Sprite onBackground;
    public Sprite offBackground;
    bool _mValue;

    public bool TargetToggleValue
    {
        get { return _mValue; }
        private set
        {
            _mValue = value;
            Handle.sprite = value ? onHandle : offHandle;
            Background.sprite = value ? onBackground : offBackground;
        }
    }

    public void Initialize()
    {
        targetToggle.graphic = null;
        TargetToggleValue = targetToggle.isOn;
        targetToggle.onValueChanged.AddListener(OnTargetToggleValueChanged);
    }

    public void OnTargetToggleValueChanged(bool OnOffValue)
    {
        if (EventSystem.current.currentSelectedGameObject != null)
            if (targetToggle.gameObject == EventSystem.current.currentSelectedGameObject)
                TargetToggleValue = OnOffValue;

        OnToggleValueChanged(TargetToggleValue);
    }

    private void Start()
    {
        Initialize();
    }

    public abstract void OnToggleValueChanged(bool newValue);
}
