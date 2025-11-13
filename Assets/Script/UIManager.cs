using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator startBtn;
    public Animator settingBtn;
    public Animator settignDialog;
    public Animator contentPanel;
    public Animator gearImg;

    public void StartGame()
    {
        SceneManager.LoadScene("RocketMouse");
    }

    public void OpenSetting()
    {
        startBtn.SetBool("isHidden", true);
        settingBtn.SetBool("isHidden", true);

        settignDialog.SetBool("isHidden", false);
    }

    public void CloseSetting()
    {
        startBtn.SetBool("isHidden", false);
        settingBtn.SetBool("isHidden", false);

        settignDialog.SetBool("isHidden", true);
    }

    public void ToggleMenu()
    {
        bool isHidden = contentPanel.GetBool("isHidden");
        contentPanel.SetBool("isHidden", !isHidden);
        gearImg.SetBool("isHidden", !isHidden);
    }



}
