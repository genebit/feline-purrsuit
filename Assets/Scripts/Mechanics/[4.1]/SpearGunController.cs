using UnityEngine;

public class SpearGunController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    [Range(0f, 100f)]
    private float bulletSpeed;

    void Update()
    {
        // rotate the z of the spear gun towards the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDir = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        RotateTowardsMouse(mousePosition, angle);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot(lookDir);
        }
    }

    void Shoot(Vector3 shootDirection)
    {
        // shoot the bullet prefab based on the angle of the spear gun
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var controller = bullet.GetComponent<HookController>();
        controller.SetInitialBulletDirection(shootDirection);
    }

    void RotateTowardsMouse(Vector3 mousePosition, float angle)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // if the mouse is to the left of the spear gun, flip the sprite
        float y = (mousePosition.x < transform.position.x) ? -0.05f : 0.05f;
        transform.localScale = new Vector3(0.04f, y, 0.04f);
    }
}