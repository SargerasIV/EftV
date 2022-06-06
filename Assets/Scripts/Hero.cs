using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed;     // объявление полей, значения задаются через инспектор
    [SerializeField] private float jumpForce;

    private Rigidbody2D rb; // объявление компонентов
    private SpriteRenderer sprite;
    private Animator animatorr;

    private bool grounded; // булева переменная, используется для того, чтобы определить состояние объекта

    private States Stat // получение и изменение заданной в Аниматоре целочисленной переменной для воспроизведения нужной анимации
    {
        get { return (States)animatorr.GetInteger("status"); } 
        set { animatorr.SetInteger("status", (int)value); }
    }
    private void Awake() // метод, вызывается при запуске скрипта
    {
        rb = GetComponent<Rigidbody2D>(); // компоненты получают значения
        sprite = GetComponentInChildren<SpriteRenderer>(); // этот компонент получает значения от дочерних объектов
        animatorr = GetComponent<Animator>();
    }

    private void FixedUpdate() // функция, вызывается через фиксированное количество кадров
    {
        CheckGround();
    }

    private void Update() // метод, вызываемый на каждом кадре
    {
        if (grounded) Stat = States.stand; // воспроизведение анимации stand, если булева переменная равна 1
        if (Input.GetButton("Horizontal")) // проверка на активацию клавиш движения
            Move(); 
        if (grounded && Input.GetButtonDown("Jump")) // проверка на условие активации клавиши "пробел" и наличия поверхности под объектом
        {
            animatorr.SetTrigger("prepare_jump"); // активация триггера на воспроизведение анимации подготовки к прыжку
            Jump();
        }
        if (!grounded) Stat = States.jump_go; // проверка на отсутствие поверхности под объектом, в противном случае воспроизводится анимация прыжка
    }
    private void Move() // объявление функции движения
    {
        if (grounded) Stat = States.move; // проверка на наличие поверхности под объектом и включение анимации движения

        Vector3 horizontalInput = transform.right * Input.GetAxis("Horizontal"); // объявление переменной, которая получает значение положения объекта по оси абсцисс
        transform.position = Vector3.MoveTowards(transform.position, horizontalInput + transform.position, Time.deltaTime * speed); // изменение переменной по вектору в пространсве, задание происходит по значению текущей позиции, новой позиции и временным промежутком
        sprite.flipX = horizontalInput.x < 0.0f; // отражение спрайта по оси ординат при отрицательном росте значения X
    }

    private void Jump() // объявление функции прыжка
    {
        rb.velocity = Vector2.up * jumpForce; // придание объекту импульса, направленного вертикально вверх
    }

    private void CheckGround() // объявление функции проверки на нахождение на поверхности
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f); // объявление массива, размер которого зависит от количества физических объектов в определенном радиусе
        grounded = collider.Length > 1; // задание значения булевой переменной в зависимости от длинны массива
    }

}

public enum States // обявление перечислений, использующихся аниматором
{
    stand,
    move,
    jump_go
}