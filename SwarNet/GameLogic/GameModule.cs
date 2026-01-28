using SwarNet.Enums;
using SwarNet.Models;
using SwarNet.Structs;

namespace SwarNet.GameLogic;

public class GameModule
{
    
    private static readonly Random m_Random = new Random();

    #region Methods

    #region Constructors

    public GameModule(int gridSize)
    {
        GridSize = gridSize;
        
        m_PlayersFleet = [new Fleet(Player.Player1, GridSize), new Fleet(Player.Player2, GridSize)];
    }

    #endregion

    public void DeployFleets()
    {
        foreach (var fleet in m_PlayersFleet)
            DeployTheFleet(fleet.Player, true); 
    }

    public BattleFieldSITREP DeployTheFleet(Player player, bool sitrepOverride = false)
    {
        m_PlayersFleet[(int)player].AutoDeployFleet();

        return new BattleFieldSITREP(PlayersTurn, m_PlayersFleet[(int)player].GetSITREP(), null);
    }

    public (BattleFieldSITREP Player1SITREP, BattleFieldSITREP Player2SITREP) Incoming(Player player, GridCell gridCell)
    {
        var offense = (int)player;
        var defense = (int)(player == Player.Player1 ? Player.Player2 : Player.Player1);
        
        m_PlayersFleet[offense].OutgoingReport(gridCell, m_PlayersFleet[defense].Incoming(gridCell)
            ? ShotReport.Hit
            : ShotReport.Miss);

        PlayersTurn = PlayersTurn == Player.Player1 ? Player.Player2 : Player.Player1;

        return GetBattleFieldSITREP();
    }

    public (BattleFieldSITREP Player1SITREP, BattleFieldSITREP Player2SITREP) GetBattleFieldSITREP()
    {
        var p1SITREP = m_PlayersFleet[(int)Player.Player1].GetSITREP();
        var p2SITREP = m_PlayersFleet[(int)Player.Player2].GetSITREP();

        return (new BattleFieldSITREP(PlayersTurn, p1SITREP, p2SITREP), new BattleFieldSITREP(PlayersTurn, p2SITREP, p1SITREP));
    }

    #endregion

    #region Properties and Fields


    private readonly Fleet[] m_PlayersFleet;
    public int GridSize { get; }

    public Player PlayersTurn { get; private set; } = Player.Player1;

    #endregion
}