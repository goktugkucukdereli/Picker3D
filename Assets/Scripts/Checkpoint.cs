using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{


    public int requiredCollecteds;
    public int totalCollecteds;
    public TextMeshPro t;
    public bool gotObstacle;
    public bool noMoreCheck;

    public Animator lift;
    public Animator gate;

    void Start()
    {
        totalCollecteds = 0;
    }

    void Update()
    {
        t.text = totalCollecteds + "/" + requiredCollecteds;
    }

    public void MoveOn()
    {
        UIManager.instance.ShowDynamicReward();
        noMoreCheck = true;
        gate.Play("Open");
        lift.Play("LiftUp");
        t.gameObject.SetActive(false);
    }
}
