// DisplayName = Construction

//--- GAME RULES BEGIN ---
// Build.
//--- GAME RULES END ---

// spam fix
function ConstructionGame::AIInit(%game) {
	//call the default AIInit() function
	AIInit();
}

function ConstructionGame::allowsProtectedStatics(%game)
{
	return true;
}

function ConstructionGame::clientMissionDropReady(%game, %client)
{
	messageClient(%client, 'MsgClientReady',"", "SinglePlayerGame");
	messageClient(%client, 'MsgMissionDropInfo', '\c0You are in mission %1 (%2).', $MissionDisplayName, $MissionTypeDisplayName, $ServerName );
	DefaultGame::clientMissionDropReady(%game, %client);
}

function ConstructionGame::onAIRespawn(%game, %client)
{
   //add the default task
	if (! %client.defaultTasksAdded)
	{
		%client.defaultTasksAdded = true;
	   %client.addTask(AIPickupItemTask);
	 //  %client.addTask(AIUseInventoryTask);
	   %client.addTask(AITauntCorpseTask);
		%client.addTask(AIEngageTurretTask);
		%client.addTask(AIDetectMineTask);
		%client.addTask(AIBountyPatrolTask);
		%client.bountyTask = %client.addTask(AIBountyEngageTask);
	}
 
//AI's get Construction toolz too
%client.player.clearInventory();
%client.player.setInventory("ConstructionTool",1,true);
%client.player.setInventory("MergeTool",1,true);
%client.player.setInventory("spineDeployable",1,true);
%client.player.use("ConstructionTool");

   //set the inv flag
   %client.spawnUseInv = true;
}

function ConstructionGame::updateKillScores(%game, %clVictim, %clKiller, %damageType, %implement) {
	if (%game.testKill(%clVictim, %clKiller)) { //verify victim was an enemy
		%game.awardScoreKill(%clKiller);
		%game.awardScoreDeath(%clVictim);
	}
	else if (%game.testSuicide(%clVictim, %clKiller, %damageType))  //otherwise test for suicide
		%game.awardScoreSuicide(%clVictim);
}

function ConstructionGame::timeLimitReached(%game) {
	logEcho("game over (timelimit)");
	%game.gameOver();
	cycleMissions();
}

function ConstructionGame::scoreLimitReached(%game) {
	logEcho("game over (scorelimit)");
	%game.gameOver();
	cycleMissions();
}

function ConstructionGame::gameOver(%game) {
	//call the default
	DefaultGame::gameOver(%game);

	//send the winner message
	%winner = "";
	if ($teamScore[1] > $teamScore[2])
		%winner = %game.getTeamName(1);
	else if ($teamScore[2] > $teamScore[1])
		%winner = %game.getTeamName(2);

	if (%winner $= 'Storm')
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.stowins.wav" );
	else if (%winner $= 'Inferno')
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.infwins.wav" );
	else if (%winner $= 'Starwolf')
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.swwin.wav" );
	else if (%winner $= 'Blood Eagle')
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.bewin.wav" );
	else if (%winner $= 'Diamond Sword')
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.dswin.wav" );
	else if (%winner $= 'Phoenix')
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.pxwin.wav" );
	else
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.gameover.wav" );

	messageAll('MsgClearObjHud', "");
	for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
		%client = ClientGroup.getObject(%i);
		%game.resetScore(%client);
	}
	for(%j = 1; %j <= %game.numTeams; %j++)
		$TeamScore[%j] = 0;
}

function ConstructionGame::clientMissionDropReady(%game, %client)
{
   messageClient(%client, 'MsgClientReady',"", "SinglePlayerGame"); //Load SP game hud.

   %game.resetScore(%client);

   messageClient(%client, 'MsgMissionDropInfo', '\c0You are in mission %1 (%2).', $MissionDisplayName, $MissionTypeDisplayName, $ServerName );

   DefaultGame::clientMissionDropReady(%game, %client);
}

function ConstructionGame::ResetScore(%game) { return %game; }
function ConstructionGame::vehicleDestroyed(%game, %vehicle, %destroyer) { return %game SPC %vehicle SPC %destroyer; }

//ScoreMenu Start
function setSaveTag(%client,%val)
{
 %client.canSave = %val;
 if (%val == 1)
 {
  messageClient(%client,'msgClient',"\c3You may now save again.");
 }
}

function setLoadTag(%client,%val)
{
 %client.canLoad = %val;
 if (%val == 1)
 {
  messageClient(%client,'msgClient',"\c3You may now load again.");
 }
}

function ConstructionGame::processGameLink(%game, %client, %arg1, %arg2, %arg3, %arg4, %arg5)
{
 
 if (%arg1 $= "SVESLT")
 {
  if (!%client.canSave)
  {
   messageClient(%client,'MsgClient',"\c3You are unable to save right now.");
   ForceScoreHudClose(%client);
   return;
  }
  %file = %arg2 @ ".cs";
  ForceScoreHudClose(%client);
  messageClient(%client,'MsgClient',"\c3Building saved to" SPC %arg2 @ ".");
  
  if ($PlayingOnline)
  saveBuilding(%client,9999,%client.guid @ "/" @ %file,1,0);
  else
  saveBuilding(%client,9999,%client.namebase @ "/" @ %file,1,0);
  
  %count = Deployables.getCount();
  %pieces = 0;
   for (%i = 0; %i < %count; %i++)
   {
    %obj = Deployables.getObject(%i);
     if (%obj.getOwner() == %client)
     %pieces++;
   }
  $Host::PieceCount[%client.guid,%arg2] = %pieces;
  export("$Host::*","prefs/serverPrefs.cs",false);
  if ($Host::ChatAI::Enabled)
  messageAll('MsgAdminForce',"\c4" @ $Host::ChatAI::Name @ ":" SPC %client.namebase SPC "has saved a structure.");
  %client.canSave = false;
  schedule(60000,0,"setSaveTag",%client,true);
  return;
 }
 else if (%arg1 $= "LDSLT")
 {
  if (!%client.canLoad)
  {
   messageClient(%client,'MsgClient',"\c3You are unable to load right now.");
   ForceScoreHudClose(%client);
   return;
  }
  %file = %arg2 @ ".cs";
  ForceScoreHudClose(%client);
  messageClient(%client,'MsgClient',"\c3Building" SPC %arg2 SPC "loaded.");
  
  if ($PlayingOnline)
  loadBuilding(%client.guid @ "/" @ %file);
  else
  loadBuilding(%client.namebase @ "/" @ %file);
    
  %client.canLoad = false;
  schedule(60000,0,"setLoadTag",%client,true);
  if ($Host::ChatAI::Enabled)
  messageAll('MsgAdminForce',"\c4" @ $Host::ChatAI::Name @ ":" SPC %client.namebase SPC "has loaded a structure of" SPC $Host::PieceCount[%client.guid,%arg2] SPC "pieces.");
 }
 else if (%arg1 $= "RENAME")
 {
  ForceScoreHudClose(%client);
  %client.isNaming = true;
  %client.nameSlot = %arg2;
  messageClient(%client,'msgClient',"\c3The next global message sent will serve as the new name.");
  return;
 }
 else if (%arg1 $= "CALL")
 {
  ForceScoreHudClose(%client);
  call("cc" @ %arg2,%client);
 }
ScoreHudUpdate(%client,%arg1);
}

function ScoreHudUpdate(%client,%page,%refresh)
{
messageClient( %client, 'ClearHud', "", 'scoreScreen', 1 );
messageClient( %client, 'SetScoreHudHeader', "", "<a:gamelink\tCMDS\t1>Commands Menu</a><rmargin:600><just:right><a:gamelink\tCLOSE\t1>Close</a>" );

switch$ (%page)
{
case "CLOSE": ForceScoreHudClose(%client);
case "MAIN": %client.scorePage = "MAIN";
messageClient( %client, 'SetScoreHudSubheader', "", "<just:center>Main Menu");
%index = 0;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>-Advanced Architecture Construction Mod Commands-');
%index++;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center><a:gamelink\tPCCNT\t1>- Piece Count</a>');
%index++;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center><a:gamelink\tCMDS\t1>- Chat Commands</a>');
%index++;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center><a:gamelink\tPCESAVE\t1>- Content Saving</a>');

case "PCCNT":
%client.scorePage = "PCCNT";
messageClient( %client, 'SetScoreHudSubheader', "", "<just:center>Piece Count");
%index = 0;

%ccount = ClientGroup.getCount();
for (%i = 0; %i < %ccount; %i++)
{
 %clid = ClientGroup.getObject(%i);
 %count = Deployables.getCount();
 %pieces[%clid] = 0;
 for (%j = 0; %j < %count; %j++)
 {
  %obj = Deployables.getObject(%i);
  if (%obj.getOwner() == %clid)
  %pieces[%clid]++;
 }
  messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>%1 - %2 Pcs.',%clid.namebase,%pieces[%clid]);
  %index++;
}

messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '');
%index++;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center><a:gamelink\tMAIN\t1>Back To Main Menu</a>');
%index++;

case "DeployedSpine":
%client.scorePage = "DeployedSpine";
messageClient( %client, 'SetScoreHudSubheader', "", "Object Description: Light Support Beam");
%index = 0;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, 'Object Name: Light Support Beam');
%index++;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, 'Object Scale: %1',%client.object.scale);
%index++;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '');
%index++;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, 'Object Description: The jack of all trades.');
%index++;

case "PCESAVE":  %client.scorePage = "PCESAVE";
messageClient( %client, 'SetScoreHudSubheader', "", "<just:center>Content Saving System");
%index = 0;

if ($PlayingOnline)
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>-Available Save Slots-');
else
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>-Available OFFLINE Save Slots-');
%index++;


for (%i = 1; %i < 11; %i++)
{

if ($PlayingOnline)
%file = "Buildings/" @ %client.guid @ "/Slot"@%i@".cs";
else
%file = "Buildings/" @ %client.namebase @ "/Slot"@%i@".cs";

if (IsFile(%file))
{
 if ($Host::FileName[%client.guid,"Slot" @ %i] !$= "")
 messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, "<just:center>Slot "@%i@": <a:gamelink\tRENAME\tSlot"@%i@"\t1>\x22"@$Host::FileName[%client.guid,"Slot" @ %i]@"\x22</a> <color:FF0033><a:gamelink\tSVESLT\tSlot"@%i@"\t1>[SAVE]</a> <color:00FF00><a:gamelink\tLDSLT\tSlot"@%i@"\t1>[LOAD]</a> <color:CCCCCC>("@$Host::PieceCount[%client.guid,"Slot" @ %i]@" Pcs.)");
 else
 messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, "<just:center>Slot "@%i@": <a:gamelink\tRENAME\tSlot"@%i@"\t1>Click to Name me</a> <color:FF0033><a:gamelink\tSVESLT\tSlot"@%i@"\t1>[SAVE]</a> <color:00FF00><a:gamelink\tLDSLT\tSlot"@%i@"\t1>[LOAD]</a> <color:CCCCCC>("@$Host::PieceCount[%client.guid,"Slot" @ %i]@" Pcs.)");
}
else
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, "<just:center>Slot "@%i@": <color:FF0000>EMPTY <color:00FF00><a:gamelink\tSVESLT\tSlot"@%i@"\t1>[SAVE]</a>");
%index++;
}
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '');
%index++;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center><a:gamelink\tMAIN\t1>Back To Main Menu</a>');
%index++;


case "CMDS": %client.scorePage = "CMDS";

messageClient( %client, 'SetScoreHudSubheader', "", "<just:center>Available Chat Commands");
%index = 0;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>-Public Chat Commands-');
%index++;

%count = $ScoreMenu::Entry::LevelCount[0];
%count--;
for (%i = 0; %i <= %count; %i++)
{

if ($ScoreMenu::Entry::ToggleButton[0,%i])
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center><a:gamelink\tCALL\t%1\t1>/%1</a> - %2',$ScoreMenu::Entry[0,%i],$ScoreMenu::Entry::Description[0,%i]);
else
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>/%1 - %2',$ScoreMenu::Entry[0,%i],$ScoreMenu::Entry::Description[0,%i]);
%index++;
}

if (%client.isAdmin)
{
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '');
%index++;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>-Admin Chat Commands-');
%index++;

%count = $ScoreMenu::Entry::LevelCount[1];
%count--;
for (%i = 0; %i <= %count; %i++)
{
if ($ScoreMenu::Entry::ToggleButton[1,%i])
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center><a:gamelink\tCALL\t%1\t1>/%1</a> - %2',$ScoreMenu::Entry[1,%i],$ScoreMenu::Entry::Description[1,%i]);
else
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>/%1 - %2',$ScoreMenu::Entry[1,%i],$ScoreMenu::Entry::Description[1,%i]);
%index++;
}
}

if (%client.isSuperadmin)
{
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '');
%index++;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>-Super Admin Chat Commands-');
%index++;
%count = $ScoreMenu::Entry::LevelCount[2];
%count--;
for (%i = 0; %i <= %count; %i++)
{
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>/%1 - %2',$ScoreMenu::Entry[2,%i],$ScoreMenu::Entry::Description[2,%i]);
%index++;
}
}

if (isSpecialUser(%client))
{
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '');
%index++;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>-Special Chat Commands-');
%index++;
%count = $ScoreMenu::Entry::LevelCount[4];
%count--;
for (%i = 0; %i <= %count; %i++)
{
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>]%1 - %2',$ScoreMenu::Entry[4,%i],$ScoreMenu::Entry::Description[4,%i]);
%index++;
}
}

messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '');
%index++;
messageClient( %client, 'SetLineHud', "", 'scoreScreen', %index, '<just:center><a:gamelink\tMAIN\t1>Back To Main Menu</a>');
%index++;
return;

default:
scoreHudUpdate(%client,"MAIN");
}
}

function ConstructionGame::updateScoreHud(%game, %client, %tag)
{
if (%client.scorePage $= "CLOSE")
ScoreHudUpdate(%client,"MAIN");
else if (%client.scorePage $= "MAIN")
ScoreHudUpdate(%client,"MAIN");
}

function forceScoreHudClose(%client)
{
serverCmdHideHud(%client, 'scoreScreen');
commandToClient(%client, 'setHudMode', 'Standard', "", 0);
%client.scorePage = "MAIN";
}

function forceScoreScreenOpen(%client,%page)
{
messageClient(%client, 'OpenHud', "", 'scoreScreen' SPC "scoreScreen");
ConstructionGame::processGameLink(Game, %client, %page);
}


