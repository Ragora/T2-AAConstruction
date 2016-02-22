//Give your avarage mpb unless enabled.. :)
$mpm::nukempb = 1;		//Mpb's deploy with nukes
$mpm::targetenemies = 0;	//Enemy targets/lasers can be targeted
$mpm::targetmpbs = 1;		//Mpbs become a target when deployed.



$mpm_loads = 0;
$mpm_ae=0;
$mpm_ve=0;
$mpm_te=0;
$mpm_be=0;
$mpm_ne=0;

//Discourage damage payloads by putting them at end of list. >:D
exec("scripts/mpm/MpM_Ae.cs");
exec("scripts/mpm/MpM_Se.cs"); //Missile datablocks
exec("scripts/mpm/MpM_Te.cs"); //Telleporter Missile
exec("scripts/mpm/MpM_Ve.cs"); //Vehicle missile
exec("scripts/mpm/MpM_Be.cs"); //Multi payload missiles
if ($mpm_NE != 1)
   {
   $mpm_load[$mpm_loads] = Mpm_Nuke_Load;
   $mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_Nuke2_Load;
   $mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_Hole_Load;
   $mpm_loads++;
   $mpm_ne = 1;
   }


//All missiles loaded so, 
//Don't complain it seems to work perfectly :D
MpmMissile1.LifeTimeMs = -1;
Mpm_B_MIS.LifeTimeMs = -1;
Mpm_B_MIS1.LifeTimeMs = -1;
Mpm_B_MIS2.LifeTimeMs = -1;
Mpm_B_MIS3.LifeTimeMs = -1;
Mpm_B_MIS4.LifeTimeMs = -1;

datablock ItemData(Mpm_Nuke_Load):Mpm_Base_Load
{
   cost = 150;
   name = "[Weapon] Drain Bramage's, Attomic(tm) Nuke Cannon"; 
   missile = MpmMissile1;
   slot = 0;
};

datablock ItemData(Mpm_Nuke2_Load):Mpm_Base_Load
{
   cost = 250;
   name = "[Weapon] Arrow IV 150 kt Nuke"; 
   missile = MpmMissile1;
   slot = 0;
};

datablock ItemData(Mpm_Hole_Load):Mpm_Base_Load
{
   cost = 200;
   name = "[Weapon] Black Hole";
   missile = MpmMissile1;
   slot = 0;
};

datablock AudioProfile(LaserTargetSound) {
	fileName = "fx/misc/red_alert_short.wav";
	description = AudioClose3d;
	preload = true;
	effect = MotionSensorDeployEffect;
};

datablock TurretData(MpmTurret) : TurretDamageProfile
{
   className      = TurretBase;
   catagory       = "Turrets";
   shapeFile      = "turret_base_large.dts";
   preload        = true;

   mass           = 1.0;  // Not really relevant

   maxDamage      = 2.25;
   destroyedLevel = 2.25;
   disabledLevel  = 1.35;
   explosion      = TurretExplosion;
	expDmgRadius = 15.0;
	expDamage = 0.66;
	expImpulse = 2000.0;
   repairRate     = 0.5;
   emap = true;

   thetaMin = 90;
   thetaMax = 90;

   isShielded           = true;
   energyPerDamagePoint = 50;
   maxEnergy = 150;
   rechargeRate = 0.31;
   humSound = SensorHumSound;
   pausePowerThread = true;

   targetNameTag = 'Base';
   targetTypeTag = 'Turret';
   sensorData = TurretBaseSensorObj;
   sensorRadius = TurretBaseSensorObj.detectRadius;
   sensorColor = "0 212 45";
   heatSignature = 1.0; 
};


datablock TurretImageData(MpmBarrel)
{
   shapeFile = "Turret_Muzzlepoint.dts";
   item      = PlasmaBarrelLargePack;

   projectile = PlasmaBarrelBolt;
   projectileType = LinearFlareProjectile;
   usesEnergy = true;
   fireEnergy = 1;
   minEnergy = 1;
   emap = true;

   offset = "-0.7 -0.04 -0.4";
   rotation = "0 1 0 90";

   yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 360.0; // more or less just tolerances


   // Turret parameters
   activationMS      = 1000;
   deactivateDelayMS = 1500;
   thinkTimeMS       = 200;
   degPerSecTheta    = 5;
   degPerSecPhi      = 5;
   attackRadius      = 10000;

   // State transitions
   stateName[0]                  = "Activate";
   stateTransitionOnNotLoaded[0] = "Dead";
   stateTransitionOnLoaded[0]    = "ActivateReady";

   stateName[1]                  = "ActivateReady";
   stateSequence[1]              = "Activate";
   stateSound[1]                 = PBLSwitchSound;
   stateTimeoutValue[1]          = 1;
   stateTransitionOnTimeout[1]   = "Ready";
   stateTransitionOnNotLoaded[1] = "Deactivate";
   stateTransitionOnNoAmmo[1]    = "NoAmmo";

   stateName[2]                    = "Ready";
   stateTransitionOnNotLoaded[2]   = "Deactivate";
   stateTransitionOnTriggerDown[2] = "Fire";
   stateTransitionOnNoAmmo[2]      = "NoAmmo";

   stateName[3]                = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3]        = 0.15; //0.3
   stateFire[3]                = true;
   stateRecoil[3]              = LightRecoil;
   stateAllowImageChange[3]    = false;
   stateSequence[3]            = "Fire";
   stateScript[3]              = "onFire";
   stateSound[3]                    = "";

   stateName[4]                  = "Reload";
   stateTimeoutValue[4]          = 0.4; //0.8
   stateAllowImageChange[4]      = false;
   stateSequence[4]              = "Reload";
   stateTransitionOnTimeout[4]   = "Ready";
   stateTransitionOnNotLoaded[4] = "Deactivate";
   stateTransitionOnNoAmmo[4]    = "NoAmmo";

   stateName[5]                = "Deactivate";
   stateSequence[5]            = "Activate";
   stateDirection[5]           = false;
   stateTimeoutValue[5]        = 1;
   stateTransitionOnLoaded[5]  = "ActivateReady";
   stateTransitionOnTimeout[5] = "Dead";

   stateName[6]               = "Dead";
   stateTransitionOnLoaded[6] = "ActivateReady";

   stateName[7]             = "NoAmmo";
   stateTransitionOnAmmo[7] = "Reload";
   stateSequence[7]         = "NoAmmo";
};

datablock StaticShapeData(MpmTurretTarg) : StaticShapeDamageProfile
        {
        shapeFile = "Turret_Muzzlepoint.dts";
        className = Target;
        dynamicType = $TypeMasks::SensorObjectType;
        heatSignature = 1;
        //targetTypeTag = 'mtc';
        sensorData = DeployedOutdoorTurretSensor;
        maxDamage = 1000;
        destroyedLevel = 200.20;
        disabledLevel = 200.00;
        };

datablock StaticShapeData(VisTarget) : StaticShapeDamageProfile
        {
	className = "target";
	shapeFile = "reticle_bomber.dts";
        };

datablock StaticShapeData(TurretArm) : StaticShapeDamageProfile 
        {
	className = "crate";
	shapeFile = "turret_base_large.dts";//"vehicle_air_hapc.dts";
};

datablock StaticShapeData(TurretLift) : StaticShapeDamageProfile 
        {
	className = "crate";
	shapeFile = "turret_base_mpb.dts";//"vehicle_air_hapc.dts";
};

datablock StaticShapeData(SelectionPad) : StaticShapeDamageProfile 
        {
	className = "crate";
	shapeFile = "station_inv_mpb.dts";//"vehicle_air_hapc.dts";
};

datablock TriggerData(nukepadTrigger)
{
   tickPeriodMS = 500;
};

function SelectionPad::onDestroyed(%this, %obj, %prevState)
{
%obj.remTrigger();
Parent::onDestroyed(%this, %obj, %prevState);
%obj.schedule(500, "delete");
}

datablock ShapeBaseImageData(Supportl) {
	
	shapeFile = "stackable2L.dts";
	mountPoint = 3;
	offset = "1.7 1.5 -2";
	rotation = "1 0 0 90";
	
};

datablock ShapeBaseImageData(Supportr) {
	
	shapeFile = "stackable2L.dts";
	mountPoint = 2;
	offset = "-1.7 1.5 -2";
	rotation = "1 0 0 90";
	
};

datablock ShapeBaseImageData(SupportM1) {
	
	shapeFile = "stackable2L.dts";
	mountPoint = 3;
	offset = "0 -2.8 0";
	rotation = "1 0 0 90";
	
};

datablock ShapeBaseImageData(SupportM2) {
	
	shapeFile = "stackable2L.dts";
	mountPoint = 3;
	offset = "0 -4.8 0";
	rotation = "1 0 0 90";
	
};

datablock StaticShapeData(Baseb) : StaticShapeDamageProfile 
        {
	className = "crate";
	shapeFile = "stackable2L.dts";//"vehicle_air_hapc.dts";
	};

datablock StaticShapeData(FrameS) : StaticShapeDamageProfile 
        {
	className = "crate";
	shapeFile = "vehicle_air_scout.dts";//"vehicle_air_hapc.dts";
	};



datablock StaticShapeData(CBarrel) : StaticShapeDamageProfile 
        {
	className = "crate";
	shapeFile = "weapon_mortar.dts";//"vehicle_air_hapc.dts";

	
};

datablock StaticShapeData(MImage) : StaticShapeDamageProfile 
        {
	className = "crate";
	shapeFile = "bomb.dts";//"vehicle_air_hapc.dts";

	
};



datablock StaticShapeData(Frame) : StaticShapeDamageProfile 
        {
	className = "crate";
	shapeFile = "Vehicle_air_hapc.dts";//"vehicle_air_hapc.dts";

};


datablock StaticShapeData(BoosterM) : StaticShapeDamageProfile 
        {
	className = "crate";
	shapeFile = "Weapon_missile_projectile.dts";//"vehicle_air_hapc.dts";

	
};


datablock TurretData(BaseArmTurret) : TurretBaseLarge
{
   className      = TurretBase;
   catagory       = "Turrets";
   shapeFile      = "turret_base_large.dts";
   preload        = true;

   mass           = 1.0;  // Not really relevant

   maxDamage      = 2.25;
   destroyedLevel = 2.25;
   disabledLevel  = 1.35;
   explosion      = TurretExplosion;
	expDmgRadius = 15.0;
	expDamage = 0.66;
	expImpulse = 2000.0;
   repairRate     = 0.5;
   emap = true;

   thetaMin = 0;
   thetaMax = 180;

   isShielded           = true;
   energyPerDamagePoint = 50;
   maxEnergy = 150;
   rechargeRate = 0.31;
   humSound = SensorHumSound;
   pausePowerThread = true;

   canControl = true;
   cmdCategory = "Tactical";
   cmdIcon = CMDTurretIcon;
   cmdMiniIconName = "commander/MiniIcons/com_turretbase_grey";
   targetNameTag = 'Base';
   targetTypeTag = 'Turret';
   sensorData = TurretBaseSensorObj;
   sensorRadius = TurretBaseSensorObj.detectRadius;
   sensorColor = "0 212 45";
   heatSignature = 1.0;
   firstPersonOnly = true;

   debrisShapeName = "debris_generic.dts";
   debris = TurretDebris;
};

function GameBase::MakeTurret(%obj,%loc)
{
//TURRET
%turret = new Turret()
         {
         className = DeployedTurret;
         dataBlock = MpmTurret;
         maxdamage = 20;
         };
%turret.SetTransform(%loc);
%turret.setSelfPowered();
%turret.mountImage("MpmBarrel",0,false);
%turret.team = %obj.team;
setTargetSensorGroup(%turret.getTarget(), %obj.team);
%turret.startfade(0,0,1);
//SIGN
%sign = new StaticShape(){
dataBlock = MpmTurretTarg;
};
%sign.team = 3;
%sign.setHeat(1);
%obj.mountObject(%sign, 4);
//setTargetSensorGroup(%sign.getTarget(),3);
%obj.signature = %sign;
%sign.owner = %obj;

%turret.aimtarget = %sign;

%turret.mpm_barrel_on();
}

function MpmTurret::onRemove(%this, %obj)
{
%obj.mpm_all_off(1);
Parent::onRemove(%this, %obj);
}

function GameBase::Mpm_Turret(%obj)
{
%turret = new Turret()
         {
         className = DeployedTurret;
         dataBlock = MpmTurret;
         maxdamage = 20;
         };
%turret.SetTransform(VectorAdd(%obj.getTransform(),realvec(%obj,"0 1.5 -0.5")) SPC %obj.getRotation());
//%obj.MountObject(%turret,5);
%turret.setSelfPowered();
%turret.mountImage("MpmBarrel",0,false);
%turret.team = %obj.team;
%turret.mpb = %obj;
setTargetSensorGroup(%turret.getTarget(), %obj.team);
%turret.mpm_all_on();
%turret.playThread(1,"activate");
%turret.addToMPMGroup(1);
return %turret;
}

function MpmTurret::selectTarget(%this, %turret)
{
%turretTarg = %turret.getTarget();
  if(%turretTarg == -1)
    return;
if (Isobject(%turret.aimtarget))
	{
        if (!IsObject(%turret.aimtarget.source) && %turret.canfire)
           {	         
	   %turret.Mpm_CycleTargets("");
           return "";
           }
	%turret.setTargetObject(%turret.aimtarget);

if (!isObject(%turret.leftpad))
  return;

	%tdir = VectorNormalize(VectorSub(%turret.aimtarget.getTransform(),%turret.getMuzzlePoint(0)));
	%vec = "0 1 0";
	%rot = %turret.getSlotRotation(0);
	%dir = validateVal(MatrixMulVector("0 0 0" SPC %rot ,%vec));
	%diff = VectorDot(%tdir,%dir);
        
	if (%diff > 0.9 && IsObject(%turret.leftpad.getMountedObject(0)) && %turret.canfire)
		{
		%plyr = %turret.leftpad.getMountedObject(0);
		%tdir = VectorNormalize(VectorSub(%turret.aimtarget.targetloc,%plyr.getEYePoint()));
	   	if (!Isobject(%turret.lvistarget))
	      		{
	  		%vistar = new StaticShape() 
				{
				dataBlock = VisTarget;
				};
			%turret.Play3D(MobileBaseStationdeploySound);
	        	%turret.lvistarget=%vistar;
			}      
		%turret.lvistarget.setScale("0.01 0.01 0.01");
		%rot = fullrot(VectorScale(%tdir,-1),"0 0 1");
                
		%pos = 	VectorAdd(%plyr.getEYePoint(),VectorScale(%tdir,0.5));    
		%turret.lvistarget.setTransform(%pos SPC %rot);
		}
        else if (IsObject(%turret.lvistarget))
		{
		%turret.lvistarget.delete();
		}
	}
}


function Mpm_Beacon::onDestroyed(%this, %obj, %prevState)
{
Parent::onDestroyed(%this, %obj, %prevState);
%obj.mpm_off_all(1);
%obj.schedule(1500,"delete");
}


function MpmBarrel::onFire( %data, %obj, %slot )
{
//%obj.firempmNow();
}

function GameBase::FireMPMNow(%obj)
{
%tdir = VectorNormalize(VectorSub(%obj.aimtarget.getTransform(),%obj.getMuzzlePoint(0)));
%vec = "0 1 0";
%rot = %obj.getSlotRotation(0);
%dir = validateVal(MatrixMulVector("0 0 0" SPC %rot ,%vec));
%diff = vectorDot(%tdir,%dir);

if (%obj.canfire && IsObject(%obj.rightpad.getMountedObject(0)) && %diff > 0.9)
   {
   %data = $mpm_load[%obj.load];
   if (%data.slot == 1||%data.slot == 2)
       %obj.Lauch_mpm(%data.slot);
   else if (%data.slot == 3)
       {
       %obj.Lauch_mpm(1);
       %obj.Lauch_mpm(2);
       }
   else if (%data.slot == 4)
       {
       %slot=%obj.HasLoad(4);
       if (%slot == 3)
          %obj.Lauch_mpm(Mfloor(GetRandom()*2+1));
       else if (%slot == 2 || %slot == 1)
  	  %obj.Lauch_mpm(%slot);
       }
   else
       %obj.Lauch_mpm(0);
   }
}

function GameBase::Lauch_mpm(%obj,%slot)
{
%data = $mpm_load[%obj.load];
%offset = realvec(%obj.barrel,"0 -2 0");
if (%slot == 1)
    %barrelt = VectorAdd(%obj.rightbarrel.getWorldBoxCenter(),%offset);
else if (%slot == 2)
    %barrelt = VectorAdd(%obj.leftbarrel.getWorldBoxCenter(),%offset);
else
    %barrelt = %obj.barrel.getTransform();
%up = Realvec(%obj,"0 0 1");

%start = VectorAdd(%barrelt,VectorScale(%up,5));
%end = %obj.aimtarget.targetloc;
%forward = VectorNormalize(VectorCross(VectorCross(%up,VectorSub(%end,%start)),%up));

if (%data.offset !$= "")
    %end = VectorAdd(%end,VectorAdd(VectorScale(%forward,GetWord(%data.offset,0)),VectorScale(%up,GetWord(%data.offset,1))));


%missileblock = %data.missile;

%solution = mpm_calc(%start,%end,%up,%missileblock);
if (%solution != -1)
   {
if (%obj.aimtarget.source.player.client)
        {
	bottomPrint( %obj.aimtarget.source.player.client, "Missile Lauched at your target by:" @ %obj.launcher.namebase, 5, 3);
        %obj.aimtarget.source.firedat++;
        }
%ltime = GetWord(%solution,0)*1000;
%time = GetWord(%solution,1)*1000;
%loc = GetWord(%solution,2);
%vector = GetWords(%solution,3,5);
%a = GetWord(%solution,6);
%p = Launch_Mpm(%start,%up,"0 0 0",%ltime+200,%missileblock);
%p.team = %obj.team;
%p.source = %obj;
%p.traject = %start SPC %up SPC %loc SPC %a SPC %vector SPC %time;
%p.load = %data;
%p.targetlocation = %end;
%p.owner = %obj.launcher;
%data.schedule(%ltime,"Stage2",%p);
%data.Stage1(%p);

if (%p.owner.player.getObjectMount().base == %obj)
    obsproj(%p,%p.owner);
%obj.ammo-= %data.cost;
%dist = VectorDist(%start,%end);
%obj.fuel-= mFloor(%dist/10);
if (%obj.ammo <= 0)
    %obj.ammo = 1;
if (%obj.fuel <= 0)
    %obj.fuel = 1;
%obj.mpm_load_off(0,%slot+1);
%obj.schedule(30000,"mpm_load_on",%slot+1);
   }

}

function obsproj(%p,%player)
{
%client = %player;
if ( !isObject( %client.comCam ) )
   {
      %client.comCam = new Camera()
      {
         dataBlock = CommanderCamera;
      };
      MissionCleanup.add(%client.comCam);
      commandToClient(%client, 'ControlObjectResponse', true, getControlObjectType(%p,%client.player));
      messageClient(%colObj.client, 'CloseHud', "", 'inventoryScreen'); 
      %client.setControlObject(%client.comCam);
      commandToClient(%client, 'CameraAttachResponse', true);
    }

%client.comCam.setTransform(%p.getTransform());
%client.comCam.setOrbitMode(%p,%p.getTransform(),0,20,-20);
%client.moveprojectile = %p;

}


function GameData::Armer(%base,%target)
{
%arm = new StaticShape() 
	{
	dataBlock = TurretArm;
	};
%arm.setScale("0.1 0.1 0.1");
%arm.playThread(0,"activate");
%base.mountObject(%arm,0);
return(%arm);
}


//turret
//	1barm
//		0base
//	0arm
//		1rbase
//		0barrel
//			0missile
//			2leftjoint
//			3rightjoint
//			1frame
//				3leftarm
//					1leftbarrel
//						0leftmissile
//				4rightarm
//					1rightbarrel
//						0rightmissile
//				

function GameBase::Mpm_Barrel_on(%obj)
{
if (!IsObject(%obj.barrel))
	{
	%arm = new StaticShape() 
		{ 
		dataBlock = TurretArm;
		};	
	%arm.startFade(0,0,1);
	%arm.playThread(1,"elevate");
	%arm.stopThread(1);
	%arm.setScale("0.1 0.1 0.1");
	%obj.mountObject(%arm,0);
	
	%barrel = new StaticShape() 
		{
		dataBlock = CBarrel;
		};	
	%barrel.startFade(0,0,1);
	%arm.mountObject(%barrel,0);
	%barrel.setScale("8 8 8");
	
	%obj.barrel = %barrel;
	%barrel.arm = %arm;
	%barrel.turret = %obj;	
	}
%obj.barrel.startfade(1000,0,0);
}

function GameBase::Mpm_Barrel_off(%obj,%del)
{
%obj.remfromMpmGroup(1);
if (IsObject(%obj.barrel))
	{
	if (!%del)
        {
	%obj.barrel.startfade(1000,0,1);
        }
	else if (%del)
           {
           %obj.barrel.schedule(1000,"delete");
           %obj.barrel.arm.schedule(1000,"delete");
           }
	}

}

function GameBase::Mpm_Support_on(%obj)
{
if (!IsObject(%obj.barrel.frame))
	{
	if (%disabled)
        {
	%basearm = new StaticShape() 
		{
		dataBlock = TurretArm;
		};
	%basearm.startFade(0,0,1);
	%basearm.setScale("0.1 0.1 0.1");
	%basearm.playThread(0,"activate");
	%basearm.setThreaddir(0,false);
        %obj.mountObject(%basearm,1);

        
	%base = new StaticShape() 
		{
		dataBlock = baseb;
		};
	%base.startFade(0,0,1);
	%base.setScale("4.5 4.5 2");
        %basearm.mountObject(%base,0);
        %obj.base = %base;
	%base.arm = %basearm;

	%rbase = new StaticShape() 
		{
		dataBlock = baseb;
		};
	%rbase.startFade(0,0,1);
	%rbase.setScale("4.5 4.5 1.9");
        %obj.barrel.arm.mountObject(%rbase,1);
        %obj.rbase = %rbase;
	}

	%ghost = new StaticShape() 
		{
		dataBlock = baseb;
		};
	%ghost.startFade(0,0,1);
	%ghost.setScale("1.5 1.5 4");
        %obj.barrel.arm.mountObject(%ghost,1);
        %obj.barrel.ghost = %ghost;
	%ghost.base = %obj;

	%frame = new StaticShape() 
		{
		dataBlock = frame;
		};
	%frame.startFade(0,0,1);
	%frame.setScale("0.1 0.1 0.1");
	%obj.barrel.MountObject(%frame,1);
	%obj.barrel.frame = %frame;

	%leftarm = new StaticShape() 
		{
		dataBlock = TurretArm;
		};
	%leftarm.startFade(0,0,1);
	%leftarm.setScale("0.1 0.1 0.1");
	%leftarm.playThread(0,"activate");
	%leftarm.setThreadDir(0,false);
	%leftarm.playThread(1,"elevate");
	%frame.MountObject(%leftarm,3);
	%frame.leftarm = %leftarm;

	%rightarm = new StaticShape() 
		{
		dataBlock = TurretArm;
		};
	%rightarm.startFade(0,0,1);
	%rightarm.setScale("0.1 0.1 0.1");
	%rightarm.playThread(0,"activate");
	%rightarm.setThreadDir(0,false);
	%rightarm.playThread(1,"elevate");
	%frame.MountObject(%rightarm,4);
	%frame.rightarm = %rightarm;
	
	%leftpad = new StaticShape() 
		{
		dataBlock = SelectionPad;
		};
	//%leftpad.startFade(0,0,1);
	%leftpad.playThread(0,"deploy");
	%leftpad.setThreaddir(0,false);
	%leftpad.PlayThread(1,"Activate1");
	%leftpad.setThreadDir(1,false);

	%obj.leftpad=%leftpad;
	%leftpad.base = %obj;
	//%leftarm.MountObject(%leftpad,0);
        %leftpad.setTransform(VectorAdd(%obj.getTransform(),realvec(%obj,"-4.5 -2.5 2.2")) SPC rotAdd(%obj.getRotation(),"0 0 1" SPC 1.56));
        %leftpad.setScale("1 1 1");

	%rightpad = new StaticShape() 
		{
		dataBlock = SelectionPad;
		};
	//%rightpad.startFade(0,0,1);
	%rightpad.playThread(0,"deploy");
	%rightpad.setThreaddir(0,false);
	%obj.rightpad=%rightpad;
	%rightpad.base = %obj;
	%rightpad.PlayThread(1,"Activate1");
	%rightpad.setThreadDir(1,false);
	//%rightarm.MountObject(%rightpad,0);
        %rightpad.setTransform(VectorAdd(%obj.getTransform(),realvec(%obj,"4.5 -2.5 2.2")) SPC rotAdd(%obj.getRotation(),"0 0 -1" SPC 1.56));
        %rightpad.setScale("1 1 1");

	%leftbarrel = new StaticShape() 
		{
		dataBlock = CBarrel;
		};
	%leftbarrel.startFade(0,0,1);
	%leftbarrel.SetScale("5 4 5");
	%leftarm.MountObject(%leftbarrel,1);
	%leftarm.barrel = %leftbarrel;
	%obj.leftbarrel = %leftbarrel;
	%leftbarrel.arm = %leftarm;

	%rightbarrel = new StaticShape() 
		{
		dataBlock = CBarrel;
		};
	%rightbarrel.startFade(0,0,1);
	%rightbarrel.SetScale("5 4 5");
	%rightarm.MountObject(%rightbarrel,1);
	%rightarm.barrel = %rightbarrel;
	%obj.rightbarrel = %rightbarrel;
	%rightbarrel.arm = %rightarm;	
	}	

//%obj.base.startfade(1000,0,0);
//%obj.rbase.startfade(1000,0,0);
%obj.rightbarrel.startfade(1000,0,0);
%obj.leftbarrel.startfade(1000,0,0);
%obj.rightpad.setThreaddir(0,true);
%obj.leftpad.setThreaddir(0,true);
%obj.rightpad.addtrigger();
%obj.leftpad.addtrigger();
%obj.leftbarrel.arm.setThreadDir(0,true);
%obj.rightbarrel.arm.setThreadDir(0,true);
//%obj.base.arm.setThreaddir(0,true);
%obj.barrel.mountImage("Supportr",2,true);
%obj.barrel.mountImage("Supportl",3,true);
%obj.rightbarrel.mountImage("SupportM1",2,true);
%obj.rightbarrel.mountImage("SupportM2",3,true);
%obj.leftbarrel.mountImage("SupportM1",2,true);
%obj.leftbarrel.mountImage("SupportM2",3,true);
MPM_AddLoadArms(%obj);	
}

function GameBase::Mpm_Support_off(%obj,%del)
{

if (IsObject(%obj.barrel.frame))
	{
        MPM_RemLoadArms(%obj,%del);
        if (isObject(%obj.rightpad))
           %obj.rightpad.remtrigger();
        if (isObject(%obj.leftpad))
           %obj.leftpad.remtrigger();
	if (!%del)
        {
	//%obj.base.startfade(1000,0,1);
	//%obj.rbase.startfade(1000,0,1);
	%obj.rightbarrel.startfade(1000,0,1);
	%obj.leftbarrel.startfade(1000,0,1);
	%obj.rightpad.setThreaddir(0,false);
	%obj.leftpad.setThreaddir(0,false);
	%obj.rightpad.setThreaddir(1,false);
	%obj.leftpad.setThreaddir(1,false);
      if (%obj.leftpad.getMountNodeObject(%i)) {
         %passenger = %obj.leftpad.getMountNodeObject(%i);
         %passenger.unmount();
      }
      if (%obj.rightpad.getMountNodeObject(%i)) {
         %passenger = %obj.rightpad.getMountNodeObject(%i);
         %passenger.unmount();
      }


	%obj.leftbarrel.arm.setThreadDir(0,false);
	%obj.rightbarrel.arm.setThreadDir(0,false);

	%obj.barrel.unmountImage(2);
	%obj.barrel.unmountImage(3);
	%obj.rightbarrel.unmountImage(2);
	%obj.rightbarrel.unmountImage(3);
	%obj.leftbarrel.unmountImage(2);
	%obj.leftbarrel.unmountImage(3);
        }
	else if (%del)
           {
           //%obj.base.schedule(1000,"delete");
           //%obj.base.arm.schedule(1000,"delete");
           //%obj.rbase.schedule(1000,"delete");
           %obj.barrel.frame.schedule(1000,"delete");
	   %obj.barrel.frame.leftarm.schedule(1000,"delete");
           %obj.barrel.frame.rightarm.schedule(1000,"delete");
	   %obj.leftpad.schedule(1000,"delete");
           %obj.rightpad.schedule(1000,"delete");
       	   %obj.leftpad.remtrigger();
           %obj.rightpad.remtrigger();
	   %obj.leftbarrel.schedule(1000,"delete");
           %obj.rightbarrel.schedule(1000,"delete");   
	   %obj.barrel.ghost.schedule(1000,"delete");                  
           }
	}
}

//Litle console spam remover.
function SelectionPad::hasDismountOverrides(%data, %obj)
{
   return false;
}


function GameBase::Addtrigger(%obj)
{
if (!IsObject(%obj.trigger))
   {
   %trigger = new Trigger()
      {
      dataBlock = nukepadTrigger;
      polyhedron = "-0.75 0.75 0.1 1.5 0.0 0.0 0.0 -1.5 0.0 0.0 0.0 2.3";
      };
      MissionCleanup.add(%trigger);
      %trigger.setTransform(%obj.getTransform());

      %trigger.pad= %obj;
      %trigger.mainObj = %obj;
      %trigger.disableObj = %obj;
      %obj.trigger = %trigger;
  }
}

function GameBase::RemTrigger(%obj)
{
if (IsObject(%obj.trigger))
    %obj.trigger.delete();
}

function GameBase::Mpm_Load_on(%obj,%slot)
{
if (!IsObject(%obj.barrel.missile))
	{
       
	%missile = new StaticShape() 
		{
		dataBlock = MImage;
		};
	%missile.startFade(0,0,1);
	%missile.setScale("4 8 4");
	%obj.barrel.MountObject(%missile,0);
	%obj.barrel.missile=%missile;
		
	
	%leftmissile = new StaticShape() 
		{
		dataBlock = BoosterM;
		};
	%leftmissile.startFade(0,0,1);
	%leftmissile.setScale("10 23 10");
	%obj.leftbarrel.MountObject(%leftmissile,0);
	%obj.leftbarrel.missile=%leftmissile;
		
	%rightmissile = new StaticShape() 
		{
		dataBlock = BoosterM;
		};
	%rightmissile.startFade(0,0,1);
	%rightmissile.setScale("10 23 10");
	%obj.rightbarrel.MountObject(%rightmissile,0);
	%obj.rightbarrel.missile=%rightmissile;	

	}
if (%slot == 1 || %slot == 0)
        {
        %obj.barrel.missile.setScale("4 8 4");
	%obj.barrel.missile.startfade(1000,0,0);
        }
if (%slot == 2 || %slot == 0)
        {
	%obj.rightbarrel.missile.startfade(1000,0,0);
	%obj.rightbarrel.missile.setScale("10 23 10");
        }
if (%slot == 3 || %slot == 0)
        {
	%obj.leftbarrel.missile.startfade(1000,0,0);
	%obj.leftbarrel.missile.setScale("10 23 10");
        }
if (%slot != 0)
	%obj.missile[%slot-1] = 1;
else
        {
   	%obj.missile[0] = 1;
	%obj.missile[1] = 1;
	%obj.missile[2] = 1;
        }
}

function GameBase::Mpm_Load_off(%obj,%del,%slot)
{
if (IsObject(%obj.barrel.missile))
	{
	if (%slot == 1 || %slot == 0)
                {
		%obj.barrel.missile.startfade(0,0,1);
		%obj.barrel.missile.setScale("1 1 1");
                }
	if (%slot == 2 || %slot == 0)
                {
		%obj.rightbarrel.missile.startfade(0,0,1);
		%obj.rightbarrel.missile.setScale("1 1 1");
                }
	if (%slot == 3 || %slot == 0)
                {
		%obj.leftbarrel.missile.startfade(0,0,1);
		%obj.leftbarrel.missile.setScale("1 1 1");
                }
        if (%del)
           {
	   %obj.barrel.missile.schedule(1000,"delete");
	   %obj.rightbarrel.missile.schedule(1000,"delete");
           %obj.leftbarrel.missile.schedule(1000,"delete");
           }
	}
if (%slot != 0)
	%obj.missile[%slot-1] = 0;
else
        {
   	%obj.missile[0] = 0;
	%obj.missile[1] = 0;
	%obj.missile[2] = 0;
        }
}

function GameBase::HasLoad(%obj,%slot)
{
if (%slot < 3)
   return (%obj.missile[%slot]==1);
else if (%slot == 3)
   return (%obj.missile[1]==1 && %obj.missile[2]==1);
else if (%slot == 4)
   return (%obj.missile[1]==1) +(%obj.missile[2]==1)*2;
}

function GameBase::Mpm_All_on(%obj)
{
%obj.mpm_barrel_on();
%obj.mpm_support_on();
%obj.mpm_load_on();
}

function GameBase::Mpm_All_off(%obj,%del)
{
%obj.mpm_barrel_off(%del);
%obj.mpm_support_off(%del);
%obj.mpm_load_off(%del);
if (isObject(%obj.aimtarget))
	%obj.aimtarget.delete();
if (isObject(%obj.lvistarge))
	%obj.lvistarget.delete();
}




function nukepadTrigger::onEnterTrigger(%data, %obj, %colObj)
{
%pad = %obj.pad;
SelectionPadmount(%pad,%colObj);
}

function nukepadTrigger::onLeaveTrigger(%data, %obj, %colObj)
{
}

function nukepadTrigger::stationTriggered(%data, %obj, %isTriggered)
{
}
function nukepadTrigger::onTickTrigger(%data, %obj)
{
}



function SelectionPadmount(%obj,%col)
{
if (%col.getClassName() !$= "Player" || %obj.base.team != %col.team|| IsObject(%obj.getMountedObject(0)))
	return; 

%base = %obj.base;
%obj.mountObject(%col,0);
%obj.mVehicle=%col;
%obj.setThreadDir(1,true);

//if (%col.getMountedImage(2) && %col.getMountedImage(2).getName() $= TurretMpm_Anti_DeployableImage)
   //{
   //%obj.base.ammo+= 20;
   //%col.getMountedImage(2).used = 0;
   //%col.decInventory(TurretMpm_Anti_Deployable, 1);	
   //%col.UnMountImage(2);
   //}

if (%base.leftpad == %obj)
   {
   bottomPrint( %col.client, "Welcome to the targetting pad\n Use pack/Healthkid key to cycle targets / active scan target.\n Targets are made by deploying 3 beacons out of the pack you just got.", 10, 3);
   //if (%col.getMountedImage(2).getName() != MPM_BeaconPackImage)
   //{
   %col.setInventory(MPM_BeaconPack,1);    
   %col.getMountedImage(2).used = 0;   
   //} 
   }
if (%base.rightpad == %obj)
   {
   bottomPrint( %col.client, "Welcome to the launch pad\n Use pack/Healthkid key to lauch / cycle payload.\n Missle cost can be paid by wearing an anti-missile turret on this pad.", 10, 3);
   }
}

function PressButton(%obj,%player,%pad,%button)
{
%base = %obj.base;
%list = GetMPMTargetList(%base.team,%base.getTransform(),100,5000);
if (!%base.ammo)
     %base.ammo = 50;
if (!%base.fuel)
     %base.fuel = 20;
if (%pad == 0)
	{
	if (%button == 0)
		%base.Mpm_CycleTargets(%list,1);
	else if (%button == 1)
                { 
                if(%base.canfire != 1)
                   {
                   bottomPrint( %player.client, '\c2 [WARNING] \c0 cannot scan because no target is selected.\n Select a target', 10, 2);
                   }
                else
                   {
  		   obspulse(%base.aimtarget.targetloc,%player.client,5000);
                   pulse(%base.aimtarget.targetloc);
   	           Mpm_report_scan(%player,%base.aimtarget.targetloc);
                   %reported = 1;
                   }
                }
        if (!%reported)
	        %base.Mpm_report_targets(%player,%list);
	}
else if (%pad == 1)
        {
        if (%button == 0)
                {
                %dist = isObject(%obj.target) ? VectorDist(%obj.target.targetloc,%obj.getTransform()) : 0;
                if (%base.ammo < $mpm_load[%base.load].cost)
                   {
	           bottomPrint( %player.client, "\c2 [WARNING] \c0 cannot fire because the current payload needs more ammo.\n Bring an MPM-Ammo pack (from pack dispenser) to one of the load arms\n to get more ammo.[Needs:" SPC  $mpm_load[%base.load].cost SPC "Current:" SPC %base.ammo@"]", 10, 3);
                   }
                else if(%base.fuel < mFloor(%dist/10))
                   {
                   bottomPrint( %player.client, "\c2 [WARNING] \c0 cannot fire because the current distance requires more fuel.\n Bring an MPM-Fuel pack (from pack dispenser) to one of the load arms\n  to get more ammo.[Needs:" SPC  mFloor(%dist/10) SPC "Current:" SPC %base.fuel@"]", 10, 3);
                   }
                else if(%base.canfire != 1)
                   {
                   bottomPrint( %player.client, "\c2 [WARNING] \c0 cannot fire because no target is selected.\n Select a target on the left pad.", 10, 2);
                   }
                else if(IsObject($mpm_load[%base.load].vehicle) && !vehicleCheck($mpm_load[%base.load].vehicle, %base.team))
                   {
                   bottomPrint( %player.client, "\c2 [WARNING] \c0 cannot fire because Spawned Vehicle will exceed maximum.", 10, 1);
                   }
                else if(!%base.HasLoad($mpm_load[%base.load].slot))
                   {
                   bottomPrint( %player.client, "\c2 [WARNING] \c0 cannot fire because the current missile barrel is reloading.", 10, 1);
                   }
                else
                   {
		   %base.launcher = %player.client;
                   %base.firempmNow();
                   }
                 }
        else if (%button == 1)
                {
                %base.Mpm_CycleLoad();
                %base.Mpm_report_targets(%player,%list);
                }
        }
}

function GameBase::Mpm_CycleTargets(%obj,%list,%mod)
{
%obj.targetselection+=%mod;
if (%list $= "")
	{
	%obj.set_mpm_target();
	%obj.targetselection = 0;       	
	}
else 
	{
	%count = getWordCount(%list);
	if (%obj.targetselection > %count)
		{
		%obj.targetselection = 0;
		%obj.set_mpm_target();
		}
        else if (%obj.targetselection < 0)
                {
		%obj.targetselection = %count;
		%target = getWord(%list,%obj.targetselection-1);
		%dist = VectorDist(%target.targetloc,%obj.getTransform());
		%obj.set_mpm_target(%target);
                }
	else
		{
		%target = getWord(%list,%obj.targetselection-1);		
		%obj.set_mpm_target(%target);
		}
	}	
      
}

function GameBase::Mpm_CycleLoad(%obj)
{
%obj.load++;
if (%obj.load >= $mpm_loads || %obj.load < 0)
    %obj.load=0;
}

function GameBase::Mpm_report_targets(%obj,%plyr,%list)
{
%selection=%obj.targetselection;
if (%obj.load $= "")
    %obj.load = 0;
%load = %obj.load;
%count = getWordCount(%list);
%dist = VectorDist(%obj.aimtarget.targetloc,%obj.getTransform());
%loadname = $mpm_load[%load].name;
%loadcost = $mpm_load[%load].cost;
if (%dist > 1000)
	%distname = "(distance:" @ mFloor(%dist/100)/10 @ "km)";
else
    	%distname = "(distance:" @ mFloor(%dist/10)*10 @ "m)";

if (isObject(%obj.aimtarget.source.player.client))
     %selectionn = %selection SPC "(" @ %obj.aimtarget.source.player.client.nameBase @ "\'s Laser)";
else if (isObject(%obj.aimtarget.source) && %obj.aimtarget.source.getDatablock().getName() $= "MpmTurret")
     {
     if (%obj.aimtarget.source.mpb.team != %plyr.team)
         %selectionn = %selection SPC "(Enemy MPB)";
     else
         %selectionn = %selection SPC "(Friendly MPB)";
     }
else
     %selectionn = %selection SPC "(" @ %obj.aimtarget.source.ownername @ "\'s Target)";

if (%count == 0)
   %target = "No targetable targets found";	
else
   if (%selection == 0)
       %target = "None /" SPC %count;
   else
	%target = %selectionn SPC %distname SPC "/" SPC %count;

bottomPrint( %plyr.client, "Target:"SPC %target SPC"\n Payload:"SPC %loadname SPC"\n Payload cost/current => ammo:"SPC %loadcost SPC "/" SPC %obj.ammo SPC "fuel:" SPC mFloor(%dist/10) SPC "/" SPC %obj.fuel, 10, 3);
}

function Mpm_report_scan(%plyr,%pos)
{
%result = pulsescan(%pos,%plyr.team,50);
%line1 = "           [Friendly]/[Enemy]\n";
%line2 = "players :  ["@ GetWord(%result,0) @"]/["@ GetWord(%result,1) @"]\n";
%line3 = "vehicles:  ["@ GetWord(%result,2) @"]/["@ GetWord(%result,3) @"]\n";
bottomPrint( %plyr.client,%line1 @ %line2 @ %line3 , 10, 3);
}

function SelectionPad::playerDismounted(%data, %obj, %player)
{
%obj.setThreadDir(1,false);
}

function GameBase::set_mpm_target(%obj,%target)
{
if (!Isobject(%obj.aimtarget))
   {
   //SIGN
   %sign = new StaticShape(){
   dataBlock = MpmTurretTarg;
   };
   %sign.team = 3;
   %sign.setHeat(1);
   //setTargetSensorGroup(%sign.getTarget(),3);
   %sign.owner = %obj;
   %obj.aimtarget = %sign;
   }
if (!Isobject(%target))
   {
   %pos = VectorAdd(%obj.getMuzzlePoint(0),realvec(%obj,"0 3 0")); //Changed target offset to 3 
   %obj.aimtarget.setTransform(%pos SPC "1 0 0 0");
   %obj.aimtarget.targetloc = %pos;
   %obj.canfire = 0;
   }
else
   {
   %pos = %target.targetloc ? %target.targetloc : %target.getTransform();
   %baseloc = %obj.getMuzzlePoint(0);
   %offset = VectorScale(VectorNormalize(VectorSub(%pos,%baseloc)),3);
   %up = realvec(%obj,"0 0 1");
   %offset = VectorScale(VectorNormalize(VectorCross(VectorCross(%up,%offset),%up)),3);
   %obj.aimtarget.setTransform(VectorAdd(%baseloc,%offset) SPC "1 0 0 0");
   %obj.aimtarget.source=%target;
   %obj.aimtarget.targetloc = %pos;
   %obj.canfire = 1;
   }
%obj.Play3D(MobileBaseStationUndeploySound);
}

////////////////////////////////LOADING STUFF///////////////////////////

datablock ShapeBaseImageData(MpmFuelPackImage) {
	mass = 20;
	emap = true;
	shapeFile = "stackable4l.dts";
	item = MpmFuelPack;
	mountPoint = 1;
	offset = "0 -0.18 -0.5";
	
	heatSignature = 0;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = true;
	maxDepSlope = 360;
	deploySound = ItemPickupSound;

	minDeployDis = 0.5;
	maxDeployDis = 50.0;
};

datablock ShapeBaseImageData(MpmFuelPackImage1):MpmFuelPackImage
{
offset = "0 -0.75 0.25";
};

datablock ItemData(MpmFuelPack) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable4l.dts";
	mass = 5.0;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "MpmFuelPackImage";
	pickUpName = "a Missile Fuel pack";
	heatSignature = 0;
	emap = true;
 };

datablock ShapeBaseImageData(MpmAmmoPackImage) {
	mass = 20;
	emap = true;
	shapeFile = "bomb.dts";
	item = MpmAmmoPack;
	mountPoint = 1;
	offset = "0 -0.18 -0.5";
	rotation = "1 0 0 90";
	heatSignature = 0;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = true;
	maxDepSlope = 360;
	deploySound = ItemPickupSound;

	minDeployDis = 0.5;
	maxDeployDis = 50.0;
};

datablock ShapeBaseImageData(MpmAmmoPackImage1):MpmAmmoPackImage
{
shapeFile = "bomb.dts";
offset = "0 -0.75 1";
rotation = "1 0 0 90";
};


datablock ItemData(MpmAmmoPack) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "bomb.dts";
	mass = 5.0;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "MpmAmmoPackImage";
	pickUpName = "a Missile Ammo pack";
	heatSignature = 0;
	emap = true;
 };

function MpmAmmoPack::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

function MpmAmmoPackImage::testNoSurfaceInRange(%item, %plyr)
{
   return (!Deployables::searchView(%plyr, $MaxDeployDistance, $TypeMasks::StaticShapeObjectType)) || (!%item.surface.isLoadClamp || %item.surface.loading);
}

function MpmAmmoPackImage::onDeploy(%item, %plyr, %slot)
{	
MPM_LoadArmin(%item.surface);
%item.surface.mountImage(MpmAmmoPackImage1,0);	
%item.surface.base.ammo += 10;
%plyr.decInventory(MpmAmmoPack, 1);	
%plyr.UnMountImage(2);
}

function MpmFuelPack::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}


function MpmFuelPackImage::testNoSurfaceInRange(%item, %plyr)
{
   return (!Deployables::searchView(%plyr, $MaxDeployDistance, $TypeMasks::StaticShapeObjectType)) || (!%item.surface.isLoadClamp || %item.surface.loading);
}

function MpmFuelPackImage::onDeploy(%item, %plyr, %slot) 
{
MPM_LoadArmin(%item.surface);
%item.surface.mountImage(MpmFuelPackImage1,0);	
%item.surface.base.fuel += 10;
%plyr.decInventory(MpmFuelPack, 1);	
%plyr.UnMountImage(2);
}

function MPM_AddLoadArms(%obj)
{
///baseb
if (!isobject(%obj.loadarm1))
   {
   %rightarm = new StaticShape() 
		{
		dataBlock = TurretLift;
                scale = "0.25 0.5 0.8";
		};
	%rightarm.playThread(0,"activate");
	%rightarm.playThread(1,"deploy");
        %rightarm.playThread(2,"Elevate");


    %rightClamp = new StaticShape() 
		{
		dataBlock = baseb;
                scale = "0.5 0.5 1";
		};
         %rightclamp.isLoadClamp = 1;
         %rightclamp.arm = %rightArm;
         %rightClamp.base = %obj;
         %rightarm.clamp = %rightClamp;
         %rightarm.MountObject(%rightClamp,0);
            
         %pos = %obj.getTransform();
         %rot = rotadd(rotadd(rotadd(GetWords(%pos,3,6),"0 1 0" SPC $pi/2),"0 0 1" SPC $pi/2),"1 0 0" SPC -1*$pi/4);
         %offset = RealVec(%obj,"-2.0 0.70 1.5");
         //%offset = RealVec(%obj,"-10 0 1");
         %rightarm.setTransform(VectorAdd(%pos,%offset) SPC %rot);
         %obj.loadarm1 = %rightarm;
   }
if (!isobject(%obj.loadarm2))
   {
   %leftarm = new StaticShape() 
		{
		dataBlock = TurretLift;
                scale = "0.25 0.5 0.8";
		};
	%leftarm.playThread(0,"activate");
	%leftarm.playThread(1,"deploy");
        %leftarm.playThread(2,"Elevate");


    %leftClamp = new StaticShape() 
		{
		dataBlock = baseb;
                scale = "0.5 0.5 1";
		};
         %leftclamp.isLoadClamp = 1;
         %leftclamp.arm = %leftArm;
         %leftClamp.base = %obj;
         %leftarm.clamp = %leftClamp;
         %leftarm.MountObject(%leftClamp,0);
            
         %pos = %obj.getTransform();
         %rot = rotadd(rotadd(rotadd(GetWords(%pos,3,6),"0 1 0" SPC -1*$pi/2),"0 0 1" SPC -1*$pi/2),"1 0 0" SPC -1*$pi/4);
         %offset = RealVec(%obj,"2.0 0.70 1.5");
         //%offset = RealVec(%obj,"-10 0 1");
         %leftarm.setTransform(VectorAdd(%pos,%offset) SPC %rot);
         %obj.loadarm2 = %leftarm;
   }
MPM_LoadArmOut(%obj.loadarm1.clamp);
MPM_LoadArmOut(%obj.loadarm2.clamp);
}

function MPM_LoadArmin(%clamp)
{
//SensorDeploySound
//MotionSensorDeploySound
//StationDeploySound
%clamp.play3d(TurretDeploySound);
%clamp.loading = 1;
//%clamp.arm.setThreaddir(0,true);
%clamp.arm.schedule(800,"setThreaddir",0,true);
%clamp.arm.setThreaddir(1,false);
//%clamp.arm.setThreaddir(2,false);
%clamp.arm.schedule(800,"setThreaddir",2,false);
schedule(2000,0,"MPM_LoadArmOut",%clamp);
}

function MPM_LoadArmOut(%clamp)
{
%clamp.play3d(StationDeploySound);
%clamp.unmountImage(0);
%clamp.arm.schedule(1000,"setThreaddir",0,false);
%clamp.schedule(1000,"play3d",TurretDeploySound);
%clamp.arm.setThreaddir(1,true);
%clamp.arm.schedule(1000,"setThreaddir",2,true);

schedule(2000,%clamp,"MPM_LoadArmFinish",%clamp);
}

function MPM_LoadArmFinish(%clamp)
{
%clamp.loading = "";
}


function MPM_RemLoadArms(%obj,%del)
{
if (isObject(%obj.loadarm1))
      {
      if (%del)
         {
         %obj.loadarm1.clamp.delete();
         %obj.loadarm1.delete();
	 %obj.loadarm2.clamp.delete();
         %obj.loadarm2.delete();
         }
      else
         { 
         MPM_LoadArmin(%obj.loadarm1.clamp);
	 MPM_LoadArmin(%obj.loadarm2.clamp);
         }
      }
}

/////////////////Not used.////////////////////

function BaseArmTurret::selectTarget(%this, %turret)
{
   %turretTarg = %turret.getTarget();
   if(%turretTarg == -1)
      return;

	if ($Host::Purebuild == 1 && $TurretEnableOverride != 1) {
		%turret.clearTarget();
		return;
	}

   // if the turret isn't on a team, don't fire at anyone
   if(getTargetSensorGroup(%turretTarg) == 0)
   {
      %turret.clearTarget();
      return;
   }

   // stop firing if turret is disabled or if it needs power and isn't powered
   if((!%turret.isPowered()) && (!%turret.needsNoPower))
   {
      %turret.clearTarget();
      return;
   }

   %TargetSearchMask = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType |
   $TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType; //$TypeMasks::StaticObjectType;

   InitContainerRadiusSearch(%turret.getMuzzlePoint(0),
                             %turret.getMountedImage(0).attackRadius,
                             %TargetSearchMask);

	while ((%potentialTarget = ContainerSearchNext()) != 0) {
		if (%potentialtarget) {
			%potTargTarg = %potentialTarget.getTarget();
			if (%turret.isValidTarget(%potentialTarget)
			&& (getTargetSensorGroup(%turretTarg) != getTargetSensorGroup(%potTargTarg))
			&& (getTargetSensorGroup(%potTargTarg) != 0)) {
				%turret.setTargetObject(%potentialTarget);
				return;
			}
		}
	}
}

///////////////////////////BEACON STUFF///////////////////////////

datablock ShapeBaseImageData(MPM_BeaconPackImage)
{
   shapeFile = "turret_belly_base.dts";
   item = MPM_BeaconPack;
   mountPoint = 1;
   offset = "0 0 0";
   rotation = "1 0 0 90";
   emap = true;
   deployed = Mpm_Beacon;

   stateName[0] = "Idle";
   stateTransitionOnTriggerDown[0] = "Activate";

   stateName[1] = "Activate";
   stateScript[1] = "onActivate";
   stateTransitionOnTriggerUp[1] = "Idle";

   isLarge = false;
   maxDepSlope = 360;
   deploySound = ItemPickupSound;

   minDeployDis = 0.5;
   maxDeployDis = 50.0;
};

datablock ItemData(MPM_BeaconPack)
{
   className = Pack;
   catagory = "Deployables";
   shapeFile = "turret_belly_base.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   rotate = true;
   image = "MPM_BeaconPackImage";
	pickUpName = "an beacon pack";

   computeCRC = true;

};

datablock StaticShapeData(Mpm_Beacon_Ghost) : StaticShapeDamageProfile {
			     
	shapeFile = "stackable4m.dts";
	maxDamage      = 0.5;
	destroyedLevel = 0.5;
	disabledLevel  = 0.3;

};



function MPM_BeaconPack::onPickup(%this, %obj, %shape, %amount)
{
	// created to prevent console errors
}

function MPM_BeaconPackImage::onDeploy(%item, %plyr, %slot) {	

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");
	%item.surfaceNrm2 = %playerVector;
	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);
        %rot = rotAdd(%rot,"1 0 0 3.14");
	%beacon = new StaticShape() 
		{
		dataBlock = Mpm_Beacon;
 		};
        %ghost = new StaticShape() 
		{
		dataBlock = Mpm_Beacon_Ghost;
                scale = "1 1 0.2";
		};
        %beacon.ghost = %ghost;
        %ghost.base = %beacon;
        
        %beacon.setTransform(%item.surfacePt SPC %rot);
        %ghost.setTransform(%item.surfacePt SPC %rot);

        %beacon.team = %plyr.client.team;
        %beacon.setOwner(%plyr);
        %beacon.ownername = %plyr.client.namebase;
        %ghost.team = %plyr.client.team;
        %ghost.setOwner(%plyr);
 
        %beacon.addtoMPMGroup(2);
        Mpm_Link(%beacon);


	// play the deploy sound
	serverPlay3D(%item.deploySound, %beacon.getTransform());

	addDSurface(%item.surface,%deplObj);

        %plyr.getMountedImage(2).used++;
        if (%plyr.getMountedImage(2).used>2)
           {
           %plyr.getMountedImage(2).used = 0;
           %plyr.decInventory(MPM_BeaconPack, 1);	
           %plyr.UnMountImage(2);
           }
	return %beacon;
}

function MPM_BeaconPackImage::testObjectTooClose(%item,%surfacePt,%plyr) {
return (GetWord(%plyr.closestmpmbeacon(0),1)<25 && %plyr.closestmpmbeacon(0));
}

function Mpm_Beacon_Ghost::disassemble(%data,%plyr,%obj) {
%base = %obj.base;
%base.destroyed=1;
if (IsObject(%base.link1))
	Mpm_Link(%base.link1,%obj);
if (IsObject(%base.link2))
	Mpm_Link(%base.link2,%obj);
Parent::onDestroyed(%this, %base, %prevState);
if (IsObject(%base.emitter))
%base.emitter.schedule(500,"delete");

%base.schedule(500, "delete");

parent::disassemble(%data,%plyr,%obj);
}

function GameBase::putpack(%obj)
{
%i = new Item() 
		{
		dataBlock = Mpm_BeaconPack;
		};
%i.setTransform(%obj.getTransform());
}

datablock StaticShapeData(Mpm_Beacon) : StaticShapeDamageProfile {
	className = "targeting";		     
	shapeFile = "turret_belly_base.dts";

	maxDamage      = 0.5;
	destroyedLevel = 0.5;
	disabledLevel  = 0.3;

	explosion      = HandGrenadeExplosion;
	expDmgRadius   = 1.0;
	expDamage      = 0.05;
	expImpulse     = 200;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSensorIcon;
	cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";
	targetNameTag = 'Deployed mpm Beacon';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
};

datablock ParticleData(MpmGreenParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 1.0;
   
   lifetimeMS           = 800;
   lifetimeVarianceMS   = 0;

   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0;
   textureName          = "flarebase";
   colors[0]     = "0.2 1 0.2 0";
   colors[1]     = "0.2 1 0.2 1";
   colors[2]     = "0.2 1 0.2 1";
   colors[3]     = "0.2 1 0.2 0";

   sizes[0]      = 1;
   sizes[1]      = 0.75;
   sizes[2]      = 0.5;
   sizes[3]      = 0.25;

   times[0]      = 0.0;
   times[1]      = 0.25;
   times[2]      = 0.5;
   times[3]      = 1;


};

datablock ParticleData(MpmRedParticle):MpmGreenParticle
{
 textureName          = "flarebase";
 colors[0]     = "1 0.2 0 0";
 colors[1]     = "1 0.2 0 1";
 colors[2]     = "1 0.2 0 1";
 colors[3]     = "1 0.2 0 0";

   sizes[0]      = 1;
   sizes[1]      = 0.75;
   sizes[2]      = 0.5;
   sizes[3]      = 0.25;

   times[0]      = 0.0;
   times[1]      = 0.25;
   times[2]      = 0.5;
   times[3]      = 1;
};

datablock ParticleEmitterData(MpmGreenEmitter)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;

   ejectionVelocity = 0.5;
   velocityVariance = 0.0;
   ejectionoffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 0.0;
	

   orientParticles  = false;
   orientOnVelocity = false;

   particles = "MpmGreenParticle";
};

datablock ParticleEmitterData(MpmRedEmitter):MpmGreenEmitter
{
particles = "MpmRedParticle";
ejectionoffset = 0;
};

function GameBase::Mpm_Beacon_Add(%obj)
{
%pos = GetWords(%obj.getTransform(),0,2);
%rot = "1 0 0 3.14";
%beacon = new StaticShape() 
		{
		dataBlock = Mpm_Beacon;
		};
%beacon.setTransform(%pos SPC %rot);
%beacon.team = %obj.client.team;
%beacon.setOwner(%obj);

%beacon.addtoMPMGroup(2);
Mpm_Link(%beacon);
}

function Mpm_Beacon::onDestroyed(%this, %obj, %prevState)
{
%obj.destroyed=1;
if (IsObject(%obj.link1))
	Mpm_Link(%obj.link1,%obj);
if (IsObject(%obj.link2))
	Mpm_Link(%obj.link2,%obj);
Parent::onDestroyed(%this, %obj, %prevState);
if (IsObject(%obj.emitter))
%obj.emitter.schedule(500,"delete");
if (IsObject(%obj.ghost))
%obj.ghost.schedule(500,"delete");
%obj.schedule(500, "delete");
}


function Mpm_Link(%obj,%exclude)
{
if (!isObject(%obj) || Mpm_checkLink(%obj) || %obj.destroyed)
   return "";

%pos = %obj.getTransform();
%maxrange = 200; //max link range boosted from 100 to 200
%mask = $TypeMasks::StaticShapeObjectType;

InitContainerRadiusSearch(%pos,%maxrange,%mask);
%amount=0;
 while ((%target = ContainerSearchNext()) != 0)
   {
   if (mpm_beacons.isMember(%target) && !%target.destroyed)
      {
      %mpmlist[%amount] = %target;
      %amount++;
      }
   }


for (%i=0;%i<%amount;%i++)
	{
	%link1 = %mpmlist[%i];
        %pos1= %link1.getTransform();        
	if (isObject(%link1) && %link1 != %obj && %link1 != %exclude && !%link1.linked && VectorDist(%pos,%pos1) < %maxrange)
           {
           for (%j=%i+1;%i<%amount;%i++)
	   	{
		%link2 = %mpmlist[%i];
        	%pos2= %link2.getTransform();        
		if (isObject(%link2) && %link2 != %obj && %link2 != %exclude && %link2 != %link1 && !%link2.linked && VectorDist(%pos,%pos2) < %maxrange && VectorDist(%pos1,%pos2) < %maxrange)
        		{      
		        Mpm_linkup(%obj,%link1,%link2);
 			}
           	}
	    } 
        }
Mpm_checkLink(%obj);
}

function Mpm_linkup(%b1,%b2,%b3)
{
%b1.link1 = %b2;
%b1.link2 = %b3;
%b1.linked = 1;
Mpm_checkLink(%b1);
%b2.link1 = %b1;
%b2.link2 = %b3;
%b2.linked = 1;
Mpm_checkLink(%b2);
%b3.link1 = %b1;
%b3.link2 = %b2;
%b3.linked = 1;
Mpm_checkLink(%b3);
}

function Mpm_nolink(%obj)
{
return !isObject(%obj) || %obj.destroyed;
}

function Mpm_checkLink(%obj)
{
if (IsObject(%obj.emitter))
	%obj.emitter.delete();
if (!%obj.linked || Mpm_nolink(%obj.link1) || Mpm_nolink(%obj.link2) )
   {
   %obj.linked = 0;
   %obj.link1 = 0;
   %obj.link2 = 0;
   %obj.emitter=CreateEmitter(VectorAdd(%obj.getTransform(),realvec(%obj,"0 0 -1")),MpmRedEmitter,"1 0 0 0"); 
   return "";
   }
else
   {
   %pos = %obj.getTransform();
   %pos1 = %obj.link1.getTransform();
   %pos2 = %obj.link2.getTransform();
   %obj.targetloc = VectorScale(VectorAdd(VectorAdd(%pos1,%pos2),%pos),1/3);
   %nrm = VectorNormalize(VectorSub(%obj.targetloc,%pos));
   %rot = fullrot(%nrm,"0 0 1");
   %obj.emitter=CreateEmitter(VectorAdd(%pos,realvec(%obj,"0 0 -1")),MpmGreenEmitter,%rot);   
   return 1;
   }
}



function GameBase::checkMpmLink(%obj,%exclude)
{
%location = %obj.getTransform();
%range = 200; //max link range boosted from 100 to 200
%mask = $TypeMasks::StaticShapeObjectType;
%done = 0;
InitContainerRadiusSearch(%location,%range,%mask);

 while ((%target = ContainerSearchNext()) != 0 && %done != 2)
   {
   if (mpm_beacons.isMember(%target))
      {
      if (%target != %obj && %target != %exclude)
         {
         if (!isObject(%obj.link1) || %obj.link1 == %exclude) //Do we have an link1
            {
            %obj.link1 = 0;   //Totally remove it.
            if (%obj.link2 != %target) //We don't already have it right?
	        if (%target.askMpmLink(%obj,%exclude)) //Can we have you?
		    {
		    %obj.MpmLink(%target,1,%exclude); //Take
                    %done++;
                    }
            }
         if (!isObject(%obj.link2) || %obj.link2 == %exclude)
            {
            %obj.link2 = 0;
            if (%obj.link1 != %target)
	        if (%target.askMpmLink(%obj,%exclude))
                    {
        	    %obj.MpmLink(%target,2,%exclude);                           
		    %done++;
                    }            
            }
         }
      }
   }
if (%done == 0)
   %obj.MpmLink();    
}


function GameBase::AskMpmLink(%target,%sender,%override)
{
if (%target.link1 == %sender || %target.link2 == %sender)
   return 1;
if (!IsObject(%target.link1) || %target.link1 == %override)
   {
   %target.MpmLink(%sender,1,%override);   
   return 1;
   }
if (!IsObject(%target.link2) || %target.link2 == %override)
   {
   %target.MpmLink(%sender,2,%override);   
   return 1;
   }
return 0;
}

function GameBase::MpmLink(%obj,%link,%part,%exclude)
{
if (IsObject(%obj.emitter))
	%obj.emitter.delete();
%location = %obj.getTransform();
%linkedto=0;
if (%part == 1)
   %obj.link1=%link;
else if (%part == 2)
   %obj.link2=%link;
if (isObject(%obj.link1) && %obj.link1 != %exclude)
   %linkedto++;
if (isObject(%obj.link2) && %obj.link2 != %exclude)
   %linkedto++;
%obj.linkedto = %linkedto;

if (!%obj.destroyed)
   {
if (%linkedto==2)
   {
   %pos1 = %obj.link1.getTransform();
   %pos2 = %obj.link2.getTransform();
   %obj.targetloc = VectorScale(VectorAdd(VectorAdd(%pos1,%pos2),%location),1/3);
   %nrm = VectorSub(%obj.targetloc,%location);
   %rot = fullrot(%nrm,"0 0 1");
   %obj.emitter=CreateEmitter(VectorAdd(%location,realvec(%obj,"0 0 -1")),MpmGreenEmitter,%rot);   
   }
else
   {
   %obj.emitter=CreateEmitter(VectorAdd(%location,realvec(%obj,"0 0 -1")),MpmRedEmitter,"1 0 0 0"); 
   }
   }
}

function GetMPMTargetList(%team,%pos,%min,%max)
{
if (!isObject(MpmGroup))
   return "";
for( %c = 0; %c < mpm_beacons.getCount(); %c++ ) 
	{
	%beacon = mpm_beacons.getObject(%c);
	if (%beacon.team == %team || $mpm::targetenemies) 
            {           
            if (findWord(%targetlist,%beacon.link1 SPC %beacon.link2) $= "" && %beacon.linked) //Only want beacons that are linked
                {
                %dist = VectorDist(%beacon.targetloc,%pos);
                if (%dist > %min && %dist < %max) // Hey not too close... too far is bad too
	                %targetlist = listAdd(%targetlist,%beacon);
                }
            else if (%beacon.end == %beacon.targetloc && %beacon.islaser) //What have we here?.. a laser target perhaps?
                {
                %dist = VectorDist(%beacon.targetloc,%pos);
                if (%dist > %min && %dist < %max)
			 %targetlist = listAdd(%targetlist,%beacon);
                }
            
            }
        }
if ($mpm::targetmpbs) //Nifty lil feature requested by peeps.
   for( %c = 0; %c < mpm_turrets.getCount(); %c++ ) 
	{
	%beacon = mpm_turrets.getObject(%c);
	if (%turret.team != %team || $teamDamage)
            {           
                %dist = VectorDist(%beacon.getTransform(),%pos); //Need transform for location :D
                if (%dist > %min && %dist < %max)
	                %targetlist = listAdd(%targetlist,%beacon);
            }
        }

return %targetlist;
}

function Gamebase::addToMPMGroup(%obj,%set)
{
   if (!isObject(MpmGroup))
      {
      %main = new SimGroup("MpmGroup");
      MissionCleanup.add(%main);
      %miss = new Simgroup("mpm_Missiles");
      %turrets = new Simgroup("mpm_turrets");
      %beacons = new Simgroup("mpm_beacons");
      %main.add(%miss);
      %main.add(%turrets);
      %main.add(%beacons);
      }
if (IsObject(%obj))
   {
   if (%set == 1)
      Mpm_Turrets.add(%obj);
   else if (%set == 2)
      mpm_beacons.add(%obj);
   else
      mpm_Missiles.add(%obj);
   }
}

function GameBase::remfromMpmGroup(%obj,%set)
{
if (!isObject(MpmGroup))
      {
      %main = new SimGroup("MpmGroup");
      MissionCleanup.add(%main);
      %miss = new Simgroup("mpm_Missiles");
      %turrets = new Simgroup("mpm_turrets");
      %beacons = new Simgroup("mpm_beacons");
      %main.add(%miss);
      %main.add(%turrets);
      %main.add(%beacons);
      }
if (IsObject(%obj))
   {
   if (%set == 1 && Mpm_turrets.IsMember(%obj))
      Mpm_turrets.remove(%obj);
   else if (%set == 2 && Mpm_beacons.IsMember(%obj))
      Mpm_beacons.remove(%obj);
   else if (Mpm_Missiles.IsMember(%obj))
      Mpm_Missiles.remove(%obj);
   }
}

function GameBase::closestmpmbeacon(%obj,%exclude)
{
%location = %obj.getTransform();
if (!mpm_beacons.getCount())
		return "";

%tbeacon = "";
for( %c = 0; %c < mpm_beacons.getCount(); %c++ ) 
	{
	%beacon = mpm_beacons.getObject(%c);
	if (%beacon != %obj && %beacon != %exclude && %beacon.team == %obj.team)
		{		
		%pos = pos(%beacon);
		%dist = vectorDist(%location,%pos);
		if (!%dis || %dist < %dis) 
			{
			%tbeacon = %beacon;
			%dis = %dist;
			}
		}
	}
return %tbeacon SPC %dist;
}

function TargetingLaserImage::Onfire(%data,%obj,%slot) {
%p = Parent::onFire(%data, %obj, %slot);	
%p.setTarget(%obj.team);
if (%obj.getMountedImage(2) && %obj.getMountedImage(2).getName() $= MPM_BeaconPackImage)
   {
   bottomPrint( %obj.client, "Keep aiming the targetlaser at the same spot\n to maintain a temperary Missile target", 10, 2);
   %obj.laser = %p;
   %p.player = %obj;
   %end =%obj.TraceLaser();
   if (%end != -1)
       %obj.laser.end=%end;
   %obj.schedule(5000,"ValidateLaser");
   }
}

function GameBase::TraceLaser(%obj)
{
%mask = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType |  $TypeMasks::VehicleObjectType |$TypeMasks::StaticShapeObjectType;
%start = %obj.getMuzzlePoint(0);
%end = VectorAdd(%start,VectorScale(%obj.getMuzzleVector(0),3000));
%res = ContainerRayCast(%start,%end,%mask, %obj);
if (%res)
   {
   return GetWords(%res,1,3);
   }
else
   return -1;
}

function GameBase::ValidateLaser(%obj)
{
if (IsObject(%obj.laser))
   {
   %end = %obj.traceLaser();
   if (%end != %obj.laser.end || %obj.laser.end==-1)
      {
      %obj.laser.remfromMpmGroup(2);
      %obj.laser.end=%end;
      %obj.laser.firedat =0;
      }
   else
      {
      %obj.laser.islaser = 1;
      %obj.laser.addtoMpmGroup(2);
      %obj.play3d(LaserTargetSound);
      if (%obj.laser.firedat)
         %text = "Missiles lauched at your laser target:"@%obj.laser.firedat;
      else
         %text = "Noting lauched so far.";
      bottomPrint( %obj.client, "Laser stable, temporary target maintained.\n Keep the laser at it\'s current target, to maintain the target.\n" SPC %text , 3, 3);
      
      %obj.laser.targetloc = %obj.laser.end;
      %obj.laser.team = %obj.team;
      }
   %obj.schedule(3000,"ValidateLaser");     
   }
}

datablock ShockwaveData(PulseWave) {
	className = "ShockwaveData";
	scale = "1 1 1";
	delayMS = "0";
	delayVariance = "0";
	lifetimeMS = "1000";
	lifetimeVariance = "0";
	width = "1";
	numSegments = "60";
	numVertSegments = "1";
	velocity = "30";
	height = "20";
	verticalCurve = "5";
	acceleration = "5";
	times[0] = "0";
	times[1] = "0.25";
	times[2] = "0.75";
	times[3] = "1";
	colors[0] = "1.000000 0.000000 0.000000 1.000000"; //1.0 0.9 0.9
	colors[1] = "1.000000 0.000000 0.000000 1.000000"; //0.6 0.6 0.6
	colors[2] = "1.000000 0.000000 0.000000 1.000000"; //0.6 0.6 0.6
	colors[3] = "1.000000 0.000000 0.000000 0.000000";
	texture[0] = "gamegrid";
	texture[1] = "gamegrid";
	texWrap = "7";
	is2D = "1";
	mapToTerrain = "0";
	orientToNormal = "1";
	renderBottom = "1";
	renderSquare = "0";
};

datablock ExplosionData(PulseExplosion):BaseExplosion //From blast.cs
          {
     	  Shockwave = "PulseWave";
          };


datablock TracerProjectileData(PulseProjectile):BaseProjectile
        {
	Explosion = "PulseExplosion";
        };

function pulse(%pos)
{
schedule(200,0,"Serverplay3D",FlashGrenadeExplosionSound,%pos);
for (%i=0;%i<5;%i++)
        {
        
	schedule(%i*200,0,"shockwave",%pos,"0 0 1",PulseProjectile);
	}
}

function obspulse(%pos,%client,%time)
{
if ( !isObject( %client.comCam ) )
   {
      %client.comCam = new Camera()
      {
         dataBlock = CommanderCamera;
      };
      MissionCleanup.add(%client.comCam);            
    }   
%sign = new StaticShape(){
dataBlock = MpmTurretTarg;
};
%sign.setTransform(VectorAdd(%pos,"0 0 5") SPC "1 0 0 -1");
%client.comCam.setTransform(VectorAdd(%pos,"0 0 50"));
%client.comCam.setOrbitMode(%sign,%pos,-10,50,-10);
%client.setControlObject(%client.comCam);
commandToClient(%client, 'CameraAttachResponse', true);
%client.schedule(%time,"setControlObject",%client.player);
commandToClient(%client, 'ControlObjectResponse', true, getControlObjectType(%client.comcam,%client.player));
messageClient(%client, 'CloseHud', "", 'inventoryScreen');    
schedule(%time,%client,"serverCmdResetControlObject",%client);
%sign.schedule(%time+100,"delete");
}

function pulsescan(%pos,%team,%range)
{
%mask = $TypeMasks::PlayerObjectType |$TypeMasks::VehicleObjectType ;
%done = 0;
InitContainerRadiusSearch(%pos,%range,%mask);
%epl = 0;
%fpl = 0;
%evh = 0;
%fvh = 0;
 while ((%target = ContainerSearchNext()) != 0)
   {
   if (%target.getType() & $TypeMasks::PlayerObjectType)
      {
      if (%target.team != %team)
          %epl++;
      else
          %fpl++;
      }
   else if(%target.getType() & $TypeMasks::VehicleObjectType)
      {
      if (%target.team != %team)
          %evh++;
      else
          %fvh++;
      }
   }
return %fpl SPC %epl SPC %fvh SPC %evh;
}
