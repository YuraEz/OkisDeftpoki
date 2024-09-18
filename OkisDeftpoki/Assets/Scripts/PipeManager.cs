using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private List<Pipe> pipes;    // Список труб

    [SerializeField] private Sprite lightOn;      // Спрайт для включенного света
    [SerializeField] private Sprite lightOff;     // Спрайт для выключенного света

    [SerializeField] private float delayBetweenActions = 1f;  // Задержка между действиями
    [SerializeField] private float lightOnDuration = 0.5f;    // Время, сколько труба будет со светом

    private void Start()
    {
        // Изначально все трубы с выключенным светом
        foreach (Pipe pipe in pipes)
        {
            pipe.light.sprite = lightOff;
            pipe.pipeTrigger.SetActive(false);
            pipe.col.enabled = false;
        }

        // Запуск корутины для управления светом и анимацией
        StartCoroutine(PipeSequence());
    }

    private IEnumerator PipeSequence()
    {
        while (true)
        {
            // Выбираем случайную трубу
            Pipe randomPipe = pipes[Random.Range(0, pipes.Count)];

            // Включаем свет на выбранной трубе
            randomPipe.light.sprite = lightOn;

            // Ждем некоторое время (время, пока свет будет включен)
            yield return new WaitForSeconds(lightOnDuration);

            randomPipe.pipeTrigger.SetActive(true);

            randomPipe.col.enabled = true;

            // Invoke(nameof(TurnOff(randomPipe)), 4);
            StartCoroutine(DeactivateTriggerAfterDelay(randomPipe, 4f));

            // Запускаем анимацию (установим триггер "up")
            randomPipe.animator.SetTrigger("up");

            // Отключаем свет после анимации
            randomPipe.light.sprite = lightOff;

            // Ждем перед тем, как снова выбрать трубу
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
