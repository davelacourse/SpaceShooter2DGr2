using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 4f;
    [SerializeField] private int _enemyPoints = 100;

    private SpriteRenderer _spriteRenderer;
    private float _halfEnemyWidth;

    private void Start()
    {
        // Permet de calculer la largueur de mon ennemi
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _halfEnemyWidth = _spriteRenderer.bounds.extents.x;
    }


    private void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * _enemySpeed);
        if (transform.position.y < -Camera.main.orthographicSize - 2f)
        {
            //Position aléatoire sur la largeur de mon écran de jeu.
            float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect + _halfEnemyWidth,
                Camera.main.orthographicSize * Camera.main.aspect - _halfEnemyWidth);

            transform.position = new Vector3(randomX, Camera.main.orthographicSize + 2, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.EnemyDestroyed(_enemyPoints, collision.gameObject.tag);
            Destroy(gameObject);  // Destruction de l'ennemi
            if (collision.gameObject.CompareTag("Laser"))
            {
                Destroy(collision.gameObject); // Destruction du laser
            }
        }
        else
        {
            //Position aléatoire sur la largeur de mon écran de jeu.
            float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect + _halfEnemyWidth,
                Camera.main.orthographicSize * Camera.main.aspect - _halfEnemyWidth);

            collision.gameObject.transform.position = new Vector3(randomX, Camera.main.orthographicSize + 2, 0f);
        }

    }
}
