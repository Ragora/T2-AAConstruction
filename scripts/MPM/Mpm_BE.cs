
if ($mpm_BE != 1)
   {
   //$mpm_load[$mpm_loads] = Mpm_Base_Load;
   //$mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_BBom_Load;
   $mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_BMis_Load;
   $mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_BMor_Load;
   $mpm_loads++;
   $mpm_BE = 1;
   }



datablock ItemData(Mpm_Base_Load) 
{
   cost = 40;
   missile = Mpm_B_MIS1;
   name = "[Weapon] Base Explosion";
   friendly = 0;
};

datablock ItemData(Mpm_BBom_Load):Mpm_Base_Load
{
   cost = 75;
   name = "[Cluster] Bomber Run";
   missile = Mpm_B_MIS1;
   offset = "0 100";
   slot = 4;
};

datablock ItemData(Mpm_BMis_Load):Mpm_Base_Load
{
   cost = 50;
   name = "[Cluster] Cluster Missile";
   missile = Mpm_B_MIS1;
   offset = "0 0";
   slot = 4;
};

datablock ItemData(Mpm_BMor_Load):Mpm_Base_Load
{
   cost = 75;
   name = "[Cluster] Mortar dump";
   missile = Mpm_B_MIS1;
   offset = "0 100";
   slot = 4;
};

datablock AudioDescription(AudioMassiveExplosion) {
	volume = "1";
	isLooping = "0";
	is3D = "1";
	minDistance = "500";
	maxDistance = "9999";
	coneInsideAngle = "360";
	coneOutsideAngle = "360";
	coneOutsideVolume = "1";
	coneVector = "0 0 1";
	environmentLevel = "1";
	loopCount = "-1";
	minLoopGap = "0";
	maxLoopGap = "0";
	type = "3";
};

datablock AudioProfile(Mpm_BE_Sound) {
	fileName = "fx/weapons/mortar_explode.wav";
	description = "AudioMassiveExplosion";
	Preload = "1";
};

datablock ParticleData(Mpm_BE_PA) {
	dragCoefficient = "0.6";
	windCoefficient = "0";
	gravityCoefficient = "0";
	inheritedVelFactor = "0";
	constantAcceleration = "0";
	lifetimeMS = "7000";
	lifetimeVarianceMS = "2000";
	spinSpeed = "0";
	spinRandomMin = "-500";
	spinRandomMax = "500";
	useInvAlpha = "0";
	animateTexture = "0";
	framesPerSec = "1";
	textureName = "special/expFlare";
	animTexName[0] = "special/expFlare";
	colors[0] = "1.000000 1.000000 1.000000 0.900000";
	colors[1] = "1.000000 0.400000 0.000000 0.300000";
	colors[2] = "1.000000 0.300000 0.000000 0.000000";
	colors[3] = "1.000000 1.000000 1.000000 1.000000";
	sizes[0] = "75";
	sizes[1] = "250";
	sizes[2] = "400";
	sizes[3] = "1";
	times[0] = "0";
	times[1] = "0.7";
	times[2] = "1";
	times[3] = "2";
};



datablock ParticleEmitterData(Mpm_BE_PE) {
	className = "ParticleEmitterData";
	ejectionPeriodMS = "3";
	periodVarianceMS = "0";
	ejectionVelocity = "57";
	velocityVariance = "20";
	ejectionOffset = "6";
	thetaMin = "0";
	thetaMax = "110";
	phiReferenceVel = "0";
	phiVariance = "360";
	overrideAdvance = "0";
	orientParticles = "0";
	orientOnVelocity = "1";
	particles = "Mpm_BE_PA";
	lifetimeMS = "3600";
	lifetimeVarianceMS = "0";
	useEmitterSizes = "0";
	useEmitterColors = "0";
	overrideAdvances = "0";
};

datablock ExplosionData(Mpm_BE) {
	className = "ExplosionData";
	soundProfile = "Mpm_BE_Sound";
	faceViewer = "0";
	particleDensity = "10";
	particleRadius = "1";
	explosionScale = "1 1 1";
	playSpeed = "1";
	emitter[0] = "Mpm_BE_PE";
	shockwaveOnTerrain = "0";
	debrisThetaMin = "0";
	debrisThetaMax = "90";
	debrisPhiMin = "0";
	debrisPhiMax = "360";
	debrisNum = "1";
	debrisNumVariance = "0";
	debrisVelocity = "2";
	debrisVelocityVariance = "0";
	delayMS = "0";
	delayVariance = "0";
	lifetimeMS = "1000";
	lifetimeVariance = "0";
	offset = "0";
	times[0] = "0";
	times[1] = "1";
	times[2] = "1";
	times[3] = "1";
	sizes[0] = "1 1 1";
	sizes[1] = "1 1 1";
	sizes[2] = "1 1 1";
	sizes[3] = "1 1 1";
	shakeCamera = "0";
	camShakeFreq = "10 10 10";
	camShakeAmp = "1 1 1";
	camShakeDuration = "1.5";
	camShakeRadius = "10";
	camShakeFalloff = "10";
};

datablock TracerProjectileData(Mpm_BE_PR):Mpm_G_PR {
	Explosion = "Mpm_BE";
	};


function Mpm_BBom_Load::Stage2(%data,%p)
{
if (IsObject(%p))
	{
        %p2 = parent::Stage2(%data,%p);
        Cancel(%p2.stage2);
        %p2.stage2 = %data.schedule((%p2.s2time-5000),"AtTarget",%p2);
	}
}

function Mpm_Bbom_Load::AtTarget(%data,%p)
{
if (IsObject(%p))
	{
        %speed = GetWords(%p.predict(),3,5);
        %p.schedule(100,"Mpm_BomberRun");
      	}
}


function Mpm_Bmis_Load::Stage2(%data,%p)
{
if (IsObject(%p))
	{
        %p2 = parent::Stage2(%data,%p);
        Cancel(%p2.stage2);
        %p2.stage2 = %data.schedule((%p2.s2time-5000),"AtTarget",%p2);
	}
}

function Mpm_Bmis_Load::AtTarget(%data,%p)
{
if (IsObject(%p))
	{
        %speed = GetWords(%p.predict(),3,5);
        %p.schedule(250,"Mpm_MissileRun");
      	}
}


function Mpm_Bmor_Load::Stage2(%data,%p)
{
if (IsObject(%p))
	{
        %p2 = parent::Stage2(%data,%p);
        Cancel(%p2.stage2);
        %p2.stage2 = %data.schedule((%p2.s2time-5000),"AtTarget",%p2);
	}
}

function Mpm_Bmor_Load::AtTarget(%data,%p)
{
if (IsObject(%p))
	{
        %speed = GetWords(%p.predict(),3,5);
        %p.schedule(100,"Mpm_MortarRun");
      	}
}


function SeekerProjectile::Mpm_BomberRun(%p)
{
%speed = VectorNormalize(GetWords(%p.predict(),3,5));
%adjust = VectorCross(%speed,"0 0 1");
%adjust = VectorAdd(VectorScale(VectorCross(%adjust,%speed),GetRandom()*2-1),VectorScale(%adjust,GetRandom()*2-1));
%p2=BomberBomb.Create(%p.getWorldBoxCenter(),"0 0 -1",VectorScale(VectorAdd(%speed,%adjust),25));     
%p.schedule(250,"Mpm_bomberRun");
}

function SeekerProjectile::Mpm_MissileRun(%p)
{
%speed = GetWords(%p.predict(),3,5);
%dir = VectorNormalize(%speed);
%for = VectorScale(%dir,-89.3);
%left = VectorNormalize(VectorCross("0 0 1",%for));
%up = VectorNormalize(VectorCross(%for,%left));
%var = VectorAdd(VectorScale(%left,getRandom()*20-10),VectorScale(%up,getRandom()*20-10));
%vel = VectorAdd(%for,%var);
ShoulderMissile.create(%p.getWorldBoxCenter(),%dir,%vel);
%p.schedule(100,"Mpm_MissileRun");
}

function SeekerProjectile::Mpm_MortarRun(%p)
{
%speed = VectorNormalize(GetWords(%p.predict(),3,5));
%adjust = VectorCross(%speed,"0 0 1");
%adjust = VectorAdd(VectorScale(VectorCross(%adjust,%speed),GetRandom()*2-1),VectorScale(%adjust,GetRandom()*2-1));

%p2=MortarShot.Create(%p.getWorldBoxCenter(),"0 0 -1",VectorScale(VectorAdd(%speed,%adjust),25));     
%p.schedule(150,"Mpm_MortarRun");
}

function Mpm_Base_Load::Explode(%data,%p,%pos)
{
if (IsObject(%p))
	{
        PlayExplosion(%pos,"Mpm_BE_PR","0 0 -1");
	RadiusExplosion(%p, %pos, 50, 5, 5000, %p.owner, $DamageType::Missile);
        }
}