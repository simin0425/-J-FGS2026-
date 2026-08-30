using UnityEngine;

public class NatureSpike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerManagerOM>().Damage(1);
            //Debug.Log("NatureSpike : Hit Player");
        }
    }
}
