using UnityEngine;


public class ObjectManager : MonoBehaviour
{
    void Awake()
    {
        _mainCharacter = new MainCharacter(Def.INIT_AGE, Def.INIT_MONEY);

        // story
        _storyMode = new Story.Mode();

        // nurture
        _nurtureMode = new Nurture.Mode(_mainCharacter);

        // board game
        _boardGameMode = new BoardGame.BoardGameMode();

        // combat
        _combatMode = new Combat.CombatMode();
    }


    #region Field, Property
    
    private MainCharacter _mainCharacter = null;
    public MainCharacter MainCharacter { get { return _mainCharacter; } }

    /***** Story *****/
    private Story.Mode _storyMode = null;
    public Story.Mode StoryMode { get { return _storyMode; } }

    /***** Nurture *****/
    private Nurture.Mode _nurtureMode = null;
    public Nurture.Mode NurtureMode { get { return _nurtureMode; } }

    /***** BoardGame *****/
    private BoardGame.BoardGameMode _boardGameMode = null;
    public BoardGame.BoardGameMode BoardGameMode { get { return _boardGameMode; } }
    
    /***** Combat *****/
    private Combat.CombatMode _combatMode = null;
    public Combat.CombatMode CombatMode { get { return _combatMode; } }

    #endregion  // Field, Property
}