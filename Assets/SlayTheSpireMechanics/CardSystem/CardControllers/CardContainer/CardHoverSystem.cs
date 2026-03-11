using System;
using System.Collections.Generic;
using Assets.SlayTheSpireMechanics.GameLogic.PlayerSystem;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using SlayTheSpireMechanics.VisualLogic.Card;
using SlayTheSpireMechanics.VisualLogic.Enemies;
using UnityEngine;


namespace SlayTheSpireMechanics.VisualLogic.CardContainer
{
    public class CardHoverSystem : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField] private CardAnimator cardAnimator;
        [SerializeField] private Player player;


        [Header("Flags")] 

        [SerializeField] private bool blockDragging;
        [SerializeField] private bool blockHovering;
        [SerializeField] private bool isGlobalActive;
        [SerializeField] private bool cardActiveNow;


        public bool BlockDragging => blockDragging;

        
        [SerializeField] private CardView _hoveredCard;
        [SerializeField] private CardView _draggingCard;

        private Vector3 _startDragPosition;



        private event Action<CardView> onCardPlayed;
        public event Action<CardView> onCardHovered;
        public event Action<CardView> onCardDragging;



        public Action cardActiveEnter;
        public Action cardActiveExit;

        
        private LayerMask _mask;
        
        public void Init(CardAnimator animator, Player player)
        {
            _mask = LayerMask.GetMask("Targetable");
            this.player = player;
            cardAnimator = animator;
        }
        


        public void OnMouseEnt(CardView cardView)
        {
            if (blockHovering || !cardView.canBeHovered || _hoveredCard != null){return;}
            SetHover(cardView);
        }

        public void OnMouseExit(CardView cardView)
        {
            if (blockHovering || !cardView.canBeHovered){return;}
            
            ResetHover();
        }

        public void ResetHover()
        {
            if (_hoveredCard != null)
            {
                CardView cv = _hoveredCard;
                _hoveredCard = null;

                cv.canBeHovered = false;
                cardAnimator.PlayReturnToSpline(cv, () => cv.canBeHovered = true);
            }

            if (_draggingCard != null)
            {
                CardView cv = _draggingCard;
                _draggingCard = null;

                cv.canBeHovered = false; 
                cv.canBeDragged = false;
                cardAnimator.PlayReturnToSpline(cv, () => 
                {
                  cv.canBeHovered = true;
                  cv.canBeDragged = true; 
                });

            }
        }
        

        
        private void SetHover(CardView cardView)
        {
            if (_hoveredCard != null) {return;}
            if (_hoveredCard == cardView) {return;}

            _hoveredCard = cardView;
            cardView.canBeDragged = true;
            cardAnimator.PlayHover(cardView, null);
        }

        public void DragStart(CardView cardView)
        {
            if (!cardView.canBeDragged) {return;}
            blockHovering = true;
            _draggingCard = cardView;
            _startDragPosition = cardView.Wrapper.position;
            
            cardAnimator.PlayDragStartRotation(cardView, null);
            
        }

        public void DragContinue(CardView cardView)
        {
            if (_draggingCard == null || blockDragging || !cardView.canBeDragged) {return;}
            
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;


            if (!cardView.CardInfo.Action.IsGlobal)
            {
                if (Mathf.Abs(worldPos.y - _startDragPosition.y) <= 5f)
                {
                    cardView.Wrapper.position = worldPos;
                }
                else
                {
                    SendCardToActiveState(cardView);
                }
            }
            else
            {
                cardView.Wrapper.position = worldPos;
                if (Mathf.Abs(worldPos.y - _startDragPosition.y) >= 10f)
                {
                    isGlobalActive = true;
                }
                else
                {
                    isGlobalActive = false;
                }
            }
        }
        public void DragEnd(CardView cardView)
        {
            if (!cardView.canBeDragged) { return; }
            cardActiveExit?.Invoke();
            blockDragging = false;
            blockHovering = false;
            cardView.canBeHovered = false;
            cardView.canBeDragged = false;
            if (isGlobalActive) 
            {
                isGlobalActive = false;
                if (player.CardModelContainer.TryPlayCard(_draggingCard.CardInfo))
                {
                    return;
                } 
            }
            else
            {
                if (RayTry()) 
                {
                    return; 
                }
            }
            cardView.canBeHovered = true;
            cardView.canBeDragged = true;
            ResetHover();
        }

        public void SendCardToActiveState(CardView cardView)
        {
            cardAnimator.PlayMoveToActiveSlot(cardView, cardActiveEnter);
            blockDragging = true;
            blockHovering = true;
        }



        public bool RayTry()
        {
            if (_draggingCard == null) {return false;}
            if (_draggingCard.CardInfo.Action.IsGlobal) {return false;}
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _mask))
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy == null) {return false;}
                Debug.Log(enemy.gameObject.name);
                _draggingCard.CardInfo.Action.Target = enemy;
                return player.CardModelContainer.TryPlayCard(_draggingCard.CardInfo);
            }
            return false;
        }

        
        
        
        
        
    }
}