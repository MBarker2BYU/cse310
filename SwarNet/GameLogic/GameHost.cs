using SwarNet.Controls;
using SwarNet.CrossThread;
using SwarNet.Enums;
using SwarNet.EventArgs;
using SwarNet.Extensions;
using SwarNet.Models;
using SwarNet.Networking;
using SwarNet.Serialization;
using SwarNet.Structs;

namespace SwarNet.GameLogic;

public class GameHost
{
    #region Events

    private void MessageReceived(NetworkMessage msg)
    {
        switch (msg.Type)
        {
            case MessageType.Attack:
                {

                    var gridCell = msg.Payload.ToGridCell();

                    PostUpdate(m_GameModule.Incoming(Player.Player2, gridCell));
                    //var sitrep = m_GameModule.Incoming(Player.Player2, gridCell);

                    //var msgOut = NetworkMessage.SITREP(sitrep.Player2SITREP);

                    //m_GameServer.SendMessage(msgOut);

                    //m_FleetAttackBoard.PostSITREP(sitrep.Player1SITREP);
                    //m_FleetStatusBoard.PostSITREP(sitrep.Player1SITREP);

                    //m_FleetAttackBoard.HoverEnabled = sitrep.Player1SITREP.PlayersTurn == Player.Player1;

                    break;
                }
        }
    }

    private void OnClientConnectedEvent()
    {

    }

    private void OnClientDisconnected()
    {

    }

    private void OnLogMessage(string obj)
    {

    }

    private void GridCellClicked(object? sender, GridCellClickedEventArgs e)
    {
        PostUpdate(m_GameModule.Incoming(Player.Player1, e.GridCell));

        //var sitrep = m_GameModule.Incoming(Player.Player1, e.GridCell);

        //var msgOut = NetworkMessage.SITREP(sitrep.Player2SITREP);

        //m_GameServer.SendMessage(msgOut);

        //m_FleetAttackBoard.PostSITREP(sitrep.Player1SITREP);
        //m_FleetStatusBoard.PostSITREP(sitrep.Player1SITREP);

        //m_FleetAttackBoard.HoverEnabled = sitrep.Player1SITREP.PlayersTurn == Player.Player1;
    }

    #endregion

    #region Methods

    #region Constructors

    public GameHost(GameServer gameServer, FleetGameBoard fleetAttackBoard, FleetGameBoard fleetStatusBoard, Label turnLabel)
    {
        m_GameServer = gameServer;
        m_GameModule = new GameModule(FleetGameBoard.GRID_SIZE);

        //Attach Events
        m_GameServer.MessageReceived += MessageReceived;
        m_GameServer.ClientConnectedEvent += OnClientConnectedEvent;
        m_GameServer.ClientDisconnected += OnClientDisconnected;
        m_GameServer.LogMessage += OnLogMessage;

        m_TurnLabel = turnLabel;

        m_FleetAttackBoard = fleetAttackBoard;
        m_FleetAttackBoard.GridCellClicked += GridCellClicked;

        m_FleetStatusBoard = fleetStatusBoard;

        m_GameModule.DeployFleets();

        PostUpdate(m_GameModule.GetBattleFieldSITREP());

    }

    private void PostUpdate((BattleFieldSITREP Player1SITREP, BattleFieldSITREP Player2SITREP) sitrep)
    {
        m_GameServer.SendMessage(NetworkMessage.PlaceShips(sitrep.Player2SITREP));

        m_FleetAttackBoard.InvokeIfRequired(() =>
        {
            m_FleetAttackBoard.PostSITREP(sitrep.Player1SITREP);
            m_FleetAttackBoard.HoverEnabled = sitrep.Player1SITREP.PlayersTurn == sitrep.Player1SITREP.Player;
        });

        m_FleetStatusBoard.InvokeIfRequired(() =>
        {
            m_FleetStatusBoard.PostSITREP(sitrep.Player1SITREP);
        });

        m_TurnLabel.InvokeIfRequired(() =>
        {
            m_TurnLabel.Text = m_FleetAttackBoard.HoverEnabled ? "Your turn. Send it!" : "Brace for impact!";
        });

    }

    #endregion

    #endregion


    #region Properties and Fields

    private readonly GameServer m_GameServer;

    private readonly GameModule m_GameModule;

    private readonly FleetGameBoard m_FleetAttackBoard;
    private readonly FleetGameBoard m_FleetStatusBoard;

    private readonly Label m_TurnLabel;

    #endregion
}