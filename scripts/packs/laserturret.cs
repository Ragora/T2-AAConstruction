//--------------------------------------------------------------------------
// Datablocks
//--------------------------------------

datablock SensorData(LaserTurretSensor) {
	detects = true;
	detectsUsingLOS = true;
	detectsPassiveJammed = false;
	detectsActiveJammed = false;
	detectsCloaked = false;
	detectionPings = true;
	detectRadius = 100;
};

datablock TurretData(LaserDeployed) : TurretDamageProfile {
	className = DeployedTurret;
	shapeFile = "camera.dts";
 mass = 1;
	maxDamage = 0.5;
	destroyedLevel = 0.5;
	disabledLevel = 0.21;
	explosion      = SmallTurretExplosion;
	expDmgRadius = 5.0;
	expDamage = 0.25;
	expImpulse = 500.0;
	repairRate = 0;
	heatSignature = 0.0;
	deployedObject = true;
	thetaMin = 5;
	thetaMax = 145;
	thetaNull = 90;
	primaryAxis = zaxis;
	isShielded = true;
	energyPerDamagePoint = 30;
	maxEnergy = 100;
	rechargeRate = 0.15;
	barrel = DeployableLaserBarrel;
	canControl = true;
	cmdCategory = "DTactical";
	cmdIcon = CMDTurretIcon;
	cmdMiniIconName = "commander/MiniIcons/com_turret_grey";
	targetNameTag = 'Laser';
	targetTypeTag = 'Turret';
	sensorData = LaserTurretSensor;
	sensorRadius = LaserTurretSensor.detectRadius;
	sensorColor = "191 0 226";
	firstPersonOnly = true;
	renderWhenDestroyed = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = TurretDebrisSmall;
};

datablock TurretImageData(DeployableLaserBarrel) {
	shapeFile = "turret_muzzlepoint.dts";
	item = LaserTurretBarrel;
	//rotation = "0 0 0 0";
	offset = "0 0 0";
	projectile = NerfBolt;
	//projectileType = TargetProjectile;
	projectileType = LinearFlareProjectile;
	usesEnergy = true;
	fireEnergy = 2;
	minEnergy = 8;
	lightType = "WeaponFireLight";
	lightColor = "0.25 0.15 0.15 1.0";
	lightTime = "1000";
	lightRadius = "2";
	muzzleFlash = IndoorTurretMuzzleFlash;
	//deleteLastProjectile = true;
	// Turret parameters
	activationMS = 150;
	deactivateDelayMS = 300;
	thinkTimeMS = 150;
	degPerSecTheta = 580;
	degPerSecPhi = 960;
	attackRadius = 100;

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
	stateTimeoutValue[3] = 0.1;
	stateFire[3] = true;
	stateShockwave[3] = true;
	stateRecoil[3] = LightRecoil;
	stateAllowImageChange[3] = false;
	stateSequence[3] = "Fire";
	stateSound[3] = IBLFireSound;
	stateScript[3] = "onFire";

	stateName[4] ="Reload";
	stateTimeoutValue[4] = 0.05;
	stateAllowImageChange[4] = false;
	stateSequence[4] = "Reload";
	stateTransitionOnTimeout[4] = "Ready";
	stateTransitionOnNotLoaded[4] = "Deactivate";
	stateTransitionOnNoAmmo[4] = "NoAmmo";

	stateName[5] = "Deactivate";
	stateSequence[5] = "Activate";
	stateDirection[5] = false;
	stateTimeoutValue[5] = 0.1;
	stateTransitionOnLoaded[5] = "ActivateReady";
	stateTransitionOnTimeout[5] = "Dead";

	stateName[6] = "Dead";
	stateTransitionOnLoaded[6] = "ActivateReady";

	stateName[7] = "NoAmmo";
	stateTransitionOnAmmo[7] = "Reload";
	stateSequence[7] = "NoAmmo";
};

datablock ShapeBaseImageData(TurretLaserDeployableImage) {
 mass = 1;
	shapeFile = "pack_deploy_turreti.dts";
	item = TurretLaserDeployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = LaserDeployed;
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

datablock ItemData(TurretLaserDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "pack_deploy_turreti.dts";
 mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = false;
	image = TurretLaserDeployableImage;
	pickUpName = "a laser turret pack";
	emap = true;
};

//--------------------------------------------------------------------------
// Functions
//--------------------------------------

function TurretLaserDeployableImage::TestNoTerrainFound(%item) {
	// created to prevent console errors
}

function TurretLaserDeployableImage::TestNoInteriorFound(%item) {
	// created to prevent console errors
}

function TurretLaserDeployable::onPickup(%this, %obj, %shape, %amount) {
	//created to prevent console errors
}

function LaserDeployed::onDestroyed(%this, %obj, %prevState) {
	if (%obj.isRemoved)
		return;
	if ($Host::InvincibleDeployables != 1 || %obj.damageFailedDecon) {
		%obj.isRemoved = true;
		$TeamDeployedCount[%obj.team, TurretLaserDeployable]--;
		remDSurface(%obj);
		%obj.schedule(500, delete);
	}
	Parent::onDestroyed(%this, %obj, %prevState);

}
