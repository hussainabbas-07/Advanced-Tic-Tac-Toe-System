using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTicTacToeGame.Interfaces
{
    public interface IStatistics
    {
        int GetTotalGames();

        double GetWinPercentage();
    }
}
