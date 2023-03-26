using UnityEngine;

public class Page : MonoBehaviour, IPage
{
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}