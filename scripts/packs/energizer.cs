//--------------------------------------------------------------------------
// Deployable Energizer
//--------------------------------------

datablock StaticShapeData(DeployedEnergizer) : StaticShapeDamageProfile {
	className = "energizer";
	shapeFile = "deploy_sensor_motion.dts";

	maxDamage      = 0.5;
	destroyedLevel = 0.5;
	disabledLevel  = 0.3;

	isShielded = true;
	energyPerDamagePoint = 30;
	maxEnergy = 50;
	rechargeRate = 0.05;

	explosion = SatchelMainExplosion;
	underwaterExplosion = UnderwaterSatchelMainExplosion;

	expDmgRadius = 20.0;
	expDamage    = 1.25;
	expImpulse   = 1500.0;

	dynamicType = $TypeMasks::StationObjectType;
	renderWhenDestroyed = false;

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.1 0.1 0.8 1.0";
	lightTime = "100";
	lightRadius = "3";

	humSound = GeneratorHumSound;

	EnergizeOthers = true;
	EnergizeRadius = 35;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSwitchIcon;
	cmdMiniIconName = "commander/MiniIcons/com_switch_grey";
	targetNameTag = 'Deployed';
	targetTypeTag = 'Energizer';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
};

datablock ItemData(EnergizerLight) : StaticShapeDamageProfile {
	className = "energizerlight";
	shapeFile = "beacon.dts";

	maxDamage = 2.0;
	destroyedLevel = 2.0;
	disabledLevel = 2.0;

 mass = 1;
	elasticity = 0.1;
	friction = 0.9;

	collideable = 1;
	pickupRadius = 1;
	sticky = false;

	explosion    = HandGrenadeExplosion;
	expDmgRadius = 1.0;
	expDamage    = 0.1;
	expImpulse   = 200.0;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.8 0.1 0.1 1.0";
	lightTime = "100";
	lightRadius = "1";

	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
};

datablock ShapeBaseImageData(EnergizerDeployableImage) {
 mass = 1;
	shapeFile = "deploy_sensor_motion.dts";
	scale = 2.5 / 3.85 @ " " @ 2.5 / 3.1 @ " " @ 5 / 3.1;
	item = EnergizerDeployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = EnergizerDeployed;
	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.1 0.1 0.8 1.0";
	lightTime = "100";
	lightRadius = "3";

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

datablock ItemData(EnergizerDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "deploy_sensor_motion.dts";
	scale = 2.5 / 3.85 @ " " @ 2.5 / 3.1 @ " " @ 5 / 3.1;
 mass = 1;

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.1 0.1 0.8 1.0";
	lightTime = "500";
	lightRadius = "3";

	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = false;
	image = "EnergizerDeployableImage";
	pickUpName = "an energizer pack";
	emap = true;
};

//--------------------------------------------------------------------------
// Functions
//--------------------------------------

function EnergizerDeployableImage::testObjectTooClose(%item,%surfacePt,%plyr) {
	%mask =	($TypeMasks::VehicleObjectType		| $TypeMasks::MoveableObjectType	|
		$TypeMasks::StaticShapeObjectType	|
		$TypeMasks::ForceFieldObjectType	| $TypeMasks::ItemObjectType		|
		$TypeMasks::PlayerObjectType		| $TypeMasks::TurretObjectType);

	InitContainerRadiusSearch(%item.surfacePt,30,%mask);

	while ((%test = containerSearchNext()) != 0) {
		if (%test.team !$= "" && %test.team != %plyr.team)
			return %test;
	}
	Parent::testObjectTooClose(%item,%surfacePt,%plyr);
}

function DeployedEnergizer::disassemble(%data,%plyr,%hTgt) {
	%hTgt.l1.schedule(500, "delete");
	%hTgt.l2.schedule(500, "delete");
	%hTgt.l3.schedule(500, "delete");
	%hTgt.l4.schedule(500, "delete");
	disassemble(%data,%plyr,%hTgt);
}

function DeployedEnergizer::onDestroyed(%this, %obj, %prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
	Parent::onDestroyed(%this, %obj, %prevState);
	$TeamDeployedCount[%obj.team, EnergizerDeployable]--;
	remDSurface(%obj);
	%obj.schedule(500, "delete");
	%obj.l1.schedule(500, "delete");
	%obj.l2.schedule(500, "delete");
	%obj.l3.schedule(500, "delete");
	%obj.l4.schedule(500, "delete");

	RadiusExplosion(%obj, %obj.getWorldBoxCenter(), %obj.expDmgRadius, %obj.expDamage, %obj.expImpulse, %obj, $DamageType::Explosion);
	fireBallExplode(%obj,10);
}

function EnergizerDeployableImage::onDeploy(%item, %plyr, %slot) {
	//Object
	%className = "StaticShape";

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");

	if (%item.surfaceinher == 0) {
		if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
			%item.surfaceNrm2 = %playerVector;
		else
			%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 1"));
	}

	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);
	%scale = "5 5 5";

	%deplObj = new (%className)() {
		dataBlock = "DeployedEnergizer";
		scale = %scale;
	};

	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);

	//Object
	%className = "Item";
	%deplObj.l1 = new (%className)() {
		dataBlock = "EnergizerLight";
		position = vectorAdd(%item.surfacePt, "2 0 1");
		rotation = %rot;
		scale = "1 1 1";
	};

	%deplObj.l2 = new (%className)() {
		dataBlock = "EnergizerLight";
		position = vectorAdd(%item.surfacePt, "0 2 1");
		rotation = %rot;
		scale = "1 1 1";
	};

	%deplObj.l3 = new (%className)() {
		dataBlock = "EnergizerLight";
		position = vectorAdd(%item.surfacePt, "-2 0 1");
		rotation = %rot;
		scale = "1 1 1";
	};

	%deplObj.l4 = new (%className)() {
		dataBlock = "EnergizerLight";
		position = vectorAdd(%item.surfacePt, "0 -2 1");
		rotation = %rot;
		scale = "1 1 1";
	};

	%deplObj.team = %plyr.client.team;
	%deplObj.setOwner(%plyr);
	%deplObj.l1.team = %plyr.client.team;
	%deplObj.l1.setOwner(%plyr);
	%deplObj.l2.team = %plyr.client.team;
	%deplObj.l2.setOwner(%plyr);
	%deplObj.l3.team = %plyr.client.team;
	%deplObj.l3.setOwner(%plyr);
	%deplObj.l4.team = %plyr.client.team;
	%deplObj.l4.setOwner(%plyr);
	addDSurface(%item.surface,%deplObj);

	if (%deplObj.getTarget() != -1)
		setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);
	if (%deplObj.l1.getTarget() != -1)
		setTargetSensorGroup(%deplObj.l1.getTarget(), %plyr.client.team);
	if (%deplObj.l2.getTarget() != -1)
		setTargetSensorGroup(%deplObj.l1.getTarget(), %plyr.client.team);
	if (%deplObj.l3.getTarget() != -1)
		setTargetSensorGroup(%deplObj.l1.getTarget(), %plyr.client.team);
	if (%deplObj.l4.getTarget() != -1)
		setTargetSensorGroup(%deplObj.l1.getTarget(), %plyr.client.team);

	addToDeployGroup(%deplObj);
	addToDeployGroup(%deplObj.l1);
	addToDeployGroup(%deplObj.l2);
	addToDeployGroup(%deplObj.l3);
	addToDeployGroup(%deplObj.l4);

	AIDeployObject(%plyr.client, %deplObj);
	AIDeployObject(%plyr.client, %deplObj.l1);
	AIDeployObject(%plyr.client, %deplObj.l2);
	AIDeployObject(%plyr.client, %deplObj.l3);
	AIDeployObject(%plyr.client, %deplObj.l4);

	serverPlay3D(%item.deploySound, %deplObj.getTransform());
	$TeamDeployedCount[%plyr.team, %item.item]++;
	%deplObj.deploy();

	%deplObj.playThread($AmbientThread, "ambient");

	%plyr.unmountImage(%slot);
	%plyr.decInventory(%item.item, 1);

	return %deplObj;
}

function EnergizerDeployable::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

function EnergizeLoop() {
	if (!isObject(MissionCleanup)) {
		schedule(10000, 0, "StartEnergizeLoop");
		return;
	}

	%dep = nameToID("MissionCleanup/Deployables");
	%clCount = ClientGroup.getCount();
	%depCount = %dep.getCount();
	for (%i=0;%i<%clCount;%i++) {
		ClientGroup.getObject(%i).shouldEnergize = 0;
	}
	for (%i=0;%i<%depCount;%i++) {
		%obj = %dep.getObject(%i);
		if (isObject(%obj)) {
			%data = %obj.getDataBlock();
			if (%data) {
				if (%data.energizeOthers && %obj.isEnabled()) {
					%pos = posFromTransform(%obj.getTransform());
					for(%i2=0;%i2<%clCount;%i2++) {
						%obj2 = ClientGroup.getObject(%i2).player;
						if ((%obj2) && (%obj2 != %obj) && (%obj2.team == %obj.team)) {
							%pos2 = %obj2.getPosition();
							if (vectorDist(%pos, %pos2) < %data.EnergizeRadius && %obj2.getArmorSize() $= "Heavy")
								%obj2.client.shouldEnergize = 1;
						}
					}
				}
			}
		}
	}
	for (%i=0;%i<%clCount;%i++) {
		%client = ClientGroup.getObject(%i);
		%obj = %client.player;
		if (isObject(%obj)) {
			if ((%client.shouldEnergize) && (!%obj.energized)) {
				%obj.energized = 1;
				if (!%obj.hasEnergizer)
					%obj.setRechargeRate(%obj.getRechargeRate() + 0.5);
				messageClient(%obj.client, 'msgClient', '\c2Entering energizer coverage.');
			}
			else if (!%client.shouldEnergize && %obj.energized) {
				%obj.energized = "";
				if (%obj.hasEnergizer)
					messageClient(%obj.client, 'msgClient', '\c2Leaving energizer coverage, switching to energizer pack.');
				else {
					%obj.setRechargeRate(%obj.getRechargeRate() - 0.5);
					messageClient(%obj.client, 'msgClient', '\c2Leaving energizer coverage, switching to armor power.');
				}
			}
		}
	}
	for (%i=0;%i<%depCount;%i++) {
		%obj = %dep.getObject(%i);
		if (isObject(%obj)) {
			%data = %obj.getDataBlock();
			if (%data.energizeOthers) {
				if (%obj.energized)
					%obj.energized = "";
			}
			else if ((%obj.shouldEnergize) && (!%obj.energized)) {
				%obj.energized = 1;
				%obj.setRechargeRate(%obj.getRechargeRate() + 0.5);
			}
			else if ((!%obj.shouldEnergize) && (%obj.energized)) {
				%obj.energized = "";
				%obj.setRechargeRate(%obj.getRechargeRate() - 0.5);
			}
		}
	}
	schedule(1000, 0, "EnergizeLoop");
}

function StartEnergizeLoop() {
	$EnergizeLoop = 1;
	if (!isObject(MissionCleanup)) {
		schedule(10000, 0, "StartEnergizeLoop");
		return;
	}
	%depGroup = nameToID("MissionCleanup/Deployables");
	if (%depGroup <= 0) {
		%depGroup = new SimGroup("Deployables");
		MissionCleanup.add(%depGroup);
	}
	EnergizeLoop();
}

function EnergizerDeployableImage::onMount(%data, %obj, %node) {
	%obj.hasEnergizer = true;
	if (!%obj.energized)
		%obj.setRechargeRate(%obj.getRechargeRate() + 0.5);
}

function EnergizerDeployableImage::onUnmount(%data, %obj, %node) {
	%obj.hasEnergizer = "";
	if (!%obj.energized)
		%obj.setRechargeRate(%obj.getRechargeRate() - 0.5);
}
