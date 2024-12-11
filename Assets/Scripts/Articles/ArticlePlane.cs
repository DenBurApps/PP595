using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Articles
{
    public class ArticlePlane : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _tag;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private ArticleData _data;

        public event Action<ArticleData> OpenButtonClicked;

        public ArticleData Data => _data;
        
        private void OnEnable()
        {
            _openButton.onClick.AddListener(OnOpenArticleClicked);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OnOpenArticleClicked);
        }

        private void Start()
        {
            _image.sprite = _data.Image;
            _tag.text = _data.Tag;
            _title.text = _data.Title;
        }

        private void OnOpenArticleClicked() => OpenButtonClicked?.Invoke(_data);
    }
    
    [Serializable]
    public class ArticleData
    {
        public string Tag;
        public string Title;
        public Sprite Image;
        public string Content;
    }
}

