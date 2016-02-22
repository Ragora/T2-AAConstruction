// Escape Pod Pack

datablock AudioProfile(EscapePodChargeSound) {
	filename = "fx/misc/cannonstart.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock AudioProfile(EscapePodBeepSound) {
	filename = "fx/misc/warning_beep.wav";
	description = AudioClosest3d;
	preload = true;
};

datablock AudioProfile(EscapePodLaunchSound) {
	filename = "fx/Bonuses/high-level4-blazing.wav";
	description = AudioBIGExplosion3d;
	preload = true;
};

datablock AudioProfile(EscapePodLaunchSound2) {
	filename = "fx/Bonuses/down_passback3_rocket.wav";
	description = AudioBIGExplosion3d;
	preload = true;
};

datablock AudioProfile(EscapePodReloadSound) {
	filename = "fx/powered/turret_heavy_reload.wav";
	description = AudioClose3d;
	preload = true;
};

datablock AudioProfile(EscapePodFadeSound) {
	filename = "fx/Bonuses/upward_perppass2_quark.wav";
	description = AudioClose3d;
	preload = true;
};

datablock StaticShapeData(DeployedEscapePod) : StaticShapeDamageProfile {
	className = "escapepod";
	shapeFile = "TR2weapon_mortar.dts";

	maxDamage      = 1;
	destroyedLevel = 1;
	disabledLevel  = 1;

	isShielded = true;
	energyPerDamagePoint = 75;
	maxEnergy = 50;
	rechargeRate = 0.35;

	explosion    = ShapeExplosion;
	expDmgRadius = 8.0;
	expDamage    = 0.4;
	expImpulse   = 1500;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSensorIcon;
	cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";
	targetNameTag = 'Deployed';
	targetTypeTag = 'Escape Pod';
	deployAmbientThread = false;
	debrisShapeName = "debris_generic.dts";
	debris = DeployableDebris;
	heatSignature = 1;
	emap = true;
};

datablock ShapeBaseImageData(EscapePodDeployableImage) {
 mass = 1;
	emap = true;
	shapeFile = "stackable1s.dts";
	item = EscapePodDeployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = DeployedEscapePod;
	heatSignature = 0;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = true;
	maxDepSlope = 360;
	deploySound = ItemPickupSound;

	minDeployDis = 0.25;
	maxDeployDis = 5;
};

datablock ItemData(EscapePodDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable1s.dts";
 mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 3;
	rotate = true;
	image = "EscapePodDeployableImage";
	pickUpName = "an escape pod pack";
	heatSignature = 0;
	emap = true;
};

function EscapePodDeployableImage::testNoTerrainFound(%item) {
	// don't check this for non-Landspike turret deployables
}

function EscapePodDeployable::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

function EscapePodDeployableImage::onDeploy(%item, %plyr, %slot) {
	%className = "StaticShape";

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");

	if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
		%item.surfaceNrm2 = %playerVector;
	else
		%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 1"));

	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);

	%deplObj = new (%className)() {
		dataBlock = DeployedEscapePod;
		scale = "4 2 4";
		impulse = $packSetting["escapepod",%plyr.packSet];
	};

	%deplObj.lTarget = new StaticShape () {
		datablock = DeployedLTarget;
		scale = vectorMultiply("1 3 0.25",1/4 SPC 1/3 SPC 2);
	};

	%deplObj.lTarget.lMain = %deplObj;

	// set orientation
	%deplObj.lTarget.setTransform(%item.surfacePt SPC %rot);
	adjustEscapePod(%deplObj);
	escapePodLoop(%deplObj);

	// set the recharge rate right away
	if (%deplObj.getDatablock().rechargeRate)
		%deplObj.setRechargeRate(%deplObj.getDatablock().rechargeRate);

	// set team, owner, and handle
	%deplObj.team = %plyr.client.Team;
	%deplObj.setOwner(%plyr);
	if (%deplObj.lTarget) {
		%deplObj.lTarget.team = %plyr.client.Team;
		%deplObj.lTarget.setOwner(%plyr);
	}

	// set the sensor group if it needs one
	if (%deplObj.getTarget() != -1)
		setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

	// place the deployable in the MissionCleanup/Deployables group (AI reasons)
	addToDeployGroup(%deplObj);
	if (%deplObj.lTarget)
		addToDeployGroup(%deplObj.lTarget);

	//let the AI know as well...
	AIDeployObject(%plyr.client, %deplObj);
	if (%deplObj.lTarget)
		AIDeployObject(%plyr.client,%deplObj.lTarget);

	// play the deploy sound
	serverplay3D(%item.deploySound, %deplObj.getTransform());

	// increment the team count for this deployed object
	$TeamDeployedCount[%plyr.team, %item.item]++;

	%deplObj.deploy();

	addDSurface(%item.surface,%deplObj);
	if (%deplObj.lTarget)
		addDSurface(%deplObj,%deplObj.lTarget);

	// take the deployable off the player's back and out of inventory
	%plyr.unmountImage(%slot);
	%plyr.decInventory(%item.item, 1);

	return %deplObj;
}

function DeployedEscapePod::onDestroyed(%this,%obj,%prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
	Parent::onDestroyed(%this,%obj,%prevState);
	$TeamDeployedCount[%obj.team, EscapePodDeployable]--;
	remDSurface(%obj);
	%obj.schedule(500, "delete");
	if (isObject(%obj.lTarget))
		%obj.lTarget.schedule(500, "delete");
	if (isObject(%obj.podVehicle))
		%obj.podVehicle.schedule(500,setDamageState,Destroyed);
}

function DeployedEscapePod::disassemble(%data,%plyr,%obj) {
	if (isObject(%obj.podVehicle))
		%obj.podVehicle.startFade(500,0,1);
		%obj.podVehicle.schedule(500,delete);
	disassemble(%data,%plyr,%obj);
}

function adjustEscapePod(%obj) {
	if (!isObject(%obj))
		return;
	%lTarget = %obj.lTarget;
	%pos = %lTarget.getPosition();
	%rot = %lTarget.getRotation();
	%nrm = realVec(%lTarget,"0 0 1");
	%obj.setTransform(vectorAdd(%pos,vectorScale(%nrm,1.25)) SPC %rot);
}

function escapePodLoop(%obj) {
	if (!isObject(%obj))
		return;
	if (%obj.getDamageState() !$= "Enabled")
		return;
	cancel(%obj.escapePodLoop);
	%lTarget = %obj.lTarget;
	%pos = %lTarget.getPosition();
	%nrm = realVec(%lTarget,"0 0 1");
	%nrm2 = realVec(%lTarget,"1 0 0");
	%epod = %obj.podVehicle;
	if (isObject(%epod)) {
		if (%epod.player) {
			%player = %epod.getMountedObject(0);
			if (isObject(%player)) {
				if (%player.getState() $= "Dead") {
					%remove = true;
					if (!%player.podFaded) {
						%player.startFade(2000,0,1);
						%player.podFaded = true;
					}
				}
			}
			else
				%remove = true;
		}
		else {
			// Remove when launched manually (no player)
			if (%epod.launched)
				%remove = true;
		}
		if (%remove == true)
			%epod.getDataBlock().fadeOutVehicle(%epod);
	}
	else {
		%epod = new HoverVehicle() {
			dataBlock = "EscapePodVehicle";
			scale = "1 1 1";
			team = %obj.team;
			mountable = "1";
			selfpower = "1";
		};
		%obj.podVehicle = %epod;
		setTargetSensorGroup(%epod.getTarget(),%epod.team);
		%epod.launcher = %obj;
		%epod.setFrozenState(true);
		%epod.setTransform(vectorAdd(%pos,vectorScale(%nrm,1)) SPC fullRot(%nrm,vectorScale(%nrm2,-1)));
		%obj.play3D(EscapePodReloadSound);
		%obj.stopThread($AmbientThread);
		%obj.setThreadDir($AmbientThread,0);
		%obj.playThread($AmbientThread,"recoil");
		%obj.schedule(1000,stopThread,$AmbientThread);
	}
	%obj.escapePodLoop = schedule(1000,0,escapePodLoop,%obj);
}

function EscapePodDeployableImage::onMount(%data, %obj, %node) {
	%obj.hasEscapePod = true; // set for podcheck
	%obj.packSet = 7;
	%obj.expertSet = 0;
}

function EscapePodDeployableImage::onUnmount(%data, %obj, %node) {
	%obj.packSet = 0;
	%obj.expertSet = 0;
	%obj.hasEscapePod = "";
}

function EscapePodVehicle::isBlocked(%data,%obj,%player) {
	%pos = %obj.getPosition();
	%startPos = vectorAdd(%pos,vectorScale(realVec(%obj,"0 -1 0"),2));
	%endPos = vectorAdd(%pos,vectorScale(realVec(%obj,"0 1 0"),2));
	%mask = $TypeMasks::VehicleObjectType | $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType | $TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::StaticObjectType | $TypeMasks::MoveableObjectType | $TypeMasks::DamagableItemObjectType;
	%res = containerRayCast(%startPos,%endPos,%mask,%obj);
	if (%res)
		return 1;
	return 0;
}

function EscapePodVehicle::playerMounted(%data, %obj, %player, %node) {
	if (%obj.player)
		return;
	%obj.player = %player;
	%obj.playerMountedTime = getSimTime();
//	commandToClient(%player.client, 'setHudMode', 'Pilot', "Hoverbike", %node);

	// update observers who are following this guy...
	if ( %player.client.observeCount > 0 )
		resetObserveFollow( %player.client, false );

	%obj.play3D(EscapePodBeepSound);
	%data.schedule(1000,actionThreadPlayer,%player,"scoutRoot");
	%obj.schedule(1500,play3D,EscapePodChargeSound);
	%data.schedule(3000,launchPod,%obj,%player,%node);
}

function EscapePodVehicle::actionThreadPlayer(%data,%player,%thread) {
	if (!isObject(%player))
		return;
	if (%player.getState() $= "Dead") {
		%player.startFade(2000,0,1);
		%player.podFaded = true;
	}
	else
		%player.setActionThread(%thread,true);
}

function EscapePodVehicle::launchPod(%data,%obj,%player,%node) {
	if (!isObject(%obj))
		return;
	%obj.launched = true;
	%obj.play3D(EscapePodLaunchSound);
	%obj.play3D(EscapePodLaunchSound2);
	%launcher = %obj.launcher;
	if (isObject(%launcher)) {
		%launcher.stopThread($AmbientThread);
		%launcher.setThreadDir($AmbientThread,1);
		%launcher.playThread($AmbientThread,"recoil");
		%launcher.schedule(1000,stopThread,$AmbientThread);
	}
	%obj.setFrozenState(false);
	%obj.applyImpulse(%obj.getPosition(),vectorScale(realVec(%obj,"0 1 0"),%launcher.impulse));
	for(%i = 0; %i < 10; %i++)
		%data.schedule((%i * %i) * 10,emitLaunchPuff,%obj);
}

function EscapePodVehicle::emitLaunchPuff(%data,%obj) {
	if (!isObject(%obj))
		return;
	%em = new ParticleEmissionDummy() {
		dataBlock = "defaultEmissionDummy";
		emitter = "VehicleLGESmokeEMitter";
		position = %obj.getPosition();
		scale = "1 1 1";
		velocity = "1";
	};
	MissionCleanup.add(%em);
	%em.schedule(1000,"delete");
}

//function EscapePodVehicle::onDestroyed(%data, %obj, %prevState) {
//	Parent::onDestroyed(%data, %obj, %prevState);
//}


//function EscapePodVehicle::playerDismounted(%data, %obj, %player) {
//	Parent::playerDismounted(%data, %obj, %player);
//}

function EscapePodVehicle::fadeOutVehicle(%data,%obj) {
	if (%obj.faded)
		return;
	%obj.faded = true;
	%obj.startFade(1000,4000,1);
	%obj.schedule(5000,delete);
	%obj.schedule(4000,play3D,EscapePodFadeSound);
}

function EscapePodVehicle::onAdd(%this, %obj) {
	Parent::onAdd(%this, %obj);
	setTargetSensorGroup(%obj.getTarget(),%obj.team);
}

function displayEscapePodBoostPower(%plyr) {
		%line = $packSetting["escapepod",%plyr.packSet];
		%max = $packSetting["escapepod",$packSettings["escapepod"]];
		%power = mAbs(mFloatLength(100 / %max * %line,1));
		bottomPrint(%plyr.client,"Escape Pod booster set to " @ %power @ "% power" ,2,1);
}
