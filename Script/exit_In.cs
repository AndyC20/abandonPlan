using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exit_In : MonoBehaviour
{
    public Button exit_button;

    void start(){
        Button exit = exit_button.GetComponent<Button>();
    }

    public void back(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }
}
