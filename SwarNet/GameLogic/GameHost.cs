// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-26-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="GameHost.cs" company="SwarNet">
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
/// Class GameHost.
/// </summary>
public class GameHost
{

    /// <summary>
    /// The resource file name
    /// </summary>
    private const string RESOURCE_FILE_NAME = "SwarNet-TextResources.json";

    #region Events

    /// <summary>
    /// Messages the received.
    /// </summary>
    /// <param name="msg">The MSG.</param>
    private void MessageReceived(NetworkMessage msg)
    {
        switch (msg.Type)
        {
            case MessageType.Attack:
                {

                    var gridCell = msg.Payload.ToGridCell();

                    PostUpdate(m_GameModule.Incoming(Player.Player2, gridCell));

                    break;
                }
        }
    }

    /// <summary>
    /// Called when [client connected event].
    /// </summary>
    private void OnClientConnectedEvent()
    {

    }

    /// <summary>
    /// Called when [client disconnected].
    /// </summary>
    private void OnClientDisconnected()
    {

    }

    /// <summary>
    /// Called when [log message].
    /// </summary>
    /// <param name="obj">The object.</param>
    private void OnLogMessage(string obj)
    {

    }

    /// <summary>
    /// Grids the cell clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="GridCellClickedEventArgs"/> instance containing the event data.</param>
    private void GridCellClicked(object? sender, GridCellClickedEventArgs e)
    {
        PostUpdate(m_GameModule.Incoming(Player.Player1, e.GridCell));

    }

    #endregion

    #region Methods

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="GameHost"/> class.
    /// </summary>
    /// <param name="gameServer">The game server.</param>
    /// <param name="fleetAttackBoard">The fleet attack board.</param>
    /// <param name="fleetStatusBoard">The fleet status board.</param>
    /// <param name="turnLabel">The turn label.</param>
    public GameHost(GameServer gameServer, FleetGameBoard fleetAttackBoard, FleetGameBoard fleetStatusBoard, Label turnLabel)
    {

        //Very basic file 
        if (!File.Exists(RESOURCE_FILE_NAME))
        {
            m_TextResources = new TextResources();

            File.WriteAllText(RESOURCE_FILE_NAME, m_TextResources.ToPayload());
        }
        else
        {
            m_TextResources = File.ReadAllText(RESOURCE_FILE_NAME).ToTextResources();
        }

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

        m_GameServer.SendMessage(NetworkMessage.TextResources(m_TextResources!));

        m_GameModule.DeployFleets();

        PostUpdate(m_GameModule.GetBattleFieldSITREP());
        
    }

    /// <summary>
    /// Posts the update.
    /// </summary>
    /// <param name="sitrep">The sitrep.</param>
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

            if (sitrep.Player1SITREP.ShotReport != ShotReport.None)
                m_FleetStatusBoard.OverlayMessage =
                    sitrep.Player1SITREP.ShotReport == ShotReport.Hit ? m_TextResources.HitText : m_TextResources.MissText;

        });

        m_TurnLabel.InvokeIfRequired(() =>
        {
            m_TurnLabel.Text = m_FleetAttackBoard.HoverEnabled ? "Your turn. Send it!" : "Brace for impact!";
        });

    }

    #endregion

    #endregion


    #region Properties and Fields

    /// <summary>
    /// The m game server
    /// </summary>
    private readonly GameServer m_GameServer;

    /// <summary>
    /// The m game module
    /// </summary>
    private readonly GameModule m_GameModule;

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