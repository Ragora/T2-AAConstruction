
//Modifier Tool v001
//Parts by Electricutioner and CaptnPower

//Variables:
$ElecMod::MergeTool::Tolerance = 0.05; //how many meters of tolerance do we give the pieces that we merge.
$ElecMod::MergeTool::Timeout = 2; //how many seconds until a selection times out when using the tool

datablock AudioProfile(chsingleshot)
{
   filename    = "fx/vehicles/tank_chaingun.wav";
   description = AudioDefault3d;
   preload = true;
   effect = AssaultChaingunFireEffect;
};
//Functions:
function MTMerge(%client, %piece1, %piece2)
{
	if (!MTCheckCompatability(%client, %piece1, %piece2))
	{
		%piece1.setCloaked(false);
		%piece2.setCloaked(false);
		MTClearClientSelection();
		return;
	}
	%piece1.setCloaked(true);
	%piece2.setCloaked(true);
	schedule(100, 0, "MTScaleShiftMerge", %piece1, %piece2);

	if (isEventPending(%client.mergeschedule))
	{
		cancel(%client.mergeschedule);
		MTClearClientSelection();
	}

}

//This function checks if 4 corners of the objects are compatable.
function MTCheckCompatability(%client, %piece1, %piece2)
{
	//do the pieces exist?
	if (!isObject(%piece1) || !isObject(%piece2))
	{
		messageClient(%client, 'MsgClient', "\c2MT: A piece appears to be missing.");
		return;
	}
	//check if the owners are the same
	if (%piece1.owner != %client || %piece2.owner != %client)
	{
		//with an exemption of admins
		if (!%client.isAdmin)
		{
			if (%piece1.ownerGUID != %client.guid || %piece2.ownerGUID != %client.guid)
			{
				messageClient(%client, 'MsgClient', "\c2MT: One or more of those pieces do not belong to you.");
				return;
			}
		}
	}

	//now we need to determine if at least 4 of the pieces axies match
	%pos1[0] = %piece1.getEdge("1 1 -1");
	%pos1[1] = %piece1.getEdge("-1 1 -1");
	%pos1[2] = %piece1.getEdge("1 -1 -1");
	%pos1[3] = %piece1.getEdge("-1 -1 -1");
	%pos1[4] = %piece1.getEdge("1 1 1");
	%pos1[5] = %piece1.getEdge("-1 1 1");
	%pos1[6] = %piece1.getEdge("1 -1 1");
	%pos1[7] = %piece1.getEdge("-1 -1 1");

	%pos2[0] = %piece2.getEdge("1 1 -1");
	%pos2[1] = %piece2.getEdge("-1 1 -1");
	%pos2[2] = %piece2.getEdge("1 -1 -1");
	%pos2[3] = %piece2.getEdge("-1 -1 -1");
	%pos2[4] = %piece2.getEdge("1 1 1");
	%pos2[5] = %piece2.getEdge("-1 1 1");
	%pos2[6] = %piece2.getEdge("1 -1 1");
	%pos2[7] = %piece2.getEdge("-1 -1 1");
	//then we compare them to see which ones match
	%k = 0;
	for (%i = 0; %i < 8; %i++)
	{
		for (%j = 0; %j < 8; %j++)
		{
			if ($ElecMod::MergeTool::Tolerance >= vectorDist(%pos1[%i], %pos2[%j]))
			{
				%k++;
			}
		}
	}
	//if less then 4 match, they can't be compatable (if more then 4 match... something odd is going on)
	if (%k < 4)
	{
		if (%k == 0)
			messageClient(%client, 'MsgClient', "\c2MT: None of the corners are shared on those objects. Cannot merge.");
		else
			messageClient(%client, 'MsgClient', "\c2MT: Only " @ %k @ " corners of the required 4 are shared.");

		return;
	}
	//if the check survived that, we continue...
	return 1;
}

//this function, after the pieces are confirmed, checks to see which of the 6 sides is in contact, refers to another
//function to find the alter side, determines distance between them to find a new "real" scale, and determines the
//new world box center for the object. Piece 1 is resized and moved, piece 2 is deconstructed.
function MTScaleShiftMerge(%piece1, %piece2)
{
	//find which axis is touching for a "this is the side we scale" discovery
	//table:
	//0: X
	//1: -X
	//2: Y
	//3: -Y
	//4: Z
	//5: -Z

	%p1S[0] = %piece1.getEdge("1 0 0");
	%p1S[1] = %piece1.getEdge("-1 0 0");
	%p1S[2] = %piece1.getEdge("0 1 0");
	%p1S[3] = %piece1.getEdge("0 -1 0");
	%p1S[4] = %piece1.getEdge("0 0 1");
	%p1S[5] = %piece1.getEdge("0 0 -1");

	%p2S[0] = %piece2.getEdge("1 0 0");
	%p2S[1] = %piece2.getEdge("-1 0 0");
	%p2S[2] = %piece2.getEdge("0 1 0");
	%p2S[3] = %piece2.getEdge("0 -1 0");
	%p2S[4] = %piece2.getEdge("0 0 1");
	%p2S[5] = %piece2.getEdge("0 0 -1");

	for (%i = 0; %i < 6; %i++)
	{
		for (%j = 0; %j < 8; %j++)
		{
			if ($ElecMod::MergeTool::Tolerance >= vectorDist(%p1S[%i], %p2S[%j]))
			{
				%side1 = %i;
				%side2 = %j;
			}
		}
	}
	//echo("Sides:" SPC %i SPC %j);
	//at this point %side1/2 will contain one of the numbers in the table above
	//we get the non-shared side at this point
	%ops1 = MTFindOpSide(%side1);
	%ops2 = MTFindOpSide(%side2);

	//this variable contains the new axis length that we are scaling on...
	%newaxis = VectorDist(%p1S[%ops1], %p2S[%ops2]);
	%currsize = %piece1.getRealSize();

	if (%side1 == 0 || %side1 == 1)
		%axis = "x";
	if (%side1 == 2 || %side1 == 3)
		%axis = "y";
	if (%side1 == 4 || %side1 == 5)
		%axis = "z";

	//echo("Axis:" SPC %axis);
	if (%axis $= "x")
		%piece1.setRealSize(%newaxis SPC getWords(%currsize, 1, 2));
	if (%axis $= "y")
		%piece1.setRealSize(getWord(%currsize, 0) SPC %newaxis SPC getWord(%currsize, 2));
	if (%axis $= "z")
		%piece1.setRealSize(getWords(%currsize, 0, 1) SPC %newaxis);
	if (%axis !$= "x" && %axis !$= "y" && %axis !$= "z")
	{
		error("MT: A scaling error has occured.");
		return;
	}
	%newpos = VectorScale(VectorAdd(%p1S[%ops1], %p2S[%ops2]), 0.5);
	%piece1.SetWorldBoxCenter(%newpos);

	%piece1.setCloaked(false);
	%piece2.setCloaked(false);

	//%piece2.delete(); //deleting is bad
	disassemble(0, %piece2.owner, %piece2); //disassemble is cleaner
}

//this function does something very simple, it finds whether a number is even or odd, and then adds or subracts
//and returns the initial input with that modification. I can't imagine where else this could be useful.
function MTFindOpSide(%side)
{
	%evencheck = %side / 2;
	if (%evencheck == mFloor(%evencheck))
		%even = 1;
	else
		%even = 0;

	if (%even)
		return (%side + 1);
	else
		return (%side - 1);
}

//simply clears a client variable... woohoo...
function MTClearClientSelection(%client)
{
	%client.mergePiece1 = "";
}


//"weapon" datablocks and such
datablock ItemData(MergeTool)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_sniper.dts";
   image        = MergeToolImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a merge tool";

   computeCRC = true;

};

datablock ShapeBaseImageData(MergeToolImage)
{
	className = WeaponImage;
	shapeFile = "weapon_sniper.dts";
	item = MergeTool;

	usesEnergy = true;
	minEnergy = 0;

	stateName[0]                     = "Activate";
	stateTransitionOnTimeout[0]      = "ActivateReady";
	stateSound[0]                    = SniperRifleSwitchSound;
	stateTimeoutValue[0]             = 0.1;
	stateSequence[0]                 = "Activate";

	stateName[1]                     = "ActivateReady";
	stateTransitionOnLoaded[1]       = "Ready";
	stateTransitionOnNoAmmo[1]       = "NoAmmo";

	stateName[2]                     = "Ready";
	stateTransitionOnNoAmmo[2]       = "NoAmmo";
	stateTransitionOnTriggerDown[2]  = "CheckWet";

	stateName[3]                     = "Fire";
	stateTransitionOnTimeout[3]      = "Reload";
	stateTimeoutValue[3]             = 0.2; //reload timeout here
	stateFire[3]                     = true;
	stateAllowImageChange[3]         = false;
	stateSequence[3]                 = "Fire";
	stateScript[3]                   = "onFire";

	stateName[4]                     = "Reload";
	stateTransitionOnTimeout[4]      = "Ready";
	stateTimeoutValue[4]             = 0.1;
	stateAllowImageChange[4]         = false;

	stateName[5]                     = "CheckWet";
	stateTransitionOnWet[5]          = "Fire";
	stateTransitionOnNotWet[5]       = "Fire";

	stateName[6]                     = "NoAmmo";
	stateTransitionOnAmmo[6]         = "Reload";
	stateTransitionOnTriggerDown[6]  = "DryFire";
	stateSequence[6]                 = "NoAmmo";

	stateName[7]                     = "DryFire";
	stateSound[7]                    = SniperRifleDryFireSound;
	stateTimeoutValue[7]             = 0.1;
	stateTransitionOnTimeout[7]      = "Ready";
};
 //modified below for calling of the modes
function MergeToolImage::onFire(%data,%obj,%slot)
{
	serverPlay3D(chsingleshot, %obj.getTransform());
	%client = %obj.client;

if (%obj.modifierMode==5){
     %client.scaler=getword($WeaponSetting["modifier4",%client.player.modifierMode2],0);
     messageclient(%client, 'MsgClient', "\c2Modifier Tool [Scaler] set to" SPC %client.scaler);
     return;
     }
     
	%pos = %obj.getMuzzlePoint($WeaponSlot);
	%vec = %obj.getMuzzleVector($WeaponSlot);
	%targetpos = VectorAdd(%pos, VectorScale(%vec, 200));
	%piece = containerRaycast(%pos, %targetpos, $TypeMasks::StaticShapeObjectType | $TypeMasks::ForceFieldObjectType, %obj);//changed to $allobjmask so it can see ff's
	%piece = getWord(%piece, 0);
		%dataBlockName = %piece.getDataBlock().getName();
if (
	%dataBlockName $= "StationInventory" ||
	%dataBlockName $= "GeneratorLarge" ||
	%dataBlockName $= "SolarPanel" ||
	%dataBlockName $= "SensorMediumPulse" ||
	%dataBlockName $= "SensorLargePulse"
    )
if (%piece.deployed != true)
   return;
   
	if (!isObject(%piece))
		return;

	if (!%client.isAdmin)
	{
		if (%piece.owner != %client)
		{
			messageClient(%client, 'MsgClient', "\c2MT: That piece isn't yours.");
			return;
		}
	}
///here the code for the other cheks
if (%obj.modifierMode==1){
   swaping(%piece,%obj,%obj.modifierMode2,0);
   return;
   }
else if (%obj.modifierMode==2){
   swaping(%piece,%obj,%obj.modifierMode2,1);
   return;
   }
else if (%obj.modifierMode==3){
     scaling(%piece,%client);
     return;
     }
else if (%obj.modifierMode==4){
     nudging(%piece,%client);
     return;
     }
	if (!isObject(%client.mergePiece1))
	{
		%client.mergePiece1 = %piece;

		%piece.setCloaked(true);
		%piece.schedule(290, "setCloaked", false);

		%client.mergeschedule = schedule($ElecMod::MergeTool::Timeout * 1000, 0, "MTClearClientSelection", %client);
	}
	else
	{
		if (%piece != %client.mergePiece1)
		{
			MTMerge(%client, %client.mergePiece1, %piece);
			MTClearClientSelection(%client);
		}
		else
		{
			messageClient(%client, 'MsgClient', "\c2MT: You cannot merge one piece into itself.");
			return;
		}
	}
}

//to electricutioner make shure you keep this in ur next vertion
//cos your merge tool has becomed the modifier tool
function MergeToolImage::onMount(%this,%obj,%slot)
{
	Parent::onMount(%this, %obj, %slot);
%obj.usingmodifier = true;
if (%obj.modifierMode2 $= "")
   %obj.modifierMode2 = 0;
if (%obj.modifierMode $= "")
   %obj.modifierMode = 0;
	%obj.client.setWeaponsHudActive("sniperrifle");
    %line = getWords($WeaponSettings2["modifier",%this.modifierMode],1,10);
	%obj.mountImage(MergeToolImage, 0);
}
function MergeToolImage::onUnmount(%data, %obj, %slot) {
%obj.usingmodifier = false;
WeaponImage::onUnmount(%data, %obj, %slot);
}

//NUDGING

function nudging(%obj,%client){
%obj.setCloaked(true);
%obj.schedule(150, "setCloaked", false);

%dataBlock   = %obj.getDataBlock();
%name        = %dataBlock.getname();
%className   = %dataBlock.className;
if (%obj.squaresize !$="")
   return;
%Transform = %obj.getTransform();
%pos       = posFromTransform(%Transform);
%dir       = %obj.getForwardVector();

if (%client.player.modifierMode2==2)
   %axis=realvec(%obj,"1 0 0");//x
if (%client.player.modifierMode2==3)
   %axis=realvec(%obj,"-1 0 0");//-x

if (%client.player.modifierMode2==4)
   %axis=realvec(%obj,"0 1 0");//y
if (%client.player.modifierMode2==5)
   %axis=realvec(%obj,"0 -1 0");//-y

if (%client.player.modifierMode2==6)
   %axis=realvec(%obj,"0 0 1");//z
if (%client.player.modifierMode2==7)
   %axis=realvec(%obj,"0 0 -1");//-z
   if (%client.scaler$="")//if the scaler dosnt exist
   %client.scaler=0.1; //default 0.1
%NewPosition=vectorAdd(%pos,vectorScale(%axis,%client.scaler));
if (%client.player.modifierMode2==0) //up
    %NewPosition=VectorAdd(%pos,VectorScale("0 0 1",%client.scaler));
if (%client.player.modifierMode2==1) //down
    %NewPosition=VectorAdd(%pos,VectorScale("0 0 -1",%client.scaler));
if (%obj.originalpos $="")
   %obj.originalpos=%NewPosition;
%dist=VectorDist(%NewPosition,%obj.originalpos);
if (%dist>8){
   messageclient(%client, 'MsgClient', "\c2Piece is too far from original position.");
   return;
   }
if (%obj.isdoor==1){//only scale door that are fully closed and not moving
   if (%obj.canmove == false){
      messageclient(%client, 'MsgClient', "\c2You cannot move a door that is already moving.");
      return;
      }
   if(%obj.state !$="closed" && %obj.state !$=""){
      messageclient(%client, 'MsgClient', "\c2You can only move fully closed doors.");
      return;
      }
   }
%obj.setTransform(%NewPosition);
//below is to make shure that all atatch parts will folow the main piece
checkAfterRot(%obj);

}

//SCALING

function scaling(%obj,%client){
%dataBlock   = %obj.getDataBlock();
%name        = %dataBlock.getname();
%className   = %dataBlock.className;
if (%obj.squaresize !$="")
   return;
%Transform    = %obj.getTransform();
if (%client.player.modifierMode2==0 || %client.player.modifierMode2==4)//sets
   %axis="1 1 1";                                                      //the
if (%client.player.modifierMode2==1 || %client.player.modifierMode2==5)//right
   %axis="1 0 0";                                                      //vector
if (%client.player.modifierMode2==2 || %client.player.modifierMode2==6)//for
   %axis="0 1 0";                                                      //scaling
if (%client.player.modifierMode2==3 || %client.player.modifierMode2==7)//
   %axis="0 0 1";                                                      //
if (%client.player.modifierMode2>3)//inverts it
   %axis=VectorScale(%axis,-1);    //if its on a -scale
if (%client.scaler$="")//if the scaler dosnt exist
   %client.scaler=0.1; //default 0.1
%multiplier=VectorScale(%axis,%client.scaler);//gets the multiplier
if (%obj.isdoor==1){//only scale door that are fully closed and not moving
   if (%obj.canmove == false){
      messageclient(%client, 'MsgClient', "\c2You cannot scale a door while it is moving.");
      return;
      }
   if(%obj.state !$="closed" && %obj.state !$=""){
      messageclient(%client, 'MsgClient', "\c2You can only scale doors while fully closed.");
      return;
      }
   }
//so spines scale corectly
if (%classname $= "spine" || %classname $= "mspine" || %classname $= "spine2" || %classname $= "floor" || %classname $= "wall" || %classname $= "wwall" || %classname $= "floor" || %classname $= "door") {
   %multiplier=VectorScale(%multiplier,"0.125 0.166666 1");
   %size=Vectoradd(%obj.getScale(),%multiplier);
   if (getword(%size,0)<0.001 || getword(%size,1)<0.001 || getword(%size,2)<0.001){ //just so
      messageclient(%client, 'MsgClient', "\c2Piece is too small.");                           //it dont go
      return;                                                                       //to small
      }
   if (getword(%size,0)>100 || getword(%size,1)>100 || getword(%size,2)>100){ //just so
      messageclient(%client, 'MsgClient', "\c2Piece is too big.");                       //it dont go
      return;                                                                 //to big
      }
   %obj.setScale(%size);
   return;
   }
//so if you hit a target its the decorations that scales
if (%name $="DeployedLTarget"){
   %size=vectoradd(%obj.lMain.getScale(),%multiplier);
   if (getword(%size,0)<0.001 || getword(%size,1)<0.001 || getword(%size,2)<0.001){
      messageclient(%client, 'MsgClient', "\c2Piece is too small.");
      return;
      }
   if (getword(%size,0)>100 || getword(%size,1)>100 || getword(%size,2)>100){ //just so
      messageclient(%client, 'MsgClient', "\c2Piece is too big.");                       //it dont go
      return;                                                                 //to big
      }
   %obj.lMain.setScale(%size);
   adjustLMain(%obj);
   return;
   }
//so both the logo and the projector scales at once
if (%name $="DeployedLogoProjector"){
   %size=vectoradd(%obj.getScale(),%multiplier);
   if (getword(%size,0)<0.01 || getword(%size,1)<0.01 || getword(%size,2)<0.01){//smaller then that
      messageclient(%client, 'MsgClient', "\c2Piece is too small.");                       //and its to small to deconstruct
      return;
      }
   if (getword(%size,0)>100 || getword(%size,1)>100 || getword(%size,2)>100){ //just so
      messageclient(%client, 'MsgClient', "\c2Piece is too big.");                       //it dont go
      return;                                                                 //to big
      }
   %obj.setScale(%size);
   if (%obj.holo !=""){
      if (isobject(%obj.holo)){
         %obj.holo.setScale(%size);
         adjustHolo(%obj);
         }
      }
   return;
   }
%size=vectoradd(%obj.getScale(),%multiplier);
if (getword(%size,0)<0.001 || getword(%size,1)<0.001 || getword(%size,2)<0.001){
   messageclient(%client, 'MsgClient', "\c2Piece is too small.");
   return;
   }
   if (getword(%size,0)>100 || getword(%size,1)>100 || getword(%size,2)>100){ //just so
      messageclient(%client, 'MsgClient', "\c2Piece is too big.");                       //it dont go
      return;                                                                 //to big
      }
%obj.setScale(%size);
}

//SWAPPING

function swaping(%hitobj,%plyr,%mode,%mode2){
%sender=%plyr.client;
if ( %hitobj.ownerGUID != %sender.guid){
   if (!%sender.isadmin && !%sender.issuperadmin){
      if (%hitobj.ownerGUID !$=""){
         messageclient(%sender, 'MsgClient', '\c2You do not own this.');
         return;
         }
      }
   }
if (%hitobj.squaresize !$="")
   return;
%classname= %hitobj.getDataBlock().classname;
%objscale = %hitobj.scale;
%grounded = %hitobj.grounded;
%pwrfreq  = %hitobj.powerFreq;
%Transform    = %hitobj.getTransform();

//////////////
//forcefeild//
//////////////
if (%classname$="forcefield" && %mode2==1){
    %db="DeployedForceField"@%mode;
  	%deplObj = new ("ForceFieldBare")() {
		dataBlock = %db;
		scale     = %objscale;
	};
    %deplObj.setTransform(%Transform);
    if (%hitobj.noSlow == true){
       %deplObj.noSlow = true;
       %deplObj.pzone.delete();
	   %deplObj.pzone = "";
       }
    if (%hitobj.pzone !$= "")
       %hitobj.pzone.delete();
    %hitobj.delete();

    // misc info
	addDSurface(%item.surface,%deplObj);

	// [[Settings]]:

	%deplObj.grounded = %grounded;
	%deplObj.needsFit = 1;

	// [[Normal Stuff]]:

	// set team, owner, and handle
	%deplObj.team = %plyr.client.team;
	%deplObj.setOwner(%plyr);

	// set power frequency
	%deplObj.powerFreq = %pwrfreq;

	// set the sensor group if it needs one
	if (%deplObj.getTarget() != -1)
		setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

	// place the deployable in the MissionCleanup/Deployables group (AI reasons)
	addToDeployGroup(%deplObj);

	//let the AI know as well...
	AIDeployObject(%plyr.client, %deplObj);

	// increment the team count for this deployed object
	$TeamDeployedCount[%plyr.team, %item.item]++;

	// Power object
	checkPowerObject(%deplObj);

	return %deplObj;
   }
///////////%objscale = %hitobj.scale;
//pads   //%oldpos   = %hitobj.position;
///////////%oldrot   = %hitobj.rotation;
else if (%mode2==0 && ((%classname $= "decoration" && %hitobj.getDataBlock().getname() $="DeployedDecoration6") || %classname $= "crate" || %classname $= "floor" || %classname $= "spine" || %classname $= "mspine" || %classname $= "wall" || %classname $= "wwall" || %classname $= "Wspine" || %classname $= "Sspine" || %classname $= "floor" || %classname $= "door"))
     {
%hitobj.setCloaked(true);
%hitobj.schedule(290, "setCloaked", false);
if (%hitobj.isdoor == 1 || %hitobj.getdatablock().getname() $="DeployedTdoor"){
   if (%hitobj.canmove == false) //if it cant move
      return;
   if (%hitobj.state !$="closed" && %hitobj.state !$="")
      return;
   }
if (%hitobj.isobjective > 0)
   return;

    %db    = getword($WeaponSetting["modifier1",%mode],0);
    %count = $WeaponSettings["modifier1"]; //counts
    if (%hitobj.getdatablock().getname() $="DeployedFloor")
       %datablock="DeployedwWall";
    else if (%hitobj.getdatablock().getname() $="DeployedMSpinering")
       %datablock="DeployedMSpine";
    else if (%hitobj.getdatablock().getname() $="DeployedTdoor"){
       %datablock="DeployedMSpine";
       }
    else
       %datablock=%hitobj.getdatablock().getname();


     %team = %hitobj.team;
     %owner     = %hitobj.owner;
     if (%hitobj.ownerGUID>0)
        %ownerGUID = %hitobj.ownerGUID;
     else
         %ownerGUID = "";

    if (%hitobj.isdoor == 1 || %hitobj.getdatablock().getname() $="DeployedTdoor"){
       %issliding     = %hitobj.issliding;
       %state         = %hitobj.state;
       %canmove       = %hitobj.canmove;
       %closedscale   = %hitobj.closedscale;
       %openedscale   = %hitobj.openedscale;
       %oldscale      = %hitobj.oldscale;
       %moving        = %hitobj.moving;
       %toggletype    = %hitobj.toggletype;
       %powercontrol  = %hitobj.powercontrol;
       %Collision     = %hitobj.Collision;
       %lv            = %hitobj.lv;
       }

     %scale = %hitobj.GetRealSize();

     %deplObj = new ("StaticShape")() {
		dataBlock = %db;
	 };
     %deplObj.SetRealSize(%scale);
     %deplObj.setTransform(%Transform);
//////////////////////////Apply settings//////////////////////////////

	// misc info
	addDSurface(%item.surface,%deplObj);

	// [[Settings]]:

	%deplObj.grounded = %grounded;
	%deplObj.needsFit = 1;

	// set team, owner, and handle
	%deplObj.team = %team;
	%deplObj.Ownerguid=%ownerGUID;
    %deplObj.Owner=%owner;

	// set power frequency
	%deplObj.powerFreq = %pwrfreq;
     %deplObj.OriginalPos = %hitobj.OriginalPos;
	// set the sensor group if it needs one
	if (%deplObj.getTarget() != -1)
		setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

	// place the deployable in the MissionCleanup/Deployables group (AI reasons)
	addToDeployGroup(%deplObj);

	//let the AI know as well...
	AIDeployObject(%plyr.client, %deplObj);

	// increment the team count for this deployed object
	$TeamDeployedCount[%plyr.team, %item.item]++;

	%deplObj.deploy();

	// Power object
	checkPowerObject(%deplObj);

    if (%hitobj.isdoor == 1 || %hitobj.getdatablock().getname() $="DeployedTdoor"){
       %deplObj.closedscale  = %deplObj.getScale();
       %deplObj.openedscale  = getwords(%deplObj.getScale(),0,1) SPC 0.1;
       %deplObj.isdoor       = 1;
       %deplObj.state        = %state  ;
       %deplObj.canmove      = %canmove  ;
       %deplObj.moving       = %moving ;
       %deplObj.toggletype   = %toggletype ;
       %deplObj.powercontrol = %powercontrol;
       %deplObj.Collision    = %Collision ;
       %deplObj.lv           = %lv ;
       }
%hitobj.delete();
	return %deplObj;
     }
}
