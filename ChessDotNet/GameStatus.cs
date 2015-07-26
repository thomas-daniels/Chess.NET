namespace ChessDotNet
{
    public class GameStatus
    {
        public enum Events
        {
            Check,
            Checkmate,
            Stalemate,
            Draw,
            Custom,
            Resign,
            VariantEnd, // to be used for chess variants, which can be derived from ChessBoard
            None
        }
        
        public Events Event
        {
            get;
            private set;
        }

        public Players PlayerWhoCausedEvent
        {
            get;
            private set;
        }

        public string EventExplanation
        {
            get;
            private set;
        }

        public GameStatus(Events _event, Players whoCausedEvent, string eventExplanation)
        {
            Event = _event;
            PlayerWhoCausedEvent = whoCausedEvent;
            EventExplanation = eventExplanation;
        }
    }
}
