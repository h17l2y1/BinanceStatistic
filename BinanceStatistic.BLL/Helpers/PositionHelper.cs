using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using BinanceStatistic.BLL.Helpers.Interfaces;
using BinanceStatistic.BinanceClient.Models;

namespace BinanceStatistic.BLL.Helpers
{
    public class PositionHelper : IPositionHelper
    {
        public List<BinancePosition> GetMocPositions()
        {
            string path = @"C:/Users/Anuitex-76/Desktop/Json.txt";
            
            OperatingSystem os = Environment.OSVersion;
            if (os.Platform == PlatformID.Unix)
            {
                path = @"/Users/new/Desktop/Json.txt";
            }
            
            string jsonString = File.ReadAllText(path, Encoding.UTF8);

            List<BinancePosition> positions = JsonSerializer.Deserialize<List<BinancePosition>>(jsonString);

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