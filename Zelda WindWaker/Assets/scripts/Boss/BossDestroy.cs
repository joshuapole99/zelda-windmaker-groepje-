using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDestroy : MonoBehaviour
{

    public IEnumerator Detatch()
    {
        Debug.Log("Started!");
        yield return new WaitForSeconds(3);
        Transform[] _objects = GetComponentsInChildren<Transform>();
        Rigidbody[] _rb = new Rigidbody[_objects.Length];
        for (int i = 0; i < _rb.Length; i++)
        {
            _objects[i].parent = null;
            if (_objects[i].gameObject.GetComponent<BossAttack>() != null)
            {
                _objects[i].gameObject.GetComponent<BossAttack>().enabled = false;
            }
            if (_objects[i].gameObject.GetComponent<BossDown>() != null)
            {
                _objects[i].gameObject.GetComponent<BossDown>().enabled = false;
            }
            if (_objects[i].gameObject.GetComponent<BossHitPlayer>() != null)
            {
                _objects[i].gameObject.GetComponent<BossHitPlayer>().enabled = false;
            }
            if (_objects[i].gameObject.GetComponent<BossHitPoint>() != null)
            {
                _objects[i].gameObject.GetComponent<BossHitPoint>().enabled = false;
            }
            if (_objects[i].gameObject.GetComponent<BossHover>() != null)
            {
                _objects[i].gameObject.GetComponent<BossHover>().enabled = false;
            }
            if (_objects[i].gameObject.GetComponent<BossIgnoreColission>() != null)
            {
                _objects[i].gameObject.GetComponent<BossIgnoreColission>().enabled = false;
            }
            if (_objects[i].gameObject.GetComponent<BossLookHeight>() != null)
            {
                _objects[i].gameObject.GetComponent<BossLookHeight>().enabled = false;
            }
            if (_objects[i].gameObject.GetComponent<BossMovement>() != null)
            {
                _objects[i].gameObject.GetComponent<BossMovement>().enabled = false;
            }
            if (_objects[i].gameObject.GetComponent<BossEye>() != null)
            {
                _objects[i].gameObject.GetComponent<BossEye>().enabled = false;
            }
            _rb[i] = _objects[i].gameObject.AddComponent<Rigidbody>();
            _rb[i].useGravity = true;
            
            yield return new WaitForEndOfFrame();
            _rb[i].AddExplosionForce(1f, transform.position, 20f);
        }
    }
}
