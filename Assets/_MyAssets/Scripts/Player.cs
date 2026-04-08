using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Propriétes Joueur")]
    [SerializeField] private float _playerSpeed = 10f;
    public float PlayerSpeed { get => _playerSpeed; set => _playerSpeed = value;}
    [SerializeField] private float _moveMaxHeight = 0f;

    [Header("Propriétes Attaque(laser)")]
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate = 0.5f;

    private InputSystem_Actions _inputSystemActions;
    private float _minX, _maxX, _minY, _maxY;
    private float _canFire = 0f;

    private SpriteRenderer _spriteRenderer;

    private bool _isFiring = false;

    private void Start()
    {
        // Liaision avec les input actions
        _inputSystemActions = new InputSystem_Actions();
        _inputSystemActions.Player.Enable();
        // _inputSystemActions.Player.Attack.performed += Attack_performed;
        _inputSystemActions.Player.Attack.started += _ => _isFiring = true;
        _inputSystemActions.Player.Attack.canceled += _ => _isFiring = false;

        // Permet de claculer la largueur et hauteur de mon joueur
        _spriteRenderer = GetComponent<SpriteRenderer>();
        float halfPlayerWidth = _spriteRenderer.bounds.extents.x;
        float halfPlayerHeight = _spriteRenderer.bounds.extents.y;

        //En fonction de la dimension de l'écran, définir les limites
        Camera mainCamera = Camera.main;

        _minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + halfPlayerWidth;
        _maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - halfPlayerWidth;
        _maxY = _moveMaxHeight - halfPlayerHeight;
        _minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + halfPlayerHeight;
    }

    /*
    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(_canFire < Time.time)
        {
            // Faire apparaitre un laser / Lancer attaque du joueur
            Instantiate(_laserPrefab, transform.position + new Vector3(0f, 0.7f, 0f), Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }

    }
    */

    private void OnDestroy()
    {
        // _inputSystemActions.Player.Attack.performed -= Attack_performed;
        _inputSystemActions.Player.Disable();
    }

    private void Update()
    {
        PlayerMovement();

        if(_isFiring && _canFire < Time.time)
        {
            // Faire apparaitre un laser / Lancer attaque du joueur
            Instantiate(_laserPrefab, transform.position + new Vector3(0f, 0.7f, 0f), Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
    }

    private void PlayerMovement()
    {
        Vector2 direction2D = _inputSystemActions.Player.Move.ReadValue<Vector2>();
        direction2D.Normalize();
        transform.Translate(direction2D * Time.deltaTime * _playerSpeed);

        float clampedX = Mathf.Clamp(transform.position.x, _minX, _maxX);
        float clampedY = Mathf.Clamp(transform.position.y, _minY, _maxY);

        transform.position = new Vector2(clampedX, clampedY);
    }
}
