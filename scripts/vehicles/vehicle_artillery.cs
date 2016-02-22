//**************************************************************
// Long Range Artillery
//**************************************************************
//**************************************************************
// SOUNDS
//**************************************************************

datablock AudioProfile(ArtillerySkid)
{
   filename    = "fx/vehicles/tank_skid.wav";
   description = ClosestLooping3d;
   preload = true;
};

datablock AudioProfile(ArtilleryEngineSound)
{
   filename    = "fx/vehicles/tank_engine.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};

datablock AudioProfile(ArtilleryThrustSound)
{
   filename    = "fx/vehicles/tank_boost.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};

datablock AudioProfile(ArtilleryChaingunFireSound)
{
   filename    = "fx/vehicles/tank_chaingun.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};

datablock AudioProfile(ArtilleryChaingunReloadSound)
{
   filename    = "fx/weapons/chaingun_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(TankChaingunProjectile)
{
   filename    = "fx/weapons/chaingun_projectile.wav";
   description = ProjectileLooping3d;
   preload = true;
};

datablock AudioProfile(ArtilleryTurretActivateSound)
{
    filename    = "fx/vehicles/tank_activate.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ArtilleryChaingunDryFireSound)
{
   filename    = "fx/weapons/chaingun_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ArtilleryChaingunIdleSound)
{
   filename    = "fx/misc/diagnostic_on.wav";
   description = ClosestLooping3d;
   preload = true;
};

datablock AudioProfile(ArtilleryMortarDryFireSound)
{
   filename    = "fx/weapons/mortar_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ArtilleryMortarFireSound)
{
   filename    = "fx/vehicles/tank_mortar_fire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ArtilleryMortarReloadSound)
{
   filename    = "fx/weapons/mortar_reload.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ArtilleryMortarIdleSound)
{
   filename    = "fx/misc/diagnostic_on.wav";
   description = ClosestLooping3d;
   preload = true;
};

//**************************************************************
// LIGHTS
//**************************************************************
datablock RunningLightData(TankLight1)
{
   radius = 1.5;
   color = "1.0 1.0 1.0 0.2";
   nodeName = "Headlight_node01";
   direction = "0.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(TankLight2)
{
   radius = 1.5;
   color = "1.0 1.0 1.0 0.2";
   nodeName = "Headlight_node02";
   direction = "0.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(TankLight3)
{
   radius = 1.5;
   color = "1.0 1.0 1.0 0.2";
   nodeName = "Headlight_node03";
   direction = "0.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(TankLight4)
{
   radius = 1.5;
   color = "1.0 1.0 1.0 0.2";
   nodeName = "Headlight_node04";
   direction = "0.0 1.0 0.0";
   texture = "special/headlight4";
};

//**************************************************************
// VEHICLE CHARACTERISTICS
//**************************************************************

datablock HoverVehicleData(Artillery) : TankDamageProfile
{
   spawnOffset = "0 0 4";

   floatingGravMag = 4.5;

   catagory = "Vehicles";
   shapeFile = "vehicle_grav_tank.dts";
   multipassenger = true;
   computeCRC = true;
   renderWhenDestroyed = false;

   weaponNode = 1;

   debrisShapeName = "vehicle_land_assault_debris.dts";
   debris = ShapeDebris;

   drag = 0.0;
   density = 0.9;

   mountPose[0] = sitting;
   mountPose[1] = sitting;
   numMountPoints = 2;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = true;

   cameraMaxDist = 20;
   cameraOffset = 3;
   cameraLag = 1.5;
   explosion = LargeGroundVehicleExplosion2;
   explosionDamage = 0.5;
   explosionRadius = 5.0;

   maxSteeringAngle = 0.5;  // 20 deg.

   maxDamage = 1.575;
   destroyedLevel = 1.575;

   isShielded = true;
   rechargeRate = 1.0;
   energyPerDamagePoint = 300;
   maxEnergy = 400;
   minJetEnergy = 15;
   jetEnergyDrain = 2.0;

   // Rigid Body
   mass = 4000;
   bodyFriction = 0.8;
   bodyRestitution = 0.5;
   minRollSpeed = 3;
   gyroForce = 400;
   gyroDamping = 0.3;
   stabilizerForce = 20;
   minDrag = 10;
   softImpactSpeed = 15;       // Play SoftImpact Sound
   hardImpactSpeed = 18;      // Play HardImpact Sound

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 17;
   speedDamageScale = 0.060;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 18;
   collDamageMultiplier   = 0.045;

   dragForce            = 40 / 20;
   vertFactor           = 0.0;
   floatingThrustFactor = 0.15;

   mainThrustForce    = 50;
   reverseThrustForce = 40;
   strafeThrustForce  = 40;
   turboFactor        = 1.7;

   brakingForce = 25;
   brakingActivationSpeed = 4;

   stabLenMin = 3.25;
   stabLenMax = 4;
   stabSpringConstant  = 50;
   stabDampingConstant = 20;

   gyroDrag = 20;
   normalForce = 20;
   restorativeForce = 10;
   steeringForce = 15;
   rollForce  = 5;
   pitchForce = 3;

   dustEmitter = TankDustEmitter;
   triggerDustHeight = 3.5;
   dustHeight = 1.0;
   dustTrailEmitter = TireEmitter;
   dustTrailOffset = "0.0 -1.0 0.5";
   triggerTrailHeight = 3.6;
   dustTrailFreqMod = 15.0;

   jetSound         = ArtilleryThrustSound;
   engineSound      = ArtilleryEngineSound;
   floatSound       = ArtillerySkid;
   softImpactSound  = GravSoftImpactSound;
   hardImpactSound  = HardImpactSound;
   wheelImpactSound = WheelImpactSound;

   forwardJetEmitter = TankJetEmitter;

   //
   softSplashSoundVelocity = 5.0;
   mediumSplashSoundVelocity = 10.0;
   hardSplashSoundVelocity = 15.0;
   exitSplashSoundVelocity = 10.0;

   exitingWater      = VehicleExitWaterMediumSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeMediumSplashSound;

   minMountDist = 4;

   damageEmitter[0] = SmallLightDamageSmoke;
   damageEmitter[1] = SmallHeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "0.0 -1.5 3.5 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 1;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDGroundTankIcon;
   cmdMiniIconName = "commander/MiniIcons/com_tank_grey";
   targetNameTag = 'Beowulf';
   targetTypeTag = 'Long Range Artillery';
   sensorData = VehiclePulseSensor;

   checkRadius = 5.5535;
   observeParameters = "1 10 10";
   runningLight[0] = TankLight1;
   runningLight[1] = TankLight2;
   runningLight[2] = TankLight3;
   runningLight[3] = TankLight4;
   shieldEffectScale = "0.9 1.0 0.6";
   showPilotInfo = 1;
};

//**************************************************************
// WEAPONS
//**************************************************************

//-------------------------------------
// Artillery CHAINGUN (projectile)
//-------------------------------------

datablock TracerProjectileData(ArtilleryChaingunBullet)
{
   doDynamicClientHits = true;

   projectileShapeName = "";
   directDamage        = 0.16;
   directDamageType    = $DamageType::TankChaingun;
   hasDamageRadius     = false;
   splash			   = ChaingunSplash;

   kickbackstrength    = 0.0;
   sound          	   = TankChaingunProjectile;

   dryVelocity       = 425.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 15.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.10;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;

   activateDelayMS   = 100;

   explosion = ChaingunExplosion;
};

//-------------------------------------
// Artillery CHAINGUN CHARACTERISTICS
//-------------------------------------

datablock TurretData(ArtilleryPlasmaTurret) : TurretDamageProfile
{
   className      = VehicleTurret;
   catagory       = "Turrets";
   shapeFile      = "Turret_tank_base.dts";
   preload        = true;

   mass           = 1.0;  // Not really relevant

   maxEnergy               = 1;
   maxDamage               = Artillery.maxDamage;
   destroyedLevel          = Artillery.destroyedLevel;
   repairRate              = 0;

   // capacitor
   maxCapacitorEnergy      = 250;
   capacitorRechargeRate   = 1.0;

   thetaMin = 0;
   thetaMax = 100;

   inheritEnergyFromMount = true;
   firstPersonOnly = true;
   useEyePoint = true;
   numWeapons = 1;

   cameraDefaultFov = 90.0;
   cameraMinFov = 5.0;
   cameraMaxFov = 120.0;

   targetNameTag = 'Artillery Chaingun';
   targetTypeTag = 'Turret';
};

datablock TurretImageData(ArtilleryTurretParam)
{
   mountPoint = 2;
   shapeFile = "turret_muzzlepoint.dts";

   projectile = ArtilleryChaingunBullet;
   projectileType = TracerProjectile;

   useCapacitor = true;
   usesEnergy = true;

   // Turret parameters
   activationMS      = 10;
   deactivateDelayMS = 15;
   thinkTimeMS       = 20;
   degPerSecTheta    = 500;
   degPerSecPhi      = 500;

   attackRadius      = 750;
};

datablock TurretImageData(ArtilleryPlasmaTurretBarrel)
{
   shapeFile = "turret_tank_barrelchain.dts";
   mountPoint = 1;

   projectile = ArtilleryChaingunBullet;
   projectileType = TracerProjectile;

   casing              = ShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 3.0;

   projectileSpread = 12.0 / 1000.0;

   useCapacitor = true;
   usesEnergy = true;
   useMountEnergy = true;
   fireEnergy = 3.75;
   minEnergy = 15.0;

   // Turret parameters
   activationMS      = 400;
   deactivateDelayMS = 50;
   thinkTimeMS       = 20;
   degPerSecTheta    = 360;
   degPerSecPhi      = 360;
   attackRadius      = 750;

   // State transitions
   stateName[0]                        = "Activate";
   stateTransitionOnNotLoaded[0]       = "Dead";
   stateTransitionOnLoaded[0]          = "ActivateReady";
   stateSound[0]                       = ArtilleryTurretActivateSound;

   stateName[1]                        = "ActivateReady";
   stateSequence[1]                    = "Activate";
   stateSound[1]                       = ArtilleryTurretActivateSound;
   stateTimeoutValue[1]                = 0.1;
   stateTransitionOnTimeout[1]         = "Ready";
   stateTransitionOnNotLoaded[1]       = "Deactivate";

   stateName[2]                        = "Ready";
   stateTransitionOnNotLoaded[2]       = "Deactivate";
   stateTransitionOnTriggerDown[2]     = "Fire";
   stateTransitionOnNoAmmo[2]          = "NoAmmo";

   stateName[3]                        = "Fire";
   stateSequence[3]                    = "Fire";
   stateSequenceRandomFlash[3]         = true;
   stateFire[3]                        = true;
   stateAllowImageChange[3]            = false;
   stateSound[3]                       = ArtilleryChaingunFireSound;
   stateScript[3]                      = "onFire";
   stateTimeoutValue[3]                = 0.1;
   stateTransitionOnTimeout[3]         = "Fire";
   stateTransitionOnTriggerUp[3]       = "Reload";
   stateTransitionOnNoAmmo[3]          = "noAmmo";

   stateName[4]                        = "Reload";
   stateSequence[4]                    = "Reload";
   stateTimeoutValue[4]                = 0.1;
   stateAllowImageChange[4]            = false;
   stateTransitionOnTimeout[4]         = "Ready";
   stateTransitionOnNoAmmo[4]          = "NoAmmo";
   stateWaitForTimeout[4]              = true;

   stateName[5]                        = "Deactivate";
   stateSequence[5]                    = "Activate";
   stateDirection[5]                   = false;
   stateTimeoutValue[5]                = 0.1;
   stateTransitionOnTimeout[5]         = "ActivateReady";

   stateName[6]                        = "Dead";
   stateTransitionOnLoaded[6]          = "ActivateReady";
   stateTransitionOnTriggerDown[6]     = "DryFire";

   stateName[7]                        = "DryFire";
   stateSound[7]                       = ArtilleryChaingunDryFireSound;
   stateTimeoutValue[7]                = 0.1;
   stateTransitionOnTimeout[7]         = "NoAmmo";

   stateName[8]                        = "NoAmmo";
   stateTransitionOnAmmo[8]            = "Reload";
   stateSequence[8]                    = "NoAmmo";
   stateTransitionOnTriggerDown[8]     = "DryFire";
};

datablock TurretImageData(ArtilleryPlasmaTurretBarrel2) : ArtilleryPlasmaTurretBarrel
{
   mountPoint = 0;
};

datablock ShapeBaseImageData(ArtilleryCannonTurret)
{
   className = WeaponImage;
   shapeFile = "turret_tank_barrelmortar.dts";
   mountPoint = 3;
   offset = "2 -2.5 1.5";
   rotation = "-1 0 0 80";
   activationMS      = 100;
   deactivateDelayMS = 150;
   thinkTimeMS       = 20;
   degPerSecTheta    = 500;
   degPerSecPhi      = 500;
   attackRadius      = 500;
};

datablock ShapeBaseImageData(ArtilleryCannonTurret2)
{
   className = WeaponImage;
   shapeFile = "turret_tank_barrelmortar.dts";
   mountPoint = 3;
   offset = "-2 -2.5 1.5";
   rotation = "-1 0 0 80";
   activationMS      = 100;
   deactivateDelayMS = 150;
   thinkTimeMS       = 20;
   degPerSecTheta    = 500;
   degPerSecPhi      = 500;
   attackRadius      = 500;
};

function Artillery::onAdd(%this, %obj)
{
   Parent::onAdd(%this, %obj);
   %turret = TurretData::create(ArtilleryPlasmaTurret);
   %turret.selectedWeapon = 1;
   MissionCleanup.add(%turret);
   %turret.team = %obj.teamBought;
   %turret.setSelfPowered();
   %obj.mountObject(%turret, 10);
   %turret.setCapacitorRechargeRate( %turret.getDataBlock().capacitorRechargeRate );
   %obj.turretObject = %turret;
   %turret.setAutoFire(false);
   %turret.mountImage(ArtilleryPlasmaTurretBarrel, 2);
   %turret.mountImage(ArtilleryPlasmaTurretBarrel2, 4);
   %turret.mountImage(AssaultTurretParam, 0);
   %obj.schedule(6000, "playThread", $ActivateThread, "activate");
   setTargetSensorGroup(%turret.getTarget(), %turret.team);
   setTargetNeverVisMask(%turret.getTarget(), 0xffffffff);

   %obj.mountImage(ArtilleryCannonTurret, 3);
   %obj.mountImage(ArtilleryCannonTurret2, 4);
   schedule(6100, 0, "ArtilleryLookForTarget", %obj);
}

function Artillery::deleteAllMounted(%data, %obj)
{
   %turret = %obj.getMountNodeObject(10);
   if (%turret)
   {
      if(%client = %turret.getControllingClient())
      {
         %client.player.setControlObject(%client.player);
         %client.player.mountImage(%client.player.lastWeapon, $WeaponSlot);
         %client.player.mountVehicle = false;
      }
      %turret.delete();
   }
   %turret = %obj.getMountNodeObject(10);
   if(%turret)
      %turret.delete();
}

function Artillery::playerMounted(%data, %obj, %player, %node)
{
   if(%node == 0)
      commandToClient(%player.client, 'setHudMode', 'Pilot', "Assault", %node);
   else if(%node == 1)
   {
      %turret = %obj.getMountNodeObject(10);
      %player.vehicleTurret = %turret;
      %player.setTransform("0 0 0 0 0 1 0");
      %player.lastWeapon = %player.getMountedImage($WeaponSlot);
      %player.unmountImage($WeaponSlot);
      if(!%player.client.isAIControlled())
      {
         %player.setControlObject(%turret);
         %player.client.setObjectActiveImage(%turret, 2);
      }
      %turret.turreteer = %player;
      $aWeaponActive = 0;
      commandToClient(%player.client,'SetWeaponryVehicleKeys', true);
      %obj.getMountNodeObject(10).selectedWeapon = 1;
	   commandToClient(%player.client, 'setHudMode', 'Pilot', "Assault", %node);
   }
   if( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, false );
   %passString = buildPassengerString(%obj);
	for(%i = 0; %i < %data.numMountPoints; %i++)
		if(%obj.getMountNodeObject(%i) > 0)
		   commandToClient(%obj.getMountNodeObject(%i).client, 'checkPassengers', %passString);
}

function ArtilleryPlasmaTurret::onDamage(%data, %obj)
{
   %newDamageVal = %obj.getDamageLevel();
   if(%obj.lastDamageVal !$= "")
      if(isObject(%obj.getObjectMount()) && %obj.lastDamageVal > %newDamageVal)
         %obj.getObjectMount().setDamageLevel(%newDamageVal);
   %obj.lastDamageVal = %newDamageVal;
}

function ArtilleryPlasmaTurret::damageObject(%this, %targetObject, %sourceObject, %position, %amount, %damageType ,%vec, %client, %projectile)
{
   %vehicle = %targetObject.getObjectMount();
   if(%vehicle)
      %vehicle.getDataBlock().damageObject(%vehicle, %sourceObject, %position, %amount, %damageType, %vec, %client, %projectile);
}

function ArtilleryPlasmaTurret::onTrigger(%data, %obj, %trigger, %state)
{
   switch (%trigger)
   {
      case 0:
         %obj.fireTrigger = %state;
         if(%state)
         {
            %obj.setImageTrigger(2, true);
            %obj.setImageTrigger(4, true);
         }
         else
         {
            %obj.setImageTrigger(2, false);
            %obj.setImageTrigger(4, false);
         }
      case 2:
         if(%state)
            %obj.getDataBlock().playerDismount(%obj);
   }
}

function ArtilleryPlasmaTurret::playerDismount(%data, %obj)
{
   %obj.fireTrigger = 0;
   %obj.setImageTrigger(2, false);
   %obj.setImageTrigger(4, false);
   %client = %obj.getControllingClient();
   %client.player.mountImage(%client.player.lastWeapon, $WeaponSlot);
   %client.player.mountVehicle = false;
   setTargetSensorGroup(%obj.getTarget(), 0);
   setTargetNeverVisMask(%obj.getTarget(), 0xffffffff);
}

function ArtilleryCannonTurret::onMount(%this, %obj, %slot)
{
}

function ArtilleryCannonTurret::onUnmount(%this, %obj, %slot)
{
}

function ArtilleryCannonTurret2::onMount(%this, %obj, %slot)
{
}

function ArtilleryCannonTurret2::onUnmount(%this, %obj, %slot)
{
}

function ArtilleryLookForTarget(%obj)
{
   if (isObject(%obj))
   {
      if (%obj.lastcannon == 3)
         %obj.lastcannon = 4;
      else
         %obj.lastcannon = 3;
      %pos = %obj.getMuzzlePoint(%obj.lastcannon);
      %vec = %obj.getMuzzleVector(%obj.lastcannon);
      %count = ClientGroup.getCount();
      %mainDist = 1760;
      %targetPos = "";
      %damageMasks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType |
                     $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType |
                     $TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType |
                     $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType;
      for (%i=0;%i<%count;%i++)
      {
         %obj2 = ClientGroup.getObject(%i);
         %player = %obj2.player;
         if (%player)
         {
            if ((%player.team == %obj.team) && (%player.posLaze))
            {
               %muzzlePos = %player.getMuzzlePoint($WeaponSlot);
               %muzzleVec = %player.getMuzzleVector($WeaponSlot);
               %endPos    = VectorAdd(%muzzlePos, VectorScale(%muzzleVec, 1000));
               %hit = ContainerRayCast(%muzzlePos, %endPos, %damageMasks, %player);
               if (%hit)
                  %pos2 = getWords(%hit, 1, 3);
               %dist = VectorDist(%pos, %pos2);
               if ((%dist > 2) && (%dist < 7500) && (%dist < %mainDist))
               {
                  %targetPos = %pos2;
                  %mainDist = %dist;
               }
            }
         }
      }
      if (%targetPos)
      {
         %p = new GrenadeProjectile() {
            dataBlock        = ArtilleryShot;
            initialDirection = %vec;
            initialPosition  = %pos;
            sourceObject     = %obj.lastpilot;
            sourceSlot       = %obj.lastcannon;
            vehicleObject    = %obj;
         };
         MissionCleanup.add(%p);
         schedule(200, 0, ArtilleryShotDrop, %p, %pos, %targetPos);
      }
      schedule (50, 0, ArtilleryLookForTarget, %obj);
   }
}

function ArtilleryShotDrop(%b, %pos, %pos2)
{
   if (isObject(%b))
   {
      %dist = VectorDist(%pos, %pos2);
      %accuracy = %dist / 100;
      %b.schedule(50, "delete");
      %pos3 = VectorAdd(%pos2, "0 0 500");
      %vec = "0 0 -1";
      %x = (getRandom() - 0.5) * 2 * 3.1415926 * %accuracy / 10000;
      %y = (getRandom() - 0.5) * 2 * 3.1415926 * %accuracy / 10000;
      %z = (getRandom() - 0.5) * 2 * 3.1415926 * %accuracy / 10000;
      %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
      %vec = MatrixMulVector(%mat, %vec);
      %p = new GrenadeProjectile() {
         dataBlock        = ArtilleryShot;
         initialDirection = %vec;
         initialPosition  = %pos3;
         sourceObject     = %b.sourceObject;
         sourceSlot       = %b.sourceSlot;
         vehicleObject    = %b.vehicleObject;
      };
      MissionCleanup.add(%p);
   }
}

datablock GrenadeProjectileData(ArtilleryShot)
{
   projectileShapeName = "mortar_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 1.0;
   damageRadius        = 25.0;
   radiusDamageType    = $DamageType::Mortar;
   kickBackStrength    = 2500;
   scale = "2 2 2";
   explosion           = "MortarExplosion";
   underwaterExplosion = "UnderwaterMortarExplosion";
   velInheritFactor    = 0.5;
   splash              = MortarSplash;
   depthTolerance      = 10.0; // depth at which it uses underwater explosion
   baseEmitter         = MortarSmokeEmitter;
   bubbleEmitter       = MortarBubbleEmitter;
   grenadeElasticity = 0.15;
   grenadeFriction   = 0.4;
   armingDelayMS     = 10;
   muzzleVelocity    = 300;
   drag              = 0.01;
   sound			 = MortarProjectileSound;
   hasLight    = true;
   lightRadius = 4;
   lightColor  = "0.05 0.2 0.05";
   hasLightUnderwaterColor = true;
   underWaterLightColor = "0.05 0.075 0.2";
};
