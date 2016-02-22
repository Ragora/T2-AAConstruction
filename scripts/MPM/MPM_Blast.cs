

////////////////////////////////////
////////////////Nova////////////////
////////////////////////////////////

datablock ParticleData(DomeRingParticle):SmokeRingParticle
{
LifetimeMS = "12000";
useInvAlpha = "0";
textureName = "special/Smoke/smoke_001";
animTexName[0] = "special/Smoke/smoke_001";
colors[0] = "1.000000 1.000000 0.500000 1.000000";
colors[1] = "0.900000 0.900000 0.200000 1.200000";
colors[2] = "0.500000 0.500000 0.100000 0.500000";
colors[3] = "0.000000 0.000000 0.100000 0.000000";
};


datablock ParticleData(FireColumParticle) {
	dragCoefficient = "1";
	windCoefficient = "0";
	gravityCoefficient = "0";
	inheritedVelFactor = "0";
	constantAcceleration = "0";
	lifetimeMS = "3000";
	lifetimeVarianceMS = "0";
	spinSpeed = "0";
	spinRandomMin = "-200";
	spinRandomMax = "200";
	useInvAlpha = "0";
	animateTexture = "0";
	framesPerSec = "1";
	textureName = "special/cloudFlash";
	animTexName[0] = "special/cloudFlash";
	colors[0] = "1.000000 1.000000 0.300000 1.000000";
	colors[1] = "1.000000 0.600000 0.100000 0.200000";
	colors[2] = "1.000000 0.400000 0.000000 0.200000";
	colors[3] = "1.000000 0.200000 0.000000 0.000000";
	sizes[0] = "90";
	sizes[1] = "125";
	sizes[2] = "150";
	sizes[3] = "190";
	times[0] = "0";
	times[1] = "0.333";
	times[2] = "0.666";
	times[3] = "1";
};

datablock ParticleData(FireTopParticle):FireColumParticle
        {
	lifetimeMS = "2000";
        };




datablock ParticleEmitterData(DomeRingEmitter1):SmokeRing
{
className = "ParticleEmitterData";
ejectionPeriodMS = "1";
periodVarianceMS = "0";
thetaMin = "95";
thetaMax = "85";
ejectionOffset = "200";
particles = "DomeRingParticle";
};

datablock ParticleEmitterData(DomeRingEmitter2):SmokeRing
{
className = "ParticleEmitterData";
ejectionPeriodMS = "1";
periodVarianceMS = "0";
thetaMin = "95";
thetaMax = "85";
ejectionOffset = "300";
particles = "DomeRingParticle";

};

datablock ParticleEmitterData(FireColumEmitter) {
	className = "ParticleEmitterData";
	ejectionPeriodMS = "1";
	periodVarianceMS = "0";
	ejectionVelocity = "5";
	velocityVariance = "0";
	ejectionOffset = "50";
	thetaMin = "95";
	thetaMax = "85";
	phiReferenceVel = "0";
	phiVariance = "360";
	overrideAdvance = "0";
	orientParticles = "0";
	orientOnVelocity = "1";
	particles = "FireColumParticle";
	lifetimeMS = "-1";
	lifetimeVarianceMS = "0";
	useEmitterSizes = "0";
	useEmitterColors = "0";
	overrideAdvances = "0";
};

datablock ParticleEmitterData(FireTopEmitter):FireColumEmitter
{
	particles = "FireTopParticle";
        thetaMin = "90";
	thetaMax = "90";
        ejectionOffset = "0";
	ejectionVelocity = "50";
	velocityVariance = "50";
};

datablock SeekerProjectileData(DomeStreamProjectile)
{
   projectileShapeName = "turret_muzzlepoint.dts";
   explosion           = "TacNukeFireballFireExp";//"GrenadeExplosion";
   velInheritFactor    = 1.0;    

   baseEmitter         = FireTopEmitter;
   delayEmitter        = FireColumEmitter;

   lifetimeMS          = 8000;
   muzzleVelocity      = 0.1;
   maxVelocity         = 500;
   acceleration        = 50;
};



function dome(%pos,%time,%inside)
{
if (!%time)
   %time = 60000;
shockwave(%pos,%nrm,"InitialProjectile");
//shockwave(%pos,%nrm,"InitialProjectile"); ??
//shockwave(%pos,%nrm,"InitialProjectile");
//shockwave(%pos,%nrm,"InitialProjectile");
for (%i=0; %i < 10;%i++)
    {
    %nrm = mCos(%i*$pi/10) SPC mSin(%i*$pi/10) SPC 0;
    shockwave(VectorAdd(%pos,VectorScale(%nrm,49)),%nrm,"DomeProjectile");
    shockwave(VectorAdd(%pos,VectorScale(%nrm,-49)),VectorScale(%nrm,-1),"DomeProjectile");   
    }
domeColum(%pos,"0 0 1");
domeColum(%pos,"0 0 -1");

%ang = GetRandom()*2*$pi;
%nrm = VectorNormalize(mCos(%ang) SPC mSin(%ang) SPC 2);
Schedule(100,0,"whiteout",%pos,500,2000);
schedule(4000,0,"darkhole",%pos,%nrm,%time,"",%inside);
schedule(2000,0,"holeDebris",%pos);
Schedule(%time,0,"whiteout",%pos,1000,2500);
schedule(%time,0,"tube",%pos,%nrm,%ang);
schedule(%time,0,"delBHPieces",%pos); //remove sucked pieces.
}

function domeColum(%pos,%nrm)
{
%speed = VectorScale(%nrm,200);
%obj = new Item() 
        {
         className = Item;
         dataBlock = MpmLaucher;
        };
%obj.mountimage(MpmLauchPoint,0);
%obj.setTransform(%pos);
%obj.setVelocity(%speed);
%speed = %obj.getVelocity();
%p = new SeekerProjectile() 
          {
          datablock = DomeStreamProjectile;
          initialDirection = VectorScale(%nrm,-1);
          initialPosition  = %obj.getWorldBoxCenter();
          SourceObject = %obj;
          SourceSlot = 0;
          };
%p.StartVelocity = %speed;
%p.createtime=getSimTime();
%obj.delete();

Schedule(0,%p,"domering",%p);
Schedule(4000,%p,"domering",%p);
}

function domering(%p)
{
shockwave(%p.getTransForm(),%p.InitialDirection,"DomeRingProjectile");   
}



////////////////////////////////////
/////////////BlackHole//////////////
////////////////////////////////////


//Hole
//GravStar
//DiskArm
//Disk
//Vent
//Dust

datablock ParticleData(DHHoleParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   
   lifetimeMS           = 6000;
   lifetimeVarianceMS   = 0;
   spinSpeed = "0";
   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0;
   textureName          = "skins/jetpackFlare";
   UseInvAlpha = True;
   colors[0]     = "0.0 0.0 0.0 0.0";
   colors[1]     = "0.0 0.0 0.0 1.0";
   colors[2]     = "0.0 0.0 0.0 1.0";
   colors[3]     = "0.0 0.0 0.0 0.0";

   sizes[0]      = 800;
   sizes[1]      = 800;
   sizes[2]      = 800;
   sizes[3]      = 800;

   times[0]      = 0.25;
   times[1]      = 0.5;
   times[2]      = 0.75;
   times[3]      = 1;

};

datablock ParticleData(DHGravStarParticle)
{
   dragCoeffiecient     = 1.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = -1.0;
   
   lifetimeMS           = 8750;
   lifetimeVarianceMS   = 0;
   constantAcceleration = "-100";
   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0;
   textureName  = "skins/jetpackflare_bio";

   colors[0]     = "1 1 1 0";
   colors[1]     = "1 1 0.7 1";
   colors[2]     = "1 1 0 1";
   colors[3]     = "1 1 1 0";

   sizes[0]      = 20;
   sizes[1]      = 15;
   sizes[2]      = 10;
   sizes[3]      = 5;

   times[0]      = 0.1;
   times[1]      = 0.5;
   times[2]      = 0.9;
   times[3]      = 1;

};


datablock ParticleData(DHInvHoleParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   
   lifetimeMS           = 6000;
   lifetimeVarianceMS   = 0;
   spinSpeed = "0";
   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0;
   textureName          = "skins/jetpackFlare";
   UseInvAlpha = false;
   colors[0]     = "1.0 1.0 1.0 0.0";
   colors[1]     = "1.0 1.0 1.0 1.0";
   colors[2]     = "1.0 1.0 1.0 1.0";
   colors[3]     = "1.0 1.0 1.0 0.0";

   sizes[0]      = 800;
   sizes[1]      = 800;
   sizes[2]      = 800;
   sizes[3]      = 800;

   times[0]      = 0.25;
   times[1]      = 0.5;
   times[2]      = 0.75;
   times[3]      = 1;

};

datablock ParticleData(DHInvGravStarParticle)
{
   dragCoeffiecient     = 1.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = -1.0;
   
   lifetimeMS           = 8750;
   lifetimeVarianceMS   = 0;
   constantAcceleration = "-100";
   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0;
   textureName  = "skins/jetpackflare_bio";

   colors[0]     = "1 1 1 0";
   colors[1]     = "1 1 0.7 1";
   colors[2]     = "1 1 0 1";
   colors[3]     = "1 1 1 0";

   sizes[0]      = 20;
   sizes[1]      = 15;
   sizes[2]      = 10;
   sizes[3]      = 5;

   times[0]      = 0.1;
   times[1]      = 0.5;
   times[2]      = 0.9;
   times[3]      = 1;

};



datablock ParticleData(DHDiskArmParticle):DHGravStarParticle
{
   lifetimeMS      = 4900;
   colors[0]     = "1 0.5 0.5 0.5";
   colors[1]     = "1 0.8 0.5 1";
   colors[2]     = "1 1 0.5 1";
   colors[3]     = "1 1 1 0";


   sizes[0]      = 20;
   sizes[1]      = 15;
   sizes[2]      = 10;
   sizes[3]      = 5;
};



datablock ParticleData(DHDiskParticle)
{
   dragCoeffiecient     = 2.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   
       lifetimeMS           = 12500;
   lifetimeVarianceMS   = 0;
   constantAcceleration = 0;
   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient =0;
   textureName  = "skins/jetpackflare_bio";


 sizes[0]      = 20;
   sizes[1]      = 30;
   sizes[2]      = 40;
   sizes[3]      = 50;

  colors[0]     = "1 0.5 0.5 0.5";
   colors[1]     = "1 1 1 1";
   colors[2]     = "1 0.5 0.5 1";
   colors[3]     = "1 0 1 0";


   times[0]      = 0.1;
   times[1]      = 0.5;
   times[2]      = 0.75;
   times[3]      = 1;

};

datablock ParticleData(DHVentParticle):DHDiskParticle
{
    dragCoeffiecient     = 0.0;
   constantAcceleration = "0.1";
   colors[0]     = "1 1 1 1";
   colors[1]     = "1 1 1 1";
   colors[2]     = "1 0.5 0.5 1";
   colors[3]     = "1 0 1 0";


   sizes[0]      = 20;
   sizes[1]      = 15;
   sizes[2]      = 10;
   sizes[3]      = 5;
};


datablock ParticleData(DHDustParticle)
{
   dragCoeffiecient     = 1.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = -1.0;
   
   lifetimeMS           = 10500;
   lifetimeVarianceMS   = 0;
   constantAcceleration = "-100";
   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.46 0.36 0.26 1";
   colors[1]     = "0.46 0.46 0.36 0.9";
   colors[2]     = "0.46 0.46 0.36 0.7";
   sizes[0]      = 15;
   sizes[1]      = 10;
   sizes[2]      = 5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};


datablock ParticleEmitterData(DHHoleEmitter)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 50;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0.1;
   ejectionoffset = 10;
   phiReferenceVel = "0";
   phiVariance = "360";
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";

   orientParticles  = true;
   orientOnVelocity = false;

   particles = "DHHoleParticle";
   predietime = 6000;
};


datablock ParticleEmitterData(DHGravStarEmitter)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0	;
   ejectionoffset = 400;
   phiReferenceVel = "0";
   phiVariance = "360";
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";

   orientParticles  = true;
   orientOnVelocity = true;

   particles = DHGravStarParticle;
   predietime = 12500;
};


datablock ParticleEmitterData(DHInvHoleEmitter)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 50;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0.1;
   ejectionoffset = 10;
   phiReferenceVel = "0";
   phiVariance = "360";
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";

   orientParticles  = true;
   orientOnVelocity = false;

   particles = "DHInvHoleParticle";
   predietime = 6000;
};


datablock ParticleEmitterData(DHInvGravStarEmitter)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0	;
   ejectionoffset = 10;
   phiReferenceVel = "0";
   phiVariance = "360";
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";

   orientParticles  = true;
   orientOnVelocity = true;

   particles = DHInvGravStarParticle;
   predietime = 12500;
};


datablock ParticleEmitterData(DHDiskArmEmitter)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 20;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0	;
   ejectionoffset = 150;
   phiReferenceVel = "20";
   phiVariance = "30";
   thetaMin         = 90.0;
   thetaMax         = 90.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";

   orientParticles  = true;
   orientOnVelocity = true;

   particles = DHDiskarmParticle;
   predietime = 13000;
};

datablock ParticleEmitterData(DHDiskEmitter)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 30;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 2;
   ejectionoffset = 150;
   phiReferenceVel = "20";
   phiVariance = "50";
   thetaMin         = 90.0;
   thetaMax         = 90.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";

   orientParticles  = true;
   orientOnVelocity = true;

   particles = DHDiskParticle;
   predietime = 13000;
};



datablock ParticleEmitterData(DHDiskEmitter2)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 30;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 2;
   ejectionoffset = 135;
   phiReferenceVel = "20";
   phiVariance = "50";
   thetaMin         = 90.0;
   thetaMax         = 90.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";

   orientParticles  = true;
   orientOnVelocity = true;

   particles = DHDiskParticle;
   predietime = 13000;
};




datablock ParticleEmitterData(DHVentEmitter)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 30;
   periodVarianceMS = 0;

   ejectionVelocity = 25;
   velocityVariance = 0;
   ejectionoffset = 50;
   phiReferenceVel = "0";
   phiVariance = "360";
   thetaMin         = 0.0;
   thetaMax         = 5.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";

   orientParticles  = true;
   orientOnVelocity = true;

   particles = DHVentParticle;
   predietime = 3000;
};


datablock ParticleEmitterData(DHDustEmitter)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 20;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0	;
   ejectionoffset = 600;
   phiReferenceVel = "20";
   phiVariance = "20";
   thetaMin         = 15.0;
   thetaMax         = 20.0;
   spinRandomMin = "-200";
   spinRandomMax = "200";

   orientParticles  = false;
   orientOnVelocity = false;

   particles = DHDustParticle;
   predietime = 13000;
};



datablock ParticleData(ArcParticle) {
	dragCoeffiecient     = 0.0;
	gravityCoefficient   = -0.2;
	inheritedVelFactor   = 0.0;

	lifetimeMS           = 2000;
	lifetimeVarianceMS   = 0;

	textureName          = "particleTest";

	useInvAlpha = false;
	spinRandomMin = -160.0;
	spinRandomMax = 160.0;

	animateTexture = true;
	framesPerSec = 15;

	animTexName[0]       = "special/Explosion/exp_0016";
	animTexName[1]       = "special/Explosion/exp_0018";
	animTexName[2]       = "special/Explosion/exp_0020";
	animTexName[3]       = "special/Explosion/exp_0022";
	animTexName[4]       = "special/Explosion/exp_0024";
	animTexName[5]       = "special/Explosion/exp_0026";
	animTexName[6]       = "special/Explosion/exp_0028";
	animTexName[7]       = "special/Explosion/exp_0030";
	animTexName[8]       = "special/Explosion/exp_0032";

	colors[0]     = "1.0 0.7 0.5 1.0";
	colors[1]     = "1.0 0.5 0.2 1.0";
	colors[2]     = "1.0 0.25 0.1 0.0";
	sizes[0]      = 2.0;
	sizes[1]      = 1.0;
	sizes[2]      = 0.5;
	times[0]      = 0.0;
	times[1]      = 0.2;
	times[2]      = 1.0;
};

datablock ParticleEmitterData(ArcEmitter) {
	ejectionPeriodMS = 5;
	periodVarianceMS = 1;

	ejectionVelocity = 0.25;
	velocityVariance = 0.0;

	thetaMin         = 90.0;
	thetaMax         = 90.0;

	particles = "ArcParticle";
};


datablock SeekerProjectileData(ArcProjectile3)
{
   projectileShapeName = "turret_muzzlepoint.dts";
  explosion            = PlasmaBoltExplosion;
	splash               = PlasmaSplash;
   velInheritFactor    = 1.0;    


   baseEmitter  = ArcEmitter;
   delayEmitter = DebrisFireEmitter;

   lifetimeMS          = -1;
   muzzleVelocity      = 0.1;
   maxVelocity         = 500;
   acceleration        = 5;
};

datablock SeekerProjectileData(ArcProjectile2)
{
   projectileShapeName = "turret_muzzlepoint.dts";
  explosion            = PlasmaBoltExplosion;
	splash               = PlasmaSplash;
   velInheritFactor    = 1.0;    


   baseEmitter  = DebrisFireEmitter;
   delayEmitter = VSmokeSpikeEmitter;

   lifetimeMS          = -1;
   muzzleVelocity      = 0.1;
   maxVelocity         = 500;
   acceleration        = 5;
};


function darkhole(%pos,%nrm,%time,%destination,%inside)
{
if (%nrm $= "" || vAbs(%nrm) $= "0 0 1")
    {
    %nrm = "0 0 1";
    %rot = "1 0 0 0";
    %rot2 = "1 0 0 3.14";
    }
else
    {
    %rot = fullrot(%nrm,"0 0 1");
    %rot2 = fullrot(VectorScale(%nrm,-1),"0 0 1");
    }

%mainGroup = nameToID("MissionCleanup/HoleGroup");
   if (%mainGroup <= 0) 
      {
      %mainGroup = new SimGroup("HoleGroup");
      MissionCleanup.add(%Group);
      }
%group = new SimGroup("Hole"@ %maingroup.holes);

//If there's no general destination transform, make one.
//Destination is an offset so two b-holes near eachother will have destinations near eachother.
if ($destination $= "")
    {
    if (vAbs(%nrm) $= "0 0 1")
       %group.destination = VectorAdd(VectorScale(VectorNormalize(GetRandom()-0.5 SPC GetRandom()-0.5),GetRandom()*50000+10000),%pos);
    else
       %group.destination = VectorAdd(VectorScale(VectorNormalize(getWords(%nrm,0,1)),GetRandom()*50000+10000),%pos);
    $destination = %group.destination;
    }

%group.destination = VectorAdd($destination,%pos);
%group.inside = %inside;
%group.rot = %rot;
%group.rot2 = %rot2;
%group.pos = %pos;
%group.nrm = %nrm;
%group.syncTime = GetSimTime();
if ((getWord(%pos,2)-getTerrainHeight(%pos))<400)
   %group.dust = 1;
//echo("dust is:" SPC %group.dust);
%maingroup.add(%group);
%maingroup.holes++;
%killtime = GetSimTime()+%time;
%group.darkholeFase(0);
if (!%time || %time > 4500)
%group.schedule(4500,"darkholeFase",1);
if (!%time || %time > 9000)
%group.schedule(9000,"darkholeFase",2);
if (!%time || %time > 13500)
%group.schedule(13500,"darkholeFase",3);
if (%time)
   {
   %group.killtime = GetSimTime()+%time;
   %group.schedule(%time-13000,"HoleStop");
   }
return %group;
}

function SimGroup::DarkHoleFase(%group,%fase)
{
if (%group.stopped)
    return "";
%pos = %group.pos;
%pos2 = %group.destination;
//echo(%pos2);
%rot = %group.rot;
%rot2 = %group.rot2;
%inside= %group.inside;
if (%fase == 0)
   {
   if (!%inside)
   %p1 =CreateEmitter(%pos,DHDiskEmitter,%rot);
   %p2 = CreateEmitter(%pos,DHGravStarEmitter,%rot);
   if (%group.dust)
   %p3 = CreateEmitter(%pos,DHDustEmitter,"1 0 0 3.14");
   %p4 = CreateEmitter(%pos,DHDiskArmEmitter,%rot);
   }
else if (%fase == 1)
   {
   if (!%inside)   
   %p1 =CreateEmitter(%pos,DHDiskEmitter2,%rot);
   %p2 =CreateEmitter(%pos,DHDiskArmEmitter,%rot);
   %p3 = %group.invhole = CreateEmitter(%pos2,DHInvHoleEmitter,%rot);
   %p4 = CreateEmitter(%pos2,DHInvGravStarEmitter,%rot);
   }
else if (%fase == 2)
   {
   %p1 = %group.hole = CreateEmitter(%pos,DHHoleEmitter,%rot);
   %p1.holesuck();
   %p2 = CreateEmitter(%pos,DHDiskARmEmitter,%rot);
   if (%group.dust)
   %p3 = CreateEmitter(%pos,DHDustEmitter,"1 0 0 3.14");
   if (!%inside)
   %p4 = CreateEmitter(%pos,DHDiskEmitter,%rot);
   }
else if (%fase == 3)
   {
   %p1 =CreateEmitter(%pos,DHDiskArmEmitter,%rot);
   if (!%inside)
   %p2 =CreateEmitter(%pos,DHDiskEmitter2,%rot);
   %p3 =CreateEmitter(%pos,DHVentEmitter,%rot);
   %p4 =CreateEmitter(%pos,DHVentEmitter,%rot2);
   }
if (%p1)
   {
   %p1.fase = %fase;
   %group.add(%p1);
   }
if (%p2)
   {
   %p2.fase = %fase;
   %group.add(%p2);
   }
if (%p3)
   {
   %p3.fase = %fase;
   %group.add(%p3);
   }
if (%p4)
    {
    %p4.fase = %fase;
    %group.add(%p4);
    }


}

function SimGroup::HoleStop(%group)
{
%group.stopped = 1;
for (%i=0; %i < %group.getCount();%i++)
    {
    %obj = %group.getObject(%i);
    %time = %obj.Emitter.predietime;
    %obj.schedule(13000-%time,"delete");
    }
%group.schedule(14000,"delete");
}

function SimGroup::HoleSeqStop(%group,%seq)
{
for (%i=0;%i<%group.getCount();%i++)
    {
    if (%group.getObject(%i).fase==%seq)
        %group.getObject(%i).delete();
    }
if (%seq == 3)
   %group.delete();
}

function holemovenrm()
{
Cancel($holemovenrm);
for (%i=0;%i<HoleGroup.getCount();%i++)
    {
    %group = HoleGroup.getObject(%i);
    %group.holemove(VectorAdd(%group.pos,GetWords(%group.nrm,0,1)) SPC "0",%group.nrm);
    }
$holemovenrm = schedule(20000,0,"HoleMovenrm");
}

function SimGroup::holeMove(%group,%pos,%nrm)
{
%synctime = (mCeil((getSimTime()-%group.getSyncTime)/18000)*18000)-getSimTime();
%group.schedule(%synctime,"HoleSeqStop",0);
%group.schedule(%synctime+4500,"HoleSeqStop",1);
%group.schedule(%synctime+9000,"HoleSeqStop",2);
%group.schedule(%synctime+13500,"HoleSeqStop",3);
if (%group.killtime)
    %time = %group.killtime-GetSimTime()-%synctime;
%vec = VectorSub(%Group.pos,%pos); 
schedule(%synctime,0,"darkhole",%pos,%nrm,%time,VectorAdd(%group.destination,%vec));
}


function HoleDebris(%pos)
{
for (%i=0;%i<50;%i++)
    {    
    arcDebris(%pos);
    }
}

function arcDebris(%pos,%nrm)
{
if (!%nrm)
    %nrm = VectorNormalize(GetRandom() -0.5 SPC GetRandom()-0.5 SPC GetRandom()-0.5);
%range = GetRandom()*70+10;
%r = GetRandom()*0.2-0.1;
%speed = VectorScale(%nrm,-1*VectorLen(arcSpeed(%nrm,VectorScale(%nrm,-1*%range))));
%nrm = VectorNormalize(VectorAdd(%nrm,VectorScale(VectorCross(%nrm,"0 0 1"),%r)));
%p = arc(%pos,%nrm,%speed);
//%time = mSolveQuadratic(-5,VectorLen(%speed),%offset;
%p.schedule((%range*1800)/5,"delete");
}

function arcspeed(%nrm,%speed)
{
%co1 = VectorLen(%speed);
%co2 = VectorDot(%speed,%nrm);
return VectorScale(%speed,mAbs((%co1/%co2)));
}

function arc(%pos,%speed,%dir)
{
%obj = new Item() 
        {
         className = Item;
         dataBlock = MpmLaucher;
        };
%obj.mountimage(MpmLauchPoint,0);
%obj.setTransform(%pos);
%obj.setVelocity(%speed);
%speed = %obj.getVelocity();
%dat = "ArcProjectile"@ (2+ GetRandom(1));
%p = new SeekerProjectile() 
          {
          datablock = %dat;
          initialDirection = %dir;
          initialPosition  = %obj.getWorldBoxCenter();
          SourceObject = %obj;
          SourceSlot = 0;
          };
%p.StartVelocity = %speed;
%p.createtime=getSimTime();
%obj.delete();
return %p;
}



function arora()
{
Cancel($arorsch);

%ang = GetRandom()*2*$pi;
%pos = VectorScale(mCos(%ang) SPC mSin(%ang) SPC 0,2000+getRandom()*1000);
dome($nextpos,1140000);

%ang = GetRandom()*2*$pi;
%pos = VectorScale(mCos(%ang) SPC mSin(%ang) SPC 0,2000+getRandom()*1000);
%ang = GetRandom()*2*$pi;
%nrm = VectorNormalize(mCos(%ang) SPC mSin(%ang) SPC 2);
$arorsch = schedule(1200000,0,"arora");
$nextpos = VectorAdd(%pos,"0 0 350");
%wp1= new WayPoint() {
	position = $nextpos;
	rotation = "1 0 0 0";
	scale = "1 1 1";
	dataBlock = "WayPointMarker";
	team = "1";
};
%wp1.name = "Next Hole";
%wp1.schedule(1200000,"delete");
}



function GameBase::HoleSuck(%obj)
{
Cancel(%obj.sucksh);
%group = nameToID("MissionCleanup/Deployables");
%pos = %obj.getWorldBoxCenter();
%area = 1000;
  InitContainerRadiusSearch(%pos, %area, $TypeMasks::VehicleObjectType |$TypeMasks::StaticShapeObjectType | $TypeMasks::PlayerObjectType| $TypeMasks::ItemObjectType | $TypeMasks::CorpseObjectType);

   while ((%targetObject = containerSearchNext()) != 0)
   {
   if((%targetObject.getType() & $TypeMasks::StaticShapeObjectType) &&  !(%targetObject.getType() & ($TypeMasks::VehicleObjectType | $TypeMasks::PlayerObjectType)) && %moved >= 20)
      continue;
      %dist = containerSearchCurrRadDamageDist(); 
      %distPct = %dist / %area;
   if (!%targetobject.HasEye(%pos)&& %dist >10)   
      continue;

     if (%area < 2000)
        %power = %area;
     else
        %power = Limit(500 - %area,0,200);

     if (%dist < 50) //Shockwave
        {
        %force = (%power/100)*%targetobject.getDatablock().mass;
        %damage = Limit(100/%dist,0,100); 
        %upvec = "0 0 1";
        }
      else
        {
        %force = (%power/100)*%targetobject.getDatablock().mass;
        %damage = 0;
        %upvec = "";
        }

      %tgtPos = %targetObject.getWorldBoxCenter();
      %vec = VectorNormalize(VectorSub(%pos,%tgtpos));
      %nForce = Limit(2000-%dist,0)*(%targetobject.getDatablock().mass/45); 
      %rotmod = VectorCross(%group.nrm,%vec);
      %forceVector = VectorAdd(vectorScale(%vec, %nForce),%rotmod);
      if((%targetObject.getType() & $TypeMasks::StaticShapeObjectType) &&  !(%targetObject.getType() & ($TypeMasks::VehicleObjectType | $TypeMasks::PlayerObjectType))&& %moved < 20)
         {
         if (!$host::invincibleDeployables)
            {
            if (%group.isMember(%targetobject)&& GetRandom()*%moved<5 && (!%targetobject.isSticky() || %dist < 50))
               {
               if (%dist<10)
                  {
                  arcDebris(%pos);
                  %targetObject.SetDamageState("Destroyed");
                  }
               %ttime = GetRandom()*400+100;
               %targetObject.Schedule(%ttime,"SuckTo",%pos);
               %targetObject.getDataBlock().damageObject( %targetObject, 0,pos( %targetObject),((2000-%dist)/200000),$DamageType::Explosion);                 
               %moved++;
               }
              }
            }
       else 
          {
          if (%damage>0.01)
           {
                          
               if (GetRandom()*100 < 25+%mod)
                  {
                  %pos2 = VectorAdd(pos(%obj.getGroup().invhole),VectorSub(%tgtpos,%pos));
                  %targetObject.SetTransform(%pos2 SPC %targetObject.GetRotation());                   
                  }
               else
                  {
                  %targetObject.getDataBlock().damageObject( %targetObject, %obj,pos( %targetObject),%damage,$DamageType::Explosion);                                   
                  }
           }                                
          
         
          
         }
    if (!%targetObject.deployed && !($host::invinciblearmors && (%targetObject.getType() & $TypeMasks::PlayerObjectType)))
        %targetObject.applyImpulse(pos(%targetObject), %forceVector);
   }
  InitContainerRadiusSearch(pos(%obj.getGroup().invhole), 50, $TypeMasks::VehicleObjectType | $TypeMasks::PlayerObjectType| $TypeMasks::ItemObjectType | $TypeMasks::CorpseObjectType);

   while ((%targetObject = containerSearchNext()) != 0)
   {
      %tgtPos = %targetObject.getWorldBoxCenter();
      %vec = VectorNormalize(VectorSub(pos(%obj.getGroup().invhole),%tgtpos));
      %nForce = Limit(2000-%dist,0)*(%targetobject.getDatablock().mass/45); 
      %rotmod = VectorCross(%group.nrm,%vec);
      %forceVector = vectorScale(%vec, -1*%nForce);
      %targetObject.applyImpulse(pos(%targetObject), %forceVector);
   }

%obj.sucksh = %obj.schedule(500,"HoleSuck");
}

function GameBase::HasEye(%obj,%pos)
{
%mask = $TypeMasks::StaticShapeObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::InteriorObjectType;
%res = containerRayCast(%pos,%obj.getWorldBoxCenter(),%mask, %obj);
return !isObject(%res);
}

function GameBase::isSticky(%obj)
{
if (IsObject(%obj.Dsurface) && (%obj.dSurface.getType() & $TypeMasks::StaticShapeObjectType))
   {
    if (%obj.Dsurface.getDataBlock().className $= "floor")
       return 1;
    if (%obj.Dsurface.getDataBlock().className $= "mSpine")
       return 1;
   }
if (%obj.getDataBlock().className $= "floor")
    return 1;
if (%obj.getDataBlock().className $= "mspine")
    return 1;
return 0;
}

function GameBase::SuckTo(%obj,%pos)
{
%opos = %obj.getWorldBoxCenter();
%orot = %obj.getRotation();
%dir = VectorNormalize(VectorSub(%pos,%opos));
%dist = VectorDist(%pos,%opos);
%head = VectorNormalize(topvec(%obj.getRealSize()));
%scaler = VectorAdd(VectorScale(%head,1.01),VectorScale(InvFace(%head),0.99));
%mForce = Limit(mpow(2000-%dist,0.5),0); 
%oldup = Realvec(%obj,"0 0 1");
%oldfo = Realvec(%obj,"1 0 0");
if (%head $= "1 0 0")
   {
   %newfo = %dir;
   %newup = VectorCross(VectorCross(%dir,%oldup),%dir);
   }
else if (%head $= "0 1 0")
   {
   %newfo = VectorCross(%dir,%oldup);
   %newup = VectorCross(%oldfo,%dir);
   }
else 
   {
   %newup = %dir;
   %newfo = VectorCross(VectorCross(%dir,%oldfo),%dir);
   }
%newup = VectorScale(%newup,0.05);
%newfo = VectorScale(%newfo,0.05);
%newrot = FullRot(VectorNormalize(VectorAdd(%oldup,%newup)),VectorNormalize(VectorAdd(%oldfo,%newfo)));
%newscale = VectorMultiply(%obj.getScale(),%scaler);
%obj.SetRotation(%newrot);
%obj.setScale(%newscale);
%newpos = VectorAdd(%obj.getEdge(VectorScale(%head,0)),VectorScale(%dir,(2000-%dist)/200));
%obj.setEdge(%newpos,VectorScale(%head,0));
%obj.wassucked = 1;
}

function delBHPieces(%pos) {

	%randomTime = 10000;
	%group = nameToID("MissionCleanup/Deployables");
	%count = %group.getCount();
	for(%i=0;%i<%count;%i++) {
		%obj =  %group.getObject(%i);
		if (%obj.wassucked) {                        
			%time = getRandom() * %randomTime;
                        if (%pos !$= "")
                            %time = VectorDist(%pos,%obj.getTransform())*1000/60;
			%obj.getDataBlock().schedule(%time,"disassemble",%plyr,%obj); // Run Item Specific code.
			%deleted++;
		}
		else
			%checked++;
	}
	
	return %randomTime;
}


///////////////////////////////////
///////////////Rift////////////////
///////////////////////////////////

datablock ParticleData(SmokeRingParticle) {
		dragCoefficient = "0.6";
	windCoefficient = "0";
	gravityCoefficient = "-0.1";
	inheritedVelFactor = "0";
	constantAcceleration = "0";
	lifetimeMS = "5000"; //11000
	lifetimeVarianceMS = "0";
	spinSpeed = "0";
	spinRandomMin = "-30";
	spinRandomMax = "30";
	useInvAlpha = "0";
	animateTexture = "0";
	framesPerSec = "1";
	textureName = "special/cloudFlash";
	animTexName[0] = "special/cloudFlash";
	colors[0] = "0.950000 0.950000 1.000000 0.000000";
	colors[1] = "0.800000 0.800000 0.950000 0.200000";
	colors[2] = "0.800000 0.800000 0.950000 0.100000";
	colors[3] = "0.550000 0.550000 0.950000 0.000000";
	sizes[0] = "10";
	sizes[1] = "20";
	sizes[2] = "20";
	sizes[3] = "20";
	times[0] = "0";
	times[1] = "0.1";
	times[2] = "0.7";
	times[3] = "1";
};



datablock ParticleEmitterData(SmokeRing) {
	className = "ParticleEmitterData";
	ejectionPeriodMS = "1";
	periodVarianceMS = "0";
	ejectionVelocity = "5";
	velocityVariance = "0";
	ejectionOffset = "200";
	thetaMin = "95";
	thetaMax = "85";
	phiReferenceVel = "0";
	phiVariance = "360";
	overrideAdvance = "0";
	orientParticles = "0";
	orientOnVelocity = "1";
	particles = "SmokeRingParticle";
	lifetimeMS = "-1";
	lifetimeVarianceMS = "0";
	useEmitterSizes = "0";
	useEmitterColors = "0";
	overrideAdvances = "0";
};


datablock LinearProjectileData(SmokeProjectile) {
	className = "LinearProjectileData";
	projectileShapeName = "turret_muzzlepoint.dts";
	baseEmitter = "mpmFlareEmitter1";
	delayEmitter = "SmokeRing";
	Explosion = "";
	hasLight = "1";
	lightRadius = "20";
	lightColor = "0.500000 0.500000 1.000000 1.000000";	
	dryVelocity = "12";
	wetVelocity = "12";
	fizzleTimeMS = "12000";
	lifetimeMS = "12000";
	explodeOnDeath = "1";
};


function tube(%pos,%nrm,%ang)
{
%pos2 = %pos;
CreateLifeEmitter(%pos,mpmFlareEmitter1,6000);
Schedule(6000,0,"smoketube",%pos,%nrm);
Schedule(6000,0,"smoketube",%pos,VectorScale(%nrm,-1));
//Schedule(3000,0,"shockwave",VectorAdd(%pos,VectorScale(%nrm,-100)),%nrm,"HugeShProjectile");
Schedule(3000,0,"shockwave",VectorAdd(%pos,VectorScale(%nrm,-100)),VectorNormalize(mCos(%ang) SPC mSin(%ang) SPC 1.5),"HugeShProjectile");
Schedule(3000,0,"shockwave",VectorAdd(%pos,VectorScale(%nrm,-100)),VectorNormalize(mCos(%ang) SPC mSin(%ang) SPC 3),"HugeShProjectile");
for (%i=0; %i < 20;%i++)
    {
    %pos = VectorAdd(%pos,VectorScale(%nrm,5));
    %pos2 = VectorAdd(%pos2,VectorScale(%nrm,-5));
    Schedule(%i*400,0,"shockwave",%pos,%nrm);
    Schedule(%i*400,0,"shockwave",%pos2,VectorScale(%nrm,-1));
    }
}

function smoketube(%pos,%nrm)
{
%p1 = new LinearProjectile() 
          {
           dataBlock        = SmokeProjectile;
           initialDirection = %nrm;
           initialPosition  = %pos;           
          };
}


///////////////////////////
/////////ShockWaves////////
///////////////////////////

datablock ShockwaveData(BaseShockWave) {
	className = "ShockwaveData";
	scale = "1 1 1";
	delayMS = "0";
	delayVariance = "0";
	lifetimeMS = "12000";
	lifetimeVariance = "0";
	width = "5";
	numSegments = "20";
	numVertSegments = "10";
	velocity = "-30";
	height = "20";
	verticalCurve = "5";
	acceleration = "5";
	times[0] = "0";
	times[1] = "0.5";
	times[2] = "1";
	times[3] = "1";
	colors[0] = "0.000000 0.000000 0.500000 0.100000"; //1.0 0.9 0.9
	colors[1] = "0.500000 0.500000 1.000000 1.000000"; //0.6 0.6 0.6
	colors[2] = "0.500000 0.500000 1.000000 0.100000"; //0.6 0.6 0.6
	colors[3] = "1.000000 1.000000 1.000000 1.000000";
	texture[0] = "special/shockwave4";
	texture[1] = "special/gradient";
	texWrap = "7";
	is2D = "0";
	mapToTerrain = "0";
	orientToNormal = "1";
	renderBottom = "1";
	renderSquare = "0";
};


datablock ShockwaveData(DomeShockWave) {
	className = "ShockwaveData";
	delayMS = "0";
	delayVariance = "0";
	lifetimeMS = "8000";
	lifetimeVariance = "0";
	width = "5";
	numSegments = "80";
	numVertSegments = "10";
	velocity = "-200";
	height = "62";
	verticalCurve = "2.5";
	acceleration = "50";
	times[0] = "0";
	times[1] = "0.5";
	times[2] = "1";
	times[3] = "1";
	colors[0] = "1.000000 1.000000 0.500000 0.100000"; //1.0 0.9 0.9
	colors[1] = "1.000000 1.000000 0.500000 1.000000"; //0.6 0.6 0.6
	colors[2] = "1.000000 1.000000 0.500000 0.100000"; //0.6 0.6 0.6
	colors[3] = "1.000000 1.000000 1.000000 1.000000";
	texture[0] = "special/whitenoAlpha";
	texture[1] = "special/whitenoAlpha";
	texWrap = "7";
	orientToNormal = "1";
	renderBottom = "1";
        mapToTerrain = "0";
};

datablock ShockwaveData(HugeShockWave) 
        {
	className = "ShockwaveData";
	delayMS = "0";
	delayVariance = "0";
	lifetimeMS = "20000";
	lifetimeVariance = "0";
	width = "500";
	numSegments = "80";
	numVertSegments = "10";
	velocity = "60";
	height = "40";
	verticalCurve = "10";
	acceleration = "-4";
	times[0] = "0";
	times[1] = "0.1";
	times[2] = "0.9";
	times[3] = "1";
	colors[0] = "0.000000 0.000000 0.500000 0.100000"; //1.0 0.9 0.9
	colors[1] = "0.500000 0.500000 1.000000 1.000000"; //0.6 0.6 0.6
	colors[2] = "0.500000 0.500000 1.000000 1.000000"; //0.6 0.6 0.6
	colors[3] = "1.000000 1.000000 1.000000 0.000000";
	texture[0] = "special/shockwave4";
	texture[1] = "special/gradient";
	texWrap = "7";
	orientToNormal = "1";
	renderBottom = "1";
        mapToTerrain = "0";
};



datablock ShockwaveData(InitialShockWave) {
	className = "ShockwaveData";
	scale = "1 1 1";
	delayMS = "0";
	delayVariance = "0";
	lifetimeMS = "11000";
	lifetimeVariance = "0";
	width = "20";
	numSegments = "240";
	numVertSegments = "12";
	velocity = "150";
	height = "100";
	verticalCurve = "0.95";
	acceleration = "0";
	times[0] = "0";
	times[1] = "0.5";
	times[2] = "1";
	times[3] = "1";
	colors[0] = "1.000000 1.000000 0.300000 1.000000"; //1.0 0.9 0.9
	colors[1] = "1.000000 0.600000 0.100000 0.600000"; //0.6 0.6 0.6
	colors[2] = "1.000000 0.400000 0.000000 0.000000"; //0.6 0.6 0.6
	colors[3] = "1.000000 1.000000 1.000000 1.000000";
	texture[0] = "special/shockwave4";
	texture[1] = "special/gradient";
	texWrap = "7";
	is2D = "0";
	mapToTerrain = "1";
	orientToNormal = "0";
	renderBottom = "1";
	renderSquare = "0";
};

datablock ExplosionData(BaseExplosion) 
        {
	className = "ExplosionData";
	particleDensity = "10";
	particleRadius = "1";
	explosionScale = "1 1 1";
	playSpeed = "1";
	Shockwave = "BaseShockWave";
	shockwaveOnTerrain = "0";
	lifetimeMS = "1000";
        };

datablock ExplosionData(DomeExplosion):BaseExplosion
          {
     	  Shockwave = "DomeShockWave";
          };

datablock ExplosionData(HugeShExplosion):BaseExplosion
          {
     	  Shockwave = "HugeShockWave";
          };

datablock ExplosionData(InitialExplosion):BaseExplosion
          {
     	  Shockwave = "InitialShockWave";
          };

datablock ExplosionData(DomeRingExplosion)
        {        
        emitter[0] = "DomeRingEmitter2";
        emitter[1] = "DomeRingEmitter1";
        };

datablock TracerProjectileData(BaseProjectile) {
	className = "TracerProjectileData";
	Explosion = "BaseExplosion";
	dryVelocity = "0.1";
	wetVelocity = "0.1";
	fizzleTimeMS = "32";
	lifetimeMS = "32";
	explodeOnDeath = "1";
	crossSize = "0.1";
	renderCross = "0";
        isFXUnit = "1";
        };

datablock TracerProjectileData(DomeProjectile):BaseProjectile
        {
	Explosion = "DomeExplosion";
        };

datablock TracerProjectileData(DomeRingProjectile):BaseProjectile
        {
	Explosion = "DomeRingExplosion";
        };

datablock TracerProjectileData(HugeShProjectile):BaseProjectile
        {
	Explosion = "HugeShExplosion";
        };

datablock TracerProjectileData(InitialProjectile):BaseProjectile
        {
	Explosion = "InitialExplosion";
        };


function shockwave(%pos,%nrm,%dat)
{
if (%dat $= "")
    %dat = BaseProjectile;
       
     %p1 = new TracerProjectile() 
          {
           dataBlock        = %dat;
           initialDirection = %nrm;
           initialPosition  = %pos;           
          };
}

function whiteout(%pos,%minrad,%maxrad)
{
 
   InitContainerRadiusSearch(%pos,%maxrad, $TypeMasks::PlayerObjectType |
                                          $TypeMasks::TurretObjectType);

   while ((%damage = containerSearchNext()) != 0)
   {
      %dist = containerSearchCurrDist();

      %eyeXF = %damage.getEyeTransform();
      %epX   = firstword(%eyeXF);
      %epY   = getWord(%eyeXF, 1);
      %epZ   = getWord(%eyeXF, 2);
      %eyePos = %epX @ " " @ %epY @ " " @ %epZ;
      %eyeVec = %damage.getEyeVector();

      // Make sure we can see the thing...
      if (ContainerRayCast(%eyePos, %pos, $TypeMasks::TerrainObjectType |
                                          $TypeMasks::InteriorObjectType |
                                          $TypeMasks::StaticObjectType, %damage) !$= "0")
      {
         continue;
      }



      %distFactor = (%maxrad-%dist)/(%maxrad-%minrad);     
      %dif = VectorNormalize(VectorSub(%pos, %eyePos));
      %dot = VectorDot(%eyeVec, %dif);

      %difAcos = mRadToDeg(mAcos(%dot));
      %dotFactor = 1.0;
      if (%difAcos > 60)
         %dotFactor = ((1.0 - ((%difAcos - 60.0) / 120.0)) * 0.2) + 0.3;
      else if (%difAcos > 45)
         %dotFactor = ((1.0 - ((%difAcos - 45.0) / 15.0)) * 0.5) + 0.5;

      %totalFactor = %dotFactor * %distFactor;
              
	  %prevWhiteOut = %damage.getWhiteOut();

		
      %whiteoutVal = %prevWhiteOut + %totalFactor;
      if(%whiteoutVal > 1)
      {
        //error("whitout at max");
        %whiteoutVal = 1;
      }
      
      %damage.setWhiteOut( %whiteoutVal );
   }
}


function TracerProjectile::ShockwaveDamage(%pos,%size,%lastsize)
{
InitContainerRadiusSearch(%pos, %area, $TypeMasks::VehicleObjectType | $TypeMasks::PlayerObjectType |$TypeMasks::StaticShapeObjectType| $TypeMasks::ItemObjectType | $TypeMasks::CorpseObjectType); // );

   %numTargets = 0;
   while ((%targetObject = containerSearchNext()) != 0)
   {
      %dist = containerSearchCurrRadDamageDist(); 
      %distPct = %dist / %area;

      %coverage = calcExplosionCoverage(%pos, %targetObject,
                                        ($TypeMasks::InteriorObjectType |
                                         $TypeMasks::TerrainObjectType |
                                         $TypeMasks::ForceFieldObjectType));
      if (%coverage == 0)
         continue;

     if (%area < 500)
        %power = %area;
     else
        %power = Limit(500 - %area,0,200);

     if (%dist > %lastar) //Shockwave
        {
        %force = (%power/100)*%targetobject.getDatablock().mass;
        %damage = Limit(100/%dist,0,100); 
        %upvec = "0 0 1";
        }
      else
        {
        %force = 0;
        %damage = Limit(5/%dist,0,100);
        %upvec = "";
        }

      %tgtPos = %targetObject.getWorldBoxCenter();
      %vec = VectorNormalize(VectorAdd(vectorNeg(vectorNormalize(vectorSub(%pos, %tgtPos))),%upvec));
      %nForce = %force * %distPct * 10; 
      %forceVector = vectorScale(%vec, %nForce);

      if(!%targetObject.inertialDampener && !%targetObject.getDatablock().forceSensitive)
          %targetObject.applyImpulse(pos( %targetObject), %forceVector);
       if (%damage>0.01)
           %targetObject.getDataBlock().damageObject( %targetObject, 0,pos( %targetObject),%damage,$DamageType::Explosion);       
   }
}


function PlasmaBolt::onExplode(%data, %proj, %pos, %mod) 
{
//funkycurve("0 0 110",%pos);
mpm_calc("0 0 110",%pos,1,0,0);
}


//Dfunctions


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
%virvec = floorvec(VectorNormalize(VirVec(%obj,%vec)),1000);
%mask = floorvec(VectorMultiply(VectorScale(VectorNormalize(%virvec),1/mPow(2,0.5)),%obj.getRealSize()),1000);
%inoutvec = VectorMultiply( VectorNormalize( TopVec( VAbs( VectorDivide( %virvec,%mask ) ))),%obj.getRealSize());
%pass = VectorScale( %virvec,VectorLen( VectorDivide( %inoutvec,%virvec ) ) );
return %pass;
}

function getpassVector(%obj,%vec)
{
return RealVec(%obj,VirpassVector(%obj,%vec));
}

function getpassLength(%obj,%vec)
{
return VectorLen(getpassVector(%obj,%vec));
}
