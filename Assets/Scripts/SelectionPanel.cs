using UnityEngine;

public class SelectionPanel : MonoBehaviour
{
    [SerializeField] private ItemData[] _itemDatas;
    [SerializeField] private Transform _container;
    [SerializeField] private ObjectPlacer _objectPlacer;
    [SerializeField] private GameObject _itemTemplate;

    private void Start()
    {
        for(int i = 0; i < _itemDatas.Length; i++)
            AddItem(_itemDatas[i]);
    }

    private void AddItem(ItemData itemData)
    {
        Instantiate(_itemTemplate, _container).TryGetComponent(out ItemView itemView);
        itemView.Initialize(itemData);
        itemView.ItemSelected += OnItemSelected;
        itemView.ItemDisabled += OnItemDisabled;
    }

    private void OnItemSelected(ItemData itemData)
    {
        _objectPlacer.SetInstallObject(itemData);
    }

    private void OnItemDisabled(ItemView itemView)
    {
        itemView.ItemSelected -= OnItemSelected;
        itemView.ItemDisabled -= OnItemDisabled;
    }
}
