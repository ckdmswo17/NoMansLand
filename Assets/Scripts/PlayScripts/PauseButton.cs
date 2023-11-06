using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private bool isPaused = false; // 게임 일시정지 상태를 추적하는 변수
    public GameObject pausePanel;

    // 일시정지와 재개를 토글하는 함수
    public void TogglePause()
    {
        isPaused = !isPaused; // 일시정지 상태 토글
        Time.timeScale = isPaused ? 0 : 1; // 일시정지 상태이면 시간을 멈추고, 아니면 다시 시작

        if (isPaused)
        {
            pausePanel.SetActive(true);
        } else
        {
            pausePanel.SetActive(false);
        }
        
    }
}
