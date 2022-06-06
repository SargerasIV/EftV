using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // используем библиотеку для возможности работы со сценами

public class _MenuContext : MonoBehaviour
{
    public static bool GamePaused = false; // булева переменная, используется для определения состояния игры, на паузе или нет

    public GameObject pauseMenuUI; // объявление объекта, отвечающего за окно паузы

    // Update is called once per frame
    void Update() // метод, вызываемый на каждом кадре
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // проверка на активацию клавиши Esc
        {
            if (GamePaused) // инструкция, которая выполняет возобновление игрового процесса с режима паузы и наоборот
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void StartGame() // функция, активирующая сцену, следующую за текущей
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Resume() // функция возобновления игрового процесса с паузы
    {
        pauseMenuUI.SetActive(false); // отключение отображения объекта с окном паузы
        Time.timeScale = 1f; // изменение скорости течения времени в игровом процессе по умолчанию
        GamePaused = false; // задание значения булевой переменной ложью
    }

    void Pause() // функция приостановки игрового процесса 
    {
        pauseMenuUI.SetActive(true); // включение отображения объекта с окном паузы
        Time.timeScale = 0f; // изменение скорости течения времени в игровом процессе на нулевое
        GamePaused = true; // задание значения булевой переменной истинной
    }

    public void StartMenu() // функция, переключающая текущую сцену на главную
    {
        SceneManager.LoadScene("ContextMenu");
        Time.timeScale = 1f; // изменение скорости течения времени в игровом процессе по умолчанию
    }

    public void QuitGame() // функция выхода из приложения
    {
        Application.Quit();
    }
}
