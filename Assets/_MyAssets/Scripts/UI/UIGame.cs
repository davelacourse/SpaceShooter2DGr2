using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _playerLivesImage;
    [SerializeField] private Sprite[] _playerLifesSpritesArray;

    private Player _player;
    
    private void Start()
    {
        _player = FindAnyObjectByType<Player>();
        ChangeLivesDisplayImage(_playerLifesSpritesArray.Length - 1);
        if (_playerLifesSpritesArray.Length - 1 != _player.PlayerLives)
        {
            Debug.LogError("Attention le nombre d'images et différents du nombre de vies !!!!");
        }
        UpdateScore();
        GameManager.Instance.OnEnemyDestroyed += GameManager_OnEnemyDestroyed;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnEnemyDestroyed -= GameManager_OnEnemyDestroyed;
    }

    private void GameManager_OnEnemyDestroyed(object sender, GameManager.OnEnemyDestroyedEventArgs e)
    {
        if(e.DestroyedGameObjectTag == "Laser")
        {
            UpdateScore();
        }
        else if(e.DestroyedGameObjectTag == "Player")
        {
            ChangeLivesDisplayImage(_player.PlayerLives);
        }
    }

    private void ChangeLivesDisplayImage(int p_noImage)
    {
        if(p_noImage < 0)
        {
            p_noImage = 0;
        }

        _playerLivesImage.sprite = _playerLifesSpritesArray[p_noImage];
    }

    private void UpdateScore()
    {
        _scoreText.text = $"Pointage: {GameManager.Instance.PlayerScore}";
    }
}
