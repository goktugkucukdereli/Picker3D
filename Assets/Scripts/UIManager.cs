using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Page[] statePages;
    public Slider levelSlider;

    public Text currentLevelText;
    public Text nextLevelText;

    public Animation dynamicReward;
    public string[] rewardStrings;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeStateUI((int)GameManager.instance.state);
    }

    private void Update()
    {
        if (GameManager.instance.state != GameState.Playing) return;
        UpdateLevelSlider();
    }

    public void ChangeStateUI(int s)
    {
        for (int i = 0; i < statePages.Length; i++)
        {
            if (i != s) statePages[i].Hide();
            else statePages[i].Show();
        }
    }

    public void ShowDynamicReward()
    {
        dynamicReward.Play("DynamicReward");
        dynamicReward.GetComponent<TextMeshProUGUI>().text = rewardStrings[Random.Range(0, rewardStrings.Length)];
    }

    public void UpdateLevelSlider()
    {
        levelSlider.value = Mathf.Lerp(levelSlider.value, Level.instance.picker.position.z / (Level.instance.finishParticle.transform.position.z-5), Time.deltaTime * 10);
    }

    public void NextLevelButton()
    {
        LevelManager.instance.RebuildLevel();
    }

    public void PlayButton()
    {
        GameManager.instance.SetState(1);
        ChangeStateUI((int)GameManager.instance.state);
        SmoothCameraFollow.instance.SetTarget(Level.instance.picker);
    }

    public void SetupLevelBar(float totalProgress)
    {
        levelSlider.value = 0;
    }

    public void Restart()
    {
        LevelManager.instance.RebuildLevel();
    }

    public void UpdateLevelText()
    {
        currentLevelText.text = (LevelManager.instance.currentLevel + 1).ToString();
        nextLevelText.text = (LevelManager.instance.currentLevel + 2).ToString();
    }
}