using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float speedMax = 2.0f;

    public void DoMove()
    {
        speed = speedMax;
    }

    public void StopMove()
    {
        speed = 0.0f;
    }
}