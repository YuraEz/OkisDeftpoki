using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallSpawner : MonoBehaviour//, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // ������ ��� ������
    public GameObject objectToSpawn;

    // ������ � �����������, ������ �������� ����� ��������
    public GameObject spawnAreaObject;

    // TextMeshPro ��� ����������� ���������� �����
    public TextMeshProUGUI ballsCountText;

    // ���������� ��� ���������� �����
    public int ballCount = 5;  // ��������� ���������� �����

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
        // �������� ��������� ������� ������
        spawnAreaCollider = spawnAreaObject.GetComponent<Collider2D>();
        if (spawnAreaCollider == null)
        {
            Debug.LogError("� ������� spawnAreaObject ����������� Collider2D!");
        }

        // ��������� ����� ����������� ���������� �����
        UpdateBallsCountText();
    }


    [SerializeField] private Animator effect;

    private void ChangeScreen()
    {
        ServiceLocator.GetService<UIMANAGER>().ChangeScreen("lose");

    }

    void Update()
    {
        // ��������� ������� ������ ��� ���� ����
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            // ���� ���� �����������
            if (ballCount == 0)
            {

                if (effect) effect.SetTrigger("exit");
                print("�� �� ������ �������� ���: � ��� ����������� ����!");
                Invoke(nameof(ChangeScreen), 0.25f);
                //, true);
                return; // ������� �� ������, ���� ����� ���
            }

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

                // ��������� ���������� �����
                ballCount--;

                // ��������� ����� ����������� ���������� �����
                UpdateBallsCountText();
            }
            else
            {
                print("������� �� ��������� ������� ������");
            }
        }
    }

    // ����� ��� ���������� ���������� �����
    public void AddBalls(int amount)
    {
        ballCount += amount;

        // ��������� ����� ����������� ���������� �����
        UpdateBallsCountText();
    }

    // ���������� ������ ���������� �����
    private void UpdateBallsCountText()
    {
        if (ballsCountText != null)
        {
            ballsCountText.text = ballCount.ToString();
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
