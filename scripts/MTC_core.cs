// MTC core

$MTC_Loaded = true;

if ($Host::MTC::MTCMax < 1)
	$Host::MTC::MTCMax = 5;
if ($Host::MTC::MTCMin $= "" || $Host::MTC::MTCMin > $Host::MTC::MTCMax)
	$Host::MTC::MTCMin = 1;

compile("scripts/MTC_weap.cs");
exec("scripts/MTC_weap.cs");
compile("scripts/MTC_eweap.cs");
exec("scripts/MTC_eweap.cs");
compile("scripts/MTC_ai.cs");
exec("scripts/MTC_ai.cs");
compile("scripts/MTC_level.cs");
exec("scripts/MTC_level.cs");

//Plasma
$DeathMessageTurretKill[$DamageType::Plasma, 0] = '\c0%1 was vaporized by a rampaging plasma MTC.';
$DeathMessageTurretKill[$DamageType::Plasma, 1] = '\c0%1 tried to dance with a plasma MTC but forgot to wear %3 dancing shoes.';
$DeathMessageTurretKill[$DamageType::Plasma, 2] = '\c0%1 was searching for a place to settle down when a plasma MTC ran over %2.';
//Chain
$DeathMessageTurretKill[$DamageType::Bullet, 0] = '\c0A misguided chain MTC thought %1 would look better perforated.';
$DeathMessageTurretKill[$DamageType::Bullet, 1] = '\c0%1 got in the way of the wrath of an almighty chain MTC.';
$DeathMessageTurretKill[$DamageType::Bullet, 2] = '\c0%1 couldn\'t dodge the 60 bullets per second a chain MTC threw at %2.';
//laser
$DeathMessageTurretKill[$DamageType::Laser, 0] = '\c0%1 looked around the corner only to get %3 head burned of by a laser MTC.';
$DeathMessageTurretKill[$DamageType::Laser, 1] = '\c0%1 forgot that the green beams where harmless, not the red ones.';
$DeathMessageTurretKill[$DamageType::Laser, 2] = '\c0%1 wasn\'t faster than the speed of light and was shot by a laser MTC.';
//grenade
$DeathMessageTurretKill[$DamageType::Grenade, 0] = '\c0%1 mistook a grenade MTC\'s rage for some harmless clouds.';
$DeathMessageTurretKill[$DamageType::Grenade, 1] = '\c0%1 was buried under the wrath of a grenade MTC.';
$DeathMessageTurretKill[$DamageType::Grenade, 2] = '\c0%1 still has to learn that you can\'t pick up grenades and trow them back in Tribes 2.';
//Disc
$DeathMessageTurretKill[$DamageType::Disc, 0] = '\c0%1 dodged right into a disc MTC\'s spinfusor.';
$DeathMessageTurretKill[$DamageType::Disc, 1] = '\c0%1 didn\'t notice the disc MTC that was sneaking up on %2 untill it was too late.';
$DeathMessageTurretKill[$DamageType::Disc, 2] = '\c0%1 had an encounter with a disc MTC and didn\'t live to tell about it.';
//Missile
$DeathMessageTurretKill[$DamageType::Missile, 0] = '\c0%1 forgot to drop a flare while running from a missle MTC\'s missile';
$DeathMessageTurretKill[$DamageType::Missile, 1] = '\c0%1 didn\'t know a missile MTC had declared it no-flying day.';
$DeathMessageTurretKill[$DamageType::Missile, 2] = '\c0%1 was gunned down by a missile MTC\'s mid-day express.';
//Blaster
$DeathMessageTurretKill[$DamageType::Blaster, 0] = '\c0%1 was smacked down by a blaster MTC.';
$DeathMessageTurretKill[$DamageType::Blaster, 1] = '\c0%1 fell down and was run over by a blaster MTC.';
$DeathMessageTurretKill[$DamageType::Blaster, 2] = '\c0%1 had the bad luck of being spotted by a blaster MTC.';
//Mortar
$DeathMessageTurretKill[$DamageType::Mortar, 0] = '\c0%1 never knew his limbs could fly in opposite directions until a mortar MTC showed him.';
$DeathMessageTurretKill[$DamageType::Mortar, 1] = '\c0%1 was fused to the terrain by a mortar MTC.';
$DeathMessageTurretKill[$DamageType::Mortar, 2] = '\c0%1 saw a MTC turret\'s green rain and forgot to get out of the way.';

$mtcBaseSize["light"] = "1.6 3 0.75";
$mtcBaseSize["medium"] = "3 3.6 1.2";
//$mtcBaseSize["light"] = "6 6 2";
//$mtcBaseSize["medium"] = "6 10 1";
$mtcBaseSize["heavy"] = "6 8 2";

$mtcTurretSize["light"] = "1.2 1.5 1.15";
$mtcTurretSize["medium"] = "2.22 2.4 1.44";
//$mtcTurretSize["light"] = "4 5 3";
//$mtcTurretSize["medium"] = "1 1 0.75";
$mtcTurretSize["heavy"] = "1 1 0.75";


$mtcnotTargetMod = 0.5;
$mtcturretMod = 0.25;


datablock AudioProfile(MTCThinkSound) {
	fileName = "fx/misc/bounty_objrem1.wav";
	description = AudioClose3d;
	preload = true;
	effect = MotionSensorDeployEffect;
};

datablock ItemData(mtc)
        {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "ammo_mine.dts";
	mass = 3.0;
	elasticity = 0.2;
	friction = 0;
	rotate = true;
	density = 0.1;
	heatSignature = 1;
	emap = true;

        dynamicType = $TypeMasks::SensorObjectType;
        targetNameTag = 'MTC';
        targetTypeTag = 'Unit';
        sensorData = DeployedOutdoorTurretSensor;
        };


datablock StaticShapeData(mtcTarget) : StaticShapeDamageProfile
        {
	className = "target";
	shapeFile = "reticle_bomber.dts";
        };


datablock StaticShapeData(MtcSig) : StaticShapeDamageProfile
        {
        shapeFile = "Turret_Muzzlepoint.dts";
        className = Target;
        dynamicType = $TypeMasks::SensorObjectType;
        heatSignature = 1;
        targetTypeTag = 'mtc';
        sensorData = DeployedOutdoorTurretSensor;
        maxDamage = 1000;
        destroyedLevel = 200.20;
        disabledLevel = 200.00;
        };


datablock ParticleData(mtcJetParticle)
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 0;
   textureName          = "particleTest";
   colors[0]     = "0.32 0.47 0.47 1.0";
   colors[1]     = "0.32 0.47 0.47 0";
   sizes[0]      = 0.40;
   sizes[1]      = 0.15;
};

datablock ParticleEmitterData(mtcJetEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 3.0;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   overrideAdvances = false;
   thetaMin         = 110;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;

   particles = "mtcJetParticle";
};


datablock TurretData(mtcTurret) : TurretDamageProfile
{
   className = MtcTurret;
   shapeFile = "turret_outdoor_deploy.dts";

   mass = 0.1;

   maxDamage = 2.20;
   destroyedLevel = 2.20;
   disabledLevel = 2.00;
   repairRate = 0;

   deployedObject = true;

   thetaMin = 0;
   thetaMax = 145;
   thetaNull = 90;
   yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances


   selfpower = true;
   maxEnergy = 60;
   rechargeRate = 0.256;

   isShielded = false;
   energyPerDamagePoint = 360;


   renderWhenDestroyed = false;

   heatSignature = 0;

   canControl = true;
   cmdCategory = "DTactical";
   cmdIcon = CMDTurretIcon;
   cmdMiniIconName = "commander/MiniIcons/com_turret_grey";
   firstPersonOnly = true;

   targetTypeTag = 'mtc';
   sensorData = DeployedOutdoorTurretSensor;
   sensorRadius = DeployedOutdoorTurretSensor.detectRadius;
   sensorColor = "191 0 226";

   explosion = HandGrenadeExplosion;
   expDmgRadius = 5.0;
   expDamage = 0.3;
   expImpulse = 500.0;

   debrisShapeName = "debris_generic_small.dts";
   debris = TurretDebrisSmall;
};

datablock TurretData(LightmtcTurret) : mtcTurret
{
   shapeFile = "turret_outdoor_deploy.dts";

   mass = 0.1;

   maxDamage = 1.10;
   destroyedLevel = 1.10;
   disabledLevel = 1.00;
   repairRate = 0;

   maxEnergy = 60;
   rechargeRate = 0.256;

   isShielded = false;
   energyPerDamagePoint = 360;
};

datablock TurretData(MediumMtcTurret) : mtcTurret
{
   shapeFile = "turret_outdoor_deploy.dts";

   mass = 0.1;

   maxDamage = 2.20;
   destroyedLevel = 2.20;
   disabledLevel = 2.00;
   repairRate = 0;

   maxEnergy = 60;
   rechargeRate = 0.256;

   isShielded = false;
   energyPerDamagePoint = 360;
};

datablock TurretData(HeavyMtcTurret) : mtcTurret
{
   shapeFile      = "turret_base_large.dts";

   mass = 0.1;

   maxDamage = 4.40;
   destroyedLevel = 4.40;
   disabledLevel = 4.40;
   repairRate = 0;

   maxEnergy = 60;
   rechargeRate = 0.256;

   isShielded = True;
   energyPerDamagePoint = 30;

   explosion      = TurretExplosion;
   expDmgRadius = 15.0;
   expDamage = 0.66;
   expImpulse = 2000.0;

   debrisShapeName = "debris_generic.dts";
   debris = TurretDebris;
};

datablock TurretData(HugeMtcTurret) : mtcTurret
{
   shapeFile      = "turret_base_large.dts";

   mass = 0.1;

   maxDamage = 2.20;
   destroyedLevel = 2.20;
   disabledLevel = 2.00;
   repairRate = 0;

   maxEnergy = 60;
   rechargeRate = 0.256;

   isShielded = false;
   energyPerDamagePoint = 360;

   explosion      = TurretExplosion;
   expDmgRadius = 15.0;
   expDamage = 0.66;
   expImpulse = 2000.0;

   debrisShapeName = "debris_generic.dts";
   debris = TurretDebris;
};

function mtcTurret::onDestroyed(%this, %obj, %prevState)
        {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;

        %obj.base.Beacon();
        %obj.base.blazetarget();
        %obj.base.specialtarget();

        Parent::onDestroyed(%this, %obj, %prevState);

	%obj.schedule(500, "delete");
        %obj.base.schedule(500, "delete");
        %killer = %obj.lastDamagedBy;

        %killer.mtckills++;
        if (%killer.player.getDatablock().classname $= "armor")
            {
            if (%killer.player.getMountedImage(0).item $= "SuperChaingun")
               {
               messageAll('msgSuicide',  '\c0%1 pulverized a %2 MTC with an huge amount of super chaingun bullets and got 0.1 points for the effort.',%killer.name,%obj.base.weapon);
               %killer.score = %killer.score +0.1;
               }
            else
               {
               %score = $mtcScore[%obj.base.weapon] * (1-($mtcnotTargetMod*(%obj.base.target != %killer.player)));
               %killer.score = %killer.score +%score;
               if (%obj.base.target != %killer.player)
                  {
                  messageAll('msgSuicide',  '\c0%1 has been awarded %2 points for killing a distracted %3 MTC',%killer.name,%score,%obj.base.weapon);
                  }
               else
                  {
                  messageAll('msgSuicide',  '\c0%1 has been awarded %2 points for killing a %3 MTC',%killer.name,%score,%obj.base.weapon);
                  }
                  messageClient(%killer, 'MsgItemPickup', '\c0You\'ve killed %1 MTC\'s.', %killer.mtckills);
                 }
            if (getRandom()*20+%killer.mtckills>30)
                   {
                   echo("bountyhunter");
                   spawnMTC("","",10,%killer);
                   }
            }
        else if (%killer.getOwner())
            {
            %score = $mtcScore[%obj.base.weapon] * (1*$mtcturretMod);
            %killer.getOwner().score = %killer.getOwner().score +%score;
            %killer.getOwner().mtckills++;
            messageClient(%killer, 'MsgItemPickup', '\c0You\'ve killed %1 MTC\'s.', %killer.mtckills);
            messageAll('msgSuicide',  '\c0%1 \'s turret killed a %3 MTC, and got %2 points for it ',%killer.getOwner().name,%score,%obj.base.weapon);
            }
        else if (%killer.getClassname() $= "TURRET")
            {
            if (%killer.IsMtc())
                 messageAll('msgSuicide',  '\c0A MTC was killed by another MTC',%killer.getOwner().name,%obj.base.weapon);
            else
                messageAll('msgSuicide',  '\c0A MTC was killed by a turret',%killer.getOwner().name,%obj.base.weapon);
            }
        else
            messageAll('msgSuicide',  '\c0A MTC was killed',%killer.getOwner().name,%obj.base.weapon);
        if (getRandom()*20+%killer.mtckills>30)
                    {
                   echo("bountyhunter");
                   spawnMTC("","",10,%killer);
                   }
        }

function mtcTurret::onDamage(%data, %obj)
        {
	Parent::onDamage(%data, %obj);
        %obj.base.aggression = %obj.base.aggression + 0.5;
        if (%obj.base.target != %obj.lastDamagedBy && !%obj.base.empsch)
              %obj.base.resetorders();
        }

function MtcTurret::DamageObject(%data,%this, %sourceObject, %position, %amount, %damageType)
{
if (%sourceObject != %this)
   {
   if (%sourceObject.client)
       %this.lastDamagedBy = %sourceObject.client;
   else
       %this.lastDamagedBy = %sourceObject;
   }

Parent::DamageObject(%data,%this, %sourceObject, %position, %amount, %damageType);
}


function mtcTurret::selectTarget(%this, %turret)
{
   %obj = %turret.base;

   //[[Early abort]] we really can't fire.
   if (%obj.nofire)
       {
       %turret.clearTarget();
       return;
       }

   if (%obj.forcetarget())
      {
      %turret.setTargetObject(%obj.forcetarget());
      %obj.blazetarget(%obj.forcetarget());
      return "";
      }


   %TargetSearchMask = $TypeMasks::PlayerObjectType | $TypeMasks::objObjectType | $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType |
   $TypeMasks::SensorObjectType | $TypeMasks::turretType | $TypeMasks::StaticShapeObjectType |  $TypeMasks::ForceFieldObjectType;

   if (!%obj.meddist)
       %range = 500;
   else
       %range = %obj.meddist;
   InitContainerRadiusSearch(%turret.getMuzzlePoint(0),%range,%TargetSearchMask);


   %group = nameToID("MissionCleanup/Deployables");

   while ((%potentialTarget = ContainerSearchNext()) != 0)
         {
         if (%potentialtarget)
            { //Legal ways
            if (%potentialtarget == %turret.base.target || (MTCGroup.blazetarget[%potentialtarget] == 1 && !(%turret.base.weapon $= "target")) )
               {
               %fstat =aboveground(%potentialtarget.getworldboxcenter(),1,0);
               %stat = GetWord(%fstat,0);
               if(%stat $= "open" || %stat $= "roof" || %stat $= "shadow")
                 {
                 if (%potentialtarget.isforcefield())
                     %turret.setTargetObject(%obj.specialtarget(%potentialtarget,0,1));
                    else
                     %turret.setTargetObject(%potentialtarget);
                    %obj.blazetarget(%potentialtarget);
                    return "";
                 }
               else if (!%potentialtarget.isforcefield())
                       {
                       %top = GetWords(%potentialtarget.seetop(),1,3);

                       if (%top !$= "")
                                {
                                %turret.setTargetObject(%obj.specialtarget(%potentialtarget,%top,1));
                                %obj.blazetarget(%potentialtarget,1);
                                return "";
                                }
                       }
                  }
               }
            }

   %obj.blazetarget();
   %obj.specialtarget();
}


//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////


function startMTC() {
	$Host::MTC::Enabled = 1;
	autoMTCSpawn(60000,$Host::MTC::MTCMin,$Host::MTC::MTCMax);
}

function stopMTC() {
	$Host::MTC::Enabled = 0;
	cancel($MTCSpawn);
    if (isObject(MTCGroup))
    MTCGroup.delete();
}

function autoMTCSpawn(%time,%min,%max) {
	cancel($MTCSpawn);
//	%group = nameToID("MissionCleanup/MTCGroup/bases");
	%count = bases.getCount();

	if (%count < %max) {
		for (%c = 0; %c < limit((%min - %count),%min,%max);%c++) {
			%weapon = MtcRandomWeapon();
			%pos = pos(RandomPlayer());
			%angle = getRandom() * 2 * $Pi;
			%dist = getRandom() * 500 + 500;
			%pos = VectorAdd(%pos,getTerrainHeight2( mCos(%angle) * %dist SPC mSin(%angle) * %dist SPC 0));
			spawnMTC(%pos,%weapon);
		}
	}
	$MTCSpawn = schedule(%time,0,"autoMTCSpawn",%time,%min,%max);
}

function spawnMTC(%pos,%weapon,%aggression,%target)
{
if ($mtcWeaponSize[%weapon]$="")
    %weapon = MtcRandomWeapon();

  %obj = new Item() {
         className = pack;
         dataBlock = mtc;
         targetNameTag = %weapon;
        };

 %obj.addToMTCGroup();
 %obj.setTransForm(%pos SPC "1 0 0 0");
 %obj.size = $mtcWeaponSize[%weapon];

 %obj.SetScale($mtcBaseSize[%obj.size]);
 if (!%aggression)
    %obj.aggression = getRandom()* 5;
  else
    %obj.aggression = %aggression;

  if (%target)
     %obj.forcetarget = %target;

  %obj.nonotify = 1;

  %obj.weapon = %weapon;

  echo(%weapon SPC "mtc spawned with" SPC %obj.aggression SPC "anger level");
  %obj.Initialize();
}

function shapebase::addToMTCGroup(%obj,%set)
{

   if (!isObject(MTCGroup))
      {
      %main = new SimGroup("MTCGroup");
      MissionCleanup.add(%TigGroup);
      %base = new Simgroup("bases");
      %turrets = new Simgroup("turrets");
      %beacons = new Simgroup("beacons");
      %main.add(%base);
      %main.add(%turrets);
      %main.add(%beacons);
      }
if (%set == 1)
   Turrets.add(%obj);
else if (%set == 2)
   beacons.add(%obj);
else
   bases.add(%obj);


}

function GameBase::isMTC(%obj) {
	%name = %obj.getDataBlock().getName();
	if (%name $= "lightMTCTurret")
		return 1;
	if (%name $= "mediumMTCTurret")
		return 1;
	if (%name $= "heavyMTCTurret")
		return 1;
	if (%name $= "HugeMTCTurret")
		return 1;
	if (%name $= "MTCSig")
		return 1;
	if (%name $= "MTC")
		return 1;
	return "";
}

function mtc::onCollision(%data,%obj,%col)
{

}

function mtcsig::onCollision(%data,%obj,%col)
{

}



function Item::Arm(%obj)
{
//echo("arm");
%team = 3;

//ChainTurret

%block = "mtcTurret";
//%turret = TurretData::create(%block);
%size = $mtcWeaponSize[%obj.weapon];
 %turret = new Turret()
         {
         className = DeployedTurret;
         dataBlock = %size @ mtcTurret;
         maxdamage = 20;
         };


%turret.SetScale($mtcTurretSize[%obj.size]);
%obj.mountObject(%turret, 0);
%obj.mountObject(%sign, 1);

%turret.setSelfPowered();
%turret.playThread($DeployThread, "deploy");
%turret.setRechargeRate(%turret.getDatablock().rechargeRate);

%barrel = %obj.weapon @ "TurretBarrel";
%turret.mountImage(%barrel,0,false);

%obj.turret = %turret;
%turret.base = %obj;

%turret.team = %team;
setTargetSensorGroup(%turret.getTarget(), %team);

%turret.addToMTCGroup(1);
}



function getorders()
{
//%group = nameToID("MissionCleanup/MTCGroup/bases");
echo(bases.getCount() SPC "mtcs found");
for( %c = 0; %c < bases.getCount(); %c++ )
   {
   %obj = bases.getObject(%c);
   if (%obj.order !$= "")
      {
      %totarg = %totarg+%obj.aggression;
      if (mtcCleanTarget(%obj.target))
         {
         %target = %obj.target.getDatablock().classname;
         if (!%gottar[%target])
            {
            %targets++;
            %targetname[%targets] = %target;
            }
            %gottar[%target]++;

         }
      %order = %obj.order;
      if (!%gotor[%order])
         {

         %orders++;
         %odername[%orders] = %order;
         }
         %gotor[%order]++;
      %weapon = %obj.weapon;
      if (!%gotweap[%weapon])
         {

         %weapons++;
         %weaponname[%weapons] = %weapon;
         }
         %gotweap[%weapon]++;
      }
   }
echo("       -" SPC "with an avarage aggression of" SPC %totarg/(bases.getCount()) );
echo("  +" SPC %orders SPC "order types found");
for( %c = 1; %c <= %orders; %c++ )
   {
   %order = %odername[%c];
   echo("    -" SPC %gotor[%order] SPC "times" SPC %order SPC "ordered");
   }
echo("  +" SPC %targets SPC "target types found");
for( %c = 1; %c <= %targets; %c++ )
   {
   %target = %targetname[%c];
   echo("    -" SPC %gottar[%target] SPC "times" SPC %target SPC "targeted");
   }
echo("  +" SPC %weapons SPC "weapon types found");
for( %c = 1; %c <= %weapons; %c++ )
   {
   %weapon = %weaponname[%c];
   echo("    -" SPC %gotweap[%weapon] SPC "times" SPC %weapon SPC "armed");
   }

}

function Item::Notify(%obj,%id)
{
if (!%obj.nonotify && !$Nomtcnotify)
   {
   switch$ (%id) {
    		  case "attack":
                   echo("[[mtc]]" SPC %obj SPC "is attacking" SPC %obj.target.getDatablock().Classname);
                   echo("  +[[Target]]" SPC %obj.target SPC "is" SPC VectorDist(pos(%obj),pos(%obj.target)) SPC "meters away from mtc");
		  case "flee":
                   echo("[[mtc]]" SPC %obj SPC "is fleeing from" SPC %obj.target.getDatablock().Classname);
                   echo("  +[[Target]]" SPC %obj.target SPC "is" SPC VectorDist(pos(%obj),pos(%obj.target)) SPC "meters away from mtc");
                  case "roam":
                   echo("[[mtc]]" SPC %obj SPC "is roaming around" SPC %obj.target.getDatablock().Classname);
                   echo("  +Closest [[target]]" SPC %obj.target SPC "is" SPC VectorDist(pos(%obj),pos(%obj.target)) SPC "meters away from mtc");
                  case "repair":
                   echo("[[mtc]]" SPC %obj SPC "is repairing at" SPC MFloor(%obj.turret.getDamagePct() *100) @"% damage and" SPC MFloor(%obj.ammo["flare"]) SPC "flares");
                  case "ammo":
                   echo("[[mtc]]" SPC %obj SPC "has:");
                   echo("                     + " @ MFloor(%obj.ammo["flare"]) SPC "flares");
                   echo("                     + " @ MFloor(%obj.ammo["mine"]) SPC "mines");
                   echo("                     + " @ MFloor(%obj.ammo["cloak"]) SPC "cloak seconds");
                   echo("           and an aggression level of" SPC MFloor(%obj.aggression));
                  }
   }
}

function mtcnerfwar()
{
for( %c = 0; %c < ClientGroup.getCount(); %c++ )
   {
   %client = ClientGroup.getObject(%c);
   spawnMTC("0 0 0","nerf",10,%client);
   }
}


function Item::blazetarget(%obj,%target,%set)
{
//echo("blazetarget");
%obj.cloak();
if (%obj.weapon $= "target")
   {
   if (%set)
      {
      if (%obj.blazetarget == %target)
          return "";
      else if (IsObject(%target))
           {
           %obj.blazetarget();
           %obj.blazetarget = %target;
           MTCGroup.blazetarget[%target] = 1;
           }
      }
   else
      {
      MTCGroup.blazetarget[%obj.blazetarget] = 0;
      %obj.blazetarget = "";
      }
   }
}


function Item::VisTarget(%obj,%linkobj,%pos,%dir,%sided)
{
//echo("visTarget");
if (!Isobject(%obj.vistarget1))
   {
   %tar1 = new StaticShape()
           {
           className = Target;
           dataBlock = mtcTarget;
           scale = "0.25 0.25 0.25";
           };
   }
else
   %tar1 = %obj.vistarget1;

if (%pos $= "" && Isobject(%linkobj))
    %pos = pos(%linkobj);
else if (%pos $= "")
    %pos = pos(%obj);

%rot1 = fullrot(%dir,"0 0 1");
%tar1.setTransform(%pos SPC %rot1);
%obj.vistarget1 =%tar1;
%tar1.schedule(60000,"delete");

   if (%sided)
      {
      if (!Isobject(%obj.vistarget2))
         {
      %tar2 = new StaticShape()
              {
              className = Target;
              dataBlock = mtcTarget;
              scale = "0.25 0.25 0.25";
              };
         }
      else
          %tar2 = %obj.vistarget2;

      %rot2 = fullrot(VectorScale(%dir,-1),"0 0 1");
      %tar2.setTransform(%pos SPC %rot2);
      %obj.vistarget2 = %tar2;
      %tar2.schedule(60000,"delete");
      }

return %tar1;
}

function Item::specialtarget(%obj,%target,%pos,%set)
{
//echo("specialtarget");
if (%set)
   {
   if (%pos !$= "")
      {
      %ppos = getTerrainHeight2(%target.getWorldboxCenter());
      %res = containerRayCast(%obj.turret.getWorldboxCenter(),VectorAdd(%ppos,"0 0 0.1"), -1,%obj.turret);
      if (GetWord(%res,0) == %target)
         {
         %dir = realvec(%target,NormalFromRaycast(%res));
         %pos = VectorAdd(PosFromRaycast(%res),VectorScale(%dir,0.1));
         }
      else
         {
          %dir = "0 0 1";
         }
     }
     else
        {
        %pos = %target.getWorldboxCenter();
        %dir = VectorNormalize(VectorSub(%pos,pos(%obj.turret)));
        %rot1 = fullrot(%dir,0);
        %rot2 = fullrot(VectorScale(%dir,-1));
        }

     return %obj.VisTarget(%target,%pos,%dir,1);
     }
else
     {
     if (isObject(%obj.vistarget1))
         %obj.vistarget1.delete();
     if (isObject(%obj.vistarget2))
         %obj.vistarget2.delete();
     }
}

function Item::canfire(%obj,%set)
{
//echo("canfire");
if (%set $= "target")
   {
   %obj.canfire = "target";
   %obj.nofire = "";
   if (%obj.turret.getTargetObject() && %obj.turret.getTargetObject() != %obj.target)
       %obj.turret.clearTarget();
   }
else if (%set $= "all")
   {
   %obj.canfire = "all";
   %obj.nofire = "";
   }
else if (!%set)
   {
   %obj.turret.clearTarget();
   %obj.nofire = 1;
   %obj.canfire = "none";
   }
}


//////////////////////////////////////////////
//////////////Inventory Actions///////////////
//////////////////////////////////////////////

//Telleportation

function Item::telleport(%obj,%loc)
{
//echo("telleport");
if (!%obj.notelle)
   {
   if (!%obj.notellefx)
      {
      teleportStartFX(%obj);
      teleportStartFX(%obj.turret);
      %obj.schedule(1002,"teleportEndFX",%obj);
      %obj.turret.schedule(1002,"teleportEndFX",%obj.turret);
      %obj.play3D(TelePadBeamSound);
      }
   %obj.schedule(1001,"SetTransform",%loc SPC rot(%obj));
   %obj.turret.schedule(1001,"SetTransform",%loc SPC rot(%obj.turret));
   }
}

//cloaking

function Item::cloak(%obj,%set)
{
//echo("cloak");
// We need atleast 20 seconds to activate cloak
if (%set && %obj.ammo["cloak"] >= 20 && !%obj.cloaked && !%obj.nocloak && getSimTime()-%obj.lastcloaktime>5000)
   {
   %obj.cloaked = 1;
   %obj.setCloaked(True);
   %obj.turret.setCloaked(True);
   %obj.cloaktime = getsimtime();
   %obj.silent(1);
   %obj.schedule(%obj.ammo["cloak"]*1000,"cloak",0);
   %obj.lastcloaktime = GetSimTime();
   }
else if (!%set)
   {
   if (%obj.cloaked)
     {
      %obj.ammo["cloak"]= %obj.ammo["cloak"]-(getSimtime()-%obj.cloaktime)/1000;
      %obj.cloaked = 0;
     }
   if (%obj.silent)
       %obj.silent();
   if (%obj.isCloaked())
       %obj.setCloaked(False);
   if (%obj.turret.isCloaked())
       %obj.turret.setCloaked(False);
   %obj.beacon(1);
   }
}

function Item::silent(%obj,%set)
{
//echo("silent");
if (!%obj.nosilent && %set)
   {
   if (!%obj.attackcloak)
        {
        %obj.canfire();
        %obj.flash();
        }
   //%obj.turret.setHeat(0);
   %obj.noflare = 1;
   %obj.notellefx = 1;
   %obj.nojet = 1;
   %obj.nosound = 1;
   %obj.nobeacon = 1;
   %obj.Beacon();
   %obj.silent = 1;
   }
else if (!%set)
   {
   //%obj.turret.setHeat(1);
   %obj.canfire("target");
   %obj.notellefx = "";
   %obj.noflare = "";
   %obj.nojet = "";
   %obj.nosound = "";
   %obj.nobeacon = "";
   %obj.noflare = "";
   %obj.silent = "";
   }
}


//Mines

function Item::Mine(%obj)
    {
//echo("mine");
    if (%obj.ammo["mine"] > 0 && !%obj.nomine && %obj.lastThrowtime + 5000 < getSimTime())
       {
       %obj.ammo["mine"] = %obj.ammo["mine"] -1;

       %thrownItem = new Item()
                  {
                  dataBlock = MineDeployed;
                  sourceObject = %obj;
                  };

        MissionCleanup.add(%thrownItem);

        // throw it
        %dir = VectorNormalize(VectorSub(%obj.target.getWorldboxCenter(),%obj.getWorldboxCenter()));
        %vec = vectorScale(%dir, (20.0));

        // add a vertical component to give it a better arc
        %dot = vectorDot("0 0 1", %dir);
        if(%dot < 0)
           %dot = -%dot;
        %vec = vectorAdd(%vec, vectorScale("0 0 4", 1 - %dot));

        // add player's velocity
        %vec = vectorAdd(%vec, vectorScale(%obj.getVelocity(), 0.4));
        %pos = %obj.getWorldboxCenter();

        %thrownItem.sourceObject = %obj;
        %thrownItem.team = %obj.team;
        %thrownItem.setTransform(%pos);

        %thrownItem.applyImpulse(%pos, %vec);
        %thrownItem.setCollisionTimeout(%obj);
        serverPlay3D(GrenadeThrowSound, %pos);
        %obj.lastThrowtime = getSimTime();

        %thrownItem.getDataBlock().onThrow(%thrownItem, %obj);
        schedule(30 * 60 * 1000,0,"explodeMine",%thrownItem, true);
        }

   }

// Flares

function Item::flare(%obj)
{
//echo("flare");
if (%obj.ammo["flare"] > 0 && !%obj.noflare)
  {
   %obj.ammo["flare"] = %obj.ammo["flare"] - 1;
  %size = getRandom() * 3;
  %p = new FlareProjectile() {
         dataBlock        = FlareGrenadeProj;
         initialDirection = "0 0" SPC %size;
         initialPosition  = %obj.getWorldboxCenter();
         sourceObject     = %obj;
         sourceSlot       = 0;
      };
      FlareSet.add(%p);
      MissionCleanup.add(%p);
      serverPlay3D(GrenadeThrowSound, %obj.getWorldboxCenter());
      %p.schedule(6000, "delete");
   }
}

function Item::Incomming(%obj)
{
InitContainerRadiusSearch(%obj.getWorldboxCenter(),500,-1);
while ((%target = ContainerSearchNext()) != 0)
   {
   if (MissileSet.isMember(%target))
      {
      if (%target.gettargetobject() == %obj)
         {
         return %target;
         }
      }
   }
return "";
}



//Flash

function Item::Flash(%obj)
{
//echo("flash");
if (%obj.ammo["flash"] > 0 && !%obj.noflash)
  {
  %obj.ammo["flash"] = %obj.ammo["flash"] - 1;
  %size = getRandom() * 3;

   %flash = new Item()
                  {
                  dataBlock = FlashGrenadeThrown;
                  sourceObject = %obj;
                  };
   %flash.setTransform(VectorAdd(pos(%obj),"0 0 4") SPC "1 0 0 0");

    schedule(500,0,"detonateFlashGrenade",%flash);
    MissionCleanup.add(%flash);
    serverPlay3D(GrenadeThrowSound, %obj.getWorldboxCenter());
   }
}


//Jetting

function ShapeBase::Jet(%obj,%time)
{

if (isObject(%obj) && !%obj.nojet && !$nojet)
   {
   if ($MTC::FancyJet)
      {
      for (%c = 0; %c < (%time/50); %c++)
          {
          %obj.schedule(%c*50,"JetPoof",50);
          }
      }
   else
      {
      createLifeEmitter(%obj.getworldBoxCenter(), mtcJetEmitter, 100);
      }
   }
}
//SmallLightDamageSmoke
function Shapebase::JetPoof(%obj,%time)
{
if (isObject(%obj) && !%obj.nojet)
   {
   %pos = %obj.getWorldboxCenter();
    %obj.firePoof = new ParticleEmissionDummy()
               {
		position = %pos;
		rotation = "";
         	DataBlock = "defaultEmissionDummy";
         	lockCount = "0";
         	homingCount = "0";
                emitter = "mtcJetEmitter";
        	velocity = "10";
               };
               MissionCleanup.add(%obj.firePoof);
               %obj.firePoof.schedule(%time, "delete");


   }
}

///

function GiveSign(%obj)
{
if (!IsObject(%obj.signature))
   {
   %sign = new StaticShape(){
           dataBlock = MtcSig;
           };
   }
   else
   %sign = %obj.signature;

   %sign.setTransform(%obj.getTransform());
   %sign.team = %obj.team;
   %sign.setHeat(1);
   %obj.mountObject(%sign, 4);
   setTargetSensorGroup(%sign.getTarget(), %obj.team);

   %obj.signature = %sign;
   %sign.owner = %obj;
   CheckSign(%sign);
   return %sign;
}

function CheckSign(%sign)
{
if (!IsObject(%sign.owner)&& IsObject(%sign))
   {
   %sign.delete();
   }
else if (IsObject(%sign.owner)&& IsObject(%sign))
   Schedule(30000,0,"CheckSign",%sign);
}

function Item::beacon(%obj,%set)
{


if (%set && !%obj.nobeacon && !%obj.turret.isRemoved && IsObject(%obj))
   {

   if (!IsObject(%obj.enemyBeacon1))
      {
      %beacon1 = new BeaconObject(){
         datablock = BomberBeacon;
      };

      }
   else
      %beacon1 = %obj.enemyBeacon1;
   if (!IsObject(%obj.enemyBeacon2))
      {
      %beacon2 = new BeaconObject(){
         datablock = BomberBeacon;
      };
      }
   else
      %beacon2 = %obj.enemyBeacon2;



   %beacon1.team = 2;
   %beacon2.team = 1;
   %beacon1.owner = %obj;
   %beacon2.owner = %obj;

   %beacon1.setTarget(2);
   %beacon2.setTarget(1);
   %obj.mountObject(%beacon1, 4);
   %obj.mountObject(%beacon2, 5);
   %obj.enemyBeacon1 = %beacon1;
   %obj.enemyBeacon2 = %beacon2;
   %beacon1.addToMTCGroup(2);
   %beacon2.addToMTCGroup(2);
   %beacon1.setBeaconType(friend);
   %beacon2.setBeaconType(friend);
   %sign = GiveSign(%obj.turret);
   %sign.addToMTCGroup(2);
   //%obj.turret.mountObject(%sign,4);
   }
else
   {
   if (IsObject(%obj.enemyBeacon1))
      {
      %obj.enemyBeacon1.delete();
      %obj.enemyBeacon1 = "";
      }
   if (IsObject(%obj.enemyBeacon2))
      {
      %obj.enemyBeacon2.delete();
      %obj.enemyBeacon2 = "";
      }
   if (IsObject(%obj.turret.signature))
      {
      %obj.turret.signature.delete();
      %obj.turret.signature = "";
      }
   }
}

function Item::Sound(%obj,%id)
{

if (!%obj.nosound)
   {
   switch$ (%id) {
    		  case "think":
                  %obj.play3D(MTCThinkSound);
                  case "move":
                  %obj.play3D(SkiAllSoftSound);
                 }
   }
}

function Item::Emp(%obj,%time)
{
//echo("emp");
cancel(%obj.empsch);
cancel(%obj.invloop);
cancel(%obj.evalloop);
cancel(%obj.mainrun);
%obj.canfire = "";
%obj.nofire = 1;
%obj.turret.clearTarget();
//%obj.zaplight(%time);
//createLifeEmitter(%obj.getWorldboxCenter(),ELFSparksEmitter, limit(%time-500,0));
%obj.turret.zapobject();
%obj.empsch = %obj.schedule(%time,"unEmp");
}

function Item::unEmp(%obj)
{
echo("umemp");
cancel(%obj.empsch);
%obj.empsch = "";
%obj.turret.stopzap();
%obj.nofire = "";
%obj.invloop();
%obj.evaluate();
}


////////////////////////////////////////////////////////////
///////////////////SHIELDING SYSTEM/////////////////////////
////////////////////////////////////////////////////////////


function gameConnection::antiMissile(%obj)
{
cancel(%obj.shieldsch);
if (!IsObject(%obj))
return "";
if (IsObject(%obj.player))
    %obj.player.pointdeflaser();
%obj.shieldsch = %obj.schedule(500,"antiMissile");
}

function ShapeBase::GetHoming(%obj)
{

if (!%obj.homingcount)
   return "";
InitContainerRadiusSearch(%obj.getWorldBoxCenter(),75,-1);
while ((%target = ContainerSearchNext()) != 0)
   {
   if (MissileSet.isMember(%target))
       {
       if (%target.getTargetObject() == %obj)
            {
            return %target;
            }
       }
   }
return "";
}


function MissileBarrelLarge::onFire(%data,%obj,%slot)
{
   %p = Parent::onFire(%data,%obj,%slot);
   Missileset.add(%p);
   if (%obj.getControllingClient())
   {
      // a player is controlling the turret
      %target = %obj.getLockedTarget();
   }
   else
   {
      // The ai is controlling the turret
      %target = %obj.getTargetObject();
   }

   if(%target)
      %p.setObjectTarget(%target);
   else if(%obj.isLocked())
      %p.setPositionTarget(%obj.getLockedPosition());
   else
      %p.setNoTarget(); // set as unguided. Only happens when itchy trigger can't wait for lock tone.
}


function ShapeBase::PointDefLaser(%obj)
{

if (%missile = %obj.getHoming())
   {
   %pos = %obj.getWorldboxCenter();
   %result = Zaplos(%pos,%missile,0,%obj.owner);
   if (GetWord(%result,0))
      {
      %lastpos = %missile.getWorldboxCenter();
      schedule(100,0,"putscreen",%obj,%missile,%lastpos);
      }
   }
}

function putscreen(%obj,%missile,%lastpos)
{
%newpos = %missile.getWorldboxCenter();
%step = VectorSub(%lastpos,%newpos);
%pos = %obj.getWorldBoxCenter();
%obj.screen(VectorAdd(%pos,%step),VectorNormalize(%step));
}

function ShapeBase::ActiveShield(%obj)
{
cancel(%obj.shieldsch);
InitContainerRadiusSearch(%obj.getWorldBoxCenter(),50,-1);
echo("shield");
while ((%target = ContainerSearchNext()) != 0)
   {
   if (%obj.IsEnemy(%target.sourceobject))
       {
       %pos = %target.getWorldBoxCenter();
       %dir = VectorNormalize(VectorSub(%obj.getWorldBoxCenter(),%pos));
       %obj.schedule(10+%count*10,"screen",VectorAdd(%pos,%dir),%dir);
       %count++;
       }
   }
%obj.shieldsch = %obj.schedule(100,"activeShield");
}

function ShieldTrack(%obj,%target,%lastpos)
{
if (!isObject(%obj) || !IsObject(%target))
   return "";
%pos = %obj.getWorldBoxCenter();
%newpos = %target.getWorldBoxCenter();
%lastdist = VectorDist(%pos,%lastpos);
%newdist =  VectorDist(%pos,%newpos);

if (%lastdist < %newdist)
   return "";

if (%newdist < 30)
    %obj.screen(%spos,%dir);
}

function ShapeBase::screen(%owner,%pos,%nrm)
{
cancel(%screen.del);
if (!ISObject(%owner.screen))
    {
    %screen = new ForceFieldBare()
              {
	      DataBlock = DeployedForceField3;
	      scale = "5 5 0.1";
              };
    }
   else
   %screen = %owner.screen;
   %left= VectorCross(%nrm,"0 0 1");
   %up = VectorCross(%left,%nrm);
   %offset = VectorScale(VectorAdd(%left,%up),-2.5);
   %rot = Fullrot(%nrm,"0 0 1");
   %screen.setTransform(VectorAdd(%pos,%offset) SPC %rot);
   %owner.screen = %screen;
   %screen.del = %screen.Schedule(500,"delete");

}

function LockedMissile(%pos,%dir,%obj)
{
%p = new SeekerProjectile() {
         dataBlock        = ShoulderMissile;
         initialDirection = %dir;
         initialPosition  = %pos;
         //sourceObject     = "";
         //sourceSlot       = %obj.fireslot;
         //vehicleObject    = %obj;
         };
   //%p = Parent::onFire(%data, %obj, %slot);
   MissileSet.add(%p);
   %p.setObjectTarget(%obj);

}


/////////////////////////////////////////////////////////////
///////////////////////Camp//////////////////////////////////
/////////////////////////////////////////////////////////////

function Shapebase::startterrainscan(%obj,%type)
{
if (%type == 1)
   {
   %obj.terrainscan(2,0,180,1,0,20,50);
   %obj.terrainscan(2,0,-180,-1,0,20,50);
   %obj.maxresult = 2;
   }
else if (%type == 2)
   {
   %obj.terrainscan(2,0,180,1,0,20,2000);
   %obj.terrainscan(2,0,-180,-1,0,20,2000);
   %obj.terrainscan(2,0,180,1,10,20,2000);
   %obj.terrainscan(2,0,-180,-1,10,20,2000);
   %obj.terrainscan(2,0,180,1,20,20,2000);
   %obj.terrainscan(2,0,-180,-1,20,20,2000);
   %obj.maxresult = 6;
   }
else
   %obj.terrainscan(2,0,360,1,0,20,50);
}

function Shapebase::terrainscan(%obj,%height,%startangle,%maxangle,%tick,%pitch,%minrange,%maxrange)
{
%pos = %obj.getWorldboxCenter();
%rot = %obj.getRotation();
%targeter = new StaticShape()
         {
	 position = %pos;
         rotation = %rot;
	 datablock = "LightningTarget";
	 };
%targeter.center = %obj;
%targeter.initialrot = GetWord(%rot,3)+mDegtoRad(%startangle);

%targeter.height = %height;
%targeter.maxangle = %maxangle;
%targeter.tick = %tick;
%targeter.pitch = mDegtoRad(%pitch);
%targeter.minrange = %minrange;
%targeter.maxrange = %maxrange;

%pointer = new TargetProjectile()
     {
     dataBlock = BasicTargeter;
     initialDirection = "0 0 1";
     initialPosition = "0 0 1";
     sourceObject = %targeter;
     damageFactor = 1;
     sourceSlot = 0;
     };
%targeter.pointer = %pointer;
%targeter.scandir(%tick);
}




function Shapebase::ScanDir(%obj,%dir)
{
//Aim beam
%rot = mDegToRad(%dir)+%obj.initialrot;
%rotation = rotadd("0 0 1" SPC %rot,"1 0 0" SPC %obj.pitch);
%vec = mSin(%rot)*mCos(%obj.pitch) SPC mCos(%rot)*mCos(%obj.pitch) SPC -1*mSin(%obj.pitch);
%offset = VectorAdd("0 0" SPC %obj.height,%vec);

%pos = VectorAdd(getTerrainHeight2(%obj.center.getWorldboxCenter()),%offset);
%obj.setTransform(%pos SPC %rotation);

///Cast terrain
%mask = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType;
%res = ContainerRayCast(%pos,VectorAdd(%pos,VectorScale(%vec,%obj.maxrange)),%mask, %obj.center);

if ((!%res && %obj.maxrange) || VectorDist(%pos,GetWords(%res,1,3)) < %obj.minrange)
   %obj.result++;

if (mAbs(%dir) >= mAbs(%obj.maxangle))
    %obj.terrainscanned();
else
    %obj.schedule(10,"Scandir",%dir+%obj.tick);
}

function Shapebase::terrainscanned(%obj)
{
%obj.center.result = stripEndSpaces(%obj.center.result SPC %obj.result);
%result = %obj.center.result;
if (getWordCount(%result) >= limit(%obj.center.maxresult,1))
   {
   echo(%result);
   %obj.center.result = "";
   }
%obj.pointer.delete();
%obj.delete();
}

