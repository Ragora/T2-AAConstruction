//--------------------------------------------------------------------------
// Nerf Ball Launcher
// ------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Nerf Ball Launcher Ball
// ------------------------------------------------------------------------

datablock AudioProfile(NerfBallBurnSound)
{
   filename = "fx/armor/bubbletrail.wav";
   description = ProjectileLooping3d;
   preload = true;
};

datablock AudioProfile(NerfBallExplosionSound)
{
   filename = "fx/weapons/cg_water2.wav";
   description = AudioClosest3d;
   preload = true;
};

//--------------------------------------------------------------------------
// Particle effects
//--------------------------------------
datablock ParticleData(NerfBallParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.15;
   inheritedVelFactor   = 0.5;

   lifetimeMS           = 3000;
   lifetimeVarianceMS   = 200;

   spinRandomMin = -200.0;
   spinRandomMax =  200.0;

   textureName          = "special/bubbles";

   colors[0]     = "0.0 0.5 1.0 0.3";
   colors[1]     = "0.0 0.4 1.0 0.2";
   colors[2]     = "0.0 0.3 1.0 0.1";

   sizes[0]      = 0.6;
   sizes[1]      = 0.3;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(NerfBallEmitter)
{
   lifetimeMS       = 0; // No limit

   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.0;
   velocityVariance = 0.5;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   orientParticles  = false;
   orientOnVelocity = false;

   particles = "NerfBallParticle";
};

datablock ParticleData(NerfBallExplosionParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.5;
   inheritedVelFactor   = 0.5;

   lifetimeMS           = 2000;
   lifetimeVarianceMS   = 200;

   spinRandomMin = -200.0;
   spinRandomMax =  200.0;

   textureName          = "special/bubbles";

   colors[0]     = "0.0 0.5 1.0 0.3";
   colors[1]     = "0.0 0.4 1.0 0.2";
   colors[2]     = "0.0 0.3 1.0 0.1";

   sizes[0]      = 0.6;
   sizes[1]      = 0.3;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(NerfBallExplosionEmitter)
{
   lifetimeMS       = 200;

   ejectionPeriodMS = 1;
   periodVarianceMS = 0;

   ejectionVelocity = 3.0;
   velocityVariance = 0.5;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   orientParticles  = false;
   orientOnVelocity = false;

   particles = "NerfBallExplosionParticle";
};

//--------------------------------------------------------------------------
// Explosion - Nerf Ball Launcher Ball
//--------------------------------------
datablock ExplosionData(NerfBallExplosion)
{
   emitter[0]   = NerfBallExplosionEmitter;
   soundProfile = NerfBallExplosionSound;
};

//--------------------------------------------------------------------------
// Projectile - Nerf Ball Launcher Ball
//--------------------------------------
datablock GrenadeProjectileData(NerfBall)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0;
   directDamageType    = $DamageType::Default;
   radiusDamageType    = $DamageType::Default;
   hasDamageRadius     = false;
   indirectDamage      = 0;
   damageRadius        = 3.0;  // used in onExplode()
   kickBackStrength    = 0;
   useLensFlare        = false;

   sound 	       = NerfBallBurnSound;
   explosion           = NerfBallExplosion;
   velInheritFactor    = 0.5;

   texture[0]          = "special/bubbles";

   baseEmitter         = NerfBallEmitter;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.1;
   armingDelayMS     = 1500;
   muzzleVelocity    = 60;
   drag = 0.1;
   gravityMod = 3.0;
};

//--------------------------------------
// Nerf Ball Launcher
//--------------------------------------

datablock AudioProfile(NerfBallLauncherFireSound)
{
   filename    = "fx/vehicles/crash_ground_vehicle.wav";
   description = AudioDefault3d;
   preload = true;
   effect = GrenadeFireEffect;
};

datablock AudioProfile(NerfBallLauncherReloadSound)
{
   filename    = "heavy_RF_uw.wav";
   description = AudioClosest3d;
   preload = true;
   effect = GrenadeReloadEffect;
};

//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(NerfBallLauncherAmmo)
{
   className	= Ammo;
   catagory	= "Ammo";
   shapeFile	= "ammo_grenade.dts";
   mass		= 1;
   elasticity	= 0.2;
   friction	= 0.6;
   pickupRadius	= 2;
   pickUpName	= "some nerf ball launcher ammo";
   computeCRC	= true;
   emap		= true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ItemData(NerfBallLauncher)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_grenade_launcher.dts";
   image = NerfBallLauncherImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   pickUpName = "a nerf ball launcher";
   computeCRC = true;

};

datablock ShapeBaseImageData(NerfBallLauncherImage)
{
   className = WeaponImage;
   shapeFile = "weapon_grenade_launcher.dts";
   item = NerfBallLauncher;
   ammo = NerfBallLauncherAmmo;
   offset = "0 0 0";
   emap = true;

   projectile = NerfBall;
   projectileType = GrenadeProjectile;

   projectileSpread = 30.0 / 1000.0;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = GrenadeSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.05;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateScript[3] = "onFire";
   stateSound[3] = NerfBallLauncherFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.1;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   stateSound[4] = NerfBallLauncherReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = GrenadeDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
};

function NerfBall::onExplode(%data,%projectile,%pos,%mod) {
	InitContainerRadiusSearch(%pos, %data.damageRadius, $TypeMasks::PlayerObjectType);
	while ((%targetObject = containerSearchNext()) != 0) {
		if (isObject(%targetObject)) {
			if (%targetObject.getClassName() $= "Player")
				applyNerf(%targetObject,%projectile.sourceObject,%pos);
		}
	}
}

// Bot fun!
function NerfBallLauncherImage::onFire(%data,%obj,%slot) {
	%p = Parent::onFire(%data, %obj, %slot);
	AIGrenadeThrown(%p);
}
