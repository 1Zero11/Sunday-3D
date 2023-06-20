using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadRotation()
    {
        SceneManager.LoadSceneAsync("Rotation");
    }
    public void LoadDriving()
    {
        SceneManager.LoadSceneAsync("Driving");
    }

}
