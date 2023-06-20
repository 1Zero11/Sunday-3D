using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    ColorChanger changer;
    // Start is called before the first frame update

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void Start()
    {
        changer = GetComponent<ColorChanger>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position, Camera.MonoOrStereoscopicEye.Mono);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        Debug.Log("Hit!");
                        changer.ChangeColor();
                    }
                }
            }
        }
    }
}
