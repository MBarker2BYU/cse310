using SwarNet.Controls;
using SwarNet.CrossThread;
using SwarNet.Enums;
using SwarNet.EventArgs;
using SwarNet.Networking;
using SwarNet.Serialization;
using SwarNet.Structs;

namespace SwarNet.GameLogic;

public class GameSession
{

    #region Events

    private void MessageReceived(NetworkMessage obj)
    {
        switch (obj.Type)
        {
            case MessageType.SITREP:
                {
                    PostUpdate(obj.Payload.ToBattleFieldSITREP());

                    //var sitrep = obj.Payload.ToBattleFieldSITREP();

                    //m_FleetAttackBoard.PostSITREP(sitrep);
                    //m_FleetStatusBoard.PostSITREP(sitrep);

                    //m_FleetAttackBoard.HoverEnabled = sitrep.PlayersTurn == Player.Player2;

                    break;
                }
            case MessageType.PlaceShips:
                {
                    PostUpdate(obj.Payload.ToBattleFieldSITREP());

                    //var sitrep = obj.Payload.ToBattleFieldSITREP();

                    //m_FleetAttackBoard.PostSITREP(sitrep);
                    //m_FleetStatusBoard.PostSITREP(sitrep);

                    //m_FleetAttackBoard.HoverEnabled = sitrep.PlayersTurn == Player.Player2;

                    break;
                }
        }
    }

    private void Disconnected()
    {

    }


    private void LogMessage(string obj)
    {

    }

    private void GridCellClicked(object? sender, GridCellClickedEventArgs e)
    {

        var msg = NetworkMessage.Attack(e.GridCell);

        m_GameClient.SendMessage(msg);

    }

    #endregion

    #region Methods

    #region Constructors

    public GameSession(GameClient gameClient, FleetGameBoard fleetAttackBoard, FleetGameBoard fleetStatusBoard, Label turnLabel)
    {
        m_GameClient = gameClient;

        //Attach Events
        m_GameClient.MessageReceived += MessageReceived;
        m_GameClient.Disconnected += Disconnected;
        m_GameClient.LogMessage += LogMessage;

        m_FleetAttackBoard = fleetAttackBoard;
        m_FleetAttackBoard.GridCellClicked += GridCellClicked;

        m_FleetStatusBoard = fleetStatusBoard;
        m_TurnLabel = turnLabel;
    }

    #endregion

    private void PostUpdate(BattleFieldSITREP sitrep)
    {

        m_FleetAttackBoard.InvokeIfRequired(() =>
            {
                m_FleetAttackBoard.PostSITREP(sitrep);
                m_FleetAttackBoard.HoverEnabled = sitrep.PlayersTurn == Player.Player2;
            });

        m_FleetStatusBoard.InvokeIfRequired(() =>
        {
            m_FleetStatusBoard.PostSITREP(sitrep);
        });

        m_TurnLabel.InvokeIfRequired(() =>
        {
            m_TurnLabel.Text = m_FleetAttackBoard.HoverEnabled ? "Your turn. Send it!" : "Brace for impact!";
        });

    }

    #endregion

    #region Properties and Fields

    private readonly GameClient m_GameClient;

    private readonly FleetGameBoard m_FleetAttackBoard;
    private readonly FleetGameBoard m_FleetStatusBoard;

    private readonly Label m_TurnLabel;

    #endregion

}