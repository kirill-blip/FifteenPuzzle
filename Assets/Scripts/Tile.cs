using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace FifteenPuzzle
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _numberText = null;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _time = .25f;
        [SerializeField] private float _timeToWait = .05f;

        private List<Vector2> directions = new()
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };
        private List<Vector3> origins = new()
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
            for (int i = 0; i < directions.Count; i++)
            {
                if (CheckIfCanMove(origins[i], directions[i]))
                {
                    Vector3 position = transform.position + (Vector3)directions[i];
                    gameObject.LeanMove(position, _time).setEaseOutExpo();
                    
                    StartCoroutine(Wait());
                }
            }

        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(_timeToWait);
            Clicked?.Invoke();
        }

        private bool CheckIfCanMove(Vector3 origin, Vector2 direction)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + origin, direction, .5f, _layerMask);

            if (raycast.transform == null)
                return true;

            return false;
        }
    }
}