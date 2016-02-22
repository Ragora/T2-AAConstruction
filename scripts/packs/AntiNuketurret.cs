datablock TracerProjectileData(Mpm_Aexp1) {
	className = "TracerProjectileData";
	emitterDelay = "-1";
	velInheritFactor = "0";
	directDamage = "0";
	hasDamageRadius = "0";
	indirectDamage = "0";
	damageRadius = "0";
	radiusDamageType = "0";
	kickBackStrength = "0";
	Explosion = "VehicleExplosion";
	hasLight = "0";
	lightRadius = "1";
	lightColor = "1.000000 1.000000 1.000000 1.000000";
	hasLightUnderwaterColor = "0";
	underWaterLightColor = "1.000000 1.000000 1.000000 1.000000";
	explodeOnWaterImpact = "0";
	depthTolerance = "5";
	bubbleEmitTime = "0.5";
	faceViewer = "0";
	scale = "1 1 1";
	dryVelocity = "0.1";
	wetVelocity = "0.1";
	fizzleTimeMS = "32";
	lifetimeMS = "32";
	explodeOnDeath = "1";
	reflectOnWaterImpactAngle = "0";
	deflectionOnWaterImpact = "0";
	fizzleUnderwaterMS = "-1";
	activateDelayMS = "-1";
	doDynamicClientHits = "0";
	tracerLength = "1";
	tracerMinPixels = "1";
	tracerAlpha = "0";
	tracerColor = "0.000000 0.000000 0.000000 0.000000";
	tracerTex[0] = "special/tracer00";
	tracerTex[1] = "special/tracercross";
	tracerWidth = "0.1";
	crossViewAng = "0.99";
	crossSize = "0.1";
	renderCross = "0";
		isFXUnit = "1";
};

datablock TracerProjectileData(Mpm_Aexp2) : Mpm_Aexp1{
Explosion = "TurretExplosion";
};



//---------------------------------------------------------------------------
// Explosions
//---------------------------------------------------------------------------
datablock ExplosionData(Mpm_Anti_MissileExplosion) {
	explosionShape = "effect_plasma_explosion.dts";
	playSpeed = 1.5;
	soundProfile   = GrenadeExplosionSound;
	faceViewer = true;

	sizes[0] = "0.5 0.5 0.5";
	sizes[1] = "0.5 0.5 0.5";
	sizes[2] = "0.5 0.5 0.5";

	emitter[0] = MissileExplosionSmokeEmitter;

	debris = MissileSpikeDebris;
	debrisThetaMin = 10;
	debrisThetaMax = 170;
	debrisNum = 8;
	debrisNumVariance = 6;
	debrisVelocity = 15.0;
	debrisVelocityVariance = 2.0;

	shakeCamera = true;
	camShakeFreq = "6.0 7.0 7.0";
	camShakeAmp = "70.0 70.0 70.0";
	camShakeDuration = 1.0;
	camShakeRadius = 7.0;
};

//--------------------------------------------------------------------------
// Projectile
//--------------------------------------
datablock SeekerProjectileData(Mpm_Anti_Missile) {
	casingShapeName     = "weapon_missile_casement.dts";
	projectileShapeName = "weapon_missile_projectile.dts";
	hasDamageRadius     = true;
	indirectDamage      = 0.2;
	damageRadius        = 4.0;
	radiusDamageType    = $DamageType::MissileTurret;
	kickBackStrength    = 1000;

	explosion           = "Mpm_Anti_MissileExplosion";
	splash              = MissileSplash;
	velInheritFactor    = 0.2;	// to compensate for slow starting velocity, this value
					// is cranked up to full so the missile doesn't start
					// out behind the player when the player is moving
					// very quickly - bramage

	baseEmitter         = MissileSmokeEmitter;
	delayEmitter        = MissileFireEmitter;
	puffEmitter         = MissilePuffEmitter;
	bubbleEmitter       = GrenadeBubbleEmitter;
	bubbleEmitTime      = 1.0;

	exhaustEmitter      = MissileLauncherExhaustEmitter;
	exhaustTimeMs       = 300;
	exhaustNodeName     = "muzzlePoint1";

	lifetimeMS          = -1;
	muzzleVelocity      = 20.0;
	maxVelocity         = 20.0;
	turningSpeed        = 110.0;
	acceleration        = 0.0;

	proximityRadius     = 3;

	terrainAvoidanceSpeed         = 180;
	terrainScanAhead              = 25;
	terrainHeightFail             = 12;
	terrainAvoidanceRadius        = 100;

	flareDistance = 200;
	flareAngle    = 30;

	sound = MissileProjectileSound;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "0.2 0.05 0";

	useFlechette = true;
	flechetteDelayMs = 550;
	casingDeb = FlechetteDebris;

	explodeOnWaterImpact = false;
};

datablock TurretData(Mpm_Anti_TurretDeployed) : TurretDamageProfile {
	className = DeployedTurret;
	shapeFile = "turret_outdoor_deploy.dts";

	rechargeRate = 0.15;

 mass = 1;
	maxDamage = 0.80;
	destroyedLevel = 0.80;
	disabledLevel = 0.35;

	explosion	= HandGrenadeExplosion;
	expDmgRadius	= 5.0;
	expDamage	= 0.5;
	expImpulse	= 500.0;

	repairRate = 0;
	deployedObject = true;

	thetaMin = 0;
	thetaMax = 145;
	thetaNull = 90;
	primaryAxis = zaxis;

	yawVariance          = 30.0; // these will smooth out the elf tracking code.
	pitchVariance        = 30.0; // more or less just tolerances

	isShielded = true;
	energyPerDamagePoint = 110;
	maxEnergy = 80;
	renderWhenDestroyed = true;
	barrel = DeployableMpm_Anti_TurretBarrel;
	heatSignature = 0.0;

	//canControl = true;
	cmdCategory = "DTactical";
	cmdIcon = CMDTurretIcon;
	cmdMiniIconName = "commander/MiniIcons/com_turret_grey";
	targetNameTag = 'Anti Missile';
	targetTypeTag = 'Turret';
	sensorData = Mpm_Anti_TurretSensor;
	sensorRadius = Mpm_Anti_TurretSensor.detectRadius;
	sensorColor = "191 0 226";

	firstPersonOnly = true;

	debrisShapeName = "debris_generic_small.dts";
	debris = TurretDebrisSmall;
	needsPower = true;
};

datablock TurretImageData(DeployableMpm_Anti_TurretBarrel) {
	shapeFile = "stackable1s.dts";
	rotation = "-0.57735 0.57735 0.57735 120";
	offset = "0 -0.3 0";
	projectile = Mpm_Anti_Missile;
	projectileType = SeekerProjectile;

	usesEnergy = true;
	fireEnergy = 7.0;
	minEnergy = 7.0 * 2;

	isSeeker     = true;
	seekRadius   = 300;
	maxSeekAngle = 30;
	seekTime     = 1.0;
	minSeekHeat  = 0.6;
	emap = true;
	minTargetingDistance = 15;

	// Turret parameters
	activationMS      = 250;
	deactivateDelayMS = 500;
	thinkTimeMS       = 200;
	degPerSecTheta    = 50;
	degPerSecPhi      = 50;
	attackRadius      = 250;

	// State transitions
	stateName[0] = "Activate";
	stateTransitionOnNotLoaded[0] = "Dead";
	stateTransitionOnLoaded[0] = "ActivateReady";

	stateName[1] = "ActivateReady";
	stateSequence[1] = "Activate";
	stateSound[1] = IBLSwitchSound;

	stateTimeoutValue[1] = 1;
	stateTransitionOnTimeout[1] = "Ready";
	stateTransitionOnNotLoaded[1] = "Deactivate";
	stateTransitionOnNoAmmo[1] = "NoAmmo";

	stateName[2] = "Ready";
	stateTransitionOnNotLoaded[2] = "Deactivate";
	stateTransitionOnTriggerDown[2] = "Fire";
	stateTransitionOnNoAmmo[2] = "NoAmmo";

	stateName[3] = "Fire";
	stateTransitionOnTimeout[3] = "Reload";
	stateTimeoutValue[3] = 0.3;
	stateFire[3] = true;
	stateShockwave[3] = true;
	stateRecoil[3] = LightRecoil;
	stateAllowImageChange[3] = false;
	stateSequence[3] = "Fire";
	stateSound[3] = MissileRackTurretFireSound;
	stateScript[3] = "onFire";

	stateName[4] ="Reload";
	stateTimeoutValue[4] = 0.5;
	stateAllowImageChange[4] = false;
	stateSequence[4] = "Reload";
	stateTransitionOnTimeout[4] = "Ready";
	stateTransitionOnNotLoaded[4] = "Deactivate";
	stateTransitionOnNoAmmo[4] = "NoAmmo";

	stateName[5] = "Deactivate";
	stateSequence[5] = "Activate";
	stateDirection[5] = false;
	stateTimeoutValue[5] = 2;
	stateTransitionOnLoaded[5] = "ActivateReady";
	stateTransitionOnTimeout[5] = "Dead";

	stateName[6] = "Dead";
	stateTransitionOnLoaded[6] = "ActivateReady";

	stateName[7] = "NoAmmo";
	stateTransitionOnAmmo[7] = "Reload";
	stateSequence[7] = "NoAmmo";

	muzzleSlots = 12;
	muzzleSlotOffset[0] = "0.65 0.5 0.4";
	muzzleSlotOffset[1] = "0.35 0.5 0.4";
	muzzleSlotOffset[2] = "0.15 0.5 0.4";
	muzzleSlotOffset[3] = "-0.15 0.5 0.4";
	muzzleSlotOffset[4] = "-0.35 0.5 0.4";
	muzzleSlotOffset[5] = "-0.65 0.5 0.4";
	muzzleSlotOffset[6] = "0.65 0.5 0.1";
	muzzleSlotOffset[7] = "0.35 0.5 0.1";
	muzzleSlotOffset[8] = "0.15 0.5 0.1";
	muzzleSlotOffset[9] = "-0.15 0.5 0.1";
	muzzleSlotOffset[10] = "-0.35 0.5 0.1";
	muzzleSlotOffset[11] = "-0.65 0.5 0.1";
};

datablock TurretImageData(DeployableMpm_Anti_TurretBarrel1) {
	shapeFile = "weapon_missile_projectile.dts";
	rotation = "1 0 0 0";
	offset = "-0.14 0.15 0.13";

};

datablock TurretImageData(DeployableMpm_Anti_TurretBarrel2) {
	shapeFile = "weapon_missile_projectile.dts";
	rotation = "1 0 0 0";
	offset = "-0.14 0.15 -0.13";
};

datablock TurretImageData(DeployableMpm_Anti_TurretBarrel3) {
	shapeFile = "weapon_missile_projectile.dts";
	rotation = "1 0 0 0";
	offset = "-0.43 0.15 0.13";
};

datablock TurretImageData(DeployableMpm_Anti_TurretBarrel4) {
	shapeFile = "weapon_missile_projectile.dts";
	rotation = "1 0 0 0";
	offset = "-0.43 0.15 -0.13";
};

function DeployableMpm_Anti_TurretBarrel::onMount(%this,%obj,%slot) {
	%obj.currentMuzzleSlot = 0;
	%obj.schedule(1000,"mountImage",DeployableMpm_Anti_TurretBarrel1,1,true);
	%obj.schedule(1000,"mountImage",DeployableMpm_Anti_TurretBarrel2,2,true);
	%obj.schedule(1000,"mountImage",DeployableMpm_Anti_TurretBarrel3,3,true);
	%obj.schedule(1000,"mountImage",DeployableMpm_Anti_TurretBarrel4,4,true);
}

// TODO - handle unmount

datablock ShapeBaseImageData(TurretMpm_Anti_DeployableImage) {
 mass = 1;
	shapeFile = "pack_deploy_turreto.dts";
	item = TurretMpm_Anti_Deployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = Mpm_Anti_TurretDeployed;
	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = true;
	emap = true;
	maxDepSlope = 360;
	deploySound = TurretDeploySound;
	minDeployDis =  0.5;
	maxDeployDis =  5.0;
};

datablock ItemData(TurretMpm_Anti_Deployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "pack_deploy_turreti.dts";
 mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = false;
	image = TurretMpm_Anti_DeployableImage;
	pickUpName = "an anti missile turret pack";
	emap = true;
};

//--------------------------------------------------------------------------
// Functions
//--------------------------------------

function TurretMpm_Anti_DeployableImage::TestNoTerrainFound(%item) {
	// created to prevent console errors
}

function TurretMpm_Anti_DeployableImage::TestNoInteriorFound(%item) {
	// created to prevent console errors
}

function TurretMpm_Anti_Deployable::onPickup(%this, %obj, %shape, %amount) {
	//created to prevent console errors
}


function TurretMpm_Anti_DeployableImage::onDeploy(%item, %plyr, %slot) {
	%className = "Turret";
        if (IsObject(%item.surface))
           if (%item.surface.getDatablock().getName() $= Mpm_Anti_TurretDeployed)
               {
               for (%i=1;%i<5;%i++)
                   {
                   if (!%item.surface.getMountedImage(%i)) 
                       {
                       %item.surface.schedule(%i*100,"mountImage","DeployableMpm_Anti_TurretBarrel"@ %i,%i,true);
		       %item.surface.schedule(%i*100,"play3d",NerfGunDryFireSound);
                       %c++;
                       }
                       if (%c)
	                  bottomPrint( %plyr.client, "Reloaded anti-mpm Turret with" SPC %c SPC "Missiles", 5,1);
                   }
               return "";
               }
	%playerVector = vectorNormalize(getWord(%plyr.getEyeVector(),1) SPC -1 * getWord(%plyr.getEyeVector(),0) SPC "0");

	if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
		%item.surfaceNrm2 = %playerVector;
	else
		%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 1"));

	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);

	%deplObj = new (%className)() {
		dataBlock = %item.deployed;
	};

	if (%plyr.packSet == 1)
		%deplObj.isSeeker = true;

	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);

	// set team, owner, and handle
	%deplObj.team = %plyr.client.Team;
	%deplObj.setOwner(%plyr);

	// set power frequency
	%deplObj.powerFreq = %plyr.powerFreq;

	// set the sensor group if it needs one
	if (%deplObj.getTarget() != -1)
		setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

	// place the deployable in the MissionCleanup/Deployables group (AI reasons)
	addToDeployGroup(%deplObj);

	//let the AI know as well...
	AIDeployObject(%plyr.client, %deplObj);

	// play the deploy sound
	serverPlay3D(%item.deploySound, %deplObj.getTransform());

	// increment the team count for this deployed object
	$TeamDeployedCount[%plyr.team, %item.item]++;

	%deplObj.deploy();

	// Power object
	checkPowerObject(%deplObj);

	addDSurface(%item.surface,%deplObj);

	// take the deployable off the player's back and out of inventory
	%plyr.unmountImage(%slot);
	%plyr.decInventory(%item.item, 1);
        bottomPrint( %plyr.client, "Deployed anti-mpm Turret with 4 missiles ammo.\n deploy another anti-mpm turret ontop to resuply.", 5,2);
	return %deplObj;
}

function Mpm_Anti_TurretDeployed::onDestroyed(%this, %obj, %prevState) {
	if (%obj.isRemoved)
		return;
	if ($Host::InvincibleDeployables != 1 || %obj.damageFailedDecon) {
		%obj.isRemoved = true;
		$TeamDeployedCount[%obj.team, TurretMpm_Anti_Deployable]--;
		remDSurface(%obj);
		%obj.schedule(500, delete);
	}
	Parent::onDestroyed(%this, %obj, %prevState);
}

function DeployableMpm_Anti_TurretBarrel::onFire(%data,%obj,%slot) {
	%targetObj = %obj.getTargetObject();
	if (%targetObj) {
		if (!%obj.getDataBlock().hasLOS(%obj,%slot,%targetObj) && %obj.aquireTime + 2000 < getSimTime()) {
			%obj.clearTarget();
			return;
		}
		if (%obj.aquireTime + 10000 + getRandom(0,1000) < getSimTime()) {
			%obj.clearTarget();
			return;
		}
	}

	%p = Parent::onFire(%data,%obj,%slot);
	serverPlay3D(MissileRackTurretFireSound2,%obj.getTransform());

	if (%obj.isSeeker) {
		if (%obj.getControllingClient())
			// a player is controlling the turret
			%target = %obj.getLockedTarget();
		else
			// The ai is controlling the turret
			%target = %obj.getTargetObject();

		if(%target)
			%p.setObjectTarget(%target);
		else if(%obj.isLocked())
			%p.setPositionTarget(%obj.getLockedPosition());
		else
			%p.setNoTarget(); // set as unguided. Only happens when itchy trigger can't wait for lock tone.
		%obj.setEnergyLevel(%obj.getEnergyLevel() - (%data.fireEnergy));
	}
}

function Mpm_Anti_TurretDeployed::hasLOS(%data,%obj,%slot,%targetObj) {
	%start = %obj.getMuzzlePoint(%slot);
	%end = %targetObj.getWorldBoxCenter();
	%res = containerRayCast(%start,%end,-1,%obj);
	return firstWord(%res) == %targetObj;
}

function TurretMpm_Anti_DeployableImage::onMount(%data, %obj, %node) {
	%obj.hasMpm_Anti= true; // set for Mpm_Anti_check
	%obj.packSet = 0;
	displayPowerFreq(%obj);
}

function TurretMpm_Anti_DeployableImage::onUnmount(%data, %obj, %node) {
	%obj.hasMpm_Anti= "";
	%obj.packSet = 0;
}


function Mpm_Anti_TurretDeployed::selectTarget(%this, %turret)
{
%turretTarg = %turret.getTarget();
  if(%turretTarg == -1)
    return;
if (Isobject(%turret.aimtarget))
	{
	%turret.setTargetObject(%turret.aimtarget);
        }
%target = %turret.get_ampm_target();
if (Isobject(%target))
   {
   %turret.Lock_ampm_target(%target);
   }
}

function GameBase::Get_ampm_target(%obj)
{
%location = %obj.getTransform();
if (!Isobject(mpm_missiles))
		return "";
if (!mpm_missiles.getCount())
		return "";

%tmissile = "";
for( %c = 0; %c < mpm_missiles.getCount(); %c++ ) 
	{
	%missile = mpm_missiles.getObject(%c);        
	if (%missile.load.Hazard(%missile,%obj,200) && !Isobject(%obj.tagged[%missile]) && !%missile.tagged && (!isObject(%missile.tracking) || %obj == %missile.tracking))
			{	
		%pos = pos(%missile);
		%dist = vectorDist(%location,%pos);
		if (!%dis || %dist < %dis) 
			{
			%tmissile = %missile;
			%dis = %dist;
			}
		}
	}
return %tmissile SPC %dist;
}

function GameBase::Lock_ampm_target(%obj,%target)
{
if (IsObject(%target)) //Is the target still there?
	{
        %predict= %target.predict();
        %loc = getWords(%predict,0,2);
        %speed = getWords(%predict,3,5);
	%pos = %obj.getTransform();
        %dist = VectorDist(%pos,%loc);
        //%dir = VectorSub(%pos,%loc);
        //%gendir =  VectorDot(VectorNormalize(%dir),VectorNormalize(%speed));
	%time = 15;//Limit(%dist/20+5-10,5,50); //%target.radiustime(%pos,100);          
	if (%time > 5 && getSimTime()+ %time*1000 < %target.dietime) //Can we get to the target in time?
		{                                 
		%loc = getWords(%target.predict((%time)*1000),0,2);
                %dist = VectorDist(%pos,%loc);
                %ttime = %dist / 20;		//travel time
                %ltime = %time - %ttime;	//launch time
                %atime = %ltime - 3;		//activate time                                               
                if (%dist > 10 && %dist < 500 && %atime > 0) //Will it within our limits?
	                {
			%res = containerRayCast(%obj.getTransform(),%loc, -1,%obj);                        
			if (!%res) //Can we hit it from here?
				{
				if ((%obj.atime-getSimTime())/1000 > %atime || (%obj.ltime-getSimTime())/1000 < 0) //Is it a better option?
					{                                        
					Cancel(%obj.activate);
					Cancel(%obj.launch);
					%obj.needtime = getSimTime()+(%time*1000);
	                                %obj.atime = getSimTime()+(%atime*1000);
        	                        %obj.ltime = getSimTime()+(%ltime*1000);
					%obj.set_ampm_target(%loc);
                                        %target.tracking = %obj;
                        		%obj.activate = %obj.schedule(%atime*1000,"set_ampm_target",%target,%loc);
                        		%obj.launch = %obj.schedule(%ltime*1000,"fire_ampm_now");                                        
					}
				}
			}
		}
	}
}

function GameBase::set_ampm_target(%obj,%target,%location)
{
if (!Isobject(%obj.aimtarget))
   {
   //SIGN
   %sign = new StaticShape(){
   dataBlock = MpmTurretTarg;
   };
   %sign.team = 3;
   %sign.setHeat(1);
   setTargetSensorGroup(%sign.getTarget(),3);
   %sign.owner = %obj;
   %obj.aimtarget = %sign;
   }
if (%location $= "" || !Isobject(%target) || IsObject(%obj.tagged[%target]) || !%obj.get_ampm_missile())
   {
   Cancel(%obj.launch);
   %pos = VectorAdd(%obj.getMuzzlePoint(0),realvec(%obj,"0 10 0"));
   %obj.aimtarget.setTransform(%pos SPC "1 0 0 0");
   %obj.canfire = 0;
   }
else
   {      
   %obj.aimtarget.setTransform(%location SPC "1 0 0 0");   
   %obj.target = %target;
   %obj.canfire = 1;
   }
}

function GameBase::Fire_ampm_now(%obj)
{

%target = %obj.target;
if (IsObject(%target) && %obj.canfire && IsObject(%obj.aimtarget))
   {
   %slot  = %obj.get_ampm_missile();
   %from = %obj.getMuzzlePoint(%slot);
   %pos = %obj.aimtarget.getTransform();
   %vec = VectorSub(%pos,%from);
   %tdir = VectorNormalize(VectorSub(%pos,%from));
   %tvec = "0 1 0";
   %rot = %obj.getSlotRotation(0);
   %dir = validateVal(MatrixMulVector("0 0 0" SPC %rot ,%tvec));
   %diff = vectorDot(%tdir,%dir);
   %time = (%obj.needtime-getSimTime())/1000;
   %speed = (VectorLen(%vec)/%time)/MpmMissile3.muzzleVelocity;

   if (%diff > 0.9 && %slot)
      {
      %p1 = new SeekerProjectile() 
          {
          datablock = Mpm_B_MIS2;
          initialDirection = VectorScale(%tdir,%speed);
          initialPosition  = %from;
          };    
      %p1.schedule(%time*1000+500,"delete");
      schedule(%time*1000,0,"range",%target,%p1);
      %p1.getDatablock().schedule(%time*1000,"onExplode",%p1,%pos, 1);
      %obj.tagged[%target] = %p1;
      %target.tagged = 1;
      %obj.set_ampm_target(0);
      %obj.unMountImage(%slot);
      }
   }
}



function range(%p1,%p2)
{
if (IsObject(%p1) && IsObject(%p2))
{
%dist = VectorDist(%p1.getTransform(),%p2.getTransform());
PlayExplosion(%p1.getTransform(),Mpm_Aexp1);

if (%dist < 10)
	{
	//createLifeEmitter(%p1.getTransform(), MpmJetEmitter3, 5000,"1 0 0 0");
	PlayExplosion(%p1.getTransform(),Mpm_Aexp2);
	%p1.load.InterCept(%p1);
	}
}
}

function GameBase::get_ampm_missile(%obj)
{
for (%i=1;%i<5;	%i++)
                   {
                   if (%obj.getMountedImage(%i)!=0) 
                      return %i; 
                   }
return "";
}

//Not used.. ha
function inrange(%p,%loc,%range)
{
if (!Isobject(%p)) 
   return "";
%c = VectorSub(%p.getTransform(),%loc);
%speed = GetWords(%p.predict(),3,5);
%r = VectorNormalize(%speed);
%v = VectorLen(%speed);
%a = %p.getDatablock().Acceleration;

if (%a != 0)
   {
   %root1 = mPow(VectorDot(%r,%c),2) * VectorDot(%r,%r) * (mPow(%g,2)-VectorDot(%c,%c));
   if (%root1 < 0) //Will never be in range.
      return "";
   %root2 = VectorDot(%r,%r)*(VectorDot(%r,%r)*mPow(%v,2)-2*%a*(VectorDot(%r,%c)+mSqrt(%root)));
   if (%root2 < 0) //Will never be in range.
      return "";
   %time = (-1*%v + mSqrt(%root2)/VectorDot(%r,%r))/%a;
   if (%time < 0) //'was'in range.
      return "";
   return %time; //Note this is seconds not ms.
   }
else
   {
   %root = mPow(%v)*(4*VectorDot(%r,%c) - 4*VectorDot(%r,%r)*(VectorDot(%c,%c)-mPow(%g,2)));
   if (%root < 0) //Will never be in range.
      return "";
   %time = (-2*%v*VectorDot(%r,%c) + mSqrt(%root)) / (2*VectorDot(%r,%r) * mPow(%v,2));
   if (%time < 0) //'was'in range.
      return "";
   return %time; //Note this is seconds not ms.
   }
}

