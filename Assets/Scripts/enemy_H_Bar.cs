using UnityEngine;
using UnityEngine.UI;
public class enemy_H_Bar : MonoBehaviour
{

    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void setmaxhealth_enemy(float health)
    {

        slider.maxValue = health;
        slider.value = health;
        
    }
    public void SetHealth_enemy(float currentHealth)
    {
        slider.value = currentHealth;
        Debug.Log("Enemy health upgraded");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
