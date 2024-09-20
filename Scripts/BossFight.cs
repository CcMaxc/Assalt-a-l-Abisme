using UnityEngine;

public class BossFight : MonoBehaviour
{
    private bool notStarted = false;

    public GameObject fase1Goblin1;
    public GameObject fase1Goblin2;

    public GameObject fase2Mushrom1;
    public GameObject fase2Mushrom2;

    public GameObject fase3Goblin1;
    public GameObject fase3Goblin2;

    public GameObject fase4Goblin1;
    public GameObject fase4Goblin2;
    public GameObject fase4Mushrom1;
    public GameObject fase4Mushrom2;

    public GameObject skeleton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!notStarted)
        {
            notStarted = true;
            BossFightFase1();
        }
    }

    private void BossFightFase1()
    {
        fase1Goblin1.SetActive(true);
        fase1Goblin2.SetActive(true);

        Invoke("BossFightFase2", 5f);
    }

    private void BossFightFase2()
    {
        fase2Mushrom1.SetActive(true);
        fase2Mushrom2.SetActive(true);

        Invoke("BossFightFase3", 3f);
    }

    private void BossFightFase3()
    {
        fase3Goblin1.SetActive(true);
        fase3Goblin2.SetActive(true);

        Invoke("BossFightFase4", 8f);
    }

    private void BossFightFase4()
    {
        fase4Goblin1.SetActive(true);
        fase4Goblin2.SetActive(true);
        fase4Mushrom1.SetActive(true);
        fase4Mushrom2.SetActive(true);

        Invoke("BossFightLastFase", 5f);
    }

    private void BossFightLastFase()
    {
        skeleton.SetActive(false);
    }
}
