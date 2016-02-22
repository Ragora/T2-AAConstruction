//--------------------------------------
// Nerf Gun
//--------------------------------------

//--------------------------------------------------------------------------
// Sounds
//--------------------------------------
datablock AudioProfile(NerfGunFireSound)
{
   filename    = "fx/armor/heavy_LF_uw.wav";
   description = AudioDefault3d;
   preload = true;
   effect = BlasterFireEffect;
};

datablock AudioProfile(NerfGunDryFireSound)
{
   filename    = "fx/weapons/chaingun_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(NerfBoltProjectileSound)
{
   filename    = "fx/weapons/ELF_hit.WAV";
   description = ProjectileLooping3d;
   preload = true;
};

datablock AudioProfile(NerfBoltExpSound)
{
   filename    = "fx/armor/heavy_LF_metal.wav";
   description = AudioClosest3d;
   preload = true;
};

//--------------------------------------------------------------------------
// Explosion
//--------------------------------------
datablock ParticleData(NerfBoltExplosionParticle1)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 000;
   textureName          = "special/crescent4";
   colors[0] = "0.1 1.0 0.1 1.0";
   colors[1] = "0.1 1.0 0.1 1.0";
   colors[2] = "0.0 0.0 0.0 0.0";
   sizes[0]      = 0.25;
   sizes[1]      = 0.5;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(NerfBoltExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 2;
   velocityVariance = 1.5;
   ejectionOffset   = 0.0;
   thetaMin         = 70;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 200;
   particles = "NerfBoltExplosionParticle1";
};

datablock ParticleData(NerfBoltExplosionParticle2)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 000;
   textureName          = "special/blasterHit";
   colors[0] = "0.1 1.0 0.1 1.0";
   colors[1] = "0.1 0.5 0.1 0.5";
   colors[2] = "0.0 0.0 0.0 0.0";
   sizes[0]      = 0.3;
   sizes[1]      = 0.90;
   sizes[2]      = 1.50;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(NerfBoltExplosionEmitter2)
{
   ejectionPeriodMS = 30;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = false;
   lifetimeMS       = 200;
   particles = "NerfBoltExplosionParticle2";
};

datablock ExplosionData(NerfBoltExplosion)
{
   soundProfile   = NerfBoltExpSound;
   emitter[0]     = NerfBoltExplosionEmitter;
   emitter[1]     = NerfBoltExplosionEmitter2;
};


//--------------------------------------------------------------------------
// Projectile
//--------------------------------------

datablock LinearFlareProjectileData(NerfBolt)
{
   emitterDelay        = -1;
   directDamage        = 0;
   directDamageType    = $DamageType::Default;
   kickBackStrength    = 0.0;
   bubbleEmitTime      = 1.0;

   sound = NerfBoltProjectileSound;
   velInheritFactor    = 0.5;

   explosion           = "NerfBoltExplosion";
   splash              = BlasterSplash;

   grenadeElasticity = 0.998;
   grenadeFriction   = 0.0;
   armingDelayMS     = 500;

   muzzleVelocity    = 100.0;

   drag = 0.05;

   gravityMod        = 0.0;

   dryVelocity       = 100.0;
   wetVelocity       = 80.0;

   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   lifetimeMS     = 3000;

   scale             = "0.1 0.1 0.1";
   numFlares         = 8;
   flareColor        = "0.1 1 0.1";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "special/lightFalloffMono";
};


//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(NerfGunImage)
{
   className = WeaponImage;
   shapeFile = "weapon_energy.dts";
   item = NerfGun;

   projectile = NerfBolt;
   projectileType = LinearFlareProjectile;

   usesEnergy = true;
   fireEnergy = 4;
   minEnergy = 4;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = BlasterSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.3;
   stateFire[3] = true;
   stateRecoil[3] = NoRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateSound[3] = NerfGunFireSound;
   stateScript[3] = "onFire";

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";
   
   stateName[6] = "DryFire";
   stateTimeoutValue[6] = 0.3;
   stateSound[6] = NerfGunDryFireSound;
   stateTransitionOnTimeout[6] = "Ready";
};

datablock ItemData(NerfGun)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = NerfGunImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a nerf gun";
};

function NerfBolt::onCollision(%data,%projectile,%targetObject,%modifier,%position,%normal) {
	if (isObject(%targetObject)) {
		if (%targetObject.getClassName() $= "Player")
			applyNerf(%targetObject,%projectile.sourceObject,%position);
	}
	Parent::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal);
}

// Bot fun!
function NerfGunImage::onFire(%data,%obj,%slot) {
	%p = Parent::onFire(%data, %obj, %slot);
	AIGrenadeThrown(%p);
}
