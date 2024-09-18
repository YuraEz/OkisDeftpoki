using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private List<Pipe> pipes;    // ������ ����

    [SerializeField] private Sprite lightOn;      // ������ ��� ����������� �����
    [SerializeField] private Sprite lightOff;     // ������ ��� ������������ �����

    [SerializeField] private float delayBetweenActions = 1f;  // �������� ����� ����������
    [SerializeField] private float lightOnDuration = 0.5f;    // �����, ������� ����� ����� �� ������

    private void Start()
    {
        // ���������� ��� ����� � ����������� ������
        foreach (Pipe pipe in pipes)
        {
            pipe.light.sprite = lightOff;
            pipe.pipeTrigger.SetActive(false);
            pipe.col.enabled = false;
        }

        // ������ �������� ��� ���������� ������ � ���������
        StartCoroutine(PipeSequence());
    }

    private IEnumerator PipeSequence()
    {
        while (true)
        {
            // �������� ��������� �����
            Pipe randomPipe = pipes[Random.Range(0, pipes.Count)];

            // �������� ���� �� ��������� �����
            randomPipe.light.sprite = lightOn;

            // ���� ��������� ����� (�����, ���� ���� ����� �������)
            yield return new WaitForSeconds(lightOnDuration);

            randomPipe.pipeTrigger.SetActive(true);

            randomPipe.col.enabled = true;

            // Invoke(nameof(TurnOff(randomPipe)), 4);
            StartCoroutine(DeactivateTriggerAfterDelay(randomPipe, 4f));

            // ��������� �������� (��������� ������� "up")
            randomPipe.animator.SetTrigger("up");

            // ��������� ���� ����� ��������
            randomPipe.light.sprite = lightOff;

            // ���� ����� ���, ��� ����� ������� �����
            yield return new WaitForSeconds(delayBetweenActions);
        }
    }

    private IEnumerator DeactivateTriggerAfterDelay(Pipe pipe, float delay)
    {
        yield return new WaitForSeconds(delay);
        pipe.pipeTrigger.SetActive(false);
        pipe.col.enabled = false;
    }

}
