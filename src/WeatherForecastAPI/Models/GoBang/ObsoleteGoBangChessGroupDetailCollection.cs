using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherForecastAPI.Models.GoBang
{
    [Obsolete]
    public class ObsoleteGoBangChessGroupDetailCollection
    {
        private List<ObsoleteGoBangChessGroupDetail> _goBangChessGroupDetailList
            = new List<ObsoleteGoBangChessGroupDetail>();

        private Dictionary<GoBangChessPosition, List<ObsoleteGoBangChessGroupDetail>> _goBangChessGroupDetailDictionary
            = new Dictionary<GoBangChessPosition, List<ObsoleteGoBangChessGroupDetail>>();

        public void AddRange(List<ObsoleteGoBangChessGroupDetail> goBangChessGroupDetailList)
        {
            foreach (var goBangChessGroupDetail in goBangChessGroupDetailList)
            {
                Add(goBangChessGroupDetail);
            }
        }

        public void Add(ObsoleteGoBangChessGroupDetail goBangChessGroupDetail)
        {
            _goBangChessGroupDetailList.Add(goBangChessGroupDetail);
            foreach (GoBangChess chess in goBangChessGroupDetail.Chesses)
            {
                if (chess.ChessType == GoBangChessType.Wall)
                    continue;

                if (!_goBangChessGroupDetailDictionary.ContainsKey(chess.Position))
                {
                    _goBangChessGroupDetailDictionary[chess.Position] = new List<ObsoleteGoBangChessGroupDetail>();
                }

                _goBangChessGroupDetailDictionary[chess.Position].Add(goBangChessGroupDetail);
            }
        }

        public void Remove(GoBangChess chess)
        {
            List<ObsoleteGoBangChessGroupDetail> toBeRemovedDetailList = new List<ObsoleteGoBangChessGroupDetail>();
            if (_goBangChessGroupDetailDictionary.ContainsKey(chess.Position))
            {
                toBeRemovedDetailList.AddRange(_goBangChessGroupDetailDictionary[chess.Position]);
                foreach (ObsoleteGoBangChessGroupDetail existedGoBangChessGroupDetail in toBeRemovedDetailList)
                {
                    Remove(existedGoBangChessGroupDetail);
                }
            }
        }

        private void Remove(ObsoleteGoBangChessGroupDetail goBangChessGroupDetail)
        {
            _goBangChessGroupDetailList.Remove(goBangChessGroupDetail);
            foreach (GoBangChess chess in goBangChessGroupDetail.Chesses)
            {
                if(chess.ChessType == GoBangChessType.Wall)
                    continue;

                _goBangChessGroupDetailDictionary[chess.Position].Remove(goBangChessGroupDetail);
            }
        }

        public long SumSore(GoBangChessType goBangChessType)
        {
            return _goBangChessGroupDetailList
                .Where(detail => detail.GoBangChessGroupDefinition.GoBangChess == goBangChessType)
                .Sum(detail => detail.GoBangChessGroupDefinition.Score);
        }

        public bool BlackWin =>
            _goBangChessGroupDetailList.Any(x =>
                x.GoBangChessGroupDefinition.AlreadyWin &&
                x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.BlackChess);

        public bool WhiteWin =>
            _goBangChessGroupDetailList.Any(x =>
                x.GoBangChessGroupDefinition.AlreadyWin &&
                x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess);

        public bool BlackHasLiveFour =>
            _goBangChessGroupDetailList.Any(x =>
                x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.LiveFour &&
                x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.BlackChess);

        public bool WhiteHasLiveFour =>
            _goBangChessGroupDetailList.Any(x =>
                x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.LiveFour &&
                x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess);

        public bool BlackHasDoubleLiveThree =>
            _goBangChessGroupDetailDictionary.Any(keyValue =>
                keyValue.Value.Count(x =>
                    x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.LiveThree &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.BlackChess) >= 2 &&
                keyValue.Value.First().Chesses.First(x => x.Position.Row == keyValue.Key.Row && x.Position.Column == keyValue.Key.Column).ChessType == GoBangChessType.BlackChess);

        public bool WhiteHasDoubleLiveThree =>
            _goBangChessGroupDetailDictionary.Any(keyValue =>
                keyValue.Value.Count(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.LiveThree &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess) >= 2 &&
                keyValue.Value.First().Chesses.First(x => x.Position.Row == keyValue.Key.Row && x.Position.Column == keyValue.Key.Column).ChessType == GoBangChessType.WhiteChess);

        public bool BlackHasDoubleDeadFour =>
            _goBangChessGroupDetailDictionary.Any(keyValue =>
                keyValue.Value.Count(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.DeadFour &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.BlackChess) >= 2 &&
                keyValue.Value.First().Chesses.First(x => x.Position.Row == keyValue.Key.Row && x.Position.Column == keyValue.Key.Column).ChessType == GoBangChessType.BlackChess);

        public bool WhiteHasDoubleDeadFour =>
            _goBangChessGroupDetailDictionary.Any(keyValue =>
                keyValue.Value.Count(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.DeadFour &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess) >= 2 &&
                keyValue.Value.First().Chesses.First(x => x.Position.Row == keyValue.Key.Row && x.Position.Column == keyValue.Key.Column).ChessType == GoBangChessType.WhiteChess);

        public bool BlackHasDeadFourLiveThree =>
            _goBangChessGroupDetailDictionary.Any(keyValue =>
                keyValue.Value.Any(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.LiveThree &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.BlackChess) &&
                keyValue.Value.Any(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.DeadFour &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.BlackChess) &&
                keyValue.Value.First().Chesses.First(x => x.Position.Row == keyValue.Key.Row && x.Position.Column == keyValue.Key.Column).ChessType == GoBangChessType.BlackChess);

        public bool WhiteHasDeadFourLiveThree =>
            _goBangChessGroupDetailDictionary.Any(keyValue =>
                keyValue.Value.Any(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.LiveThree &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess) &&
                keyValue.Value.Any(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.DeadFour &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess) &&
                keyValue.Value.First().Chesses.First(x => x.Position.Row == keyValue.Key.Row && x.Position.Column == keyValue.Key.Column).ChessType == GoBangChessType.WhiteChess);

        public bool BlackMustFollow =>
            _goBangChessGroupDetailList.Any(x =>
                x.GoBangChessGroupDefinition.EnemyMustFollow &&
                x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess);


        public bool WhiteMustFollow =>
            _goBangChessGroupDetailList.Any(x =>
                x.GoBangChessGroupDefinition.EnemyMustFollow &&
                x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.BlackChess);


        public string ConvertToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var detail in _goBangChessGroupDetailList)
            {
                sb.Append(detail.ConvertToString());
                sb.Append(";");
            }
            return sb.ToString().TrimEnd(';');
        }

        public void ConvertFromString(string s)
        {
            string[] splits = s.Split(";");

            foreach (var split in splits)
            {
                ObsoleteGoBangChessGroupDetail? detail = ObsoleteGoBangChessGroupDetail.ConvertFromString(split);
                if (detail != null)
                {
                    Add(detail);
                }
            }
        }

        public ObsoleteGoBangChessGroupDetailCollection Copy()
        {
            ObsoleteGoBangChessGroupDetailCollection collection = new ObsoleteGoBangChessGroupDetailCollection();
            foreach (var goBangChessGroupDetail in _goBangChessGroupDetailList)
            {
                collection.Add(goBangChessGroupDetail);
            }
            return collection;
        }

        public List<GoBangChessPosition> GetWhiteMustFollowChessPosition()
        {
            var whiteMustFollowDetailList = _goBangChessGroupDetailList.Where(x =>
                x.GoBangChessGroupDefinition.EnemyMustFollow &&
                x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.BlackChess);

            List<GoBangChessPosition> mustFollowPositionList = new List<GoBangChessPosition>();
            bool couldFollowByAddingFourChess = true;
            foreach (var blackChessGroupDetail in whiteMustFollowDetailList)
            {
                couldFollowByAddingFourChess &= blackChessGroupDetail.GoBangChessGroupDefinition.CouldFollowByAddingFourChess;
                mustFollowPositionList.AddRange(blackChessGroupDetail.GetMustFollowGoBangChessPosition());
            }

            if (couldFollowByAddingFourChess)
            {
                var whiteDetailList = _goBangChessGroupDetailList.Where(x => x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess);
                foreach (var whiteChessGroupDetail in whiteDetailList)
                {
                    var addToFourChessPositions = whiteChessGroupDetail.GetAddToFourChessPosition();
                    if (addToFourChessPositions.Any())
                    {
                        mustFollowPositionList.AddRange(addToFourChessPositions);
                    }
                }
            }

            return mustFollowPositionList;
        }

        public List<GoBangChessPosition> GetBlackMustFollowChessPosition()
        {
            var blackMustFollowDetailList = _goBangChessGroupDetailList.Where(x =>
                x.GoBangChessGroupDefinition.EnemyMustFollow &&
                x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess);

            List<GoBangChessPosition> mustFollowPositionList = new List<GoBangChessPosition>();
            bool couldFollowByAddingFourChess = true;
            foreach (var whiteChessGroupDetail in blackMustFollowDetailList)
            {
                couldFollowByAddingFourChess &= whiteChessGroupDetail.GoBangChessGroupDefinition.CouldFollowByAddingFourChess;
                mustFollowPositionList.AddRange(whiteChessGroupDetail.GetMustFollowGoBangChessPosition());
            }

            if (couldFollowByAddingFourChess)
            {
                var blackDetailList = _goBangChessGroupDetailList.Where(x => x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.BlackChess);
                foreach (var blackChessGroupDetail in blackDetailList)
                {
                    var addToFourChessPositions = blackChessGroupDetail.GetAddToFourChessPosition();
                    if (addToFourChessPositions.Any())
                    {
                        mustFollowPositionList.AddRange(addToFourChessPositions);
                    }
                }
            }

            return mustFollowPositionList;
        }
    }
}
