using UnityEngine;

public class DamageNPC : MonoBehaviour
{
    public GameObject NPC;
    public int Damage = 50;

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<NPC>().TakeDamage(Damage);
        }
    }
}
