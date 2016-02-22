///Using new weapon mode switching logic :D :D

$WeaponSettings1[TractorGunImage] = "7 -1 TractorGun:[Power]";
$WeaponSetting1[TractorGunImage,0] = "Grapling Power 1";
$WeaponSetting1[TractorGunImage,1] = "Grapling Power 2";
$WeaponSetting1[TractorGunImage,2] = "Grapling Power 3";
$WeaponSetting1[TractorGunImage,3] = "Grapling Power 4";
$WeaponSetting1[TractorGunImage,4] = "Grapling Power 5";
$WeaponSetting1[TractorGunImage,5] = "Grapling Power 10";
$WeaponSetting1[TractorGunImage,6] = "Grapling Power 125";
$WeaponSetting1[TractorGunImage,7] = "Grapling Power 256";


$WeaponSettings2[TractorGunImage] = "5 -1 TractorGun:[Direction]";
$WeaponSetting2[TractorGunImage,0] = "Pull target";
$WeaponSetting2[TractorGunImage,1] = "Push target";
$WeaponSetting2[TractorGunImage,2] = "Keep target at 10 meters distance";
$WeaponSetting2[TractorGunImage,3] = "Pull user";
$WeaponSetting2[TractorGunImage,4] = "Push user";
$WeaponSetting2[TractorGunImage,5] = "Keep user at 10 meters distance";


datablock ItemData(TractorGun)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_elf.dts";
   image        = TractorGunImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "an Tractor gun";

   computeCRC = true;
   emap = true;
};



///////////////////////////////////////////////////
////////////////Tractor Beam///////////////////////
///////////////////////////////////////////////////

datablock ELFProjectileData(TractorBeam)
{
   beamRange         = 150;
   numControlPoints  = 8;
   restorativeFactor = 3.75;
   dragFactor        = 4.5;
   endFactor         = 2.25;
   randForceFactor   = 2;
   randForceTime     = 0.125;
	drainEnergy			= 0.0;
	drainHealth			= 0.0;
   directDamageType  = $DamageType::ELF;
	mainBeamWidth     = 0.1;           // width of blue wave beam
	mainBeamSpeed     = 9.0;            // speed that the beam travels forward
	mainBeamRepeat    = 0.25;           // number of times the texture repeats
   lightningWidth    = 0.5;
   lightningDist      = 0.5;           // distance of lightning from main beam

   fireSound    = ElfGunFireSound;
   wetFireSound = ElfFireWetSound;

   textures[0] = "special/Sniper";
   textures[1] = "special/FlareSpark";
   textures[2] = "special/Redflare";

   emitter = FlareEmitter;
};


datablock ShapeBaseImageData(TractorGunImage)
{
   className = WeaponImage;

   shapeFile = "weapon_elf.dts";
   item = TractorGun;
   offset = "0 0 0";

   projectile = TractorBeam;
   projectileType = ELFProjectile;
   deleteLastProjectile = true;
   emap = true;


	usesEnergy = true;
 	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = ELFGunSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "CheckWet";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 5;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   //stateSound[3]                    = ElfFireWetSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
   stateTransitionOnNoAmmo[6]       = "NoAmmo";

   stateName[6]                     = "DryFire";
   stateSound[6]                    = ElfFireWetSound;
   stateTimeoutValue[6]             = 0.5;
   stateTransitionOnTimeout[6]      = "Ready";

   stateName[7]                     = "CheckWet";
   stateTransitionOnWet[7]          = "DryFire";
   stateTransitionOnNotWet[7]       = "Fire";
};

datablock TurretImageData(TractorTurretBarrel)
{


   shapeFile = "turret_Elf_large.dts";//"weapon_elf.dts";//
   item = ELFGun;

   projectile = TractorBeam;
   projectileType = ELFProjectile;

   deleteLastProjectile = true;
   emap = true;


   UsesEnergy = true;
   minEnergy = 0.1;
   fireEnergy = 0.1;

   rotation = "0 1 0 90";
   offset = "0 -0.5 0";

   yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances
   // Turret parameters
   activationMS      = 500;
   deactivateDelayMS = 100;
   thinkTimeMS       = 100;
   degPerSecTheta    = 580;
   degPerSecPhi      = 960;
   attackRadius      = 500;


   yawVariance          = 60.0; // these will smooth out the elf tracking code.
   pitchVariance        = 60.0; // more or less just tolerances


   // State transiltions
   stateName[0]                        = "Activate";
   stateTransitionOnNotLoaded[0]       = "Dead";
   stateTransitionOnLoaded[0]          = "ActivateReady";

   stateName[1]                        = "ActivateReady";
   stateSequence[1]                    = "Activate";
   stateSound[1]                       = EBLSwitchSound;
   stateTimeoutValue[1]                = 1;
   stateTransitionOnTimeout[1]         = "Ready";
   stateTransitionOnNotLoaded[1]       = "Deactivate";
   stateTransitionOnNoAmmo[1]          = "NoAmmo";

   stateName[2]                        = "Ready";
   stateTransitionOnNotLoaded[2]       = "Deactivate";
   stateTransitionOnTriggerDown[2]     = "Fire";
   stateTransitionOnNoAmmo[2]          = "NoAmmo";

   stateName[3]                        = "Fire";
   stateFire[3]                        = true;
   stateRecoil[3]                      = LightRecoil;
   stateAllowImageChange[3]            = false;
   stateSequence[3]                    = "Fire";
   stateSound[3]                       = EBLFireSound;
   stateScript[3]                      = "onFire";
   stateTransitionOnTriggerUp[3]       = "Deconstruction";
   stateTransitionOnNoAmmo[3]          = "Deconstruction";

   stateName[4]                        = "Reload";
   stateTimeoutValue[4]                = 0.01;
   stateAllowImageChange[4]            = false;
   stateSequence[4]                    = "Reload";
   stateTransitionOnTimeout[4]         = "Ready";
   stateTransitionOnNotLoaded[4]       = "Deactivate";
   stateTransitionOnNoAmmo[4]          = "NoAmmo";

   stateName[5]                        = "Deactivate";
   stateSequence[5]                    = "Activate";
   stateDirection[5]                   = false;
   stateTimeoutValue[5]                = 1;
   stateTransitionOnLoaded[5]          = "ActivateReady";
   stateTransitionOnTimeout[5]         = "Dead";

   stateName[6]                        = "Dead";
   stateTransitionOnLoaded[6]          = "ActivateReady";

   stateName[7]                        = "NoAmmo";
   stateTransitionOnAmmo[7]            = "Reload";
   stateSequence[7]                    = "NoAmmo";

   stateName[8]                       = "Deconstruction";
   stateScript[8]                     = "deconstruct";
   stateTransitionOnNoAmmo[8]         = "NoAmmo";
   stateTransitionOnTimeout[8]        = "Reload";
   stateTimeoutValue[8]               = 0.1;
};

function TractorBeam::zapTarget(%data, %projectile, %target, %targeter)
{
%projectile.checkTractorStatus(%data, %target, %targeter);
}

function TractorTurretBarrel::onFire(%data, %obj, %slot)
{
//if (getSimTime()-%target.swingend > 5000)
    %p = Parent::onFire(%data, %obj, %slot);
}

function ELFProjectile::checkTractorStatus(%this, %data, %target, %targeter)
{
   %obj = %this.sourceObject;
   if((isObject(%target)) && (isObject(%obj)))
   {
      if(%target.getDamageState() $= "Destroyed")
      {
         cancel(%this.Tractorrecur);
         %this.delete();
         %targeter.base.resetorders();
         return;
      }

      %data = %target.getDataBlock();
      %data2 = %targeter.getDataBlock();
      %pos = %target.getWorldBoxCenter();
      %pos2 = %targeter.getWorldBoxCenter();
      if (%targeter.IsMtc())
          %pos2= VectorAdd(%pos2,"0 0 15");
      %vec = VectorSub(%pos2,%pos);
      %dist = VectorLen(%vec);
      %dir = VectorNormalize(%vec);
      %set = %targeter.weaponSet1;
      if (%set !$="")          
          %speed = getWord($WeaponSetting1[TractorGunImage,%targeter.weaponSet1],2);
      else
          %speed = 5;

      %dspeed = Limit((%dist-10)/10,-1,1);
      if (%targeter.weaponSet2 == 2 || %targeter.weaponSet2 == 5)
         {
         %speed = %dspeed*%dspeed*%speed;
         %dir = VectorScale(%dir,Lev(%dspeed));
         }

      if (%targeter.weaponSet2 == 1 || %targeter.weaponSet2 == 4)
          %dir = VectorScale(%dir,-1);
      //%amount = VectorScale(%dir, Limit((10000/%dist),0,10000));
      if (%client = %targeter.client)
      %admin = (%client.isAdmin || %client.isSuperAdmin);

      %wantuser = (%targeter.weaponSet2 > 2);
      %tisplayer = (%target.getType() & $TypeMasks::PlayerObjectType);
      %tisgveh = (%target.getType() & $TypeMasks::VehicleObjectType && !%obj.disableMove);
      %allowed = ((%target.team != %obj.team) || ($teamDamage || %admin));

      if (%allowed && (%tisplayer || %tisgveh) && !%wantuser)
         {
            if(%obj.getObjectMount() != %target && %this.hasTarget())
               {
               %limit = (%target.getType() & $TypeMasks::VehicleObjectType);
               %size = Limit(3*%data.mass*%speed,0,10000*(1+9*%limit));
               %amount = VectorScale(%dir, %size);
               %target.applyImpulse(%pos, %amount);
               }
         }
       else
          {
          if(%obj.getObjectMount() != %target && %this.hasTarget())
               {
               %size = Limit(3*%data2.mass*%speed,0,10000);
               %amount = VectorScale(%dir, %size);
               %targeter.applyImpulse(%pos2, VectorScale(%amount,-1));
               }
          }
      %this.TractorRecur = %this.schedule(70, checkTractorStatus, %data, %target, %targeter);
   }
}

/////////////////////////////////////////////////////////////////////////////////
/////////////////////////////SwarmDisc////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////


datablock SeekerProjectileData(SeekingDisc)
{
   projectileShapeName = "disc.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.10;
   damageRadius        = 7.5;
   radiusDamageType    = $DamageType::Disc;
   kickBackStrength    = 1750;

   sound = discProjectileSound;
   explosion           = "DiscExplosion";
   underwaterExplosion = "UnderwaterDiscExplosion";
   splash              = DiscSplash;


   velInheritFactor  = 0.5;
   fizzleTimeMS      = 50000;
   lifetimeMS        = 50000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;


    baseEmitter         = HumanArmorJetEmitter;
   delayEmitter        = HumanArmorJetEmitter;

   exhaustEmitter      =  DiscMistEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";


   muzzleVelocity      = 10.0;
   maxVelocity         = 30.0;
   turningSpeed        = 110.0;
   acceleration        = 5;

   proximityRadius     = 0;

   terrainAvoidanceSpeed         = 180;
   terrainScanAhead              = 25;
   terrainHeightFail             = 12;
   terrainAvoidanceRadius        = 100;


  activateDelayMS = 200;

   hasLight    = true;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";

};


datablock TurretImageData(SwarmDiscTurretBarrel)
{

   shapeFile = "turret_AA_large.dts";//weapon_missile.dts";
   item = MissileLauncher;
   //ammo = MissileLauncherAmmo;
   armThread = lookms;
   emap = true;

   projectile= SeekingDisc;
   projectileType= SeekerProjectile;

   isSeeker     = true;
   seekRadius   = 900;
   maxSeekAngle = 90;
   seekTime     = 5.0;
   minSeekHeat  = 0.0;  // the heat that must be present on a target to lock it.
   yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances

   usesEnergy = true;
   fireEnergy = 5;
   minEnergy = 5;
   projectileSpread = 0.01;


  offset = "0.7 -0.04 -0.45";
   rotation = "0 1 0 -90";

   // only target objects outside this range
   minTargetingDistance             = 4;

   // Turret parameters
   activationMS      = 250;
   deactivateDelayMS = 500;
   thinkTimeMS       = 200;
   degPerSecTheta    = 580;
   degPerSecPhi      = 1080;
   attackRadius      = 500;

   // State transitions
   stateName[0]                  = "Activate";
   stateTransitionOnNotLoaded[0] = "Dead";
   stateTransitionOnLoaded[0]    = "ActivateReady";

   stateName[1]                  = "ActivateReady";
   stateSequence[1]              = "Activate";
   stateSound[1]                 = MBLSwitchSound;
   stateTimeoutValue[1]          = 2;
   stateTransitionOnTimeout[1]   = "Ready";
   stateTransitionOnNotLoaded[1] = "Deactivate";
   stateTransitionOnNoAmmo[1]    = "NoAmmo";

   stateName[2]                    = "Ready";
   stateTransitionOnNotLoaded[2]   = "Deactivate";
   stateTransitionOnTriggerDown[2] = "Fire";
   stateTransitionOnNoAmmo[2]      = "NoAmmo";

   stateName[3]                = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3]        = 0.3;
   stateFire[3]                = true;
   stateRecoil[3]              = LightRecoil;
   stateAllowImageChange[3]    = false;
   stateSequence[3]            = "Fire";
   stateSound[3]               = MBLFireSound;
   stateScript[3]              = "onFire";

   stateName[4]                  = "Reload";
   stateTimeoutValue[4]          = 3.5;
   stateAllowImageChange[4]      = false;
   stateSequence[4]              = "Reload";
   stateTransitionOnTimeout[4]   = "Ready";
   stateTransitionOnNotLoaded[4] = "Deactivate";
   stateTransitionOnNoAmmo[4]    = "NoAmmo";

   stateName[5]                = "Deactivate";
   stateSequence[5]            = "Activate";
   stateDirection[5]           = false;
   stateTimeoutValue[5]        = 2;
   stateTransitionOnLoaded[5]  = "ActivateReady";
   stateTransitionOnTimeout[5] = "Dead";

   stateName[6]               = "Dead";
   stateTransitionOnLoaded[6] = "ActivateReady";

   stateName[7]             = "NoAmmo";
   stateTransitionOnAmmo[7] = "Reload";
   stateSequence[7]         = "NoAmmo";
};

datablock TurretImageData(SwarmDiscTurretBarrel1):SwarmDiscTurretBarrel
{
    shapeFile = "turret_AA_large.dts";//weapon_mortar.dts";
   item = Mortar;
   offset = "-0.7 -0.04 -0.45";
   rotation = "0 1 0 -90";
};


function SwarmDiscTurretBarrel::onMount(%this, %obj, %slot)
{
%mount1 = "SwarmDiscTurretBarrel1";
%mount2 = "MtcTurretCore";
%obj.schedule(2000,"mountImage",%mount1,1,true);
%obj.mountImage(%mount2,2,true);
}


function SwarmDiscTurretBarrel::onFire(%data,%obj,%slot)
{
for (%i = 0; %i < 5;%i++)
   {
%obj.currentbarrel = !%obj.currentbarrel;
%slot = %obj.currentbarrel;
   %p = Parent::onFire(%data, %obj, %slot);
   if (%p)
      {
      MissileSet.add(%p);

      if (%obj.base.target)
         {
      %target = %obj.base.target;
         }
      else
         %target = %obj.getLockedTarget();

       %sign = GiveSign(%target);
    if(%target)
       {
       %p.setObjectTarget(%sign);
       }
     else if(%obj.isLocked())
        %p.setPositionTarget(%obj.getLockedPosition());
      else
        %p.setNoTarget();
   discoff(%p);
      }
   }

}

function SwarmDiscTurretBarrel1::onFire(%data,%obj,%slot)
{
//Do nothing;
}

function discoff(%p)
{
if (!IsObject(%p))
return "";
if (!IsObject(%p.getTargetObject()) && IsObject(%p.disctarget))
   {
   %p.setObjectTarget(%p.disctarget);
   }
else if (IsObject(%p.getTargetObject()))
   {
   %p.disctarget = %p.getTargetObject();
   %p.setNoTarget();
   }
else
   return "";
schedule(getRandom()*1000+500,0,"discoff",%p);
}

