using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The bomb is visual only: it homes in without a collider, then damages an area on impact.
public class BombAttackEffect : MonoBehaviour
{
    private GameObject _target;
    private SOAtack _attack;
    private SpriteRenderer _renderer;
    private const float ExplosionRadius = 1.15f;

    public void Init(GameObject target, SOAtack attack, SpriteRenderer turretRenderer)
    {
        _target = target;
        _attack = attack;
        _renderer = gameObject.AddComponent<SpriteRenderer>();
        _renderer.sprite = attack.Sprite;
        _renderer.sharedMaterial = turretRenderer.sharedMaterial;
        _renderer.sortingLayerID = turretRenderer.sortingLayerID;
        _renderer.sortingOrder = turretRenderer.sortingOrder + 1;
        StartCoroutine(TravelAndExplode());
    }

    private IEnumerator TravelAndExplode()
    {
        Vector3 destination = _target != null ? _target.transform.position : transform.position;
        while (true)
        {
            if (_target != null) destination = _target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, destination, Mathf.Max(_attack.Speed, 0.01f) * Time.deltaTime);
            if (Vector3.Distance(transform.position, destination) < 0.001f) break;
            yield return null;
        }

        var damaged = new HashSet<EnemyController>();
        foreach (Collider2D hit in Physics2D.OverlapCircleAll(transform.position, ExplosionRadius))
        {
            EnemyController enemy = hit.GetComponentInParent<EnemyController>();
            if (enemy != null && damaged.Add(enemy)) enemy.Damaged(_attack.Damage);
        }

        float duration = 0.5f;
        if (_attack.Animation != null)
        {
            Animator animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = _attack.Animation;
            AnimationClip[] clips = _attack.Animation.animationClips;
            if (clips.Length > 0 && clips[0].frameRate > 0)
                duration = clips[0].length + 1f / clips[0].frameRate;
        }
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
