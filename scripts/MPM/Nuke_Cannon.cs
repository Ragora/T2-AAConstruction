///////////////////////////////////////////////////////////////////////////////
//             "Project Uber: Planetary Cannon Edition"                      //
//                   "The nuke to end all nukes"                             //
//                     coded by: Drain Bramage                               //
//---------------------------------------------------------------------------//
//credit to: HellNight, Lt Earthworm, Southtown, and most expecialy The_Force//       //
//                also credit to the AdvancedMod coding forums               //
//---------------------------------------------------------------------------//
//Features:                                                                  //
//This nuke is roughly 1000m tall and 150-200m across.  It has most of the   //
//features of an accualy atomic bomb: The mushroom cloud, impact smoke, smoke//
//ring, debris, shockwaves and compression rings.  If you have Burn Damage in//
//your mod this nuke will set stuff on fire.(want the Burn Damage?, look     //
//for it at http://www.advancedmod.com/forums/index.php)  There is also a    //
//warning message and 1 minute countdown displayed in the chat hud, complete //
//with sounds, when this sucker is fired.  Lastly there is "ash" that falls  //
//over the entire map for 20 seconds after the final explosion.              //
//---------------------------------------------------------------------------//
//***New Weapon Image on its way*** ***Maybe a cleaned up version***         //
//---------------------------------------------------------------------------//
//Notes:                                                                     //
//-Right now the nuke is set up for testing settings, and is very weak       //
// for its size, feel free to adjust damage amounts and damage radii to your //
// likeing.                                                                  //
//                                                                           //
//-I left several unused particles and emitters and a 2nd "debris"           //
//projectile with its onExplode for anyone that might want to use them.      //
//                                                                           //
//-Use the nuke how ever you like but please give credit where its due.      //
//                                                                           //
//-This nuke LAGS, it was not made to be spammed, infact, more than one going //
//off in a semi-full server will probaly crash the server, if it doesnt crash //
//it most of the players will probaly leave due to the lag.  This nuke was    //
//made for "eyecandy" lovers like myself.                                     //
//----------------------------------------------------------------------------//
//***!!!IMPORTANT!!!***                                                       //
//-You will need to create new audio descriptions for the sounds to work.     //
//It is very easy to do, simply open up serverAudio.cs, copy the              //
//AudioBIGExplosion3d decription, change the name to AudioNukeUpProjectile3d  //
//and AudioNukeTopExplosion3d adjust the settings.  I sugest a maximum of     //
//between 1000 and 2000 and a minimum of around half your maximum.            //
//                                                                            //
//-The whiteout will not work unless u have function raidusWhiteout.          //
//I will not include this function in this file because i do not have permission
//from The_Force to give it out.  I'm also to lazy to ask.  Go to             //
//http://www.advancedmod.com/forums/index.php and find it yourself.           //
//----------------------------------------------------------------------------//
//And that concludes all the technical crap, I wrote all that because its 3 in//
//the morning and I'm bored off my ass.  I hope you enjoy "Project Uber"      //
//because it took me a period of around 36 hours over 3 days.  Plus what ever //
//time it takes me to make the weapon image.(work for tomarrow)               //
//                           -Drain Bramage                                   //
////////////////////////////////////////////////////////////////////////////////
datablock AudioProfile(NukeExplosionSound)
{
   filename    = "fx/Bonuses/upward_passback1_bomb.wav";
   description = AudioBIGExplosion3d;
   preload = true;
};

datablock AudioProfile(NukeBottomExplosionSound)
{
   filename    = "fx/explosions/vehicle_explosion.wav";
   description = AudioBIGExplosion3d;
   preload = true;
};

datablock AudioProfile(NukeTopExplosionSound)
{
   filename    = "fx/vehicles/bomber_bomb_impact.wav";
   description = AudioNukeTopExplosion3d;
   preload = true;
};

datablock AudioProfile(NukeUpProjectileSound)
{
   filename    = "fx/vehicles/htransport_boost.wav";
   description = AudioUpProjectile3d;
};

datablock AudioProfile(NukeFireSound)
{
   filename    = "fx/vehicles/tank_mortar_fire.wav";
   description = AudioClosest3d;
};

datablock AudioProfile(NukeReloadSound)
{
   filename    = "fx/vehicles/bomber_turret_activate.wav";
   description = AudioClosest3d;
};
datablock AudioProfile(NukeSwitchSound)
{
   filename    = "fx/misc/cannonstart.wav";
   description = AudioBIGExplosion3d;
};
//nuke explosion emitter
datablock ParticleData(NukeExplosionParticle)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 10000;
   lifetimeVarianceMS   = 150;
   useInvAlpha          = true;
   textureName          = "particleTest";
   colors[0]     = "0.1 0.1 0.1 1.0";
   colors[1]     = "0.1 0.1 0.1 0.3";
   sizes[0]      = 0.5;
   sizes[1]      = 2;
};

datablock ParticleEmitterData(NukeExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 100;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "NukeExplosionParticle";
};

//nuke debris
//--------------------------------------------------------------
// Nuke Debris Fire Particles
//--------------------------------------------------------------

datablock ParticleData(NukeDebrisFireParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 350;
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
   sizes[0]      = 0.5;
   sizes[1]      = 2.0;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(NukeDebrisFireEmitter)
{
   ejectionPeriodMS = 20;
   periodVarianceMS = 1;

   ejectionVelocity = 0.25;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 30.0;

   particles = "NukeDebrisFireParticle";
};
//--------------------------------------------------------------
// Nuke Debris Smoke Particles
//--------------------------------------------------------------

datablock ParticleData( NukeDebrisSmokeParticle )
{
   dragCoeffiecient     = 4.0;
   gravityCoefficient   = -0.00;   // rises slowly
   inheritedVelFactor   = 0.2;

   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 100;   // ...more or less

   textureName          = "particleTest";

   useInvAlpha =     true;

   spinRandomMin = -50.0;
   spinRandomMax = 50.0;

   colors[0]     = "0.3 0.3 0.3 0.0";
   colors[1]     = "0.3 0.3 0.3 1.0";
   colors[2]     = "0.0 0.0 0.0 0.0";
   sizes[0]      = 2;
   sizes[1]      = 3;
   sizes[2]      = 5;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( NukeDebrisSmokeEmitter )
{
   ejectionPeriodMS = 25;
   periodVarianceMS = 5;

   ejectionVelocity = 1.0;  // A little oomph at the back end
   velocityVariance = 0.5;

   thetaMin         = 10.0;
   thetaMax         = 30.0;

   useEmitterSizes = true;

   particles = "NukeDebrisSmokeParticle";
};

datablock DebrisData( NukeDebris )
{
   emitters[0] = BurningAshEmitter;//was NukeDebrisSmokeEmitter
   emitters[1] = NukeDebrisFireEmitter;

   explosion = DebrisExplosion;
   explodeOnMaxBounce = true;

   elasticity = 0.4;
   friction = 0.2;

   lifetime = 100.0;
   lifetimeVariance = 30.0;

   numBounces = 0;
   bounceVariance = 0;
};

//bottom smoke
datablock ParticleData(BottomSmoke1)
{
   dragCoefficient      = 1.0;
   windCoefficient      = 0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 15000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.5 0.5 0.5 0.41";
   colors[1]     = "0.4 0.4 0.4 0.41";
   colors[2]     = "0.3 0.3 0.3 0.41";
   sizes[0]      = 23.4;
   sizes[1]      = 30.2;
   sizes[2]      = 30.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(BottomSmokeEmitter1)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 310.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 87;
   thetaMax         = 87;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 5000;
   particles = "BottomSmoke1";
};

datablock ParticleData(BottomSmoke2)
{
   dragCoefficient      = 1.0;
   windCoefficient      = 0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 15000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.5 0.5 0.5 0.41";
   colors[1]     = "0.4 0.4 0.4 0.41";
   colors[2]     = "0.3 0.3 0.3 0.41";
   sizes[0]      = 23.4;
   sizes[1]      = 30.2;
   sizes[2]      = 30.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(BottomSmokeEmitter2)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 305.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 84;
   thetaMax         = 84;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 5000;
   particles = "BottomSmoke2";
};

datablock ParticleData(BottomSmoke3)
{
   dragCoefficient      = 1.0;
   windCoefficient      = 0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 15000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.5 0.5 0.5 0.41";
   colors[1]     = "0.4 0.4 0.4 0.41";
   colors[2]     = "0.3 0.3 0.3 0.41";
   sizes[0]      = 23.4;
   sizes[1]      = 30.2;
   sizes[2]      = 30.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(BottomSmokeEmitter3)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 290.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 81;
   thetaMax         = 81;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 5000;
   particles = "BottomSmoke3";
};

datablock ParticleData(BottomSmoke4)
{
   dragCoefficient      = 1.0;
   windCoefficient      = 0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 15000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.5 0.5 0.5 0.41";
   colors[1]     = "0.4 0.4 0.4 0.41";
   colors[2]     = "0.3 0.3 0.3 0.41";
   sizes[0]      = 23.4;
   sizes[1]      = 30.2;
   sizes[2]      = 30.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(BottomSmokeEmitter4)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 275.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 78;
   thetaMax         = 78;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 5000;
   particles = "BottomSmoke4";
};

datablock ParticleData(BottomSmoke5)
{
   dragCoefficient      = 1.0;
   windCoefficient      = 0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 10000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.5 0.5 0.5 0.41";
   colors[1]     = "0.4 0.4 0.4 0.41";
   colors[2]     = "0.3 0.3 0.3 0.41";
   sizes[0]      = 23.4;
   sizes[1]      = 30.2;
   sizes[2]      = 30.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(BottomSmokeEmitter5)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 250.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 75;
   thetaMax         = 75;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 5000;
   particles = "BottomSmoke5";
};

datablock ParticleData(BottomSmoke6)
{
   dragCoefficient      = 1.0;
   windCoefficient      = 0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 10000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.5 0.5 0.5 0.41";
   colors[1]     = "0.4 0.4 0.4 0.41";
   colors[2]     = "0.3 0.3 0.3 0.41";
   sizes[0]      = 23.4;
   sizes[1]      = 30.2;
   sizes[2]      = 30.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(BottomSmokeEmitter6)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 235.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 75;
   thetaMax         = 75;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 5000;
   particles = "BottomSmoke6";
};

datablock ParticleData(BottomSmoke7)
{
   dragCoefficient      = 1.0;
   windCoefficient      = 0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 10000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.5 0.5 0.5 0.41";
   colors[1]     = "0.4 0.4 0.4 0.41";
   colors[2]     = "0.3 0.3 0.3 0.41";
   sizes[0]      = 23.4;
   sizes[1]      = 30.2;
   sizes[2]      = 30.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(BottomSmokeEmitter7)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 180.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 75;
   thetaMax         = 75;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 5000;
   particles = "BottomSmoke7";
};
//middle dust ring
datablock ParticleData(NukeRing)
{
   dragCoefficient      = 1.0;
   windCoefficient      = 0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 15000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.6 0.3 0.2 0.41";
   colors[1]     = "0.6 0.3 0.2 0.41";
   colors[2]     = "0.6 0.3 0.2 0.41";
   sizes[0]      = 29.4;
   sizes[1]      = 32.2;
   sizes[2]      = 33.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(NukeRingEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 200.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 500;
   particles = "NukeRing";
};
//condensation rings
datablock ParticleData(NukeDust)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = -0.03;
   windCoefficient      = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.005;
   lifetimeMS           = 10000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "1.0 1.0 1.0 0.2";
   colors[1]     = "1.0 1.0 1.0 0.2";
   colors[2]     = "1.0 1.0 1.0 0.2";
   sizes[0]      = 39.2;
   sizes[1]      = 42.6;
   sizes[2]      = 43.0;
   times[0]      = 0.0;
   times[1]      = 2.0;
   times[2]      = 4.0;
};

datablock ParticleData(NukeDust2)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = -0.03;
   windCoefficient      = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.005;
   lifetimeMS           = 10000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "1.0 1.0 1.0 0.2";
   colors[1]     = "1.0 1.0 1.0 0.2";
   colors[2]     = "1.0 1.0 1.0 0.2";
   sizes[0]      = 39.2;
   sizes[1]      = 42.6;
   sizes[2]      = 43.0;
   times[0]      = 0.0;
   times[1]      = 2.0;
   times[2]      = 4.0;
};

datablock ParticleData(NukeDust3)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = -0.03;
   windCoefficient      = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.005;
   lifetimeMS           = 10000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "1.0 1.0 1.0 0.2";
   colors[1]     = "1.0 1.0 1.0 0.2";
   colors[2]     = "1.0 1.0 1.0 0.2";
   sizes[0]      = 39.2;
   sizes[1]      = 42.6;
   sizes[2]      = 43.0;
   times[0]      = 0.0;
   times[1]      = 2.0;
   times[2]      = 4.0;
};

datablock ParticleEmitterData(NukeDustEmitter)
{
   ejectionPeriodMS = 0;
   delayMS          = 30;
   periodVarianceMS = 0;
   ejectionVelocity = 475.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 110;
   thetaMax         = 110;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 500;
   particles = "NukeDust";
};

datablock ParticleEmitterData(UpAngleNukeDustEmitter)
{
   ejectionPeriodMS = 0;
   delayMS          = 30;
   periodVarianceMS = 0;
   ejectionVelocity = 475.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 135;
   thetaMax         = 135;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 500;
   particles = "NukeDust3";
};

datablock ParticleEmitterData(DownAngleNukeDustEmitter)
{
   ejectionPeriodMS = 0;
   delayMS          = 30;
   periodVarianceMS = 0;
   ejectionVelocity = 475.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 135;
   thetaMax         = 135;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 500;
   particles = "NukeDust3";
};
//added these ones some 15 hours after the ones up above, thats y there not in order....
datablock ParticleData(NukeCond1)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = -0.03;
   windCoefficient      = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.025;
   lifetimeMS           = 12000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "1.0 1.0 1.0 0.2";
   colors[1]     = "1.0 1.0 1.0 0.2";
   colors[2]     = "1.0 1.0 1.0 0.2";
   sizes[0]      = 39.2;
   sizes[1]      = 42.6;
   sizes[2]      = 43.0;
   times[0]      = 0.0;
   times[1]      = 2.0;
   times[2]      = 4.0;
};

datablock ParticleEmitterData(NukeCondEmitter1)
{
   ejectionPeriodMS = 0;
   delayMS          = 0;
   periodVarianceMS = 0;
   ejectionVelocity = 425.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 135;
   thetaMax         = 135;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 5000;
   particles = "NukeCond1";
};

datablock ParticleData(NukeCond2)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = -0.03;
   windCoefficient      = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.025;
   lifetimeMS           = 12000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "1.0 1.0 1.0 0.2";
   colors[1]     = "1.0 1.0 1.0 0.2";
   colors[2]     = "1.0 1.0 1.0 0.2";
   sizes[0]      = 39.2;
   sizes[1]      = 42.6;
   sizes[2]      = 43.0;
   times[0]      = 0.0;
   times[1]      = 2.0;
   times[2]      = 4.0;
};

datablock ParticleEmitterData(NukeCondEmitter2)
{
   ejectionPeriodMS = 0;
   delayMS          = 0;
   periodVarianceMS = 0;
   ejectionVelocity = 475.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 120;
   thetaMax         = 120;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 5000;
   particles = "NukeCond2";
};

datablock ParticleData(NukeCond3)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = -0.03;
   windCoefficient      = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.025;
   lifetimeMS           = 12000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "1.0 1.0 1.0 0.2";
   colors[1]     = "1.0 1.0 1.0 0.2";
   colors[2]     = "1.0 1.0 1.0 0.2";
   sizes[0]      = 39.2;
   sizes[1]      = 42.6;
   sizes[2]      = 43.0;
   times[0]      = 0.0;
   times[1]      = 2.0;
   times[2]      = 4.0;
};

datablock ParticleEmitterData(NukeCondEmitter3)
{
   ejectionPeriodMS = 0;
   delayMS          = 0;
   periodVarianceMS = 0;
   ejectionVelocity = 475.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 105;
   thetaMax         = 105;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 5000;
   particles = "NukeCond3";
};

//shockwaves
datablock ShockwaveData(middleShockwave)
{
   width = 500.0;
   numSegments = 190;
   numVertSegments = 150;
   velocity = 5;
   acceleration = 1.0;
   lifetimeMS = 10500;
   height = 50.0;
   verticalCurve = 0.25;
   is2D = false;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "1.0 0.0 0.0 0.50";
   colors[1] = "1.0 0.0 0.0 0.25";
   colors[2] = "1.0 0.0 0.0 0.0";

   mapToTerrain = false;
   orientToNormal = false;
   renderBottom = false;
};

datablock ShockwaveData(TopShockwave)
{
   width = 400.0;
   numSegments = 190;
   numVertSegments = 150;
   velocity = 50;
   acceleration = 10.0;
   lifetimeMS = 10000;
   height = 75.0;
   verticalCurve = 0.25;
   is2D = false;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "1.0 0.5 0.0 0.5";
   colors[1] = "1.0 0.5 0.0 0.5";
   colors[2] = "1.0 0.5 0.0 0.0";

   mapToTerrain = false;
   orientToNormal = false;
   renderBottom = true;
};

datablock ShockwaveData(bottomShockwave)
{
   width = 200.0;
   numSegments = 50;
   numVertSegments = 56;
   velocity = 200;
   acceleration = 1.0;
   lifetimeMS = 17000;
   height = 10.0;
   verticalCurve = 0.25;
   is2D = false;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 100.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "1.0 1.0 1.0 0.50";
   colors[1] = "1.0 1.0 1.0 0.25";
   colors[2] = "1.0 1.0 1.0 0.0";

   mapToTerrain = false;
   orientToNormal = false;
   renderBottom = true;
};


//--------------------------------------------------------------------------
// Explosion Particle effects
//--------------------------------------
datablock ParticleData( NukeCrescentParticle )
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 3600;
   lifetimeVarianceMS   = 000;
   textureName          = "special/crescent3";
   colors[0]     = "0.5 0.0 0.0 1.0";
   colors[1]     = "0.5 0.0 0.0 0.5";
   colors[2]     = "0.5 0.0 0.0 0.0";
   sizes[0]      = 4.0;
   sizes[1]      = 8.0;
   sizes[2]      = 9.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( NukeCrescentEmitter )
{
   ejectionPeriodMS = 25;
   periodVarianceMS = 0;
   ejectionVelocity = 40;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 2000;
   particles = "NukeCrescentParticle";
};

//---------------------------------------------------------------------------
// Explosion/s
//---------------------------------------------------------------------------

datablock ExplosionData(BottomSubExplosion1)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   emitter[0] = BottomSmokeEmitter5;
   //emitter[1] = NukeCondEmitter1;

   delayMS = 100;



   playSpeed = 0.025;

   sizes[0] = "58.05 58.05 58.05";
   sizes[1] = "58.05 58.05 58.05";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(BottomSubExplosion2)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   //emitter[0] = NukeCondEmitter3;
   //emitter[1] = NukeCondEmitter2;

   delayMS = 500;



   playSpeed = 0.025;

   sizes[0] = "58.05 58.05 58.05";
   sizes[1] = "58.05 58.05 58.05";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(BottomSubExplosion3)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   emitter[0] = BottomSmokeEmitter6;
   //emitter[1] = NukeCondEmitter3;

   delayMS = 1000;



   playSpeed = 0.025;

   sizes[0] = "58.05 58.05 58.05";
   sizes[1] = "58.05 58.05 58.05";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(BottomExplosion)
{
   soundProfile   = NukeBottomExplosionSound;
   
   subExplosion[0] = BottomSubExplosion1;
   subExplosion[1] = BottomSubExplosion2;
   subExplosion[2] = BottomSubExplosion3;

   shockwave = bottomShockwave;
   shockwaveOnTerrain = true;
   
   debris = NukeDebris;
   debrisThetaMin = 10;
   debrisThetaMax = 60;
   debrisNum = 15;
   debrisNumVariance = 5;
   debrisVelocity = 75.0;
   debrisVelocityVariance = 5.0;

   particleEmitter = PlasmaExplosionEmitter;
   particleDensity = 10000;
   particleRadius = 50.25;
   faceViewer = true;

   emitter[0] = BottomSmokeEmitter1;
   emitter[1] = BottomSmokeEmitter2;
   emitter[2] = BottomSmokeEmitter3;
   emitter[3] = BottomSmokeEmitter4;

   shakeCamera = true;
   camShakeFreq = "9900.0 8900.0 7800.0";
   camShakeAmp = "1000.0 1000.0 1000.0";
   camShakeDuration = 5.0;
   camShakeRadius = 125.0;
};

datablock ExplosionData(MidSubExplosion1)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;

   delayMS = 100;



   playSpeed = 0.025;

   sizes[0] = "44.05 44.05 44.05";
   sizes[1] = "44.05 44.05 44.05";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(MidSubExplosion2)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;

   delayMS = 500;



   playSpeed = 0.025;

   sizes[0] = "44.05 44.05 44.05";
   sizes[1] = "44.05 44.05 44.05";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(MidSubExplosion3)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;

   delayMS = 1000;



   playSpeed = 0.025;

   sizes[0] = "44.05 44.05 44.05";
   sizes[1] = "44.05 44.05 44.05";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(MidExplosion)
{
   soundProfile   = NukeExplosionSound;

   subExplosion[0] = MidSubExplosion1;
   subExplosion[1] = MidSubExplosion2;
   subExplosion[2] = MidSubExplosion3;

   //shockwave = AirBlastShockwave;
   //shockwaveOnTerrain = false;
   
   debris = NukeDebris;
   debrisThetaMin = 10;
   debrisThetaMax = 60;
   debrisNum = 15;
   debrisNumVariance = 5;
   debrisVelocity = 75.0;
   debrisVelocityVariance = 5.0;
   
   particleEmitter = PlasmaExplosionEmitter;
   particleDensity = 10000;
   particleRadius = 40.25;
   faceViewer = true;

   emitter[0] = NukeExplosionSmokeEmitter;
   emitter[1] = NukeCrescentEmitter;
   //emitter[2] = NukeDustEmitter;

   shakeCamera = true;
   camShakeFreq = "9900.0 8900.0 7800.0";
   camShakeAmp = "1000.0 1000.0 1000.0";
   camShakeDuration = 5.0;
   camShakeRadius = 50.0;
};

datablock ExplosionData(RingExplosion)
{
   soundProfile   = NukeExplosionSound;

   subExplosion[0] = MidSubExplosion1;
   subExplosion[1] = MidSubExplosion2;
   subExplosion[2] = MidSubExplosion3;

   //shockwave = AirBlastShockwave;
   //shockwaveOnTerrain = false;
   
   debris = NukeDebris;
   debrisThetaMin = 10;
   debrisThetaMax = 60;
   debrisNum = 15;
   debrisNumVariance = 5;
   debrisVelocity = 75.0;
   debrisVelocityVariance = 5.0;
   
   
   particleEmitter = PlasmaExplosionEmitter;
   particleDensity = 10000;
   particleRadius = 40.25;
   faceViewer = true;

   emitter[0] = NukeCondEmitter1;
   emitter[1] = NukeCondEmitter2;
   emitter[2] = NukeCondEmitter3;
   emitter[3] = NukeRingEmitter;

   shakeCamera = true;
   camShakeFreq = "9900.0 8900.0 7800.0";
   camShakeAmp = "1000.0 1000.0 1000.0";
   camShakeDuration = 5.0;
   camShakeRadius = 150.0;
};

datablock ExplosionData(TopSubExplosion1)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   soundProfile   = NukeTopExplosionSound;

   delayMS = 100;



   playSpeed = 0.025;

   sizes[0] = "155.05 130.05 130.05";
   sizes[1] = "305.05 280.05 280.05";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(TopSubExplosion2)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   soundProfile   = NukeTopExplosionSound;

   delayMS = 500;


   playSpeed = 0.025;

  sizes[0] = "155.05 130.05 130.05";
   sizes[1] = "305.05 280.05 280.05";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(TopSubExplosion3)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   soundProfile   = NukeTopExplosionSound;

   delayMS = 1000;



   playSpeed = 0.025;

  sizes[0] = "155.05 130.05 130.05";
   sizes[1] = "305.05 280.05 280.05";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(TopSubExplosion4)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   soundProfile   = NukeTopExplosionSound;

   delayMS = 100;



   playSpeed = 0.025;

   sizes[0] = "155.05 130.05 130.05";
   sizes[1] = "305.05 280.05 280.05";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(TopSubExplosion5)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   soundProfile   = NukeTopExplosionSound;

   delayMS = 500;


   playSpeed = 0.025;

   sizes[0] = "155.05 130.05 130.05";
   sizes[1] = "305.05 280.05 280.05";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(TopSubExplosion6)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   soundProfile   = NukeTopExplosionSound;


   delayMS = 1000;

   playSpeed = 0.025;

   sizes[0] = "155.05 130.05 130.05";
   sizes[1] = "305.05 280.05 280.05";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(TopExplosion)
{
   soundProfile   = NukeTopExplosionSound;

   subExplosion[0] = TopSubExplosion1;
   subExplosion[1] = TopSubExplosion2;
   subExplosion[2] = TopSubExplosion3;
   subExplosion[3] = TopSubExplosion4;
   subExplosion[4] = TopSubExplosion5;
   subExplosion[5] = TopSubExplosion6;

   shockwave = TopShockwave;
   shockwaveOnTerrain = false;
   
   debris = NukeDebris;
   debrisThetaMin = 10;
   debrisThetaMax = 60;
   debrisNum = 250;
   debrisNumVariance = 5;
   debrisVelocity = 125.0;
   debrisVelocityVariance = 5.0;
   
   particleEmitter = PlasmaExplosionEmitter;
   particleDensity = 15000;
   particleRadius = 275.25;
   faceViewer = true;

   emitter[0] = NukeCrescentEmitter;
   emitter[1] = NukeDustEmitter;
   emitter[2] = UpAngleNukeDustEmitter;
   //emitter[2] = DownAngleNukeDustEmitter;

   shakeCamera = true;
   camShakeFreq = "1000.0 1000.0 1000.0";
   camShakeAmp = "1000.0 900.0 1500.0";
   camShakeDuration = 8.0;
   camShakeRadius = 1500.0;
};

//---------------------------------------------------------------------------
// Smoke particles
//---------------------------------------------------------------------------
datablock ParticleData(CoolSmokeParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.0;   // rises slowly
   inheritedVelFactor   = 0.00;

   lifetimeMS           = 1700;  // lasts 2 second
   lifetimeVarianceMS   = 150;   // ...more or less

   textureName          = "particleTest";

   useInvAlpha = true;
   spinRandomMin = -30.0;
   spinRandomMax = 30.0;

   colors[0]     = "0.5 0.3 0.1 1.0";
   colors[1]     = "0.5 0.3 0.1 0.5";
   colors[2]     = "0.5 0.3 0.1 0.1";

   sizes[0]      = 5.25;
   sizes[1]      = 6.0;
   sizes[2]      = 8.0;

   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(CoolSmokeEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 0.25;
   velocityVariance = 0.50;

   thetaMin         = -10.0;
   thetaMax         = 10.0;

   particles = "CoolSmokeParticle";
};

datablock ParticleData(CannonSmokeParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.0;   // rises slowly
   inheritedVelFactor   = 0.00;

   lifetimeMS           = 10700;  // lasts 2 second
   lifetimeVarianceMS   = 150;   // ...more or less

   textureName          = "special/flareSpark";

   useInvAlpha = false;
   spinRandomMin = -30.0;
   spinRandomMax = 30.0;

   colors[0]     = "0.0 1.0 0.4 0.3";
   colors[1]     = "0.0 1.0 0.4 0.3";
   colors[2]     = "0.0 1.0 0.4 0.1";

   sizes[0]      = 45.25;
   sizes[1]      = 46.0;
   sizes[2]      = 38.0;

   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(CannonSmokeEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 0.25;
   velocityVariance = 0.50;

   thetaMin         = -10.0;
   thetaMax         = 10.0;

   particles = "CannonSmokeParticle";
};

datablock ParticleData(BurningAshParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.0;   // rises slowly
   inheritedVelFactor   = 0.00;

   lifetimeMS           = 700;  // lasts 2 second
   lifetimeVarianceMS   = 150;   // ...more or less

   textureName          = "particleTest";

   useInvAlpha = true;
   spinRandomMin = -30.0;
   spinRandomMax = 30.0;

   colors[0]     = "1.0 1.0 0.0 1.0";
   colors[1]     = "0.8 0.5 0.0 1.0";
   colors[2]     = "0.5 0.2 0.0 0.0";

   sizes[0]      = 0.25;
   sizes[1]      = 0.15;
   sizes[2]      = 0.05;

   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(BurningAshEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionVelocity = 1.25;
   velocityVariance = 0.50;

   thetaMin         = 0.0;
   thetaMax         = 0.0;

   particles = "BurningAshParticle";
};


//--------------------------------------------------------------------------
// Projectile
//--------------------------------------
datablock TargetProjectileData(RedTargeterBeam)
{
   directDamage        	= 0.0;
   hasDamageRadius     	= false;
   indirectDamage      	= 0.0;
   damageRadius        	= 0.0;
   velInheritFactor    	= 1.0;

   maxRifleRange       	= 1000;
   beamColor           	= "1.0 0.1 0.1";

   startBeamWidth			= 0.50;
   pulseBeamWidth 	   = 1.15;
   beamFlareAngle 	   = 3.0;
   minFlareSize        	= 0.0;
   maxFlareSize        	= 400.0;
   pulseSpeed          	= 6.0;
   pulseLength         	= 0.150;

   textureName[0]      	= "special/nonlingradient";
   textureName[1]      	= "special/flare";
   textureName[2]      	= "special/pulse";
   textureName[3]      	= "special/expFlare";
   beacon               = true;
};

datablock TargetProjectileData(NukeTargeterBeam)
{
   directDamage        	= 0.0;
   hasDamageRadius     	= false;
   indirectDamage      	= 0.0;
   damageRadius        	= 0.0;
   velInheritFactor    	= 1.0;

   maxRifleRange       	= 1000;
   beamColor           	= "0.1 1.0 1.0";

   startBeamWidth			= 0.20;
   pulseBeamWidth 	   = 0.15;
   beamFlareAngle 	   = 3.0;
   minFlareSize        	= 0.0;
   maxFlareSize        	= 400.0;
   pulseSpeed          	= 6.0;
   pulseLength         	= 0.150;

   textureName[0]      	= "special/nonlingradient";
   textureName[1]      	= "special/flare";
   textureName[2]      	= "special/pulse";
   textureName[3]      	= "special/expFlare";
   beacon               = true;
};

datablock LinearFlareProjectileData(TargeterShot)
{
   scale               = "0.1 0.1 0.1";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.01;
   damageRadius        = 0.01;
   radiusDamageType    = $DamageType::Plasma;
   kickBackStrength    = 0;

   explosion           = "SniperExplosion";
   splash              = PlasmaSplash;


   dryVelocity       = 5095.0;
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 29000;
   lifetimeMS        = 30000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;


   numFlares         = 35;
   flareColor        = "0 0 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = false;
};

datablock LinearFlareProjectileData(NukeBolt)
{
   scale               = "70.0 70.0 70.0";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.02;
   damageRadius        = 1000.0;
   radiusDamageType    = $DamageType::Plasma;  //CHANGE BACK TO FIRE!!!
   kickBackStrength    = 55000;

   explosion           = "BottomExplosion";
   splash              = PlasmaSplash;
   
   baseEmitter         = CannonSmokeEmitter;

   dryVelocity       = 295.0;
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 29000;
   lifetimeMS        = 30000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;


   numFlares         = 350;
   flareColor        = "1 0.45 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock LinearProjectileData(UpPart1)
{
   projectileShapeName = "weapon_missile_projectile.dts";
   directDamage        = 0.2;
   hasDamageRadius     = true;
   damageRadius        = 350.0;
   indirectDamage      = 1.01;
   kickBackStrength    = 3000.0;
   radiusDamageType    = $DamageType::Fire;
   
   explosion           = "MidExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 300;
   wetVelocity       = 300;
   velInheritFactor  = 0;
   fizzleTimeMS      = 0;
   lifetimeMS        = 237;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 200;


	sound        = NukeUpProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock LinearProjectileData(UpPart2)
{
   projectileShapeName = "weapon_missile_projectile.dts";
   directDamage        = 0.2;
   hasDamageRadius     = true;
   damageRadius        = 350.0;
   indirectDamage      = 1.1;
   kickBackStrength    = 3000.0;
   radiusDamageType    = $DamageType::Fire;

   explosion           = "MidExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 300;
   wetVelocity       = 300;
   velInheritFactor  = 0;
   fizzleTimeMS      = 0;
   lifetimeMS        = 237;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 200;
   
   sound        = NukeUpProjectileSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock LinearProjectileData(UpPart3)
{
   projectileShapeName = "weapon_missile_projectile.dts";
   directDamage        = 0.2;
   hasDamageRadius     = true;
   damageRadius        = 350.0;
   indirectDamage      = 1.1;
   kickBackStrength    = 3000.0;
   radiusDamageType    = $DamageType::Fire;

   explosion           = "MidExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 300;
   wetVelocity       = 300;
   velInheritFactor  = 0;
   fizzleTimeMS      = 0;
   lifetimeMS        = 237;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 200;

	sound        = NukeUpProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock LinearProjectileData(UpPartRing)
{
   projectileShapeName = "weapon_missile_projectile.dts";
   directDamage        = 0.2;
   hasDamageRadius     = true;
   damageRadius        = 450.0;
   indirectDamage      = 1.5;
   kickBackStrength    = 5000.0;
   radiusDamageType    = $DamageType::Fire;

   explosion           = "RingExplosion";
   splash              = PlasmaSplash;

  dryVelocity       = 300;
   wetVelocity       = 300;
   velInheritFactor  = 0;
   fizzleTimeMS      = 0;
   lifetimeMS        = 237;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 200;

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};
datablock LinearProjectileData(UpPart4)
{
   projectileShapeName = "weapon_missile_projectile.dts";
   directDamage        = 0.2;
   hasDamageRadius     = true;
   damageRadius        = 350.0;
   indirectDamage      = 1.1;
   kickBackStrength    = 3000.0;
   radiusDamageType    = $DamageType::Fire;

   explosion           = "MidExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 300;
   wetVelocity       = 300;
   velInheritFactor  = 0;
   fizzleTimeMS      = 0;
   lifetimeMS        = 237;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 200;

	sound        = NukeUpProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock LinearProjectileData(UpPart5)
{
   projectileShapeName = "weapon_missile_projectile.dts";
   directDamage        = 0.2;
   hasDamageRadius     = true;
   damageRadius        = 350.0;
   indirectDamage      = 1.1;
   kickBackStrength    = 3000.0;
   radiusDamageType    = $DamageType::Fire;

   explosion           = "MidExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 300;
   wetVelocity       = 300;
   velInheritFactor  = 0;
   fizzleTimeMS      = 0;
   lifetimeMS        = 237;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 200;

	sound        = NukeUpProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock LinearProjectileData(UpPart6)
{
   projectileShapeName = "weapon_missile_projectile.dts";
   directDamage        = 0.2;
   hasDamageRadius     = true;
   damageRadius        = 350.0;
   indirectDamage      = 1.1;
   kickBackStrength    = 3000.0;
   radiusDamageType    = $DamageType::Fire;

   explosion           = "MidExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 300;
   wetVelocity       = 300;
   velInheritFactor  = 0;
   fizzleTimeMS      = 0;
   lifetimeMS        = 237;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 200;

	sound        = NukeUpProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock LinearProjectileData(UpPart7)
{
   projectileShapeName = "weapon_missile_projectile.dts";
   directDamage        = 0.2;
   hasDamageRadius     = true;
   damageRadius        = 350.0;
   indirectDamage      = 1.1;
   kickBackStrength    = 3000.0;
   radiusDamageType    = $DamageType::Fire;

   explosion           = "MidExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 300;
   wetVelocity       = 300;
   velInheritFactor  = 0;
   fizzleTimeMS      = 0;
   lifetimeMS        = 237;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 200;

	sound        = NukeUpProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock LinearProjectileData(UpPart8)
{
   projectileShapeName = "weapon_missile_projectile.dts";
   directDamage        = 0.2;
   hasDamageRadius     = true;
   damageRadius        = 350.0;
   indirectDamage      = 1.1;
   kickBackStrength    = 3000.0;
   radiusDamageType    = $DamageType::Fire;

   explosion           = "MidExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 300;
   wetVelocity       = 300;
   velInheritFactor  = 0;
   fizzleTimeMS      = 0;
   lifetimeMS        = 237;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 200;

	sound        = NukeUpProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock LinearProjectileData(Top)
{

   projectileShapeName = "weapon_missile_projectile.dts";
   directDamage        = 0.2;
   hasDamageRadius     = true;
   damageRadius        = 550.0;
   indirectDamage      = 1.1;
   kickBackStrength    = 9500.0;
   radiusDamageType    = $DamageType::Fire;

   explosion           = "TopExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 300;
   wetVelocity       = 300;
   velInheritFactor  = 0;
   fizzleTimeMS      = 0;
   lifetimeMS        = 313;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 200;


	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock GrenadeProjectileData(DebrisGrenade)
{
   projectileShapeName = "grenade_projectile.dts";
   scale               = "0.1 0.1 0.1";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.40;
   damageRadius        = 5.0;
   radiusDamageType    = $DamageType::Fire;
   kickBackStrength    = 1500;
   bubbleEmitTime      = 1.0;

   sound               = GrenadeProjectileSound;
   explosion           = "PlasmaBoltExplosion";
   underwaterExplosion = "UnderwaterGrenadeExplosion";
   velInheritFactor    = 0.5;
   splash              = GrenadeSplash;


   baseEmitter         = MissileSmokeEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;
   
   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 1000;
   muzzleVelocity    = 47.00;
   drag = 0.1;
};

datablock GrenadeProjectileData(FireDebrisGrenade)
{
   projectileShapeName = "grenade_projectile.dts";
   scale               = "0.1 0.1 0.1";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.20;
   damageRadius        = 2.0;
   radiusDamageType    = $DamageType::Fire;
   kickBackStrength    = 1500;
   bubbleEmitTime      = 1.0;

   sound               = GrenadeProjectileSound;
   explosion           = "PlasmaBoltExplosion";
   underwaterExplosion = "UnderwaterGrenadeExplosion";
   velInheritFactor    = 0.5;
   splash              = GrenadeSplash;


   baseEmitter         = BurningAshEmitter;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.1;
   armingDelayMS     = 1000;
   muzzleVelocity    = 107.00;
   drag = 0.1;
};

//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(NukeAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_mortar.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some Nuke ammo";

   computeCRC = true;

};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ItemData(Nuke)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_mortar.dts";
   image = NukeImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Nuke";

   computeCRC = true;
   emap = true;
};

datablock ShapeBaseImageData(NukeImage)
{
   className = WeaponImage;
   shapeFile = "weapon_mortar.dts";
   item = Nuke;
   //ammo = NukeAmmo;
   offset = "0 0 0";
   emap = true;
   UsesEnergy = true;
   fireEnergy = 1;
   minEnergy = 1;

   projectile = TargeterShot;
   projectileType = LinearFlareProjectile;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = NukeSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";
   //stateSound[2] = MortarIdleSound;

   stateName[3] = "Fire";
   stateSequence[3] = "Recoil";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.8;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateSound[3] = NukeFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 2.0;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   stateSound[4] = NukeReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = MortarDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
};

function NukeBolt::onExplode(%data, %proj, %pos, %mod)
{
%start = getSimTime();
nukewind(VectorAdd(%pos,"0 0 50"),%start,6000);

if (%data.hasDamageRadius)
RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
for(%i=0;%i<1;%i++)
{
%p = new LinearProjectile()
{
dataBlock = UpPart1;
initialDirection = "0 0 1";
initialPosition = %pos;

};
MissionCleanup.add(%p);
}
//raidusWhiteout(%pos, 2500.0, 0.75);
whiteout(%pos,1000,2500); //used my own. :P
  Parent::onExplode(%data, %proj, %pos, %mod);


}

function UpPart1::onExplode(%data, %proj, %pos, %mod)
{
if (%data.hasDamageRadius)
RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
for(%i=0;%i<1;%i++)
{
%p = new LinearProjectile()
{
dataBlock = UpPart2;
initialDirection = "0 0 1";
initialPosition = %pos;

};
MissionCleanup.add(%p);
}
}

function UpPart2::onExplode(%data, %proj, %pos, %mod)
{
if (%data.hasDamageRadius)
RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
for(%i=0;%i<1;%i++)
{
%p = new LinearProjectile()
{
dataBlock = UpPart3;
initialDirection = "0 0 1";
initialPosition = %pos;

};
MissionCleanup.add(%p);
}
}

function UpPart3::onExplode(%data, %proj, %pos, %mod)
{
if (%data.hasDamageRadius)
RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
for(%i=0;%i<1;%i++)
{
%p = new LinearProjectile()
{
dataBlock = UpPartRing;
initialDirection = "0 0 1";
initialPosition = %pos;

};
MissionCleanup.add(%p);
}
}

function UpPartRing::onExplode(%data, %proj, %pos, %mod)
{
if (%data.hasDamageRadius)
RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
for(%i=0;%i<1;%i++)
{
%p = new LinearProjectile()
{
dataBlock = UpPart4;
initialDirection = "0 0 1";
initialPosition = %pos;

};
MissionCleanup.add(%p);
}

}
function UpPart4::onExplode(%data, %proj, %pos, %mod)
{
if (%data.hasDamageRadius)
RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
for(%i=0;%i<1;%i++)
{
%p = new LinearProjectile()
{
dataBlock = UpPart5;
initialDirection = "0 0 1";
initialPosition = %pos;

};
MissionCleanup.add(%p);
}
}

function UpPart5::onExplode(%data, %proj, %pos, %mod)
{
if (%data.hasDamageRadius)
RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
for(%i=0;%i<1;%i++)
{
%p = new LinearProjectile()
{
dataBlock = UpPart6;
initialDirection = "0 0 1";
initialPosition = %pos;

};
MissionCleanup.add(%p);
}
}

function UpPart6::onExplode(%data, %proj, %pos, %mod)
{
if (%data.hasDamageRadius)
RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
for(%i=0;%i<1;%i++)
{
%p = new LinearProjectile()
{
dataBlock = UpPart7;
initialDirection = "0 0 1";
initialPosition = %pos;

};
MissionCleanup.add(%p);
}
}

function UpPart7::onExplode(%data, %proj, %pos, %mod)
{
if (%data.hasDamageRadius)
RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
for(%i=0;%i<1;%i++)
{
%p = new LinearProjectile()
{
dataBlock = Top;
initialDirection = "0 0 1";
initialPosition = %pos;

};
MissionCleanup.add(%p);
}
}

function Top::onExplode(%data, %proj, %pos, %mod)
{
   if (%data.hasDamageRadius)
      RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
   for(%i=0;%i<75;%i++)
   {
      %x = (getRandom() * 2) - 1; //-1 to 1, ie: full circle
      %y = (getRandom() * 2) - 1;
      %z = (getRandom() * 2) - 1;
      %vec = %x SPC %y SPC %z; //Shoot any direction
      %p = new GrenadeProjectile() {
         dataBlock        = DebrisGrenade;
         initialDirection = %vec;
         initialPosition  = %pos;

      };
      MissionCleanup.add(%p);
   }
   if (%data.hasDamageRadius)
      RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
   for(%i=0;%i<0;%i++)
   {
      %x = (getRandom() * 2) - 1; //-1 to 1, ie: full circle
      %y = (getRandom() * 2) - 1;
      %z = (getRandom() * 2) - 1;
      %vec = %x SPC %y SPC %z; //Shoot any direction
      %p = new GrenadeProjectile() {
         dataBlock        = FireDebrisGrenade;
         initialDirection = %vec;
         initialPosition  = %pos;

      };
      MissionCleanup.add(%p);
   }
   
   %Ash = new Precipitation() {
      position = "0 0 0";
      rotation = "1 0 0 0";
      scale = "1 1 1";
      dataBlock = "Snow";
      percentage = "1";
      color1 = "0.3 0.3 0.3 1";
      color2 = "0.3 0.3 0.3 1";
      color3 = "0.3 0.3 0.3 1";
      offsetSpeed = "0.25";
      minVelocity = "0.25";
      maxVelocity = "1.5";
      maxNumDrops = "2000";
      maxRadius = "125";
         locked = "true";
   };
   
   MissionCleanup.add(%Ash);
   
   %Ash.schedule(20000, "delete");


}

function CmndMessageAll(%mes)
{
      messageAll('ALLMsg', %mes);
}

function NukeImage::onFire(%data, %obj, %slot)
{
   %vector = %obj.getMuzzleVector(%slot);
      %x = (getRandom() - 0.0) * 1 * 1 * %data.projectileSpread;
      %y = (getRandom() - 0.0) * 1 * 1 * %data.projectileSpread;
      %z = (getRandom() - 0.0) * 1 * 1 * %data.projectileSpread;
      %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
      %vector = MatrixMulVector(%mat, %vector);

   %p = new (%data.projectileType)() {
      dataBlock        = %data.projectile;
      initialDirection = %vector;
      initialPosition  = %obj.getMuzzlePoint(%slot);
      sourceObject     = %obj;
      sourceSlot       = %slot;
   };
   MissionCleanup.add(%p);
   %obj.lastProjectile = %p;
   %client.projectile = %p;


}

function NukeImage::onMount(%data, %obj, %node)
{
   Parent::onMount(%data, %obj, %node);
   %p = new TargetProjectile(){
                        dataBlock        = NukeTargeterBeam;
                        initialDirection = %obj.getMuzzleVector(%slot);
                        initialPosition  = %obj.getMuzzlePoint(%slot);
                        sourceObject     = %obj;
                        sourceSlot       = %slot;
                        vehicleObject    = %vehicle;
                     };
   MissionCleanup.add(%p);
   %p.setTarget(%obj.team);
   %obj.NukeTargBeam = %p;
   
   Parent::onMount(%this, %obj, %slot);
   commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:22><color:ff3300>Project Uber: Planetary Cannon Edition<spop>", 5, 2);
}

function NukeImage::onUnmount(%data, %obj, %node)
{
   if(isObject(%obj.NukeTargBeam))
      %obj.NukeTargBeam.delete();
   Parent::onUnmount(%data, %obj, %node);
}
//thanks to The_Force for these 2 functions
function BigFatNukeTarget(%pos)
{
   echo("nuke target position: " @ %pos);
   %mainUpPos = vectoradd(%pos, "0 0 1000");// 1000m up

   %lasePos1 = vectoradd(%mainUpPos, "1000 0 0");
   %laseVec1 = VectorAdd(%pos, VectorScale(%lasePos1, -1));
   //%laseVec1 = VectorNormalize(%laseVec1);
   %laser1 = new TargetProjectile()
   {
        dataBlock        = RedTargeterBeam;
        initialDirection = %laseVec1;
        initialPosition  = %lasePos1;
   };
   MissionCleanup.add(%laser1);

   %lasePos2 = vectoradd(%mainUpPos, "-1000 0 0");
   %laseVec2 = VectorAdd(%pos, VectorScale(%lasePos2, -1));
   //%laseVec2 = VectorNormalize(%lasePos2);
   %laser2 = new TargetProjectile()
   {
        dataBlock        = RedTargeterBeam;
        initialDirection = %laseVec2;
        initialPosition  = %lasePos2;
   };
   MissionCleanup.add(%laser2);

   %lasePos3 = vectoradd(%mainUpPos, "0 1000 0");
   %laseVec3 = VectorAdd(%pos, VectorScale(%lasePos3, -1));
   //%laseVec3 = VectorNormalize(%laseVec3);
   %laser3 = new TargetProjectile()
   {
        dataBlock        = RedTargeterBeam;
        initialDirection = %laseVec3;
        initialPosition  = %lasePos3;
   };
   MissionCleanup.add(%laser3);

   %lasePos4 = vectoradd(%mainUpPos, "0 -1000 0");
   %laseVec4 = VectorAdd(%pos, VectorScale(%lasePos4, -1));
   //%laseVec4 = VectorNormalize(%laseVec4);
   %laser4 = new TargetProjectile()
   {
        dataBlock        = RedTargeterBeam;
        initialDirection = %laseVec4;
        initialPosition  = %lasePos4;
   };
   MissionCleanup.add(%laser4);
   
   %client = %obj.sourceObject.client;
   messageAll("", "\c2A Planetary Cannon has been targeted!");
   messageAll("", "\c2Planetary Cannon fires in 1:00 min.~wfx/misc/red_alert.wav");

   schedule(30000,0, "CmndMessageAll",  '\c2Planetary Cannon fires in 30 seconds.~wfx/misc/hunters_30.wav');
   schedule(50000,0, "CmndMessageAll",  '\c2Planetary Cannon fires in 10 seconds.~wfx/misc/hunters_10.wav');
   schedule(55000,0, "CmndMessageAll",  '\c2Planetary Cannon fires in 5 seconds.~wfx/misc/hunters_5.wav');
   schedule(56000,0, "CmndMessageAll",  '\c2Planetary Cannon fires in 4 seconds.~wfx/misc/hunters_4.wav');
   schedule(57000,0, "CmndMessageAll",  '\c2Planetary Cannon fires in 3 seconds.~wfx/misc/hunters_3.wav');
   schedule(58000,0, "CmndMessageAll",  '\c2Planetary Cannon fires in 2 seconds.~wfx/misc/hunters_2.wav');
   schedule(59000,0, "CmndMessageAll",  '\c2Planetary Cannon fires in 1 second.~wfx/misc/hunters_1.wav');
   schedule(60000,0, "CmndMessageAll",  '\c2The Planetary Cannon has fired!');

   %laser1.schedule(61.5 * 1000, "delete");// Delete all the lasers after 1 min, 5 sec.
   %laser2.schedule(61.5 * 1000, "delete");
   %laser3.schedule(61.5 * 1000, "delete");
   %laser4.schedule(61.5 * 1000, "delete");

   schedule(60 * 1000, 0, BigFatNukeDrop, %mainUpPos);
}

function BigFatNukeDrop(%pos)
{

   echo("nuke target position: " @ %pos);
   %mainUpPos = vectoradd(%pos, "0 0 1000");// 1000m up

   %BigFatNuke = new LinearFlareProjectile()
   {
        dataBlock        = NukeBolt;
        initialPosition  = %mainUpPos;
        initialDirection = "0 0 -1";
   };
   MissionCleanup.add(%BigFatNuke);

   %alertPos = VectorAdd(%pos, "0 0 1000");// Meters from the target pos.
   %nukeAlert = new WayPoint()
   {
      position = %alertPos;
      rotation = "1 0 0 0";
      scale = "1 1 1";
      name = "Incoming Cannon Blast";
      dataBlock = "WayPointMarker";
      lockCount = "0";
      homingCount = "0";
   };
   MissionCleanup.add(%nukeAlert);

   %nukeAlert.schedule(5 * 1000, "delete"); // Delte the warning way point after 5 seconds.
   centerPrintAll("Incoming Planetary Cannon Blast! Get your ass under cover!", 4, 2);
   messageAll('',"~wfx/misc/red_alert.wav");// Alarm Sound
}

function TargeterShot::onExplode(%data, %proj, %pos, %mod)
{
BigFatNukeTarget(%pos);
}

function GameConnection::Givepce(%cl)
{
%cl.player.MountImage(NukeImage,0);
}



function nukewind(%pos,%start,%maxrad,%lastar)
{

%rad = nukerad(%start);

nukewindset(%pos, %rad,%lastar);
%lastar = %rad;
if (%rad < %maxrad)
    schedule(500,0,"nukewind",%pos,%start,%maxrad,%lastar);
}

function nukewindset(%pos, %area,%lastar)
{
if (%area > 2000)
   %area = 2000;

  InitContainerRadiusSearch(%pos, %area, $TypeMasks::VehicleObjectType | $TypeMasks::PlayerObjectType |$TypeMasks::StaticShapeObjectType| $TypeMasks::ItemObjectType | $TypeMasks::CorpseObjectType); // );

   %numTargets = 0;
   while ((%targetObject = containerSearchNext()) != 0)
   {
      %dist = containerSearchCurrRadDamageDist();
      
      if (%dist > %area)
         continue;

      %targets[%numTargets]     = %targetObject;
      %targetDists[%numTargets] = %dist;
      %numTargets++;
   }

   for (%i = 0; %i < %numTargets; %i++)
   {
      %targetObject = %targets[%i];
      %dist = %targetDists[%i];
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


function nukerad(%start)
{
%seconds = (GetSimTime()-%start)/1000;
%rad = %seconds * 200 + Mpow(%seconds,2)*1;
return %rad;
}
