namespace ChessDotNet
{
    public enum GameEvent
    {
        Check,
        Checkmate,
        Stalemate,
        Draw,
        Custom,
        Resign,
        VariantEnd, // to be used for chess variants, which can be derived from ChessGame
        None
    }
}
