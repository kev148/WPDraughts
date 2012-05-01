namespace WPDraughts

{
    public enum PlayerColours { White, Black };
    public enum MoveDirections { TopLeft, TopRight, BottomLeft, BottomRight };
    public enum Difficulty { Hard, Normal, Easy };
    public enum GameStates { TitleScreen, SelectDifficultyScreen, Playing, PieceSelected, NextJump, GameOver};
    public enum PlayerTurn { WhiteTurn, BlackTurn };
    public enum PlayerTypes { Human, Computer };
    public enum GameType { TwoPlayer, OnePlayer };
}
