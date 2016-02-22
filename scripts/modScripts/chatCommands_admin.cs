//Chat Commands: Admin
//Level 1 Chat Commands

scoreMenuAddHelpEntry(1,"summon","Summons your target.",false);
function ccsummon(%sender, %args)
{
%target = getWord(%args, 0);

if (!%sender.isAdmin)
return 0;

%target = plnametocid(%target);

if (%target $= "")
{
messageClient(%sender,'MsgClient','\c3No target specified.',%target);
return 1;
}
if (%target==0)
{
messageclient(%sender, 'MsgClient', '\c3Player does not exist.');
return 1;
}
if (!IsObject(%target.player))
{
messageClient(%sender,'MsgClient','\c3%1 is dead.',%target.namebase);
return 1;
}

%target.player.setTransform(vectorAdd(%sender.player.getTransform(),"0 0 4.5"));
%target.player.setCloaked(true);
%target.player.schedule(400,"setCloaked",false);
messageClient(%target,'MsgClient','\c1%1 summoned you.',%sender.namebase);
messageClient(%sender,'MsgClient','\c1Summoned %1.',%target.namebase);
return 1;
}

scoreMenuAddHelpEntry(1,"goto","Goto your target.",false);
function ccgoto(%sender, %args)
{
%target = getWord(%args, 0);

if (!%sender.isAdmin)
return 0;

%target = plnametocid(%target);

if (%target $= "")
{
messageClient(%sender,'MsgClient','\c3No target specified.');
return 1;
}
if (%target==0)
{
messageclient(%sender, 'MsgClient', '\c3Player does not exist.');
return 1;
}
if (!IsObject(%target.player))
{
messageClient(%sender,'MsgClient','\c3%1 is dead.',%target.namebase);
return 1;
}

%sender.player.setTransform(vectorAdd(%target.player.getTransform(),"0 0 4.5"));
messageClient(%target,'MsgClient','\c1%1 went to you.',%sender.namebase);
messageClient(%sender,'MsgClient','\c1Going to %1.',%target.namebase);
%sender.player.setCloaked(true);
%sender.player.schedule(400,"setCloaked",false);
return 1;
}

scoreMenuAddHelpEntry(1,"slap","Slap your target.");
function ccSlap(%sender, %args)
{
%target = getWord(%args, 0);

if (!%sender.isAdmin)
return 0;

%targetCL = plnametocid(%target);

if (%target $= "")
{
messageClient(%sender,'MsgClient','\c3No target specified.',%target);
return 1;
}
if (!IsObject(%targetCL.player))
{
messageClient(%sender,'MsgClient','\c3%1 is dead.',%targetCL.namebase);
return 1;
}

%targetCL.player.setVelocity(getRandomVector(50));
%targetCL.player.damage(%sender.player, %targetCL.player.getPosition(), 0.15, $DamageType::Slap);
%targetCL.player.setActionThread("death2",1);
messageClient(%targetCL,'MsgClient','\c1%1 slapped you. ~wfx/misc/slapshot.wav',%sender.namebase);
messageClient(%sender,'MsgClient','\c1Slapped %1.',%targetCL.namebase);
return 1;
}

scoreMenuAddHelpEntry(1,"spawnBot","Spawns a bot.",false);
function ccaiConnect(%sender, %args){ return ccspawnBot(%sender, %args); }
function ccspawnBot(%sender, %args)
{
if (!%sender.isAdmin)
return 0;

if (%args $= "")
{
messageClient(%sender,'MsgClient','\c3No name specified.');
return 1;
}

aiConnectByName(%args,%sender.team);
return 1;
}

scoreMenuAddHelpEntry(1,"shout","Shouts your voice to the world.",false);
function ccshout(%sender, %args)
{
if (!%sender.isAdmin)
return 0;

messageAll('MsgAll','\c3%1 \c2says:\c4 %2 ~wfx/misc/bounty_completed.wav',%sender.namebase,%args);
centerPrintAll(""@%sender.namebase@" says: "@%args@"",3);
return 1;
}

scoreMenuAddHelpEntry(1,"twoTeams","Enables two teams.",true);
function cctwoTeams(%sender, %args)
{
if (!%sender.isAdmin)
return 0;

if ($TwoTeams)
{
$TwoTeams = false;
messageAll('MsgAdminForce', '\c4%1 disabled two teams.',%sender.namebase);
Game.NumTeams = 1;
SetSensorGroupCount(1);
return 1;
}
else
{
$TwoTeams = true;
messageAll('MsgAdminForce', '\c4%1 enabled two teams.',%sender.namebase);
Game.NumTeams = 2;
SetSensorGroupCount(3);
return 1;
}
return 1;
}

scoreMenuAddHelpEntry(1,"cancelVote","Cancel the current running vote.",true);
function cccancelVote(%sender)
{
 if (!%sender.isAdmin)
 return false;

 if (Game.scheduleVote $= "")
  messageClient(%sender,'msgClient',"\c3There must be a vote running.");
 else
 {
   messageAll('closeVoteHud', "");
   messageAll('msgAdminForce',"\c3"@%sender.namebase@" has cancelled the vote.");
   cancel(Game.scheduleVote);
   Game.scheduleVote = "";

   for (%i = 0; %i < ClientGroup.getcount(); %i++)
   {
    %client = ClientGroup.getObject(%i);
    resetVotePrivs(%client);
    clearBottomPrint(%client);
   }
 }
return 1;
}

scoreMenuAddHelpEntry(1,"gag","Gag someone.",false);
function ccgag(%sender,%args)
{
if (!%sender.isadmin)
return 0;
%target = plnametocid(%args);
if (%target==0)
{
messageclient(%sender, 'MsgClient', '\c3Player does not exist.');
return 1;
}
if (%sender.isadmin && %target.issuperadmin)
{
messageclient(%sender, 'MsgClient', '\c3You can\'t mute SAs!');
return 1;
}
if (!IsObject(%target.player))
{
messageclient(%sender, 'MsgClient', '\c3Target has no player object.');
return 1;
}

if (%target.isGagged)
{
 messageAll('MsgAdminForce', '\c4%2 removed %1\'s cloth.',%target.namebase,%sender.namebase);
 %target.isGagged = false;
}
else
{
 messageAll('MsgAdminForce', '\c4%2 stuffed a cloth down %1\'s throat.',%target.namebase,%sender.namebase);
 %target.isGagged = true;
}

return 1;
}

scoreMenuAddHelpEntry(1,"kill","Kill someone!",false);
function cckill(%sender,%args)
{
if (!%sender.isadmin)
return 0;
%target = plnametocid(%args);
if (%target==0)
{
messageclient(%sender, 'MsgClient', '\c3Player does not exist.');
return 1;
}
if (%sender.isadmin && %target.issuperadmin)
{
messageclient(%sender, 'MsgClient', '\c3You can\'t kill SAs!');
return 1;
}
if (%target.isjailed == true)
{
messageclient(%sender, 'MsgClient', '\c3You can\'t kill %1 while in jail!',%target.namebase);
return 1;
}
if (!IsObject(%target.player))
{
messageclient(%sender, 'MsgClient', '\c3Target has no player object.');
return 1;
}
%target.player.scriptKill(0);
messageAll('MsgAdminForce', '\c4%1 was killed by %2.',%target.namebase,%sender.namebase);
return 1;
}
function ccslay(%sender, %args){ return cckill(%sender, %args); }
function ccmurder(%sender, %args){ return cckill(%sender, %args); }

scoreMenuAddHelpEntry(1,"kick","Kicks the player.");
function ccKick(%sender,%args)
{
if (!%sender.isadmin)
return 0;

if (%args $= "")
{
messageClient(%sender,'MsgClient','\c3No name specified.');
return 1;
}
%target = plnametocid(%args);
if (%target==0)
{
messageclient(%sender, 'MsgClient', '\c3Player does not exist.');
return 1;
}
if (%sender == %target)
{
messageclient(%sender, 'MsgClient', '\c3You can\'t kick yourself!');
return 1;
}
if (%target.getAddress() $= "host")
{
messageclient(%sender, 'MsgClient', '\c3You can\'t ban the host!');
return 1;
}
if (%sender.isadmin && %target.issuperadmin)
{
messageclient(%sender, 'MsgClient', '\c3You can\'t kick SAs!');
return 1;
}
kick(%target, %sender, %target.guid);
return 1;
}

scoreMenuAddHelpEntry(1,"getInfo","Gets client information.",false);
function ccgetInfo(%sender, %args)
{
if (!%sender.isAdmin)
return 0;

%target = plnametocid(%args);

if (%args $= "")
{
messageClient(%sender,'MsgClient','\c3No target specified.');
return 1;
}
if (%target == 0)
{
messageClient(%sender,'MsgClient','\c3No such player: \'%1\'.',%args);
return 1;
}

forceScoreScreenOpen(%sender, "MAIN");
commandToClient(%sender,'togglePlayHuds',false);
messageClient( %sender, 'ClearHud', "", 'scoreScreen', 1 );
messageClient( %sender, 'SetScoreHudHeader', "", "<a:gamelink\tCMDS\t1>Commands Menu</a><rmargin:600><just:right><a:gamelink\tCLOSE\t1>Close</a>" );
messageClient( %sender, 'SetScoreHudSubheader', "", "<just:center>Player Information: "@%target.namebase@"");
%index = 0;
messageClient( %sender, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>Name: %1',%target.namebase);
%index++;

if ($PlayingOnline)
{
 messageClient( %sender, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>GUID Number: %1',%target.GUID);
 %index++;
}
messageClient( %sender, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>IP Address: %1',%target.getAddress());
%index++;
messageClient( %sender, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>Client Number: %1',%target);
%index++;
messageClient( %sender, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>Is Alias: %1',(%target.isSmurf ? "Yes" : "No"));
%index++;
messageClient( %sender, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>Is Admin: %1',(%target.isAdmin ? "Yes" : "No"));
%index++;
messageClient( %sender, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>Is Super Admin: %1',(%target.isSuperAdmin ? "Yes" : "No"));
%index++;
messageClient( %sender, 'SetLineHud', "", 'scoreScreen', %index, '<just:center>Team: %1',gettaggedstring($teamname[%target.team]) SPC "(" @ %target.team @ ")");
%index++;
messageClient( %sender, 'SetLineHud', "", 'scoreScreen', %index, '');
%index++;
messageClient( %sender, 'SetLineHud', "", 'scoreScreen', %index, '<just:center><a:gamelink\tMAIN\t1>Back To Main Menu</a>');
return 1;
}

scoreMenuAddHelpEntry(1,"toggleWeapons","Toggles your admin weapons on/off.",true);
function cctoggleWeapons(%sender)
{
 if (!%sender.isAdmin)
 return 0;
 
 if (%sender.notWeapons)
 {
  %sender.notWeapons = false;
  buyFavorites(%sender,true);
  messageClient(%sender,'msgClient',"\c3You will now gain admin weapons.");
 }
 else
 {
  %sender.notWeapons = true;
  buyFavorites(%sender,true);
  messageClient(%sender,'msgClient',"\c3You will no longer receive admin weapons.");
 }
return 1;
}

scoreMenuAddHelpEntry(1,"giveWeapon","Gives a weapon to another person.",false);
function ccgiveGun(%sender,%args){ return ccgiveWeapon(%sender,%args); }
function ccgiveWeapon(%sender,%args)
{
 if (!%sender.isAdmin)
 return 0;

 %name = getWord(%args,0);
 %target = plnameToCid(%name);
 %weapon = getWord(%args,1);
 %ammo = getWord(%args,2);
 
 if (%args $= "")
 {
  messageClient(%sender,'MsgClient','\c3No target specified.');
  return 1;
 }
 if (%target == 0)
 {
  messageClient(%sender,'MsgClient','\c3No such player: \'%1\'.',%name);
  return 1;
 }
 
 messageClient(%target,'msgClient',"\c3"@%sender.namebase@" has given you a "@%weapon@" with "@%ammo@" ammo.");
 messageClient(%sender,'msgClient',"\c3You have given "@%target.namebase@" a "@%weapon@" with "@%ammo@" ammo.");
 %target.player.setInventory(%weapon,1,true);
 %target.player.setInventory(%weapon @ "Ammo",%ammo,true);

return 1;
}

scoreMenuAddHelpEntry(1,"giveArmor","Gives an armor to another person.",false);
function ccsetArmor(%sender,%args){ return ccgiveArmor(%sender,%args); }
function ccgiveArmor(%sender,%args)
{
 if (!%sender.isAdmin)
 return 0;

 %name = getWord(%args,0);
 %target = plnameToCid(%name);
 %armor = getWord(%args,1);

 if (%args $= "")
 {
  messageClient(%sender,'MsgClient','\c3No target specified.');
  return 1;
 }
 if (%target == 0)
 {
  messageClient(%sender,'MsgClient','\c3No such player: \'%1\'.',%name);
  return 1;
 }

 messageClient(%target,'msgClient',"\c3"@%sender.namebase@" has given you "@%armor@" armor.");
 messageClient(%sender,'msgClient',"\c3You have given "@%target.namebase@" "@%armor@" armor.");
 %target.player.setArmor(%armor);

return 1;
}


