using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer))]
public class Parallellax_Effect : MonoBehaviour
{
    [SerializeField]
    
    private float Parallelax_Speed_X;
    [SerializeField]
    private float Parallelax_Speed_Y;
    private Transform CameraTransform;
    private float Start_Pos_x;
    private float Start_Pos_Y;
    private float Sprite_Size;

    public void Start()
    {
        Start_Pos_x = transform.position.x;
        CameraTransform = Camera.main.transform;
        Sprite_Size = GetComponent<SpriteRenderer>().bounds.size.x;
        Start_Pos_Y = transform.position.y;
}

    public void Update()
    {
        float Camera_Dist = CameraTransform.position.x * Parallelax_Speed_X;
        float Camera_Dist_Y = CameraTransform.position.y * Parallelax_Speed_Y;
        transform.position = new Vector3(Start_Pos_x + Camera_Dist , transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, Start_Pos_Y + Camera_Dist_Y, transform.position.z);


        float relative_Camera_Dist = CameraTransform.position.x * (1 - Parallelax_Speed_X);
        float relative_Camera_Dist_Y = CameraTransform.position.y * (1 - Parallelax_Speed_Y);

        if (relative_Camera_Dist_Y > Start_Pos_Y + Sprite_Size)
        {
            Start_Pos_Y += Sprite_Size;
        }
        else if (relative_Camera_Dist_Y < Start_Pos_Y - Sprite_Size)
        {
            Start_Pos_Y -= Sprite_Size;
        }
        if (relative_Camera_Dist > Start_Pos_x + Sprite_Size)
        {
            Start_Pos_x += Sprite_Size;
        }
        else if(relative_Camera_Dist < Start_Pos_x - Sprite_Size)
        {
            Start_Pos_x -= Sprite_Size;
        }
    }








}