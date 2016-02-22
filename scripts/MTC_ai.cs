$MtcSuperRange = 0;

$MtcMaxAmmo["flare","light"] = 8;
$MtcMaxAmmo["flare","medium"] = 10;
$MtcMaxAmmo["flare","heavy"] = 0;
$MtcMaxAmmo["flash","light"] = 5;
$MtcMaxAmmo["flash","medium"] = 8;
$MtcMaxAmmo["flash","heavy"] = 0;
$MtcMaxAmmo["cloak","light"] = 160;
$MtcMaxAmmo["cloak","medium"] = 120;
$MtcMaxAmmo["cloak","heavy"] = 0;
$MtcMaxAmmo["mine","light"] = 4;
$MtcMaxAmmo["mine","medium"] = 6;
$MtcMaxAmmo["mine","heavy"] = 20;

$MtcDamageRun["light"] = 0.5;
$MtcDamageRun["medium"] = 0.55;
$MtcDamageRun["heavy"] = 0.75;

/////////////////////////////////////////////
///////////////[[Part1]]/////////////////////
/////////////////////////////////////////////

//Initialisation runs

function Item::Initialize(%obj)
{
//echo("Initialize");
///Arm ourselfes if we need to.
if (!IsObject(%obj.turret) || %obj.weapon $= "")
    %obj.Arm();

//Starting supplies
%size = $mtcWeaponSize[%obj.weapon];

%obj.ammo["mine"] = $MtcMaxAmmo["mine",%size] * getRandom();
%obj.ammo["Cloak"] =$MtcMaxAmmo["cloak",%size] * getRandom();
%obj.ammo["flare"] =$MtcMaxAmmo["flare",%size] * getRandom();
%obj.ammo["flash"] =$MtcMaxAmmo["flash",%size] * getRandom();

//Set best attack patern
    %obj.SetPatern();

//Set inventory loop
if (!%obj.invloop)
   %obj.invloop();

//Set orders and run them
   %obj.evaluate();
}


function Item::SetPatern(%obj)
{
//echo("setPatern");
///
///  sensors //// static /// turret /// gen
///
%obj.maxdist = 500; //Standart set for now
%obj.noacloak = 1; //Standart off
%obj.nocloak = 0;

if ($mtcWeaponSize[%obj.weapon] $= "light")
   {

   }
else if ($mtcWeaponSize[%obj.weapon] $= "medium")
   {
      %obj.movemod = -0.25;
   }
else if ($mtcWeaponSize[%obj.weapon] $= "heavy")
   {
      %obj.nofly = 1;
      %obj.movemod = 0.5;
      %obj.nocloak = 1;
   }


if (%obj.weapon $= "chain")
      {
      %obj.patern = "attack";
      %obj.mindist = 20;
      %obj.meddist = 50;
      %obj.preftargets = "nturret lsensor ustatic astatic";
      %obj.preftargetslack = 100;
      }
else if (%obj.weapon $= "plasma")
      {
      %obj.patern = "attack";
      %obj.mindist = 5;
      %obj.meddist = 50;
      %obj.preftargets = "lsensor lturret ustatic";
      %obj.preftargetslack = 50;
      %obj.movemod = -0.25;
      %obj.noacloak = 0;
      }
else if (%obj.weapon $= "Fusion")
      {
      %obj.patern = "attack";
      %obj.mindist = 20;
      %obj.meddist = 80;
      %obj.preftargets = "agen astatic aturret";
      %obj.preftargetslack = 150;
      }
else if  (%obj.weapon $= "nerf")
      {
      %obj.patern = "attack";
      %obj.mindist = 5;
      %obj.meddist = 20;
      %obj.preftargets = "aplayer";
      %obj.preftargetslack = 200;
      }
else if  (%obj.weapon $= "disc")
      {
      %obj.patern = "attack";
      %obj.mindist = 15;
      %obj.meddist = 200;
      %obj.preftargets = "nturret lsensor aplayer";
      %obj.preftargetslack = 50;
      }
else if  (%obj.weapon $= "blaster")
      {
      %obj.patern = "attack";
      %obj.mindist = 5;
      %obj.meddist = 50;
      %obj.preftargets = "lsensor lturret nturret agen";
      %obj.preftargetslack = 50;
      %obj.noacloak = 0;
      }
else if  (%obj.weapon $= "target")
      {
      %obj.patern = "range";
      %obj.mindist = 25;
      %obj.meddist = 200;
      %obj.preftargets = "aturret agen";
      %obj.preftargetslack = 50;

      }
else if  (%obj.weapon $= "grenade")
      {
      %obj.patern = "range";
      %obj.mindist = 30;
      %obj.meddist = 200;
      %obj.preftargets = "ustatic lsensor agen";
      %obj.preftargetslack = 50;
      }
else if  (%obj.weapon $= "laser")
      {
      %obj.patern = "range";
      %obj.mindist = 20;
      %obj.meddist = 200;
      %obj.preftargets = "aplayer lsensor";
      %obj.preftargetslack = 50;
      }
else if  (%obj.weapon $= "missile")
      {
      %obj.patern = "range";
      %obj.mindist = 50;
      %obj.meddist = 200;
      %obj.preftargets = "aturret aplayer avehicle";
      %obj.preftargetslack = 75;
      }
else if  (%obj.weapon $= "swarmdisc")
      {
      %obj.patern = "range";
      %obj.mindist = 50;
      %obj.meddist = 200;
      %obj.preftargets = "aturret aplayer avehicle";
      %obj.preftargetslack = 75;
      }
else if  (%obj.weapon $= "ionmissile")
      {
      %obj.patern = "range";
      %obj.mindist = 100;
      %obj.meddist = 200;
      %obj.preftargets = "pstatic pturret pgen";
      %obj.preftargetslack = 1000;
      }
else if  (%obj.weapon $= "ionbeam")
      {
      %obj.patern = "range";
      %obj.mindist = 50;
      %obj.meddist = 200;
      %obj.preftargets = "pstatic pturret pgen";
      %obj.preftargetslack = 1000;
      }
else if  (%obj.weapon $= "mortar")
      {
      %obj.patern = "range";
      %obj.mindist = 50;
      %obj.meddist = 300;
      %obj.preftargets = "agen astatic aturret";
      %obj.preftargetslack = 50;
      }
else if  (%obj.weapon $= "tractor")
      {
      %obj.patern = "range";
      %obj.mindist = 50;
      %obj.meddist = 100;
      %obj.preftargets = "aplayer avehicle";
      %obj.preftargetslack = 500;
      }
else
      {
      %obj.patern = "attack";
      %obj.mindist = 5;
      %obj.meddist = 20;
      %obj.preftargets = "aplayer";
      %obj.preftargetslack = 50;
    }
}

/////////////////////////////////////////////
///////////////[[Part2]]/////////////////////
/////////////////////////////////////////////

//Loops

function Item::invloop(%obj)
{
//echo("invloop");
Cancel(%obj.invloop);

if (!%obj.nosuply)
   {
   %size = $mtcWeaponSize[%obj.weapon];
   %obj.ammo["mine"] = limit(%obj.ammo["mine"] + 1,0, $MtcMaxAmmo["mine",%size]);
   %obj.ammo["cloak"] = limit(%obj.ammo["cloak"] + 30,0, $MtcMaxAmmo["cloak",%size]);
   %obj.ammo["flare"] = limit(%obj.ammo["flare"] + 1,0, $MtcMaxAmmo["flare",%size]);
   %obj.ammo["flash"] = limit(%obj.ammo["flash"] + 1,0, $MtcMaxAmmo["flash",%size]);
   }

if (!%obj.noaggression)
    %obj.aggression = limit(%obj.aggression + getRandom()-0.5,0,10);

%obj.notify("ammo");

//Gets his stuff every minute.
%obj.invloop = %obj.schedule(60000,"invloop");
}

function Item::evaluate(%obj)
{
%obj.lastevaltime = getSimTime();
Cancel(%obj.evalloop);

%obj.sound("think");

if (%obj.order !$= "")
   {
   //Attack
   if (%obj.order $= "attack")
      {
      if (%obj.ValidTarget() || %obj.targetrun < 6)
         {
         %obj.targetrun++;
         ///LETS GO ATTACK
         %obj.attackrun(); //Only attack the same target for 1 minute
         %obj.notify("attack");
         }
      else
         {
         %obj.target = %obj.NewTarget();
         if (%obj.ValidTarget())
            {
            ///STOP ATTACKING
            %obj.order = "";
            }
         else
            {
            //Go attack the next run
            %obj.targetrun = "";
            }
         }
      }

   //Flee
   else if (%obj.order $= "flee")
      {
      if (%target = %obj.gotThreat() !$= "")
         {
         ///LETS GO FLEE
         %obj.target = %target;
         %obj.fleerun();
         %obj.notify("flee");
         }
      else
         {
         //STOP FLEEING
         %obj.order = "";
         }
      }

   //Roam
   else if (%obj.order $= "roam")
      {
      //Still no valid target?
      %target = %obj.ClosestTarget();
      if (!%obj.ValidTarget(%obj.NewTarget()))
         {
         ///LETS GO ROAM
         //%obj.target = %target;
         %obj.roamrun();
         %obj.notify("roam");
         }
      else
         {
         ///STOP ROAMING
         %obj.order = "";
         }
      }
   //Repair
   else if (%obj.order $= "repair")
      {
      //Are we still damaged?
      if (%obj.turret.getDamagePct() > 0.1)
        {
        //Are there enemies nearby?
        if (%obj.gotThreat())
           {
           if (%obj.needflee())
              {
              //STOP REPAIRING
              %obj.order = "";
              }
           else
              {
              //STOP REPAIRING
              %obj.order = "";
              }
           }
        else
           {
           ///LETS GO REPAIR
           %obj.repairrun();
           %obj.notify("repair");
           }
        }
      else
        {
        ///STOP REPAIRING
        %obj.order = "";
        }
      }
   }

///So we didn't have any orders?

if (%obj.order $= "")
   {
   ///Are we hurt??
   if (%obj.needflee())
      {
      ///Do we need to run?
      if (%obj.gotThreat())
         {
         %obj.order = "flee";
         %rechecktime = 450;
         Cancel(%obj.mainrun);
         }
      else
         {
         %obj.order = "repair";
         %rechecktime = 450;
         Cancel(%obj.mainrun);
         }
      }
   else
      {
      ///Anyone to attack?
      %obj.target = %obj.NewTarget();
      if (!%obj.ValidTarget())
         {
         %obj.target= %obj.closestTarget();
         %obj.order = "roam";
         %rechecktime = 450;
         Cancel(%obj.mainrun);
         }
      else
         {
         %obj.order = "attack";
         %rechecktime = 450;
         Cancel(%obj.mainrun);
         }
      }
   }

if (!%rechecktime)
    %rechecktime = 10000;

%obj.evalloop = %obj.schedule(%rechecktime,"evaluate");
}

function Item::Needflee(%obj)
{
%size = $mtcWeaponSize[%obj.weapon];
if (%obj.turret.getDamagePct() > $MtcDamageRun[%size])
   return 1;
else if (%obj.ammo["flare"] <  $MtcDamageRun[%size]*$MtcMaxAmmo["flare",%size] && $MtcMaxAmmo["flare",%size])
   return 1;
return 0;
}

/////////////////////////////////////////////
///////////////[[Part3]]/////////////////////
/////////////////////////////////////////////

//Targeting

// Defined in scripts/ion.cs
//function GameBase::isEnemy(%obj,%target) {
//	//echo("isenemy");
//	if (!isObject(%obj) || !isObject(%target))
//		return "";
//	if (%obj.team != %target.team) ///Other teams default to enemy.
//		return 1;
//	if (%obj.getOwner().evil) {
//		if (%obj.getOwner() != %target.client && %obj.getOwner() != %target.getOwner())
//			return 1;
//	}
//	return "";
//}

function Item::gotThreat(%obj) //////Needs update.
{
//echo("gotThreat");
%target = closestTarget(%obj.getWorldboxCenter(),"aplayer pturret cvehile",%obj.maxdist,%obj);
if (GetWord(%target,1) < %obj.maxdist)
   return GetWord(%target,0);
return "";
}

function Item::ValidTarget(%obj,%target) ///Needs update
{
if (!%target)
   %target = %obj.target;

if (mtcCleantarget(%target))
   {
   if (%target == %obj.forcetarget())
       return 1;

   if (%obj.terdist(%target) < %obj.maxdist)
      {
      if (!(%target.getDataBlock().className $= "Armor" && %target.getState() $= "Dead"))
         {
            return 1;
         }
      }
   }
return "";
}

//Can we as mtc get to this target?
function mtcCleantarget(%target)
{
//echo("mtcCleantarget");
    //Only exsistant objects, no clients.
    //No nonpowered forcefields.
    //Only players,vehicles or stuff deployed ingame.
    //Only stuff outside buildings or that sticks out.

%group = nameToID("MissionCleanup/Deployables");
if (%target && IsObject(%target) && !%target.player)
   {
   if (!(!%target.isPowered() && %target.isforcefield()))
      {
      if (%target.getType() & typelist("aplayer avehicle") || %group.isMember(%target))
         {
         %fstat = aboveground(%target.getworldboxcenter(),1,%obj);
         %stat = GetWord(%fstat,0);
         if(%stat $= "open" || %stat $= "roof" || %stat $= "shadow")
            return 1;
         else if (%target.seetop())
            return 1;
         }
      }
   }
return "";
}

function Item::NewTarget(%obj,%target)
{
//echo("NewTarget");
if (%obj.forcetarget())
   return %obj.forcetarget();

%sugtarget = %target;

if (Change(%obj.aggression,5,10) && IsObject(%plyrtarget))
      {
      %target = closestTarget(%obj.getWorldboxCenter(),"aplayer cvehile","",%obj);
      }
   else
      {
      %preftarget = closestTarget(%obj.getWorldboxCenter(),%obj.preftargets,"",%obj);
      %alttarget = closestTarget(%obj.getWorldboxCenter(),"aall","",%obj);
      if (GetWord(%alttarget,1) > GetWord(%preftarget,1)- %obj.preftargetslack && IsObject(%preftarget))
          {
          %target = %preftarget;
          }
      else
          {
          %target = %alttarget;
          }
      }
if (!IsObject(%target))
    %target = ClosestPlayer(%obj.getWorldBoxCenter(),"","",%obj);

return getWord(%target,0);
}

function Item::ClosestTarget(%obj)
{
//echo("closesttarget");
return closestTarget(%obj.getWorldBoxCenter(),"aall","",%obj);
}

function closestTarget(%location,%ftype,%range,%obj)
{
//echo("closesttarget");
%group = nameToID("MissionCleanup/Deployables");

if (%ftype $= "")
   %ftype = aall;
if (!%range)
    %range = 1000 + (100000* $MtcSuperRange);

%mask = typelist(%ftype);


InitContainerRadiusSearch(%location,%range,%mask);

 while ((%target = ContainerSearchNext()) != 0)
   {
   if (mtcCleantarget(%target))
      {
      if (%target.onpreflist(%ftype) && (IsObject(%obj) && %obj.isEnemy(%target)))
          return %target SPC containerSearchCurrRadDamageDist();
      }

   }

return "";
}


function Item::forcetarget(%obj)
{
//echo("forcetarget");
%target = %obj.forcetarget;
if (%target !$= "")
   {
   if (IsObject(%target.player))
       return %target.player;
   else if (%target.getClassname() !$= "GameConnection")
       return %target;
   }
%target = mtcgroup.forcetarget;
if (%target !$= "")
   {
   if (IsObject(%target.player))
       return %target.player;
   else if (%target.getClassname() !$= "GameConnection")
       return %target;
   }
return "";
}

/////////////////////////////////////////////
///////////////[[Part4]]/////////////////////
/////////////////////////////////////////////

//Actions

function Item::resetorders(%obj)
{
//echo("resetorders");
   %obj.order = "";
   Cancel(%obj.evalloop);
   %obj.evalloop = %obj.schedule(450,"evaluate");
   return"";
}

//GO ATTACK
function Item::attackrun(%obj)
{
//echo("attackrun");
Cancel(%obj.mainrun);

%obj.checkflare();
%obj.flaredelay(1);

if (!%obj.ValidTarget() || %obj.needflee())
   {
   return %obj.resetorders();
   }

///Basic Information
%loc = %obj.getWorldboxCenter();
%target = %obj.target;
%tloc = %obj.target.getWorldboxCenter();
%line = VectorSub(%tloc,%loc);
%dir = VectorNormalize(%line);
%dist = VectorLen(%line);

///Movement patern
%obj.attackcloak = 0;
%obj.canfire("target");

if (%dist < %obj.mindist && !(%obj.forcetarget() && !%obj.nofly))
   {
   ///Whoa we're too close
   %obj.move(%tloc,"to",-0.6);
   %obj.dodge = randomlev();
   }
else if (%dist  > %obj.mindist && %dist < %obj.meddist && !(%obj.forcetarget() && !%obj.nofly))
   {
   //perfect let's Guooo.
   if (%obj.patern $= "attack")
      {
      ///Lets make him dizzy
      %obj.move(%tloc,"side",%obj.dodge*0.5);

      }
   else if (%obj.patern $= "range")
      {
      ///Lets get a nice high spot
      %obj.move(%tloc,"up",1);
      }
   else
      {
      ///Oh sure.. I'l be dead in the water.
      }
   }
else if ((%dist > %objmeddist && %dist < %obj.maxdist) || (%obj.forcetarget() && !%obj.nofly))
   {
   //Damn he's too far
   %obj.move(%tloc,"to",0.9);
   %obj.dodge = randomlev();
   if ((!%obj.noacloak) &&  !%obj.forcetarget() && %dist > %obj.meddist*1.2)
      {
      %obj.attackcloak = 1;
      %obj.cloak(1);
      }
   }
else
   {
   //We're outa range.
   %obj.move(%tloc,"to",0.5);
   %obj.canfire();
   if (!%obj.noacloak)
       {
       %obj.attackcloak = 1;
       %obj.cloak(1);
       }
   }

if (!%obj.attackcloak)
    %obj.cloak();

%obj.mainrun = %obj.schedule(500, "attackrun");
}

//GO FLEE

function Item::fleerun(%obj)
{
//echo("fleerun");
Cancel(%obj.mainrun);

%obj.checkflare();
%obj.flaredelay();

if (%target = %obj.gotThreat())
    %obj.target = %target;
else if (%target = %obj.closestTarget())
    %obj.target = %target;
else
   {
   //Omg there's noone near for miles
   return %obj.resetorders();
   }

///Basic Information
%loc = %obj.getWorldboxCenter();
%target = %obj.target;
%tloc = %obj.target.getWorldboxCenter();
%line = VectorSub(%tloc,%loc);
%dir = VectorNormalize(%line);
%dist = VectorLen(%line);

///Movement patern
    %obj.canfire();

if (%dist < %obj.mindist)
   {
   //Whoa double time
   %obj.move(%tloc,"to",-1.2);
   %obj.cloak(1);
   if (!%obj.cloaked)
       %obj.canfire("target");
   }
else if (%dist  > %obj.mindst && %dist < %obj.meddist )
   {
   //Move move move
   %obj.move(%tloc,"to",-1);
   %obj.cloak(1);
   %obj.Mine();
   if (!%obj.cloaked)
       %obj.canfire("target");
   }
else if (%dist > %objmeddist && %dist < %obj.maxdist )
   {
   //Just a litle futher
   %obj.target = %obj.closesttarget();
   %obj.move(%tloc,"to",-0.8);
   %obj.cloak(1);
   }
else
   {
   %obj.target = %obj.closesttarget();
   //Finally he's gone.
   %obj.cloak(0);
   }
%obj.mainrun = %obj.schedule(500, "fleerun");
}

//GO REPAIR

function Item::repairrun(%obj)
{
//echo("repairrun");
Cancel(%obj.mainrun);
Cancel(%obj.repairsch);

%obj.checkflare();
%obj.flaredelay();

if (%obj.gotThreat())
           {
           return %obj.resetorders();
           }


%obj.cloak(1);
%obj.canfire();

createLifeEmitter(%obj.getWorldboxCenter(), ELFSparksEmitter, 450);

%size = $mtcWeaponSize[%obj.weapon];
%obj.ammo["flare"] = limit(%obj.ammo["flare"] + 1,0,$MtcMaxAmmo["flare",%size]);

%obj.turret.setRepairRate(0.004);
%obj.repairsch = %obj.turret.schedule(450,"setRepairRate",0);

%obj.mainrun = %obj.schedule(500,"repairrun");
}

//GO ROAM

function Item::roamrun(%obj)
{
//echo("roamrun");
Cancel(%obj.mainrun);

%obj.roamtime = %obj.roamtime - 1;

//%obj.target = %obj.NewTarget();
//%obj.ValidTarget()
if ( %obj.needflee())
   {
   return %obj.resetorders();
   }


%obj.checkflare();
%obj.flaredelay();

if (%obj.roamtime < 0)
   {
   %obj.totalroam++;
   %obj.roamdir = getRandom();
   %obj.roamtime = getRandom() * 200+50;
   if (%obj.totalroam > 20)
      {
      %obj.totalroam = 0;
      %obj.telleport(getTerrainHeight2("0 0 0","0 0 -1"));
      }
   }

%obj.cloak();
%obj.canfire();

///This will make the mtc move directly towards the closest target when he's angry
///And all directions when he's not.
if (IsObject(%obj.target) && %obj.dist(%obj.target) < %obj.maxdist)
    %obj.moveangle = RandomLev()*($Pi + $Pi*getRandom());
else
    %obj.moveangle = %obj.roamdir*(((1-(%obj.aggression /10))* 2* $Pi) - $Pi);

if (MtcCleanTarget(%obj.target))
   {
   %location = %obj.target.getWorldBoxCenter();
   if ((%obj.dist(%obj.target)/%obj.altdist(%obj.target))<1.5)
       %obj.moveangle = 0;
   }
else
   {
   %location = VectorAdd(mCos(%obj.Moveangle)*100 SPC mSin(%obj.moveangle)*100 SPC 0,%obj.getWorldBoxCenter());
   }

%obj.move(%location,"angle",0.8);

%obj.mainrun = %obj.schedule(500, "roamrun");
}


//Go Move Target (avioding threats)

function Item::moveTrun(%obj)
{
//echo("moverun");
Cancel(%obj.mainrun);

%obj.checkflare();

if (!mtcCleanTarget(%obj.target))
   {
   %obj.order = "";
   Cancel(%obj.evalloop);
   %obj.evalloop = %obj.schedule(450,"evaluate");
   }

///Basic Information
%loc = %obj.getWorldboxCenter();
%target = %obj.target;
%tloc = %obj.target.getWorldboxCenter();
%line = VectorSub(%tloc,%loc);
%dir = VectorNormalize(%line);
%dist = VectorLen(%line);

if (%threat = %obj.gotThreat())
   {
   %trloc = %Threat.getWorldBoxCenter();
   %trdir = %obj.dir(%threat);
   %angle = VectorAngle(%trdir,%dir);
   %avoiddir = VectorCross(VectorCross(%trdir,%dir),%trdir);
   }

%obj.canfire("");

if (%dist < %obj.mindist)
   {
   %obj.order = "";
   Cancel(%obj.evalloop);
   %obj.evalloop = %obj.schedule(450,"evaluate");
   }
else
   {
   if (%angle > $Pi)
      %obj.move(%tloc,"to",1);
   else
      {
      %obj.movedir = %avoiddir;
      %obj.move(%trloc,"dir",1);
      }
   }


%obj.mainrun = %obj.schedule(500, "moveTrun");
}

//Basic Moving
function Item::move(%obj,%location,%mdir,%speed)
{
//echo("move");
if (%obj.nomove)
return "";

%obj.evalpos++;

//Information

%selfloc = %obj.getWorldBoxCenter();
%targetloc = %location;
%targetline = Vectorsub(%targetloc,%selfloc);
%targetdir = VectorNormalize(%targetline);
%targetdist = VectorLen(%targetline);
%altdist = GetWord(%targetline,2);


if (%obj.evalpos > 10)
   {
   %obj.FixHeight();

   %movedist = VectorDist(%obj.getWorldboxCenter(),%obj.lastlocation);

   if ((%movedist < 5 && %mdir !$= "up" && %targetdist > %obj.meddist) || (%movedist < 1  && %targetdist > %obj.mindist) || %movedist < 0.1)
      {
      %obj.freeforce = %obj.freeforce + 10;
         if (%obj.freeforce * %speed < %targetdist)
             %dir = VectorScale(%targetdir,%obj.freeforce*%speed);
         else
             %dir =  VectorScale(%targetdir,%targetdist - 2);

         %newpos = getTerrainHeight2(VectorAdd(%obj.getWorldboxCenter(),%dir),"0 0 -1");
         %obj.telleport(%newpos); //Break free.
      %obj.movepower = 60;
      }
   else if (%movedist < 20)
      {
      %obj.movepower = Limit(%obj.movepower+20,0,1000);
      %obj.freeforce = 10;
      }
   else
      {
      %obj.movepower = 60;
      %obj.freeforce = 10;
      }

   %obj.lastlocation = %obj.getWorldboxCenter();
   %obj.evalpos = 0;
   }



if (%targetdist > 2000 && %obj.order !$= "roam")
   {
   %dir = VectorScale(%targetdir,%targetdist-500);
   %newpos = VectorAdd(%obj.getWorldboxCenter(),%dir);
   if ((%targetdist/%altdist)>1.5)
        %newpos = getTerrainHeight2(%newpos,"0 0 -1");
   %obj.telleport(%newpos); //Get way closer.
   }
else if (%targetdist > 4000 && %obj.order $= "roam" && %disabled)
   {
   %dir = VectorScale(%targetdir,%targetdist-3000);
   %newpos = VectorAdd(%obj.getWorldboxCenter(),%dir);
   if ((%targetdist/%altdist)>1.5)
        %newpos = getTerrainHeight2(%newpos,"0 0 -1");
   %obj.telleport(%newpos); //Get some closer.
   }


%offset = VectorScale(VectorCross(VectorCross("0 0 1",%targetdir),%targetdir),20);

%res = containerRayCast(%obj.getWorldboxCenter(), VectorAdd(%obj.getWorldboxCenter(),%offset), $TypeMasks::TerrainObjectType, %obj.turret);


if (%res)
   {
   %nrm = normalFromRaycast(%res);
   }
else
   {
   %nrm = "0 0 1";
   }

%dir = %targetdir;
if (%mdir $= "to") // Towards/from
   %movedir = VectorCross(VectorCross(%nrm,%dir),%nrm);

else if (%mdir $= "side") //Sideways
   %movedir = VectorCross(%nrm,%dir);

else if (%mdir $= "up") //Up the mountain
     {
     if (vAbs(floorvec(%nrm,100)) $= "0 0 1")
          %movedir = VectorCross(%nrm,%dir);
     else
          %movedir = VectorCross(VectorCross(%nrm,"0 0 1"),%nrm);
     }

else if (%mdir $= "angle") //Certain angle away from target.
     {
     %movedir1 = VectorScale(VectorCross(VectorCross(%nrm,%dir),%nrm),MCos(%obj.moveangle));
     %movedir2 = VectorScale(VectorCross(%nrm,%dir),MSin(%obj.moveangle));
     %movedir = VectorAdd(%movedir1,%movedir2);
     }
else if (%mdir $= "dir")
     {
     %movedir = VectorCross(VectorCross(%nrm,%obj.movedir),%nrm);
     }

//Flying

%targetplace = GetWord(aboveground(%targetloc,1,0),0);
if (((%obj.wasflying && (%targetdist/%altdist)<2)|| %obj.forcetarget() ||  ((%targetdist/%altdist)<1.5 && %targetdist > 200)) && %obj.order $= "attack" && !%obj.nofly && (%targetplace $= "open"|| %targetplace $="roof"))
   {
   %speed = mCeil(%targetdist/100)+0.5;
   %nrm = "0 0 1";
   %movedir = VectorNormalize(VectorAdd(VectorAdd(VectorCross(VectorCross(%nrm,%dir),%nrm),VectorCross(%nrm,%dir)),"0 0" SPC Lev(GetWord(%targetdir,2))/2));
   %obj.wasflying = 1;
   %obj.flaredelay(1);
   }
else if (VectorDist(getTerrainHeight2(pos(%obj),"0 0 -1"),pos(%obj))>50)
       %movedir = VectorAdd(%movedir,"0 0" SPC GetWord(%targetdir,2));
else
   %obj.wasflying = 0;

%pulse = Limit(1 * %obj.movepower * %speed*(1+%obj.movemod),-1000,1000);

//Floating
if (%disabled && Isobject(Water) )
   {
   %surface = GetWords(pos(%obj),0,1) SPC (GetWord(Water.getTransform(),2)+GetWord(Water.getScale(),2))+2;
   if ((GetWord(pos(%obj),2)-GetWord(%surface,2))<2)
      {
      %res2 = containerRayCast(%surface,pos(%obj),-1, %obj.turret);
      ////echo(%res2.getClassname());
      if (%res2 && %res2.getClassname() $= "Waterblock")
         {
         //%movedir = VectorAdd(%movedir,"0 0" SPC GetWord(%targetdir,2));
         //%obj.setTransform(%surface);
         ////echo("float");
         }
      }
   }

%obj.sound("move");
%obj.Jet(500);
%movedir = VectorNormalize(%movedir);

%obj.applyImpulse(%obj.getWorldboxCenter(), VectorScale(%movedir,%pulse));
//%obj.setRotation(fullrot("0 0 1",%movedir));
}

function Item::flaredelay(%obj,%set)
{
if (%set)
   %obj.flaredelay++;
else
   %obj.flaredelay = 0;
}

function Item::checkflare(%obj)
{
//echo("checkflare");
%size = $mtcWeaponSize[%obj.weapon];
   if (%size $= "heavy")
      %obj.turret.signature.PointDefLaser();

else if (%obj.turret.signature.homingcount)
   {
      %delay = Limit(%obj.flaredelay,0,10)*getRandom()*1000+100;
      if (getRandom(1))
          %obj.schedule(500+getRandom()*500+%delay,"flare");

   }
}


function Item::FixHeight(%obj)
{
//echo("fixheight");
%pos = getTerrainHeight2(%obj.getWorldboxCenter(),"0 0 -1");
if (GetWord(%obj.getWorldboxCenter(),2) < GetWord(%pos,2))
   {
   %obj.telleport(%pos);
   }
}




function Gamebase::hastag(%obj,%tag)
{
//echo("hastag");
//Sizes
if (%tag $= "l" || %tag $= "m" || %tag $= "h")
   {
   %maxdamage= %obj.getDatablock().maxdamage;
   if (%tag $= "l" && %maxdamage <= 1)
       return 1;
   else if (%tag $= "m" && %maxdamage > 1)
       return 1;
   else if (%tag $= "h" && %maxdamage > 2)
       return 1;
   }
//Powered
else if (%tag $= "p" || %tag $= "n")
   {
   %power = %obj.isPowered();
   if (%tag $= "p" && %power)
      return 1;
   else if (%tag $= "n" && !%power)
      return 1;
   }
//Shielded
else if (%tag $= "s" || %tag $= "u")
   {
   %shield = (%obj.getDataBlock().isShielded == 1 && %obj.getEnergyLevel() > 0);
   if (%tag $= "s" && %shield)
      return 1;
   if (%tag $= "u" && !%shield)
      return 1;
   }
//Controlled
else if (%tag $= "c" || %tag $= "e")
   {
   %Piloted = !((%obj.getType() & $TypeMasks::VehicleObjectType) || %obj.getPilot());
   if (%tag $= "c" && %piloted)
      return 1;
   if (%tag $= "e" && !%piloted)
      return 1;
   }
//Everything ;)
else if (%tag $= "a")
   return 1;

return "";
}

function gettype(%type)
{
//echo("gettype");
if (%type $= "turret")
   return $TypeMasks::TurretObjectType;
else if (%type $= "sensor")
    return $TypeMasks::SensorObjectType;
else if (%type $= "gen")
    return $TypeMasks::GeneratorObjectType;
else if (%type $= "static")
    return $TypeMasks::StaticShapeObjectType;
else if (%type $= "player")
    return $TypeMasks::PlayerObjectType;
else if (%type $= "vehicle")
    return $TypeMasks::VehicleObjectType;
else if (%type $= "forcefield")
    return $TypeMasks::ForceFieldObjectType;
else if (%type $= "all")
    return $TypeMasks::StaticShapeObjectType | $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::ForceFieldObjectType;
else
    return 0;
}

function typelist(%ftype)
{
//echo("typelist");
for(%tt = 0; %tt< GetWordCount(%ftype); %tt++ )
         {
         %part = getWord(%ftype,%tt);
         %type = getSubStr(%part,1,strLen(%part));
         %list = %list | gettype(%type);
         }
return %list;
}

function Gamebase::onpreflist(%obj,%ftype)
{
//echo("onpreftlist");
for(%tt = 0; %tt< GetWordCount(%ftype); %tt++ )
         {
         %part = getWord(%ftype,%tt);
         %tag = getSubStr(%part,0,1);
         %type = getSubStr(%part,1,strLen(%part));
         if (%obj.getType() & gettype(%type) && %obj.hastag(%tag))
            return 1;
         }
return "";
}


function AssesBase(%location)
{
//echo("assesbase");
InitContainerRadiusSearch(%location,200,-1);

 while ((%target = ContainerSearchNext()) != 0)
   {
   if (%target.getType() & $TypeMasks::TurretObjectType && %target.hastag("s"))
       %turretCount++;
   if (%target.getType() & $TypeMasks::InteriorObjectType)
       %interiorCount++;
   if (%target.getType() & $TypeMasks::StaticShapeObjectType)
       {
       %staticCount++;
       %height = %height + GetWord(AboveGround(pos(%target),1),1);
       }
   if (%target.getType() & $TypeMasks::PlayerObjectType)
       %playerCount++;
   %count=%count++;
   }
%height = %height/%staticCount;
//echo(%count);
//echo(%height);
//echo(%interiorcount);
//echo(%turretcount);
//echo(%staticCount);
//echo(%playercount);
}

function Vehicle::getpilot(%obj) ///Needs update to remotecontroll
{
%pilot = %obj.getMountNodeObject(0);
%client = %pilot.client;
return %client;
}


function Shapebase::dir(%obj,%target)
{
%pos1 = %obj.getWorldboxCenter();
%pos2 = %target.getWorldboxCenter();
return VectorNormalize(VectorSub(%pos2,%pos1));
}

function Shapebase::dist(%obj,%target)
{
%pos1 = %obj.getWorldboxCenter();
%pos2 = %target.getWorldboxCenter();
return VectorDist(%pos1,%pos2);
}
function Shapebase::terdist(%obj,%target)
{
%pos1 = %obj.getWorldboxCenter();
%pos2 = %target.getWorldboxCenter();
return VectorDist(GetWords(%pos1,0,1),GetWords(%pos2,0,1));
}

function Shapebase::altdist(%obj,%target)
{
%pos1 = %obj.getWorldboxCenter();
%pos2 = %target.getWorldboxCenter();
return VectorDist(GetWord(%pos1,2),GetWord(%pos2,2));
}
