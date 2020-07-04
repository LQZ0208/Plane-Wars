using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//需要使用这个命名空间进行场景切换
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    //开始游戏
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //退出游戏
    public void QuitGame()
    {
        Application.Quit();
    }
    //帧动画显示主菜单
    public void UIenable()//在菜单动画结束后调用，用来显示button
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }

    //暂停游戏
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;//暂停游戏，实际上是改变游戏运算速度，就像是游戏中的慢动作改变这个范围比如0.5等等
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;//恢复游戏运行
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }
    //返回主菜单
    public void BackMenu()
    {

        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("level02"));
        SceneManager.LoadScene(0);//有问题！！！
        Time.timeScale = 1f;

    }
}
