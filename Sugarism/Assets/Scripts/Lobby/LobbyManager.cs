using UnityEngine;


// prohibit from abusing Singleton.
// @IMPORTANT : prohibit from re-define constuctor because of inheriting MonoBehaviour.
public class LobbyManager : MonoBehaviour
{
    private static LobbyManager _instance = null;
    public static LobbyManager Instance { get { return _instance; } }

    /********* Editor Interface *********/
    // Prefabs
    public PlayerInitProperty PrefPlayerInitProperty;
    public LobbyUIManager PrefLobbyUIManager;

    /********* Game Interface *********/
    //
    private PlayerInitProperty _playerInitProperty = null;
    public PlayerInitProperty PlayerInitProperty { get { return _playerInitProperty; } }

    private LobbyUIManager _ui = null;
    public LobbyUIManager UI { get { return _ui; } }

    //
    void Awake()
    {
        _instance = this;

        //
        _playerInitProperty = Instantiate(PrefPlayerInitProperty);
        DontDestroyOnLoad(_playerInitProperty);

        _ui = Instantiate(PrefLobbyUIManager);
    }

}   // class
