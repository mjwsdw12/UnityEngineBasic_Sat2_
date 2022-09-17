using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHitter : MonoBehaviour
{
    public KeyCode Key;
    [SerializeField] private LayerMask _noteLayer;
    private SpriteRenderer _spriteRenderer;
    private Color _colorOrigin;
    [SerializeField] private Color _colorPressed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _colorOrigin = _spriteRenderer.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            HitNote();
            SetColorPressed();
        }

        if(Input.GetKeyUp(Key))
        {
            SetColorOrigin();
        }
    }

    private void HitNote()
    {
        HitType hitType = HitType.Miss;
        List<Collider2D> overlaps = Physics2D.OverlapBoxAll(point: transform.position,
                                                            size: new Vector2(transform.lossyScale.x / 2.0f,
                                                                              transform.lossyScale.y * Constants.HIT_JUDGE_RANGE_BAD),
                                                            angle: 0.0f,
                                                            layerMask: _noteLayer).ToList();
        if (overlaps.Count > 0)
        {
            List<Collider2D> overlapsOrdered = overlaps.OrderBy(x => Mathf.Abs(x.transform.position.y - transform.position.y)).ToList();

            float distance = Mathf.Abs(overlapsOrdered[0].transform.position.y - transform.position.y);
            if      (distance < Constants.HIT_JUDGE_RANGE_COOL)     hitType = HitType.Cool;
            else if (distance < Constants.HIT_JUDGE_RANGE_GREAT)    hitType = HitType.Great;
            else if (distance < Constants.HIT_JUDGE_RANGE_GOOD)     hitType = HitType.Good;
            else if (distance < Constants.HIT_JUDGE_RANGE_BAD)      hitType = HitType.Bad;

            overlapsOrdered[0].gameObject.GetComponent<Note>().Hit(hitType);
            Destroy(overlapsOrdered[0].gameObject);
        }
    }

    private void SetColorPressed()
    {
        _spriteRenderer.color = _colorPressed;
    }

    private void SetColorOrigin()
    {
        _spriteRenderer.color = _colorOrigin;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2.0f,
                                                            transform.lossyScale.y * Constants.HIT_JUDGE_RANGE_MISS,
                                                            0.0f));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2.0f,
                                                            transform.lossyScale.y * Constants.HIT_JUDGE_RANGE_BAD,
                                                            0.0f));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2.0f,
                                                            transform.lossyScale.y * Constants.HIT_JUDGE_RANGE_GOOD,
                                                            0.0f));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2.0f,
                                                            transform.lossyScale.y * Constants.HIT_JUDGE_RANGE_GREAT,
                                                            0.0f));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2.0f,
                                                            transform.lossyScale.y * Constants.HIT_JUDGE_RANGE_COOL,
                                                            0.0f));
    }
}
