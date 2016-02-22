//Custom functions by Mostlikely
//There's a lot of outdated stuff here
//But those things are needed for basic deploying until I can update the code.
//Be sure to mail any questions to "sebastiaan_keek@hotmail.com"


// Basic dynamic movement script.
// Fps reduction and lag producing [[A bit]]
// To be expanded/uniformed in later versions

$Pi = 3.1415926535897932;
$AnimationTime = 150;

function lev(%num) {
	if (%num < 0)
		return -1;
	else
		if (%num > 0)
			return 1;
	else
		return 0;
}

//function abs(%num) {
//	%num = %num * lev(%num);
//	if (%num == 0)
//		%num = 0;
//	return %num;
//}

function vlev(%vec)
{
return lev(getWord(%vec,0)) SPC lev(getWord(%vec,1)) SPC lev(getWord(%vec,2));
}

function vAbs(%vec) {
	return mAbs(getWord(%vec,0)) SPC mAbs(getWord(%vec,1)) SPC mAbs(getWord(%vec,2));
}

//Basic test.. don't use this function jet... it won't stop

function DynamicRotate(%obj,%vec,%crot)
{
%vec = vectorNormalize(%vec);
//%rot = getrot(getRotation(%obj));
//%rx = getWord(%rot,0);
//%ry = getWord(%rot,1);
//%rz = getWord(%rot,2);
//%xr = %rx+ getWord(%pace,0);
//%yr = %ry + getWord(%pace,1);
//%zr = %rz + getWord(%pace,2);
%oldrot = rotFromTransform(%Obj.getTransform());
//Schedule(50, %obj,"dynamicRotate2", %obj, %vec, %crot,%oldrot);
}

function DynamicRotate2(%obj,%vec,%crot,%oldrot)
{
%vec = vectorNormalize("0 0 1");
%crot = %crot + 0.1;
%newrot = %vec SPC %crot;
%setrot = rotAdd(%oldrot,%newrot);
setRotation(%obj, %setrot);
Schedule(50, %obj,"dynamicRotate2", %obj, %vec, %crot,%oldrot);
}

//DynamicScale
//Better than dynamicmove...
//To be expanded.

function DynamicScale(%obj,%time,%pace,%end)
{
%scale = %obj.GetScale();
%sx = getWord(%scale,0);
%sy = getWord(%scale,1);
%sz = getWord(%scale,2);
%newx = %sx + getWord(%pace,0);
%newy = %sy + getWord(%pace,1);
%newz = %sz + getWord(%pace,2);


%done = 1;
if (mAbs(%newx - getWord(%end,0)) > mAbs(%sx - getWord(%end,0)))
   %xscale = getWord(%end,0); // If we didn't make progress..
else
   {
   %xscale = %newx; // If we did make progress..
   %done = 0;
   }
if (mAbs(%newy - getWord(%end,1)) > mAbs(%sy - getWord(%end,1)))
   %yscale = getWord(%end,1); // If we didn't make progress..
else
   {
   %yscale = %newy; // If we did make progress..
   %done = 0;
   }
if (mAbs(%newz - getWord(%end,2)) > mAbs(%sz - getWord(%end,2)))
   %zscale = getWord(%end,2); // If we didn't make progress..
else
   {
   %zscale = %newz; // If we did make progress..
   %done = 0;
   }

%newscale = %xscale SPC %yscale SPC %zscale;
%obj.setScale(%newscale);
if (%done == 0)
	schedule(%time, %obj,"dynamicScale", %obj, %time, %pace, %end ,%plyr);
}

//Returns xyz rotation from tribes uniformalized 4 digit rotation.

function getrot(%rotation)
{
%xr = getWord(%rotation,0)*getWord(%rotation,3);
%yr = getWord(%rotation,1)*getWord(%rotation,3);
%zr = getWord(%rotation,2)*getWord(%rotation,3);
return %xr SPC %yr SPC %zr;
}

//Returns tribes rotation from xyz rotation in an outdated way.

function setrot(%rot)
{
%mat = MatrixCreateFromEuler(%rot);
return getWord(%mat,3) SPC getWord(%mat,4) SPC getWord(%mat,5) SPC getWord(%mat,6);
}

//Controlled version setrot
//Soon to be used in deploying.

function setrot2(%rot)
{
%vn = vectorNormalize(%rot);
if (getWord(%vn,0) != 0)
%zn = getWord(%rot,0) / getWord(%vn,0);
else if (getWord(%vn,1) != 0)
%zn = getWord(%rot,1) / getWord(%vn,1);
else if (getWord(%vn,2) != 0)
%zn = getWord(%rot,2) / getWord(%vn,2);
else
%zn = 0;
return %vn SPC %zn;
}

//Returns a rotation from a normal that matches the normal and faces up the slope

// for loadscreen.cs
if ($MissionDisplay != true)
	%of = true;

function slopfromnrm(%up)
{

%x=getWord(%up,0);
%y=getWord(%up,1);
%z=getWord(%up,2);
%xy = Mpow((Mpow(%x,2)+Mpow(%y,2)),0.5);
%zrot = MAtan(%y,%x)-1.5708;
%xrot = -MAtan(%xy,%z);
return  %xrot SPC "0" SPC %zrot;
}

//expanded version of slopfromnrm
//Doesn't not work properly.. to be updated.

function rotfromnrm(%up,%le)
{
%le = vectorScale(vectorright(%up,%le),-1);
%fo = VectorCross(%up,%le);
%x = getWord(%fo,0);
%y = getWord(%fo,1);
%zrot = MAtan(%y,%x);
%up = rotatenormal(%up,0,0,-1 * %zrot);
%fo = rotatenormal(%fo,0,0,-1 * %zrot);
%le = rotatenormal(%le,0,0,-1 * %zrot);
%x = getWord(%fo,0);
%y = getWord(%fo,2);
%yrot =  MAtan(%y,%x);
%up = rotatenormal(%up,0,-1 * %yrot,0);
%fo = rotatenormal(%fo,0,-1 * %yrot,0);
%le = rotatenormal(%le,0,-1 * %yrot,0);
%x = getWord(%up,1);
%y = getWord(%up,2);
%xrot =  MAtan(%x,%y);
%up = rotatenormal(%up, %xrot,0,0);
%fo = rotatenormal(%fo, %xrot,0,0);
%le = rotatenormal(%le, %xrot,0,0);
return  %xrot SPC %yrot SPC %zrot + 3.14159;
}

// for loadscreen.cs
if (%of == true) {
	return;
}

//Should rotate things in the same order as tribes2 does.
function rotatenormal(%vector,%xr,%yr,%zr)
{
                // z > x
                // z > y
                // y > x
//x rot
%x = getWord(%vector,0);
%y = getWord(%vector,1);
%z = getWord(%vector,2);
%vector =  %x SPC rotatedot(%y SPC %z, %xr);
//y rot
%x = getWord(%vector,0);
%y = getWord(%vector,1);
%z = getWord(%vector,2);
%vector = getWord(rotatedot(%x SPC %z,%yr),0) SPC %y SPC getWord(rotatedot(%x SPC %z,%yr),1);
//z rot
%x = getWord(%vector,0);
%y = getWord(%vector,1);
%z = getWord(%vector,2);
%vector =  rotatedot(%x SPC %y, %zr) SPC %z;
return %vector;
}

//2dimensional rotation... used in some calculations

function rotatedot(%vector,%rot)
{
%nx = MCos(%rot) * getWord(%vector,0) + -1 *  MSin(%rot) * getWord(%vector,1);
%ny = MSin(%rot) * getWord(%vector,0) + MCos(%rot) * getWord(%vector,1);
%nvector = %nx SPC %ny;
return %nvector;
}

//Returns the proper y as when a proper x and problematic y are introduced

function vectorright(%mainvec,%chanvec)
{
%temp = VectorCross(%mainvec,%chanvec);
return vectorNormalize(VectorCross(%mainvec,%temp));
}

//Returns the vector that %vector is heading in %direction

function vectorproject(%vector,%direction)
{
%v = %vector;//VectorSub(%item.surfacePt,%item.surface.getTransform());
%a = %direction; //VectorCross(%item.surface.up,%item.surfaceNrm);
return vectorScale(%a,(VectorDot(%a,%v) / MPow (VectorLen(%a),2)));
}

//Returns the lengt of %vector in %direction

function vectorcouple(%vector,%direction)
{
%v = %vector;//VectorSub(%item.surfacePt,%item.surface.getTransform());
%a = %direction; //VectorCross(%item.surface.up,%item.surfaceNrm);
return (VectorDot(%a,%v) / MPow (VectorLen(%a),2));
}

function rotAdd(%rotation1,%rotation2)
{
%v1 = "0 0 0" SPC %rotation1;
%v2 = "0 0 0" SPC %rotation2;
// This is the function verified to sometimes return "inf" or "nan". The others are checked for safety's sake
%v3 = validateVal(MatrixMultiply(%v1,%v2));
%axis = vectorNormalize(getWords(%v3,3,5));
return %axis SPC getWord(%v3,6);
}

function realrot(%obj,%rotation)
{
%rot = rot(%Obj);
//[Note] Old notation caused trouble.
//Not sure if the new one is totaly accurate.. but it works ;)
//return rotAdd(rotAdd(%rot,%rotation),invrot(%rot));
%axis = realvec(%obj,getWords(%rotation,0,2));
return %axis SPC getWord(%rotation,3);
}

function virrot(%obj,%rotation)
{
%rot = rot(%Obj);
//return rotAdd(rotAdd(invrot(%rot),%rotation),%rot);
%axis = virvec(%obj,getWords(%rotation,0,2));
return %axis SPC getWord(%rotation,3);
}

function remoteRotate(%mobj,%rot,%cobj,%mcenter)
{
%oldrot = rot(%mObj);
%oldpos = vectorAdd(pos(%mObj),%center);
%center = vectorAdd(pos(%cObj),Realvec(%cobj,VectorMultiply(%mcenter,vectorScale(realSize(%cobj),-1))));
%rrot = realrot(%cobj,%rot);
%newpos = vectorAdd(validateVal(MatrixMulPoint("0 0 0" SPC %rrot,vectorSub(%oldpos,%center))),%center);
return %newpos SPC rotAdd(%rrot,%oldrot);
}

function realSize(%obj) {
	if (%obj.getType() & $TypeMasks::StaticShapeObjectType)
		%ws = (%obj.getDataBlock().getName() $= "DeployedWoodSpine");
	if (isCubic(%obj) || %ws) {
		%scale = %obj.GetScale();
		if (%ws)
			return vectorDivide(%scale,1.845 SPC 2 SPC 1.744); // Update spine.cs if changed!
		else
			return (getWord(%scale,0) * 4) SPC (getWord(%scale,1) * 3) SPC (getWord(%scale,2) * 0.5);
	}
}

function isCubic(%obj) {
	if (!isObject(%obj))
		return;
	if (!(%obj.getType() & $TypeMasks::StaticShapeObjectType))
		return;
	%shape = %obj.getDatablock().shapefile;
	if (getSubStr(%shape,1,strLen(%shape)) $= "miscf.dts")
		return 1;
	else
		return "";
}

function realvec(%obj,%vec) {
	%rot = rot(%Obj);
	return validateVal(MatrixMulVector("0 0 0" SPC %rot ,%vec));
}

function virvec(%obj,%vec)
{
%rot = rot(%Obj);
%rot = getWord(%rot,0) SPC getWord(%rot,1) SPC getWord(%rot,2) SPC (-1*getWord(%rot,3) );
return validateVal(MatrixMulVector("0 0 0" SPC %rot ,%vec));
}

function vectorangle(%vec1,%vec2)
{
return MACos(VectorDot(%vec1,%vec2)/(VectorLen(%vec1)*VectorLen(%vec2)));
}

function vectormultiply(%vec1,%vec2)
{
return getWord(%vec1,0) * getWord(%vec2,0) SPC getWord(%vec1,1)*  getWord(%vec2,1) SPC getWord(%vec1,2)*  getWord(%vec2,2);
}

// An updated version of this function is located further down in the file - JackTL
//function vectordivide(%vec1,%vec2)
//{
//return getWord(%vec1,0) / getWord(%vec2,0) SPC getWord(%vec1,1) /  getWord(%vec2,1) SPC getWord(%vec1,2) /  getWord(%vec2,2);
//}

function vectordescale(%vec1,%scale)
{
return %scale/getWord(%vec1,0)  SPC %scale/getWord(%vec1,1)  SPC %scale /getWord(%vec1,2);
}

function getface(%obj,%vec)
{
%rot = getWords(rot(%Obj),0,2) SPC -1 * getWord(rot(%Obj),3);
return validateVal(MatrixMulVector("0 0 0" SPC %rot ,%vec));
}

function floorvec(%vec,%size)
{
%mod = vLev(%vec);
%vec = vAbs(vectorScale(%vec,%size));
%vec = Mfloor(getWord(%vec,0)) SPC  Mfloor(getWord(%vec,1)) SPC  Mfloor(getWord(%vec,2));
return VectorMultiply(vectorScale(%vec,(1/%size)),%mod);
}

function invface(%face)
{
return VectorSub("1 1 1",vAbs(%face));
}

function getfacesize(%obj,%face)
{
return VectorMultiply(vAbs(invface(%face)),realSize(%obj));
}

function breach(%val,%border)
{
if (%border < 0)
return %val;
if (%val > %border)
return (%val-%border);
else
return 0;
}

function topface(%vec)
{
if (mAbs(getWord(%vec,0)) >= mAbs(getWord(%vec,1)) + mAbs(getWord(%vec,2)))
return getWord(%vec,0) SPC "0 0";
if (mAbs(getWord(%vec,1)) >= mAbs(getWord(%vec,0)) + mAbs(getWord(%vec,2)))
return "0" SPC getWord(%vec,1) SPC "0";
if (mAbs(getWord(%vec,2)) >= mAbs(getWord(%vec,0)) + mAbs(getWord(%vec,1)))
return "0 0" SPC getWord(%vec,2);
}

function pullaxis(%vec1,%vec2)
{
%face = invface(%vec2);
%hit = vectorNormalize(topface(vectormultiply(%vec1,%face)));
return vectorNormalize(VectorCross(%hit,%vec2));
}

function pullobject(%obj,%vec1,%vec2,%angle,%center) {
	if (!isObject(%obj))
		return;
	%pos = pos(%Obj);
	%rot = rot(%Obj);
	%vec = vectorSub(%vec1,%pos);
	%vec = virVec(%obj,%vec);
	%vec = vectorAdd(vectorDivide(%vec,vectorScale( realSize(%obj),0.5 ) ),"0 0 -1");
	%axis = pullAxis(%vec,%vec2);
	if (!isCubic(%obj) && %obj.getDataBlock().getName() !$= "DeployedWoodSpine")
		%axis = "0 0 1";
	if (%obj.getType() & $TypeMasks::ForceFieldObjectType)
		return;
	%obj.setTransform(remoteRotate(%obj,%axis SPC %angle,%obj,%center));
		checkAfterRot(%obj);
}

function rotateSection(%obj,%list,%rotation) {
	%count = getWordCount(%list);
	for (%id = 0; %id < %count; %id++) {
		%obj2 = getWord(%list,%id);
		if (isObject(%obj2)) {
			%obj2.setTransform(remoteRotate(%obj2,%rotation,%obj));
			checkAfterRot(%obj2);
		}
	}
}

function checkAfterRot(%obj) {
	if (!$Host::AllowUnderground) {
		%terrain = getTerrainHeight2(%obj.getPosition());
		if (getWord(%obj.getPosition(),2) < getWord(%terrain,2) || %terrain $= "")
			%obj.setPosition(%terrain);
	}
	if (isObject(%obj.pzone))
		%obj.pzone.setTransform(%obj.getTransform());
	if (isObject(%obj.trigger))
		adjustTrigger(%obj);
	if (isObject(%obj.beam))
		tp_adjustBeam(%obj);
	if (isObject(%obj.holo))
		adjustHolo(%obj);
	if (isObject(%obj.light))
		adjustLight(%obj);
	if (isObject(%obj.lMain))
		adjustLMain(%obj);
	if (%obj.getDataBlock().needsPower) {
		%obj.powerCount = "";
		checkPowerObject(%obj);
	}
	// TODO - handle gens
}

function adjustTrigger(%obj) {
	// TODO - adjust trigger for InventoryDeployable
	%dataBlockName = %obj.getDataBlock().getName();
	if (%dataBlockName $= "StationInventory") {
		%adjust = vectorMultiply(realVec(%obj,"0 0 1"),"1 1 3");
		%adjustUp = getWord(%adjust,2);
		%adjust = getWords(%adjust,0,1) SPC ((%adjustUp * 0.5) + (mAbs(%adjustUp) * -0.5));
		%obj.trigger.setTransform(vectorAdd(%obj.getPosition(),%adjust) SPC "0 0 0"); // %obj.rotation);
	}
}

// Oh hush, it works. :)
function adjustLMain(%obj) {
	%dataBlockName = %obj.lMain.getDataBlock().getName();
	if (getSubStr(%dataBlockName,0,18) $= "DeployedDecoration") {
		%pos = %obj.getPosition();
		%rot = %obj.getRotation();
		%nrm = realVec(%obj,"0 0 1");
		%nrm2 = realVec(%obj,"1 0 0");
		%nrm3 = realVec(%obj,"0 1 0");
		%no = trim(getSubStr(%dataBlockName,18,2));
		if (%no < 3) {
			%nrm2 = vectorScale(%nrm2,-1);
			%pos = vectorAdd(%pos,vectorScale(%nrm3,2.80));
			%pos = vectorAdd(%pos,vectorScale(%nrm,0.25));
			%rot =  rotAdd(%rot,"1 0 0" SPC $Pi / 2);
			%rot =  rotAdd(%rot,"0 1 0" SPC $Pi);
		}
		else if (%no > 2 && %no < 6) {
			%pos = vectorAdd(%pos,vectorScale(%nrm,-0.2));
			if (%no == 3) {
				%pos = vectorAdd(%pos,vectorScale(%nrm2,0.14));
				%pos = vectorAdd(%pos,vectorScale(%nrm3,-0.51));
			}
			if (%no == 4) {
				%pos = vectorAdd(%pos,vectorScale(%nrm2,-0.25));
				%pos = vectorAdd(%pos,vectorScale(%nrm3,0.4));
			}
			if (%no == 5) {
				%pos = vectorAdd(%pos,vectorScale(%nrm2,-0.05));
				%pos = vectorAdd(%pos,vectorScale(%nrm3,-0.4));
				%rot =  rotAdd(%rot,"0 0 -1" SPC $Pi / 4);
			}
		}
		else if (%no > 11 && %no < 16) {
			%pos = vectorAdd(%pos,vectorScale(%nrm,1));
			%pos = vectorAdd(%pos,vectorScale(%nrm3,23.1));
			%rot =  rotAdd(%rot,"-1 0 0" SPC $Pi / 2);
		}
		%obj.lMain.setTransform(%pos SPC %rot);
	}
	else if (getSubStr(%dataBlockName,0,18) $= "DeployedEscapePod") {
		adjustEscapePod(%obj.lMain);
		%epod = %obj.lMain.podVehicle;
		if (isObject(%epod) && %epod.used == false)
			%epod.delete();
	}
}

function invrot(%rot)
{
return vectorScale(getWord(%rot,0) SPC getWord(%rot,1) SPC getWord(%rot,2),-1) SPC getWord(%rot,3);
}

//Tones down the outer parts
//In real cube size.

function cubefix(%cube,%point,%joint)
{
%limit = vectorScale(realSize(%cube),0.5);
%px = getWord(%point,0) - ( lev( getWord(%point,0) ) * breach( mAbs(getWord(%point,0)) , getWord(%limit,0)-getWord(%joint,0) ) );
%py = getWord(%point,1) - ( lev( getWord(%point,1) ) * breach( mAbs(getWord(%point,1)) , getWord(%limit,1)-getWord(%joint,1) ) );
%pz = getWord(%point,2) - ( lev( getWord(%point,2) ) * breach( mAbs(getWord(%point,2)) , getWord(%limit,2)-getWord(%joint,2) ) );
return %px SPC %py SPC %pz;
}

//Same as cubefix
//Only in virtual cube size.

function vircubefix(%point,%joint)
{
%limit = "1 1 1";
%px = getWord(%point,0) - ( lev( getWord(%point,0) ) * breach( mAbs(getWord(%point,0)) , getWord(%limit,0)-getWord(%joint,0) ) );
%py = getWord(%point,1) - ( lev( getWord(%point,1) ) * breach( mAbs(getWord(%point,1)) , getWord(%limit,1)-getWord(%joint,1) ) );
%pz = getWord(%point,2) - ( lev( getWord(%point,2) ) * breach( mAbs(getWord(%point,2)) , getWord(%limit,2)-getWord(%joint,2) ) );
return %px SPC %py SPC %pz;
}

//Obj = Surface.
//face = surfaceNrm (Real)
//point = surfacePt (Real)
//joint = dobj surface size (Real)
//center = relative center for surface (Virtual)

function sidelink(%obj,%face,%point,%joint,%center)
{
%loc = pos(%Obj);

//Fitting All stuff to virtual space
%relpoint = VectorSub(%point,%loc);
%virpoint =  virvec(%obj,%relpoint);
%virpoint = VectorDivide(%virpoint,vectorScale( realSize(%obj),0.5 ));
%virpoint = vectorAdd(%virpoint,vectorScale(%center,2));

%virjoint = VectorDivide( %joint,vectorScale( realSize(%obj),1 ));
%virface = virvec(%obj,%face);

//Desiding which side should be linked to.
%sidedir = pullaxis(%virpoint,%virface);
%side = VectorCross(%sidedir,%virface);

//Keeping the movement on that side within bounds.
%virpoint = vircubefix(%virpoint,%virjoint);
%virpoint = VectorMultiply( %virpoint, vAbs(%sidedir));

//Fitting the new location next to the object.
%overpull = VectorMultiply(vectorScale(%side,-1),vectorAdd(%virjoint,"1 1 1"));


//Getting it all together.
%mask = vectorAdd(vectorAdd(%virpoint,%virface),%overpull);


//Returning to real space.
%virpoint = vectorAdd(%mask,vectorScale(%center,-2));

%virpoint = VectorMultiply(vectorScale(realSize(%obj),0.5),%virpoint);

%relpoint = realvec( %obj , %virpoint);

//Also returning the side used.
//echo(floorvec(%virface,1));
if (vAbs(vlev(floorvec(%virface,100))) $= "0 1 0")
%goodside = VectorCross(%virface,"0 0 1");
else
%goodside = VectorCross(%virface,"0 1 0");

%realside = vectorNormalize(realvec(%obj,%side));
%dirside = vectorNormalize(realvec(%obj,%goodside));

return vectorAdd(%loc,%relpoint) SPC %realside SPC %dirside;
}



//Function returns the fited placement.
//Obj = Surface object.
//Face = surface normal. (Virtual)
//Point = Surface Hit point. (Real)
//Joint = Surface size placement object. (Real)
//Center = Models t2 center relative to the real center.  (Virtual)


function link(%obj,%face,%point,%joint,%center)
{
%loc = pos(%Obj);
%relpoint = VectorSub(%point,%loc);

	%virpoint =  virvec(%obj,%relpoint);

                %adjust = VectorMultiply(%center,realSize(%obj));

		%virpoint = vectorAdd(%virpoint,%adjust);


		%virpoint = cubefix(%obj,%virpoint,%joint);

		%virpoint = VectorMultiply( %virpoint, invface(%face) );

		%mask = VectorMultiply(vectorScale(realSize(%obj),0.5), %face);

                %virpoint = vectorAdd(%mask,%virpoint);

                %virpoint = vectorAdd(%virpoint,vectorScale(%adjust,-1));

	%relpoint = realvec( %obj , %virpoint);

return vectorAdd(%loc,%relpoint);
}



function slopeRot(%vec)
{
if (vAbs(floorvec(%vec,100)) $= "0 0 1")
   return "1 0 0" SPC $Pi/2 - (lev(getWord(%vec,2))*$Pi/2);
else
   %secv = "0 0 1";
%axis = VectorCross(%vec,%secv);
%angle = VectorAngle(%vec,%secv);
return validateVal(vectorNormalize(%axis) SPC %angle);
}

function intRot(%rot,%vec)
{
%vec1 =  validateVal(MatrixMulVector("0 0 0" SPC invrot(%rot),%vec));
%vec1 = vectorNormalize(getWord(%vec1,0) SPC getWord(%vec1,1) SPC "0");
%angle = MAtan(getWord(%vec1,1),getWord(%vec1,0));
return "0 0 -1" SPC %angle;
}

//Function returns rotation.
//Vec1 = Alignment direction.
//Vec2 = Facing direction.

function fullrot(%vec,%vec2)
{
%rot = slopeRot(%vec);
return rotAdd(%rot,intRot(%rot,%vec2));
}

function rayDist(%vecs,%sizes,%mask,%obj) {
	if (!%mask)
		%mask = -1;
	%obj = mAbs(%obj);
	%start = getWords(%vecs,0,2);
	%dir = vectorNormalize(getWords(%vecs,3,5));
	%min = getWord(%sizes,0);
	%norm = getWord(%sizes,1);
	%max = getWord(%sizes,2);
	%endPos = vectorAdd(%start,vectorScale(%dir,%max));

	%res = containerRayCast(%start, %endPos, %mask, %obj);

	if (%res) {
		if (%res.getType() & $TypeMasks::TerrainObjectType)
			%res = containerRayCast(vectorAdd(%start,"0 0 0.1"), %endPos, %mask, %obj);
		if (%res > 0) {
			%hit = getWords(%res,1,3);
			%dist = vectorDist(%start,%hit);
		}
	}
	if (%dist <= 0)
		%dist = %norm;
	if (%dist < %min)
		%dist = %min;
	return %dist;
}


//Function Returns scale and transform of a wall.
//Vec1 = Start,Fitdirection1,Fitdirection2.
//Sizes = Minsize & thickness, No hit size, Max fit size.
//Center = Models t2 center relative to the real center.

function pad(%vecs,%sizes,%center,%mask,%obj) {
	if (!%mask)
		%mask = $AllObjMask;
	%obj = mAbs(%obj);
	%start1 = getWords(%vecs,0,2);
	%dir1 = vectorNormalize(getWords(%vecs,3,5));
	%dir2 = vectorNormalize(getWords(%vecs,6,8));
	%dir3 = vectorNormalize(VectorCross(%dir1,%dir2));
	%height = rayDist(%start1 SPC %dir1,%sizes,%mask,%obj);
	%start2 = vectorAdd(vectorScale(%dir1,%height/2),%start1);
	%right = rayDist(%start2 SPC %dir2,%sizes,%mask,%obj);
	%left = rayDist(%start2 SPC vectorScale(%dir2,-1),%sizes,%mask,%obj);
	if ((%left + %right)>getWord(%sizes,2)) {
		%lef = %left;
		%left = %left-(((%left+%right)-getWord(%sizes,2))/2);
		%right = %right-(((%lef+%right)-getWord(%sizes,2))/2);
	}
	%scale1 = %height;
	%scale2 = (%left+%right);
	%scale3 = getWord(%sizes,0);
	%scale = %scale1 SPC %scale2 SPC %scale3;

	%moveDist1 = vectorScale( %dir2, ( ( (%left+%right)/2 ) - %left ) );
	%moveDist2 = vectorScale( %dir1, %height/2 );
	%moveDist3 = "0 0 0";//vectorScale(%dir3,getWord(%sizes,0)*getWord(%center,2));

	%Adjust1 = vectorScale(%dir1,%scale1*getWord(%center,0));
	%Adjust2 = vectorScale(%dir2,%scale2*getWord(%center,1));
	%Adjust3 = vectorScale(%dir3,%scale3*getWord(%center,2));

	%moveDist = vectorAdd(vectorAdd(%movedist1,%movedist2),%movedist3);
	%Adjust = vectorAdd(vectorAdd(%Adjust1,%Adjust2),%Adjust3);
	%TotalMove = vectorAdd(%movedist,%adjust);

	%rot = slopeRot(%dir1);
	%rot2 = rotAdd("0 0 1" SPC (1/2 * $Pi),"0 1 0" SPC (1/2 * $Pi));
	%rot = rotAdd(%rot,intRot(%rot,%dir2));
	%rotation = rotAdd(%rot,%rot2);
	return %scale SPC vectorAdd(%TotalMove,%start1) SPC %rotation;
}

//// [[[[List operations]]]];

function createList(%size) {
	for(%id = 0; %id < %size; %id++)
		%list = %list SPC "0";
	return trim(%list);
}

function listAdd(%list,%words,%location) {
	if (%location == -1)
		%list = %list SPC %words;
	else if (%location == 0)
		%list = %words SPC %list;
	else
		%list = getWords(%list,0,%location-1) SPC %words SPC getWords(%list,%location,getWordCount(%list));
	return trim(%list);
}

function listReplace(%list,%words,%location) {
	if (%location == 0)
		%list = %words SPC getWords(%list,1,getWordCount(%list));
	else if (%location == getWordCount(%list))
		%list = getWords(%list,0,%location) SPC %words;
	else
		%list = getWords(%list,0,%location-1) SPC %words SPC getWords(%list,%location+1,getWordCount(%list));
	return trim(%list);
}

function listDel(%list,%ids) {
	%size = getWordCount(%list);
	for(%id = 0; %id < %size; %id++) {
			if (findWord(%ids,%id) $= "")
				%tempList = %tempList SPC getWord(%list,%id);
	}
	return trim(%tempList);
}

function listSort(%list,%sortList) {
	for (%id = 0; %id < getWordCount(%sortList); %id++)
		%tempList = %tempList SPC getWord(%list,getWord(%sortList,%id));
	return trim(%tempList);
}

function listBuild(%list,%sortList) {
	%tempList = createList(getWordCount(%sortList));
	for (%id = 0; %id < getWordCount(%sortList); %id++)
		%tempList = listReplace(%tempList,getWord(%list,%id),getWord(%sortList,%id));
	return trim(%tempList);
}

function findWord(%list,%words) {
	%result = "";
	%size = getWordCount(%list);
	%size2 = getWordCount(%words);
	for (%marker = 0; %marker < %size; %marker++) {
		for (%find = 0; %find < %size2; %find++) {
			if (getWord(%words,%find) $= getWord(%list,%marker))
				%result = %result SPC %marker;
		}
	}
	return trim(%result);
}

//function stripEndSpaces(%list) {
//	if (getSubStr(%list,0,1) $= " ")
//		%list = getSubStr(%list,1,strLen(%list)-1);
//	return stripTrailingSpaces(%list);
//}


function pos(%loc,%mod) {
	if (!isObject(%loc))
		return;
	if (!%mod)
		return posFromTransform(%loc.getTransform());
	%mainmod = getWord(%mod,0);
	if (%mainmod $="o") {
		if (%loc) {
			%trans = %loc.getTransform();
			%xyz = posFromTransform(%trans);
			%mod =getWords(%mod,1,getWordCount(%mod));
		}
	}
	else if (%mainmod $="t") {
		if (%loc) {
			%xyz = posFromTransform(%loc);
			%mod =getWords(%mod,1,getWordCount(%mod));
		}
	}
	for(%i = 0; %i < getWordCount(%mod); %i++)
		%result = %result SPC poshelp1(%xyz,getWord(%mod,%i));
	return %result;
}


function poshelp1(%xyz,%id)
{
if (%id $= "x")
              return getWord(%xyz,0);
if (%id $= "y")
              return getWord(%xyz,1);
if (%id $= "z")
              return getWord(%xyz,2);

    return getWord(%xyz,%id);
}


function rot(%loc,%mod) {
	if (!isObject(%loc))
		return;
	if (!%mod)
		return rotFromTransform(%loc.getTransform());
	%mainmod = getWord(%mod,0);
	if (%mainmod $="o") {
		if (%loc) {
			%trans = %loc.getTransform();
			%xyz = rotFromTransform(%trans);
			%mod =getWords(%mod,1,getWordCount(%mod));
		}
	}
	else if (%mainmod $="t"){
		if (%loc){
			%xyz = rotFromTransform(%loc);
			%mod =getWords(%mod,1,getWordCount(%mod));
		}
	}
	for(%i = 0; %i < getWordCount(%mod); %i++)
		%result = %result SPC rothelp1(%xyz,getWord(%mod,%i));
	return %result;
}


function rothelp1(%xyz,%id)
{
if (%id $= "x")
              return getWord(%xyz,0);
if (%id $= "y")
              return getWord(%xyz,1);
if (%id $= "z")
              return getWord(%xyz,2);
if (%id $= "a")
              return getWord(%xyz,3);
return getWord(%xyz,%id);
}

function getjoint(%item)
{
if (%item.item.joint)
    return %item.item.joint;
else
    return "0.5 0.5 0.5";
}

//

function findhit(%plyr)
{
   $MaxDeployDistance = %item.maxDeployDis;
   $MinDeployDistance = %item.minDeployDis;

   %surface = Deployables::searchView(%plyr,
                                      $MaxDeployDistance,
                                      ($TypeMasks::TerrainObjectType |
                                       $TypeMasks::InteriorObjectType));
   if (%surface)
   {
      %surfacePt  = posFromRaycast(%surface);
      %surfaceNrm = normalFromRaycast(%surface);
      return %surface SPC %surfacept SPC %surfacenrm;
   }
return "";
}

function getTerrainHeight2(%loc,%rail,%obj) {
	%mask = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType;
	if (%rail $= "" || %rail $= "0")
		%rail = "0 0 1";
	//else if (getWord(%rail,2) <= 0)
	//%rail = vAbs(%rail);
	%rail = vectorNormalize(%rail);
	%start = vectorAdd(%loc,vectorScale(%rail,-100000));
	%end = vectorAdd(%loc,vectorScale(%rail,100000));
	%res = containerRayCast(%start, %end, %mask,%obj);
	if (%res)
		return getWords(%res,1,3);
	else
		return "";
}

// Animations

function groundpop(%obj,%rail,%time)
{
%oldpos = pos(%obj);
%rail = vectorNormalize(%rail);
%top = VectorLen(VectorMultiply(realSize(%obj),vAbs(vlev(floorvec( virvec(%obj,%rail),1000)))));
%newpos = vectorAdd(GetHeight(pos(%obj),%rail),vectorScale(%rail,(-0.5 * %top) -0.2));
%movement = VectorSub(pos(%obj),%newpos);
%speed = (VectorLen(%movement) / %time)*150;
%tick = vectorScale(vectorNormalize(%movement),%speed);
%obj.setTransform(%newpos SPC rot(%obj));
dynamicMove(%obj,%tick,%time/150,%oldpos);
}

function DynamicMove(%obj,%tick,%times,%end)
{
%pos = posFromTransform(%Obj.getTransform());
%rot = rotFromTransform(%Obj.getTransform());
if (%times < 1)
    %tick = vectorScale(%tick,%times);
%newpos = vectorAdd(%pos,%tick);
%Obj.setTransform(%newpos SPC %rot);
%times=%times-1;
if (%times > 0)
   schedule($AnimationTime, %obj, "dynamicMove", %obj, %tick,%times,%end);
else
   %Obj.setTransform(%end SPC %rot);
}

function getRandomVec() {
	%vec = getRandom() - 0.5 SPC getRandom() - 0.5 SPC getRandom() - 0.5;
	return vectorNormalize(%vec);
}

function validateVal(%val) {
	if (strStr(%val,"nan") != -1 || strStr(%val,"inf") != -1) {
		%val = "";
		error("Rot error no. " @ $RotErrors++);
	}
	return %val;
}

//////// Dfunctions additon Volume 2
//////// 1-9-2002

///Returns the vector off by a certain amount.
function VectorMiss(%vec)
{
%x = (getRandom() - 0.5) * 2 * 3.1415926 * %miss/10000;
%y = (getRandom() - 0.5) * 2 * 3.1415926 * %miss/10000;
%z = (getRandom() - 0.5) * 2 * 3.1415926 * %miss/10000;
%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
return validateVal(MatrixMulVector(%mat, %vec));
}

//Returns the Volume of the object's box
function GameBase::GetVolume(%obj)
{
%size = VectorMultiply(VectorSub(GetWords(%obj.getObjectbox(),0,2),GetWords(%obj.getObjectbox(),3,5)),%obj.getScale());
return VectorDot(%size,%size);
}

//Returns the surfaces of the object's box
function GameBase::GetSurface(%obj)
{
%size = VectorMultiply(VectorSub(GetWords(%obj.getObjectbox(),0,2),GetWords(%obj.getObjectbox(),3,5)),%obj.getScale());
%sizex = GetWord(%size,1)*GetWord(%size,2);
%sizey = GetWord(%size,0)*GetWord(%size,2);
%sizez = GetWord(%size,0)*GetWord(%size,1);
return %sizex SPC %sizey SPC %sizez;
}

//Limits the value between boundries

function limit(%value,%min,%max)
{
if (%value > %max && %max !$= "")
   return %max;
if (%value < %min && %min !$= "")
   return %min;
return %value;
}

//Returns -1 or 1

function randomlev()
{
 if(getRandom(1))
      return 1;
   else
      return -1;
}

//Returns if the target is an forcefield
function GameBase::isforcefield(%target)
{
if (%target && isObject(%target))
   {
   if (%target.getDatablock().classname $= "forcefield")
      return 1;
   }
return "";
}


//A random bool going from always 0 when the imputed value is below min
//To awlays 1 when the imputed value is above max
function Change(%value,%min,%max)
{
return ((%value-%min)/(%max-%min)) > getRandom();
}

//Returns a random player
function Randomplayer()
{
if (!ClientGroup.getCount())
   return "";

%client = ClientGroup.getObject(GetRandom()*ClientGroup.getCount());
return %client.player;
}

//Retuns a random deployable
function randomdeployable()
{
%group = nameToID("MissionCleanup/Deployables");
%count = %group.getCount();

if (!%count)
   return "";

return %group.getObject(getRandom()*%count);
}

//Checks if there's line of sight between objects
function los(%obj1,%obj2,%offset1,%offset2,%mask) {
	if (!%mask)
		%mask = -1;
	%start = pos(%obj1);
	%end = pos(%obj2);
	if (%offset1)
		%start = vectorAdd(%start,realvec(%obj1,%offset1));
	if (%offset2)
		%end = vectorAdd(%end,realvec(%obj2,%offset2));
	%res = containerRayCast(%start,%end,%mask,%obj1);
	return (%res == %obj2);
}


//Returns the closests player to the given location
function closestPlayer(%location) {
	%dis = "";

	if (!ClientGroup.getCount())
		return "";

	%tplayer = "";
	for( %c = 0; %c < ClientGroup.getCount(); %c++ ) {
		%client = ClientGroup.getObject(%c);
		%player = %client.player;
		%pos = pos(%player);
		%dist = vectorDist(%location,%pos);
		if ($MTC_Loaded = true) {
			if (!%dis || %dist < %dis && MTCCleanTarget(%player)) {
				%tplayer = %player;
				%dis = %dist;
			}
		}
		else {
			if (!%dis || %dist < %dis) {
				%tplayer = %player;
				%dis = %dist;
			}
		}
	}
	return %tplayer SPC %dist;
}

//Moderate

//Returns Location information about the location.
function aboveground(%pos,%work,%obj)
       {
        ///1 Give ground under feet
        %mask = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType;
	%res1 = containerRayCast(%pos, GetWords(%pos,0,1) SPC "-10000", %mask,0);
	if (%res1)
               {
               if (%res1.getClassName() $= TerrainBlock)
                   %ground = 1;
               else
                   %ground = 2;
               }
	else
		%ground = "";

       ///2 Give cieling
       if (%work > 0)
          {
          %res2 = containerRayCast(%pos,vectorAdd(%pos,"0 0 10000"), %mask,0);
        	if (%res2)
                   {
                   if (%res2.getClassName() $= TerrainBlock)
                       %cieling = 1;
                   else
                       %cieling = 2;
                   }
        	else
	               %cieling = "";

           }
        if (%work > 1)
           {
           if (%res1)
               %down = VectorDist(posFromRaycast(%res1),%pos);
           if (%res2)
               %up =  VectorDist(posFromRaycast(%res2),%pos);
           }


/// void = 0
/// terrain = 1
/// Interior = 2
/// Ground-Cieling;

%answer = %ground + %cieling*3;
switch$ (%answer) {
		case 0:
			return "underground" SPC %down SPC %up; //0-0
		case 1:
			return "open" SPC %down SPC %up; //1-0
		case 2:
			return "roof" SPC %down SPC %up; //2-0
		case 3:
			return "underground" SPC %down SPC %up; //0-1
		case 4:
			return "odd-t" SPC %down SPC %up; //1-1
		case 5:
			return "odd-int" SPC %down SPC %up; //2-1
		case 6:
			return "underbuilding" SPC %down SPC %up; //0-2
		case 7:
			return "shadow" SPC %down SPC %up; //1-2
		case 8:
			return "inside" SPC %down SPC %up; //2-2
                }
}


//Returns wetehr or not we can see the top of the object above ground.
function GameBase::seetop(%obj)
{
%res = containerRayCast(vectorAdd(%obj.getWorldboxcenter(),"0 0 1000"),%obj.getWorldboxcenter(),-1,0);
	if (GetWord(%res,0) == %obj)
               return %res;
        else
		return "";
}

/////////////////////////
/////////new since 24-8//
/////////////////////////

///Dfunctions

function MicroAdjust(%pos)
{
%offset = (GetRandom()*0.02 - 0.01) SPC (GetRandom()*0.02 - 0.01) SPC (GetRandom()*0.02 - 0.01);
return vectorAdd(%pos,%offset);
}

function MatrixDot(%vector,%matrix)
{
%count1 = GetWordCount(%vector);
%count2 = GetWordCount(%matrix);
if (%count1 == 2 &&  %count2 == 4)
    return VectorDot(GetWords(%matrix,0,1),%vector) SPC VectorDot(GetWords(%matrix,2,3),%vector);
if (%count1 == 3 &&  %count2 == 9)
    return VectorDot(GetWords(%matrix,0,2),%vector) SPC VectorDot(GetWords(%matrix,3,5),%vector) SPC VectorDot(GetWords(%matrix,6,8),%vector);
}

function MatrixMult(%vector,%matrix)
{
%v1 = vectorScale(GetWords(%matrix,0,2),GetWord(%vector,0));
%v2 = vectorScale(GetWords(%matrix,3,5),GetWord(%vector,1));
%v3 = vectorScale(GetWords(%matrix,6,8),GetWord(%vector,2));
return vectorAdd(%v1,vectorAdd(%v2,%v3));
}

function TransPose(%matrix)
{
%count = GetWordCount(%matrix);
if (%count == 4)
   return GetWord(%matrix,0) SPC GetWord(%matrix,2) SPC GetWord(%matrix,1) SPC GetWord(%matrix,3);
else if (%count == 9)
   return GetWord(%matrix,0) SPC GetWord(%matrix,3) SPC GetWord(%matrix,5) SPC GetWord(%matrix,1) SPC GetWord(%matrix,4) SPC GetWord(%matrix,6) SPC GetWord(%matrix,2) SPC GetWord(%matrix,5) SPC GetWord(%matrix,7);
}


function dCross(%vector,%dir)
{
if (!%dir)
    %dir =1;
return %dir*GetWord(%vector,1) SPC %dir* -1*GetWord(%vector,0);
}


function MatrixfromVector(%vector,%dir)
{
%count = GetWordCount(%vector);
if (%count == 2)
   {
   return %vector SPC dCross(%vector,%dir);
   }
else if (%count == 3)
   {
   %x = GetWord(%vector,0);
   %y = GetWord(%vector,1);
   %z = GetWord(%vector,2);
   %x2 = -1*((%x * %y)/(Mpow(%x,2)+Mpow(%y,2)+Mpow(%z,2)));
   %y2 = (mPow(%x,2) * mPow(%z,2))/(Mpow(%x,2)+Mpow(%y,2)+Mpow(%z,2));
   %z2 = -1*((%y * %z)/(Mpow(%x,2)+Mpow(%y,2)+Mpow(%z,2)));
   %x3 = -1*((%x * %z)/(Mpow(%x,2)+Mpow(%z,2)));
   %z3 = mPow(%x,2)/(Mpow(%x,2)+Mpow(%z,2));
   return %vector SPC %x2 SPC %y2 SPC %z2 SPC %x3 SPC "0" SPC %z3;
   }
}

function Inverse(%matrix)
{
%count = GetWordCount(%matrix);
if (%count == 4)
   {
   %a = GetWord(%matrix,0);
   %b = GetWord(%matrix,1);
   %c = GetWord(%matrix,2);
   %d = GetWord(%matrix,3);
   %prod = (-1*%b * %c + %a * %d);
   return %d/%prod SPC -1*%b/%prod SPC -1*%c / %prod SPC %a / %prod;
   }
else if (%count == 9)
   {
   %a = GetWord(%matrix,0);
   %b = GetWord(%matrix,1);
   %c = GetWord(%matrix,2);
   %e = GetWord(%matrix,3);
   %f = GetWord(%matrix,4);
   %g = GetWord(%matrix,5);
   %h = GetWord(%matrix,6);
   %i = GetWord(%matrix,7);
   %j = GetWord(%matrix,8);
   %prod = -1*%c*%e*%g+%b*%f*%g+%c*%d*%h-%a*%f*%h-%b*%d*%i+%a*%e*%i;
   %a1 = (-1*%f*%h+%e*%i)/%prod;
   %b1 = (-1*%c*%h-%b*%i)/%prod;
   %c1 = (-1*%c*%e+%b*%f)/%prod;
   %d1 = (-1*%f*%g-%d*%i)/%prod;
   %e1 = (-1*%c*%g+%a*%i)/%prod;
   %f1 = (-1*%c*%d-%a*%f)/%prod;
   %g1 = (-1*%e*%g+%d*%h)/%prod;
   %h1 = (-1*%b*%g-%a*%h)/%prod;
   %i1 = (-1*%b*%d+%a*%e)/%prod;
   return %a1 SPC %b1 SPC %c1 SPC %d1 SPC %e1 SPC %f1 SPC %g1 SPC %h1 SPC %i1;
   }
}

function GameBase::GetShapeSize(%obj)
{
return VectorSub(getWords(%obj.getObjectBox(),3,5),getWords(%obj.getObjectBox(),0,2));
}

function GameBase::GetRealSize(%obj)
{
%return = VectorMultiply(%obj.getScale(),%obj.getShapeSize());
if (%obj.getdatablock().getname() $="DeployedCrate11")   //for pad swap
    %return = vectorMultiply(%return,0.121951 SPC 0.238095 SPC 0.243902);
return %return;
}

function GameBase::SetRealSize(%obj,%size)
{
%scale = vectordivide(%size,%obj.getShapeSize());
if (%obj.getdatablock().getname() $="DeployedCrate11") //for pad swap
    %scale = vectorMultiply(%scale,8.20002 SPC 4.2 SPC 4.1);
%obj.setScale(%scale);
}

function GameBase::SetWorldBoxCenter(%obj,%location)
{
%obj.setEdge(%location,"0 0 0");
}

function GameBase::SetEdge(%obj,%location,%offset)
{
%VirCenter = vectorScale(vectorAdd(getWords(%obj.getObjectBox(),3,5),getWords(%obj.getObjectBox(),0,2)),0.5);
%VirOffset = VectorMultiply(vectorScale(%offset,0.5),%obj.getShapeSize());
%realoffset = RealVec(%obj,VectorMultiply(vectorAdd(%VirCenter,%VirOffset),%obj.getScale()));
%pos = vectorAdd(%location,vectorScale(%realoffset,-1));
%obj.setTransform(%pos SPC rot(%obj));
}

function GameBase::GetEdge(%obj,%offset)
{
%VirCenter = vectorScale(vectorAdd(getWords(%obj.getObjectBox(),3,5),getWords(%obj.getObjectBox(),0,2)),0.5);
%VirOffset = VectorMultiply(vectorScale(%offset,0.5),%obj.getShapeSize());
%realoffset = RealVec(%obj,VectorMultiply(vectorAdd(%VirCenter,%VirOffset),%obj.getScale()));
return vectorAdd(GetWords(%obj.getTransform(),0,2),%realoffset);
}


/////////////////////////
/////////new since 1-9//
/////////////////////////

//Replacement function for old calcb
//returns the changes damage instead of the coverage!

function calcBuildingInWay2(%position, %targetObject,%damage,%radius)
        {
	%targetPos = %targetObject.getWorldBoxCenter();
        $SolidCo = 1;
	%sourcePos = %position;

	%mask = $TypeMasks::StaticObjectType;
        %found = containerRayCast(%sourcePos, %targetPos, %mask,%lastfound);
        while (%found && %found != %targetObject && %damage > 0)
                {
      	        //Theoretically this should never happen
		if ((%found.getClassName() $= InteriorInstance) || (%found.getClassName() $= TSStatic))
			return 0;
                %dist = VectorDist(%sourcepos,getWords(%found,1,3));
                %amount = (1.0 - ((%dist / %radius) * 0.88)) * %damage;
                %damage = %amount - (%found.getDamageLeft() * %SolidCo);
                %lastfound = %found;
		%lastsourcepos = GetWords(%found,1,3);
                %found = containerRayCast(%lastsourcePos, %targetPos, %mask,%lastfound);
		}
	return Limit(%damage,0);
}

//Returns the highest value of the vector only

function topvec(%vec) {
	%x = getWord(%vec,0);
	%y = getWord(%vec,1);
	%z = getWord(%vec,2);
	if (mAbs(%x) >= mAbs(%y) && mAbs(%x) >= mAbs(%z))
		return %x SPC "0 0";
	else if (mAbs(%y) >= mAbs(%x) && mAbs(%y) >= mAbs(%z))
		return "0" SPC %y SPC "0";
	else
		return "0 0" SPC %z;
}

//Changed Vector Divide, to be more stable.

function VectorDivide(%vec1,%vec2)
{
%x1 =  getWord(%vec1,0);
%y1 =  getWord(%vec1,1);
%z1 =  getWord(%vec1,2);
%x2 =  getWord(%vec2,0);
%y2 =  getWord(%vec2,1);
%z2 =  getWord(%vec2,2);
%x3 =  0;
%y3 =  0;
%z3 =  0;
if (%x1 != 0||%x2 != 0)
   %x3 = %x1/%x2;
if (%y1 != 0||%y2 != 0)
   %y3 = %y1/%y2;
if (%z1 != 0||%z2 != 0)
   %z3 = %z1/%z2;
return %x3 SPC %y3 SPC %z3;
}


function VirpassVector(%obj,%vec)
{
%virvec = floorvec(vectorNormalize(VirVec(%obj,%vec)),1000);
%mask = floorvec(VectorMultiply(vectorScale(vectorNormalize(%virvec),1/mPow(2,0.5)),%obj.getRealSize()),1000);
%inoutvec = VectorMultiply( vectorNormalize( TopVec( VAbs( VectorDivide( %virvec,%mask ) ))),%obj.getRealSize());
%pass = vectorScale( %virvec,VectorLen( VectorDivide( %inoutvec,%virvec ) ) );
return %pass;
}

function getpassVector(%obj,%vec)
{
return RealVec(%obj,VirpassVector(%obj,%vec));
}

/////////The amout of substance you'd be crossing of an object if you went through the object in %vec direction.
/////////ie. going through the front of a 10 10 1 wall would return 1 going to the side would return 10.

function getpassLength(%obj,%vec)
{
return VectorLen(getpassVector(%obj,%vec));
}

//////// END Dfunctions additon Volume 2
//////// 1-9-2002
