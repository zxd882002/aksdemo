using System.Collections.Generic;

namespace WeatherForecastAPI.Models.GoBang
{
    public class GoBangChessGroupDefinitionCollection
    {
        public static readonly GoBangChessGroupDefinition BlackMoreThanFiveChessGroup = new GoBangChessGroupDefinition
        {
            DefinitionId = 1,
            Type = GoBangChessGroupType.MoreThanFive,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess
            },
            Score = 5000000,
            AlreadyWin = true,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackFiveChessGroup = new GoBangChessGroupDefinition
        {
            DefinitionId = 2,
            Type = GoBangChessGroupType.Five,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess
            },
            Score = 5000000,
            AlreadyWin = true,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackLiveFourGroup = new GoBangChessGroupDefinition
        {
            DefinitionId = 3,
            Type = GoBangChessGroupType.LiveFour,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            },
            Score = 100000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) =>
            {
                List<(int row, int column)> followedPoint = new List<(int row, int column)>();
                if (fromRow == toRow)
                {
                    if (fromColumn > 0 && board.Board[fromRow, fromColumn - 1] == GoBangChessType.Blank) followedPoint.Add((fromRow, fromColumn - 1));
                    if (toColumn < 14 && board.Board[toRow, toColumn + 1] == GoBangChessType.Blank) followedPoint.Add((toRow, toColumn + 1));
                }
                else if (fromColumn == toColumn)
                {
                    if (fromRow > 0 && board.Board[fromRow - 1, fromColumn] == GoBangChessType.Blank) followedPoint.Add((fromRow - 1, fromColumn));
                    if (toRow < 14 && board.Board[toRow + 1, toColumn] == GoBangChessType.Blank) followedPoint.Add((toRow + 1, toColumn));
                }
                else
                {
                    if (fromColumn > 0 && fromRow > 0 && board.Board[fromRow - 1, fromColumn - 1] == GoBangChessType.Blank) followedPoint.Add((fromRow - 1, fromColumn - 1));
                    if (toColumn < 14 && toRow < 14 && board.Board[fromRow + 1, fromColumn + 1] == GoBangChessType.Blank) followedPoint.Add((fromRow + 1, fromColumn + 1));
                }

                return followedPoint;
            },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadFourGroup1 = new GoBangChessGroupDefinition
        {
            DefinitionId = 4,
            Type = GoBangChessGroupType.DeadFour,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            },
            Score = 10000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) =>
            {
                List<(int row, int column)> followedPoint = new List<(int row, int column)>();
                followedPoint.Add((fromRow, fromColumn));
                return followedPoint;
            },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadFourGroup1Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 5,
            Type = GoBangChessGroupType.DeadFour,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            },
            Score = 10000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) =>
            {
                List<(int row, int column)> followedPoint = new List<(int row, int column)>();
                followedPoint.Add((toRow, toColumn));
                return followedPoint;
            },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadFourGroup2 = new GoBangChessGroupDefinition
        {
            DefinitionId = 6,
            Type = GoBangChessGroupType.DeadFour,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
            },
            Score = 10000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) =>
            {
                List<(int row, int column)> followedPoint = new List<(int row, int column)>();
                if (fromRow == toRow)
                {
                    followedPoint.Add((fromRow, fromColumn + 1));
                }
                else if (fromColumn == toColumn)
                {
                    followedPoint.Add((fromRow + 1, fromColumn));
                }
                else
                {
                    followedPoint.Add((fromRow + 1, fromColumn + 1));
                }
                return followedPoint;
            },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadFourGroup2Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 7,
            Type = GoBangChessGroupType.DeadFour,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
            },
            Score = 10000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) =>
            {
                List<(int row, int column)> followedPoint = new List<(int row, int column)>();
                if (fromRow == toRow)
                {
                    followedPoint.Add((toRow, toColumn - 1));
                }
                else if (fromColumn == toColumn)
                {
                    followedPoint.Add((toRow - 1, toColumn));
                }
                else
                {
                    followedPoint.Add((toRow - 1, toRow - 1));
                }
                return followedPoint;
            },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadFourGroup3 = new GoBangChessGroupDefinition
        {
            DefinitionId = 8,
            Type = GoBangChessGroupType.DeadFour,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
            },
            Score = 10000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) =>
            {
                List<(int row, int column)> followedPoint = new List<(int row, int column)>();
                if (fromRow == toRow)
                {
                    followedPoint.Add((fromRow, fromColumn + 2));
                }
                else if (fromColumn == toColumn)
                {
                    followedPoint.Add((fromRow + 2, fromColumn));
                }
                else
                {
                    followedPoint.Add((fromRow + 2, fromColumn + 2));
                }
                return followedPoint;
            },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackLiveThreeGroup1 = new GoBangChessGroupDefinition
        {
            DefinitionId = 9,
            Type = GoBangChessGroupType.LiveThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            },
            Score = 8000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) =>
            {
                List<(int row, int column)> followedPoint = new List<(int row, int column)>();
                if (fromRow == toRow)
                {
                    followedPoint.Add((fromRow, fromColumn + 1));
                    followedPoint.Add((toRow, toColumn - 1));
                }
                else if (fromColumn == toColumn)
                {
                    followedPoint.Add((fromRow + 1, fromColumn));
                    followedPoint.Add((toRow - 1, toColumn));
                }
                else
                {
                    followedPoint.Add((fromRow + 1, fromColumn + 1));
                    followedPoint.Add((toRow - 1, toColumn - 1));
                }
                return followedPoint;
            },
            CouldFollowByAddingFourChess = true
        };
        public static readonly GoBangChessGroupDefinition BlackLiveThreeGroup2 = new GoBangChessGroupDefinition
        {
            DefinitionId = 10,
            Type = GoBangChessGroupType.LiveThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            },
            Score = 8000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) =>
            {
                List<(int row, int column)> followedPoint = new List<(int row, int column)>();
                if (fromRow == toRow)
                {
                    followedPoint.Add((fromRow, fromColumn + 1));
                    followedPoint.Add((toRow, toColumn - 1));
                }
                else if (fromColumn == toColumn)
                {
                    followedPoint.Add((fromRow + 1, fromColumn));
                    followedPoint.Add((toRow - 1, toColumn));
                }
                else
                {
                    followedPoint.Add((fromRow + 1, fromColumn + 1));
                    followedPoint.Add((toRow - 1, toColumn - 1));
                }
                followedPoint.Add((toRow, toColumn));
                return followedPoint;
            },
            CouldFollowByAddingFourChess = true
        };
        public static readonly GoBangChessGroupDefinition BlackLiveThreeGroup2Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 11,
            Type = GoBangChessGroupType.LiveThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.WhiteChess,
            },
            Score = 8000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) =>
            {
                List<(int row, int column)> followedPoint = new List<(int row, int column)>();
                if (fromRow == toRow)
                {
                    followedPoint.Add((fromRow, fromColumn + 1));
                    followedPoint.Add((toRow, toColumn - 1));
                }
                else if (fromColumn == toColumn)
                {
                    followedPoint.Add((fromRow + 1, fromColumn));
                    followedPoint.Add((toRow - 1, toColumn));
                }
                else
                {
                    followedPoint.Add((fromRow + 1, fromColumn + 1));
                    followedPoint.Add((toRow - 1, toColumn - 1));
                }
                followedPoint.Add((fromRow, fromColumn));
                return followedPoint;
            },
            CouldFollowByAddingFourChess = true
        };
        public static readonly GoBangChessGroupDefinition BlackJumpLiveThreeGroup = new GoBangChessGroupDefinition
        {
            DefinitionId = 12,
            Type = GoBangChessGroupType.JumpLiveThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            },
            Score = 7000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) =>
            {
                List<(int row, int column)> followedPoint = new List<(int row, int column)>();
                if (fromRow == toRow)
                {
                    followedPoint.Add((fromRow, fromColumn));
                    followedPoint.Add((fromRow, fromColumn + 2));
                    followedPoint.Add((toRow, toColumn));
                }
                else if (fromColumn == toColumn)
                {
                    followedPoint.Add((fromRow, fromColumn));
                    followedPoint.Add((fromRow + 2, fromColumn));
                    followedPoint.Add((toRow, toColumn));
                }
                else
                {
                    followedPoint.Add((fromRow, fromColumn));
                    followedPoint.Add((fromRow + 2, fromColumn + 2));
                    followedPoint.Add((toRow, toColumn));
                }
                return followedPoint;
            },
            CouldFollowByAddingFourChess = true
        };
        public static readonly GoBangChessGroupDefinition BlackJumpLiveThreeGroupMirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 13,
            Type = GoBangChessGroupType.JumpLiveThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            },
            Score = 7000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) =>
            {
                List<(int row, int column)> followedPoint = new List<(int row, int column)>();
                if (fromRow == toRow)
                {
                    followedPoint.Add((fromRow, fromColumn));
                    followedPoint.Add((toRow, toColumn - 2));
                    followedPoint.Add((toRow, toColumn));
                }
                else if (fromColumn == toColumn)
                {
                    followedPoint.Add((fromRow, fromColumn));
                    followedPoint.Add((toRow - 2, toColumn));
                    followedPoint.Add((toRow, toColumn));
                }
                else
                {
                    followedPoint.Add((fromRow, fromColumn));
                    followedPoint.Add((toRow - 2, toColumn - 2));
                    followedPoint.Add((toRow, toColumn));
                }
                return followedPoint;
            },
            CouldFollowByAddingFourChess = true
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup1 = new GoBangChessGroupDefinition
        {
            DefinitionId = 14,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            },
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup1Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 15,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            },
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup2 = new GoBangChessGroupDefinition
        {
            DefinitionId = 16,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            },
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup2Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 17,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            },
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup3 = new GoBangChessGroupDefinition
        {
            DefinitionId = 18,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            },
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup3Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 19,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            },
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup4 = new GoBangChessGroupDefinition
        {
            DefinitionId = 20,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
            },
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup4Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 21,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
            },
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup5 = new GoBangChessGroupDefinition
        {
            DefinitionId = 22,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
            },
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup6 = new GoBangChessGroupDefinition
        {
            DefinitionId = 23,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.WhiteChess,
            },
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackLiveTwoGroup1 = new GoBangChessGroupDefinition
        {
            DefinitionId = 24,
            Type = GoBangChessGroupType.LiveTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            },
            Score = 50,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackLiveTwoGroup2 = new GoBangChessGroupDefinition
        {
            DefinitionId = 25,
            Type = GoBangChessGroupType.LiveTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.WhiteChess,
            },
            Score = 50,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackLiveTwoGroup2Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 26,
            Type = GoBangChessGroupType.LiveTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            },
            Score = 50,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackLiveTwoGroup3 = new GoBangChessGroupDefinition
        {
            DefinitionId = 27,
            Type = GoBangChessGroupType.LiveTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            },
            Score = 50,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackLiveTwoGroup4 = new GoBangChessGroupDefinition
        {
            DefinitionId = 28,
            Type = GoBangChessGroupType.LiveTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            },
            Score = 50,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup1 = new GoBangChessGroupDefinition
        {
            DefinitionId = 29,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            },
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup1Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 30,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            },
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup2 = new GoBangChessGroupDefinition
        {
            DefinitionId = 31,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            },
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup2Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 32,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            },
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup3 = new GoBangChessGroupDefinition
        {
            DefinitionId = 33,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            },
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup3Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 34,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            },
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup4 = new GoBangChessGroupDefinition
        {
            DefinitionId = 35,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new GoBangChessType[] {
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess
            },
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPointFuncs = (fromRow, fromColumn, toRow, toColumn, board) => { return new List<(int row, int column)>(); },
            CouldFollowByAddingFourChess = false
        };

        public static readonly GoBangChessGroupDefinition WhiteMoreThanFiveChessGroup = BlackMoreThanFiveChessGroup.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteFiveChessGroup = BlackFiveChessGroup.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteLiveFourGroup = BlackLiveFourGroup.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadFourGroup1 = BlackDeadFourGroup1.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadFourGroup1Mirror = BlackDeadFourGroup1Mirror.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadFourGroup2 = BlackDeadFourGroup2.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadFourGroup2Mirror = BlackDeadFourGroup2Mirror.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadFourGroup3 = BlackDeadFourGroup3.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteLiveThreeGroup1 = BlackLiveThreeGroup1.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteLiveThreeGroup2 = BlackLiveThreeGroup2.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteLiveThreeGroup2Mirror = BlackLiveThreeGroup2Mirror.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteJumpLiveThreeGroup = BlackJumpLiveThreeGroup.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteJumpLiveThreeGroupMirror = BlackJumpLiveThreeGroupMirror.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadThreeGroup1 = BlackDeadThreeGroup1.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadThreeGroup1Mirror = BlackDeadThreeGroup1Mirror.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadThreeGroup2 = BlackDeadThreeGroup2.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadThreeGroup2Mirror = BlackDeadThreeGroup2Mirror.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadThreeGroup3 = BlackDeadThreeGroup3.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadThreeGroup3Mirror = BlackDeadThreeGroup3Mirror.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadThreeGroup4 = BlackDeadThreeGroup4.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadThreeGroup4Mirror = BlackDeadThreeGroup4Mirror.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadThreeGroup5 = BlackDeadThreeGroup5.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadThreeGroup6 = BlackDeadThreeGroup6.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteLiveTwoGroup1 = BlackLiveTwoGroup1.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteLiveTwoGroup2 = BlackLiveTwoGroup2.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteLiveTwoGroup2Mirror = BlackLiveTwoGroup2Mirror.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteLiveTwoGroup3 = BlackLiveTwoGroup3.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteLiveTwoGroup4 = BlackLiveTwoGroup4.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadTwoGroup1 = BlackDeadTwoGroup1.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadTwoGroup1Mirror = BlackDeadTwoGroup1Mirror.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadTwoGroup2 = BlackDeadTwoGroup2.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadTwoGroup2Mirror = BlackDeadTwoGroup2Mirror.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadTwoGroup3 = BlackDeadTwoGroup3.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadTwoGroup3Mirror = BlackDeadTwoGroup3Mirror.ReverseGoBangChessGroup();
        public static readonly GoBangChessGroupDefinition WhiteDeadTwoGroup4 = BlackDeadTwoGroup4.ReverseGoBangChessGroup();

        public static readonly GoBangChessGroupDefinition[] AllBlack = new GoBangChessGroupDefinition[]
        {
            BlackMoreThanFiveChessGroup,
            BlackFiveChessGroup,
            BlackLiveFourGroup,
            BlackDeadFourGroup1,
            BlackDeadFourGroup1Mirror,
            BlackDeadFourGroup2,
            BlackDeadFourGroup2Mirror,
            BlackDeadFourGroup3,
            BlackLiveThreeGroup1,
            BlackLiveThreeGroup2,
            BlackLiveThreeGroup2Mirror,
            BlackJumpLiveThreeGroup,
            BlackJumpLiveThreeGroupMirror,
            BlackDeadThreeGroup1,
            BlackDeadThreeGroup1Mirror,
            BlackDeadThreeGroup2,
            BlackDeadThreeGroup2Mirror,
            BlackDeadThreeGroup3,
            BlackDeadThreeGroup3Mirror,
            BlackDeadThreeGroup4,
            BlackDeadThreeGroup4Mirror,
            BlackDeadThreeGroup5,
            BlackDeadThreeGroup6,
            BlackLiveTwoGroup1,
            BlackLiveTwoGroup2,
            BlackLiveTwoGroup2Mirror,
            BlackLiveTwoGroup3,
            BlackLiveTwoGroup4,
            BlackDeadTwoGroup1,
            BlackDeadTwoGroup1Mirror,
            BlackDeadTwoGroup2,
            BlackDeadTwoGroup2Mirror,
            BlackDeadTwoGroup3,
            BlackDeadTwoGroup3Mirror,
            BlackDeadTwoGroup4
        };

        public static readonly GoBangChessGroupDefinition[] AllWhite = new GoBangChessGroupDefinition[]
        {
           WhiteMoreThanFiveChessGroup,
           WhiteFiveChessGroup,
           WhiteLiveFourGroup,
           WhiteDeadFourGroup1,
           WhiteDeadFourGroup1Mirror,
           WhiteDeadFourGroup2,
           WhiteDeadFourGroup2Mirror,
           WhiteDeadFourGroup3,
           WhiteLiveThreeGroup1,
           WhiteLiveThreeGroup2,
           WhiteLiveThreeGroup2Mirror,
           WhiteJumpLiveThreeGroup,
           WhiteJumpLiveThreeGroupMirror,
           WhiteDeadThreeGroup1,
           WhiteDeadThreeGroup1Mirror,
           WhiteDeadThreeGroup2,
           WhiteDeadThreeGroup2Mirror,
           WhiteDeadThreeGroup3,
           WhiteDeadThreeGroup3Mirror,
           WhiteDeadThreeGroup4,
           WhiteDeadThreeGroup4Mirror,
           WhiteDeadThreeGroup5,
           WhiteDeadThreeGroup6,
           WhiteLiveTwoGroup1,
           WhiteLiveTwoGroup2,
           WhiteLiveTwoGroup2Mirror,
           WhiteLiveTwoGroup3,
           WhiteLiveTwoGroup4,
           WhiteDeadTwoGroup1,
           WhiteDeadTwoGroup1Mirror,
           WhiteDeadTwoGroup2,
           WhiteDeadTwoGroup2Mirror,
           WhiteDeadTwoGroup3,
           WhiteDeadTwoGroup3Mirror,
           WhiteDeadTwoGroup4
        };

        private static Dictionary<int, GoBangChessGroupDefinition>? definitionIdDictionary = null;
        public static GoBangChessGroupDefinition? GetGoBangChessGroupDefinitionByDefinitionId(int definitionId)
        {
            if (definitionIdDictionary == null)
            {
                definitionIdDictionary = new Dictionary<int, GoBangChessGroupDefinition>();
                foreach (var black in AllBlack)
                {
                    definitionIdDictionary.Add(black.DefinitionId, black);
                }
                foreach (var white in AllWhite)
                {
                    definitionIdDictionary.Add(white.DefinitionId, white);
                }
            }

            if (!definitionIdDictionary.ContainsKey(definitionId))
            {
                return null;
            }
            return definitionIdDictionary[definitionId];
        }
    }
}
