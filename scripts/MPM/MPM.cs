



datablock ParticleData(MpmFlareParticle1)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 1.0;
   
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 0;

   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0;
   textureName          = "skins/jetflare03";

   colors[0]     = "0.7 0.7 1.0 0.5";
   colors[1]     = "0.7 0.7 1.0 0.5";
   colors[2]     = "0.7 0.7 1.0 0.5";
   colors[3]     = "0.7 0.7 1.0 0.5";

   sizes[0]      = 50;
   sizes[1]      = 50;
   sizes[2]      = 50;
   sizes[3]      = 50;

   times[0]      = 0.25;
   times[1]      = 0.25;
   times[2]      = 0.25;
   times[3]      = 1;

};

datablock ParticleData(MpmFlareParticle3)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 1.0;
   
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;

   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0;
   textureName          = "skins/jetflare03";

   colors[0]     = "0.7 0.7 1.0 0.5";
   colors[1]     = "0.7 0.7 1.0 0.5";
   colors[2]     = "0.7 0.7 1.0 0.5";
   colors[3]     = "0.7 0.7 1.0 0.5";

   sizes[0]      = 50;
   sizes[1]      = 50;
   sizes[2]      = 50;
   sizes[3]      = 50;

   times[0]      = 0.0;
   times[1]      = 0.25;
   times[2]      = 0.5;
   times[3]      = 0.75;

};

datablock ParticleData(MpmFlareParticle2):MpmFlareParticle1
{
textureName          = "skins/jetpackflare_bio";
useInvAlpha = 0;
lifetimeMS           = 2500;
spinRandomMin = -360.0;
spinRandomMax =  360.0;
sizes[0]      = 15;
sizes[1]      = 15;
sizes[2]      = 15;
sizes[3]      = 15;
};

datablock ParticleData(MpmJetSmoke1)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   
   lifetimeMS           = 60000;
   lifetimeVarianceMS   = 0;

   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0.5;
   textureName          = "skins/jetflare2";

   colors[0]     = "0.8 0.8 0.8 1";
   colors[1]     = "0.8 0.8 0 0.9";
   colors[2]     = "0.8 0.8 0.8 0.5";
   colors[3]     = "0.8 0.8 0.8 0.0";

   sizes[0]      = 5;
   sizes[1]      = 17;
   sizes[2]      = 18;
   sizes[3]      = 20;

   times[0]      = 0;
   times[1]      = 0.25;
   times[2]      = 0.5;
   times[3]      = 0.75;

};



datablock ParticleData(MpmJetSmoke2):MpmJetSmoke1
{

  dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   
   lifetimeMS           = 60000;
   lifetimeVarianceMS   = 0;

   spinRandomMin = 0.0;
   spinRandomMax =  0.0;
   windcoefficient = 0.5;
   textureName          = "skins/jetflare2";

   colors[0]     = "0.8 0.8 0.8 1";
   colors[1]     = "0.8 0.8 0 0.9";
   colors[2]     = "0.8 0.8 0.8 0.5";
   colors[3]     = "0.8 0.8 0.8 0.0";

   sizes[0]      = 5;
   sizes[1]      = 17;
   sizes[2]      = 18;
   sizes[3]      = 20;

   times[0]      = 0;
   times[1]      = 0.25;
   times[2]      = 0.5;
   times[3]      = 0.75;



};



datablock ParticleEmitterData(MpmFlareEmitter1)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0.0;
   ejectionoffset = 5;
   thetaMin         = 0.0;
   thetaMax         = 0.0;
	

   orientParticles  = false;
   orientOnVelocity = false;

   particles = "MpmFlareParticle1";
};

datablock ParticleEmitterData(MpmFlareEmitter2):MpmFlareEmitter1
{
particles = "MpmFlareParticle2";
};

datablock ParticleEmitterData(MpmJetEmitter1)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 10.0;
   velocityVariance = 1.0;
   ejectionoffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 5.0;


   orientParticles  = false;
   orientOnVelocity = false;

   particles = "MpmJetSmoke1";
};

datablock ParticleEmitterData(MpmJetEmitter2):MpmJetEmitter1
{
   ejectionPeriodMS = 10;
   particles = "MpmJetSmoke2";
   thetaMin         = 0.0;
   thetaMax         = 1.5;
};


datablock ParticleEmitterData(MpmJetEmitter3):MpmJetEmitter1
{
   ejectionPeriodMS = 30;
   ejectionVelocity = 4.0;
   velocityVariance = 1.0;
   particles = "MpmFlareParticle2";
   ejectionoffset = 10;
   thetaMin         = 0.0;
   thetaMax         = 360.0;

};

datablock TracerProjectileData(Mpm_G_PR) {
	className = "TracerProjectileData";
	emitterDelay = "-1";
	velInheritFactor = "0";
	directDamage = "0";
	hasDamageRadius = "0";
	indirectDamage = "0";
	damageRadius = "0";
	radiusDamageType = "0";
	kickBackStrength = "0";
	Explosion = "TurretExplosion";
	hasLight = "0";
	lightRadius = "1";
	lightColor = "1.000000 1.000000 1.000000 1.000000";
	hasLightUnderwaterColor = "0";
	underWaterLightColor = "1.000000 1.000000 1.000000 1.000000";
	explodeOnWaterImpact = "0";
	depthTolerance = "5";
	bubbleEmitTime = "0.5";
	faceViewer = "0";
	scale = "1 1 1";
	dryVelocity = "0.1";
	wetVelocity = "0.1";
	fizzleTimeMS = "32";
	lifetimeMS = "32";
	explodeOnDeath = "1";
	reflectOnWaterImpactAngle = "0";
	deflectionOnWaterImpact = "0";
	fizzleUnderwaterMS = "-1";
	activateDelayMS = "-1";
	doDynamicClientHits = "0";
	tracerLength = "1";
	tracerMinPixels = "1";
	tracerAlpha = "0";
	tracerColor = "0.000000 0.000000 0.000000 0.000000";
	tracerTex[0] = "special/tracer00";
	tracerTex[1] = "special/tracercross";
	tracerWidth = "0.1";
	crossViewAng = "0.99";
	crossSize = "0.1";
	renderCross = "0";
        isFXUnit = "1";
};



datablock SeekerProjectileData(MpmMissile1)
{
   heatSignature = 1;
   sensorData = DeployedOutdoorTurretSensor;
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "bomb.dts";
   hasDamageRadius     = false;
   indirectDamage      = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::Missile;
   kickBackStrength    = 20000;

   explosion           = "GrenadeExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage

   baseEmitter         = MpmJetEmitter1;
   delayEmitter        = MpmFlareEmitter1;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = -1;
   muzzleVelocity      = 0.1;
   maxVelocity         = 8000;
   turningSpeed        = 0.0;
   acceleration        = 1;
   scale = "1 1 1";
   proximityRadius     = 3;

   terrainAvoidanceSpeed         = 180;
   terrainScanAhead              = 25;
   terrainHeightFail             = 12;
   terrainAvoidanceRadius        = 100;  
   
   flareDistance = 0;
   flareAngle    = 0;

   sound = HAPCFlyerThrustSound;
   //BomberFlyerThrustSound;
   explodeOnDeath = "1";
   hasLight    = true;
   lightRadius = 10.0;
   lightColor  = "1 1 0";

   useFlechette = false;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = true;
};



datablock GrenadeProjectileData(BoosterGrenade)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.1;
   damageRadius        = 0.1;
   radiusDamageType    = $DamageType::Grenade;
   kickBackStrength    = 1500;
   bubbleEmitTime      = 1.0;

   sound               = GrenadeProjectileSound;
   explosion           = "GrenadeExplosion";
   underwaterExplosion = "UnderwaterGrenadeExplosion";
   velInheritFactor    = 0.5;
   splash              = GrenadeSplash;

   //baseEmitter         = //MpmJetEmitter2;
   bubbleEmitter       = GrenadeBubbleEmitter;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 250;
   muzzleVelocity    = 10;
   drag = 0.1;
};


datablock SeekerProjectileData(MpmMissile2):MpmMissile1
{
 directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.1;
   damageRadius        = 0.1;

explosion = LargeAirVehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 0.2;

muzzleVelocity      = 0.1;
maxVelocity         = 80000;
acceleration        = 1;

projectileShapeName = "weapon_missile_casement.dts";
baseEmitter         = MpmJetEmitter2;
delayEmitter        = MpmFlareEmitter2;
lifetimeMS          = -1;
   sound = HAPCFlyerThrustSound;
};

datablock SeekerProjectileData(MpmMissile3):MpmMissile2
{
lifetimeMS          = -1;
muzzleVelocity      = 20; //5 
maxVelocity         = 20;//80
acceleration        = 0; //1
};

function MpmMissile1::onExplode(%data, %proj, %pos, %mod)
{
%proj.load.Explode(%proj,%pos);
//parent::onExplode(%data,%proj,%pos,%mod);
}

function Mpm_B_MIS::onExplode(%data, %proj, %pos, %mod)
{
%proj.load.Explode(%proj,%pos);
//parent::onExplode(%data,%proj,%pos,%mod);
}

function Mpm_B_MIS1::onExplode(%data, %proj, %pos, %mod)
{
%proj.load.Explode(%proj,%pos);
//parent::onExplode(%data,%proj,%pos,%mod);
}

function Mpm_B_MIS2::onExplode(%data, %proj, %pos, %mod)
{
//Anti missile missile :D
//%proj.load.Explode(%proj,%pos);
//parent::onExplode(%data,%proj,%pos,%mod);
}

function Mpm_B_MIS3::onExplode(%data, %proj, %pos, %mod)
{
%proj.load.Explode(%proj,%pos);
//parent::onExplode(%data,%proj,%pos,%mod);
}

function Mpm_B_MIS4::onExplode(%data, %proj, %pos, %mod)
{
%proj.load.Explode(%proj,%pos);
//parent::onExplode(%data,%proj,%pos,%mod);
}


function MpmMissile1::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal)
{
parent::onExplode(%data,%projectile,%position,%modifier);
}


function MpmMissile2::onExplode(%data, %proj, %pos, %mod)
{

%speed = GetWords(%proj.predict(),3,5);
%dir = VectorScale(%speed,1/BasicGrenade.muzzlevelocity);
%p1 = new GrenadeProjectile() 
          {
           dataBlock        = BoosterGrenade;
           initialDirection = %dir;
           initialPosition  = %pos;           
          };
%proj.delete();
}

function MpmMissile3::onExplode(%data, %proj, %pos, %mod)
{
%speed = GetWords(%proj.predict(),3,5);
%dir = VectorScale(%speed,1/BasicGrenade.muzzlevelocity);
%p1 = new GrenadeProjectile() 
          {
           dataBlock        = BoosterGrenade;
           initialDirection = %dir;
           initialPosition  = %pos;           
          };
%proj.delete();
}

function SeekerProjectile::Predict(%p,%ttime)
{
%time = ((GetSimTime()+%ttime) - %p.createtime)/1000;
%dat = %p.getDatablock();
%startspeed = VectorScale(%p.InitialDirection,%dat.muzzleVelocity);
if (%p.sourceObject)
    {
    %co1 = mAbs(VectorDot(%p.InitialDirection,VectorNormalize(%p.startVelocity)));
    %sourceSpeed = VectorScale(%p.startVelocity,%dat.velInheritFactor*%co1); 
    //if (%p.speedmod == 1)
    %sourceSpeed = VectorAdd(%sourcespeed,VectorScale(VectorNormalize(%p.startVelocity),-0.5));
    %leng = mAbs(VectorDot(%p.InitialDirection,%p.startVelocity));
    %dir = VectorNormalize(%p.initialdirection);
    %sourceSpeedlen = VectorDot(%SourceSpeed,VectorNormalize(%p.initialDirection));
    }
%speedaccel = Limit(%dat.acceleration*(%time-1),0,Limit(%dat.maxVelocity-(%dat.muzzleVelocity+%sourceSpeedlen),0));
%accelVec = VectorScale(vectorNormalize(%p.InitialDirection),%speedaccel);

%Speed = VectorAdd(VectorAdd(%startspeed,%sourceSpeed),VectorAdd(%accelvec,%gravvec));

%distance1 = VectorScale(%startspeed,%time);
%distance2 = VectorScale(%sourceSpeed,%time);

%distance3 = VectorScale(%accelvec,0.5*(%time));
%distance = VectorAdd(VectorAdd(%p.initialPosition,%distance3),VectorAdd(%distance2,%distance1));

//if (!%p.speedmod)
  //%p.speedmod = 1;

//if (VectorDist(%distance,%p.getTransform())>4)
     //%p.speedmod *= -1;

if (getSimTime()-%p.lasttime>1000)
   %p.schedule(%ttime,"logerror",%distance);
%distance=VectorAdd(%distance,VectorScale(%p.lasterror,-1));

return %distance SPC %speed;
}

function testpred(%dir,%speed,%time)
{
%p = Launch_Mpm("0 0 110",%dir,%speed,%time*1000,mpmMissile1);
for (%t=0;%t < %time;%t++)
    {
    %p.schedule(%t*1000,predict);
    }
}



function putwp(%pos)
{

%wp = new WayPoint() {
	position = %pos;
	rotation = "1 0 0 0";
	scale = "1 1 1";
	dataBlock = "WayPointMarker";
 	team = 1;
        };
%wp.schedule(5000,"delete");
}


function SeekerProjectile::logerror(%p,%distance)
{
%p.lasterror = VectorSub(%distance,%p.getTransform());
%p.lasttime = (GetSimTime()-%p.createtime)/1000;
}


function ItemData::Stage1(%data,%p)
{

if (IsObject(%p))
	{
	}
}

function ItemData::Stage2(%data,%p)
{

if (IsObject(%p))
	{
        %start = GetWords(%p.traject,0,2);
        %up = GetWords(%p.traject,3,5);
        %loc = GetWord(%p.traject,6);
        %a = GetWord(%p.traject,7);
        %vector = GetWords(%p.traject,8,10);
        %time = GetWord(%p.traject,11);
	%stlevel = %p.getTransform();//VectorAdd(%start,VectorScale(%up,%loc));
	%stspeed = VectorScale(%up,%a);
        %missileblock = %p.load.missile;
	%p2 = Launch_Mpm(%stlevel,%vector,%stspeed,%time+50000,%missileblock);
        %p2.team = %p.team;
        %p2.source = %p.source;
        %p2.traject = %p.traject;
        %p2.load = %p.load;
        %p2.owner = %p.owner;
        %p2.s2time = %time;
	%p2.targetlocation = %p.targetlocation;
        %p2.stage2 = %p2.load.schedule(%time,"AtTarget",%p2);
        if (%p.owner.getControlObject() == %p.owner.comcam && %p.owner.moveprojectile = %p)
	   obsproj(%p2,%p.owner);
        return %p2;
	}
}

datablock ItemData(Mpm_Null_Load) 
{
   cost = 0;
   missile = Mpm_B_MIS1;
   name = "Null";
   friendly = 0;
};

function testenemy(%pos,%dir,%speed,%time)
{
%p = Launch_Mpm(%pos,%dir,%speed,%time,MpmMissile1);
%p.team = 0;
%p.load = Mpm_Null_Load;
%p.schedule(360000,"delete");
}

function ItemData::AtTarget(%data,%p)
{

if (IsObject(%p))
	{
        //do nothing
	}
}

function ItemData::Explode(%data,%p,%pos)
{

if (IsObject(%p))
	{
        if (IsObject(%data.vehicle))
             //if(VectorDist(%p.targetlocation,%pos)<50)
                  Mpm_VE_Load0.AtTarget(%p);
	}
}

function Mpm_Nuke_Load::Explode(%data,%p,%pos)
{
if (IsObject(%p))
	{
        BigFatNukeTarget(%pos);
	}
}

function Mpm_Nuke2_Load::Explode(%data,%p,%pos)
{
if (IsObject(%p))
	{
        ShoulderNuclear.onExplode(%p, %pos);
	}
}



function Mpm_Hole_Load::Explode(%data,%p,%pos)
{
if (IsObject(%p))
	{
        %mask = $TypeMasks::StaticShapeObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::InteriorObjectType;
        %res = containerRayCast(%pos,"0 0 500",%mask, %p);
        if (%res)
           %inside = 1;
	dome(VectorAdd(%pos,"0 0" SPC (1-%inside)*200),60000,%inside);
	}
}


function ItemData::InterCept(%data,%p)
{
if (IsObject(%p))
	{
        //Be suprised
	%p.delete();
	}
}

function ItemData::Hazard(%data,%p,%obj,%radius)
{
if (%p.team != %obj.team)
	return 1;
if (!%data.friendly && VectorDist(%p.targetlocation,%obj.getTransform())<%radius)
	return 1;

return 0;
}

function PlayExplosion(%pos,%data,%dir)
{
if (%dir $= "")
   %dir = "0 0 1";
if (IsObject(%data))
    {
    %p = new LinearProjectile() 
         {
         dataBlock        = %data;
         initialDirection = %dir;
         initialPosition  = %pos;
         };
    }
}


function GrenadeProjectile::Predict(%p,%time)
{
//Todo: make this one
}


//Custon information functions (not used)
function ListSpeed(%xlist,%tlist)
{

if (GetWordCount(%xlist)<2)
   return "";
for (%c=0;%c<GetWordCount(%xlist)-1;%c++)
	{
        %dx = GetWord(%xlist,%c+1)-GetWord(%xlist,%c);
        %dt = GetWord(%tlist,%c+1)-GetWord(%tlist,%c);  
        if (%dx == 0 || %dt == 0)
	        %slist = %slist SPC 0;
        else
	        %slist = %slist SPC %dx/%dt;
	}
return trim(%slist);
}

function ListShift(%list)
{
if (GetWordCount(%list)<2)
   return 0;
for (%c=0;%c<GetWordCount(%list)-1;%c++)
	{
	%mid = (GetWord(%list,%c+1)+GetWord(%list,%c))/2;
        %nlist = %nlist SPC %mid;
        }
return trim(%nlist);
}

function ListMean(%list)
{
if (GetWordCount(%list)<1)
   return "";
for (%c=0;%c<GetWordCount(%list);%c++)
	{
	%mean+= GetWord(%list,%c);
	}
return %mean/GetWordCount(%list);
}

function ListSub(%list1,%list2)
{
if (GetWordCount(%list1)<1)
   return "";
for (%c=0;%c<GetWordCount(%list1);%c++)
	{
        %mid = GetWord(%list1,%c)-GetWord(%list2,%c);
        %nlist = %nlist SPC %mid;
        }
return trim(%nlist);
}

function mechanics(%vec)
{

%nrm = VectorNormalize(%vec);
%p = Launch_Mpm("0 0 110",%nrm ,"0 0 10",12000,"mpmMissile1");
%p.vec = %vec;
%p.st = getSimTime();
for (%i=0;%i<10;%i++)
    schedule(%i*1000,0,"noteinfo",%p);
schedule(10000,0,"listinfo",%p);
}

function noteinfo(%p)
{
%n = %p.noted;
%p.noted++;
%predict = %p.predict();
%p.predict[%n] = %predict;
%p.real[%n] = %p.getTransform();
%p.time[%n] = (getSimTime()-%p.st)/1000;
}

function listinfo(%p)
{
for (%i=0;%i<11;%i++)
    {
    %pxlist =trim(%pxlist SPC GetWord(%p.predict[%i],0));
    %pzlist =trim(%pzlist SPC GetWord(%p.predict[%i],2));
    %xlist =trim(%xlist SPC GetWord(%p.real[%i],0));
    %zlist =trim(%zlist SPC GetWord(%p.real[%i],2));
    %tlist = trim(%tlist SPC %p.time[%i]);
    }
    //echo(%tlist);
    //echo(%pxlist);
    //echo(%pzlist);
    //echo(%xlist);
    //echo(%zlist);
    %speed = listspeed(%pzlist,%tlist);
    %acc = listspeed(%speed,listshift(%tlist));
    //echo(%speed);
   
    %speed1 = listspeed(%zlist,%tlist);
    %acc1 = listspeed(%speed1,listshift(%tlist));
    //echo(%speed1);
    %v1 = %speed-%acc*1/2;
    %v2 = %speed1-%acc1*1/2;
    $diff[getWord(%p.vec,0),getWord(%p.vec,2)] = %v1-%v2;
    //echo(ListSub(%speed,%speed1));
    //echo(listmean(%acc));
    //echo(listmean(%acc1));
    //echo(1/2*(listmean(%acc1)-listmean(%acc))*10*10);
}

function testrange()
{
for (%x=-5;%x<6;%x+=0.2)
    {
for (%y=0;%y<6;%y+=0.2)
    {    
    %vec = %x SPC "0" SPC %y;
    schedule((%x+5)*100000+%y*10000,0,"mechanics",%vec);
    }
    }
}

function plotres()
{
for (%x=-5;%x<6;%x+=0.2)
    {
for (%y=0;%y<6;%y+=0.2)
    {    
    %line = %line SPC $diff[%x,%y];
    }
    echo(%line);
    %line = "";
    }
}