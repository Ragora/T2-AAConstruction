$MtcWeaponCount = 15;
///Light
$MtcWeapontype[0] = "blaster";
$MtcWeapontype[1] = "disc";
$MtcWeapontype[2] = "laser";
///Medium
$MtcWeapontype[3] = "grenade";
$MtcWeapontype[4] = "plasma";
$MtcWeapontype[5] = "chain";
///Heavy
$MtcWeapontype[6] = "missile";
$MtcWeapontype[7] = "mortar";
$MtcWeapontype[8] = "fusion";
///No damage
$MtcWeapontype[9] = "nerf";
$MtcWeapontype[10] = "target";
///Ion weapons
$MtcWeapontype[11] = "IonMissile";
$MtcWeapontype[12] = "IonBeam";
//Excotic weapons
$MtcWeapontype[13] = "Tractor";
$MtcWeapontype[14] = "SwarmDisc";

$mtcWeaponSize["nerf"] = "light";
$mtcWeaponSize["blaster"] = "light";
$mtcWeaponSize["disc"] = "light";
$mtcWeaponSize["laser"] = "light";
$mtcWeaponSize["target"] = "light";
$mtcWeaponSize["grenade"] = "medium";
$mtcWeaponSize["plasma"] = "medium";
$mtcWeaponSize["chain"] = "medium";
$mtcWeaponSize["Missile"] = "heavy";
$mtcWeaponSize["Mortar"] = "heavy";
$mtcWeaponSize["Fusion"] = "heavy";
$mtcWeaponSize["IonMissile"] = "heavy";
$mtcWeaponSize["IonBeam"] = "heavy";
$mtcWeaponSize["Tractor"] = "light";
$mtcWeaponSize["SwarmDisc"] = "heavy";

$mtcScore["nerf"] = 1;
$mtcScore["blaster"] = 5;
$mtcScore["disc"] = 8;
$mtcScore["laser"] = 8;
$mtcScore["target"] = 1;
$mtcScore["grenade"] = 5;
$mtcScore["plasma"] = 5;
$mtcScore["chain"] = 10;
$mtcScore["Missile"] = 15;
$mtcScore["Mortar"] = 25;
$mtcScore["Fusion"] = 25;
$mtcScore["IonMissile"] = 15;
$mtcScore["Ionbeam"] = 20;
$mtcScore["Tractor"] = 10;
$mtcScore["SwarmDisc"] = 30;

$MtcBaseChange["nerf"] = 1;
$MtcBaseChange["blaster"] = 1;//6;
$MtcBaseChange["disc"] = 1;//8;
$MtcBaseChange["laser"] = 1;//4;
$MtcBaseChange["target"] = 1;
$MtcBaseChange["grenade"] = 1;//8;
$MtcBaseChange["plasma"] = 1;//8;
$MtcBaseChange["chain"] = 1;//8;
$MtcBaseChange["Missile"] = 1;//4;
$MtcBaseChange["Mortar"] = 1;//2;
$MtcBaseChange["Fusion"] = 1;//4;
$MtcBaseChange["IonMissile"] = 1;//2;
$MtcBaseChange["Ionbeam"] = 1;
$MtcBaseChange["Tractor"] = 1;//2;
$MtcBaseChange["SwarmDisc"] = 1;

$MtcWeaponBlock["nerf"] = 1;

function MtcMakeChangeList()
{
$MTCCHANGE::count=0;
$MTCCHANGE::totchange=0;
for (%i=0;%i< $MtcWeaponCount;%i++)
    {
    %weapon = $MtcWeapontype[%i];
    if (!$MtcWeaponBlock[%weapon] && $MtcBaseChange[%weapon])
         {
         $MTCCHANGE::weapon[%i] =%weapon;
         $MTCCHANGE::change[%i] = $MTCCHANGE::totchange + $MtcBaseChange[%weapon];
         $MTCCHANGE::totchange =$MTCCHANGE::change[%i];
         $MTCCHANGE::count++;
         }
    }
}

function MtcRandomWeapon()
{
%count = $MTCCHANGE::count;
if (!%count)
    MTCmakeChangelist();
%r = getRandom()*$MTCCHANGE::totchange;

for (%i=0; %i<%count; %i++)
    {
    if ($MTCCHANGE::change[%i]>%r)
        return $MTCCHANGE::weapon[%i];
    }
return $MTCCHANGE::weapon[0];
}


datablock ShapeBaseImageData(MtcTurretCore)
{
 shapeFile = "pack_barrel_elf.dts";//weapon_missile.dts";

   item = MissileLauncher;

   offset = "0 0.5 -0.7";
   rotation = "1 0 0 90";
};


/////////////////////////////////////////////
////////////////Fusion///////////////////////
/////////////////////////////////////////////


datablock TurretImageData(FusionTurretBarrel)
{
   shapeFile = "turret_fusion_large.dts";
   item      = PlasmaBarrelLargePack;

   projectile = PlasmaBarrelBolt;
   projectileType = LinearFlareProjectile;
   usesEnergy = true;
   fireEnergy = 10;
   minEnergy = 10;
   emap = true;

   offset = "-0.7 -0.04 -0.4";
   rotation = "0 1 0 90";


   // Turret parameters
   activationMS      = 1000;
   deactivateDelayMS = 1500;
   thinkTimeMS       = 200;
   degPerSecTheta    = 300;
   degPerSecPhi      = 500;
   attackRadius      = 120;

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
   stateSound[3]               = PBLFireSound;
   stateScript[3]              = "onFire";

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

datablock TurretImageData(FusionTurretBarrel1)
{
shapeFile = "turret_fusion_large.dts";
item      = PlasmaBarrelLargePack;
offset = "0.7 -0.04 -0.4";
rotation = "0 1 0 -90";
};

function FusionTurretBarrel::onMount(%this, %obj, %slot)
{
%mount1 = "FusionTurretBarrel1";
%mount2 = "MtcTurretCore";
%obj.schedule(2000,"mountImage",%mount1,1,true);
%obj.mountImage(%mount2,2,true);
}

function FusionTurretBarrel::onFire( %data, %obj, %slot )
{
Parent::onFire(%data,%obj,%obj.currentbarrel);
%obj.currentbarrel = !%obj.currentbarrel;
}

function FusionTurretBarrel1::onFire( %data, %obj, %slot )
{
///Do nothing;
}

/////////////////////////////////////////////
////////////////Mortar///////////////////////
/////////////////////////////////////////////



datablock TurretImageData(MortarTurretBarrel)
{
   shapeFile = "turret_mortar_large.dts";//weapon_mortar.dts";
   item = Mortar;
   mountPoint = 0;
   //ammo = MortarAmmo;

   emap = true;

   projectileSpread = 8/1000;

   usesEnergy = true;
   fireEnergy = 4;
   minEnergy = 4;

  yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances

    offset = "-0.7 -0.04 -0.45";
   rotation = "0 1 0 90";

    // Turret parameters
   activationMS      = 3000;
   deactivateDelayMS = 5000;
   thinkTimeMS       = 2000;
   degPerSecTheta    = 2080;
   degPerSecPhi      = 2080;
   attackRadius      = 800;

   projectile = MortarShot;
   projectileType = GrenadeProjectile;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = MortarSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";
   //stateSound[2] = MortarIdleSound;

   stateName[3] = "Fire";
   stateSequence[3] = "Recoil";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.4; // 0.8
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateSound[3] = MortarFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 1.0; //2.0
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   stateSound[4] = MortarReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = MortarDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
};

datablock TurretImageData(MortarTurretBarrel1):MortarTurretBarrel
{
    shapeFile = "turret_mortar_large.dts";//weapon_mortar.dts";
   item = Mortar;
   mountPoint = 0;
   offset = "0.7 -0.04 -0.45";
   rotation = "0 1 0 -90";
};


function MortarTurretBarrel::onMount(%this, %obj, %slot)
{
%mount1 = "MortarTurretBarrel1";
%mount2 = "MtcTurretCore";
%obj.schedule(2000,"mountImage",%mount1,1,true);
%obj.mountImage(%mount2,2,true);
}


function MortarTurretBarrel::onFire(%data,%obj,%slot)
{
Parent::onFire(%data,%obj,%obj.currentbarrel);
%obj.currentbarrel = !%obj.currentbarrel;
}

function MortarTurretBarrel1::onFire(%data,%obj,%slot)
{
///Do nothing;
}


/////////////////////////////////////////////
////////////////Blaster//////////////////////
/////////////////////////////////////////////




datablock TurretImageData(blasterTurretBarrel)
{

   shapeFile = "weapon_energy.dts";
   item = Blaster;
   projectile = EnergyBolt;
   projectileType = EnergyProjectile;

   usesEnergy = true;
   fireEnergy = 4;
   minEnergy = 4;

  yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances

   offset = "0.05 -0.4 0";
   rotation = "0 1 0 90";

   projectileSpread = 8/1000;

    // Turret parameters
   activationMS      = 3000;
   deactivateDelayMS = 5000;
   thinkTimeMS       = 2000;
   degPerSecTheta    = 2080;
   degPerSecPhi      = 2080;
   attackRadius      = 800;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = BlasterSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.3;
   stateFire[3] = true;
   stateRecoil[3] = NoRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateSound[3] = BlasterFireSound;
   stateScript[3] = "onFire";

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6] = "DryFire";
   stateTimeoutValue[6] = 0.3;
   stateSound[6] = BlasterDryFireSound;
   stateTransitionOnTimeout[6] = "Ready";
};

/////////////////////////////////////////////
////////////////Disc/////////////////////////
/////////////////////////////////////////////


datablock TurretImageData(DiscTurretBarrel)
{

   shapeFile = "weapon_disc.dts";
   item = Disc;
   //ammo = DiscAmmo;

   emap = true;

   projectileSpread = 4.0 / 1000.0;

   projectile = DiscProjectile;
   projectileType = LinearProjectile;

   usesEnergy = true;
   fireEnergy = 4;
   minEnergy = 4;

  yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances

    offset = "0 -0.2 0";
    rotation = "0 1 0 90";
    // Turret parameters
   activationMS      = 3000;
   deactivateDelayMS = 5000;
   thinkTimeMS       = 2000;
   degPerSecTheta    = 2080;
   degPerSecPhi      = 2080;
   attackRadius      = 800;

   // State Data
   stateName[0]                     = "Preactivate";
   stateTransitionOnLoaded[0]       = "Activate";
   stateTransitionOnNoAmmo[0]       = "NoAmmo";

   stateName[1]                     = "Activate";
   stateTransitionOnTimeout[1]      = "Ready";
   stateTimeoutValue[1]             = 0.5;
   stateSequence[1]                 = "Activated";
   stateSound[1]                    = DiscSwitchSound;

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";
   stateSequence[2]                 = "DiscSpin";
   stateSound[2]                    = DiscLoopSound;

   stateName[3]                     = "Fire";
   stateTransitionOnTimeout[3]      = "Reload";
   stateTimeoutValue[3]             = 1.25;
   stateFire[3]                     = true;
   stateRecoil[3]                   = LightRecoil;
   stateAllowImageChange[3]         = false;
   stateSequence[3]                 = "Fire";
   stateScript[3]                   = "onFire";
   stateSound[3]                    = DiscFireSound;

   stateName[4]                     = "Reload";
   stateTransitionOnNoAmmo[4]       = "NoAmmo";
   stateTransitionOnTimeout[4]      = "Ready";
   stateTimeoutValue[4]             = 0.5; // 0.25 load, 0.25 spinup
   stateAllowImageChange[4]         = false;
   stateSequence[4]                 = "Reload";
   stateSound[4]                    = DiscReloadSound;

   stateName[5]                     = "NoAmmo";
   stateTransitionOnAmmo[5]         = "Reload";
   stateSequence[5]                 = "NoAmmo";
   stateTransitionOnTriggerDown[5]  = "DryFire";

   stateName[6]                     = "DryFire";
   stateSound[6]                    = DiscDryFireSound;
   stateTimeoutValue[6]             = 1.0;
   stateTransitionOnTimeout[6]      = "NoAmmo";

};




/////////////////////////////////////////////
////////////////Missile//////////////////////
/////////////////////////////////////////////



datablock TurretImageData(MissileTurretBarrel)
{

   shapeFile = "turret_missile_large.dts";//weapon_missile.dts";
   item = MissileLauncher;
   //ammo = MissileLauncherAmmo;
   armThread = lookms;
   emap = true;

   projectile = ShoulderMissile;
   projectileType = SeekerProjectile;

   isSeeker     = true;
   seekRadius   = 900;
   maxSeekAngle = 90;
   seekTime     = 2.5;  //5.0
   minSeekHeat  = 0.0;  // the heat that must be present on a target to lock it.
  yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances

   usesEnergy = true;
   fireEnergy = 4;
   minEnergy = 4;
   projectileSpread = 8/1000;

   offset = "-0.7 -0.04 -0.45";
   rotation = "0 1 0 70";


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
   stateTimeoutValue[3]        = 0.15; //0.3
   stateFire[3]                = true;
   stateRecoil[3]              = LightRecoil;
   stateAllowImageChange[3]    = false;
   stateSequence[3]            = "Fire";
   stateSound[3]               = MBLFireSound;
   stateScript[3]              = "onFire";

   stateName[4]                  = "Reload";
   stateTimeoutValue[4]          = 1.75; //3.5
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

datablock TurretImageData(MissileTurretBarrel1):MissileTurretBarrel
{
   shapeFile = "turret_missile_large.dts";//weapon_missile.dts";
   item = MissileLauncher;
   offset = "0.7 -0.04 -0.45";
   rotation = "0 1 0 -70";
};



function MissileTurretBarrel::onMount(%this, %obj, %slot)
{
%mount1 = "MissileTurretBarrel1";
%mount2 = "MtcTurretCore";
%obj.schedule(2000,"mountImage",%mount1,1,true);
%obj.mountImage(%mount2,2,true);
}


function MissileTurretBarrel::onFire(%data,%obj,%slot)
{
%obj.currentbarrel = !%obj.currentbarrel;
%slot = %obj.currentbarrel;
   %p = Parent::onFire(%data, %obj, %slot);
   MissileSet.add(%p);

   //echo(%p.getDatablock.dynamictype);

  if (%obj.base.target)
       {
       if (%obj.base.target.getheat()>0.2)
          {
          %target = %obj.base.target;
          }
       }
   else
       %target = %obj.getLockedTarget();


   if(%target)
      %p.setObjectTarget(%target);
   else if(%obj.isLocked())
      %p.setPositionTarget(%obj.getLockedPosition());
   else
      %p.setNoTarget();
}

function MissileTurretBarrel1::onFire(%data,%obj,%slot)
{
  //Do nothing
}

/////////////////////////////////////////////
////////////////IonMissile///////////////////
/////////////////////////////////////////////


datablock TurretImageData(ionMissileTurretBarrel):MissileTurretBarrel
{
projectile = IonMissile;
projectileType = SeekerProjectile;
projectileSpread = 8/1000;

offset = "0 -0.2 -0.3";
   rotation = "0 1 0 00";
  yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances
};


function IonMissileTurretBarrel::onFire(%data,%obj,%slot)
{
   %p = Parent::onFire(%data, %obj, %slot);
   MissileSet.add(%p);

   //echo(%p.getDatablock.dynamictype);


   if (%obj.base.target)
       {
       if (%obj.base.target.getheat()>0.2)
          {
          %target = %obj.base.target;
          }
       }
   else
       %target = %obj.getLockedTarget();

   if(%target)
      %p.setObjectTarget(%target);
   else if(%obj.isLocked())
      %p.setPositionTarget(%obj.getLockedPosition());
   else
      %p.setNoTarget();
}



/////////////////////////////////////////////
////////////////IonBeam//////////////////////
/////////////////////////////////////////////



datablock TurretImageData(ionbeamTurretBarrel):LaserTurretBarrel
{
shapeFile = "turret_Elf_large.dts";//weapon_missile.dts";
offset = "-0.7 -0.04 -0.4";
rotation = "0 1 0 90";
projectile = ShockBeam2;
projectileType = SniperProjectileData;

   usesEnergy = true;
   fireEnergy = 50.0;
   minEnergy = 50.0;

  yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances
   // Turret parameters
   activationMS      = 500;
   deactivateDelayMS = 100;
   thinkTimeMS       = 100;
   degPerSecTheta    = 580;
   degPerSecPhi      = 960;
   attackRadius      = 500;


   yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances


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


datablock TurretImageData(ionbeamTurretBarrel1):ionbeamTurretBarrel
{
shapeFile = "turret_Elf_large.dts";//weapon_missile.dts";
offset = "0.7 -0.04 -0.4";
rotation = "0 1 0 -90";
};

function IonBeamTurretBarrel::onMount(%this, %obj, %slot)
{
%mount1 = "IonBeamTurretBarrel1";
%mount2 = "MtcTurretCore";
%obj.schedule(2000,"mountImage",%mount1,1,true);
%obj.mountImage(%mount2,2,true);
}

function ionbeamTurretBarrel::onFire( %data, %obj, %slot )
{
%obj.currentbarrel = !%obj.currentbarrel;
%slot = %obj.currentbarrel;
%vec = %obj.getMuzzleVector(%slot);
%vec = VectorMiss(%vec,80);

%energy = %obj.getEnergyLevel();

%p = discharge2(%obj.getMuzzlePoint(%slot),%vec,0,%obj);

   %p.setEnergyPercentage(1);
   %obj.lastProjectile = %p;
   MissionCleanup.add(%p);
   %obj.setEnergyLevel(%energy - %data.fireEnergy);
}

function ionbeamTurretBarrel1::onFire( %data, %obj, %slot )
{
///Do nothing;
}


/////////////////////////////////////////////
////////////////////NERF/////////////////////
/////////////////////////////////////////////


datablock TurretImageData(NerfTurretBarrel)
{

   shapeFile = "weapon_energy.dts";
   item = NerfGun;

   projectile = NerfBolt;
   projectileType = LinearFlareProjectile;

   usesEnergy = true;
   fireEnergy = 4;
   minEnergy = 4;

  yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances

   offset = "0.05 -0.4 0";
   rotation = "0 1 0 90";

    // Turret parameters
   activationMS      = 3000;
   deactivateDelayMS = 5000;
   thinkTimeMS       = 2000;
   degPerSecTheta    = 2080;
   degPerSecPhi      = 2080;
   attackRadius      = 800;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = BlasterSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.3;
   stateFire[3] = true;
   stateRecoil[3] = NoRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateSound[3] = NerfGunFireSound;
   stateScript[3] = "onFire";

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6] = "DryFire";
   stateTimeoutValue[6] = 0.3;
   stateSound[6] = NerfGunDryFireSound;
   stateTransitionOnTimeout[6] = "Ready";
};



/////////////////////////////////////////////
////////////////GRENDADE/////////////////////
/////////////////////////////////////////////


datablock TurretImageData(GrenadeTurretBarrel)
{

   shapeFile = "weapon_grenade_launcher.dts";
   item = GrenadeLauncher;

   emap = true;


   usesEnergy = true;
   fireEnergy = 1.0;
   minEnergy = 1.0;

  yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances

offset = "-0.3 0 -0.05";
rotation = "0 1 0 90";
    // Turret parameters
   activationMS      = 3000;
   deactivateDelayMS = 5000;
   thinkTimeMS       = 2000;
   degPerSecTheta    = 2080;
   degPerSecPhi      = 2080;
   attackRadius      = 800;

   projectileSpread = 8/1000;

   projectile = BasicGrenade;
   projectileType = GrenadeProjectile;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = GrenadeSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.4;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateScript[3] = "onFire";
   stateSound[3] = GrenadeFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.5;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   stateSound[4] = GrenadeReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = GrenadeDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
};


/////////////////////////////////////////////
////////////////LASER////////////////////////
/////////////////////////////////////////////


datablock TurretImageData(LaserTurretBarrel)
{

   shapeFile = "weapon_sniper.dts";
   item = SniperRifle;
   projectile = BasicSniperShot;
   projectileType = SniperProjectile;

	armThread = looksn;

	usesEnergy = true;
	minEnergy = 30.0;
        fireEnergy = 20.0;

rotation = "0 1 0 90";
offset = "0 -0.12 0";

  yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances

    // Turret parameters
   activationMS      = 3000;
   deactivateDelayMS = 5000;
   thinkTimeMS       = 2000;
   degPerSecTheta    = 2080;
   degPerSecPhi      = 2080;
   attackRadius      = 800;


   stateName[0]                     = "Activate";
   stateTransitionOnTimeout[0]      = "ActivateReady";
   stateSound[0]                    = SniperRifleSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateSequence[0]                 = "Activate";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnLoaded[1]       = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "CheckWet";

   stateName[3]                     = "Fire";
   stateTransitionOnTimeout[3]      = "Reload";
   stateTimeoutValue[3]             = 0.5;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateSequence[3]                 = "Fire";
   stateScript[3]                   = "onFire";

   stateName[4]                     = "Reload";
   stateTransitionOnTimeout[4]      = "Ready";
   stateTimeoutValue[4]             = 0.5;
   stateAllowImageChange[4]         = false;

   stateName[5]                     = "CheckWet";
   stateTransitionOnWet[5]          = "DryFire";
   stateTransitionOnNotWet[5]       = "Fire";

   stateName[6]                     = "NoAmmo";
   stateTransitionOnAmmo[6]         = "Reload";
   stateTransitionOnTriggerDown[6]  = "DryFire";
   stateSequence[6]                 = "NoAmmo";

   stateName[7]                     = "DryFire";
   stateSound[7]                    = SniperRifleDryFireSound;
   stateTimeoutValue[7]             = 0.5;
   stateTransitionOnTimeout[7]      = "Ready";
};

function LaserTurretBarrel::onFire( %data, %obj, %slot )
{
%vec = %obj.getMuzzleVector(%slot);
 if(IsObject(%obj.base.target))
   {
      %spread = %obj.base.target.getVelocity();
      %x = (getRandom() - 0.5) * 2 * 3.1415926 * %spread* getWord(%spread,0)/5000;
      %y = (getRandom() - 0.5) * 2 * 3.1415926 * %spread* getWord(%spread,1)/5000;
      %z = (getRandom() - 0.5) * 2 * 3.1415926 * %spread* getWord(%spread,2)/5000;
      %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
      %vec = MatrixMulVector(%mat, %vec);
   }

   %energy = %obj.getEnergyLevel();
   %p = new (SniperProjectile)() {
      dataBlock = BasicSniperShot;
      initialDirection = %vec;
      initialPosition = %obj.getMuzzlePoint(%slot);
      sourceObject = %obj;
      damageFactor = 1;
      sourceSlot = %slot;
   };



   %p.setEnergyPercentage(1);
   %obj.lastProjectile = %p;
   MissionCleanup.add(%p);
   %obj.setEnergyLevel(%energy - %data.fireEnergy);
}

/////////////////////////////////////////////
////////////////TARGET///////////////////////
/////////////////////////////////////////////


datablock TurretImageData(TargetTurretBarrel)
{


   shapeFile = "weapon_targeting.dts";
   item = TargetingLaser;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;

	usesEnergy = true;
	minEnergy = 10;
rotation = "0 1 0 90";
offset = "0 -0.5 0";

    // Turret parameters
   activationMS      = 3000;
   deactivateDelayMS = 5000;
   thinkTimeMS       = 2000;
   degPerSecTheta    = 2080;
   degPerSecPhi      = 2080;
   attackRadius      = 800;

  yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

/////////////////////////////////////////////
////////////////PLASMA///////////////////////
/////////////////////////////////////////////
datablock TurretImageData(Plasmaturretbarrel)
{

   shapeFile = "weapon_plasma.dts";
   item = Plasma;
   ammo = PlasmaAmmo;


   projectile = PlasmaBolt;
   projectileType = LinearFlareProjectile;

   emap = true;

   usesEnergy = true;
   fireEnergy = 1.0;
   minEnergy = 1.0;

   offset = "-0.19 -0.24 0";
   rotation = "0 1 0 90";

  yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances

    // Turret parameters
   activationMS      = 3000;
   deactivateDelayMS = 5000;
   thinkTimeMS       = 2000;
   degPerSecTheta    = 2080;
   degPerSecPhi      = 2080;
   attackRadius      = 800;

   projectileSpread = 16.0 / 1000.0;


   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = PlasmaSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "CheckWet";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.1;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateEmitterTime[3] = 0.2;
   stateSound[3] = PlasmaFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.6;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   stateSound[4] = PlasmaReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = PlasmaDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";

   stateName[7]       = "WetFire";
   stateSound[7]      = PlasmaFireWetSound;
   stateTimeoutValue[7]        = 1.5;
   stateTransitionOnTimeout[7] = "Ready";

   stateName[8]               = "CheckWet";
   stateTransitionOnWet[8]    = "WetFire";
   stateTransitionOnNotWet[8] = "Fire";
};

/////////////////////////////////////////////
////////////////CHAINGUN/////////////////////
/////////////////////////////////////////////


datablock TurretImageData(ChainturretBarrel)
{

   shapeFile = "weapon_chaingun.dts";
   item      = Chaingun;


   usesEnergy = true;
   fireEnergy = 0.1;
   minEnergy = 10;

   offset = "-0.48 -0.05 0.1";
   rotation = "0 -1 0 90";

  yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances


   projectile = ChaingunBullet;
   projectileType = TracerProjectile;
   emap = true;

   casing              = ShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 3.0;

   projectileSpread = 8/1000;

   // Turret parameters
   activationMS      = 1;
   deactivateDelayMS = 1;
   thinkTimeMS       = 1;
   degPerSecTheta    = 2080;
   degPerSecPhi      = 2080;
   attackRadius      = 400;

 // State transitions
stateName[0] = "Activate";
stateTransitionOnNotLoaded[0] = "Dead";
stateTransitionOnLoaded[0] = "ActivateReady";

stateName[1] = "ActivateReady";
stateSequence[1] = "Activate";
stateSound[1] = PBLSwitchSound;
stateTimeoutValue[1] = 0.001;
stateTransitionOnTimeout[1] = "Ready";
stateTransitionOnNotLoaded[1] = "Deactivate";
stateTransitionOnNoAmmo[1] = "NoAmmo";

stateName[2] = "Ready";
stateTransitionOnNotLoaded[2] = "Deactivate";
stateTransitionOnTriggerDown[2] = "Fire";
stateTransitionOnNoAmmo[2] = "NoAmmo";

// fire off about 2 quick shots
stateName[3] = "Fire";
stateFire[3] = true;
stateAllowImageChange[3] = false;
stateSequence[3] = "Fire";
stateSound[3] = ChaingunFireSound;
stateScript[3] = "onFire";
stateTimeoutValue[3] = 0.05; //0.3
stateTransitionOnTimeout[3] = "Fire";
stateTransitionOnTriggerUp[3] = "Ready";
// stateTransitionOnTriggerUp[3] = "Reload";
stateTransitionOnNoAmmo[3] = "NoAmmo";

stateName[8] = "Reload";
stateTimeoutValue[7] = 0.001;
stateAllowImageChange[8] = false;
stateSequence[8] = "Reload";
stateTransitionOnTimeout[8] = "Ready";
stateTransitionOnNotLoaded[8] = "Deactivate";
stateTransitionOnNoAmmo[8] = "NoAmmo";

stateName[9] = "Deactivate";
stateSequence[9] = "Activate";
stateDirection[9] = false;
stateTimeoutValue[9] = 0.1;
stateTransitionOnLoaded[9] = "ActivateReady";
stateTransitionOnTimeout[9] = "Dead";

stateName[10] = "Dead";
stateTransitionOnLoaded[10] = "ActivateReady";

stateName[11] = "NoAmmo";
stateTransitionOnAmmo[11] = "Reload";
stateSequence[11] = "NoAmmo";
};
