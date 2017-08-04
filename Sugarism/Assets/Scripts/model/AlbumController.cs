
public class AlbumController
{
    /*** Album ID = Album Classification ***/
    // @note: TargetId (0~#)
    public const int ETC_ALBUM_ID = -1;
    public const int MAIN_CHARACTER_ALBUM_ID = -2;

    public const int MAX_ALBUM_ID = -3;

    //
    public enum EAlbumType
    {
        PICTURE = 0,
        MINI_PICTURE,
        NURTURE_ENDING,
        VACATION,

        MAX
    }

    //
    public static bool IsValid(int albumId)
    {
        if (ETC_ALBUM_ID == albumId)
            return true;
        else if (MAIN_CHARACTER_ALBUM_ID == albumId)
            return true;
        else
            return isValidTarget(albumId);
    }


    public static string GetName(int albumId)
    {
        if (ETC_ALBUM_ID == albumId)
        {
            return Def.ETC;
        }
        else if (MAIN_CHARACTER_ALBUM_ID == albumId)
        {
            return Def.DEFAULT_PLAYER_NAME;
        }
        else if (isValidTarget(albumId))
        {
            Target t = LobbyManager.Instance.DT.Target[albumId];
            Character c = LobbyManager.Instance.DT.Character[t.characterId];
            return c.name;
        }
        else
        {
            return string.Empty;
        }
    }
    
    public static string GetAlbumTypeName(EAlbumType albumType)
    {
        switch(albumType)
        {
            case EAlbumType.PICTURE:
                return Def.ALBUM_TYPE_NAME_PICTURE;

            case EAlbumType.MINI_PICTURE:
                return Def.ALBUM_TYPE_NAME_MINIPICTURE;

            case EAlbumType.NURTURE_ENDING:
                return Def.ALBUM_TYPE_NAME_NURTURE_ENDING;

            case EAlbumType.VACATION:
                return Def.ALBUM_TYPE_NAME_VACTION;

            default:
                return string.Empty;
        }
    }


    //
    private static bool isValidTarget(int id)
    {
        if (id < 0)
            return false;
        else if (id >= LobbyManager.Instance.DT.Target.Count)
            return false;
        else
            return true;
    }
}
