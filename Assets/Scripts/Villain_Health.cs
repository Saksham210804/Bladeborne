using UnityEngine;
using UnityEngine.UI;
public class Villain_Health : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void setmaxhealth_enemy(int health)
    {

        slider.maxValue = health;
        slider.value = health;

    }
    public void SetHealth_enemy(int currentHealth)
    {
        slider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
