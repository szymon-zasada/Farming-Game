using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }
    public Tile SelectedTile { get; set; }
    public Item SelectedItem { get; set; }

    [SerializeField] private TileInteractivePanel _tileInteractivePanel;

    [SerializeField] public EventSystem EventSystem { get; private set; }

    private void Start()
    {
        _tileInteractivePanel.gameObject.SetActive(false);
        EventSystem = FindObjectOfType<EventSystem>();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Reset()
    {
        SelectedTile = null;
        SelectedItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent<Tile>(out var tile))
                {
                    SelectedTile = tile;
                    UseItem();

                    TileInteractivePanel();
                }
                else
                {
                    SelectedTile = null;
                    ResetSelectedItem();
                }
            }
        }
    }


    private void UseItem()
    {
        if (SelectedItem == null)
            return;




        SelectedItem.Use();
        if(SelectedItem is IMultipleUses multipleUsesItem)
        {
            if (multipleUsesItem.Uses <= 0)
            {
                ResetSelectedItem();
            }
            return;
        }
    }


    private void PlaceItem()
    {
        if (SelectedItem == null)
            return;

        if (SelectedTile is not ISolidBlock solidBlock)
            throw new System.InvalidOperationException("You can't place that here!");

        if (SelectedTile is not ICanPlaceOn tile)
            throw new System.InvalidOperationException("You can't place that here!");

        if (SelectedItem is not IPlaceable placeable)
            throw new System.InvalidOperationException("You can't place that here!");
        
        placeable.Place();
    }

    public void ResetSelectedItem()
    {
        SelectedItem = null;
        EventSystem.SetSelectedGameObject(null);
    }

    private void TileInteractivePanel()
    {
        if (SelectedTile is IFertile tile)
        {
            _tileInteractivePanel.gameObject.SetActive(true);
            _tileInteractivePanel.SetItem(SelectedTile);
        }
        else
        {
            _tileInteractivePanel.gameObject.SetActive(false);
            SelectedItem = null;
        }
    }
}
