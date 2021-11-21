using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using BinanceStatistic.BLL.Helpers.Interfaces;
using BinanceStatistic.Core.Models;

namespace BinanceStatistic.BLL.Helpers
{
    public class PositionHelper : IPositionHelper
    {
        public List<Position> GetMocPositions()
        {
            string jsonString = File.ReadAllText(@"/Users/new/Desktop/Json.txt", Encoding.UTF8);
            
            List<Position> positions =
                JsonSerializer.Deserialize<List<Position>>(jsonString);

            foreach (var position in positions)
            {
                position.FormattedUpdateTime = new DateTime(
                    position.UpdateTime[0],
                    position.UpdateTime[1],
                    position.UpdateTime[2],
                    position.UpdateTime[3],
                    position.UpdateTime[4],
                    position.UpdateTime[5]
                    );
            }
            
            return positions;

        }
    }
}