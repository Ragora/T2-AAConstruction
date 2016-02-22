datablock AudioProfile(FwProjectileSound)
{
   filename    = "armor/thrust_UW.wav";
   description = ProjectileLooping3d;
   preload = true;
};

datablock ItemData(MpmLaucher)
{
   className = Pack;
   catagory = "Funky";
   shapeFile = "Turret_Muzzlepoint.dts";
   mass = 1;
   elasticity = 0.0;
   friction = 1.0;
   computeCRC = true;
};

datablock TurretImageData(MpmLauchPoint)
{
shapeFile = "Turret_Muzzlepoint.dts";
item      = PlasmaBarrelLargePack;
offset = "0 0 0";
rotation = "0 1 0 90";
};

///old algo
//%time = Limit(VectorLen(%dist)/20,30,360);
//%z = getWord(%dist,2);
//%x = GetWord(%dist,0) SPC GetWord(%dist,1);
//%x = Mpow(VectorDot(%x,%x),0.5);
//%co1 = mpow(4*(mpow(%x,2))+ mpow(2*%muz*%time+%ac*mpow(%time,2)+2*%z,2),0.5);
//%co2 = %time * (2*%muz * %time+%ac*mpow(%time,2)+2*%z);
//%vector = (%x*%co1)/(%co2) SPC (%co1/(2*%time));
//%vector = VectorAdd(VectorScale(VectorNormalize(GetWords(%dist,0,1)),getWord(%vector,0)) ,"0 0" SPC getWord(%vector,1));
//%p=launchmpm(%start,"0 0 -1",%vector,%time*1000+5000);


//Algo for mpm_missile lauch.
//-Stage1-
//Lauch up a certain amount of time
//-Stage2-
//Turn missile and head for target

function mpm_calc(%start,%end,%up,%dat)
{
if (!IsObject(%dat))
    %dat = MpmMissile1;
if (%up $= "")
    %up = "0 0 1";

%ac = %dat.acceleration; //Acceleration for calc

%vector = VectorSub(%end,%start); //Vector source,target
%dir = VectorNormalize(%vector);

if (mAbs(VectorDot(%up,%dir))==1) //can't fire directly up or down.
	return -1;
%rup = VectorNormalize(VectorCross(%up,%dir));
%up = VectorNormalize(%up);
%ndir = VectorNormalize(VectorCross(VectorCross(%up,%dir),%up));

%z = VectorDot(%up,%dir)*VectorLen(%vector);
%x = VectorDot(%ndir,%dir)*VectorLen(%vector);

%mintime = mSqrt(2*%x)/mSqrt(%ac); //Minium travel time
%time = %mintime + 5;  //Added 5 seconds for slack

//Calculations for preffered arc.
	%root = mSqrt(Mpow(%ac,2)*Mpow(%time,4)-4*Mpow(%x,2));

	%ltimeco1 =  %ac*Mpow(%time,2)+4*%root+8*%z;
	%ltimeco2 = -4*Mpow(%x,2)+%ac*Mpow(%time,2)*%ltimeco1;
	%ltimeco3 = 2*%ac*%time;
	%ltime = (-1*%root+mSqrt(%ltimeco2))/%ltimeco3;

	%a = (%ac*%ltime) /2;
	%loc = %a*%ltime;

	%co = %ac*mPow(%time,2);

	%xx = 2*%x/%co;
	%zz = -1*(%root/%co);

%vz = VectorScale(%up,%zz);
%vx = VectorScale(%ndir,%xx);

%vector = VectorAdd(%vx,%vz);

return %ltime SPC %time SPC %loc SPC %vector SPC %a;
}



function Launch_Mpm(%pos,%dir,%speed,%time,%data)
{
%dir = VectorNormalize(%dir);

	%obj = new Item() 
        	{
	         className = Item;
        	 dataBlock = MpmLaucher;
	        };
	%obj.mountimage(MpmLauchPoint,0);
	%obj.setTransform(%pos);
	%obj.setVelocity(%speed);
	%p = %obj.Fire_MpM(%dir,%data);
	%obj.schedule(50,"delete");

%p.lasterror = "0 0 0";
%p.lasttime = GetSimTime();
if (%time !$=	 "")
    %p.dietime = GetSimTime()+%time;
    %p.schedule(%time,"delete");

%p.addToMPMGroup();
return %p;
}

function ProjectileData::Create(%data,%pos,%dir,%speed)
{
%obj = new Item() 
        	{
	         className = Item;
        	 dataBlock = MpmLaucher;
	        };
	%obj.mountimage(MpmLauchPoint,0);
	%obj.setTransform(%pos);
        if (VectorLen(%speed) > 200)
            %speed = VectorScale(VectorNormalize(%speed),200);
	%obj.setVelocity(%speed);
	%p = %obj.Fire_MpM(%dir,%data);
	%obj.schedule(50,"delete");
return %p;
}

function GameBase::Fire_MpM(%obj,%dir,%data)
{
%speed = %obj.getVelocity();
%classname = %data.classname;
%class = getSubStr(%classname,0,strLen(%classname)-4);
%p = new (%class)() 
          {
          datablock = %data;
          initialDirection = %dir;
          initialPosition  = %obj.getWorldBoxCenter();
          SourceObject = %obj;
          SourceSlot = 0;
          };
%p.StartVelocity = %speed;
%p.createtime=getSimTime();

return %p;
}

function extend(%p,%odir)
{
%dir = %p.InitialDirection;
%pos = VectorAdd(%p.InitialPosition,VectorScale(%dir,-0.5));
%left = VectorNormalize(VectorCross(VectorNormalize(%odir),"0 0 1"));
%p1 = new SeekerProjectile() 
          {
           dataBlock        = mpmmissile2;
           initialDirection = %dir;
           initialPosition  = VectorAdd(%pos,VectorScale(%left,5));
           sourceObject = %p.sourceObject;
          };

%p2 = new SeekerProjectile() 
          {
           dataBlock        = mpmmissile2;
           initialDirection = %dir;
           initialPosition  = VectorAdd(%pos,VectorScale(%left,-5));
           sourceObject = %p.sourceObject;
          };
%p1.createtime=getSimTime();
   %p1.StartVelocity =  %p.sourceObject.GetVelocity();
%p2.createtime=getSimTime();
   %p2.StartVelocity =  %p.sourceObject.GetVelocity();
%dietime = %p.dietime-GetSimTime();
%p1.schedule(%dietime+10000,"delete");
%p2.schedule(%dietime+10000,"delete");
}


