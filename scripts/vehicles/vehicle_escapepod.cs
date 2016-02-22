// Escape Pod

datablock HoverVehicleData(EscapePodVehicle) : WildcatDamageProfile {
	spawnOffset = "0 0 1";
	canControl = false;
	floatingGravMag = 2; // 3.5;

	catagory = "Vehicles";
	shapeFile = "vehicle_grav_scout.dts";
	computeCRC = true;

	debrisShapeName = "vehicle_grav_scout_debris.dts";
	debris = ShapeDebris;
	renderWhenDestroyed = false;

	drag = 0.0;
	density = 0.9;

	mountPose[0] = sitting;
	cameraMaxDist = 5.0;
	cameraOffset = 0.7;
	cameraLag = 0.5;
	numMountPoints = 1;
	isProtectedMountPoint[0] = true;
	explosion = VehicleExplosion;
	explosionDamage = 0; // 0.5;
	explosionRadius = 0; // 5.0;

	lightOnly = 0;

	maxDamage = 0.60;
	destroyedLevel = 0.60;

	isShielded = true;
	rechargeRate = 0.7;
	energyPerDamagePoint = 75;
	maxEnergy = 150;
	minJetEnergy = 160; // disable?
	jetEnergyDrain = 1.3;

	// Rigid Body
	mass =  100;
	bodyFriction = 0.1;
	bodyRestitution = 0.5;
	softImpactSpeed = 20;		 // Play SoftImpact Sound
	hardImpactSpeed = 28;		// Play HardImpact Sound

	// Ground Impact Damage (uses DamageType::Ground)
	minImpactSpeed = 29;
	speedDamageScale = 0.010;

	// Object Impact Damage (uses DamageType::Impact)
	collDamageThresholdVel	= 23;
	collDamageMultiplier	= 0.030;

	dragForce		= 25 / 45.0;
	vertFactor		= 0.0;
	floatingThrustFactor	= 0.001; // 0.35;

	mainThrustForce		= 0.001;
	reverseThrustForce	= 0.001;
	strafeThrustForce	= 0.001;
	turboFactor		= 0.001;

	brakingForce = 25;
	brakingActivationSpeed = 20;

	stabLenMin = 2.25;
	stabLenMax = 3.75;
	stabSpringConstant  = 30;
	stabDampingConstant = 16;

	gyroDrag = 16;
	normalForce = 10;
	restorativeForce = 5;
	steeringForce = 0.001;
	rollForce  = 0.001;
	pitchForce = 0.001;

	dustEmitter = VehicleLiftoffDustEmitter;
	triggerDustHeight = 2.5;
	dustHeight = 1.0;
	dustTrailEmitter = TireEmitter;
	dustTrailOffset = "0.0 -1.0 0.5";
	triggerTrailHeight = 3.6;
	dustTrailFreqMod = 15.0;

//	jetSound	= ScoutSqueelSound;
//	engineSound	= ScoutEngineSound;
//	floatSound	= ScoutThrustSound;
	softImpactSound	= GravSoftImpactSound;
	hardImpactSound	= HardImpactSound;

	//
	softSplashSoundVelocity = 10.0;
	mediumSplashSoundVelocity = 20.0;
	hardSplashSoundVelocity = 30.0;
	exitSplashSoundVelocity = 10.0;

	exitingWater		= VehicleExitWaterSoftSound;
	impactWaterEasy	= VehicleImpactWaterSoftSound;
	impactWaterMedium = VehicleImpactWaterSoftSound;
	impactWaterHard	= VehicleImpactWaterMediumSound;
	waterWakeSound	 = VehicleWakeSoftSplashSound;

	minMountDist = 4;

	damageEmitter[0] = SmallLightDamageSmoke;
	damageEmitter[1] = SmallHeavyDamageSmoke;
	damageEmitter[2] = DamageBubbles;
	damageEmitterOffset[0] = "0.0 -1.5 0.5 ";
	damageLevelTolerance[0] = 0.3;
	damageLevelTolerance[1] = 0.7;
	numDmgEmitterAreas = 1;

	splashEmitter[0] = VehicleFoamDropletsEmitter;
	splashEmitter[1] = VehicleFoamEmitter;

	shieldImpact = VehicleShieldImpact;

//	forwardJetEmitter = WildcatJetEmitter;

	cmdCategory = Tactical;
	cmdIcon = CMDHoverScoutIcon;
	cmdMiniIconName = "commander/MiniIcons/com_landscout_grey";
	targetNameTag = 'Escape';
	targetTypeTag = 'Pod';
	sensorData = VehiclePulseSensor;

	checkRadius = 1.7785;
	observeParameters = "1 10 10";

	runningLight[0] = WildcatLight1;
	runningLight[1] = WildcatLight2;
	runningLight[2] = WildcatLight3;

	shieldEffectScale = "0.9375 1.125 0.6";
};
