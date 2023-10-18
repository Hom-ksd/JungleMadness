using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Damageable damageable;
    public Slider slider;

    private void Awake()
    {
        slider.maxValue = damageable.MaxHealth;
        slider.value = damageable.MaxHealth;
    }
    public void UpdateHealthBar()
    {
        slider.value = damageable.Health;
    }

    // Update is called once per frame
    void Update()
    {
       UpdateHealthBar();
    }
}
