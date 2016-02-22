//--------------------------------------------------------------------------
// Projectile Turret
//--------------------------------------------------------------------------

datablock StaticShapeData(DeployedProjectileTurret) : StaticShapeDamageProfile {
	className = "projectileturret";
	shapeFile = "camera.dts";

	maxDamage      = 1.00;
	destroyedLevel = 1.00;
	disabledLevel  = 0.75;

	isShielded = true;
	energyPerDamagePoint = 30;
	maxEnergy = 100;
	rechargeRate = 0.05;

	explosion    = HandGrenadeExplosion;
	expDmgRadius = 1.0;
	expDamage    = 0.3;
	expImpulse   = 500;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSensorIcon;
	cmdMiniIconName = "commander/MiniIcons/com_switch_grey";
	targetNameTag = 'Projectile';
	targetTypeTag = 'Turret';
//	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
    needsPower = true;
	emap = true;
};

datablock ShapeBaseImageData(ProjectileDeployableImage) {
 mass = 1;
	emap = true;
	shapeFile = "stackable1s.dts";
	item = ProjectileDeployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = DeployedProjectileTurret;
	heatSignature = 0;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = true;
	maxDepSlope = 360;
	deploySound = ItemPickupSound;

	flatMinDeployDis = 0.25;
	flatMaxDeployDis = 5.0;

	minDeployDis = 2;
	maxDeployDis = 5;
};

datablock ItemData(ProjectileDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable1s.dts";
 mass = 1;

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.1 0.8 0.8 1.0";
	lightTime = "1000";
	lightRadius = "3";

	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 3;
	rotate = true;
	image = "ProjectileDeployableImage";
	pickUpName = "a projectile turret";
	heatSignature = 0;
	emap = true;
};

function ProjectileDeployableImage::testNoTerrainFound(%item) {
	// don't check this for non-Landspike turret deployables
}

function ProjectileDeployable::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

function ProjectileDeployableImage::onDeploy(%item, %plyr, %slot) {
	%className = "StaticShape";

	%playerVector = vectorNormalize(getWord(%plyr.getEyeVector(),1) SPC -1 * getWord(%plyr.getEyeVector(),0) SPC "0");
	%item.surfaceNrm2 = %playerVector;

	if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
		%item.surfaceNrm2 = %playerVector;
	else
		%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 -1"));

	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);

	%deplObj = new (%className)() {
		scale = "1 1 1";
		dataBlock = DeployedProjectileTurret;
        projectile = $packSetting["projectileTurret",%plyr.packSet];
		deployed = true;
	};
 
    %deplObj.deploy();

	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);

	// set team, owner, and handle
	%deplObj.team = %plyr.client.Team;
	%deplObj.setOwner(%plyr);

	// set power frequency
	%deplObj.powerFreq = %plyr.powerFreq;

	// set skin
	setTargetSkin(%deplObj.target,Game.getTeamSkin(%plyr.client.team));

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
 
 checkPowerObject(%deplObj);

	addDSurface(%item.surface,%deplObj);

	// take the deployable off the player's back and out of inventory
	%plyr.unmountImage(%slot);
	%plyr.decInventory(%item.item, 1);

	return %deplObj;
}

function DeployedProjectileTurret::onDestroyed(%this,%obj,%prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
	Parent::onDestroyed(%this, %obj, %prevState);
	$TeamDeployedCount[%obj.team, SwitchDeployable]--;
	remDSurface(%obj);
	%obj.schedule(500, "delete");
}

function ProjectileDeployableImage::onMount(%data,%obj,%node) {
	%obj.hasProjectile = true; // set for switchcheck
	%obj.packSet = 2;
	%obj.expertSet = 0;
	displayPowerFreq(%obj);
}

function ProjectileDeployableImage::onUnmount(%data,%obj,%node) {
	%obj.hasProjectile = "";
	%obj.packSet = 0;
	%obj.expertSet = 0;
}

function DeployedProjectileTurret::gainPower(%data,%obj)
{
    %data.think(%obj);
	Parent::onGainPowerEnabled(%data,%obj);
}

function DeployedProjectileTurret::think(%data,%obj)
{
 if (!isObject(%obj))
 return;
 
 if (%obj.powerCount == 1 && %obj.timeout)
 %obj.schedule = %data.schedule(%obj.timeout,"think",%obj);
 
 //if (%obj.spread)
  //  {
     %vec = %obj.getMuzzleVector(0);
      %x = (getRandom() - 0.5) * 2 * 3.1415926 * 0.002;
      %y = (getRandom() - 0.5) * 2 * 3.1415926 * 0.002;
      %z = (getRandom() - 0.5) * 2 * 3.1415926 * 0.022;
      %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
      %vector = MatrixMulVector(%mat, %vec);
   // }
  ///  else
   //%vector = %obj.getMuzzleVector(0);
   
   %initialPos = %obj.getMuzzlePoint(0);
   %nrm = %vector;
   %nrm2 = vectorNormalize(vectorCross("0 0 -1",%nrm));
   %nrm3 = vectorNormalize(vectorCross(%nrm2,%nrm));
  // %offsetVec = %data.muzzleSlotOffset[%obj.currentMuzzleSlot];
  %offsetVec = "0 0 1";

   %initialPos = vectorAdd(%initialPos,vectorScale(%nrm2,getWord(%offsetVec,0)));
   %initialPos = vectorAdd(%initialPos,vectorScale(%nrm,getWord(%offsetVec,1)));
   %initialPos = vectorAdd(%initialPos,vectorScale(%nrm3,getWord(%offsetVec,2)));

   %projectile = new (strReplace(%obj.projectile.getClassname(),"Data",""))()
    {
     Datablock = %obj.projectile;
     initialPosition = %initialPos;
     //initialDirection = %x SPC %y SPC %z;
     initiailDirection = %vector;
     sourceObject = %obj;
     sourceClient = %obj.owner;
     sourceSlot = 0;
    };
    AIGrenadeThrown(%projectile);
    missionCleanup.add(%projectile);

}

function DeployedProjectileTurret::losePower(%data,%obj) {
	Parent::onLosePowerDisabled(%data,%obj);
}

function scanProjectileDatabase(%tolerance)
{
 error("-- Projectile Turret Database Scan Initialized --");
 %count = DatablockGroup.getCount();
 error("Total datablocks to scan:" SPC %count);
 error("Projectiles with a damage radius of less than" SPC %tolerance SPC "will be allowed.");
 
 if ($packSettings["projectileTurret"] !$= "")
 {
 %settings = $packSettings["projectileTurret"];
  error("WARNING: "@%settings@" projectile rules have already been set! Rewriting ...");
  for (%i = 0; %i < %settings; %i++)
  {
   $packSetting["projectileTurret",%i] = "";
  }
 }
 $packSettings["projectileTurret"] = 0;
 
 if (%tolerance $= "")
 %tolerance = 8; //8 meters by default
 
 %allowed = 0;
 %disallowed = 0;
 %seekers = 0;
 %tracers = 0;
 %linear = 0;
 %grenades = 0;
 
 for (%i = 0; %i < %count; %i++)
 {
  %db = DatablockGroup.getObject(%i);
  %radius = %db.damageRadius;
  %class = %db.getClassName();
  
  switch$(%class)
  {
   case "SeekerProjectileData":
     if (%radius > %tolerance)
     %disallowed++;
     else
     {
      $packSetting["projectileTurret",%allowed] = %db.getName();
      %seekers++;
      %allowed++;
     }
     
   case "TracerProjectileData":
     if (%radius > %tolerance)
     %disallowed++;
     else
     {
      $packSetting["projectileTurret",%allowed] = %db.getName();
      %tracers++;
      %allowed++;
     }
    
   case "LinearProjectileData":
     if (%radius > %tolerance)
     %disallowed++;
     else
     {
      $packSetting["projectileTurret",%allowed] = %db.getName();
      %linear++;
      %allowed++;
     }

   case "GrenadeProjectileData":
   if (%radius > %tolerance)
     %disallowed++;
     else
     {
      $packSetting["projectileTurret",%allowed] = %db.getName();
      %grnades++;
      %allowed++;
     }
  }
 }
 $packSettings["projectileTurret"] = %allowed;
 error("Seeker Projectiles allowed:" SPC %seekers);
 error("Tracer Projectiles allowed:" SPC %tracers);
 error("Linear Projectiles allowed:" SPC %linear);
 error("Grenade Projectiles allowed:" SPC %grenades);
 error("Total Projectiles disallowed:" SPC %disallowed);
 error("Total projectiles allowed:" SPC %allowed);
 error("-- Projectile Turret Database Scan End --");
}
