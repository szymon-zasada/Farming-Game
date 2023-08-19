using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }
    public Tile SelectedTile { get; set; }
    public Item SelectedItem { get; set; }

    [SerializeField] private TileInteractivePanel _tileInteractivePanel;

    private void Start()
    {
        _tileInteractivePanel.gameObject.SetActive(false);
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
                if(hit.collider.TryGetComponent<Tile>(out var tile))
                {
                    SelectedTile = tile;
                    UseItem();

                    TileInteractivePanel();
                }
                else
                {
                    SelectedTile = null;
                }
            }
        }
    }


    private void UseItem()
    {
        if(SelectedItem == null)
            return;

        


        SelectedItem.Use();
        if(SelectedItem.Uses <= 0)
        {
            SelectedItem = null;
        }
    }

    private void TileInteractivePanel()
    {
        if(SelectedTile is IFertile tile)
        {
            _tileInteractivePanel.gameObject.SetActive(true);
            _tileInteractivePanel.SetItem(SelectedTile);
        }
        else
        {
            _tileInteractivePanel.gameObject.SetActive(false);
        }
    }
}
