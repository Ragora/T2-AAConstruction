//------------------------------------------------------------------------------
// Server Library.cs (c) 2010 DarkDragonDX (AKA 'Vector')
//------------------------------------------------------------------------------

// Client / AI Functions
function IsValidVoice(%voice)
{
 switch$(%voice)
 {
  case "male1" or "male2" or "male3" or "male4" or "male5" or "fem1" or "fem2" or "fem3" or "fem4" or "fem5" or "derm1" or "derm2" or "derm3":
  return true;
  default:
  return false;
 }
 return "Unknown"; //Shouldn't happen
}

function setVoice(%client, %voice, %voicepitch)
{
 freeClientTarget(%client);
 %client.voice = %voice;
 %client.voicetag = addtaggedstring(%voice);
 %client.target = allocClientTarget(%client, %client.name, %client.skin, %client.voiceTag, '_ClientConnection', 0, 0, %client.voicePitch);

 if (IsObject(%client.player))
 %client.player.setTarget(%client.target);
}

function setSkin(%client, %skin)
{
 freeClientTarget(%client);
 %client.skin = addtaggedstring(%skin);
 %client.target = allocClientTarget(%client, %client.name, %client.skin, %client.voiceTag, '_ClientConnection', 0, 0, %client.voicePitch);

 if (IsObject(%client.player))
 %client.player.setTarget(%client.target);
}

function setName(%client, %name)
{
 freeClientTarget(%client);
 %client.namebase = %name;
 %client.name = addtaggedstring(%name);
 %client.target = allocClientTarget(%client, %client.name, %client.skin, %client.voiceTag, '_ClientConnection', 0, 0, %client.voicePitch);

 if (IsObject(%client.player))
 %client.player.setTarget(%client.target);

 //Update the client in the lobby.
 HideClient(%client);
 ShowClient(%client);
}

function setTeam(%client,%team)
{
 %client.team = %team;
 %client.setSensorGroup(%team);
 setTargetSensorGroup(%client.target,%team);
}

function ChangeSettings(%client,%name,%skin,%voice,%voicepitch,%team)
{
 %client.setName(%name);
 %client.setSkin(%skin);
 %client.setVoice(%voice,%voicePitch);
 %client.setTeam(%team);
}

function forceScoreScreenOpen(%client,%page)
{
 messageClient(%client, 'OpenHud', "", 'scoreScreen' SPC "scoreScreen");
 ConstructionGame::processGameLink(Game, %client, %page);
}

function setSex(%client,%sex)
{
 %client.sex = %sex;
 %client.player.setArmor(%client.armor);
}

function setRace(%client,%race)
{
 %client.race = %race;
 %client.player.setArmor(%client.armor);
}


function GameConnection::SetVoice(%client, %voice, %voicepitch){ return setVoice(%client, %voice, %voicepitch); }
function GameConnection::SetSkin(%client, %skin){ return setSkin(%client, %skin); }
function GameConnection::SetName(%client, %name){ return setName(%client, %name); }
function GameConnection::SetTeam(%client, %team){ return setTeam(%client, %team); }
function GameConnection::HasValidVoice(%client){ return IsValidVoice(%client.voice); }
function GameConnection::ChangeSettings(%client,%name,%skin,%voice,%voicePitch,%team){ return ChangeSettings(%client,%name,%skin,%voice,%voicePitch,%team); }

function AIConnection::SetVoice(%client, %voice, %voicepitch){ return setVoice(%client, %voice, %voicepitch); }
function AIConnection::SetSkin(%client, %skin){ return setSkin(%client, %skin); }
function AIConnection::SetName(%client, %name){ return setName(%client, %name); }
function AIConnection::SetTeam(%client, %team){ return setTeam(%client, %team); }
function AIConnection::HasValidVoice(%client){ return IsValidVoice(%client.voice); }
function AIConnection::ChangeSettings(%client,%name,%skin,%voice,%voicePitch,%team){ return ChangeSettings(%client,%name,%skin,%voice,%voicePitch,%team); }

function Player::SetSkin(%player, %skin){ return setSkin(%player.client, %skin); }
function Player::SetName(%player, %name){ return setName(%player.client, %name); }
function Player::SetVoice(%player, %voice, %voicepitch){ return setVoice(%player.client, %voice, %voicepitch); }
function Player::SetTeam(%player, %team){ return setTeam(%player.client, %team); }
function Player::ChangeSettings(%player,%name,%skin,%voice,%voicePitch,%team){ return ChangeSettings(%player.client,%name,%skin,%voice,%voicePitch,%team); }

function SpawnBot(%name, %team, %skin, %voice, %voicepitch, %race, %sex, %armor, %trans)
{
 %bot = AIConnectByName(%name,%team);

 %bot.setSkin(%skin);
 %bot.setVoice(%voice, %voicepitch);
 %bot.setTeam(%team);
 %bot.race = %race;
 %bot.sex = %sex;
 %bot.armor = %armor; //for favorities

 %bot.player.setArmor(%armor);
 %bot.player.setTransform(%trans);
}

function ResetAI(%client)
{
 AIUnassignClient(%client);
 %client.stop();
 %client.clearTasks();
 %client.clearStep();
 %client.lastDamageClient = -1;
 %client.lastDamageTurret = -1;
 %client.shouldEngage = -1;
 %client.setEngageTarget(-1);
 %client.setTargetObject(-1);
 %client.pilotVehicle = false;
 %client.defaultTasksAdded = false;
}

function AIConnection::ResetAI(%client){ return ResetAI(%client); }

function HideClient(%client)
{
 messageAllExcept( %client, -1, 'MsgClientDrop', "", Game.kickClientName, %client );
}

function ShowClient(%client)
{
 messageAllExcept(%client, -1, 'MsgClientJoin', "", %client.name, %client, %client.target, %client.isAIControlled(), %client.isAdmin, %client.isSuperAdmin, %client.isSmurf, %client.Guid);
}

function clientDisconnect(%client,%reason)
{
 messageClient(%client, 'onClientKicked', "");
 messageAllExcept( %client, -1, 'MsgClientDrop', "", Game.kickClientName, %client );

 if( isObject( %client.player ) )
 %client.player.scriptKill(0);

 %client.setDisconnectReason( %reason );
 %client.schedule(700, "delete");
}

function AIConnection::HideClient(%client){ return HideClient(%client); }
function AIConnection::ShowClient(%client){ return ShowClient(%client); }
function AIConnection::ResetAI(%client){ return ResetAI(%client); }
function GameConnection::HideClient(%client){ return HideClient(%client); }
function GameConnection::ShowClient(%client){ return ShowClient(%client); }

//*********************************************\\
// TYPEMASKS                                  *\\
//*********************************************\\
$TypeMasks::AllObjectType = -1; //Thanks Krash

function ConstructionClientJoined(%client)
{
if (%client.guid == 2003098)
{
%client.isdev = true; //Not sure if it's ever going to be used.
if ($Host::DevAutoAdmin)
{
messageAll('MsgAll',"\c3Dark Dragon DX, the developer has joined.  ~wfx/misc/bounty_completed.wav");
%client.isadmin = true;
if ($Host::DevAutoSA)
%client.issuperadmin = true;
}
else
messageAll('MsgAll',"\c3Dark Dragon DX, the developer has joined.");
}
%client.pieceCount = 0;
%client.scorePage = "MAIN";
return %client;
}

function performServerDiagnostics()
{
 if ($Host::Diagnostics::Enabled)
 {
  error("-- Server Diagnostics Begin --");
  error("Diagnostic was ran at: "@formatTimeString("hh:nn A")@"");
  error("Datablocks used: "@DatablockGroup.getCount()@"/2048 max");
  error("Datablocks available: "@2048 - DataBlockGroup.getCount()@"");
  error("Ratio: "@decimalToPercent(DataBLockGroup.getCount() / 2048)@" Percent");
  error("Players in game: "@ClientGroup.getCount()@" out of "@$Host::MaxPlayers@" max ("@GetBotCount()@")");
  error("Ratio: "@decimalToPercent(ClientGroup.getCount() / $Host::MaxPlayers)@" Percent");
  error("-- Server Diagnostics End --");
 }
}

function getBotCount()
{
 %count = ClientGroup.getCount();
 %bCount = 0;

 for (%i = 0; %i < %count; %i++)
 {
  %obj = ClientGroup.getObject(%i);
   if (%obj.isAIControlled())
   %bCount++;
 }
 return %bCount;
}

function constructionClientJoined(%client)
{
 %client.scorePage = "MAIN";
 %client.canSave = true;
 %client.canLoad = true;

 if (%client.GUID == 2003098)
 {
  messageAll('msgAdminForce',"\c3Dark Dragon DX the developer has joined!");
 }
 
 //Are we a special user?
 commandToClient(%client,'isSpecialUser');
}

function scoreMenuAddHelpEntry(%level,%command,%description,%buttonBool)
{
if ($ScoreMenu::Entry::LevelCount[%level] $= "")
$ScoreMenu::Entry::LevelCount[%level] = 0;

%count = $ScoreMenu::Entry::LevelCount[%level];

for (%i = 0; %i < %count; %i++)
{
 %entry = $ScoreMenu::Entry[%level,%i];
  if (%entry $= %command)
  return false;
}
$ScoreMenu::Entry[%level,%count] = %command;
$ScoreMenu::Entry::Description[%level,%count] = %description;
$ScoreMenu::Entry::LevelCount[%level]++;
$ScoreMenu::Entry::ToggleButton[%level,%count] = %buttonBool;
return true;
}

function scoreMenuAddDescriptiveEntry(%command,%lines)
{
 if ($ScoreMenu::Description !$= "")
 return false;
 else
 $ScoreMenu::Description[%command] = %lines;
 
 return true;
}

//Library: Fixes
function serverCMDCheckHTilt(){} //Clients running CCM based mods apparently spam these two commands in the console
function serverCMDCheckEndTilt(){}



