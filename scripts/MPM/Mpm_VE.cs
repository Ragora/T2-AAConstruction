if ($mpm_VE!=1)
   {
   $mpm_load[$mpm_loads] = Mpm_VE_Load0;
   $mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_VE_Load1;
   $mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_VE_Load2;
   $mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_VE_Load3;
   $mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_VE_Load4;
   $mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_VE_Load5;
   $mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_VE_Load6;
   $mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_VE_Load7;
   $mpm_loads++;
   $mpm_load[$mpm_loads] = Mpm_VE_Load8;
   $mpm_loads++;
   $mpm_VE = 1;
   }

datablock ParticleData(Mpm_VE_P1)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   
   lifetimeMS           = 2500;
   lifetimeVarianceMS   = 0;

   spinRandomMin = 0.0;
   spinRandomMax = 0.0;
   windcoefficient = 0;
  textureName          = "special/GameGrid";

   colors[0]     = "0.3 0.3 1.0 0.1";
   colors[1]     = "0.3 0.3 1.0 1";
   colors[2]     = "0.3 0.3 1.0 1";
   colors[3]     = "0.3 0.3 1.0 0.1";

   sizes[0]      = 5;
   sizes[1]      = 5;
   sizes[2]      = 5;
   sizes[3]      = 5;

   times[0]      = 0.1;
   times[1]      = 0.5;
   times[2]      = 0.9;
   times[3]      = 1;

};


datablock ParticleData(Mpm_VE_P2)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   
   lifetimeMS           = 2500;
   lifetimeVarianceMS   = 0;

   spinRandomMin = 0.0;
   spinRandomMax = 0.0;
   windcoefficient = 0;
   textureName          = "special/GameGrid";


   colors[0]     = "0.3 0.3 1.0 0.1";
   colors[1]     = "0.3 0.3 1.0 1";
   colors[2]     = "0.3 0.3 1.0 1";
   colors[3]     = "0.3 0.3 1.0 0.1";

   sizes[0]      = 5;
   sizes[1]      = 5;
   sizes[2]      = 5;
   sizes[3]      = 5;

    times[0]      = 0.1;
   times[1]      = 0.5;
   times[2]      = 0.9;
   times[3]      = 1;


};

datablock ParticleEmitterData(Mpm_VE_PE1)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 15;
   periodVarianceMS = 0;

   ejectionVelocity = 0.1;
   velocityVariance = 0.0;
   ejectionoffset = 8;
   thetaMin         = 70.0;
   thetaMax         = 70.0;
	
   phiReferenceVel = "180";
   phiVariance = "5";
   orientParticles  = true;
   orientOnVelocity = false;

   particles = "Mpm_VE_P1";
};


datablock ParticleEmitterData(Mpm_VE_PE2)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 15;
   periodVarianceMS = 0;

   ejectionVelocity = 0.01;
   velocityVariance = 0.0;
   ejectionoffset = 8;
   thetaMin         = 30.0;
   thetaMax         = 30.0;
	
   phiReferenceVel = "180";
   phiVariance = "5";
  orientParticles  = true;
   orientOnVelocity = false;


   particles = "Mpm_VE_P2";
};

datablock ItemData(Mpm_VE_Load0) 
{
   cost = 20;
   missile = Mpm_B_MIS3;
   name = "[Vehicle] Grav Cycle Missile";
   friendly = 1;
   vehicle = ScoutVehicle; 
   slot = 0; 
};

datablock ItemData(Mpm_VE_Load1):Mpm_VE_Load0 
{
   cost = 40;
   name = "[Vehicle] Tank Missile";
   vehicle = AssaultVehicle;  
};
datablock ItemData(Mpm_VE_Load2):Mpm_VE_Load0
{
   cost = 100;
   name = "[Vehicle] Mpb Missile";
   vehicle = MobileBaseVehicle;  
};

datablock ItemData(Mpm_VE_Load3):Mpm_VE_Load0 
{
   cost = 25;
   name = "[Vehicle] Shrike Missile";
   vehicle = ScoutFlyer;  
};
datablock ItemData(Mpm_VE_Load4):Mpm_VE_Load0
{
   cost = 50;
    name = "[Vehicle] Bomber Missile";
   vehicle = BomberFlyer;  
};

datablock ItemData(Mpm_VE_Load5):Mpm_VE_Load0 
{
   cost = 80;
   name = "[Vehicle] Havoc Missile";
   vehicle = HAPCFlyer;  
};
datablock ItemData(Mpm_VE_Load6):Mpm_VE_Load0
{
   cost = 800;
    name = "[Vehicle] Super Grav Cycle Missile";
   vehicle = SuperScoutVehicle;  
};

datablock ItemData(Mpm_VE_Load7):Mpm_VE_Load0 
{
   cost = 800;
   name = "[Vehicle] Super Havoc Missile";
   vehicle = SuperHAPCFlyer;  
};
datablock ItemData(Mpm_VE_Load8):Mpm_VE_Load0
{
   cost = 700;
    name = "[Vehicle] Artillery Missile";
   vehicle = Artillery;  
};

function Mpm_VE_Load0::AtTarget(%data,%p)
{
if (IsObject(%p) && vehicleCheck(%p.load.vehicle, %p.source.team))
	{
        %p1 = CreateEmitter(%p.getTransform(),Mpm_VE_PE1);
        %p2 = CreateEmitter(%p.getTransform(),Mpm_VE_PE2);
        $VehicleTotalCount[%p.source.team, %p.load.vehicle]++;
        %vehicle = %p.load.vehicle.create(%p.source.team);
        %vehicle.telleport(VectorAdd(%p.getTransform(),"0 0 10"));
        %p1.schedule(8000,"delete");
        %p2.schedule(8000,"delete");
	}
}
