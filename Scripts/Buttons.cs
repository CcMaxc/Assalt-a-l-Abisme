using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject text1;
    public GameObject button1;

    public GameObject text2;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;

    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene0()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowControls()
    {
        text1.SetActive(false);
        button1.SetActive(false);

        text2.SetActive(true);
        image1.SetActive(true);
        image2.SetActive(true);
        image3.SetActive(true);
        image4.SetActive(true);
        Invoke("LoadScene1", 3f);
    }

    public void LoadScene3()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

