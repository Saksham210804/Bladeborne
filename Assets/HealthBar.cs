using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float max_health;
    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
    }
    public void setmaxhealth(int health)
    {

       slider.maxValue = health;
        slider.value = health;
    }
    public void sethealth( int health)
    {
        slider.value = health;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
