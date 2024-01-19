using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{
    public Button start_button;
    public Button Instuction_button;

    void start(){
        Button start = start_button.GetComponent<Button>();
        Button instuc = Instuction_button.GetComponent<Button>();
    }

    public void to_game(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void to_Intr(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Instuction");
    }
}