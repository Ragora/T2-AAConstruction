function chatcommands(%sender, %message) {
%cmd=getWord(%message,0);
%cmd=stripChars(%cmd,"/");
%cmd=stripChars(%cmd,"{");
%cmd=stripChars(%cmd,"]");
%count=getWordCount(%message);
%args=getwords(%message,1);
%cmd="cc" @ %cmd;
if (%cmd $="ccopen") //so u can call //open instead of //opendoor
   %cmd="ccopendoor";
%test = call(%cmd,%sender,%args);
if (!%test)
messageClient(%sender,'msgNoSuchCommand',"\c3No such command: \'"@strReplace(%cmd,"cc","")@"\'");
}

function ccopendoor(%sender,%args) {
%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj        = getword(%obj,0);
%dataBlock  = %obj.getDataBlock();
%className  = %dataBlock.className;
%cash       = %obj.amount;
%owner      = %obj.owner;
%obj.issliding = 0;
if (%obj.Collision == true) //if is a colition door
   return;                  //stop here
if (%obj.canmove == false) //if it cant move
   return;                  //stop here
if (%obj.isdoor != 1 && %hitobj.getdatablock().getname() !$="DeployedTdoor"){
   messageclient(%sender, 'MsgClient', '\c5No door in range.');
   return;
   }
if (!isobject(%obj)) {
   messageclient(%sender, 'MsgClient', '\c5No door in range.');
   return;
   }
if (%obj.powercontrol == 1) {
   messageclient(%sender, 'MsgClient', '\c5This door is controlled by a power supply.');
   return;
   }
   %pass = %args;
if (%obj.pw $= %pass) {
   if (%obj.toggletype ==1){
      if (%obj.moving $="close" || %obj.moving $="" || %going $="opening"){
         schedule(10,0,"open",%obj);
         }
      else if (%obj.moving $="open" || %going $="closeing"){
           schedule(10,0,"close",%obj);
           }
   }
   else
       schedule(10,0,"open",%obj);
}
if (%obj.pw !$= %pass)
   messageclient(%sender,'MsgClient',"\c2Password Denied.");

}

function ccsetdoorpass(%sender,%args){
%pos=%sender.player.getMuzzlePoint($WeaponSlot);
%vec = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos=vectoradd(%pos,vectorscale(%vec,100));
%obj=containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj=getword(%obj,0);
%dataBlock = %obj.getDataBlock();
%className = %dataBlock.className;
if (%classname !$= "door") {
messageclient(%sender, 'MsgClient', '\c2No door in range.');
return;
}
if (%obj.owner!=%sender && %obj.owner !$="")
messageclient(%sender, 'MsgClient', '\c2You do not own this door.');
if (!isobject(%obj))
messageclient(%sender, 'MsgClient', '\c2No door in range.');
if (%obj.Collision $= true) {
messageclient(%sender, 'MsgClient', '\c2Collision doors can not have passwords.');
return;
}
if(isobject(%obj) && %obj.owner==%sender) {
%pw=getword(%args,0);
%obj.pw=%pw;
messageclient(%sender, 'MsgClient', '\c2Password set, password is %1.',%pw);
}
}

function ccbf(%sender,%args) {
buyFavorites(%sender);
return 1;
}

function resetStruct()
{
 %count = deployables.getCount();
 for (%i = 0; %i < %count; %i++)
 {
  %obj = deployables.getObject(%i);
  %obj.ownerGUID = "";
  %obj.owner = 4937;
  %obj.ownerClient = 4937;
 }
}

