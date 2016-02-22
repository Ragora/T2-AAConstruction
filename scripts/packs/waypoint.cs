datablock StaticShapeData(DeployedWaypoint) : StaticShapeDamageProfile
{
	className = "spine";
	shapeFile = "camera.dts";

	maxDamage      = 0.5;
	destroyedLevel = 0.5;
	disabledLevel  = 0.3;

	isShielded = true;
	energyPerDamagePoint = 240;
	maxEnergy = 50;
	rechargeRate = 0.25;

	explosion    = HandGrenadeExplosion;
	expDmgRadius = 3.0;
	expDamage    = 0.1;
	expImpulse   = 200.0;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSensorIcon;
	cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
	needsPower = true;
};

datablock ShapeBaseImageData(WaypointDeployableImage)
{
    mass = 10;
    emap = true;
    shapeFile = "stackable1s.dts";
    item = WaypointDeployable;
    mountPoint = 1;
    offset = "0 0 0";
    deployed = DeployedWaypoint;
    heatSignature = 0;
    collideable = 1;
    stateName[0] = "Idle";
    stateTransitionOnTriggerDown[0] = "Activate";

    stateName[1] = "Activate";
    stateScript[1] = "onActivate";
    stateTransitionOnTriggerUp[1] = "Idle";

    isLarge = true;
    maxDepSlope = 360;
    deploySound = ItemPickupSound;

    minDeployDis = 0.5;
    maxDeployDis = 5.0;
};


datablock ItemData(WaypointDeployable)
{
	className = Pack;
	catagory = "Deployables";
	shapeFile = "camera.dts";
    mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "WaypointDeployableImage";
	pickUpName = "a deployable waypoint";
	heatSignature = 0;
	emap = true;
};

function WaypointDeployableImage::testObjectTooClose(%item)
{
	return "";
}

function WaypointDeployableImage::testNoTerrainFound(%item)
{
	// don't check this for non-Landspike turret deployables
}

function WaypointDeployable::onPickup(%this, %obj, %shape, %amount)
{
	// created to prevent console errors
}

function WaypointDeployableImage::onDeploy(%item, %plyr, %slot)
{
	//Object
	%className = "StaticShape";

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
	if (!%scaleIsSet)
		%scale = vectorMultiply(%scale,1/4 SPC 1/3 SPC 2);

	%deplObj = new (%className)() {
		dataBlock = DeployedWaypoint;
		scale = "1 1 1";
	};

//////////////////////////Apply settings//////////////////////////////

	// [[Location]]:

	// exact:
	%deplObj.setTransform(%item.surfacePt SPC %rot);
 
    //add the Waypoint
    %deplObj.waypoint = new waypoint() {
    Datablock = WaypointMarker;
    Position = %deplObj.getPosition();
    Team = %plyr.client.team;
    Name = %plyr.client.namebase @ "'s Waypoint";
    };

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
 
    %plyr.unmountImage(%slot);
    %plyr.decInventory(%item.item, 1);

	// increment the team count for this deployed object
	$TeamDeployedCount[%plyr.team, %item.item]++;

	%deplObj.deploy();

	// Power object
	checkPowerObject(%deplObj);
 
    DeployedWaypoint.think(%deplObj);

	return %deplObj;
}

function DeployedWaypoint::onLoaded(%obj,%name,%team) //To allow WP saving.
{
%obj.waypoint.delete();

%obj.waypoint = new waypoint() {
Datablock = WaypointMarker;
Team = %team;
Position = %obj.getPosition();
Name = %name;
};
}

function DeployedWaypoint::think(%data,%obj) //<scroll> tag :)
{
 if (!IsObject(%obj))
 return;
 
 echo("LOL");
 
 %name = %obj.waypoint.name;
 
 if (strStr(%name,"<scroll>") != -1) //If it has <scroll> in it, we strip "scroll" and set the scroll var
 {
  %name = strReplace(%name,"<scroll>","");
  %wp = %obj.waypoint;
  
  %obj.waypoint = new waypoint() {
  Datablock = WaypointMarker;
  Team = %wp.team;
  Position = %obj.getPosition();
  Name = %name;
  };
  %wp.delete();
  %obj.scroll = true;
 }
 
 if (%obj.scroll)
 {
  %name = translateCharacters(%name,1);
  %wp = %obj.waypoint;

  %obj.waypoint = new waypoint() {
  Datablock = WaypointMarker;
  Team = %wp.team;
  Position = %obj.getPosition();
  Name = %name;
  };
  %obj.waypoint.schedule(501,"delete");
  %wp.delete();
 }
 
 %obj.schedule = %data.schedule(500,"think",%obj);
}

/////////////////////////////////////
function DeployedWaypoint::onDestroyed(%this, %obj, %prevState)
{
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
    %obj.waypoint.delete();
	Parent::onDestroyed(%this, %obj, %prevState);
	$TeamDeployedCount[%obj.team, spineDeployable]--;
	remDSurface(%obj);
	%obj.schedule(500, "delete");
	cascade(%obj);
	fireBallExplode(%obj,1);
}

function DeployedWaypoint::Disassemble(%data,%plyr,%obj)
{
%obj.waypoint.delete();
Parent::Disassemble(%data,%plyr,%obj);
}
