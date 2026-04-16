using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler<OnEnemyDestroyedEventArgs> OnEnemyDestroyed;
    public class OnEnemyDestroyedEventArgs : EventArgs
    {
        public string DestroyedGameObjectTag;
    }

    private int _playerScore;
    public int PlayerScore => _playerScore;


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

    private void Start()
    {
        PlayerPrefs.SetInt("PlayerScore", 0);
    }

    public void EnemyDestroyed(int p_points, string p_gameObjectTag)
    {
        if(p_gameObjectTag == "Laser")
        {
            _playerScore += p_points;
        }
        
        OnEnemyDestroyed?.Invoke(this, new OnEnemyDestroyedEventArgs
        {
            DestroyedGameObjectTag = p_gameObjectTag
        });
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("PlayerScore", _playerScore);

        if (PlayerPrefs.HasKey("PlayerHighScore"))
        {
            int highScore = PlayerPrefs.GetInt("PlayerHighScore", 0);
            if (_playerScore > highScore) 
            {
                PlayerPrefs.SetInt("PlayerHighScore", _playerScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt("PlayerHighScore", _playerScore);
        }

        PlayerPrefs.Save();

        SceneManager.LoadScene("End");
    }
}
