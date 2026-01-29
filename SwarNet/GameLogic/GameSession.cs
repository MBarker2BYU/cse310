// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-26-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="GameSession.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SwarNet.Controls;
using SwarNet.CrossThread;
using SwarNet.Enums;
using SwarNet.EventArgs;
using SwarNet.Models;
using SwarNet.Networking;
using SwarNet.Serialization;
using SwarNet.Structs;

namespace SwarNet.GameLogic;

/// <summary>
/// Class GameSession.
/// </summary>
public class GameSession
{

    #region Events

    /// <summary>
    /// Messages the received.
    /// </summary>
    /// <param name="obj">The object.</param>
    private void MessageReceived(NetworkMessage obj)
    {
        switch (obj.Type)
        {
            case MessageType.SITREP:
                {
                    PostUpdate(obj.Payload.ToBattleFieldSITREP());
                    
                    break;
                }
            case MessageType.PlaceShips:
                {
                    PostUpdate(obj.Payload.ToBattleFieldSITREP());
                    
                    break;
                }
            case MessageType.TextResources:
            {
                m_TextResources = obj.Payload.ToTextResources();


                break;
            }
        }
    }

    /// <summary>
    /// Instance Disconnect Event
    /// </summary>
    private void Disconnected()
    {

    }


    /// <summary>
    /// Logs the message.
    /// </summary>
    /// <param name="obj">The object.</param>
    private void LogMessage(string obj)
    {

    }

    /// <summary>
    /// Grids the cell clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="GridCellClickedEventArgs"/> instance containing the event data.</param>
    private void GridCellClicked(object? sender, GridCellClickedEventArgs e)
    {

        var msg = NetworkMessage.Attack(e.GridCell);

        m_GameClient.SendMessage(msg);

    }

    #endregion

    #region Methods

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="GameSession"/> class.
    /// </summary>
    /// <param name="gameClient">The game client.</param>
    /// <param name="fleetAttackBoard">The fleet attack board.</param>
    /// <param name="fleetStatusBoard">The fleet status board.</param>
    /// <param name="turnLabel">The turn label.</param>
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

    /// <summary>
    /// Posts the update.
    /// </summary>
    /// <param name="sitrep">The sitrep.</param>
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

            if (sitrep.ShotReport != ShotReport.None)
                m_FleetAttackBoard.OverlayMessage =
                    sitrep.ShotReport == ShotReport.Hit ? m_TextResources.HitText : m_TextResources.MissText;
        });

        m_TurnLabel.InvokeIfRequired(() =>
        {
            m_TurnLabel.Text = m_FleetAttackBoard.HoverEnabled ? m_TextResources.YourTurnText : m_TextResources.OpponentTurnText;
        });

    }

    #endregion

    #region Properties and Fields

    /// <summary>
    /// The m game client
    /// </summary>
    private readonly GameClient m_GameClient;

    /// <summary>
    /// The m fleet attack board
    /// </summary>
    private readonly FleetGameBoard m_FleetAttackBoard;
    /// <summary>
    /// The m fleet status board
    /// </summary>
    private readonly FleetGameBoard m_FleetStatusBoard;

    /// <summary>
    /// The m turn label
    /// </summary>
    private readonly Label m_TurnLabel;

    /// <summary>
    /// The m text resources
    /// </summary>
    private TextResources m_TextResources;

    #endregion

}