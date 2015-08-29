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

    public class GameStatus
    {   
        public GameEvent Event
        {
            get;
            private set;
        }

        public Player PlayerWhoCausedEvent
        {
            get;
            private set;
        }

        public string EventExplanation
        {
            get;
            private set;
        }

        public GameStatus(GameEvent @event, Player whoCausedEvent, string eventExplanation)
        {
            Event = @event;
            PlayerWhoCausedEvent = whoCausedEvent;
            EventExplanation = eventExplanation;
        }
    }
}
