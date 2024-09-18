using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallSpawner : MonoBehaviour//, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Префаб для спавна
    public GameObject objectToSpawn;

    // Объект с коллайдером, внутри которого можно спавнить
    public GameObject spawnAreaObject;

    private Collider2D spawnAreaCollider;

    void Start()
    {
        // Получаем коллайдер области спавна
        spawnAreaCollider = spawnAreaObject.GetComponent<Collider2D>();
        if (spawnAreaCollider == null)
        {
            Debug.LogError("У объекта spawnAreaObject отсутствует Collider2D!");
        }
    }

    void Update()
    {
        // Проверяем касание экрана или клик мыши
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
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
            }
            else
            {
                print("Касание за пределами области спавна");
            }
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
