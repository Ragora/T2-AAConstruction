//--------------------------------------------------------------------------
// Datablocks
//--------------------------------------

datablock AudioProfile(MissileRackTurretFireSound) {
	filename    = "";
	description = AudioDefault3d;
	preload     = true;
	effect      = MissileFireEffect;
};

datablock AudioProfile(MissileRackTurretFireSound2) {
	filename    = "fx/weapons/sniper_miss.wav";
	description = AudioDefault3d;
	preload     = true;
};

datablock SensorData(MissileRackTurretSensor) {
	detects = true;
	detectsUsingLOS = true;
	detectsPassiveJammed = false;
	detectsActiveJammed = false;
	detectsCloaked = false;
	detectionPings = true;
	detectRadius = 100;
};

//---------------------------------------------------------------------------
// Explosions
//---------------------------------------------------------------------------
datablock ExplosionData(MissileRackMissileExplosion) {
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
datablock SeekerProjectileData(MissileRackMissile) {
	casingShapeName     = "weapon_missile_casement.dts";
	projectileShapeName = "weapon_missile_projectile.dts";
	hasDamageRadius     = true;
	indirectDamage      = 0.2;
	damageRadius        = 4.0;
	radiusDamageType    = $DamageType::MissileTurret;
	kickBackStrength    = 1000;

	explosion           = "MissileRackMissileExplosion";
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

	lifetimeMS          = 6000;
	muzzleVelocity      = 10.0;
	maxVelocity         = 80.0;
	turningSpeed        = 110.0;
	acceleration        = 200.0;

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

datablock TurretData(MissileRackTurretDeployed) : TurretDamageProfile {
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
	barrel = DeployableMissileRackTurretBarrel;
	heatSignature = 0.0;

	canControl = true;
	cmdCategory = "DTactical";
	cmdIcon = CMDTurretIcon;
	cmdMiniIconName = "commander/MiniIcons/com_turret_grey";
	targetNameTag = 'Missile Rack';
	targetTypeTag = 'Turret';
	sensorData = MissileRackTurretSensor;
	sensorRadius = MissileRackTurretSensor.detectRadius;
	sensorColor = "191 0 226";

	firstPersonOnly = true;

	debrisShapeName = "debris_generic_small.dts";
	debris = TurretDebrisSmall;
	needsPower = true;
};

datablock TurretImageData(DeployableMissileRackTurretBarrel) {
	shapeFile = "stackable1s.dts";
	rotation = "-0.57735 0.57735 0.57735 120";
	offset = "0 -0.3 0";
	projectile = MissileRackMissile;
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
	degPerSecTheta    = 580;
	degPerSecPhi      = 1080;
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

datablock TurretImageData(DeployableMissileRackTurretBarrelR) {
	shapeFile = "stackable1s.dts";
	rotation = "-0.57735 0.57735 0.57735 120";
	offset = "0 -0.3 0.5";

};

datablock TurretImageData(DeployableMissileRackTurretBarrelL) {
	shapeFile = "stackable1s.dts";
	rotation = "-0.57735 0.57735 0.57735 120";
	offset = "0 -0.3 -0.5";
};

function DeployableMissileRackTurretBarrel::onMount(%this,%obj,%slot) {
	%obj.currentMuzzleSlot = 0;
	%obj.schedule(1000,"mountImage",DeployableMissileRackTurretBarrelR,1,true);
	%obj.schedule(1000,"mountImage",DeployableMissileRackTurretBarrelL,2,true);
}

// TODO - handle unmount

datablock ShapeBaseImageData(TurretMissileRackDeployableImage) {
 mass = 1;
	shapeFile = "pack_deploy_turreto.dts";
	item = TurretMissileRackDeployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = MissileRackTurretDeployed;
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

datablock ItemData(TurretMissileRackDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "pack_deploy_turreti.dts";
 mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = false;
	image = TurretMissileRackDeployableImage;
	pickUpName = "a missile rack turret pack";
	emap = true;
};

//--------------------------------------------------------------------------
// Functions
//--------------------------------------

function TurretMissileRackDeployableImage::TestNoTerrainFound(%item) {
	// created to prevent console errors
}

function TurretMissileRackDeployableImage::TestNoInteriorFound(%item) {
	// created to prevent console errors
}

function TurretMissileRackDeployable::onPickup(%this, %obj, %shape, %amount) {
	//created to prevent console errors
}

function TurretMissileRackDeployableImage::onDeploy(%item, %plyr, %slot) {
	%className = "Turret";

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

	return %deplObj;
}

function MissileRackTurretDeployed::onDestroyed(%this, %obj, %prevState) {
	if (%obj.isRemoved)
		return;
	if ($Host::InvincibleDeployables != 1 || %obj.damageFailedDecon) {
		%obj.isRemoved = true;
		$TeamDeployedCount[%obj.team, TurretMissileRackDeployable]--;
		remDSurface(%obj);
		%obj.schedule(500, delete);
	}
	Parent::onDestroyed(%this, %obj, %prevState);
}

function DeployableMissileRackTurretBarrel::onFire(%data,%obj,%slot) {
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

function MissileRackTurretDeployed::hasLOS(%data,%obj,%slot,%targetObj) {
	%start = %obj.getMuzzlePoint(%slot);
	%end = %targetObj.getWorldBoxCenter();
	%res = containerRayCast(%start,%end,-1,%obj);
	return firstWord(%res) == %targetObj;
}

function TurretMissileRackDeployableImage::onMount(%data, %obj, %node) {
	%obj.hasMissileRack = true; // set for missilerackcheck
	%obj.packSet = 0;
	displayPowerFreq(%obj);
}

function TurretMissileRackDeployableImage::onUnmount(%data, %obj, %node) {
	%obj.hasMissileRack = "";
	%obj.packSet = 0;
}
