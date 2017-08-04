using UnityEngine;


public class AlbumCGPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    [SerializeField]
    private AlbumFullCGPanel PrefFullCGPanel = null;
    [SerializeField]
    private AlbumMiniCGPanel PrefMiniCGPanel = null;

    //
    private AlbumFullCGPanel _fullCGPanel = null;
    private AlbumMiniCGPanel _miniCGPanel = null;


    //
    void Awake()
    {
        _fullCGPanel = Instantiate(PrefFullCGPanel);
        _fullCGPanel.transform.SetParent(transform, false);
        _fullCGPanel.Hide();

        _miniCGPanel = Instantiate(PrefMiniCGPanel);
        _miniCGPanel.transform.SetParent(transform, false);
        _miniCGPanel.Hide();
    }


    public void ShowFullCG(Sprite s)
    {
        _fullCGPanel.Show(s);
    }

    public void ShowMiniCG(Sprite s)
    {
        _miniCGPanel.Show(s);
    }

}   // class
