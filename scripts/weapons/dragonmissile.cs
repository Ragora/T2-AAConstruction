$WeaponSettings1[MissileTransformer] = "9 -1 TractorGun";
$WeaponSetting1[MissileTransformer,0] = "Travel Time: 1 second [35m]";
$WeaponSetting1[MissileTransformer,1] = "Travel Time: 2 seconds [120m]";
$WeaponSetting1[MissileTransformer,2] = "Travel Time: 3 seconds [255m]";
$WeaponSetting1[MissileTransformer,3] = "Travel Time: 4 seconds [440m]";
$WeaponSetting1[MissileTransformer,4] = "Travel Time: 5 seconds [675m]";
$WeaponSetting1[MissileTransformer,5] = "Travel Time: 6 seconds [960m]";
$WeaponSetting1[MissileTransformer,6] = "Travel Time: 7 seconds [1.3km]";
$WeaponSetting1[MissileTransformer,7] = "Travel Time: 8 seconds [1.7km]";
$WeaponSetting1[MissileTransformer,8] = "Travel Time: 9 seconds [2.1km]";
$WeaponSetting1[MissileTransformer,9] = "Travel Time: 10 seconds [2.6km]";


datablock ParticleData( D_GDebrisSmokeParticle )
{
   dragCoeffiecient     = 1.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;

   lifetimeMS           = 750;  
   lifetimeVarianceMS   = 100;

   textureName          = "particleTest";

   useInvAlpha =     true;

   spinRandomMin = -60.0;
   spinRandomMax = 60.0;

   colors[0]     = "0.4 0.4 0.4 1.0";
   colors[1]     = "0.3 0.3 0.3 0.1";
   colors[2]     = "0.0 0.0 0.0 0.0";
   sizes[0]      = 1.1;
   sizes[1]      = 4.0;
   sizes[2]      = 3.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( D_GDebrisSmokeEmitter )
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 1;

   ejectionVelocity = 2.0;  // A little oomph at the back end
   velocityVariance = 0.2;

   thetaMin         = 0.0;
   thetaMax         = 0.0;

   particles = "D_GDebrisSmokeParticle";
};


datablock DebrisData(D_Debris)
{
   emitters[0] = D_GDebrisSmokeEmitter;

   explodeOnMaxBounce = true;

   elasticity = 0.4;
   friction = 0.2;

   lifetime = 0.2;
   lifetimeVariance = 1;

   numBounces = 10;
};             


datablock ParticleData(D_Dust)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 2000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.3 0.3 0.3 0.5";
   colors[1]     = "0.3 0.3 0.3 0.5";
   colors[2]     = "0.3 0.3 0.3 0.0";
   sizes[0]      = 5.2;
   sizes[1]      = 7.6;
   sizes[2]      = 7.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(D_DustEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 15.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 250;
   particles = "D_Dust";
};


datablock ParticleData(D_ESmoke)
{
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = -0.5;   // rises slowly
   inheritedVelFactor   = 0.025;

   lifetimeMS           = 1750;
   lifetimeVarianceMS   = 0;

   textureName          = "particleTest";

   useInvAlpha =  true;
   spinRandomMin = -200.0;
   spinRandomMax =  200.0;

   textureName = "special/Smoke/smoke_001";

   colors[0]     = "0.8 0.4 0.2 1.0";
   colors[1]     = "0.5 0.3 0.1 1.0";
   colors[2]     = "0.1 0.1 0.1 0.0";
   sizes[0]      = 2.0;
   sizes[1]      = 6.0;
   sizes[2]      = 2.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(D_ESmokeEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionVelocity = 8.25;
   velocityVariance = 0.25;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   lifetimeMS       = 250;

   particles = "D_ESmoke";
};



datablock ParticleData(D_Sparks)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 350;
   textureName          = "special/bigspark";
   colors[0]     = "0.56 0.36 0.26 1.0";
   colors[1]     = "0.56 0.36 0.26 1.0";
   colors[2]     = "1.0 0.36 0.26 0.0";
   sizes[0]      = 2.5;
   sizes[1]      = 2.5;
   sizes[2]      = 2.75;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(D_SparksEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;
   ejectionVelocity = 12;
   velocityVariance = 6.75;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "D_Sparks";
};





datablock ExplosionData(D_Explosion)
{
   soundProfile   = GrenadeExplosionSound;

   faceViewer           = true;
   explosionScale = "0.9 0.9 0.9";

   debris = D_Debris;
   debrisThetaMin = 10;
   debrisThetaMax = 50;
   debrisNum = 50;
   debrisVelocity = 20.0;
   debrisVelocityVariance = 10.0;

   emitter[0] = D_DustEmitter;
   emitter[1] = D_ESmokeEmitter;
   emitter[2] = D_SparksEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 6.0 9.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 20.0;
};

datablock TracerProjectileData(LHE):Mpm_G_PR {
	Explosion = "D_Explosion";
	};

function GameConnection::flukeattack(%cl)
{
%image = %cl.player.getMountedImage(0);
if (!IsObject(%image))
    return "";
%image.onFire(%cl.player,0);
}

datablock ParticleData( LoadingP1 )
{
   dragCoeffiecient     = 0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = -1.0;
   constantAcceleration = -4;
   lifetimeMS           = 850;  
   lifetimeVarianceMS   = 0;
   windCoefficient = 0.0;
   textureName          = "flarebase";

   useInvAlpha =     false;

   spinRandomMin = 0.0;
   spinRandomMax = 0.0;

   colors[0]     = "0.20 0.20 1 1.0";
   colors[1]     = "0.20 0.20 1 1.0";
   colors[2]     = "0.20 0.20 1 1.0";
   colors[3]     = "0.2 0.2 1 0.0";
   sizes[0]      = 0.05;
   sizes[1]      = 0.1;
   sizes[2]      = 0.2;
   sizes[3]      = 0.8;
   times[0]      = 0.5;
   times[1]      = 0.7;
   times[2]      = 0.90;
   times[3]      = 1.0;
};



datablock ParticleEmitterData( LoadingE2 )
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 1;

   ejectionVelocity = 1.7;  // A little oomph at the back end
   velocityVariance = 0.0;
   ejectionoffset = 0.8;
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   phiReferenceVel = "0";
   phiVariance = "360";
   particles = "LoadingP1";
};


datablock ParticleData( Loading1P )
{
   dragCoeffiecient     = 0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = -1.0;
   constantAcceleration = -4;
   lifetimeMS           = 850;  
   lifetimeVarianceMS   = 0;
   windCoefficient = 0.0;
   textureName          = "flarebase";

   useInvAlpha =     false;

   spinRandomMin = 0.0;
   spinRandomMax = 0.0;

   colors[0]     = "0.20 0.20 1 1.0";
   colors[1]     = "0.20 0.20 1 1.0";
   colors[2]     = "0.20 0.20 1 1.0";
   colors[3]     = "0.2 0.2 1 0.0";
   sizes[0]      = 0.05;
   sizes[1]      = 0.1;
   sizes[2]      = 0.2;
   sizes[3]      = 0.8;
   times[0]      = 0.5;
   times[1]      = 0.7;
   times[2]      = 0.90;
   times[3]      = 1.0;
};



datablock ParticleEmitterData( Loading1E )
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 1;

   ejectionVelocity = 1.7;  // A little oomph at the back end
   velocityVariance = 0.0;
   ejectionoffset = 0.8;
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   phiReferenceVel = "0";
   phiVariance = "360";
   particles = "Loading1P";
};


datablock ParticleData( LoadingP )
{
   dragCoeffiecient     = 0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0;
   lifetimeMS           = 70;  
   lifetimeVarianceMS   = 0;
   windCoefficient = 0.0;
   textureName          = "flarebase";

   useInvAlpha =     false;

   spinRandomMin = 0.0;
   spinRandomMax = 0.0;

   colors[0]     = "0.20 0.20 1 1.0";
   colors[1]     = "0.20 0.20 1 1.0";
   colors[2]     = "0.20 0.20 1 1.0";
   colors[3]     = "0.2 0.2 1 0.0";
   sizes[0]      = 0.9;
   sizes[1]      = 1;
   sizes[2]      = 1;
   sizes[3]      = 0.9;
   times[0]      = 0.25;
   times[1]      = 0.5;
   times[2]      = 0.75;
   times[3]      = 1.0;
};



datablock ParticleEmitterData( LoadingE )
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;  // A little oomph at the back end
   velocityVariance = 0.0;
   ejectionoffset = 1.5;
   thetaMin         = 0.0;
   thetaMax         = 5.0;
   phiReferenceVel = "0";
   phiVariance = "360";
   particles = "LoadingP";
   orientParticles  = false;
   orientOnVelocity = false;
};



datablock SeekerProjectileData(TransformerMissile):ShoulderMissile
{
casingShapeName     = "weapon_missile_casement.dts";
   directDamage        =0;
   directDamageType    = $DamageType::SuperChaingun;
  
   hasDamageRadius     = False;
   indirectDamage      = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::SuperChaingun;

   //muzzleVelocity      = 50;
   maxVelocity         = 800;
   //turningSpeed        = 0.0;
   acceleration        = 50;
   exhaustEmitter      = LoadingE;
   exhaustTimeMs       = 10000;
   exhaustNodeName     = "muzzlePoint1";
   lifetimems          = 12000;
};

//make sure.
TransformerMissile.lifetimems = 12000;

datablock ShapeBaseImageData(MissileTransformer):NerfBallLauncherImage
{
   className = WeaponImage;
   shapeFile = "weapon_grenade_launcher.dts";
   item = TransGun;
   usesEnergy = true;
   fireEnergy = 50;
   minEnergy = 50;
   ammo = "";
   offset = "0 0 0";
   emap = true;

   projectile = TransformerMissile;
   projectileType = SeekerProjectile;

   projectileSpread = 30.0 / 1000.0;
   stateSound[3] = MissileFireSound;

};
datablock ItemData(TransGun)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = MissileTransformer;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a trasportation gun";
};


function TransformerMissile::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal)
{
%client=%projectile.cl;
Cancel(%client.endobsch);
%client.player.setTransform(%position);
%client.player.schedule(100,blowup); // chunkOrama!
%client.player.schedule(100,scriptkill,$DamageType::Crash);
Parent::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal);
}

function TransformerMissile::onExplode(%data, %proj, %pos, %mod)
{
%client=%proj.cl;
Cancel(%client.endobsch);
%client.player.setTransform(%pos);
%client.player.schedule(100,blowup); // chunkOrama!
%client.player.schedule(100,scriptkill,$DamageType::Crash);
Parent::onExplode(%data, %proj, %pos, %mod);
}

// Bot fun!
function MissileTransformer::onFire(%data,%obj,%slot) 
{
//parent::onFire(%data,%obj,%slot);
//%client = %obj.client;
//%p = TransformerMissile1.Create(%client.player.getMuzzlePoint(0),%client.player.getMuzzleVector(0),%client.player.getVelocity());
testobs(%obj.client);
}



function testobs(%client)
{
%p = TransformerMissile.Create(%client.player.getMuzzlePoint(0),%client.player.getMuzzleVector(0),%client.player.getVelocity());
%p.dir = %client.player.getMuzzleVector(0);
%p.rot = %client.player.getRotation();
%p.cl = %client;

%time = %client.player.weaponSet1+1;
if (%time $= "" || %time < 1 || %time > 10)
    %time = 5;

if(!isObject(%p))
  return"";

if ( !isObject( %client.comCam ) )
   {
      %client.comCam = new Camera()
      {
         dataBlock = CommanderCamera;
      };
      MissionCleanup.add(%client.comCam);
   }
   //commandToClient(%client, 'ControlObjectResponse', true, getControlObjectType(%p,%client.player));
   //messageClient(%colObj.client, 'CloseHud', "", 'inventoryScreen');
   %client.comCam.setTransform(%p.getTransform());
   %client.comCam.setOrbitMode(%p,%p.getTransform(),0,10,-10);
    
   %client.setControlObject(%client.comCam);
   %client.moveprojectile = %p;
   commandToClient(%client, 'CameraAttachResponse', true);
   %client.player.startfade(0,0,1);
   if (isObject(%client.player.getobjectMount()))
       %client.player.unmount();
   %client.player.setTransform(VectorAdd(%client.player.getTransform(),"0 0 -50000"));
   schedule(750,0,"checkobs",%client);


   %client.endobsch = schedule(%time*1000,0,"endobs",%client);
}

function checkobs(%client)
{
if (!isObject(%client.moveprojectile))
   {
   %pos = %client.comCam.getTransform();
   Cancel(%client.endobsch);
   %client.player.setTransform(%pos);
   %client.player.schedule(100,blowup); // chunkOrama!
   %client.player.schedule(100,scriptkill,$DamageType::Crash);
   PlayExplosion(%pos,LHE,"0 0 1");
   return "";
   }
}

function endobs(%client)
{
if (!isObject(%client.moveprojectile))
   {
   %pos = %client.comCam.getTransform();
   Cancel(%client.endobsch);
   %client.player.setTransform(%pos);
   %client.player.schedule(100,blowup); // chunkOrama!
   %client.player.schedule(100,scriptkill,$DamageType::Crash);
   PlayExplosion(%pos,LHE,"0 0 1");
   return "";
   }
%client.player.setTransform(getWords(%client.moveprojectile.getTransform(),0,2) SPC %client.moveporjectile.rot);
%client.player.startfade(0,0,0);
%client.player.setVelocity("0 0 0");
%client.player.applyImpulse(%client.player.getTransform(),VectorScale(%client.moveprojectile.dir,20000));
%client.setControlObject(%client.player);
%client.moveprojectile.delete();

//%client.comCam.delete();
}
