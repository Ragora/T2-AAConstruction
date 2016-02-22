//---------------------------------------------------------
// Window Deployable
// Idea by Exerpt
// Code by Dark Dragon DX
//---------------------------------------------------------

datablock ShapeBaseImageData(windowDeployableImage) {
 mass = 1;
	emap = true;
	shapeFile = "ammo_chaingun.dts";
	item = windowDeployable;
	mountPoint = 1;
	offset = "-0.2 -0.125 0";
    rotation = "0 -1 0 90";
	deployed = DeployedWindow;
	heatSignature = 0;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = false;
	maxDepSlope = 360;
	deploySound = ItemPickupSound;

	minDeployDis = 0.1;
	maxDeployDis = 50.0;
};

datablock ItemData(windowDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable1s.dts";
    mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "windowDeployableImage";
	pickUpName = "a window pack";
	heatSignature = 0;
	emap = true;
};

function windowDeployableImage::testObjectTooClose(%item) {
	return "";
}

function windowDeployableImage::testNoTerrainFound(%item) {
	// don't check this for non-Landspike turret deployables
}

function windowDeployable::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

function windowDeployableImage::onDeploy(%item, %plyr, %slot) {
	//Object
	%className = "ForceFieldBare";

	%grounded = 0;
	if (%item.surface.getClassName() $= TerrainBlock)
		%grounded = 1;

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");

	if (%item.surfaceinher == 0) {
		if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
			%item.surfaceNrm2 = %playerVector;
		else
			%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 1"));
	}

	%item.surfaceNrm3 = vectorCross(%item.surfaceNrm,%item.surfaceNrm2);
 
    %rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);
	%scale = getWords($packSetting["forcefield",%plyr.packSet],0,2);
	%mCenter = "-0.5 -0.5 -0.5";
	%pad = pad(%item.surfacePt SPC %item.surfaceNrm SPC %item.surfaceNrm2,%scale,%mCenter);
	%scale = getWords(%pad,0,2);
	%item.surfacePt = getWords(%pad,3,5);
	%rot = getWords(%pad,6,9);

	// Add padding
	%padSize = 0.01;
	%scale = vectorAdd(%scale,%padSize * 2 SPC %padSize * 2 SPC -%padSize * 2);
	%item.surfacePt = vectorSub(%item.surfacePt,vectorScale(vectorNormalize(%item.surfaceNrm),%padSize));
	%item.surfacePt = vectorSub(%item.surfacePt,vectorScale(vectorNormalize(%item.surfaceNrm2),%padSize));
	%item.surfacePt = vectorSub(%item.surfacePt,vectorScale(vectorNormalize(vectorCross(%item.surfaceNrm,%item.surfaceNrm2)),-%padSize));
 
    %datablock = "DeployedForceField" @ %plyr.packSet;

	%deplObj = new (%className)() {
		dataBlock = %dataBlock;
        selfPowered = true;
		scale = %scale;
	};

    schedule(1,0,"setSelfPowered",%deplObj);

//////////////////////////Apply settings//////////////////////////////

	// [[Location]]:

	// exact:
	%deplObj.setTransform(%item.surfacePt SPC %rot);

	// misc info
	addDSurface(%item.surface,%deplObj);

	// [[Settings]]:

	%deplObj.grounded = %grounded;
	%deplObj.needsFit = 1;

	// [[Normal Stuff]]:

//	if(%deplObj.getDatablock().rechargeRate)
//		%deplObj.setRechargeRate(%deplObj.getDatablock().rechargeRate);

	// set team, owner, and handle
	%deplObj.team = %plyr.client.team;
	%deplObj.setOwner(%plyr);

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
	$TeamDeployedCount[%plyr.team, "forceFieldDeployable"]++;
 
	return %deplObj;
}

function setSelfPowered(%obj)
{
 %obj.setSelfPowered();
}

/////////////////////////////////////

function windowDeployableImage::onMount(%data, %obj, %node) {
	%obj.hasWindow = true; // set for spinecheck
	%obj.packSet = 0;
	displayPowerFreq(%obj);
}

function windowDeployableImage::onUnmount(%data, %obj, %node) {
	%obj.hasWindow = "";
	%obj.packSet = 0;
}
