//--------------------------------------------------------------------------
// Datablocks Disc turret
//--------------------------------------

datablock SensorData(DiscTurretSensor)
{
detects = true;
detectsUsingLOS = true;
detectsPassiveJammed = false;
detectsActiveJammed = false;
detectsCloaked = false;
detectionPings = true;
detectRadius = 60;
};

datablock TurretData(DiscTurretDeployed) : TurretDamageProfile
{
className = DeployedTurret;
shapeFile = "turret_outdoor_deploy.dts";
mass = 1;
maxDamage = 1.5; //was 0.5;
destroyedLevel = 0.5;
disabledLevel = 0.21;
explosion = SmallTurretExplosion;
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
maxEnergy = 50;
rechargeRate = 0.20; //was 0.10;
barrel = DeployableDiscTurretBarrel;
canControl = true;
cmdCategory = "DTactical";
cmdIcon = CMDTurretIcon;
cmdMiniIconName = "commander/MiniIcons/com_turret_grey";
targetNameTag = 'Disc';
targetTypeTag = 'Turret';
sensorData = DiscTurretSensor;
sensorRadius = DiscTurretSensor.detectRadius;
sensorColor = "191 0 226";
firstPersonOnly = true;
renderWhenDestroyed = true;
debrisShapeName = "debris_generic_small.dts";
debris = TurretDebrisSmall;
};

datablock TurretImageData(DeployableDiscTurretBarrel)
{
shapeFile = "weapon_disc.dts";
item = DiscTurretBarrel;
rotation = "0 1 0 90";
//rotation = "0 0 0 0";
offset = "0 0 0";
projectile = DiscProjectile;
projectileType = LinearProjectile;
usesEnergy = true;
fireEnergy = 10; ///was 20
minEnergy = 10; ///was 20
lightType = "WeaponFireLight";
lightColor = "0.25 0.15 0.15 1.0";
lightTime = "1000";
lightRadius = "2";
muzzleFlash = IndoorTurretMuzzleFlash;

// Turret parameters
activationMS = 150;
deactivateDelayMS = 300;
thinkTimeMS = 150;
degPerSecTheta = 580;
degPerSecPhi = 960;
attackRadius = 150; //100

// State Data
stateName[0] = "Preactivate";
stateTransitionOnLoaded[0] = "Activate";
stateTransitionOnNoAmmo[0] = "NoAmmo";

stateName[1] = "Activate";
stateTransitionOnTimeout[1] = "Ready";
stateTimeoutValue[1] = 0.05;
stateSequence[1] = "Activated";
stateSound[1] = DiscSwitchSound;

stateName[2] = "Ready";
stateTransitionOnNoAmmo[2] = "NoAmmo";
stateTransitionOnTriggerDown[2] = "Fire";
stateSequence[2] = "DiscSpin";
stateSound[2] = DiscLoopSound;

stateName[3] = "Fire";
stateTransitionOnTimeout[3] = "Reload";
stateTimeoutValue[3] = 0.025;
stateFire[3] = true;
stateRecoil[3] = LightRecoil;
stateAllowImageChange[3] = false;
stateSequence[3] = "Fire";
stateScript[3] = "onFire";
stateSound[3] = DiscFireSound;

stateName[4] = "Reload";
stateTransitionOnNoAmmo[4] = "NoAmmo";
stateTransitionOnTimeout[4] = "Ready";
stateTimeoutValue[4] = 0.1; // 0.25 load, 0.25 spinup
stateAllowImageChange[4] = false;
stateSequence[4] = "Reload";
stateSound[4] = DiscReloadSound;

stateName[5] = "NoAmmo";
stateTransitionOnAmmo[5] = "Reload";
stateSequence[5] = "NoAmmo";
stateTransitionOnTriggerDown[5] = "DryFire";

stateName[6] = "DryFire";
//stateSound[6] = DiscDryFireSound;
stateTimeoutValue[6] = 0.1;
stateTransitionOnTimeout[6] = "NoAmmo";

};
datablock ShapeBaseImageData(DiscTurretDeployableImage)
{
mass = 1;
shapeFile = "pack_deploy_turreti.dts";
item = DiscTurretDeployable;
mountPoint = 1;
offset = "0 0 0";
deployed = DiscTurretDeployed;
stateName[0] = "Idle";
stateTransitionOnTriggerDown[0] = "Activate";

stateName[1] = "Activate";
stateScript[1] = "onActivate";
stateTransitionOnTriggerUp[1] = "Idle";

isLarge = true;
emap = true;
maxDepSlope = 360;
deploySound = TurretDeploySound;
minDeployDis = 0.5;
maxDeployDis = 5.0;
};

datablock ItemData(DiscTurretDeployable)
{
className = Pack;
catagory = "Deployables";
shapeFile = "pack_deploy_turreti.dts";
mass = 1;
elasticity = 0.2;
friction = 0.6;
pickupRadius = 1;
rotate = false;
image = "DiscTurretDeployableImage";
pickUpName = "a disc turret pack";
emap = true;
};

//--------------------------------------------------------------------------
// Functions
//--------------------------------------

function DiscTurretDeployableImage::testNoTerrainFound(%item)
{
// created to prevent console errors
}

function DiscTurretDeployableImage::testNoInteriorFound(%item)
{
// created to prevent console errors
}

function DiscTurretDeployableImage::onDeploy(%item, %plyr, %slot)
{
%searchRange = 5.0;
%mask = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType;
%eyeVec = %plyr.getEyeVector();
%eyeTrans = %plyr.getEyeTransform();
%eyePos = posFromTransform(%eyeTrans);
%nEyeVec = VectorNormalize(%eyeVec);
%scEyeVec = VectorScale(%nEyeVec, %searchRange);
%eyeEnd = VectorAdd(%eyePos, %scEyeVec);
//%searchResult = containerRayCast(%eyePos, %eyeEnd, %mask, 0);
//if(!%searchResult ) {
//messageClient(%plyr.client, 'MsgBeaconNoSurface', 'c2Cannot place turret. You are too far from surface.');
//return 0;
//}
%terrPt = %item.surfacept;
%terrNrm = %item.surfacenrm;
%intAngle = getTerrainAngle(%terrNrm);
%rotAxis = vectorNormalize(vectorCross(%terrNrm, "0 0 1"));
if ((getWord(%terrNrm, 2) == 1) || (getWord(%terrNrm, 2) == -1))
%rotAxis = vectorNormalize(vectorCross(%terrNrm, "0 1 0"));
%rotation = %rotAxis @ " " @ %intAngle;
%plyr.unmountImage(%slot);
%plyr.decInventory(%item.item, 1);

%deplObj = new Turret() {
dataBlock = %item.deployed;
position = VectorAdd(%terrPt, VectorScale(%terrNrm, 0.03));
rotation = %rotation;
};
	addDSurface(%item.surface,%deplObj);
%deplObj.setRechargeRate(%deplObj.getDatablock().rechargeRate);
%deplObj.team = %plyr.client.team;
	%deplObj.setOwner(%plyr);
%deplObj.setOwnerClient(%plyr.client);
if(%deplObj.getTarget() != -1)
setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

addToDeployGroup(%deplObj);
AIDeployObject(%plyr.client, %deplObj);
serverPlay3D(%item.deploySound, %deplObj.getTransform());
$TeamDeployedCount[%plyr.team, %item.item]++;
%deplObj.deploy();
%deplObj.playThread($AmbientThread, "ambient");
return %deplObj;
}

function DiscTurretDeployable::onPickup(%this, %obj, %shape, %amount)
{
// created to prevent console errors
}

function DeployableDiscTurretBarrel::onFire( %data, %obj, %slot ) {
	%energy = %obj.getEnergyLevel();
	%p = new (LinearProjectile)() {
		dataBlock = DiscProjectile;
		initialDirection = %obj.getMuzzleVector(%slot);
		initialPosition = %obj.getMuzzlePoint(%slot);
		sourceObject = %obj;
		damageFactor = 1;
		sourceSlot = %slot;
	};
	%obj.lastProjectile = %p;
	MissionCleanup.add(%p);
	%obj.setEnergyLevel(%energy - %data.fireEnergy);
}

function DiscTurretDeployed::onDestroyed(%this, %obj, %prevState) {
	if (%obj.isRemoved)
		return;
	if ($Host::InvincibleDeployables != 1 || %obj.damageFailedDecon) {
		%obj.isRemoved = true;
		$TeamDeployedCount[%obj.team, DiscTurretDeployable]--;
		remDSurface(%obj);
		%obj.schedule(500, "delete");
	}
	Parent::onDestroyed(%this, %obj, %prevState);
}

