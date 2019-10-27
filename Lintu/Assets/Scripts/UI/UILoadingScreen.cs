using UnityEngine.UI;

public class UILoadingScreen : MonoBehaviourSingleton<UILoadingScreen>
{
    public Slider LoadingSlider;

    public override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    public void SetVisible(bool show)
    {
        gameObject.SetActive(show);
    }

    public void Update()
    {
        int loadingVal = (int) (LoaderManager.Instance.LoadingProgress * 100);
        LoadingSlider.value = loadingVal;
        if (LoaderManager.Instance.LoadingProgress >= 1)
            SetVisible(false);
    }
}