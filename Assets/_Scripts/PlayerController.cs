using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputs), typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _cam;

    [Header("Move Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _sprintMultiplier = 2f;

    [Header("Look Settings")]
    [SerializeField] private float _lookSense = .1f;
    [SerializeField] private float _lookLimitV = 89f;

    public static event Action OnAttack;
    public static event Action OnRespawn;

    private Vector2 MoveInput => _playerInputs.MoveInput;
    private Vector2 LookInput => _playerInputs.LookInput;
    private bool SprintInput => _playerInputs.SprintInput;
    private bool AttackInput => _playerInputs.AttackInput;
    private bool RespawnInput => _playerInputs.RespawnInput;

    private PlayerInputs _playerInputs;
    private CharacterController _characterController;

    private Vector2 _cameraRotation = Vector2.zero;

    private void Awake()
    {
        _playerInputs = GetComponent<PlayerInputs>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float speed = SprintInput ? _moveSpeed * _sprintMultiplier : _moveSpeed;
        float hSpeed = MoveInput.x * speed;
        float vSpeed = MoveInput.y * speed;

        Vector3 hMovement = new Vector3(hSpeed, 0, vSpeed);
        hMovement = transform.rotation * hMovement;

        _characterController.Move(hMovement * Time.deltaTime);

        if (AttackInput) OnAttack?.Invoke();
        if (RespawnInput) OnRespawn?.Invoke();
    }

    private void LateUpdate()
    {
        _cameraRotation.x = LookInput.x * _lookSense;
        _cameraRotation.y -= LookInput.y * _lookSense;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y, -_lookLimitV, _lookLimitV);

        transform.Rotate(0, _cameraRotation.x, 0);
        _cam.transform.localRotation = Quaternion.Euler(_cameraRotation.y, 0, 0);
    }
}
