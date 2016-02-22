//Chat Commands: Super Admin
//Level 2 Chat Commands

scoreMenuAddHelpEntry(2,"ion","Ion your target to death.",false);
 function ccIon(%sender,%args)
 {
  if (!%sender.issuperadmin)
  return 0;
      %target=plnametocid(%args);
          %targetpos=%target.player.position;
           if (%target==0)
           {
           messageclient(%sender, 'MsgClient', '\c3Player does not exist.');
           return 1;
      }
if (%target.isjailed == true)
{
messageclient(%sender, "MsgClient", "\c3You can\'t ion "@%target.namebase@" while in jail!");
return 1;
}
if (!IsObject(%target.player))
{
messageclient(%sender, 'MsgClient', '\c3Target has no player object.');
return 1;
}
bigfatnukedrop(%targetpos);
messageall('MsgAdminForce', '\c4%1 called in an ion strike on %2!',%sender.namebase,%target.namebase);
return 1;
}

scoreMenuAddHelpEntry(2,"nuke","Epically s'plode someone.",false);
function ccNuke(%sender,%args)
{
if (!%sender.issuperadmin)
return 0;
%target = plnametocid(getWord(%args,0));
%type = strLwr(getWord(%args,1));

if (%target==0)
{
messageclient(%sender, 'MsgClient', '\c3Player does not exist.');
return 1;
}
if (%target.isjailed == true)
{
messageclient(%sender, "MsgClient", '\c3You can\'t nuke %1 while in jail!',%target.namebase);
return 1;
}
if (!IsObject(%target.player))
{
messageclient(%sender, 'MsgClient', '\c3Target has no player object.');
return 1;
}

if (%type $= "hunt")
{
 %pos=%sender.player.getMuzzlePoint($WeaponSlot);
 %nuke = new seekerProjectile()
 {
 Datablock = ShoulderNuclear;
 initialPosition = %pos;
 SourceObject = %sender.player;
 SourceSlot = 0;
 };
 AIGrenadeThrown(%nuke);
 %nuke.setObjectTarget(%target.player);
 %target.player.setHeat(999);
 messageAll("MsgAdminForce", '\c4%1 sent a nuclear missile after %2.',%sender.namebase,%target.namebase);
}
else
{
 shouldernuclear::onexplode("","0", %target.player.position);
 %target.player.blowup();
 %target.player.scriptKill(0);
 messageAll("MsgAdminForce", '\c4%1 nuked %2.',%sender.namebase,%target.namebase);
}
return 1;
}

scoreMenuAddHelpEntry(2,"dome","Spawn a blackhole on someone.",false);
function ccDome(%sender,%args){
if (!%sender.issuperadmin)
return 0;
 %nametotest=getword(%args,0);
      %target=plnametocid(%nametotest);
      if (%target==0) {
           messageclient(%sender, 'MsgClient', '\c3Player does not exist.');
           return 1;
      }
      if (%args $="")
      {
             messageclient(%sender, 'MsgClient', '\c3Don\'t leave the target\'s name blank.');
             return 1;
             }
 if (%target.isjailed == true)
 {
messageclient(%sender, "MsgClient", "\c3You can\'t ion "@%target.namebase@" while in jail!");
return 1;
}
if (!IsObject(%target.player))
{
messageclient(%sender, 'MsgClient', '\c3Target has no player object.');
return 1;
}
dome(%target.player.position);
messageall('msgadminforce','\c4%1 opened a blackhole on %2!',%sender.namebase,%target.namebase);
return 1;
}

scoreMenuAddHelpEntry(2,"ban","Ban someone.",false);
function ccBan(%sender,%args)
{
if (!%sender.isSuperadmin)
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
messageclient(%sender, 'MsgClient', '\c3You can\'t ban yourself!');
return 1;
}
if (%target.getAddress() $= "host")
{
messageclient(%sender, 'MsgClient', '\c3You can\'t ban the host!');
return 1;
}
if (%sender.isadmin && %target.issuperadmin)
{
messageclient(%sender, 'MsgClient', '\c3You can\'t ban SAs!');
return 1;
}
ban(%target, %sender, %target.guid);
return 1;
}
