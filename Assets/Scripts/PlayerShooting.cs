using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireCooldown = 0.2f;

    private float fireTimer = 0f;
    private Camera mainCam;

    public int projectileCount = 1;
    public float projectileSpeed = 10f;
    public int projectileDamage = 1;
    public bool tripleShot = false;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) && fireTimer >= fireCooldown)
        {
            ShootTowardMouse();
            fireTimer = 0f;
        }
    }

    void ShootTowardMouse()
    {
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector2 baseDir = (mouseWorldPos - firePoint.position).normalized;

        int count = tripleShot ? 3 : projectileCount;
        float spreadAngle = 15f;

        for (int i = 0; i < count; i++)
        {
            float angleOffset = (count == 1) ? 0 : spreadAngle * (i - (count - 1) / 2f);
            Vector2 dir = Quaternion.Euler(0, 0, angleOffset) * baseDir;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = dir * projectileSpeed;

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.damage = projectileDamage;
        }
    }

    // 💥 아이템 효과용 코루틴들
    public System.Collections.IEnumerator BoostFireRate(float amount, float duration)
    {
        fireCooldown -= amount;
        yield return new WaitForSeconds(duration);
        fireCooldown += amount;
    }

    public System.Collections.IEnumerator BoostDamage(int bonus, float duration)
    {
        projectileDamage += bonus;
        yield return new WaitForSeconds(duration);
        projectileDamage -= bonus;
    }

    public System.Collections.IEnumerator TripleShot(float duration)
    {
        tripleShot = true;
        yield return new WaitForSeconds(duration);
        tripleShot = false;
    }
}
