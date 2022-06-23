using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherForecastAPI.Models.GoBang
{
    public class GoBangChessGroupDetailCollection
    {
        private List<GoBangChessGroupDetail> _goBangChessGroupDetailList
            = new List<GoBangChessGroupDetail>();

        private Dictionary<GoBangChessPosition, List<GoBangChessGroupDetail>> _goBangChessGroupDetailDictionary
            = new Dictionary<GoBangChessPosition, List<GoBangChessGroupDetail>>();

        public void Add(GoBangChessGroupDetail goBangChessGroupDetail)
        {
            //// check existed affected goBangChessGroupDetailList
            //List<GoBangChessGroupDetail> toBeRemovedDetailList = new List<GoBangChessGroupDetail>();
            //foreach (GoBangChess chess in goBangChessGroupDetail.Chesses)
            //{
            //    if (_goBangChessGroupDetailDictionary.ContainsKey(chess.Position))
            //    {
            //        foreach (GoBangChessGroupDetail existedGoBangChessGroupDetail in _goBangChessGroupDetailDictionary[chess.Position])
            //        {
            //            if (existedGoBangChessGroupDetail.Direction == goBangChessGroupDetail.Direction)
            //            {
            //                toBeRemovedDetailList.Add(existedGoBangChessGroupDetail);
            //            }
            //        }
            //    }
            //}
            //toBeRemovedDetailList = toBeRemovedDetailList.Distinct().ToList();
            //foreach (GoBangChessGroupDetail? toBeRemovedDetail in toBeRemovedDetailList)
            //{
            //    Remove(toBeRemovedDetail);
            //}

            // add new detail

            _goBangChessGroupDetailList.Add(goBangChessGroupDetail);
            foreach (GoBangChess chess in goBangChessGroupDetail.Chesses)
            {
                if (chess.Chess == GoBangChessType.Wall)
                    continue;
                if (!_goBangChessGroupDetailDictionary.ContainsKey(chess.Position))
                {
                    _goBangChessGroupDetailDictionary[chess.Position] = new List<GoBangChessGroupDetail>();
                }

                _goBangChessGroupDetailDictionary[chess.Position].Add(goBangChessGroupDetail);
            }
        }

        public void Remove(GoBangChess chess)
        {
            List<GoBangChessGroupDetail> toBeRemovedDetailList = new List<GoBangChessGroupDetail>();
            if (_goBangChessGroupDetailDictionary.ContainsKey(chess.Position))
            {
                toBeRemovedDetailList.AddRange(_goBangChessGroupDetailDictionary[chess.Position]);
                foreach (GoBangChessGroupDetail existedGoBangChessGroupDetail in toBeRemovedDetailList)
                {
                    Remove(existedGoBangChessGroupDetail);
                }
            }
        }

        public void Remove(GoBangChessGroupDetail goBangChessGroupDetail)
        {
            _goBangChessGroupDetailList.Remove(goBangChessGroupDetail);
            foreach (GoBangChess chess in goBangChessGroupDetail.Chesses)
            {
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
                keyValue.Value.First().Chesses.First(x => x.Position.Row == keyValue.Key.Row && x.Position.Column == keyValue.Key.Column).Chess == GoBangChessType.BlackChess);

        public bool WhiteHasDoubleLiveThree =>
            _goBangChessGroupDetailDictionary.Any(keyValue =>
                keyValue.Value.Count(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.LiveThree &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess) >= 2 &&
                keyValue.Value.First().Chesses.First(x => x.Position.Row == keyValue.Key.Row && x.Position.Column == keyValue.Key.Column).Chess == GoBangChessType.WhiteChess);

        public bool BlackHasDoubleDeadFour =>
            _goBangChessGroupDetailDictionary.Any(keyValue =>
                keyValue.Value.Count(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.DeadFour &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.BlackChess) >= 2 &&
                keyValue.Value.First().Chesses.First(x => x.Position.Row == keyValue.Key.Row && x.Position.Column == keyValue.Key.Column).Chess == GoBangChessType.BlackChess);

        public bool WhiteHasDoubleDeadFour =>
            _goBangChessGroupDetailDictionary.Any(keyValue =>
                keyValue.Value.Count(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.DeadFour &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess) >= 2 &&
                keyValue.Value.First().Chesses.First(x => x.Position.Row == keyValue.Key.Row && x.Position.Column == keyValue.Key.Column).Chess == GoBangChessType.WhiteChess);

        public bool BlackHasDeadFourLiveThree =>
            _goBangChessGroupDetailDictionary.Any(keyValue =>
                keyValue.Value.Any(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.LiveThree &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.BlackChess) &&
                keyValue.Value.Any(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.DeadFour &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.BlackChess) &&
                keyValue.Value.First().Chesses.First(x => x.Position.Row == keyValue.Key.Row && x.Position.Column == keyValue.Key.Column).Chess == GoBangChessType.BlackChess);

        public bool WhiteHasDeadFourLiveThree =>
            _goBangChessGroupDetailDictionary.Any(keyValue =>
                keyValue.Value.Any(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.LiveThree &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess) &&
                keyValue.Value.Any(
                    x => x.GoBangChessGroupDefinition.Type == GoBangChessGroupType.DeadFour &&
                    x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess) &&
                keyValue.Value.First().Chesses.First(x => x.Position.Row == keyValue.Key.Row && x.Position.Column == keyValue.Key.Column).Chess == GoBangChessType.WhiteChess);

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
                GoBangChessGroupDetail? detail = GoBangChessGroupDetail.ConvertFromString(split);
                if (detail != null)
                {
                    Add(detail);
                }
            }
        }
    }
}
