using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using System.Threading;
using UniRx;

public class TestMultiThread : MonoBehaviour
{
    private float time;

    void Start()
    {
        Debug.Log("Start");
        Observable
            .Start(Count)
            .SubscribeOn(Scheduler.ThreadPool) // Указываем использование пула потоков для выполнения метода
            .ObserveOn(Scheduler.MainThread) // Указываем переключение на основной поток после выполнения метода
            .Subscribe(result =>
            {
                EndLog();
            });
        Debug.Log("Thread free");
    }

    private void Count()
    {
        Debug.Log("Start count");
        for (int i = 0; i < 100; i++)
        {
            Thread.Sleep(50);
        }

        Debug.Log("Finish Count");
    }

    private void EndLog()
    {
        Debug.Log("all operations ends");
    }
}
