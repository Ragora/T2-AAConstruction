///Telleport missile

datablock ParticleData(Mpm_B_MIS_P1)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 1.0;
   
   lifetimeMS           = 8000;
   lifetimeVarianceMS   = 0;

   spinRandomMin = 0.0;
   spinRandomMax = 0.0;
   windcoefficient = 0;
   textureName          = "special/lensflare/flare00";

   colors[0]     = "0.3 0.3 1.0 0";
   colors[1]     = "0.3 0.3 1.0 1";
   colors[2]     = "0.3 0.3 1.0 1";
   colors[3]     = "0.3 0.3 1.0 0.1";

   sizes[0]      = 0;
   sizes[1]      = 10;
   sizes[2]      = 5;
   sizes[3]      = 20;

   times[0]      = 0.1;
   times[1]      = 0.2;
   times[2]      = 0.3;
   times[3]      = 1;

};

datablock ParticleEmitterData(Mpm_B_MIS_PE1)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1;
   velocityVariance = 0.0;
   ejectionoffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 0.0;
	
   phiReferenceVel = "0";
   phiVariance = "360";
   orientParticles  = true;
   orientOnVelocity = true;

   particles = "Mpm_B_MIS_P1";
};

datablock ParticleData(Mpm_B_MIS_P2):Mpm_B_MIS_P1
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 1.0;
   
   lifetimeMS           = 1500;
   lifetimeVarianceMS   = 0;

   spinRandomMin = 0.0;
   spinRandomMax = 0.0;
   windcoefficient = 0;
   textureName          = "special/lensflare/flare00";

   colors[0]     = "0.3 0.3 1.0 0";
   colors[1]     = "0.3 0.3 1.0 1";
   colors[2]     = "0.3 0.3 1.0 1";
   colors[3]     = "0.3 0.3 1.0 0.1";

   sizes[0]      = 0;
   sizes[1]      = 8;
   sizes[2]      = 8;
   sizes[3]      = 20;

   times[0]      = 0.3;
   times[1]      = 0.5;
   times[2]      = 0.8;
   times[3]      = 1;

};

datablock ParticleEmitterData(Mpm_B_MIS_PE2):Mpm_B_MIS_PE1
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 50;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0.0;
   ejectionoffset = 0;
   thetaMin         = 140.0;
   thetaMax         = 160.0;
	

   orientParticles  = false;
   orientOnVelocity = false;

   particles = "Mpm_B_MIS_P2";
};




datablock SeekerProjectileData(Mpm_B_MIS):MpmMissile1
{
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "turret_muzzlePoint.dts";
   

   explosion           = "GrenadeExplosion";
   splash              = MissileSplash;
   
   baseEmitter         = Mpm_B_MIS_PE1;
   delayEmitter        = Mpm_B_MIS_PE2;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = -1;
   muzzleVelocity      = 0.1;
   maxVelocity         = 80000;
   turningSpeed        = 0.0;
   acceleration        = 3;
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
   lightColor  = "0 0 1";

   useFlechette = false;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = true;
};

//Multi WarHead Missiles

datablock ParticleData(Mpm_B_MIS1_P1)
{
   dragCoefficient      = 5;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0;
   lifetimeMS           = 30000;
   lifetimeVarianceMS   = 0;
   textureName          = "special/BigSpark";
    windcoefficient = 0;
   colors[0]     = "0.6 0.6 0.6 1";
   colors[1]     = "0.6 0.6 0.6 1";
   colors[2]     = "0.6 0.6 0.6 1";
   colors[3]     = "0.2 0.2 0.2 1";
   sizes[0]      = 0;
   sizes[1]      = 15;
   sizes[2]      = 5;
   sizes[3]      = 0;
   times[0]      = 0.0;
   times[1]      = 0.04;
   times[2]      = 0.08;
   times[3]      = 1;

};

datablock ParticleEmitterData(Mpm_B_MIS1_PE1)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 10;
   velocityVariance = 0.0;
   ejectionoffset = 2;
   thetaMin         = 180.0;
   thetaMax         = 180.0;
	
   phiReferenceVel = "0";
   phiVariance = "0";
   orientParticles  = true;
   orientOnVelocity = false;

   particles = "Mpm_B_MIS1_P1";
};

datablock ParticleData(Mpm_B_MIS1_P2):Mpm_B_MIS_P1
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 1.0;
   constantAcceleration = -0.25;
   lifetimeMS           = 10000;
   lifetimeVarianceMS   = 0;

   spinRandomMin = 0.0;
   spinRandomMax = 0.0;
   windcoefficient = 0;
   textureName          = "special/BigSpark";

   colors[0]     = "1 1 1.0 1";
   colors[1]     = "1 1 1.0 1";
   colors[2]     = "1 1 1.0 1";
   colors[3]     = "1 1 1.0 0.1";

   sizes[0]      = 0;
   sizes[1]      = 5;
   sizes[2]      = 3;
   sizes[3]      = 0;

    times[0]      = 0.0;
   times[1]      = 0.04;
   times[2]      = 0.08;
   times[3]      = 1;

};

datablock ParticleEmitterData(Mpm_B_MIS1_PE2):Mpm_B_MIS_PE1
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 50;
   periodVarianceMS = 0;

   ejectionVelocity = 5;
   velocityVariance = 0.0;
   ejectionoffset = 5;
   thetaMin         = 150.0;
   thetaMax         = 180.0;
	 phiReferenceVel = "0";
   phiVariance = "360";

   orientParticles  = true;
   orientOnVelocity = true;

   particles = "Mpm_B_MIS1_P2";
};



datablock SeekerProjectileData(Mpm_B_MIS1):MpmMissile1
{
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "turret_muzzlePoint.dts";
   

   explosion           = "GrenadeExplosion";
   splash              = MissileSplash;
   
   baseEmitter         = Mpm_B_MIS1_PE1;
   delayEmitter        = Mpm_B_MIS1_PE2;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = -1;
   muzzleVelocity      = 0.1;
   maxVelocity         = 80000;
   turningSpeed        = 0.0;
   acceleration        = 3;
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



//Anti Missile

datablock ParticleData(Mpm_B_MIS2_P1)
{
   dragCoefficient      = 5;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0;
   lifetimeMS           = 30000;
   lifetimeVarianceMS   = 0;
   textureName          = "special/sniper00"; 
   windcoefficient = 0;
   colors[0]     = "0.6 0.6 0.6 1";
   colors[1]     = "0.6 0.6 0.6 1";
   colors[2]     = "0.6 0.6 0.6 1";
   colors[3]     = "0.2 0.2 0.2 0";
   sizes[0]      = 0;
   sizes[1]      = 5;
   sizes[2]      = 2;
   sizes[3]      = 2;
   times[0]      = 0.0;
   times[1]      = 0.04;
   times[2]      = 0.08;
   times[3]      = 1;

};

datablock ParticleEmitterData(Mpm_B_MIS2_PE1)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 0;
   velocityVariance = 0.0;
   ejectionoffset = 2;
   thetaMin         = 180.0;
   thetaMax         = 180.0;
	
   phiReferenceVel = "0";
   phiVariance = "0";
   orientParticles  = true;
   orientOnVelocity = false;

   particles = "Mpm_B_MIS2_P1";
};

datablock ParticleData(Mpm_B_MIS2_P2):Mpm_B_MIS_P1
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 1.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 0;

   spinRandomMin = -30.0;
   spinRandomMax = 30.0;
   windcoefficient = 0.5;
   textureName          = "special/flare3";
   colors[0]     = "1 1 0.0 1";
   colors[1]     = "1 1 0.0 1";
   colors[2]     = "1 1 0.0 1";
   colors[3]     = "1 1 0.0 0.1";

   sizes[0]      = 10;
   sizes[1]      = 10;
   sizes[2]      = 10;
   sizes[3]      = 10;

    times[0]      = 0.4;
   times[1]      = 0.6;
   times[2]      = 0.8;
   times[3]      = 1;

};

datablock ParticleEmitterData(Mpm_B_MIS2_PE2):Mpm_B_MIS_PE1
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0.0;
   ejectionoffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 0.0;
   phiReferenceVel = "0";
   phiVariance = "0";

   orientParticles  = false;
   orientOnVelocity = false;

   particles = "Mpm_B_MIS2_P2";
};

datablock SeekerProjectileData(Mpm_B_MIS2):MpmMissile1
{
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "turret_muzzlePoint.dts";
   

   explosion           = "GrenadeExplosion";
   splash              = MissileSplash;
   
   baseEmitter         = Mpm_B_MIS2_PE1;
   delayEmitter        = Mpm_B_MIS2_PE2;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = -1;
   muzzleVelocity      = 20.0;
   maxVelocity         = 20;
   turningSpeed        = 0.0;
   acceleration        = 0;
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
   lightColor  = "1 0 0";

   useFlechette = false;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = true;
};




//Vehicle Missile

datablock ParticleData(Mpm_B_MIS3_P1)
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

   colors[0]     = "0.3 0.8 0.6 1";
   colors[1]     = "0.3 0.8 0.0 0.9";
   colors[2]     = "0.3 0.8 0.6 0.5";
   colors[3]     = "0.3 0.8 0.6 0.0";

   sizes[0]      = 5;
   sizes[1]      = 17;
   sizes[2]      = 18;
   sizes[3]      = 20;

   times[0]      = 0;
   times[1]      = 0.25;
   times[2]      = 0.5;
   times[3]      = 0.75;


};

datablock ParticleEmitterData(Mpm_B_MIS3_PE1)
{
  
   lifetimeMS        = 10;
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;

   ejectionVelocity = 10.0;
   velocityVariance = 1.0;
   ejectionoffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 5.0;


   orientParticles  = false;
   orientOnVelocity = false;

   particles = "Mpm_B_MIS3_P1";
};

datablock ParticleData(Mpm_B_MIS3_P2):Mpm_B_MIS_P1
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 1.0;
   
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;

   spinRandomMin = -160.0;
   spinRandomMax =  160.0;
   windcoefficient = 0;
   textureName = "skins/flaregreen";
   UseInvAlpha = false;
   colors[0]     = "0.7 0.7 1.0 0.5";
   colors[1]     = "0.7 0.7 1.0 0.5";
   colors[2]     = "0.7 0.7 1.0 0.5";
   colors[3]     = "0.7 0.7 1.0 0.5";

   sizes[0]      = 20;
   sizes[1]      = 20;
   sizes[2]      = 20;
   sizes[3]      = 20;

   times[0]      = 0.25;
   times[1]      = 0.25;
   times[2]      = 0.25;
   times[3]      = 1;
 
};

datablock ParticleEmitterData(Mpm_B_MIS3_PE2):Mpm_B_MIS_PE1
{
  lifetimeMS        = 10;
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0.0;
   ejectionoffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 0.0;
	 phiReferenceVel = "0";
   phiVariance = "0";

   orientParticles  = false;
   orientOnVelocity = false;

   particles = "Mpm_B_MIS3_P2";
};

datablock SeekerProjectileData(Mpm_B_MIS3):MpmMissile1
{
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "turret_muzzlePoint.dts";
   

   explosion           = "GrenadeExplosion";
   splash              = MissileSplash;
   
   baseEmitter         = Mpm_B_MIS3_PE1;
   delayEmitter        = Mpm_B_MIS3_PE2;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = -1;
   muzzleVelocity      = 0.1;
   maxVelocity         = 80000;
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
   lightColor  = "0 1 0";

   useFlechette = false;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = true;
};



//Aid Missile

datablock ParticleData(Mpm_B_MIS4_P1)
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

   colors[0]     = "0.3 0.3 0.8 1";
   colors[1]     = "0.0 0.0 0.8 0.9";
   colors[2]     = "0.3 0.3 0.8 0.5";
   colors[3]     = "0.3 0.3 0.8 0.0";

   sizes[0]      = 5;
   sizes[1]      = 17;
   sizes[2]      = 18;
   sizes[3]      = 20;

   times[0]      = 0;
   times[1]      = 0.25;
   times[2]      = 0.5;
   times[3]      = 0.75;


};

datablock ParticleEmitterData(Mpm_B_MIS4_PE1)
{
  
   lifetimeMS        = 10;
   ejectionPeriodMS = 50;
   periodVarianceMS = 0;

   ejectionVelocity = 10.0;
   velocityVariance = 1.0;
   ejectionoffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 5.0;


   orientParticles  = false;
   orientOnVelocity = false;

   particles = "Mpm_B_MIS4_P1";
};

datablock ParticleData(Mpm_B_MIS4_P2):Mpm_B_MIS_P1
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 1.0;
   
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;

   spinRandomMin = -160.0;
   spinRandomMax =  160.0;
   windcoefficient = 0;
   textureName = "skins/flaregreen";
   UseInvAlpha = false;
   colors[0]     = "0.2 0.2 1.0 0.5";
   colors[1]     = "0.2 0.2 1.0 0.5";
   colors[2]     = "0.2 0.2 1.0 0.5";
   colors[3]     = "0.2 0.2 1.0 0.5";

   sizes[0]      = 20;
   sizes[1]      = 20;
   sizes[2]      = 20;
   sizes[3]      = 20;

   times[0]      = 0.25;
   times[1]      = 0.25;
   times[2]      = 0.25;
   times[3]      = 1;
 
};

datablock ParticleEmitterData(Mpm_B_MIS4_PE2):Mpm_B_MIS_PE1
{
  lifetimeMS        = 10;
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0.0;
   ejectionoffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 0.0;
	 phiReferenceVel = "0";
   phiVariance = "0";

   orientParticles  = false;
   orientOnVelocity = false;

   particles = "Mpm_B_MIS4_P2";
};

datablock SeekerProjectileData(Mpm_B_MIS4):MpmMissile1
{
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "turret_muzzlePoint.dts";
   

   explosion           = "GrenadeExplosion";
   splash              = MissileSplash;
   
   baseEmitter         = Mpm_B_MIS4_PE1;
   delayEmitter        = Mpm_B_MIS4_PE2;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = -1;
   muzzleVelocity      = 0.1;
   maxVelocity         = 80000;
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
   lightColor  = "0 0 1";

   useFlechette = false;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = true;
};


