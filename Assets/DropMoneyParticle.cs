using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMoneyParticle : MonoBehaviour
{
    [HideInInspector] public int moneyToDrop;
    public GameObject moneyPrefab;
    public Transform goToPosition;

    public void dropMoney()
    {
        float dropForce= Random.Range(1500f, 3000f);
        for (int i = 0; i < moneyToDrop; i++)
        {
            MoneyParticleAnim moneySpawned = Instantiate(moneyPrefab, transform.parent).GetComponent<MoneyParticleAnim>();
            moneySpawned.transform.position = transform.position;
            moneySpawned.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * dropForce,ForceMode2D.Impulse);
            moneySpawned.goToPosition = goToPosition;
        }
    }
}
