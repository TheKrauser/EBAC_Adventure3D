using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    //[SerializeField] private GameObject coinParticle;

    private void Awake()
    {
        //base.AudioSource = GetComponentInChildren<AudioSource>();
        base.Visuals = transform.Find("Visuals");
        base.Coll = GetComponent<Collider>();
    }

    protected override void OnCollect()
    {
        //var obj = Instantiate(coinParticle, transform.position, Quaternion.identity);
        //obj.transform.SetParent(null);
        base.Visuals.gameObject.SetActive(false);
        base.Coll.enabled = false;

        if(base.AudioSource != null) base.AudioSource.Play();

        //Destroy(obj, 3f);
        Destroy(gameObject, 3f);
        base.OnCollect();
    }
}
