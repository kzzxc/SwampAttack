using UnityEngine;

public class Shotgun : Weapon
{
    private float _accuracy;
    private int _bulletsCount;

    public override void Shoot(Transform shootPoint)
    {
        _accuracy = 0.3f;
        _bulletsCount = 5;

        for (int i = 0; i < _bulletsCount; i++)
        {
            float accuracyX = Random.Range( - _accuracy, _accuracy);
            float accuracyY = Random.Range( - _accuracy, _accuracy);

            Instantiate(Bullet, shootPoint.position + new Vector3(accuracyX, accuracyY, 0), Quaternion.identity);
        }
    }
}