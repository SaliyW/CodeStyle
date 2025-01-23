using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        float hueMin = 0;
        float hueMax = 1;

        if (collision.gameObject.GetComponent<Bullet>())
        {
            GetComponent<Renderer>().material.color = Random.ColorHSV(hueMin, hueMax);
        }
    }
}
