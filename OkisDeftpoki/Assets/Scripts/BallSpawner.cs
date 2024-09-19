using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallSpawner : MonoBehaviour//, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Префаб для спавна
    public GameObject objectToSpawn;

    // Объект с коллайдером, внутри которого можно спавнить
    public GameObject spawnAreaObject;

    // TextMeshPro для отображения количества шаров
    public TextMeshProUGUI ballsCountText;

    // Переменная для количества шаров
    public int ballCount = 5;  // Начальное количество шаров

    private Collider2D spawnAreaCollider;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    void Start()
    {
        // Получаем коллайдер области спавна
        spawnAreaCollider = spawnAreaObject.GetComponent<Collider2D>();
        if (spawnAreaCollider == null)
        {
            Debug.LogError("У объекта spawnAreaObject отсутствует Collider2D!");
        }

        // Обновляем текст отображения количества шаров
        UpdateBallsCountText();
    }


    [SerializeField] private Animator effect;

    private void ChangeScreen()
    {
        ServiceLocator.GetService<UIMANAGER>().ChangeScreen("lose");

    }

    void Update()
    {
        // Проверяем касание экрана или клик мыши
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            // Если шары закончились
            if (ballCount == 0)
            {

                if (effect) effect.SetTrigger("exit");
                print("Вы не можете спавнить шар: у вас закончились шары!");
                Invoke(nameof(ChangeScreen), 0.25f);
                //, true);
                return; // Выходим из метода, если шаров нет
            }

            Vector3 touchPosition;

            if (Input.touchCount > 0)
            {
                // Получаем экранные координаты первого касания
                Touch touch = Input.GetTouch(0);
                touchPosition = touch.position;
            }
            else
            {
                // Получаем экранные координаты мыши
                touchPosition = Input.mousePosition;
            }

            // Преобразуем экранные координаты в мировые
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            worldPosition.z = 0;  // Обнуляем Z для 2D

            // Проверяем, попадает ли касание/клик в коллайдер объекта
            if (spawnAreaCollider == Physics2D.OverlapPoint(worldPosition))
            {
                print("Касание в пределах области спавна");

                // Спавним объект в точке касания/клика
                Instantiate(objectToSpawn, worldPosition, Quaternion.identity);

                // Уменьшаем количество шаров
                ballCount--;

                // Обновляем текст отображения количества шаров
                UpdateBallsCountText();
            }
            else
            {
                print("Касание за пределами области спавна");
            }
        }
    }

    // Метод для увеличения количества шаров
    public void AddBalls(int amount)
    {
        ballCount += amount;

        // Обновляем текст отображения количества шаров
        UpdateBallsCountText();
    }

    // Обновление текста количества шаров
    private void UpdateBallsCountText()
    {
        if (ballsCountText != null)
        {
            ballsCountText.text = ballCount.ToString();
        }
    }

    // Метод для рисования гизмо (чтобы видеть область в редакторе)
    void OnDrawGizmos()
    {
        if (spawnAreaObject != null)
        {
            // Рисуем контур области спавна в виде квадрата (гизмо)
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(spawnAreaObject.transform.position, spawnAreaObject.transform.localScale);
        }
    }
}
