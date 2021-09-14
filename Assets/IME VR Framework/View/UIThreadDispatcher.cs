using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

/*
 * OBJETO QUE INTERMEDEIA AÇÕES ENTRE OUTRAS Threads E A UIThread 
 */
public class UIThreadDispatcher : MonoBehaviour {

    //VARIÁVEIS DE CONTROLE
    private static UIThreadDispatcher _instance;
    private static volatile bool _queued = false;
    private static List<Action> _backlog = new List<Action>(8);
    private static List<Action> _actions = new List<Action>(8);

    //MÉTODOS PÚBLICOS E ESTÁTICOS
    public static void RunAsync(Action action)
    {
        ThreadPool.QueueUserWorkItem(o => action());
    }

    public static void RunAsync(Action<object> action, object state)
    {
        ThreadPool.QueueUserWorkItem(o => action(o), state);
    }

    public static void RunOnMainThread(Action action)
    {
        lock (_backlog)
        {
            _backlog.Add(action);
            _queued = true;
        }
    }

    //INICIALIZAÇÃO
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (_instance == null)
        {
            _instance = new GameObject("UIThreadDispatcher").AddComponent<UIThreadDispatcher>();
            DontDestroyOnLoad(_instance.gameObject);
        }
    }

    //ATUALIZAÇÃO
    private void Update()
    {
        if (_queued)
        {
            lock (_backlog)
            {
                var tmp = _actions;
                _actions = _backlog;
                _backlog = tmp;
                _queued = false;
            }

            foreach (var action in _actions)
                action();

            _actions.Clear();
        }
    }
}