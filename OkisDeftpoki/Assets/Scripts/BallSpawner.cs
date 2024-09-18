using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallSpawner : MonoBehaviour//, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // ������ ��� ������
    public GameObject objectToSpawn;

    // ������ � �����������, ������ �������� ����� ��������
    public GameObject spawnAreaObject;

    private Collider2D spawnAreaCollider;

    void Start()
    {
        // �������� ��������� ������� ������
        spawnAreaCollider = spawnAreaObject.GetComponent<Collider2D>();
        if (spawnAreaCollider == null)
        {
            Debug.LogError("� ������� spawnAreaObject ����������� Collider2D!");
        }
    }

    void Update()
    {
        // ��������� ������� ������ ��� ���� ����
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Vector3 touchPosition;

            if (Input.touchCount > 0)
            {
                // �������� �������� ���������� ������� �������
                Touch touch = Input.GetTouch(0);
                touchPosition = touch.position;
            }
            else
            {
                // �������� �������� ���������� ����
                touchPosition = Input.mousePosition;
            }

            // ����������� �������� ���������� � �������
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            worldPosition.z = 0;  // �������� Z ��� 2D

            // ���������, �������� �� �������/���� � ��������� �������
            if (spawnAreaCollider == Physics2D.OverlapPoint(worldPosition))
            {
                print("������� � �������� ������� ������");

                // ������� ������ � ����� �������/�����
                Instantiate(objectToSpawn, worldPosition, Quaternion.identity);
            }
            else
            {
                print("������� �� ��������� ������� ������");
            }
        }
    }

    // ����� ��� ��������� ����� (����� ������ ������� � ���������)
    void OnDrawGizmos()
    {
        if (spawnAreaObject != null)
        {
            // ������ ������ ������� ������ � ���� �������� (�����)
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(spawnAreaObject.transform.position, spawnAreaObject.transform.localScale);
        }
    }
}
