//Chat Commands: Public
//Level 0 Chat Commands

function ccHelp(%sender,%args){ commandToClient(%sender,'togglePlayHuds',false); forceScoreScreenOpen(%sender,"CMDS"); return 1; }

scoreMenuAddHelpEntry(0,"buyFavorites","Buys your loadout.",true);
function ccbuyFavorites(%sender){ return ccBf(%sender); }
function ccbuyFavs(%sender){ return ccBf(%sender); }

scoreMenuAddHelpEntry(0,"setName","Name the object you are looking at.",false);
function ccname(%sender, %args){ return ccSetName(%sender,%args); }
function ccobjName(%sender, %args){ return ccSetName(%sender,%args); }
function ccobjectName(%sender, %args){ return ccSetName(%sender,%args); }
function ccsetName(%sender, %args)
{
%pos = %sender.player.getMuzzlePoint($WeaponSlot);
%vec = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos = vectoradd(%pos,vectorscale(%vec,100));
%obj = containerraycast(%pos,%targetpos,$TypeMasks::StaticShapeObjectType | $TypeMasks::VehicleObjectType,%sender.player);
%obj = getword(%obj,0);

if (!%obj)
{
messageclient(%sender,'MsgClient',"\c3No deployable in range.");
return 1;
}

if (!Deployables.IsMember(%obj) && (%obj.getClassName() !$= "FlyingVehicle" && %obj.getClassName() !$= "HoverVehicle" && %obj.getClassName() !$= "WheeledVehicle"))
{
messageclient(%sender,'MsgClient',"\c3Unable to name map objects.");
return 1;
}

if (%obj.getDatablock().getname() $= "DeployedWaypoint")
{
 if (%sender.isGagged)
 {
  messageClient(%sender,'msgClient',"\c3You have a cloth lodged in your throat.");
  return;
 }
%obj.waypoint.delete();

%obj.waypoint = new waypoint() {
Datablock = WaypointMarker;
Team = %sender.team;
Position = %obj.getPosition();
Name = %args;
};

messageclient(%sender,'MsgClient','\c3Waypoint named to \'%1\'.',%args);
}
else
{
SetTargetName(%obj.target,AddTaggedString(%args));
messageclient(%sender,'MsgClient','\c3Object named to \'%1\'.',%args);
}

return 1;
}

scoreMenuAddHelpEntry(0,"cloak","Cloaks the piece you are looking at.",true);
function ccCloak(%sender,%args)
{
 %pos=%sender.player.getMuzzlePoint($WeaponSlot);
 %vec = %sender.player.getMuzzleVector($WeaponSlot);
 %targetpos=vectoradd(%pos,vectorscale(%vec,100));
 %obj=containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
 %obj=getword(%obj,0);

 if (%sender.isAdmin && %args $= "self")
 {
  if (%sender.player.isCloaked)
  {
   %sender.player.isCloaked = false;
   %sender.player.setCloaked(false);
  }
  else
  {
   %sender.player.isCloaked = true;
   %sender.player.setCloaked(true);
  }
  return 1;
 }

 if (!%obj)
 {
  messageclient(%sender, 'MsgClient', '\c3No piece in range.');
  return 1;
 }
 if (%obj.getOwner() != %sender)
 {
  messageclient(%sender, 'MsgClient', '\c3You do not own this piece.');
  return 1;
 }

 if (%obj.isCloaked)
 {
  %obj.isCloaked = false;
  %obj.setCloaked(false);
 }
 else
 {
 %obj.isCloaked = true;
 %obj.setCloaked(true);
 }

 return 1;
}

scoreMenuAddHelpEntry(0,"fade","Fades the piece you are looking at.",true);
function ccFade(%sender,%args)
{
if (!%sender.isAdmin)
return 0;

%pos=%sender.player.getMuzzlePoint($WeaponSlot);
%vec = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos=vectoradd(%pos,vectorscale(%vec,100));
%obj=containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj=getword(%obj,0);


if (%args $= "self")
 {
  if (%sender.player.isFaded)
  {
   %sender.player.isFaded = false;
   %sender.player.startFade(0,0,0);
  }
  else
  {
   %sender.player.isFaded = true;
   %sender.player.startFade(1,0,1);
  }
  return 1;
 }

if (!%obj)
{
messageclient(%sender, 'MsgClient', '\c3No piece in range.');
return 1;
}
if (%obj.getOwner() != %sender)
{
messageclient(%sender, 'MsgClient', '\c3Not your piece.');
return 1;
}

if (%obj.isFaded)
{
%obj.isFaded = false;
%obj.startFade(0,0,0);
}
else
{
%obj.isFaded = true;
%obj.startFade(1,0,1);
}

return 1;
}

scoreMenuAddHelpEntry(0,"delMyPieces","Deletes all of your deployed pieces.",true);
function ccdelMyPieces(%sender)
{
 %count = Deployables.getCount();
  for (%i = 0; %i < %count; %i++)
  {
   %obj = Deployables.getObject(%i);
    if (%obj.getOwner() == %sender)
   	%obj.getDataBlock().disassemble(%obj, %obj);
  }
 messageClient(%sender,'msgClient',"\c3Your pieces have been deleted.");
 return 1;
}

scoreMenuAddHelpEntry(0,"me","Displays a message as an action of yourself.");
function ccMe(%sender,%args)
{
 if (%sender.isGagged)
 {
  messageClient(%sender,'msgClient',"\c3You have a cloth lodged in your throat.");
  return;
 }

 messageAll('msgAll','%1 %2',%sender.namebase, %args);
 return 1;
}

function ccMe1(%sender,%args)
{
 if (%sender.isGagged)
 {
  messageClient(%sender,'msgClient',"\c3You have a cloth lodged in your throat.");
  return;
 }

 messageAll('msgAll','\c1%1 %2',%sender.namebase, %args);
 return 1;
}

function ccMe2(%sender,%args)
{
 if (%sender.isGagged)
 {
  messageClient(%sender,'msgClient',"\c3You have a cloth lodged in your throat.");
  return;
 }

 messageAll('msgAll','\c2%1 %2',%sender.namebase, %args);
 return 1;
}

function ccMe3(%sender,%args)
{
 if (%sender.isGagged)
 {
  messageClient(%sender,'msgClient',"\c3You have a cloth lodged in your throat.");
  return;
 }

 messageAll('msgAll','\c3%1 %2',%sender.namebase, %args);
 return 1;
}

function ccMe4(%sender,%args)
{
 if (%sender.isGagged)
 {
  messageClient(%sender,'msgClient',"\c3You have a cloth lodged in your throat.");
  return;
 }

 messageAll('msgAll','\c4%1 %2',%sender.namebase, %args);
 return 1;
}

function ccMe5(%sender,%args)
{
 if (%sender.isGagged)
 {
  messageClient(%sender,'msgClient',"\c3You have a cloth lodged in your throat.");
  return;
 }

 messageAll('msgAll','\c5%1 %2',%sender.namebase, %args);
 return 1;
}

scoreMenuAddHelpEntry(0,"hover","Allows you to hover.",true);
function ccHover(%sender,%args)
{
 if (!isObject(%sender.player) || %sender.player.getState() $= "dead")
 {
  messageClient(%sender,'msgClient',"\c3No player.");
  return 1;
 }
 else if (%sender.isHovering)
 {
  %sender.isHovering = false;
  if (IsObject(%sender.player.hoverObj))
  %sender.player.hoverObj.delete();
  messageClient(%sender,'msgClient',"\c3Not hovering anymore.");
 }
 else
 {
  %sender.isHovering = true;
  %args = strLwr(%args);
  if (%args $= "old")
  %sender.player.advancedHover = true;
  else
  %sender.player.advancedHover = false;
  hoverLoop(%sender.player);
  messageClient(%sender,'msgClient',"\c3Now hovering.");
 }
return 1;
}


function hoverLoop(%player,%height)
{
if (!isObject(%player) || %player.getState() $= "dead" || !%player.client.isHovering)
{
cancel(%player.hoverloop);
return;
}

%pos = %player.getPosition();
if (%height $= "")
%height = getWord(vectorAdd(%player.getPosition(),"0 0 -2"),2);

if (!IsObject(%player.hoverObj))
{
%player.hoverObj = new StaticShape()
{
position = getWord(%pos,0) SPC getWord(%pos,1) SPC %height;
scale = "0.3 0.3 1";
rotation = %player.getRotation();
Datablock = "DeployedSpine";
};
%player.hoverObj.setCloaked(true);
}

%player.hoverObj.setRotation(%player.getRotation());
if (!%player.advancedHover)
%player.hoverObj.setPosition(vectorAdd(%pos,"0 0 -0.5"));
else
{
%player.hoverObj.setPosition(getWord(%pos,0) SPC getWord(%pos,1) SPC %height);
}

cancel(%player.hoverloop);
%player.hoverloop = schedule(10,0,"hoverLoop",%player,%height);
}

scoreMenuAddHelpEntry(0,"vehLock","Locks your car.",true);
function cclockVeh(%sender){ return ccvehLock(%sender); }
function cclock(%sender){ return ccvehLock(%sender); }
function ccvehLock(%sender)
{
%pos = %sender.player.getMuzzlePoint($WeaponSlot);
%vec = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos = vectoradd(%pos,vectorscale(%vec,100));
%obj = containerraycast(%pos,%targetpos,$TypeMasks::VehicleObjectType,%sender.player);
%obj = getword(%obj,0);

if (%sender.player.isMounted())
{
 if (%sender.player.mountObj.owner != %sender)
 {
  messageclient(%sender,'MsgClient',"\c3Not your vehicle.");
  return 1;
 }
 else
 {
  %obj = %sender.player.mountobj;
  if (%obj.isLocked)
  {
   %obj.isLocked = false;
   %obj.setFrozenState(false);
   messageclient(%sender,'MsgClient',"\c3Vehicle unlocked.");
   return 1;
  }
  else
  {
   %obj.isLocked = true;
   %obj.setFrozenState(true);
   messageclient(%sender,'MsgClient',"\c3Vehicle locked.");
   return 1;
  }
 }
}
else
{
if (!%obj)
{
messageclient(%sender,'MsgClient',"\c3No vehicle in range.");
return 1;
}
if (%obj.owner != %sender && !%sender.isAdmin)
{
messageclient(%sender,'MsgClient',"\c3Not your vehicle.");
return 1;
}

if (%obj.isLocked)
{
 %obj.isLocked = false;
 %obj.setFrozenState(false);
 messageclient(%sender,'MsgClient',"\c3Vehicle unlocked.");
 return 1;
}
else
{
 %obj.isLocked = true;
 %obj.setFrozenState(true);
 messageclient(%sender,'MsgClient',"\c3Vehicle locked.");
 return 1;
}
}
}

// Chat Commands: Sizing & Scaling
scoreMenuAddHelpEntry(0,"objectScale","Scales the piece you are looking at by X Y Z.",false);
function ccscale(%sender, %args){ return ccobjectScale(%sender, %args); }
function ccsetScale(%sender, %args){ return ccobjectScale(%sender, %args); }
function ccscaleObject(%sender, %args){ return ccobjectScale(%sender, %args); }
function ccscaleObj(%sender, %args){ return ccobjectScale(%sender, %args); }
function ccobjectscale(%sender,%args) //By Naosyth, originally for DDDXMod
{
  %size = getwords(%args,0);
  %pos=%sender.player.getMuzzlePoint($WeaponSlot);
  %vec = %sender.player.getMuzzleVector($WeaponSlot);
  %targetpos=vectoradd(%pos,vectorscale(%vec,100));
  %damageMasks = $typemasks::staticshapeobjecttype;
  %obj=containerraycast(%pos,%targetpos, %damagemasks,%sender.player);

   if(!%obj)
  {
    messageClient(%sender, "MsgClient", "\c3No item in range (" @ $RPCM::Command::Range SPC "meters) to resize.");
    return 1;
  }

  if(!deployables.ismember(%obj))
  {
    messageClient(%sender, "MsgClient", "\c3You can\'t resize map objects.");
    return 1;
  }

  if(%obj.owner != %sender && !%sender.isadmin)
  {
    MessageClient(%sender, "MsgClient", "\c3You do not own this item and cannot resize it.");
    return 1;
  }
  if(getword(%size, 0)=="0" || getword(%size, 1)=="0" || getword(%size, 2)=="0")
{
  messageClient(%sender, "MsgClient", "\c3Too small.");
  return 1;
}

if(getword(%size, 0)>="1000" || getword(%size, 1)>="1000" || getword(%size, 2)>="1000")
{
  messageClient(%sender, "MsgClient", "\c3Too big.");
  return 1;
}

MessageClient(%sender, "MsgClient", "\c3Rescaling object from" SPC %obj.getScale() SPC "to" SPC %size  @ ".");

if (%obj.isDecoration)
%obj.parent.setscale(%size);
else
%obj.setScale(%size);
return 1;
}

scoreMenuAddHelpEntry(0,"spine","Scale a piece along the Z axis.");
function ccsetLength(%sender, %args){ return ccspine(%sender, %args); }
function ccspine(%sender,%args) //By Naosyth, originally for DDDXMod
{
  %size = getword(%args,0);
  %pos=%sender.player.getMuzzlePoint($WeaponSlot);
  %vec = %sender.player.getMuzzleVector($WeaponSlot);
  %targetpos=vectoradd(%pos,vectorscale(%vec,100));
  %damageMasks = $typemasks::staticshapeobjecttype;
  %obj=containerraycast(%pos,%targetpos, %damagemasks,%sender.player);


   if(!%obj)
  {
    messageClient(%sender, "MsgClient", "\c3No item in range (" @ $RPCM::Command::Range SPC "meters) to resize.");
    return 1;
  }

  if(!deployables.ismember(%obj))
  {
    messageClient(%sender, "MsgClient", "\c3You can\'t resize map objects.");
    return 1;
  }

  if(%obj.owner != %sender && !%sender.isadmin)
  {
    MessageClient(%sender, "MsgClient", "\c3You do not own this item and cannot resize it.");
    return 1;
  }
  if(getword(%size, 0)=="0")
{
  messageClient(%sender, "MsgClient", "\c3Too small.");
  return 1;
}

if(getword(%size, 0)>="1000")
{
  messageClient(%sender, "MsgClient", "\c3Too big.");
  return 1;
}

MessageClient(%sender, "MsgClient", "\c3Spine length set to "@%size@".");
%scale = %obj.getScale();
%x = GetWord(%scale, 0);
%y = GetWord(%scale, 1);

if (%obj.isDecoration)
%obj.parent.setscale(%x SPC %y SPC %size);
else
%obj.setscale(%x SPC %y SPC %size);
return 1;
}

scoreMenuAddHelpEntry(0,"getScale","Get an object\'s scale.",true);
function ccgetScale(%sender, %args)
{
  %pos=%sender.player.getMuzzlePoint($WeaponSlot);
  %vec = %sender.player.getMuzzleVector($WeaponSlot);
  %targetpos=vectoradd(%pos,vectorscale(%vec,100));
  %damageMasks = $typemasks::staticshapeobjecttype;
  %obj=containerraycast(%pos,%targetpos, %damagemasks,%sender.player);

  if (!%obj) //Do nothing.
  return 1;

  messageClient(%sender,'MsgClient','\c3Scale: %1.',%obj.getScale());
  return 1;
}

scoreMenuAddHelpEntry(0,"objectSize","Sets an object\'s size.");
function ccsize(%sender, %args){ return ccobjectSize(%sender, %args); }
function ccsetSize(%sender, %args){ return ccobjectSize(%sender, %args); }
function ccsizeObject(%sender, %args){ return ccobjectSize(%sender, %args); }
function ccsizeObj(%sender, %args){ return ccobjectSize(%sender, %args); }
function ccobjectSize(%sender,%args)
{
  %size = getwords(%args,0);
  %pos=%sender.player.getMuzzlePoint($WeaponSlot);
  %vec = %sender.player.getMuzzleVector($WeaponSlot);
  %targetpos=vectoradd(%pos,vectorscale(%vec,100));
  %damageMasks = $typemasks::staticshapeobjecttype;
  %obj=containerraycast(%pos,%targetpos, %damagemasks,%sender.player);


   if(!%obj)
  {
    messageClient(%sender, "MsgClient", "\c3No item in range (" @ $RPCM::Command::Range SPC "meters) to resize.");
    return 1;
  }

  if(!deployables.ismember(%obj))
  {
    messageClient(%sender, "MsgClient", "\c3You can\'t resize map objects.");
    return 1;
  }

  if(%obj.owner != %sender && !%sender.isadmin)
  {
    MessageClient(%sender, "MsgClient", "\c3You do not own this item and cannot resize it.");
    return 1;
  }
  if(getword(%size, 0)=="0" || getword(%size, 1)=="0" || getword(%size, 2)=="0")
{
  messageClient(%sender, "MsgClient", "\c3Too small.");
  return 1;
}

if(getword(%size, 0)>="1000" || getword(%size, 1)>="1000" || getword(%size, 2)>="1000")
{
  messageClient(%sender, "MsgClient", "\c3Too big.");
  return 1;
}

MessageClient(%sender, "MsgClient", "\c3Resizing object from" SPC %obj.getRealSize() SPC "to" SPC %size  @ ".");
if (%obj.isDecoration)
%obj.parent.setRealSize(%size);
else
%obj.setRealSize(%size);
return 1;
}
function ccSetSize(%sender, %args){ return ccObjectSize(%sender, %args); }
function ccObjSize(%sender, %args){ return ccObjectSize(%sender, %args); }

scoreMenuAddHelpEntry(0,"getSize","Get an object\'s size.",true);
function ccgetSize(%sender, %args)
{
  %pos=%sender.player.getMuzzlePoint($WeaponSlot);
  %vec = %sender.player.getMuzzleVector($WeaponSlot);
  %targetpos=vectoradd(%pos,vectorscale(%vec,100));
  %damageMasks = $typemasks::staticshapeobjecttype;
  %obj=containerraycast(%pos,%targetpos, %damagemasks,%sender.player);

  if (!%obj) //Do nothing.
  return 1;

  messageClient(%sender,'MsgClient','\c3Size: %1.',%obj.getRealSize());
  return 1;
}

// Power Frequency Commands
scoreMenuAddHelpEntry(0,"getFreq","Gets the power frequency of the piece you are looking at.",true);
function ccgetFreq(%sender)
{
  %size = getwords(%args,0);
  %pos=%sender.player.getMuzzlePoint($WeaponSlot);
  %vec = %sender.player.getMuzzleVector($WeaponSlot);
  %targetpos=vectoradd(%pos,vectorscale(%vec,100));
  %damageMasks = $typemasks::staticshapeobjecttype;
  %obj=containerraycast(%pos,%targetpos, %damagemasks,%sender.player);

   if(!%obj)
  {
    messageClient(%sender, "MsgClient", "\c3No object in range.");
    return 1;
  }

  if(!deployables.ismember(%obj))
  {
    messageClient(%sender, "MsgClient", "\c3Map objects do not obey the Power Frequency system.");
    return 1;
  }

MessageClient(%sender, "MsgClient", "\c3Power frequency: "@%obj.powerFreq@"");
return 1;
}

scoreMenuAddHelpEntry(0,"emote","Allows you to animate your character.",false);
function ccEmote(%sender,%args)
{
 if (!isObject(%sender.player))
 {
  messageClient(%sender, "MsgClient", "\c3No active player object.");
  return 1;
 }

 %conversion["laydown"] = "death1";
 %conversion[""] = "death2";
 %conversion["heartattack"] = "death3";
 %conversion["sit"] = "sitting";
 %conversion["squat"] = "scoutroot";
 
 if (%conversion[strLwr(%args)] !$="")
 %args = %conversion[strLwr(%args)];

 %sender.player.setActionThread(%args,true);

 return 1;
}




