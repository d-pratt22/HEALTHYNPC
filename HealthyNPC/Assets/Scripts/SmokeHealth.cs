using System;
using System.Collections;
using UnityEngine;

public class SmokeHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private ParticleSystem smokeBomb;
    [SerializeField] private GameObject npc;
    [SerializeField] private float hiddenTime = 5f;

    private int currentHealth;
    private bool canTakeDamage = true;

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public float CurrentHpPct
    {
        get { return (float)currentHealth / (float)startingHealth; }
    }

    public void TakeDamage(int amount)
    {
        if (canTakeDamage)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException("Invalid Damage amount specified: " + amount);

            currentHealth -= amount;

            OnHPPctChanged(CurrentHpPct);

            SmokeBomb();

            StartCoroutine(HiddenTime());

            if (CurrentHpPct <= 0)
                Die();
        }
    }

    private void SmokeBomb()
    {
        smokeBomb.Play();
    }

    private IEnumerator HiddenTime()
    {
        npc.SetActive(false);
        canTakeDamage = false;
        yield return new WaitForSeconds(hiddenTime);
        npc.SetActive(true);
        canTakeDamage = true;
    }

    private void Die()
    {
        OnDied();
        GameObject.Destroy(this.gameObject);
    }
}
