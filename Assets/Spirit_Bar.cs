using UnityEngine;
using UnityEngine.UI;

public class Spirit_Bar : MonoBehaviour
{
    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(slider == null)
        {

           slider = GetComponent<Slider>();
        }
    }
    public void setmaxspirit(int spirit)
    {
        slider.maxValue = spirit;
        slider.value = spirit;
    }
    public void setspirit(int spirit)
    {
        slider.value = spirit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
