//AA Construction Mod Load Screen
package loadmodinfo
{
 function sendLoadInfoToClient( %client, %second )
{
   //error( "** SENDING LOAD INFO TO CLIENT " @ %client @ "! **" );
   %singlePlayer = $CurrentMissionType $= "SinglePlayer";
   messageClient( %client, 'MsgLoadInfo', "", $CurrentMission, $MissionDisplayName, $MissionTypeDisplayName );

   // Send map quote:
   for ( %line = 0; %line < $LoadQuoteLineCount; %line++ )
   {
      if ( $LoadQuoteLine[%line] !$= "" )
         messageClient( %client, 'MsgLoadQuoteLine', "", $LoadQuoteLine[%line] );
   }

   // Send map objectives:
   if ( %singlePlayer )
   {
      switch ( $pref::TrainingDifficulty )
      {
         case 2:  %diff = "Medium";
         case 3:  %diff = "Hard";
         default: %diff = "Easy";
      }
      messageClient( %client, 'MsgLoadObjectiveLine', "", "<spush><font:" @ $ShellLabelFont @ ":" @ $ShellMediumFontSize @ ">DIFFICULTY: <spop>" @ %diff );
   }

   for ( %line = 0; %line < $LoadObjLineCount; %line++ )
   {
      if ( $LoadObjLine[%line] !$= "" )
         messageClient( %client, 'MsgLoadObjectiveLine', "", $LoadObjLine[%line], !%singlePlayer );
   }

   // Send rules of engagement:
   if ( !%singlePlayer )
      messageClient( %client, 'MsgLoadRulesLine', "", "<spush><font:Univers Condensed:18>RULES OF ENGAGEMENT:<spop>", false );

   for ( %line = 0; %line < $LoadRuleLineCount; %line++ )
   {
      if ( $LoadRuleLine[%line] !$= "" )
         messageClient( %client, 'MsgLoadRulesLine', "", $LoadRuleLine[%line], !%singlePlayer );
   }

   messageClient( %client, 'MsgLoadInfoDone' );

   // ----------------------------------------------------------------------------------------------
   // z0dd - ZOD, 5/12/02. Send the mod info screen if this isn't the second showing of mission info
   if(!%second)
      schedule(6000, 0, "sendModInfoToClient", %client);
   // ----------------------------------------------------------------------------------------------
}

function sendModInfoToClient(%client) //Kinda Jacked Classic's loadScreen here.. but it is generally not used
{
   %on = "On";
   %off = "Off";
   %line[0] = "<color:556B2F>Game Type: <color:8FBC8F>" @ $CurrentMissionType;
   %modName = "T2Bol" SPC $ModVersionText @ "";
   %ModCnt = 1;
   %ModLine[0] = "<spush><font:univers condensed:15>Developers: <a:PLAYER\tz0dd>DarkDragonDX</a><spop>";

   %SpecialCnt = 3;
   %SpecialTextLine[0] = "Map:" SPC $CurrentMission;
   %SpecialTextLine[1] = "Game Type:" SPC $CurrentMissionType;

   if ($Host::BotsEnabled)
   %SpecialTextLine[2] = "Bot Count:" SPC $HostGameBotCount;

   %ServerCnt = 1;
   %ServerTextLine[0] = "";
   %ServerTextLine[1] = "";

   %singlePlayer = $CurrentMissionType $= "SinglePlayer";
   messageClient( %client, 'MsgLoadInfo', "", $CurrentMission, %modName, $Host::GameName );

   // Send mod details (non bulleted list, small text):
   for(%line = 0; %line < %ModCnt; %line++)
   {
      if(%ModLine[%line] !$= "")
         messageClient(%client, 'MsgLoadQuoteLine', "", %ModLine[%line]);
   }

   // Send mod special settings (bulleted list, large text):
   for (%line = 0; %line < %SpecialCnt; %line++)
   {
      if(%SpecialTextLine[%line] !$= "")
         messageClient( %client, 'MsgLoadObjectiveLine', "", %SpecialTextLine[%line], !%singlePlayer);
   }

   // Send server info:
   if ( !%singlePlayer )
      messageClient( %client, 'MsgLoadRulesLine', "", "<color:8FBC8F>" @ $Host::Info, false );

   for(%line = 0; %line < %ServerCnt; %line++)
   {
      if (%ServerTextLine[%line] !$= "")
         messageClient(%client, 'MsgLoadRulesLine', "", %ServerTextLine[%line], !%singlePlayer);
   }
   messageClient(%client, 'MsgLoadInfoDone');
   // z0dd - ZOD, 5/12/02. Send mission info again so as not to conflict with cs scripts.
   schedule(7000, 0, "sendLoadInfoToClient", %client, true);
}

function debriefLoad(%client)
   {
      if (isObject(Game))
         %game = Game.getId();
      else
         return;

   if ($HostGameType $= "SinglePlayer")
   return;

   //Clear the debrief first
   messageClient( %client, 'MsgClearDebrief', "");

   messageClient( %client, 'MsgDebriefResult', "", "<Just:Center><font:Broadway Bt:21><Color:FFFFFF>"@$Host::GameName@"\n<font:Broadway Bt:14>Advanced Architechture Construction Mod "@getWord($ModVersion,0)@"");
   messageClient( %client, 'MsgDebriefAddLine', "", " " );
   messageClient( %client, 'MsgDebriefAddLine', "", "<Just:Center><Color:FFFFFF><font:Broadway Bt:15>If you do not see the image <Color:888888>below<Color:FFFFFF> then you do not have the mod installed." );
   messageClient( %client, 'MsgDebriefAddLine', "", " " );
   messageClient( %client, 'MsgDebriefAddLine', "", "<Just:Center><bitmap:AALogo>");

   //Several blank lines must be added to compensate for the large AA Logo image
   for (%i = 0; %i < 9; %i++)
   {
    messageClient( %client, 'MsgDebriefAddLine', "", " " );
   }

   messageClient( %client, 'MsgDebriefAddLine', "", "\n<Just:Center>www.AAConstruction.net");
   messageClient( %client, 'MsgDebriefAddLine', "", " ");
   messageClient( %client, 'MsgDebriefAddLine', "", "\n<Just:Center><font:Arial Bold:16>-Developer Credits-");
   messageClient( %client, 'MsgDebriefAddLine', "", "<Just:Center><font:Broadway Bt:15><a:PLAYER\tDarkDragonDX>Dark Dragon DX</a> - Main Coding");
   messageClient( %client, 'MsgDebriefAddLine', "", "<Just:Center><font:Broadway Bt:15><a:PLAYER\tSignal360>Signal360</a> - Currently making a replica of ECM's Logic Pack");
   messageClient( %client, 'MsgDebriefAddLine', "", "<Just:Center><font:Broadway Bt:15><a:PLAYER\tExerpt>Exerpt</a> - Graphics Artist, 3D Modeler, Website Design, Project Supervisor");
   messageClient( %client, 'MsgDebriefAddLine', "", "<Just:Center><font:Broadway Bt:15><a:PLAYER\tThyth>Thyth</a> - Provided the MIST for the entire community to use");
   messageClient( %client, 'MsgDebriefAddLine', "", "" );

   //Go to the debrief gui.
   messageClient( %client, 'MsgGameOver', "" );

   }
};
activatepackage(loadmodinfo);
