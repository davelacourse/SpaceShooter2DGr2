using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler<OnEnemyDestroyedEventArgs> OnEnemyDestroyed;
    public class OnEnemyDestroyedEventArgs : EventArgs
    {
        public string DestroyedGameObjectTag;
    }

    private int _playerScore;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogError("Un 2e Gamemanager tente d'être créer");
        }
    }

    public void EnemyDestroyed(int p_points, string p_gameObjectTag)
    {
        if(p_gameObjectTag == "Laser")
        {
            _playerScore += p_points;
            Debug.Log($"Pointage: {_playerScore}");
        }
        
        OnEnemyDestroyed?.Invoke(this, new OnEnemyDestroyedEventArgs
        {
            DestroyedGameObjectTag = p_gameObjectTag
        });
    }
}
