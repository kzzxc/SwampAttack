using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Camera _camera;

    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private int _currentHealth;
    private Animator _animator;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        _currentWeapon = _weapons[0];
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        _weapons.Add(weapon);
        MoneyChanged?.Invoke(Money);
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _currentWeapon.Shoot(_shootPoint);
    }
}