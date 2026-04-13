using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;

    [SerializeField] private float _spawnIntervalMin = 3f;
    [SerializeField] private float _spawnIntervalMax = 5f;
    [SerializeField] private float _initialSpawnInterval = 3f;

    private SpriteRenderer _spriteRenderer;
    private float _halfEnemyWidth;
    private bool _isSpawning = true;

    private void Start()
    {
        _spriteRenderer = _enemyPrefab.GetComponent<SpriteRenderer>();
        _halfEnemyWidth = _spriteRenderer.bounds.extents.x;

        StartCoroutine(SpawnEnemyCoroutine());
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(_initialSpawnInterval);

        while (_isSpawning)
        {
 
            //Position aléatoire sur la largeur de mon écran de jeu.
            float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect + _halfEnemyWidth,
                Camera.main.orthographicSize * Camera.main.aspect - _halfEnemyWidth);
            Vector3 spawnPosition = new Vector3(randomX, Camera.main.orthographicSize + 2, 0f);

            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(Random.Range(_spawnIntervalMin, _spawnIntervalMax));
        }

    }

    public void EndSpawning()
    {
        _isSpawning = false;
    }
}
