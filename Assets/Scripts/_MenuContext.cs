using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ���������� ���������� ��� ����������� ������ �� �������

public class _MenuContext : MonoBehaviour
{
    public static bool GamePaused = false; // ������ ����������, ������������ ��� ����������� ��������� ����, �� ����� ��� ���

    public GameObject pauseMenuUI; // ���������� �������, ����������� �� ���� �����

    // Update is called once per frame
    void Update() // �����, ���������� �� ������ �����
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // �������� �� ��������� ������� Esc
        {
            if (GamePaused) // ����������, ������� ��������� ������������� �������� �������� � ������ ����� � ��������
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void StartGame() // �������, ������������ �����, ��������� �� �������
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Resume() // ������� ������������� �������� �������� � �����
    {
        pauseMenuUI.SetActive(false); // ���������� ����������� ������� � ����� �����
        Time.timeScale = 1f; // ��������� �������� ������� ������� � ������� �������� �� ���������
        GamePaused = false; // ������� �������� ������� ���������� �����
    }

    void Pause() // ������� ������������ �������� �������� 
    {
        pauseMenuUI.SetActive(true); // ��������� ����������� ������� � ����� �����
        Time.timeScale = 0f; // ��������� �������� ������� ������� � ������� �������� �� �������
        GamePaused = true; // ������� �������� ������� ���������� ��������
    }

    public void StartMenu() // �������, ������������� ������� ����� �� �������
    {
        SceneManager.LoadScene("ContextMenu");
        Time.timeScale = 1f; // ��������� �������� ������� ������� � ������� �������� �� ���������
    }

    public void QuitGame() // ������� ������ �� ����������
    {
        Application.Quit();
    }
}
