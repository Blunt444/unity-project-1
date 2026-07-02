using System;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;
    public Vector2 aimDirection;
    public float shootCooldown;
    public Animator anim;
    public PlayerMovement playerMovement;
    public Camera mainCamera;

    private float shootTimer;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        anim.SetFloat("aimX", aimDirection.x);
        anim.SetFloat("aimY", aimDirection.y);
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            playerMovement.isShooting = true;
            playerMovement.ChangeState(PlayerState.Shooting);
        }
        if (playerMovement.isShooting)
        {
            HandleAiming();
        }
    }
    private void OnEnable()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 1);
    }
    private void OnDisable()
    {
        anim.SetLayerWeight(0, 1);
        anim.SetLayerWeight(1, 0);
    }
    public void Shoot()
    {
        if (shootTimer > 0) return;

        Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();

        arrow.Launch(aimDirection);
        shootTimer = shootCooldown;

        playerMovement.ChangeState(PlayerState.Idle);
        playerMovement.isShooting = false;
    }
    private void HandleAiming()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCamera.WorldToScreenPoint(transform.position).z;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        aimDirection = (mouseWorldPos - launchPoint.position);
        aimDirection.Normalize();

        anim.SetFloat("aimX", aimDirection.x);
        anim.SetFloat("aimY", aimDirection.y);

        if (aimDirection.x != 0)
        {
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * (aimDirection.x > 0 ? 1 : -1), transform.localScale.y, transform.localScale.z);
        }
    }
}
