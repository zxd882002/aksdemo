using System.Collections.Generic;
using System.Linq;

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
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess
            }),
            Score = 5000000,
            AlreadyWin = true,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackFiveChessGroup = new GoBangChessGroupDefinition
        {
            DefinitionId = 2,
            Type = GoBangChessGroupType.Five,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess
            }),
            Score = 5000000,
            AlreadyWin = true,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackLiveFourGroup = new GoBangChessGroupDefinition
        {
            DefinitionId = 3,
            Type = GoBangChessGroupType.LiveFour,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            }),
            Score = 100000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition> { chesslist[0].Position, chesslist[5].Position }; },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadFourGroup1 = new GoBangChessGroupDefinition
        {
            DefinitionId = 4,
            Type = GoBangChessGroupType.DeadFour,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            }),
            Score = 10000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition> { chesslist[0].Position }; },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadFourGroup1Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 5,
            Type = GoBangChessGroupType.DeadFour,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            }),
            Score = 10000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition> { chesslist[5].Position }; },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadFourGroup2 = new GoBangChessGroupDefinition
        {
            DefinitionId = 6,
            Type = GoBangChessGroupType.DeadFour,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
            }),
            Score = 10000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition> { chesslist[1].Position }; },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadFourGroup2Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 7,
            Type = GoBangChessGroupType.DeadFour,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
            }),
            Score = 10000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition> { chesslist[3].Position }; },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadFourGroup3 = new GoBangChessGroupDefinition
        {
            DefinitionId = 8,
            Type = GoBangChessGroupType.DeadFour,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
            }),
            Score = 10000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition> { chesslist[2].Position }; },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackLiveThreeGroup1 = new GoBangChessGroupDefinition
        {
            DefinitionId = 9,
            Type = GoBangChessGroupType.LiveThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            }),
            Score = 8000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition> { chesslist[1].Position, chesslist[5].Position }; },
            CouldFollowByAddingFourChess = true,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[0].Position, chesslist[1].Position, chesslist[5].Position, chesslist[6].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackLiveThreeGroup2 = new GoBangChessGroupDefinition
        {
            DefinitionId = 10,
            Type = GoBangChessGroupType.LiveThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            }),
            Score = 8000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition> { chesslist[1].Position, chesslist[5].Position, chesslist[6].Position }; },
            CouldFollowByAddingFourChess = true,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[1].Position, chesslist[5].Position, chesslist[6].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackLiveThreeGroup2Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 11,
            Type = GoBangChessGroupType.LiveThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.WhiteChess,
            }),
            Score = 8000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition> { chesslist[0].Position, chesslist[1].Position, chesslist[5].Position }; },
            CouldFollowByAddingFourChess = true,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[0].Position, chesslist[1].Position, chesslist[5].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackJumpLiveThreeGroup = new GoBangChessGroupDefinition
        {
            DefinitionId = 12,
            Type = GoBangChessGroupType.JumpLiveThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            }),
            Score = 7000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition> { chesslist[0].Position, chesslist[2].Position, chesslist[5].Position }; },
            CouldFollowByAddingFourChess = true,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[0].Position, chesslist[2].Position, chesslist[5].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackJumpLiveThreeGroupMirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 13,
            Type = GoBangChessGroupType.JumpLiveThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            }),
            Score = 7000,
            AlreadyWin = false,
            EnemyMustFollow = true,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition> { chesslist[0].Position, chesslist[3].Position, chesslist[5].Position }; },
            CouldFollowByAddingFourChess = true,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[0].Position, chesslist[3].Position, chesslist[5].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup1 = new GoBangChessGroupDefinition
        {
            DefinitionId = 14,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            }),
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[0].Position, chesslist[1].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup1Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 15,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            }),
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[4].Position, chesslist[5].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup2 = new GoBangChessGroupDefinition
        {
            DefinitionId = 16,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            }),
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[0].Position, chesslist[2].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup2Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 17,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            }),
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[3].Position, chesslist[5].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup3 = new GoBangChessGroupDefinition
        {
            DefinitionId = 18,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            }),
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[0].Position, chesslist[3].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup3Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 19,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            }),
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[2].Position, chesslist[5].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup4 = new GoBangChessGroupDefinition
        {
            DefinitionId = 20,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
            }),
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[1].Position, chesslist[2].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup4Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 21,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
            }),
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[2].Position, chesslist[3].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup5 = new GoBangChessGroupDefinition
        {
            DefinitionId = 22,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
            }),
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[1].Position, chesslist[3].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadThreeGroup6 = new GoBangChessGroupDefinition
        {
            DefinitionId = 23,
            Type = GoBangChessGroupType.DeadThree,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.WhiteChess,
            }),
            Score = 500,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition> { chesslist[1].Position, chesslist[5].Position }; },
        };
        public static readonly GoBangChessGroupDefinition BlackLiveTwoGroup1 = new GoBangChessGroupDefinition
        {
            DefinitionId = 24,
            Type = GoBangChessGroupType.LiveTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            }),
            Score = 50,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackLiveTwoGroup2 = new GoBangChessGroupDefinition
        {
            DefinitionId = 25,
            Type = GoBangChessGroupType.LiveTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.WhiteChess,
            }),
            Score = 50,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackLiveTwoGroup2Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 26,
            Type = GoBangChessGroupType.LiveTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            }),
            Score = 50,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackLiveTwoGroup3 = new GoBangChessGroupDefinition
        {
            DefinitionId = 27,
            Type = GoBangChessGroupType.LiveTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            }),
            Score = 50,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackLiveTwoGroup4 = new GoBangChessGroupDefinition
        {
            DefinitionId = 28,
            Type = GoBangChessGroupType.LiveTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            }),
            Score = 50,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup1 = new GoBangChessGroupDefinition
        {
            DefinitionId = 29,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            }),
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup1Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 30,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            }),
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup2 = new GoBangChessGroupDefinition
        {
            DefinitionId = 31,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            }),
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup2Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 32,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
            }),
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup3 = new GoBangChessGroupDefinition
        {
            DefinitionId = 33,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.WhiteChess,
            }),
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup3Mirror = new GoBangChessGroupDefinition
        {
            DefinitionId = 34,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.WhiteChess,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
            }),
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
        };
        public static readonly GoBangChessGroupDefinition BlackDeadTwoGroup4 = new GoBangChessGroupDefinition
        {
            DefinitionId = 35,
            Type = GoBangChessGroupType.DeadTwo,
            GoBangChess = GoBangChessType.BlackChess,
            EnemyChess = GoBangChessType.WhiteChess,
            Pattern = new List<GoBangChessType>(new[] {
                GoBangChessType.BlackChess,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.Blank,
                GoBangChessType.BlackChess
            }),
            Score = 10,
            AlreadyWin = false,
            EnemyMustFollow = false,
            GetFollowingPosition = (chesslist) => { return new List<GoBangChessPosition>(); },
            CouldFollowByAddingFourChess = false,
            AddToFourChess = (chesslist) => { return new List<GoBangChessPosition>(); },
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

        public static readonly GoBangChessGroupDefinition[] AllWinBlack = AllBlack.Where(x => x.AlreadyWin == true).ToArray();
        public static readonly GoBangChessGroupDefinition[] AllCriticalBlack = AllBlack.Where(x => x.EnemyMustFollow == true).ToArray();
        public static readonly GoBangChessGroupDefinition[] AllNormalBlack = AllBlack.Except(AllWinBlack).Except(AllCriticalBlack).ToArray();

        public static readonly GoBangChessGroupDefinition[] AllWinWhite = AllWhite.Where(x => x.AlreadyWin == true).ToArray();
        public static readonly GoBangChessGroupDefinition[] AllCriticalWhite = AllWhite.Where(x => x.EnemyMustFollow == true).ToArray();
        public static readonly GoBangChessGroupDefinition[] AllNormalWhite = AllBlack.Except(AllWinWhite).Except(AllCriticalWhite).ToArray();

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
