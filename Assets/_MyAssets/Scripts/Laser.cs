using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _diffFromPlayerSpeed = 5f;

    private float _laserSpeed;

    private void Start()
    {
        Player player = FindAnyObjectByType<Player>();
        _laserSpeed = player.PlayerSpeed + _diffFromPlayerSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * _laserSpeed);

        if (transform.position.y > Camera.main.orthographicSize + 2f)
        {
            Destroy(gameObject);
        }
    }
}
