using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FifteenPuzzle
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _numberText = null;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _time = .25f;
        [SerializeField] private float _timeToWait = .05f;

        private List<Vector2> _directions = new()
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };
        private List<Vector3> _origins = new()
        {
            new Vector3(0, 0.5125f),
            new Vector3(0, -0.5125f),
            new Vector3(-0.5125f, 0),
            new Vector3(0.5125f, 0),
        };

        public System.Action Clicked;

        public void SetNumber(int number)
        {
            _numberText.text = number.ToString();
        }

        public string GetNumberText()
        {
            return _numberText.text;
        }

        public void CreateNumberText()
        {
            _numberText = gameObject.AddComponent<TextMeshPro>();
        }

        private void OnMouseDown()
        {
            Move();
        }

        private void Move()
        {
            for (int i = 0; i < _directions.Count; i++)
            {
                if (CheckIfCanMove(_origins[i], _directions[i]))
                {
                    Vector3 position = transform.position + (Vector3)_directions[i];
                    gameObject.LeanMove(position, _time).setEaseOutExpo();

                    StartCoroutine(Wait());

                    return;
                }
            }
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(_timeToWait);
            Clicked?.Invoke();
        }

        public bool CheckIfCanMove(Vector3 origin, Vector2 direction)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + origin, direction, .5f, _layerMask);

            if (raycast.transform == null)
            {
                return true;
            }

            return false;
        }
    }
}