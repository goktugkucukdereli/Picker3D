using UnityEngine;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject[] staticLevels;
    [Header("Realtime")]
    public int currentLevel;
    public Level _currentLevel;
    public GameObject currentLevelObject;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnLevel();
    }

    public void RebuildLevel()
    {
        Destroy(currentLevelObject);
        GameManager.instance.RestartLevel();
        UIManager.instance.ChangeStateUI(1);
        SpawnLevel();
    }

    private void SpawnLevel()
    {
        //PlayerPrefs.DeleteAll();
        currentLevel = PlayerPrefs.GetInt("currentLevel", 0);

        try
        {
            currentLevelObject = Instantiate(staticLevels[currentLevel]);
        }
        catch
        {
            currentLevel = PlayerPrefs.GetInt("currentLevel", 0) + 1;
            currentLevelObject = Instantiate(staticLevels[Random.Range(0,staticLevels.Length)]);
        }

        _currentLevel = currentLevelObject.GetComponent<Level>();
        SmoothCameraFollow.instance.SetTarget(Level.instance.picker);
        UIManager.instance.UpdateLevelText();
    }

    public void LevelUp()
    {
        _currentLevel.finishParticle.Play();
        currentLevel++;
        PlayerPrefs.SetInt("currentLevel", currentLevel);
    }
}