using System.Collections;
using UnityEngine;

namespace Mechanics
{
    public class SpearGunController : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        [SerializeField] private GameObject player;
        [SerializeField] private PlayerDivingController playerController;
        [SerializeField] private SpriteRenderer playerSpriteRenderer;
        [SerializeField] private AudioSource gunSFX;

        [Range(0f, 10f)]
        [SerializeField] private float bulletSpeed;

        [Range(0f, 5f)]
        [SerializeField] private float bulletReloadSpeed;

        [Range(0f, 100f)]
        [SerializeField] private float knockbackStrength;

        private Rigidbody2D playerRb;
        private bool allowShooting;

        private void Start()
        {
            allowShooting = true;
            playerRb = player.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            // rotate the z of the spear gun towards the mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 lookDir = mousePosition - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

            RotateTowardsMouse(mousePosition, angle);

            if (Input.GetMouseButtonDown(0) && allowShooting)
            {
                StartCoroutine(Shoot(lookDir));
            }
        }

        IEnumerator Shoot(Vector3 shootDirection)
        {
            // if the player is not underwater, play the gun sound effect and shoot the bullet
            if (!playerController.isBreathing)
            {

                gunSFX.Play();
                // shoot the bullet prefab based on the angle of the spear gun
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                var controller = bullet.GetComponent<HookController>();
                controller.SetInitialBulletDirection(shootDirection);
                controller.speed = bulletSpeed;

                playerRb.AddForce(-shootDirection.normalized * knockbackStrength, ForceMode2D.Impulse);

                allowShooting = false;
                yield return new WaitForSeconds(bulletReloadSpeed);
                allowShooting = true;
            }
        }

        void RotateTowardsMouse(Vector3 mousePosition, float angle)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            // if the mouse is to the left of the spear gun, flip the sprite
            float y = mousePosition.x < transform.position.x ? -0.05f : 0.05f;
            transform.localScale = new Vector3(0.04f, y, 0.04f);

            playerSpriteRenderer.flipX = mousePosition.x <= transform.position.x;
        }
    }
}