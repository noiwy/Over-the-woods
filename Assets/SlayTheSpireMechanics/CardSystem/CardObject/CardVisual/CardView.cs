using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace SlayTheSpireMechanics.VisualLogic
{
    public class CardView : MonoBehaviour
    {
        [SerializeField]private SpriteRenderer _icon;
        [SerializeField]private TMP_Text _titleText;
        [SerializeField]private TMP_Text _descriptionText;
        [SerializeField]private TMP_Text _costText;
        [SerializeField]private Transform _wrapper;
        [SerializeField]private CardModel _cardInfo;
        [SerializeField]private SortingGroup _sortingGroup;
        public CardTweens _cardTweens;
        public CardTweens CardTweens => _cardTweens;
        public Transform Wrapper => _wrapper;
        public CardModel CardInfo => _cardInfo;
        
        
        private int _sortOrder;
        public int SortOrder
        {
            get {return _sortOrder;}
            set
            {
                _sortOrder = value;
                _sortingGroup.sortingOrder = _sortOrder;
            }
        }
        public bool isAnimationPlaying = false;
        
        public bool canBeDragged = true;
        public bool canBeHovered = true;


        public Action<CardView> OnEnter;
        public Action<CardView> OnExit;
        public Action<CardView> DragStart;
        public Action<CardView> DragContinue;
        public Action<CardView>  DragEnd;
        



        
        public void Init(CardModel card)
        {
            _cardInfo = card;

            UpdateData();
        }

        public void UpdateData()
        {
            if (_cardInfo == null) {throw new Exception("Card Model is null");}
            
            
            _icon.sprite = _cardInfo.Sprite;
            _titleText.text = _cardInfo.Title;
            _descriptionText.text = _cardInfo.Description;
            _costText.text = _cardInfo.Cost.ToString();
        }

        public void TriggerAction()
        {
            ActionSystem.Instance.AddActionToQueue(CardInfo.Action);
        }
        private void OnMouseDown()
        {
            DragStart?.Invoke(this);
        }
        private void OnMouseUp()
        {
            DragEnd?.Invoke(this);
        }

        private void OnMouseDrag()
        {
            DragContinue?.Invoke(this);
        }

        private void OnMouseEnter()
        {
            OnEnter?.Invoke(this);
        }
        private void OnMouseExit()
        {
            OnExit?.Invoke(this);
        }

        public void OnDestroy()
        {
            Wrapper.DOKill();
        }
    }
    public struct CardTweens
    {
        public Tween rotationTween;
        public Tween scaleTween;
        public Tween moveTween;
    }
}